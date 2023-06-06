using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.ProjectContracts
{
    public record UpdateProjectDetailsRequest
   (
        string Name,
        string Description,
        string Version,
        DateTime UpdatedAt,
        List<string> Tags
   );
}
