using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public class Crypto
    {
        protected string password = "Anet";
        protected string salt = "SystemAndersson";


        public byte[] Encrypt(string Message)
        {
            //byte[] encryptedMessage = EncryptMessage(message, password, salt);
            return EncryptMessage(Message, password, salt);//Encoding.ASCII.GetString(encryptedMessage);
        }


        public string Decrypt(byte[] Message)
        {


            //byte[] messageByteArray = Encoding.ASCII.GetBytes(Message);
            return DecryptMessage(Message, password, salt);
        }

        public string Decrypt(byte[] Message, int pwdLength)
        {


            //byte[] messageByteArray = Encoding.ASCII.GetBytes(Message);
            return DecryptMessage(Message, password, salt, pwdLength);
        }

        private static byte[] EncryptMessage(string message, string password, string salt)
        {
            // Skapa AES-algoritmobjektet
            RijndaelManaged cipher = CreateAesWithKeys(password, salt);

            // Skapa ett krypteringsobjekt
            ICryptoTransform encryptor = cipher.CreateEncryptor();

            // Skapa en krypteringsström som omsluts av en strömskrivare som skriver ner det
            // krypterade datat till en minnesström.
            MemoryStream memStream = new MemoryStream();
            CryptoStream encryptStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);
            StreamWriter streamWriter = new StreamWriter(encryptStream);

            streamWriter.Write(message);

            // Se till att stänga alla strömmar för att säkerställa att allt data töms
            streamWriter.Close();
            encryptStream.Close();
            memStream.Close();

            // Konvertera data från minnesströmmen till en byte-vektor
            byte[] encryptedArray = memStream.ToArray();
            return encryptedArray;
        }


        private static string DecryptMessage(byte[] message, string password, string salt)
        {
            RijndaelManaged cipher = CreateAesWithKeys(password, salt);

            ICryptoTransform decryptor = cipher.CreateDecryptor();

            MemoryStream memStream = new MemoryStream(message);
            CryptoStream decryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(decryptStream);
            string decryptedMessage = streamReader.ReadToEnd();

            streamReader.Close();
            decryptStream.Close();
            memStream.Close();

            decryptedMessage = decryptedMessage.Remove(6);
            decryptedMessage = decryptedMessage.Replace("\0", "");
            return decryptedMessage;
        }


        private static string DecryptMessage(byte[] message, string password, string salt, int pwdLength)
        {
            RijndaelManaged cipher = CreateAesWithKeys(password, salt);

            ICryptoTransform decryptor = cipher.CreateDecryptor();

            MemoryStream memStream = new MemoryStream(message);
            CryptoStream decryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(decryptStream);
            string decryptedMessage = streamReader.ReadToEnd();

            streamReader.Close();
            decryptStream.Close();
            memStream.Close();

            decryptedMessage = decryptedMessage.Remove(pwdLength);
            decryptedMessage = decryptedMessage.Replace("\0", "");
            return decryptedMessage;
        }

        private static RijndaelManaged CreateAesWithKeys(string password, string salt)
        {
            byte[] saltAsBytes = Encoding.ASCII.GetBytes(salt);
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, saltAsBytes);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Key = key.GetBytes(cipher.KeySize / 8);
            cipher.IV = key.GetBytes(cipher.BlockSize / 8);
            cipher.Padding = PaddingMode.Zeros;

            return cipher;
        }

    }
}
