using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;
using System.Transactions;
using System.Data;
using FashionZone.BL.Util;

namespace FashionZone.BL.Manager.Impl
{
    /// <summary>
    /// Handles all the business logic related to order processing.
    /// Manager classes orchestrate the behaviour by using the necessary DAOs
    /// DAOs is an access object to a single entity and must not use other DAOs
    /// </summary>
    public class OrderManager : BaseManager, IOrderManager
    {
        private IOrderDAO _orderDAO;
        private IProductDAO _productDAO;
        private IBonusDAO _bonusDAO;
        private IShoppingCartDAO _cartDAO;
        private IAddressInfoDAO _addressInfoDAO;
        private ICustomerDAO _customerDAO;

        public OrderManager(IContextContainer container)
            : base(container)
        {
            _orderDAO = new OrderDAO(container);
            _productDAO = new ProductDAO(container);
            _bonusDAO = new BonusDAO(container);
            _cartDAO = new ShoppingCartDAO(container);
            _addressInfoDAO = new AddressInfoDAO(container);
            _customerDAO = new CustomerDAO(container);
        }

        public List<D_ORDER_STATUS> GetStatuses()
        {
            List<D_ORDER_STATUS> statuses = Context.D_ORDER_STATUS.OrderBy(x => x.ID).ToList();
            return statuses;
        }

        public void Insert(ORDERS Order)
        {
            Insert(Order, true);
        }

        public string Insert(ORDERS Order, bool FrontEnd, bool SaveContext = true)
        {
            string retString = String.Empty;
            //if there is any used bonus, decrease the remainder
            if (Order.ORDER_BONUS.Count > 0)
            {
                BONUS bonus;
                foreach (ORDER_BONUS bon in Order.ORDER_BONUS)
                {
                    bonus = _bonusDAO.GetById(bon.BonusID);

                    // object must be detached from context, otherwise Version field for concurrency check will not be updated!!!
                    Context.BONUS.Detach(bonus);

                    // using the version that was initially retrieved from db
                    // if another user modified this record an OptimisticConcurrencyException will be raised
                    if (bon.Value > bonus.ValueRemainder)
                    {
                        //TODO think what to do with version
                        //bonus.Version = bon.BONUS.Version;
                        throw new System.Data.OptimisticConcurrencyException("Bonus not sufficient.");
                    }
                    bonus.ValueRemainder -= bon.Value;

                    _bonusDAO.Update(bonus);
                    bon.BONUS = null;
                }
            }

            CUSTOMER customer = _customerDAO.GetById(Order.CustomerID.Value);

            // for electronic payments insert directly
            if ((Order.PAYMENT.Type == (int)PaymentType.PP && Order.PAYMENT.PAYPAL_PAYMENT.TransactionStatus == "Completed")
                    || (Order.PAYMENT.Type == (int)PaymentType.EP && Order.PAYMENT.EASYPAY_PAYMENT.TransactionStatus == "Success")
                    || (Order.PAYMENT.Type == (int)PaymentType.CA && !FrontEnd && Order.Completed))
            {
                if (!customer.FirstBuy.HasValue || !customer.FirstBuy.Value)
                {
                    // set first buy of user
                    customer.FirstBuy = true;
                    _customerDAO.Update(customer, true, false);

                    // first buy of this customer & invited by someone
                    if (customer.InvitedFrom.HasValue)
                    {
                        insertCustomerBonus(customer);
                        retString = "Bonus added to inviter " + customer.CUSTOMER2.Email;
                        Util.Mailer.SendBonus(customer, customer.CUSTOMER2.Email, Configuration.BonusValue + " € bonus per ju ne FZone");
                    }
                }
            }
            Guid g = Guid.NewGuid();
            UniqueIdGenerator unique = UniqueIdGenerator.GetInstance();
            string vNum = unique.GetBase32UniqueId(g.ToByteArray(), 20).ToLower();
            Order.VerificationNumber = vNum;
            _orderDAO.Insert(Order);

            // payment
            if (Order.PAYMENT != null)
            {
                Context.ObjectStateManager.ChangeObjectState(Order.PAYMENT, EntityState.Added);
                if (Order.PAYMENT.Type == (int)PaymentType.CA)
                {
                    Context.ObjectStateManager.ChangeObjectState(Order.PAYMENT.CASH_PAYMENT, System.Data.EntityState.Added);
                }
                else if (Order.PAYMENT.Type == (int)PaymentType.PP)
                {
                    Context.ObjectStateManager.ChangeObjectState(Order.PAYMENT.PAYPAL_PAYMENT, System.Data.EntityState.Added);
                }
                else if (Order.PAYMENT.Type == (int)PaymentType.EP)
                {
                    Context.ObjectStateManager.ChangeObjectState(Order.PAYMENT.EASYPAY_PAYMENT, System.Data.EntityState.Added);
                }
            }

            if (SaveContext)
            {
                Context.SaveChanges();
            }
            return retString;
        }

