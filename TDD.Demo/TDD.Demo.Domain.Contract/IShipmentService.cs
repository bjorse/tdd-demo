using System.Collections.Generic;
using System.Threading.Tasks;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Domain.Contract
{
    public interface IShipmentService
    {
        Task<OrderShipmentSavedResult> SaveOrderShipmentAsync(OrderShipmentModel orderShipment);

        Task<IEnumerable<OrderShipmentModel>> GetAllShipmentsAsync();

        Task<IEnumerable<OrderShipmentModel>> GetAllShipmentsByCustomerIdAsync(int customerId);

        Task<IEnumerable<OrderShipmentModel>> GetAllNotShippedShipmentsByCustomerIdAsync(int customerId);

        Task<OrderShipmentModel> GetLatestShipmentByOrderNumberAsync(int orderNumber);

        Task<OrderShipmentModel> GetPreviousShipmentWithAnotherOrderRevisionAsync(int shipmentId);

        Task<OrderShipmentModel> GetShipmentByIdAsync(int shipmentId);
    }
}
