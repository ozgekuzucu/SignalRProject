using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ProductDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class MenuController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public MenuController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index(int id)
		{
			ViewBag.v = id; // Burada MenuTableId değerini ayarlıyoruz
							// TempData["x"] = id; // Eğer bunu kullanıyorsanız

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7101/api/Product/ProductListWithCategory");
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
			return View(values);
		}


	}
}
