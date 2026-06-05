using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DomainSQLite.Crypto
{
    public static class Encriptador
    {
        private static readonly string TailscaleKey = "dfasfsaaeds♀5_UÄN©}¢$ÜW?^fn5☺5A";

        public static string EncriptarValor(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            byte[] salt = new byte[16];
            byte[] iv = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(iv);
            }

            byte[] key;
            byte[] hmacKey;
            using (var keyDeriv = new Rfc2898DeriveBytes(TailscaleKey, salt, 10000))
            {
                key = keyDeriv.GetBytes(32);
                hmacKey = keyDeriv.GetBytes(32);
            }

            byte[] cipherText;
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(valor);
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    cipherText = ms.ToArray();
                }
            }

            // Header: [version(1)] + [salt(16)] + [iv(16)] + [cipherText]
            byte[] header;
            using (var ms = new MemoryStream())
            {
                ms.WriteByte(1);
                ms.Write(salt, 0, salt.Length);
                ms.Write(iv, 0, iv.Length);
                ms.Write(cipherText, 0, cipherText.Length);
                header = ms.ToArray();
            }

            byte[] hmac;
            using (var hmacSha = new HMACSHA256(hmacKey))
            {
                hmac = hmacSha.ComputeHash(header);
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(header, 0, header.Length);
                ms.Write(hmac, 0, hmac.Length);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string DesencriptarValor(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            try
            {
                byte[] raw = Convert.FromBase64String(valor);

                if (raw.Length < 65 || raw[0] != 1)
                    return DesencriptarLegacy(valor);

                byte[] salt = new byte[16];
                byte[] iv = new byte[16];
                Buffer.BlockCopy(raw, 1, salt, 0, 16);
                Buffer.BlockCopy(raw, 17, iv, 0, 16);

                const int hmacLength = 32;
                const int cipherStart = 33;
                int cipherLength = raw.Length - cipherStart - hmacLength;

                if (cipherLength <= 0)
                    return DesencriptarLegacy(valor);

                byte[] cipherText = new byte[cipherLength];
                Buffer.BlockCopy(raw, cipherStart, cipherText, 0, cipherLength);

                int headerLength = cipherStart + cipherLength;
                byte[] header = new byte[headerLength];
                Buffer.BlockCopy(raw, 0, header, 0, headerLength);

                byte[] hmac = new byte[hmacLength];
                Buffer.BlockCopy(raw, headerLength, hmac, 0, hmacLength);

                byte[] key;
                byte[] hmacKey;
                using (var keyDeriv = new Rfc2898DeriveBytes(TailscaleKey, salt, 10000))
                {
                    key = keyDeriv.GetBytes(32);
                    hmacKey = keyDeriv.GetBytes(32);
                }

                using (var hmacSha = new HMACSHA256(hmacKey))
                {
                    byte[] computed = hmacSha.ComputeHash(header);
                    if (!BytesIguales(computed, hmac))
                        return string.Empty;
                }

                using (var aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherText, 0, cipherText.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                return DesencriptarLegacy(valor);
            }
        }

        private static bool BytesIguales(byte[] a, byte[] b)
        {
            // Comparación en tiempo constante para evitar timing attacks
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private static string DesencriptarLegacy(string valor)
        {
            // Tu lógica legacy aquí
            return string.Empty;
        }
    }

}
