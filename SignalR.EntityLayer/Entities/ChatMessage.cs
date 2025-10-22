using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.EntityLayer.Entities
{
	public class ChatMessage
	{
		public int ChatMessageId { get; set; }
		public string ConnectionId { get; set; }
		public string Role { get; set; }
		public string Content { get; set; }
		public int? TableNumber { get; set; }
		public int? OrderId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
