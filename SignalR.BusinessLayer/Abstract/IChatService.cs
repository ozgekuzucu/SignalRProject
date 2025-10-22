using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Abstract
{
	public interface IChatService : IGenericService<ChatMessage>
	{
		List<ChatMessage> TGetChatHistoryByConnectionId(string connectionId);
		List<ChatMessage> TGetChatHistoryByTableNumber(int tableNumber);
		void TDeleteOldMessages(DateTime olderThan);
	}
}
