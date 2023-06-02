using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.UserContracts
{
    public record UpsertUserRequest
    (
        string UserId,
        string password
    );
}
