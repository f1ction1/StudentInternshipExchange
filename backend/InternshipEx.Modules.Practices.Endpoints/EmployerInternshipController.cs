using InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.AddInternship;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternship;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetEmployerInternships;
using InternshipEx.Modules.Practices.Application.UseCases.Internships.PatchIntership;
using InternshipEx.Modules.Practices.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipEx.Modules.Practices.Endpoints
{
    [Route("api/employer")]
    [Authorize(Roles = "employer")]
    [ApiController]
    public class EmployerInternshipController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployerInternshipController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("internships/add")]
        public async Task<IActionResult> AddInternship(AddInternshipDto internshipDto)
        {
            await _sender.Send(new AddInternshipCommand(internshipDto));
            return NoContent();
        }

        [HttpGet("internships")]
        public async Task<IActionResult> GetIntershipsByEmployer()
        {
            var internships = await _sender.Send(new GetEmployerInternshipsQuery());
            return Ok(internships);
        }

        [HttpGet("internships/{internshipId}")]
        public async Task<IActionResult> GetEmployerInternship(Guid internshipId)
        {
            var internship = await _sender.Send(new GetEmployerInternshipQuery(internshipId));
            return Ok(internship);
        }

        [HttpPatch("internships/edit")]
        public async Task<IActionResult> PatchInternship(PatchInternshipDto internshipDto)
        {
            await _sender.Send(new PatchIntershipCommand(internshipDto));
            return NoContent();
        }
    }
}
