using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validation
{
    //To Check That Not Sql Injection Attack
    public static class SqlInjectionValidator
    {
        //Words That Cause Sql Injection
        private static readonly string[] SqlInjectionPatterns = new[]
        {
        "--", ";--", ";", "/*", "*/", "@@",
        "char", "nchar", "varchar", "nvarchar",
        "alter", "begin", "cast", "create", "cursor", "declare",
        "delete", "drop", "exec", "execute", "fetch", "insert",
        "kill", "select", "sys", "sysobjects", "syscolumns",
        "table", "update", "shutdown"
    };

        public static bool IsSqlInjection(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string lowerInput = input.ToLower();

            return SqlInjectionPatterns.Any(pattern => lowerInput.Contains(pattern));
        }
    }

}