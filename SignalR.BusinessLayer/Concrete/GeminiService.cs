using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;
using System.Text;
using System.Text.Json;

namespace SignalR.BusinessLayer.Concrete
{
	public class GeminiService : IOpenAIService
	{
		private readonly string _apiKey;
		private readonly HttpClient _httpClient;

		public GeminiService(IConfiguration configuration)
		{
			_apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini API Key is required");
			_httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
		}

		public async Task<string> GetChatResponseAsync(List<ChatMessage> conversationHistory, int? tableNumber = null, int? orderId = null)
		{
			try
			{
				Order? order = null;
				List<Product>? menuItems = null;

				using (var db = new SignalRContext())
				{
					try
					{
						if (orderId.HasValue)
						{
							order = await db.Orders
								.Include(o => o.OrderDetails)
								.ThenInclude(od => od.Product)
								.FirstOrDefaultAsync(o => o.OrderID == orderId.Value);
						}
						else if (tableNumber.HasValue)
						{
							var searchPatterns = new List<string>
							{
								$"Bahçe {tableNumber.Value:D2}",
								$"Salon {tableNumber.Value:D2}",
								$"Teras {tableNumber.Value:D2}",
								$"Masa {tableNumber.Value}"
							};

							order = await db.Orders
								.Include(o => o.OrderDetails)
								.ThenInclude(od => od.Product)
								.Where(o => searchPatterns.Contains(o.TableNumber)
									&& (o.Description == "Müşteri Masada" || o.Description == "Hazırlanıyor"))
								.OrderByDescending(o => o.OrderDate)
								.FirstOrDefaultAsync();
						}

						menuItems = await db.Products
							.Include(p => p.Category)
							.Where(p => p.ProductStatus)
							.ToListAsync();
					}
					catch (Exception dbEx)
					{
						Console.WriteLine($"DB Error: {dbEx.Message}");
					}
				}

				var prompt = BuildPrompt(conversationHistory, order, menuItems);
				var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.0-flash:generateContent?key={_apiKey}";

				var requestBody = new
				{
					contents = new[] { new { parts = new[] { new { text = prompt } } } },
					generationConfig = new { temperature = 0.7, maxOutputTokens = 800 }
				};

				var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
					return "Üzgünüm, şu anda yanıt veremiyorum.";

				var responseJson = await response.Content.ReadAsStringAsync();
				var jsonDoc = JsonSerializer.Deserialize<JsonElement>(responseJson);
				var text = jsonDoc
					.GetProperty("candidates")[0]
					.GetProperty("content")
					.GetProperty("parts")[0]
					.GetProperty("text")
					.GetString();

				return string.IsNullOrWhiteSpace(text) ? "Üzgünüm, yanıt oluşturamadım." : text;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Gemini Service Error: {ex.Message}");
				return "Üzgünüm, bir hata oluştu.";
			}
		}

		private string BuildPrompt(List<ChatMessage> conversationHistory, Order? order, List<Product>? menuItems)
		{
			var sb = new StringBuilder();

			sb.AppendLine("Sen bir restoran bilgilendirme asistanısın.");
			sb.AppendLine();
			sb.AppendLine("GÖREVLER:");
			sb.AppendLine("- Mevcut sipariş durumlarını sorgula ve bildir");
			sb.AppendLine("- Menü ve fiyat bilgilerini paylaş");
			sb.AppendLine("- Müşteri sorularını yanıtla");
			sb.AppendLine();
			sb.AppendLine("KURALLAR:");
			sb.AppendLine("- SADECE bilgi ver, sipariş ALMA");
			sb.AppendLine("- Yalnızca veritabanındaki bilgileri kullan");
			sb.AppendLine("- Net ve samimi cevaplar ver ve emojiler kullan");
			sb.AppendLine("- Türkçe konuş");
			sb.AppendLine();

			if (order != null)
			{
				sb.AppendLine("MEVCUT SİPARİŞ:");
				sb.AppendLine($"Masa: {order.TableNumber}");
				sb.AppendLine($"Durum: {order.Description}");
				sb.AppendLine($"Tarih: {order.OrderDate:dd.MM.yyyy HH:mm}");
				sb.AppendLine($"Toplam: {order.TotalPrice:F2} TL");
				sb.AppendLine();

				if (order.OrderDetails?.Any() == true)
				{
					sb.AppendLine("Ürünler:");
					foreach (var detail in order.OrderDetails)
					{
						sb.AppendLine($"  {detail.Product?.ProductName ?? "Bilinmeyen"} x{detail.Count} ({detail.UnitPrice:F2} TL) = {detail.TotalPrice:F2} TL");
					}
					sb.AppendLine();
				}
			}

			if (menuItems?.Any() == true)
			{
				sb.AppendLine("MENÜ:");
				var groupedMenu = menuItems.GroupBy(p => p.Category?.Name ?? "Diğer");

				foreach (var group in groupedMenu.Take(10))
				{
					sb.AppendLine($"{group.Key}:");
					foreach (var product in group.Take(8))
					{
						sb.AppendLine($"  {product.ProductName} - {product.Price:F2} TL");
					}
					sb.AppendLine();
				}
			}

			sb.AppendLine("SON KONUŞMA:");
			foreach (var msg in conversationHistory.TakeLast(5))
			{
				sb.AppendLine($"{(msg.Role == "user" ? "Müşteri" : "Asistan")}: {msg.Content}");
			}

			var lastMsg = conversationHistory.LastOrDefault(x => x.Role == "user");
			if (lastMsg != null)
			{
				sb.AppendLine();
				sb.AppendLine($"SORU: {lastMsg.Content}");
			}

			return sb.ToString();
		}
	}
}