using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.Cryptography
{
    public class SHA3
    {
        public static byte[] GetSHA3Hash(string input)
        {
            Sha3Digest digest = new Sha3Digest();

            byte[] inputBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);

            byte[] result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);

            return result;
        }
    }
}
