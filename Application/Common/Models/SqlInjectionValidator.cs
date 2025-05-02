using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace Application.Common.Models
{
    public static class SqlInjectionValidator
    {
        public static bool ISValid(string input)
        {
            string[] sqlInjectionKeywords = new[]
{
    "select", "insert", "update", "delete", "drop", "exec",
                "execute", "union", "--", "#", ";", "or", "and", "xp_cmdshell", "information_schema"
};

            foreach (var keyword in sqlInjectionKeywords)
            {
                if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                 
                    Log.Error("SqlInjectionAttack", "UnAuthorized");

                    return false;
                }
            }

            return true;
        }
    }
}
