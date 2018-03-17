using Orders.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Orders.Services
{
    public class OrderEventService : IOrderEventService
    {
        private readonly ISubject<OrderEvent> _eventStream = new ReplaySubject<OrderEvent>(1);

        public OrderEventService()
        {
            AllEvents = new ConcurrentStack<OrderEvent>();
        }

        public ConcurrentStack<OrderEvent> AllEvents { get; }

        public void AddError(Exception exception)
        {
            _eventStream.OnError(exception);
        }

        public OrderEvent AddEvent(OrderEvent orderEvent)
        {
            AllEvents.Push(orderEvent);
            _eventStream.OnNext(orderEvent);
            return orderEvent;
        }

        public IObservable<OrderEvent> EventStream()
        {
            return _eventStream.AsObservable();
        }
    }

    public interface IOrderEventService
    {
        ConcurrentStack<OrderEvent> AllEvents { get; }
        void AddError(Exception exception);
        OrderEvent AddEvent(OrderEvent orderEvent);
        IObservable<OrderEvent> EventStream();
    }
}
