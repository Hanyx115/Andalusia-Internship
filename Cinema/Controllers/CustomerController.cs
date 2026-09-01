using AutoMapper;
using Cinema.DTO;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public CustomersController(
        ICustomerService customerService,
        IBookingService bookingService,
        IMapper mapper)
    {
        _customerService = customerService;
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerRequest request)
    {
        var customer = await _customerService.CreateAsync(request);
        var dto = _mapper.Map<CustomerDto>(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] UpdateCustomerRequest request)
    {
        var customer = await _customerService.UpdateAsync(id, request);
        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}/bookings")]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings(int id)
    {
        var bookings = await _bookingService.GetByCustomerAsync(id);
        return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
    }
}
