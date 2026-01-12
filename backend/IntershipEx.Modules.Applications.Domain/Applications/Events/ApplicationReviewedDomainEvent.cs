using Modules.Common.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipEx.Modules.Applications.Domain.Applications.Events
{
    public record ApplicationReviewedDomainEvent(Guid EventId, Guid ApplicationId, DateTime ReviewedAt) : IDomainEvent;
}
