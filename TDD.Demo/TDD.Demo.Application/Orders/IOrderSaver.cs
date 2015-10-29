using TDD.Demo.Domain.Orders;

namespace TDD.Demo.Application.Orders
{
    public interface IOrderSaver
    {
        OrderSavedResult SaveOrder(OrderModel order);
    }
}
