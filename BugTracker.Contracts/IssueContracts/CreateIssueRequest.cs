using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.IssueContracts
{
    public record CreateIssueRequest
    (
        string IssueName,
        string IssueDescription,
        string IssueType,
        List<string> AssignedTo,
        string CreatedBy,
        string CurrentStatus,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
