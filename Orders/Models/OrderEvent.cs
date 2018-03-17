using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Models
{
    public class OrderEvent
    {
        public OrderEvent(string orderId, string name, OrderStatuses status, DateTime timestamp)
        {
            OrderId = orderId;
            Name = name;
            Status = status;
            Timestamp = timestamp;
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public OrderStatuses Status { get; set; }
        public DateTime Timestamp { get; private set; }
    }
}
