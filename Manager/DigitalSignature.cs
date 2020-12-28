using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public enum HashAlgorithms { SHA1, SHA256 }

    public class DigitalSignature
    {
        public static byte[] Create(string message, HashAlgorithms hashAlgorithm, X509Certificate2 certificate)
        {
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PrivateKey;

            if (csp == null) throw new Exception("Valid certificate was not found");

            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(message); //current message encripted in bytes
            byte[] hash = null; //hash result

            if (hashAlgorithm.Equals(HashAlgorithms.SHA1))
            {
                SHA1Managed sha1 = new SHA1Managed();
                hash = sha1.ComputeHash(data);
            }
            else if (hashAlgorithm.Equals(HashAlgorithms.SHA256))
            {
                SHA256Managed sha256 = new SHA256Managed();
                hash = sha256.ComputeHash(data);
            }

            return csp.SignHash(hash, CryptoConfig.MapNameToOID(hashAlgorithm.ToString()));
        }

        public static bool Verify(string message, HashAlgorithms hashAlgorithm, byte[] signature, X509Certificate2 certificate)
        {
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(message);
            byte[] hash = null;

            if (hashAlgorithm.Equals(HashAlgorithms.SHA1))
            {
                SHA1Managed sha1 = new SHA1Managed();
                hash = sha1.ComputeHash(data);
            }
            else if (hashAlgorithm.Equals(HashAlgorithms.SHA256))
            {
                SHA256Managed sha256 = new SHA256Managed();
                hash = sha256.ComputeHash(data);
            }

            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID(hashAlgorithm.ToString()), signature);
        }
    }
}