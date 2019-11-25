using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class SecretsRepository : ISecretsRepository
    {

        private readonly ILogger _logger;
        private readonly SecretClient _secretClient;
        private readonly IKeyVaultClient _liveClient;
        private readonly IHostEnvironment _env;

        public SecretsRepository(ILogger<ISecretsRepository> logger, IKeyVaultClient client, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _logger.LogInformation("Initializing key vault");
            if (_env.IsProduction())
            {
                _liveClient = client;
            }
            else
            {
                try
                {
                    var uri = InitializeKeyVault();
                    _secretClient = new SecretClient(uri, new DefaultAzureCredential());
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Coult not connect to key-vault");
                    _logger = null;
                    _secretClient = null;
                }
            }
        }

        public async Task<string> GetSecretAsync(string keyName)
        {
            if (_env.IsProduction())
            {
                var secret = await _liveClient.GetSecretAsync(keyName);
                return secret.Value.ToString();
            }
            else
            {
                var secret = await _secretClient.GetSecretAsync(keyName);
                return secret.Value.ToString();
            }
        }

        public string GetSecret(string keyName)
        {
            if (_env.IsProduction())
            {
                var secret = _liveClient.GetSecretAsync(keyName).Result;
                return secret.Value.ToString();
            }
            else
            {
                var secret = _secretClient.GetSecret(keyName);
                return secret.Value.ToString();
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
