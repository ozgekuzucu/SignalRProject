using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace SignalRApi.Hubs
{
	public class ChatHub : Hub
	{
		private readonly IOpenAIService _geminiService;
		private readonly IChatService _chatService;
		private static readonly ConcurrentDictionary<string, List<ChatMessage>> _conversations = new();

		public ChatHub(IOpenAIService geminiService, IChatService chatService)
		{
			_geminiService = geminiService;
			_chatService = chatService;
		}

		public override async Task OnConnectedAsync()
		{
			_conversations.TryAdd(Context.ConnectionId, new List<ChatMessage>());
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			_conversations.TryRemove(Context.ConnectionId, out _);
			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(string userMessage)
		{
			try
			{
				var connectionId = Context.ConnectionId;

				if (string.IsNullOrWhiteSpace(userMessage))
				{
					await Clients.Caller.SendAsync("ReceiveMessage", "Lütfen bir mesaj yazın.");
					return;
				}

				if (!_conversations.TryGetValue(connectionId, out var conversation))
					return;

				var userChatMessage = new ChatMessage
				{
					ConnectionId = connectionId,
					Role = "user",
					Content = userMessage,
					CreatedDate = DateTime.Now
				};

				conversation.Add(userChatMessage);
				_chatService.TAdd(userChatMessage);

				var (tableNumber, orderId) = ExtractOrderContext(userMessage);

				var aiResponse = await _geminiService.GetChatResponseAsync(conversation, tableNumber, orderId);

				var aiChatMessage = new ChatMessage
				{
					ConnectionId = connectionId,
					Role = "assistant",
					Content = aiResponse,
					TableNumber = tableNumber,
					OrderId = orderId,
					CreatedDate = DateTime.Now
				};

				conversation.Add(aiChatMessage);
				_chatService.TAdd(aiChatMessage);

				if (conversation.Count > 20)
					conversation.RemoveRange(0, conversation.Count - 20);

				await Clients.Caller.SendAsync("ReceiveMessage", aiResponse);
			}
			catch (Exception ex)
			{
				await Clients.Caller.SendAsync("ReceiveMessage", "Üzgünüm, bir hata oluştu. Lütfen tekrar deneyin.");
			}
		}

		private (int? tableNumber, int? orderId) ExtractOrderContext(string message)
		{
			int? tableNumber = null;
			int? orderId = null;

			var tablePatterns = new[]
			{
				(@"salon\s*0?(\d+)", "Salon"),
				(@"bahçe\s*0?(\d+)", "Bahçe"),
				(@"bahce\s*0?(\d+)", "Bahçe"),
				(@"teras\s*0?(\d+)", "Teras"),
				(@"masa\s*0?(\d+)", "Masa"),
				(@"(\d+)\s*(?:numara|numaralı|nolu)?\s*masa", "Masa")
			};

			foreach (var (pattern, _) in tablePatterns)
			{
				var match = Regex.Match(message, pattern, RegexOptions.IgnoreCase);
				if (match.Success && int.TryParse(match.Groups[1].Value, out int num))
				{
					tableNumber = num;
					break;
				}
			}

			var orderMatch = Regex.Match(message, @"sipariş\s*#?(\d+)", RegexOptions.IgnoreCase);
			if (orderMatch.Success && int.TryParse(orderMatch.Groups[1].Value, out int order))
				orderId = order;

			return (tableNumber, orderId);
		}

		public async Task JoinTableGroup(int tableNumber)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, $"Table_{tableNumber}");
			await Clients.Caller.SendAsync("ReceiveMessage", $"Masa {tableNumber} grubuna katıldınız.");
		}

		public async Task LeaveTableGroup(int tableNumber)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Table_{tableNumber}");
		}

		public async Task SendMessageToTable(int tableNumber, string message)
		{
			await Clients.Group($"Table_{tableNumber}").SendAsync("ReceiveMessage", $"Restoran: {message}");
		}

		public async Task BroadcastMessage(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", $"Duyuru: {message}");
		}
	}
}