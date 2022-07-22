using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizeApi.Utils
{
    public class RSAHelper
    {
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters rSAParameters)
        {
            string filename = withPrivate ? "key.json" : "key.publish.json";
            string fileTotalPath = Path.Combine(filePath, filename);
            rSAParameters = default(RSAParameters);

            if (!File.Exists(fileTotalPath))
            {
                return false;
            }
            else
            {
                rSAParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(fileTotalPath));
                return true;
            }
        }

        public static RSAParameters GenerateAndSaveKey(string filePath, bool withPrivate = true)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonConvert.SerializeObject(privateKeys));
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonConvert.SerializeObject(publicKeys));

            return withPrivate ? privateKeys : publicKeys;

        }

        public static string RSAEncrypt(string plainText)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] DataToEncrypt = Encoding.UTF8.GetBytes(plainText);

            RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
            byte[] bytes_Cyper_Str = rSA.Encrypt(DataToEncrypt, false);
            string str_cypher = Convert.ToBase64String(bytes_Cyper_Str);
            return str_cypher;
        }
    }
}
