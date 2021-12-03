using Spese.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spese.Mock.Repositories
{
    public class InMemoryStorage
    {
        public static List<Categoria> categorie = new List<Categoria>()
        {
            new Categoria() { Id = 1, Nome = "Casa" },
            new Categoria() { Id = 2, Nome = "Alimenti" },
            new Categoria() { Id = 3, Nome = "Elettronica" }
        };

        public static List<Utente> utenti = new List<Utente>()
        {
            new Utente() {Id = 1, Nome = "Mario", Cognome = "Rossi"},
            new Utente() {Id = 2, Nome = "Lucia", Cognome = "Bianchi"},
            new Utente() {Id = 3, Nome = "Alice", Cognome = "Verdi"}
        };

        public static List<Spesa> spese = new List<Spesa>()
        {
            new Spesa() {Id = 1, UtenteId = 1, CategoriaId = 1, Approvato = true, Data = new DateTime(2021,10,02), Descrizione = "Detersivi", Importo = 12.50m},
            new Spesa() {Id = 2, UtenteId = 2, CategoriaId = 2, Approvato = false, Data = new DateTime(2021,11,11), Descrizione = "Pane", Importo = 3.0m},
            new Spesa() {Id = 3, UtenteId = 3, CategoriaId = 2, Approvato = true, Data = new DateTime(2021,12,02), Descrizione = "Cibo", Importo = 20.99m},
            new Spesa() {Id = 4, UtenteId = 2, CategoriaId = 3, Approvato = true, Data = new DateTime(2021,11,22), Descrizione = "TV", Importo = 400.0m}
        };
    }
}
