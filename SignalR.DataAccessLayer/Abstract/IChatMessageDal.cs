using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
	public interface IChatMessageDal : IGenericDal<ChatMessage>
	{
		List<ChatMessage> GetChatHistoryByConnectionId(string connectionId);
		List<ChatMessage> GetChatHistoryByTableNumber(int tableNumber);
		void DeleteOldMessages(DateTime olderThan);
	}
}
