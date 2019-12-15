using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class SecretsRepository : ISecretsRepository
    {
        private readonly SecretClient _secretClient;

        public SecretsRepository()
        {
            var uri = InitializeKeyVault();
            _secretClient = new SecretClient(uri, new DefaultAzureCredential());
        }

        public async Task<string> GetSecretAsync(string keyName)
        {
            var secret = await _secretClient.GetSecretAsync(keyName).ConfigureAwait(false);
            return secret.Value.ToString();
        }

        public string GetSecret(string keyName)
        {
            var secret = _secretClient.GetSecret(keyName);
            return secret.Value.Value.ToString();
        }

        private static Uri InitializeKeyVault()
        {
            var kvUri = "https://" + "TimeKeeperVault" + ".vault.azure.net";
            return new Uri(kvUri);
        }
        
    }
}
