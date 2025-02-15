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
            CreateMap<Issue, CreateIssueDto>().ReverseMap().ForMember(dest => dest.Images, opt => opt.Ignore()); // Images are handled separately
            ;
            CreateMap<Issue, IssueToReturn>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList())); // Map images


        }
    }
}
