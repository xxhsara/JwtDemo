using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ResourceService
{
    public class RSAHelper
    {
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters rSAParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
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

        public static RSAParameters GenerateAndSaveKey(string filePath,bool withPrivate=true)
        {
            RSAParameters publicKeys, privateKeys;
            using(var rsa=new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                catch(Exception ex)
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
    }
}
