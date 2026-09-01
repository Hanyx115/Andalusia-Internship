using Cinema.DTO;
using Cinema.Models;


namespace Cinema.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<Customer> CreateAsync(CreateCustomerRequest request);
    Task<Customer> UpdateAsync(int id, UpdateCustomerRequest request);
    Task DeleteAsync(int id);
}
