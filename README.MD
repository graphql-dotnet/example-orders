# Example-Orders
A sample GraphQL order management endpoint using GraphQL for .NET. This is a more advanced version of the example I used in my course [API Development in .NET with GraphQL](https://www.lynda.com/NET-tutorials/API-Development-NET-GraphQL/664823-2.html)

## Running the sample
* If usng Visual Studio, open `Orders-GraphQL.sln` build and run
* On Mac/Linux go the `Server` folder and run `dotnet run -f netcoreapp2.0`

## Overview
This endpoint contains 2 core data types
* Orders - Orders that have been added to the system
* Customers - Customers for each order

## API
### Queries
#### orders
Retrieves a list of orders

example:
```
query getOrders {
  orders {
    id
    name
    description
    customer {
        name
    }
  }
}
```
#### orderById
Retrieves a specified order

*Arguments*
* orderId - The id of the order

example:
```
query getOrder {
  orderById(orderId: "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827") {
    id
    name
    description
  }
}
```
#### customers
Retrieves all customers

example:
```
query getCustomers {
  customers {
    id
    name
  }
}
```
### Mutations
#### createOrder
Creates an order

*Arguments*
* order - Information for the order to be created.

example:
```
mutation createOrder{
  createOrder(order:
    {
      name:"Glenn",
      description:"Test",
      customerId: 1,
      created: "03/16/2018"
    }
  ) 
  {
    id
    name
  }
}
```
#### startOrder
Starts the processing of an order. Can only be called if the order is in a `CREATED` state.

*Arguments*
* orderId - The id of the order

example starting an order
```
mutation startOrder {
  startOrder(orderId: "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827") {
    id
    status
  }
}
```
#### completeOrder
Finishes the processing of an order. Can only be called if the order is in the `PROCESSING` state.

*Arguments*
* orderId - The id of the order

example completing an order
```
mutation completeOrder {
  completeOrder(orderId: "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827") {
    id
    status
  }
}
```
#### cancelOrder
Cancels an order. Can only be called if the order is not in the `COMPLETED` or `CANCELLED` state.

*Arguments*
* orderId - The id of the order

example cancelling an order
```
mutation cancelOrder {
  cancelOrder(orderId: "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827") {
    id
    status
  }
}
```
#### closeOrder
Closes an order. Can only be called if the order is in the `COMPLETED` state.

*Arguments*
* orderId - The id of the order

example closing an order
```
mutation closeOrder {
  closeOrder(orderId: "FAEBD971-CBA5-4CED-8AD5-CC0B8D4B7827") {
    id
    status
  }
}
```
### Subscriptions
You can use subscriptions to get notified when an order is created. This relies on the awesome [GraphQL Server](https://github.com/graphql-dotnet/server) project.

To test out subscriptions, you'll want to open two graphiql instances. One for subscribing, and the other for performing a mutation.

#### orderEvent
Notifies when order status changes.

*Arguments*
* statuses (optional) - An array of one or more statuses to receive notifications on

example subscribing to all order updates
```
subscription createdEvent {
  orderEvent {
    id
    name
    status
  }
}
```

example subscribing to order started events
```
subscription startedEvent {
  orderEvent ([PROCESSING]) {
    id
    name
    status
  }
}
```

## Solution Structure
* Server - Contains the GraphQL Server / wires up GraphQL.NET middleware
* Orders - Contains models and the GraphQL Schema
  * Models - Contains the underlying models which drive the schema
  * Services - Contains data services for retrieving and updating models, as well as for notifications. Services are registered with the ASP.NET Core container allowing them to be injected wherever need.
  * Schema - Contains the GraphQL types, queries, mutuations and subscriptions.

## License
Apache 2.0






