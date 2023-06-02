using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.ProjectContracts
{
    public record CreateProjectRequest
   (
        string ProjectName,
        string Description,
        string Version,
        string OwnerId,
        string OwnerName,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        List<string> Contributors,
        List<string> Tags
   );
}
