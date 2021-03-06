﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Domain.Contract
{
    public interface IOrderService
    {
        Task<OrderSavedResult> SaveOrderAsync(OrderModel order);

        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();

        Task<IEnumerable<OrderModel>> GetAllOrdersByCustomerIdAsync(int customerId);

        Task<OrderModel> GetLatestOrderByOrderNumberAsync(int orderNumber);

        Task<OrderModel> GetPreviousOrderByRevisionIdAsync(int revisionId);

        Task<OrderModel> GetOrderByIdAsync(int orderId);
    }
}
