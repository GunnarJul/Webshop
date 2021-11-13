using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    /// <summary>
    /// Database-klasse. Alle metoder i denne klasse kaster en exception, fordi du ikke har adgang til databasen i test.
    /// Du skal ikke rette noget i denne klasse.
    /// 
    /// </summary>
    public class Database : IDatabase
    {
        public void AddToBasket(string brugernavn, int varenummer, int antal)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromBasket(string brugernavn, int varenummer)
        {
            throw new NotImplementedException();
        }

        public BasketEntry[] GetBasket(string brugernavn)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int varenummer)
        {
            throw new NotImplementedException();
        }
    }
}
