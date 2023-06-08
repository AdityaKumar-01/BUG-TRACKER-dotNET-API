using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.IssueContracts
{
    public record TransferIssueRequest
    (
        string IssueId,
        string FromProjectId,
        string ToProjectId
        );
}
