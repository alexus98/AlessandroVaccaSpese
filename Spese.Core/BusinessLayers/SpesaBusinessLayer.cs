using Spese.Core.Interfaces;
using Spese.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spese.Core.BusinessLayers
{
    public class SpesaBusinessLayer
    {
        private readonly ISpesaRepository spesaRepository;
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IUtenteRepository utenteRepository;

        public SpesaBusinessLayer(ISpesaRepository spesaRepository, ICategoriaRepository categoriaRepository, IUtenteRepository utenteRepository)
        {
            this.spesaRepository = spesaRepository;
            this.categoriaRepository = categoriaRepository;
            this.utenteRepository = utenteRepository;
        }


        #region Spesa
        public bool InsertNewUtente(Spesa s)
        {
            return spesaRepository.Add(s);
        }

        public List<Spesa> FetchAllSpese()
        {
            return spesaRepository.Fetch().ToList();
        }

        public Spesa FetchSpesaById(int id)
        {
            return spesaRepository.Fetch(s => s.Id == id).FirstOrDefault();
        }

        public bool ApprovaSpesa(int id)
        {
            return spesaRepository.Approva(id);
        }

        public List<Spesa> FetchSpeseApprovateMesePrecedente()
        {
                int LastMonth = DateTime.Today.Month == 1 ? 12 : DateTime.Today.Month - 1;
                int LastYear = LastMonth == 12 ? DateTime.Today.Year - 1 : DateTime.Today.Year;
                return spesaRepository.Fetch(s => s.Data.Month == LastMonth && s.Data.Year == LastYear  && s.Approvato == true).ToList();
        }

        public List<Spesa> FetchSpeseById(int id)
        {
            return spesaRepository.Fetch(s => s.UtenteId == id).ToList();
        }

        public decimal GetTotaleSpesoByCategoria(int idCategoria)
        {
            return spesaRepository.Fetch(S => S.CategoriaId == idCategoria).Sum(S => S.Importo);
        }

        public List<Spesa> FetchAllSpeseOrdinate()
        {
            return spesaRepository.Fetch().OrderByDescending(S => S.Data).ToList();
        }
        #endregion


        #region Categorie
        public List<Categoria> FetchAllCategorie()
        {
            return categoriaRepository.Fetch().ToList();
        }

        public Categoria FetchCategoriaById(int id)
        {
            return categoriaRepository.Fetch(c => c.Id == id).FirstOrDefault();
        }
        #endregion


        #region Utente
        public List<Utente> FetchAllUtenti()
        {
            return utenteRepository.Fetch().ToList();
        }

        public Utente FetchUtenteById(int id)
        {
            return utenteRepository.Fetch(u => u.Id == id).FirstOrDefault();
        }
        #endregion
    }
}
