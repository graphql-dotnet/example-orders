using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Models
{
    public class Order
    {
        public Order(string name, string description, DateTime created, int customerId, string Id)
        {
            Name = name;
            Description = description;
            Created = created;
            CustomerId = customerId;
            this.Id = Id;
            Status = OrderStatuses.CREATED;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; private set; }
        public int CustomerId { get; set; }
        public string Id { get; private set; }
        public OrderStatuses Status { get; private set; }

        public void Start()
        {
            if (Status != OrderStatuses.CREATED) {
                throw new InvalidOperationException(string.Format("Order: {0} cannot be started", Id));
            }
            Status = OrderStatuses.PROCESSING;
        }

        public void Complete() {
            if (Status != OrderStatuses.PROCESSING) {
                throw new InvalidOperationException(string.Format("Order: {0} cannot be completed", Id));
            }

            Status = OrderStatuses.COMPLETED;
        }

        public void Cancel() {
            if (Status == OrderStatuses.CANCELLED || Status == OrderStatuses.CLOSED || Status == OrderStatuses.COMPLETED) {
                throw new InvalidOperationException(string.Format("Order: {0} cannot be cancelled", Id));
            }
            
            Status = OrderStatuses.CANCELLED;
        }

        public void Close() {
            if (Status != OrderStatuses.COMPLETED) {
                throw new InvalidOperationException(string.Format("Order: {0} cannot be closed", Id));
            }

            Status = OrderStatuses.CLOSED;
        }

        
    }

    [Flags]
    public enum OrderStatuses
    {
        CREATED = 2,
        PROCESSING = 4,
        COMPLETED = 8,
        CANCELLED = 16,
        CLOSED = 32
    }
}
