using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador
{
    public static class EncryptionService
    {
        private const int IvSize = 16;

        public static void DecryptFile(string encryptedFilePath, string decryptedFilePath, string key)
        {
            byte[] keyBytes = new byte[32];
            var configKeyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(configKeyBytes, keyBytes, Math.Min(keyBytes.Length, configKeyBytes.Length));

            using (var inStream = new FileStream(encryptedFilePath, FileMode.Open))
            {
                byte[] iv = new byte[IvSize];
                inStream.Read(iv, 0, IvSize);

                using (var aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var outStream = new FileStream(decryptedFilePath, FileMode.Create))
                    using (var cryptoStream = new CryptoStream(inStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outStream);
                    }
                }
            }
        }
    }
}
