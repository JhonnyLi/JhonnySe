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
            var secret = await _secretClient.GetSecretAsync(keyName).ConfigureAwait(false) ?? throw new ArgumentNullException();
            return secret.Value.ToString();
        }

        public string GetSecret(string keyName)
        {
            var secret = _secretClient.GetSecret(keyName) ?? throw new ArgumentNullException();
            return secret.Value.Value.ToString();
        }

        private Uri InitializeKeyVault()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME", EnvironmentVariableTarget.User);
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            return new Uri(kvUri);
        }

    }
}
