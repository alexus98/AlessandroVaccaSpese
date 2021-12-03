using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spese.Core.Models
{
    public class Utente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public override string ToString()
        {
            return "Id Utente: " + Id + " Nome: " + Nome + " Cognome: " + Cognome;
        }

    }

}
