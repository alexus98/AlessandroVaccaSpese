using Spese.Core.Models;
using Spese.Core.Interfaces;
using Spese.Core.BusinessLayers;
using Spese.Mock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spese.Client
{
    internal class Menu
    {
        private static readonly SpesaBusinessLayer businessLayer = new SpesaBusinessLayer(new MockSpesa(), new MockCategoria(), new MockUtente());

        internal static void Start()
        {
            char scelta;

            do
            {
                Console.WriteLine("Scegli 1 per Inserire una nuova spesa" +
                    "\nScegli 2 per approvare una spesa" +
                    "\nScegli 3 per visualizzare le spese approvate nel mese precedente" +
                    "\nScegli 4 per Visualizzare le spese di un utente" +
                    "\nScegli 5 per Visualizzare il totale speso in una determinata categoria" +
                    "\nScegli 6 per Visualizzare le spese registrate ordinate dalla piu recente alla meno recente" +
                    "\nScegli 7 per Visualizzare il totale speso per ogni categoria" +
                    "\nScegli C per pulire la console" +
                    "\nScegli Q per uscire");

                scelta = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (scelta)
                {
                    case '1':
                        InserisciNuovaSpesa();
                        break;
                    case '2':
                        ApprovaSpesa();
                        break;
                    case '3':
                        GetSpeseApprovateMesePrecedente();
                        break;
                    case '4':
                        GetSpeseByUtente();
                        break;
                    case '5':
                        GetTotaleSpesoByCategoria();
                        break;
                    case '6':
                        GetSpeseOrdinate();
                        break;
                    case '7':
                        GetTotaleSpesoByAllCategorie();
                        break;
                    case 'C':
                        Console.Clear();
                        break;
                    case 'Q':
                        Console.WriteLine("Arrivederci");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida");
                        break;
                }

            } while (scelta != 'Q');
        }

        public static void GetTotaleSpesoByAllCategorie()
        {
            foreach (Categoria c in businessLayer.FetchAllCategorie())
            {
                businessLayer.FetchCategoriaById(c.Id);
                Console.WriteLine("Il totale speso nella categoria " + c.Nome + " è: " + businessLayer.GetTotaleSpesoByCategoria(c.Id));
            }
        }
        public static void GetSpeseOrdinate()
        {
            List<Spesa> spese = businessLayer.FetchAllSpeseOrdinate();
            foreach (Spesa s in spese)
            {
                Console.WriteLine(s);
            }
        }

        public static void GetTotaleSpesoByCategoria()
        {
            int categoriaId;
            Categoria categoria;
            PrintCategorie();
            do
            {
                categoriaId = GetNumberInt("l'id della categoria ");
                categoria = businessLayer.FetchCategoriaById(categoriaId);
            } while (categoria == null);

            Console.WriteLine("Il totale speso nella categoria " + categoria.Nome + " è: " + businessLayer.GetTotaleSpesoByCategoria(categoriaId));
        }

        public static void GetSpeseByUtente()
        {
            int utenteId;
            PrintAllUtenti();
            do
            {
                utenteId = GetNumberInt("l'id dell'utente ");
            } while (businessLayer.FetchUtenteById(utenteId) == null);

            List<Spesa> spese = businessLayer.FetchSpeseById(utenteId);
            PrintSpese(spese);
        }

        public static void GetSpeseApprovateMesePrecedente()
        {
            List<Spesa> spese = businessLayer.FetchSpeseApprovateMesePrecedente();
            foreach (Spesa s in spese)
            {
                Console.WriteLine(s);
            }
        }

        public static void ApprovaSpesa()
        {
            int spesaId;
            PrintAllSpese();
            do
            {
                spesaId = GetNumberInt("l'id della spesa ");
            } while (businessLayer.FetchSpesaById(spesaId) == null);

            if (businessLayer.ApprovaSpesa(spesaId))
                Console.WriteLine("Spesa Approvata");
            else
                Console.WriteLine("Spesa non approvata");
        }

        public static void InserisciNuovaSpesa()
        {
            int id, categoriaId, utenteId;
            string descrizione;
            decimal importo;

            PrintCategorie();
            do
            {
                categoriaId = GetNumberInt("l'id della categoria ");
            } while (businessLayer.FetchCategoriaById(categoriaId) == null);

            PrintAllUtenti();
            do
            {
                utenteId = GetNumberInt("l'id dell'utente ");
            } while (businessLayer.FetchUtenteById(utenteId) == null);
            
            importo = GetNumberDecimal("l'importo della spesa ");
            descrizione = GetString("la descrizione della spesa ");
            id = NewSpesaId();

            Spesa s = new Spesa() { Id = id, Approvato = false, CategoriaId = categoriaId, Data = DateTime.Now, Descrizione = descrizione, Importo = importo, UtenteId = utenteId};


            if(businessLayer.InsertNewUtente(s))
                Console.WriteLine("-- La tua spesa verrà approvata a breve --\n");
            else
                Console.WriteLine("-- C'è stato qualche problema con la tua spesa --\n");
        }

        private static void PrintSpese(List<Spesa> spese)
        {
            foreach (Spesa s in spese)
            {
                Console.WriteLine(s);
            }
        }

        private static void PrintAllSpese()
        {
            List<Spesa> spese = businessLayer.FetchAllSpese();
            foreach (Spesa s in spese)
            {
                Console.WriteLine(s);
            }
        }

        private static int NewSpesaId()
        {
            return businessLayer.FetchAllSpese().Last().Id + 1;
            
        }

        private static void PrintAllUtenti()
        {
            List<Utente> utenti = businessLayer.FetchAllUtenti();
            foreach (Utente u in utenti)
            {
                Console.WriteLine(u);
            }
        }
        
        private static void PrintCategorie()
        {
            List<Categoria> categorie= businessLayer.FetchAllCategorie();
            foreach (Categoria c in categorie)
            {
                Console.WriteLine(c);
            }
        }

        private static string GetString(string message)
        {
            string str;

            Console.Write("\nInserisci " + message);

            do
            {
                str = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(str))
                    Console.Write("Inserisci una stringa valida");
            } while (string.IsNullOrWhiteSpace(str));

            return str;
        }

        private static int GetNumberInt(string message)
        {
            Console.Write("\nInserisci " + message);

            int number;

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("\nPuoi inserire solo numeri interi. Riprova: ");
            }

            return number;
        }

        private static decimal GetNumberDecimal(string message)
        {
            Console.Write("\nInserisci " + message);

            decimal number;

            while (!decimal.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("\nInserisci un numero valido. Riprova: ");
            }

            return number;
        }
    }
}
