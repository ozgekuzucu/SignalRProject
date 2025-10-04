using SignalR.EntityLayer.Entities;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
	public interface IBookingDal : IGenericDal<Booking>
	{
		void BookingStatusApproved(int id);
		void BookingStatusCancelled(int id);
	}
}
