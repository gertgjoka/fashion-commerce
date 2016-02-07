using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO
{
    public interface IOrderDAO : IDAO<ORDERS>
    {
        /// <summary>
        /// Get a listo of ORDERS.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" ORDERS.</returns>
        List<ORDERS> GetOrdersTopList(int length);

        void Update(ORDERS Order, bool Attach = true);
        /// <summary>
        /// Removes the bonus.
        /// </summary>
        /// <param name="Bonus">The bonus.</param>
        void RemoveBonus(ORDER_BONUS Bonus);

        /// <summary>
        /// Gets the order bonuses.
        /// </summary>
        /// <param name="Order">The order.</param>
        /// <returns></returns>
        List<ORDER_BONUS> GetOrderBonuses(ORDERS Order);

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
        /// Gets the orders for return.
        /// </summary>
        /// <param name="idCustomer">The id customer.</param>
        /// <param name="validDate">The valid date.</param>
        /// <returns></returns>
        List<ORDERS> GetOrdersForReturn(int idCustomer, DateTime validDate);

        /// <summary>
        /// Inserts the detail.
        /// </summary>
        /// <param name="Detail">The detail.</param>
        void InsertDetail(ORDER_DETAIL Detail);
        List<ORDER_DETAIL> GetDetails(int OrderID);

        List<ORDER_NOTES> GetNotes(int OrderId);
        void InsertNote(ORDER_NOTES Note);
        void DeleteNote(int NoteId);

        string GetOrderCampaigns(int OrderId);
    }
}
