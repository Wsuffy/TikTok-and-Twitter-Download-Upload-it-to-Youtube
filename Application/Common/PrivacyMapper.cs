using System.Collections.Generic;

namespace Application.Common
{
    public static class PrivacyMapper
    {
        private static readonly Dictionary<bool, string> PrivacyMap = new Dictionary<bool, string>()
        {
            {true, "private"},
            {false, "public"}
        };

        public static string Map(bool isPrivate)
        {
            return PrivacyMap[isPrivate];
        }
    }
}