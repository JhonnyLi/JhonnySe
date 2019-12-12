using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public interface ILinkedinRepository
    {
        string GetLinkedInProfileLink();
        Task<string> GetAuthToken();
        void SetBearerToken(string token);
    }
}
