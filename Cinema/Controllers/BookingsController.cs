using AutoMapper;
using Cinema.DTO;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public BookingsController(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<PagedResult<BookingDto>>> GetAll([FromQuery] BookingFilterParams filter)
    {
        var result = await _bookingService.GetAllAsync(filter);
        return Ok(new PagedResult<BookingDto>
        {
            Data = _mapper.Map<IEnumerable<BookingDto>>(result.Data),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookingDto>> GetById(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        return Ok(_mapper.Map<BookingDto>(booking));
    }

    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create([FromBody] CreateBookingRequest request)
    {
        var booking = await _bookingService.CreateAsync(request);
        var dto = _mapper.Map<BookingDto>(booking);
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, dto);
    }

    [HttpPatch("{id:int}/confirm")]
    public async Task<ActionResult<BookingDto>> Confirm(int id)
    {
        var booking = await _bookingService.ConfirmAsync(id);
        return Ok(_mapper.Map<BookingDto>(booking));
    }

    [HttpPatch("{id:int}/cancel")]
    public async Task<ActionResult<BookingDto>> Cancel(int id)
    {
        var booking = await _bookingService.CancelAsync(id);
        return Ok(_mapper.Map<BookingDto>(booking));
    }
}
