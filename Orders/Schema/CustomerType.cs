using GraphQL.Types;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Schema
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
