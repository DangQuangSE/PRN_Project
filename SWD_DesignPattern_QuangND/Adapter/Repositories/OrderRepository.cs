using Adapter.Interfaces;
using Adapter.Models;

namespace Adapter.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        private int _nextOrderId = 1;

        public Order Create(Order order)
        {
            order.Id = _nextOrderId++;
            order.OrderDate = DateTime.Now;
            _orders.Add(order);
            return order;
        }

        public List<Order> GetAll() => _orders.ToList();

        public Order GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public bool Update(int id, Order updatedOrder)
        {
            var index = _orders.FindIndex(o => o.Id == id);
            if (index == -1) return false;

            updatedOrder.Id = id;
            updatedOrder.OrderDate = _orders[index].OrderDate;
            _orders[index] = updatedOrder;
            return true;
        }

        public bool Delete(int id)
        {
            var order = GetById(id);
            if (order == null) return false;

            _orders.Remove(order);
            return true;
        }
    }
}
