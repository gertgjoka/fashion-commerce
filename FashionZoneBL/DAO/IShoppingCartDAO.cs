using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface IShoppingCartDAO : IDAO<SHOPPING_CART>
    {
        SHOPPING_CART GetById(String Id, int ProdAttrID);
        
        /// <summary>
        /// This must never be used alone.
        /// To guarantee product availability consistency, the method must be called inside an ObjectContext transaction scope which also
        /// updates the availability of product quantities.
        /// </summary>
        /// <param name="Id"> The guid identifying this particular shopping cart</param>
        void DeleteShoppingCart(String Id);
        List<SHOPPING_CART> GetShoppingCartItems(String Id);
        Decimal? GetShoppingCartTotalAmount(String Id);
        int? GetShoppingCartTotalItems(String Id);
        List<String> GetShoppingCarts();
        void Delete(SHOPPING_CART Entity, bool Attach = true);
        List<String> GetExpiredCarts();
    }
}
