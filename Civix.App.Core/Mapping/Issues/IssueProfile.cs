using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Civix.App.Core.Dtos.Issue;
using Civix.App.Core.Entities;

namespace Civix.App.Core.Mapping.Issues
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, CreateIssueDto>().ReverseMap();
            CreateMap<Issue, IssueToReturn>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));
                
        }
    }
}
