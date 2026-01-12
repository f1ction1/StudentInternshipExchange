using InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternshipToFavorite;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetInternshipDetails;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetIntershipsList;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetLikedInternships;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.RemoveInternshipFromFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InternshipEx.Modules.Practices.Endpoints
{
    [Route("api")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly ISender _sender;

        public InternshipController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("internships")]
        public async Task<IActionResult> GetPublishedInternships([FromQuery] GetPublishedInternshipsListQuery request) 
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpGet("internship-dictionaries")]
        public async Task<IActionResult> GetInternshipDictioanaries()
        {
            var dictionaries = await _sender.Send(new GetAllInternshipDictionariesQuery());
            return Ok(dictionaries);
        }

        [HttpGet("internships/{Id}")]
        public async Task<IActionResult> GetInternshipDetail(Guid Id)
        {
            var command = new GetIntershipDetailsQuery(Id);
            var result = await _sender.Send(command);

            if (result.IsFailure) 
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "student")]
        [HttpPost("internships/{id}/favorite")] 
        public async Task<IActionResult> AddInternshipToFavorite(Guid id)
        {
            var result = await _sender.Send(new AddInternshipToFavoriteCommand(id));
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
        [Authorize(Roles = "student")]
        [HttpDelete("internships/{id}/favorite")]
        public async Task<IActionResult> RemoveInternshipFromFavorite(Guid id)
        {
            var result = await _sender.Send(new RemoveInternshipFromFavoriteCommand(id));
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpGet("internships/favorites")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetLikedInternships()
        {
            var result = await _sender.Send(new GetLikedInternshipsQuery());
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
