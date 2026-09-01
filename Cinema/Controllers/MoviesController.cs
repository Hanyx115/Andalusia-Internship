//using Asp.Versioning;
//using Microsoft.AspNetCore.Mvc;
//using AutoMapper;
//using Cinema.DTO;
//using Cinema.Services.Interfaces;

//namespace Cinema.Controllers;

//[ApiController]
////[ApiVersion("1.0")]
//[ApiVersion("2.0")]
//[Route("api/v{version:apiVersion}/movies")]
//public class MoviesController : ControllerBase
//{
//    private readonly IMovieService _movieService;
//    private readonly IMapper _mapper;

//    public MoviesController(IMovieService movieService, IMapper mapper)
//    {
//        _movieService = movieService;
//        _mapper = mapper;
//    }

//    // GET api v1
//    [HttpGet]
//    //[MapToApiVersion("1.0")]
//    public async Task<ActionResult<PagedResult<MovieV1Dto>>> GetAllV1([FromQuery] MovieFilterParams filter)
//    {
//        var result = await _movieService.GetAllAsync(filter);
//        return Ok(new PagedResult<MovieV1Dto>
//        {
//            Data = _mapper.Map<IEnumerable<MovieV1Dto>>(result.Data),
//            Page = result.Page,
//            PageSize = result.PageSize,
//            TotalCount = result.TotalCount
//        });
//    }

//    // GET api v2
//    [HttpGet]
//    //[MapToApiVersion("2.0")]
//    public async Task<ActionResult<PagedResult<MovieV2Dto>>> GetAllV2([FromQuery] MovieFilterParams filter)
//    {
//        var result = await _movieService.GetAllAsync(filter);
//        return Ok(new PagedResult<MovieV2Dto>
//        {
//            Data = _mapper.Map<IEnumerable<MovieV2Dto>>(result.Data),
//            Page = result.Page,
//            PageSize = result.PageSize,
//            TotalCount = result.TotalCount
//        });
//    }

//    [HttpGet("{id:int}")]
//    //[MapToApiVersion("1.0")]
//    public async Task<ActionResult<MovieV1Dto>> GetByIdV1(int id)
//    {
//        var movie = await _movieService.GetByIdAsync(id);
//        return Ok(_mapper.Map<MovieV1Dto>(movie));
//    }

//    [HttpGet("{id:int}")]
//    //[MapToApiVersion("2.0")]
//    public async Task<ActionResult<MovieV2Dto>> GetByIdV2(int id)
//    {
//        var movie = await _movieService.GetByIdAsync(id);
//        return Ok(_mapper.Map<MovieV2Dto>(movie));
//    }


//    [HttpPost]
//    public async Task<ActionResult<MovieV2Dto>> Create([FromBody] CreateMovieRequest request)
//    {
//        var movie = await _movieService.CreateAsync(request);
//        var dto = _mapper.Map<MovieV2Dto>(movie);


//        var version = RouteData.Values["version"]?.ToString() ?? "1.0";
//        return CreatedAtAction(nameof(GetByIdV1), new { id = movie.Id, version }, dto);
//    }

//    [HttpPut("{id:int}")]
//    public async Task<ActionResult<MovieV2Dto>> Update(int id, [FromBody] UpdateMovieRequest request)
//    {
//        var movie = await _movieService.UpdateAsync(id, request);
//        return Ok(_mapper.Map<MovieV2Dto>(movie));
//    }

//    [HttpDelete("{id:int}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        await _movieService.DeleteAsync(id);
//        return NoContent();
//    }
//}
