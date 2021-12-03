using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spese.Core.Interfaces;
using Spese.Core.Models;

namespace Spese.Mock.Repositories
{
    public class MockUtente : IUtenteRepository
    {
        public bool Add(Utente item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Utente> Fetch(Func<Utente, bool> lambda = null)
        {
            if (lambda != null)
                return InMemoryStorage.utenti.Where(lambda);
            return InMemoryStorage.utenti;
        }
    }
}
