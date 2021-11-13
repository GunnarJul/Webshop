using Moq;
using System;
using System.Collections.Generic;
using Webshop;
using Xunit;

namespace WebShopTest
{
    public class BasketIntegrationTest
    {

        private Mock<IDatabase> SetupDb()
        {
            
            var stubDb = new Mock<IDatabase>();
            stubDb.Setup(s => s.AddToBasket(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            stubDb.Setup(s => s.GetArticle(It.IsAny<int>())).Returns(It.IsAny<Article>());
            //stubDb.Setup(s => s.GetBasket(It.IsAny<string>())).Returns(It.IsAny<List<BasketEntry>>().ToArray());
            stubDb.Setup(s => s.RemoveFromBasket(It.IsAny<string>(), It.IsAny<int>()  ));
            return stubDb;
        }


        [Fact]
        public void En_tilføjet_varer_incremere_basket()
        {
            // arrange 
            var db = SetupDb();
            var basket = new ShoppingBasket(db.Object);
            // act
            basket.AddToBasket("mig", 1, 1);

            // assert
            db.Verify(api => api.AddToBasket(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory] 
        [InlineData(1) ]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(13)]
        [InlineData(21)]
        public void Flere_tilføjet_varer_incremere_basket(int antalKald )
        {
            // arrange 
            var db = SetupDb();
            var basket = new ShoppingBasket(db.Object);
            // act
            for (var idx = 0; idx < antalKald; idx++ )
              basket.AddToBasket("mig", 1, 1);

            // assert
            db.Verify(api => api.AddToBasket(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly( antalKald));
        }

        [Fact]
        public void Når_indkøb_fortrydes_tømmes_basket()
        {
            // arrange 
            var db = SetupDb();
            var basket = new ShoppingBasket(db.Object);
            
            // act
            var expected = 10;
            for (var idx = 0; idx < expected; idx++)
                basket.AddToBasket("mig", 1, 1);
            basket.AddToBasket("mig", 1, 0);
            
            // assert
            db.Verify(api => api.AddToBasket(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(expected));
            db.Verify(api => api.RemoveFromBasket (It.IsAny<string>(),   It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public void Når_pris_beregnes_bestemmes_valgte_artikler()
        {
            // arrange 
            var db = SetupDb();
            var basket = new ShoppingBasket(db.Object);

            // act
            basket.AddToBasket("mig", 1, 1);
            basket.AddToBasket("mig", 2, 1);
            basket.GetTotalPrice("mig");

            // assert
            db.Verify(api => api.GetArticle( It.IsAny<int>()), Times.Exactly(2));
         }

        [Fact]
        public void Når_pris_beregnes_bruges_basket()
        {
            // arrange 
            var db = SetupDb();
            var basket = new ShoppingBasket(db.Object);

            // act
            basket.AddToBasket("mig", 1, 1);
            basket.AddToBasket("mig", 2, 1);
            basket.GetTotalPrice("mig");

            // assert
            db.Verify(api => api.GetBasket (It.IsAny<string>()), Times.Once);
        }
    }
}
