using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public interface ISecretsRepository
    {
        Task<string> GetSecretAsync(string keyName);
        string GetSecret(string keyName);
    }
}
