using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
	public class ChatService : IChatService
	{
		private readonly IChatMessageDal _chatMessageDal;

		public ChatService(IChatMessageDal chatMessageDal)
		{
			_chatMessageDal = chatMessageDal;
		}

		public void TAdd(ChatMessage entity)
		{
			_chatMessageDal.Add(entity);
		}

		public void TDelete(ChatMessage entity)
		{
			_chatMessageDal.Delete(entity);
		}

		public ChatMessage TGetById(int id)
		{
			return _chatMessageDal.GetById(id);
		}

		public List<ChatMessage> TGetListAll()
		{
			return _chatMessageDal.GetListAll();
		}

		public void TUpdate(ChatMessage entity)
		{
			_chatMessageDal.Update(entity);
		}

		public List<ChatMessage> TGetChatHistoryByConnectionId(string connectionId)
		{
			return _chatMessageDal.GetChatHistoryByConnectionId(connectionId);
		}

		public List<ChatMessage> TGetChatHistoryByTableNumber(int tableNumber)
		{
			return _chatMessageDal.GetChatHistoryByTableNumber(tableNumber);
		}

		public void TDeleteOldMessages(DateTime olderThan)
		{
			_chatMessageDal.DeleteOldMessages(olderThan);
		}
	}
}
