using System.Threading.Tasks;
using TDD.Demo.Domain.Customers;

namespace TDD.Demo.Domain.Contract
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerByIdAsync(int customerId);
    }
}
