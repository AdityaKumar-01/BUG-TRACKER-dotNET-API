using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.UserContracts
{
    public record UpdateIssueListRequest
    (
        string UserId,
        string IssueId
        );
}
