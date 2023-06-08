using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.ProjectContracts
{
    public record CreateProjectRequest
   (
        string Name,
        string Description,
        string Version,
        string OwnerId,
        string OwnerName,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        Dictionary<string, Dictionary<string,string>> Contributors,
        List<string> HasIssue,
        List<string> Tags
   );
}