        private void insertCustomerBonus(CUSTOMER customer)
        {
            BONUS b = new BONUS()
            {
                CustomerID = customer.InvitedFrom.Value,
                DateAssigned = DateTime.Today,
                Validity = DateTime.Today.AddMonths(2),
                Description = "Per ftesen e " + customer.Email,
                Value = Configuration.BonusValue,
                ValueRemainder = Configuration.BonusValue
            };
            _bonusDAO.Insert(b);
        }

        public void InsertDetailsFromCart(ORDERS Order, List<SHOPPING_CART> Cart, bool Attach = true)
        {
            if (Cart != null && Cart.Count > 0)
            {
                ORDER_DETAIL detail;
                foreach (SHOPPING_CART item in Cart)
                {
                    detail = new ORDER_DETAIL();
                    detail.OrderID = Order.ID;
                    detail.CampaignID = item.CampaignID;
                    detail.ProdAttrID = item.ProdAttrID;
                    detail.Quantity = item.Quantity;

                    // first insert order
                    _orderDAO.InsertDetail(detail);

                    item.CAMPAIGN = null;
                    item.CUSTOMER = null;
                    item.PRODUCT_ATTRIBUTE = null;

                    //second remove the related cart item
                    _cartDAO.Delete(item, Attach);
                }
                Context.SaveChanges();
            }
        }

        public ORDERS GetById(int id)
        {
            return _orderDAO.GetById(id);
        }

