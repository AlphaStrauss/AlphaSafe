using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlphaSafe.Core.Cryptography
{
    public class Aes256
    {
        public static byte[] EncryptByteArray(byte[] key, byte[] secret)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] iv = new byte[16]; //for the sake of demo

                //Set up
                AesEngine engine = new AesEngine();
                CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
                PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher); //Default scheme is PKCS5/PKCS7
                KeyParameter keyParam = new KeyParameter(key);
                ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 16);

                // Encrypt
                cipher.Init(true, keyParamWithIV);
                byte[] outputBytes = new byte[cipher.GetOutputSize(secret.Length)];
                int length = cipher.ProcessBytes(secret, outputBytes, 0);
                cipher.DoFinal(outputBytes, length); //Do the final block

                return outputBytes;
            }
        }
        
        public static byte[] DecryptByteArray(byte[] key, byte[] secret)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] iv = new byte[16]; //for the sake of demo

                //Set up
                AesEngine engine = new AesEngine();
                CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
                PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher); //Default scheme is PKCS5/PKCS7
                KeyParameter keyParam = new KeyParameter(key);
                ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 16);

                //Decrypt            
                cipher.Init(false, keyParamWithIV);
                byte[] comparisonBytes = new byte[cipher.GetOutputSize(secret.Length)];
                int length = cipher.ProcessBytes(secret, comparisonBytes, 0);
                cipher.DoFinal(comparisonBytes, length); //Do the final block

                return comparisonBytes;
            }
        }
    }
}
