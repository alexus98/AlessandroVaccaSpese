using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spese.Core.Interfaces;
using Spese.Core.Models;

namespace Spese.Mock.Repositories
{
    public class MockCategoria : ICategoriaRepository
    {
        public bool Add(Categoria item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categoria> Fetch(Func<Categoria, bool> lambda = null)
        {
            if (lambda != null)
                return InMemoryStorage.categorie.Where(lambda);
            return InMemoryStorage.categorie;
        }
    }
}
