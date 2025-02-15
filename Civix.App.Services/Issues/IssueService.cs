using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Civix.App.Core;
using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;
using Civix.App.Core.Helpers;
using Civix.App.Core.Service.Contracts.Issues;
using Civix.App.Core.Specifications.Issues_Specs;

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

        public async Task<Pagination<IssueToReturn>?> GetAllIssuesAsyncWithSpec(IssueSpecParams specParams)
        {

            var spec = new IssueWithCategorySpecifications(specParams);

            var result = await _unitOfWork.Repository<Issue, string>().GetAllWithSpecAsync(spec);
            var restotal = await _unitOfWork.Repository<Issue, string>().GetAllAsync();

            var total = restotal.Count();

            var countSpec = new IssueWithFilterationCountSpecifications(specParams);

            var count = await _unitOfWork.Repository<Issue, string>().CountAsync(spec);

            var res = new Pagination<IssueToReturn>(specParams.PageSize, specParams.PageIndex, count, _mapper.Map<IEnumerable<IssueToReturn>>(result), total);

            return res;
        }
    }
}
