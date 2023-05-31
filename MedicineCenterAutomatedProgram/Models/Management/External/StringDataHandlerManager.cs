namespace MedicineCenterAutomatedProgram.Models.Management.External
{
    public class StringDataHandlerManager
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

        public static string UserMainInteractionShiftHospitalAddressStringValue(string valueSource, string valueStart, string valueEnd)
        {
            int startIndex, endIndex;

            if (valueSource.Contains(valueStart) && valueSource.Contains(valueEnd))
            {
                startIndex = valueSource.IndexOf(valueStart, 0) + valueStart.Length;

                endIndex = valueSource.IndexOf(valueEnd, startIndex);

                return valueSource.Substring(startIndex, endIndex - startIndex);
            }

            return "";
        }
    }
}
