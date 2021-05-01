using BLOGCORE.APPLICATION.Core.Constants;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Utilities
{
    public class Crypto
    {
        private static TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        public static string CifrarClave(string stringToEncrypt, string key = Constantes.semillaEncriptacion)
        {
            DES.Key = MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        private static byte[] MD5Hash(string value)
        {
            return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
        }

        public static string DescifrarClave(string encryptedString, string key = Constantes.semillaEncriptacion)
        {
            try
            {
                string clave = "";
                DES.Key = MD5Hash(key);
                DES.Mode = CipherMode.ECB;
                if (!string.IsNullOrEmpty(encryptedString))
                {
                    byte[] Buffer = Convert.FromBase64String(encryptedString.Replace(" ", "+"));
                    clave = ASCIIEncoding.UTF8.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
                }

                return clave;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
