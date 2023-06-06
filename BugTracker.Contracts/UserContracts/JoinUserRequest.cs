﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Contracts.UserContracts
{
    public record JoinUserRequest
    (
        string Name,
        string Password,
        List<string> ContributorOfProject,
        List<string> AssignedIssue
    );
}
