using Adapter.Models;

namespace Adapter.Interfaces
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        List<Order> GetAll();
        Order GetById(int id);
        bool Update(int id, Order updatedOrder);
        bool Delete(int id);
    }
}
