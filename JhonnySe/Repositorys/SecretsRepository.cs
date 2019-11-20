using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Uri InitializeKeyVault()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME", EnvironmentVariableTarget.User);
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            return new Uri(kvUri);
        }

    }
}
