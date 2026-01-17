using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaPhotoQueries;
using MovieApi.Dto.Dtos.MovieDtos;

namespace MovieApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaPhotosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MediaPhotosController(IMediator mediator) => _mediator = mediator;

        // POST api/MediaPhotos
        [HttpPost]
        public async Task<IActionResult> AddMediaPhoto([FromBody] AddMediaPhotoCommand cmd)
        {
            await _mediator.Send(cmd);
            return Created(string.Empty, null);
        }

        // GET api/MediaPhotos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResultPhotoDto>> GetPhoto(int id)
        {
            var dto = await _mediator.Send(new GetPhotoByIdQuery(id));
            return dto is null ? NotFound() : Ok(dto);
        }

        // PUT api/MediaPhotos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePhoto(int id,
                   [FromBody] UpdateMediaPhotoCommand cmd)
        {
            if (id != cmd.PhotoId) return BadRequest("Path id ≠ body id");
            await _mediator.Send(cmd);
            return NoContent();
        }

        // DELETE api/MediaPhotos/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await _mediator.Send(new RemoveMediaPhotoCommand(id));
            return NoContent();
        }
    }
}
