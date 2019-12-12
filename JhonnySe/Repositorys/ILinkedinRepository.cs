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
