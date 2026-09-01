using AutoMapper;
using Cinema.DTO;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("api/auditoriums")]
public class AuditoriumsController : ControllerBase
{
    private readonly IAuditoriumService _auditoriumService;
    private readonly IShowTimeService _showTimeService;
    private readonly IMapper _mapper;

    public AuditoriumsController(
        IAuditoriumService auditoriumService,
        IShowTimeService showTimeService,
        IMapper mapper)
    {
        _auditoriumService = auditoriumService;
        _showTimeService = showTimeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetAll()
    {
        var auditoriums = await _auditoriumService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<AuditoriumDto>>(auditoriums));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuditoriumDto>> GetById(int id)
    {
        var auditorium = await _auditoriumService.GetByIdAsync(id);
        return Ok(_mapper.Map<AuditoriumDto>(auditorium));
    }

    [HttpPost]
    public async Task<ActionResult<AuditoriumDto>> Create([FromBody] CreateAuditoriumRequest request)
    {
        var auditorium = await _auditoriumService.CreateAsync(request);
        var dto = _mapper.Map<AuditoriumDto>(auditorium);
        return CreatedAtAction(nameof(GetById), new { id = auditorium.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AuditoriumDto>> Update(int id, [FromBody] UpdateAuditoriumRequest request)
    {
        var auditorium = await _auditoriumService.UpdateAsync(id, request);
        return Ok(_mapper.Map<AuditoriumDto>(auditorium));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _auditoriumService.DeleteAsync(id);
        return NoContent();
    }


    [HttpGet("{id:int}/showtimes")]
    public async Task<ActionResult<IEnumerable<ShowTimeDto>>> GetShowTimes(int id, [FromQuery] DateTime? date)
    {
        var showTimes = await _showTimeService.GetByAuditoriumAsync(id, date);
        return Ok(_mapper.Map<IEnumerable<ShowTimeDto>>(showTimes));
    }
}
