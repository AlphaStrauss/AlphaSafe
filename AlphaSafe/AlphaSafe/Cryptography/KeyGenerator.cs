using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.Cryptography
{
    public class KeyGenerator
    {
        public static string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string symbols = "_-<>|!?.,;:+*~#[]{}$%&§°";

        public static char[] alphanumericChars = chars.ToCharArray();
        public static char[] alphanumericCharsAndSymbols = (chars+symbols).ToCharArray();

        public static SecureRandom random;

        public static string GenerateReadableKey(int numberSymbols, bool withSymbols = true)
        {
            string key = "";

            for (int i = 0; i < numberSymbols; i++)
            {
                if (withSymbols)
                {
                    int randomNumber = random.NextInt() % alphanumericCharsAndSymbols.Length;
                    key += alphanumericCharsAndSymbols[randomNumber * (randomNumber < 0 ? -1 : 1)];
                }
                else
                {
                    int randomNumber = random.NextInt() % alphanumericChars.Length;
                    key += alphanumericChars[randomNumber * (randomNumber < 0 ? -1 : 1)];
                }
            }

            return key;
        }
    }
}
