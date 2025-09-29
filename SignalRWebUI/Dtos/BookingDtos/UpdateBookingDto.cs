using Newtonsoft.Json;

namespace SignalRWebUI.Dtos.BookingDtos
{
	
	public class UpdateBookingDto
	{
		[JsonProperty("bookingId")]
		public int BookingID { get; set; }   // View’de asp-for="BookingID"

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("mail")]
		public string Mail { get; set; }

		[JsonProperty("personCount")]
		public int PersonCount { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }
	}

}
