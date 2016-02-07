using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.Manager
{
    public interface IOrderManager : IManager<ORDERS>
    {
        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <returns></returns>
        List<D_ORDER_STATUS> GetStatuses();

        /// <summary>
        /// Gets the shipping types.
        /// </summary>
        /// <returns></returns>
        List<SHIPPING> GetShippings();

        SHIPPING GetShipping(int Id);
        //void RemoveUnusedBonusesFromOrder(ORDERS Order, bool Save, SortedList<int, byte[]> Bonuses);
        
        /// <summary>
        /// Gets the order bonuses.
        /// </summary>
        /// <param name="Order">The order.</param>
        /// <returns></returns>
        List<ORDER_BONUS> GetOrderBonuses(ORDERS Order);

        string Update(ORDERS Order, bool Attach = true);
       
        /// <summary>
        /// Searches the order bonuses.
        /// </summary>
        /// <param name="BonusID">The bonus ID.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="PageIndex">Index of the page.</param>
        /// <param name="TotalRecords">The total records.</param>
        /// <param name="OrderExp">The order exp.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <returns></returns>
        List<ORDER_BONUS> SearchOrderBonuses(int BonusID, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection);

        /// <summary>
        /// Inserts the details from cart.
        /// </summary>
        /// <param name="Order">The order.</param>
        /// <param name="Cart">The cart.</param>
        void InsertDetailsFromCart(ORDERS Order, List<SHOPPING_CART> Cart, bool Attach = true);

        List<ORDER_DETAIL> GetDetails(int OrderID);
        /// <summary>
        /// Gets the orders for return.
        /// </summary>
        /// <param name="idCustomer">The id customer.</param>
        /// <param name="validDate">The valid date.</param>
        /// <returns></returns>
        List<ORDERS> GetOrdersForReturn(int idCustomer, DateTime validDate);

        string Insert(ORDERS Order, bool FrontEnd, bool SaveContext = true);

        void InsertAddress(ADDRESSINFO Address);
        void UpdateAddress(ADDRESSINFO Address);
        void DeleteAddress(ADDRESSINFO Address);
        ADDRESSINFO GetAddress(int Id);


        List<ORDER_NOTES> GetNotes(int OrderId);
        void InsertNote(ORDER_NOTES Note);
        void DeleteNote(int NoteId);

        string GetOrderCampaigns(int OrderId);

        void CancelOrderById(int OrderId);
    }
}
