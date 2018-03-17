using System;
using System.Collections.Generic;
using System.Text;
using Orders.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private IList<Order> _orders;
        private readonly IOrderEventService _events;

        public OrderService(IOrderEventService events)
        {
            _orders = new List<Order>();
            _orders.Add(new Order("1000", "250 Conference brochures", DateTime.Now, 1, "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827"));
            _orders.Add(new Order("2000", "250 T-shirts", DateTime.Now.AddDays(-1), 2, "F43A4F9D-7AE9-4A19-93D9-2018387D5378"));
            _orders.Add(new Order("3000", "500 Stickers", DateTime.Now.AddDays(-1), 3, "2D542571-EF99-4786-AEB5-C997D82E57C7"));
            _orders.Add(new Order("4000", "10 Posters", DateTime.Now.AddDays(-1), 1, "8F2E3A32-EF09-45E7-88E9-6F62EBC47548"));
            _orders.Add(new Order("5000", "150 Hats", DateTime.Now.AddDays(-2), 2, "41D73692-104B-4615-AA10-B6F5B51BFAF5"));
            _orders.Add(new Order("6000", "750 Keychains", DateTime.Now.AddDays(-2), 3, "6E431741-F049-4B1A-BC64-408986BD209A"));
            _orders.Add(new Order("7000", "5 Drones", DateTime.Now.AddDays(-2), 1, "17EF9022-8D38-42F7-A21B-3A41847005FD"));
            _orders.Add(new Order("8000", "10 Posters", DateTime.Now.AddDays(-3), 2, "B34B06E2-8E27-4DFC-B7B3-D0282BA34F8A"));
            _orders.Add(new Order("9000", "200 Pens", DateTime.Now.AddDays(-3), 3, "18E6242C-4A9E-4741-A962-DFB02958BBCB"));
            _orders.Add(new Order("10000", "500 Business cards", DateTime.Now.AddDays(-3), 1, "FF10F49B-A2EB-473A-A52F-AA63F30D7ED1"));
            _orders.Add(new Order("11000", "250 Pins", DateTime.Now.AddDays(-4), 2, "88F6D29C-4CDC-48DF-B41C-D5801EC0B980"));
            _orders.Add(new Order("12000", "500 Coffee Sleeves", DateTime.Now.AddDays(-4), 3, "CDD0FC42-7605-4874-B175-161F4E2606D5"));
            _orders.Add(new Order("13000", "400 Pads", DateTime.Now.AddDays(-4), 1, "7E4A1B65-54A1-4782-A7D5-3834F02B1DE1"));
            _orders.Add(new Order("14000", "100 Bandanas", DateTime.Now.AddDays(-5), 2, "89D53FE2-1F26-4568-A0C6-370280802647"));
            _orders.Add(new Order("15000", "50 Spinners", DateTime.Now.AddDays(-5), 3, "75803E03-0100-4A47-91F5-CF7F5738B89B"));
            this._events = events;
        }

        private Order GetById(string id)
        {
            var order = _orders.SingleOrDefault(o => Equals(o.Id, id));
            if (order == null)
            {
                throw new ArgumentException(string.Format("Order ID '{0}' is invalid", id));
            }
            return order;
        }


        public Task<Order> CreateAsync(Order order)
        {
            _orders.Add(order);
            var orderEvent = new OrderEvent(order.Id, order.Name, OrderStatuses.CREATED,DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(string id)
        {
            return Task.FromResult(GetById(id));
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<Order> StartAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Start();
            var orderEvent = new OrderEvent(order.Id, order.Name, OrderStatuses.PROCESSING, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }

        public Task<Order> CompleteAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Complete();
            var orderEvent = new OrderEvent(order.Id, order.Name, OrderStatuses.COMPLETED, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }

        public Task<Order> CancelAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Cancel();
            var orderEvent = new OrderEvent(order.Id, order.Name, OrderStatuses.CANCELLED, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }

        public Task<Order> CloseAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Close();
            var orderEvent = new OrderEvent(order.Id, order.Name, OrderStatuses.CLOSED, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }
    }

    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> StartAsync(string orderId);
        Task<Order> CompleteAsync(string orderId);
        Task<Order> CancelAsync(string orderId);
        Task<Order> CloseAsync(string orderId);
    }
}
