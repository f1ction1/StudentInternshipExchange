using IntershipEx.Modules.Applications.Application.UseCases.Applications.ApplyInternship;
using IntershipEx.Modules.Applications.Application.UseCases.Applications.GetApplicationDetail;
using IntershipEx.Modules.Applications.Application.UseCases.Applications.GetStudentApplications;
using IntershipEx.Modules.Applications.Application.UseCases.Applications.MakeReview;
using IntershipEx.Modules.Applications.Application.UseCases.Applications.RejectApplication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Common.Domain.Abstractions;
using System.Threading;

namespace IntershipEx.Modules.Applications.Infrastructure.Controllers.Application
{
    [ApiController]
    [Route("api/student/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly ISender _sender;

        public ApplicationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> ApplyInternship(ApplyInternshipRequest request, CancellationToken cancellationToken)
        {
            var command = new ApplyInternshipCommand(request.InternshipId, request.CoverLetter);

            Result result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentApplications(CancellationToken cancellationToken)
        {
            var query = new GetStudentApplicationsQuery();

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetApplicationDetails(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetApplicationDetailQuery(id), cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPost("{applicationId}/review")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> ReviewApplication([FromRoute]Guid applicationId, [FromBody]string? reviewNotes, CancellationToken cancellationToken)
        {
            var command = new MakeReviewCommand(applicationId, reviewNotes);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPost("{applicationId}/reject")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> RejectApplication([FromRoute]Guid applicationId, [FromBody]string? rejectionReason, CancellationToken cancellationToken)
        {
            var command = new RejectApplicationCommand(applicationId, rejectionReason);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }

}
