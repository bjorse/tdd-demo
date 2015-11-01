using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDD.Demo.Domain.Contract;
using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Application.Orders
{
    #pragma warning disable 1998

    public class OrderService : IOrderService
    {
        private readonly IOrderSaver _saver;

        public OrderService(IOrderSaver saver)
        {
            _saver = saver;
        }

        public async Task<OrderSavedResult> SaveOrderAsync(OrderModel order)
        {
            return _saver.SaveOrder(order);
        }

        public Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderModel>> GetAllOrdersByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetLatestOrderByOrderNumberAsync(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetPreviousOrderByRevisionIdAsync(int revisionId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }

    #pragma warning restore 1998
}
