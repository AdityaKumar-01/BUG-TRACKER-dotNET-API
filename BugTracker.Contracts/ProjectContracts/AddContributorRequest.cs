using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.ProjectContracts
{
    public record AddContributorRequest
   (
       string UserId,
       string ProjectId,
       string Role,
       string UserName
   );
}
