using AutoMapper;
using Cinema.DTO;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
        => await _customerRepository.GetAllAsync();

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null)
            throw new CustomerNotFoundException($"Customer with ID {id} was not found.");

        return customer;
    }

    public async Task<Customer> CreateAsync(CreateCustomerRequest request)
    {
        var customer = _mapper.Map<Customer>(request);

        var now = DateTime.UtcNow;
        customer.CreatedAt = now;
        customer.UpdatedAt = now;

        return await _customerRepository.AddAsync(customer);
    }

    public async Task<Customer> UpdateAsync(int id, UpdateCustomerRequest request)
    {
        var existing = await _customerRepository.GetByIdAsync(id);
        if (existing is null)
            throw new CustomerNotFoundException($"Customer with ID {id} was not found.");

        existing.Name = request.Name;
        existing.Email = request.Email;
        existing.UpdatedAt = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _customerRepository.GetByIdAsync(id);
        if (existing is null)
            throw new CustomerNotFoundException($"Customer with ID {id} was not found.");

        if (await _customerRepository.HasBookingsAsync(id))
            throw new DeleteConflictException(
                $"Customer with ID {id} cannot be deleted because they have one or more bookings.");

        await _customerRepository.DeleteAsync(existing);
    }
}