        public List<ORDERS> Search(ORDERS Order, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _orderDAO.Search(Order, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<ORDERS> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _orderDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public void Update(ORDERS Order)
        {
            Update(Order, true);
        }

        public void Delete(ORDERS Order)
        {
            _orderDAO.Delete(Order);
            //TODO handle related objects in transaction (product quantity)
        }

        public void DeleteById(int Id)
        {
            _orderDAO.DeleteById(Id);
            Context.SaveChanges();
        }

        public List<ORDER_BONUS> GetOrderBonuses(ORDERS Order)
        {
            return _orderDAO.GetOrderBonuses(Order);
        }


        public List<ORDER_BONUS> SearchOrderBonuses(int BonusID, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _orderDAO.SearchOrderBonuses(BonusID, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }


        public List<ORDERS> GetOrdersForReturn(int idCustomer, DateTime validDate)
        {
            return _orderDAO.GetOrdersForReturn(idCustomer, validDate);
        }


        public void InsertAddress(ADDRESSINFO Address)
        {
            _addressInfoDAO.Insert(Address);
            Context.SaveChanges();
        }


        public void UpdateAddress(ADDRESSINFO Address)
        {
            _addressInfoDAO.Update(Address);
            Context.SaveChanges();
        }

        public void DeleteAddress(ADDRESSINFO Address)
        {
            _addressInfoDAO.Delete(Address);
            Context.SaveChanges();
        }

        public ADDRESSINFO GetAddress(int Id)
        {
            return _addressInfoDAO.GetById(Id);
        }


        public List<SHIPPING> GetShippings()
        {
            List<SHIPPING> shippings = Context.SHIPPING.OrderBy(x => x.ID).ToList();
            return shippings;
        }


        public List<ORDER_NOTES> GetNotes(int OrderId)
        {
            return _orderDAO.GetNotes(OrderId);
        }


        public void InsertNote(ORDER_NOTES Note)
        {
            _orderDAO.InsertNote(Note);
            Context.SaveChanges();
        }

        public void DeleteNote(int NoteId)
        {
            _orderDAO.DeleteNote(NoteId);
            Context.SaveChanges();
        }


        public SHIPPING GetShipping(int Id)
        {
            return Context.SHIPPING.Where(s => s.ID == Id).FirstOrDefault();
        }


        public List<ORDER_DETAIL> GetDetails(int OrderID)
        {
            return _orderDAO.GetDetails(OrderID);
        }

        public string GetOrderCampaigns(int OrderId)
        {
            return _orderDAO.GetOrderCampaigns(OrderId);
        }

        public string Update(ORDERS Order, bool Attach = true)
        {
            string retString = String.Empty;
            CUSTOMER customer = _customerDAO.GetById(Order.CustomerID.Value);

            // first buy of this customer & invited by someone
            if ((!customer.FirstBuy.HasValue || !customer.FirstBuy.Value) && Order.PAYMENT.Type == (int)PaymentType.CA && Order.Completed)
            {
                if (customer.InvitedFrom.HasValue)
                {
                    insertCustomerBonus(customer);
                    retString = "Bonus added to inviter " + customer.CUSTOMER2.Email;
                    Util.Mailer.SendBonus(customer, customer.CUSTOMER2.Email, Configuration.BonusValue + " € bonus per ju ne FZone");
                }

                customer.FirstBuy = true;
                _customerDAO.Update(customer, true, false);
            }
            _orderDAO.Update(Order, Attach);

            // payment
            if (Order.PAYMENT != null)
            {
                if (Order.PAYMENT.Type == (int)PaymentType.CA)
                {
                    var entry = Context.ObjectStateManager.GetObjectStateEntry(Order.PAYMENT.CASH_PAYMENT);
                    entry.SetModifiedProperty("Receiver");
                    entry.SetModifiedProperty("Comments");
                    entry.SetModifiedProperty("PaidOn");
                }
                else if (Order.PAYMENT.Type == (int)PaymentType.PP)
                {
                    //TODO
                }
            }
            Context.SaveChanges();
            return retString;
        }


        public void CancelOrderById(int OrderId)
        {
            ORDERS order = _orderDAO.GetById(OrderId);
            PRODUCT_ATTRIBUTE prodAttr;

            // put back to the warehouse each product that was in the order
            // version is not needed as the availability is incremented, so no risk
            foreach (ORDER_DETAIL item in order.ORDER_DETAIL)
            {
                prodAttr = null;
                prodAttr = _productDAO.GetProductAttributeById(item.ProdAttrID);
                prodAttr.Availability += item.Quantity;
                _productDAO.UpdateProductAttribute(prodAttr, false);
            }

            order.Canceled = true;
            order.Completed = false;
            order.Status = 6;
            _orderDAO.Update(order, false);


            // TODO Revert BONUS
            if (order.BonusUsed.HasValue && order.BonusUsed.Value > 0)
            {
                decimal usedValue = 0;
                foreach (ORDER_BONUS b in order.ORDER_BONUS)
                {
                    usedValue += b.Value.Value;
                }
                DateTime validity = order.ORDER_BONUS.Max(b => b.BONUS.Validity).Value;

                BONUS bon = new BONUS()
                {
                    CustomerID = order.CustomerID,
                    DateAssigned = DateTime.Today,
                    Validity = validity,
                    Description = "Per anullim porosie",
                    Value = usedValue,
                    ValueRemainder = usedValue
                };
                _bonusDAO.Insert(bon);
            }
            Context.SaveChanges();
        }
    }
}
