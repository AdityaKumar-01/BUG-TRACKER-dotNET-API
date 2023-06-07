using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.IssueContracts
{
    public record CreateIssueRequest
    (
        string Name,
        string Description,
        string Type,
        List<string> AssignedTo,
        string CreatedBy,
        string CurrentStatus,
        string BelongsToProject,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
