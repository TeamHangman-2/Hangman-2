namespace Hangman.Extensions
{
    using System;

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string str, bool ignoreCase = true) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (T)Enum.Parse(typeof(T), str, ignoreCase);
        }
    }
}
