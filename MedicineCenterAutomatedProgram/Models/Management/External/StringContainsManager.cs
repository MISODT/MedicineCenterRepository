using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineCenterAutomatedProgram.Models.Management.External
{
    public class StringContainsManager
    {
        public static bool IsStringContainsDigits(string s)
        {
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsStringContainsSymbols(string s)
        {
            foreach (char c in s)
            {
                if (char.IsSymbol(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
