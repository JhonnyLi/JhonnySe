using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class SecretsRepository : ISecretsRepository
    {
        private readonly SecretClient _secretClient;
        private readonly ILogger _logger;

        public SecretsRepository(ILogger<ISecretsRepository> logger)
        {
            _logger = logger;
            try
            {
                var uri = InitializeKeyVault();
                _logger.LogInformation("Getting information from key-vault");
                _secretClient = new SecretClient(uri, new DefaultAzureCredential());
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Coult not connect to key-vault");
                _logger = null;
                _secretClient = null;
            }
        }

        public async Task<string> GetSecretAsync(string keyName)
        {
            var secret = await _secretClient.GetSecretAsync(keyName).ConfigureAwait(false) ?? throw new ArgumentNullException();
            return secret.Value.ToString();
        }

        public string GetSecret(string keyName)
        {
            try
            {
                var secret = _secretClient.GetSecret(keyName);
                return secret.Value.Value.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get secrets from vault.");
                throw ex;
            }
        }

        private Uri InitializeKeyVault()
        {
            string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME", EnvironmentVariableTarget.User);
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            return new Uri(kvUri);
        }

    }
}
