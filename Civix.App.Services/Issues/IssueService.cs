using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Civix.App.Core;
using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;
using Civix.App.Core.Service.Contracts.Issues;

namespace Civix.App.Services.Issues
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IssueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IssueToReturn?> CreateIssue(CreateIssueDto model)
        {
            if (model is null) return null;

            var issue =  _mapper.Map<Issue>(model);
            await _unitOfWork.Repository<Issue, string>().AddAsync(issue);
            var count = await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<IssueToReturn>(issue);
            if (count > 0) return result;
            else return null;
        }

        public async Task<IEnumerable<IssueToReturn>?> GetAllIssuesAsync()
        {
            var result = await _unitOfWork.Repository<Issue, string>().GetAllAsync();

            return _mapper.Map<IEnumerable<IssueToReturn>>(result);
        }
    }
}
