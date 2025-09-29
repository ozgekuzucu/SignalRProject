using SignalR.EntityLayer.Entities;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
	public interface ICategoryDal : IGenericDal<Category>
	{
		int CategoryCount();
		int ActiveCategoryCount();
		int PassiveCategoryCount();
	}
}
