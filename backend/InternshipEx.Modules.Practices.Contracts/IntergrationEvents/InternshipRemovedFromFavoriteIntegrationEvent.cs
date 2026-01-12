using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipEx.Modules.Practices.Contracts.IntergrationEvents
{
    public record InternshipRemovedFromFavoriteIntegrationEvent(Guid EventId, Guid InternshipId, Guid StudentId);
}
