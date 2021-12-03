using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spese.Core.Interfaces;
using Spese.Core.Models;

namespace Spese.Mock.Repositories
{
    public class MockSpesa : ISpesaRepository
    {
        public bool Add(Spesa item)
        {
            InMemoryStorage.spese.Add(item);
            return true;
        }

        public IEnumerable<Spesa> Fetch(Func<Spesa, bool> lambda = null)
        {
            if (lambda != null)
                return InMemoryStorage.spese.Where(lambda);
            return InMemoryStorage.spese;
        }

        public bool Approva(int id)
        {
            Spesa s = InMemoryStorage.spese.Where(s => s.Id == id).FirstOrDefault();
            if (s != null)
            {
                s.Approvato = true;
                return true;
            }
            else
                return false;
        }
    }
}
