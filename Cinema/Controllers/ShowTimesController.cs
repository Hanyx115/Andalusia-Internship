using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Cinema.DTO;
using Cinema.Services.Interfaces;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("api/showtimes")]

    public class ShowTimesController : ControllerBase
    {

        private readonly IShowTimeService _showTimeService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public ShowTimesController(
            IShowTimeService showTimeService,
            IBookingService bookingService,
            IMapper mapper)
        {
            _showTimeService = showTimeService;
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowTimeDto>>> GetAll()
        {
            var showTimes = await _showTimeService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ShowTimeDto>>(showTimes));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShowTimeDto>> GetById(int id)
        {
            var showTime = await _showTimeService.GetByIdAsync(id);
            return Ok(_mapper.Map<ShowTimeDto>(showTime));
        }

        [HttpPost]
        public async Task<ActionResult<ShowTimeDto>> Create([FromBody] CreateShowTimeRequest request)
        {
            var showTime = await _showTimeService.CreateAsync(request);
            var dto = _mapper.Map<ShowTimeDto>(showTime);
            return CreatedAtAction(nameof(GetById), new { id = showTime.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ShowTimeDto>> Update(int id, [FromBody] UpdateShowTimeRequest request)
        {
            var showTime = await _showTimeService.UpdateAsync(id, request);
            return Ok(_mapper.Map<ShowTimeDto>(showTime));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _showTimeService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("{id:int}/bookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings(int id)
        {
            var bookings = await _bookingService.GetByShowTimeAsync(id);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }
    }
}
