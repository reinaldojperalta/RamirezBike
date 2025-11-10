using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class HasheoClave
    {
        // Las constantes deben ser estáticas o estar en esta clase para acceder a ellas
        private const int Iterations = 10000; // Valor seguro
        private const int SaltSize = 16;
        private const int HashSize = 20;

        public static string MtHashClave(string clave)
        {
            // ... (El código de tu MtHashClave aquí, usando las constantes de esta clase)
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // ... generación de hash y combinación ...

            var pbkdf2 = new Rfc2898DeriveBytes(clave, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool MtVerificarClave(string claveIngresada, string hashAlmacenado)
        {
            // ... (El código de tu MtVerificarClave aquí, usando las constantes de esta clase)
            byte[] hashBytes = Convert.FromBase64String(hashAlmacenado);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(claveIngresada, salt, Iterations);
            byte[] hashIngresado = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                // La comparación debe ser contra el hash almacenado, que empieza después del salt
                if (hashBytes[i + SaltSize] != hashIngresado[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
