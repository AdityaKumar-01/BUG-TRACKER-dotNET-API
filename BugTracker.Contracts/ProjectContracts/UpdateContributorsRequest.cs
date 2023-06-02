using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.ProjectContracts
{
    public record UpdateContributorsRequest
   (
       string UserId,
       string ProjectId
   );
}
