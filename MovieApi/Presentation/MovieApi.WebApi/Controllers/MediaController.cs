// 📁 MovieApi.WebApi/Controllers/MediaController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaQueries;
using MovieApi.Dto.Dtos.MovieDtos;

namespace MovieApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // CREATE: api/Media
        [HttpPost]
        public async Task<IActionResult> CreateMedia([FromBody] CreateMediaCommand cmd)
        {
            await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetMediaById), new { id = cmd.MovieId }, null);
        }

        // UPDATE: api/Media/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedia(int id, [FromBody] UpdateMediaCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _mediator.Send(cmd);
            return NoContent();
        }

        // DELETE: api/Media/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            await _mediator.Send(new RemoveMediaCommand(id));
            return NoContent();
        }


        // READ BY ID: api/Media/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultMediaDto>> GetMediaById(int id)
        {
            var result = await _mediator.Send(new GetMediaByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // READ LIST BY MOVIE: api/Media/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<List<ResultMediaDto>>> GetMediaByMovie(int movieId)
        {
            var list = await _mediator.Send(new GetMediaListByMovieIdQuery(movieId));
            return Ok(list);
        }

    }
}
