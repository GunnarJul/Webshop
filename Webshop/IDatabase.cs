namespace Webshop
{
    public interface IDatabase
    {
        void AddToBasket(string brugernavn, int varenummer, int antal);
        Article GetArticle(int varenummer);
        BasketEntry[] GetBasket(string brugernavn);
        void RemoveFromBasket(string brugernavn, int varenummer);
    }
}