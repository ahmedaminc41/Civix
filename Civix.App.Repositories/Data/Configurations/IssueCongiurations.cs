using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Civix.App.Repositories.Data.Configurations
{
    public class IssueCongiurations : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(I => I.Status)
                 .HasConversion(IStatus => IStatus.ToString(), IStatus => (IssueStatus)Enum.Parse(typeof(IssueStatus), IStatus));


            builder.Property(I => I.Priority)
                .HasConversion(IStatus => IStatus.ToString(), IStatus => (IssuePriority)Enum.Parse(typeof(IssuePriority), IStatus));

        }
    }
}
