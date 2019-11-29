using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class LinkedinRepository : ILinkedinRepository
    {
        readonly ISecretsRepository _secretsRepository;
        public LinkedinRepository(ISecretsRepository secrets)
        {
            _secretsRepository = secrets;
        }
        public string GetLinkedInProfileLink()
        {
            return _secretsRepository.GetSecret("LinkedinProfileLink");
        }
    }
}
