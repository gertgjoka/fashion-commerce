using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IShoppingCartManager : IManager<SHOPPING_CART>
    {
        void Update(SHOPPING_CART Entity, int PreviousQuantity);
        void DeleteById(String Id, int ProdAttrID);
        List<SHOPPING_CART> GetShoppingCartItems(String Id);
        SHOPPING_CART GetById(String Id, int ProdAttrID);
        void DeleteShoppingCart(String Id);
        Decimal? GetShoppingCartTotalAmount(String Id);
        int? GetShoppingCartTotalItems(String Id);
        List<String> GetShoppingCarts();
        List<String> GetExpiredCarts();
    }
}
