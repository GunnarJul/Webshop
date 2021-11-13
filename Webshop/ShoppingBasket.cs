using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    public class ShoppingBasket

    {
        private readonly IDatabase _db;
        public ShoppingBasket(IDatabase db)
        {
            _db = db;
        }
        /// <summary>
        /// Tilføjer en vare til indkøbskurven
        /// </summary>
        /// <param name="brugernavn">brugerens id</param>
        /// <param name="varenummer">varenummer på den vare der skal tilføjet</param>
        /// <param name="antal">antallet af varer</param>
        /// <returns>Det samlede antal varer i kurven</returns>
        public int AddToBasket(string brugernavn, int varenummer, int antal)
        {
            
            if (antal > 0)
            {
                _db.AddToBasket(brugernavn, varenummer, antal);
            } else if (antal == 0)
            {
                _db.RemoveFromBasket(brugernavn, varenummer);
            }
            
            BasketEntry[] varer = _db.GetBasket(brugernavn);
            int totalAntal = 0;
            foreach (BasketEntry vare in varer) {
                totalAntal = totalAntal + vare.Antal;
            }
            return totalAntal;
        }

        /// <summary>
        /// Returnerer den samlede pris for alle varer i indkøbskurven
        /// 
        /// </summary>
        /// <param name="brugernavn">brugerens id</param>
        /// <returns>Den samlede pris som et beløb i kr.</returns>
        public double GetTotalPrice(string brugernavn)
        {
            
            BasketEntry[] varer = _db.GetBasket(brugernavn);
            int totalPris = 0;
            foreach (BasketEntry entry in varer)
            {
                Article vare = _db.GetArticle(entry.Varenummer);
                totalPris = totalPris + entry.Antal * (int)vare.Price;
            }
            return totalPris;

        }
    }
}
