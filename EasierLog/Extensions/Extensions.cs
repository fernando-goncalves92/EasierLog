namespace EasierLog
{
    internal static class Extensions
    {
        public static string ToSqlString(this string value)
        {
            return "\'" + value + "\'";
        }

        public static string ToConventionPattern(this string value)
        {
            switch (Settings.DatabaseConventionPatternForTableAndColumns)
            {
                case DatabaseConventionPattern.UpperCase:
                    {
                        return value.ToUpper();
                    }
                case DatabaseConventionPattern.LowerCase:
                    {
                        return value.ToLower();
                    }
                case DatabaseConventionPattern.UpperCamelCase:
                    {
                        var words = value.Split('_');

                        if (words.Length > 1)
                        {
                            var finalWord = string.Empty;

                            for (int count = 0; count < words.Length; count++)
                            {
                                var upperCamelCaseWord = words[count].Substring(0, 1).ToUpper() + words[count].Substring(1).ToLower();

                                finalWord += upperCamelCaseWord + (count + 1 < words.Length ? "_" : string.Empty);
                            }

                            return finalWord;
                        }

                        return value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
                    }
                default: return value;
            }
        }
    }
}
