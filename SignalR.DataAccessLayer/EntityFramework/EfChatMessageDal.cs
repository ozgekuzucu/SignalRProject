using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
	public class EfChatMessageDal : GenericRepository<ChatMessage>, IChatMessageDal
	{
		public EfChatMessageDal(SignalRContext context) : base(context)
		{
		}

		public List<ChatMessage> GetChatHistoryByConnectionId(string connectionId)
		{
			using var context = new SignalRContext();
			return context.ChatMessages
				.Where(x => x.ConnectionId == connectionId)
				.OrderBy(x => x.CreatedDate)
				.Take(20) 
				.ToList();
		}

		public List<ChatMessage> GetChatHistoryByTableNumber(int tableNumber)
		{
			using var context = new SignalRContext();
			return context.ChatMessages
				.Where(x => x.TableNumber == tableNumber)
				.OrderByDescending(x => x.CreatedDate)
				.Take(50)
				.ToList();
		}

		public void DeleteOldMessages(DateTime olderThan)
		{
			using var context = new SignalRContext();
			var oldMessages = context.ChatMessages
				.Where(x => x.CreatedDate < olderThan)
				.ToList();

			context.ChatMessages.RemoveRange(oldMessages);
			context.SaveChanges();
		}
	}
}
