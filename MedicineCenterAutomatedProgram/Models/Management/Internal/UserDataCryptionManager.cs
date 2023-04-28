using System;
using System.Security.Cryptography;
using System.Text;
namespace MedicineCenterAutomatedProgram.Models.Management.Internal
{
    public class UserDataCryptionManager
    {
        public static string UserDataEncrypt(string userDataDecryptedText)
        {
            string hashKey = "M*d$C!nE";

            byte[] encodedData = Encoding.UTF8.GetBytes(userDataDecryptedText);

            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] computedHashKeys = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(hashKey));

                using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider() { Key = computedHashKeys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();

                    byte[] encryptionResultData = cryptoTransform.TransformFinalBlock(encodedData, 0, encodedData.Length);

                    return Convert.ToBase64String(encryptionResultData, 0, encryptionResultData.Length);
                }
            }
        }

        public static string UserDataDecrypt(string userDataEncryptedText)
        {
            string hashKey = "M*d$C!nE";

            byte[] encodedData = Convert.FromBase64String(userDataEncryptedText);

            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] computedHashKeys = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(hashKey));

                using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider() { Key = computedHashKeys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();

                    byte[] decryptionResultData = cryptoTransform.TransformFinalBlock(encodedData, 0, encodedData.Length);

                    return Encoding.UTF8.GetString(decryptionResultData);
                }
            }
        }
    }
}
