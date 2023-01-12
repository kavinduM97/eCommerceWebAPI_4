﻿using Assignment.Request;
using Azure.Core;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Order
{
    public interface IOrderRepository
    {
        public OrderErrorHandler PlaceOrder(int id, OrderRequest request);
        //public Task<ActionResult<List<Product>>> UpdateTables(int id, UpdateOrderRequestscs responseP);

    }
}