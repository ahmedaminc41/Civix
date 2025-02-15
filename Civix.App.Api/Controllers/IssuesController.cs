using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;
using Civix.App.Core.Helpers;
using Civix.App.Core.Service.Contracts.Issues;
using Civix.App.Core.Specifications.Issues_Specs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Civix.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateIssue(CreateIssueDto issue)
        {
            if (issue is null) return BadRequest();

            var result = await _issueService.CreateIssue(issue);
            if (result is null) return BadRequest();

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllIssues([FromQuery]IssueSpecParams specParams)
        {
            var result = await _issueService.GetAllIssuesAsyncWithSpec(specParams);

            if (result is null) return BadRequest();

            return Ok(result);
        }

    }
}
