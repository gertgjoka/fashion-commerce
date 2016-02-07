using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.BL.Util;
using FashionZone.Admin.Utils;
using System.Drawing;
using FashionZone.Admin.CustomControl;

namespace FashionZone.Admin.Secure.Order
{
    // extending base page with type customer for the realization of customer popup
    public partial class OrderNew : FZBasePage<CUSTOMER>
    {
        #region Properites in viewstate

        protected int OrderID
        {
            get
            {
                int id;
                if (ViewState["OrderID"] != null && Int32.TryParse(ViewState["OrderID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["OrderID"] = value;
            }
        }

        protected int CustomerID
        {
            get
            {
                int id;
                if (ViewState["CustomerID"] != null && Int32.TryParse(ViewState["CustomerID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["CustomerID"] = value;
            }
        }

        protected int ShippingAddressID
        {
            get
            {
                int id;
                if (ViewState["ShippingAddressID"] != null && Int32.TryParse(ViewState["ShippingAddressID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["ShippingAddressID"] = value;
            }
        }

        protected int BillingAddressID
        {
            get
            {
                int id;
                if (ViewState["BillingAddressID"] != null && Int32.TryParse(ViewState["BillingAddressID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["BillingAddressID"] = value;
            }
        }

        protected Decimal TotalAmount
        {
            get
            {
                decimal tot;
                if (ViewState["TotalAmount"] != null && Decimal.TryParse(ViewState["TotalAmount"].ToString(), out tot))
                    return tot;
                else
                    return 0;
            }
            set
            {
                ViewState["TotalAmount"] = value;
            }
        }

        protected Decimal BonusUsed
        {
            get
            {
                decimal tot;
                if (ViewState["BonusUsed"] != null && Decimal.TryParse(ViewState["BonusUsed"].ToString(), out tot))
                    return tot;
                else
                    return 0;
            }
            set
            {
                ViewState["BonusUsed"] = value;
            }
        }

        #endregion

        #region Page utils

        protected void Page_Load(object sender, EventArgs e)
        {
            string ordId = Request["ID"];
            int id;
            if (!IsPostBack)
            {
                populateStatus();
                populateShippings();
                if (!String.IsNullOrWhiteSpace(ordId) && Int32.TryParse(ordId, out id) && id != 0)
                {
                    if (!loadOrder(id))
                    {
                        writeResult("Order was not found.", true);
                    }
                    buttExportPdf.Visible = true;
                }
                else
                {
                    // Shopping cart initialization
                    Guid g = Guid.NewGuid();
                    UniqueIdGenerator unique = UniqueIdGenerator.GetInstance();
                    string cartId = unique.GetBase32UniqueId(g.ToByteArray(), 20).ToLower();
                    cart.CartID = cartId;
                    lblDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    shAddress.Visible = true;
                }
            }
            txtPaid.Attributes.Add("readonly", "readonly");
            txtCustomer.Attributes.Add("readonly", "readonly");
            txtAmount.Attributes.Add("readonly", "readonly");
            txtShippingCost.Attributes.Add("readonly", "readonly");

            txtPaymentAmount.Attributes.Add("readonly", "readonly");
            //txtPaidOn.Attributes.Add("readonly", "readonly");

            //if (OrderID != 0)
            //{
            //    txtReceiver.Attributes.Add("readonly", "readonly");
            //    txtCashComments.Attributes.Add("readonly", "readonly");
            //}
        }

        public void writeResult(string message, bool isError)
        {
            lblErrors.Visible = true;
            if (isError)
            {
                lblErrors.ForeColor = Color.Red;
                message = "Error occurred: " + message;
            }
            else
            {
                lblErrors.ForeColor = Color.Green;
            }
            lblErrors.Text = message;
            updPnl.Update();
        }

        #endregion

        #region Order handling

        protected bool loadOrder(int id)
        {
            ORDERS order = null;
            try
            {
                order = ApplicationContext.Current.Orders.GetById(id);
                if (order == null)
                {
                    return false;
                }

                OrderID = order.ID;
                TotalAmount = order.TotalAmount;

                divOnEdit.Visible = true;

                if (order.CustomerID.HasValue)
                {
                    CustomerID = order.CustomerID.Value;
                    pnlCustomerBonus.Update();
                }

                litNumber.Text = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, order.ID)
                    + " (" + order.VerificationNumber + ")";
                lblDate.Text = order.DateCreated.ToString("dd/MM/yyyy");
                txtCustomer.Text = order.CustomerName;
                chkCanceled.Checked = order.Canceled;
                if (order.Canceled)
                {
                    chkCanceled.Enabled = false;
                    chkCompleted.Enabled = false;
                    disablePayment();
                }
                else
                {
                    btnCancel.Visible = true;
                }
                chkCompleted.Checked = order.Completed;
                txtAmount.Text = order.TotalAmount.ToString("N2");
                txtPaid.Text = order.AmountPaid.ToString("N2");
                txtComments.Text = order.Comments;

                if (order.Status.HasValue)
                {
                    ddlStatus.SelectedValue = order.Status.Value.ToString();
                }

                if (order.ShippingID.HasValue)
                {
                    ddlShipping.SelectedValue = order.ShippingID.Value.ToString();
                    if (order.SHIPPING != null)
                    {
                        txtShippingCost.Text = order.SHIPPING.ShippingCost.ToString("N2");
                    }
                }

                if (order.BonusUsed.HasValue)
                {
                    lnkBonus.Text = order.BonusUsed.Value.ToString("N2");
                    BonusUsed = order.BonusUsed.Value;
                }
                else
                {
                    lnkBonus.Text = "0";
                }

                if (order.DateShipped.HasValue)
                {
                    txtDateShipped.Text = order.DateShipped.Value.Date.ToString("dd/MM/yyyy");
                    txtTimeShipped.Text = order.DateShipped.Value.ToString("HH:mm");
                }

                if (order.DateDelivered.HasValue)
                {
                    txtDeliveryDate.Text = order.DateDelivered.Value.Date.ToString("dd/MM/yyyy");
                    txtDeliveryTime.Text = order.DateDelivered.Value.ToString("HH:mm");
                }

                txtTracking.Text = order.TrackingNumber;
                txtShippingDetails.Text = order.ShippingDetails;

                // loading the used bonus list
                loadUsedBonus();

                loadDetails(order.ORDER_DETAIL, true);
                loadNotes();
                disableEdit(order.ORDER_DETAIL.ToList(), true);
                btnReset.Visible = false;

                if (order.ShippingAddress.HasValue)
                {
                    ShippingAddressID = order.ShippingAddress.Value;
                    loadShippingAddress(order.ShippingAddress.Value, false);
                    shAddress.Visible = true;
                }
                else
                {
                    shAddress.Visible = false;
                }

                if (order.BillingAddress.HasValue)
                {
                    BillingAddressID = order.BillingAddress.Value;
                    loadBillingAddress(order.BillingAddress.Value, false);
                    bAddress.Visible = true;
                }
                else
                {
                    bAddress.Visible = false;
                }
                updAddresses.Update();

                if (order.PAYMENT != null)
                {
                    if (order.PAYMENT.Type == (int)PaymentType.CA)
                    {
                        txtReceiver.Text = order.PAYMENT.CASH_PAYMENT.Receiver;
                        txtCashComments.Text = order.PAYMENT.CASH_PAYMENT.Comments;
                        if (order.PAYMENT.CASH_PAYMENT.PaidOn.HasValue)
                        {
                            txtPaidOn.Text = order.PAYMENT.CASH_PAYMENT.PaidOn.Value.ToString("dd/MM/yyyy");
                        }
                        txtPaymentAmount.Text = order.PAYMENT.CASH_PAYMENT.Amount.Value.ToString("N2");

                        radioBtnCash.Checked = true;
                        radioBtnPaypal.Enabled = false;
                        radioBtnEasypay.Enabled = false;
                        accPanePaypal.Visible = false;
                        accPaneEasyPay.Visible = false;
                    }
                    else if (order.PAYMENT.Type == (int)PaymentType.PP)
                    {
                        litPPAmount.Text = order.PAYMENT.PAYPAL_PAYMENT.Amount.Value.ToString("N2");
                        litPPCurrency.Text = order.PAYMENT.PAYPAL_PAYMENT.Currency;
                        litPPDate.Text = order.PAYMENT.PAYPAL_PAYMENT.PaidOn.Value.ToString("dd/MM/yyyy hh:mm");
                        litPPFee.Text = order.PAYMENT.PAYPAL_PAYMENT.Fee.Value.ToString("N2");
                        litPPName.Text = order.PAYMENT.PAYPAL_PAYMENT.PayerName;
                        litPPPayerEmail.Text = order.PAYMENT.PAYPAL_PAYMENT.PayerEmail;
                        litPPPayerStatus.Text = order.PAYMENT.PAYPAL_PAYMENT.PayerStatus;
                        litPPStatus.Text = order.PAYMENT.PAYPAL_PAYMENT.TransactionStatus;
                        litPPTransaction.Text = order.PAYMENT.PAYPAL_PAYMENT.TransactionKey;
                        txtPPResponse.Text = order.PAYMENT.PAYPAL_PAYMENT.Response;
                        radioBtnPaypal.Checked = true;
                        radioBtnCash.Enabled = false;
                        radioBtnEasypay.Enabled = false;
                        accPanePaypal.Visible = true;
                        accPaneCash.Visible = false;
                        accPaneEasyPay.Visible = false;
                    }
                    else if (order.PAYMENT.Type == (int)PaymentType.EP)
                    {
                        lblEPAmount.Text = order.PAYMENT.EASYPAY_PAYMENT.Amount.ToString("N2");
                        lblEPDate.Text = order.PAYMENT.EASYPAY_PAYMENT.Date.ToString("dd/MM/yyyy hh:mm");
                        lblEPFee.Text = order.PAYMENT.EASYPAY_PAYMENT.Fee.ToString("N2");
                        lblEPResponse.Text = order.PAYMENT.EASYPAY_PAYMENT.OriginalResponse;
                        lblEPStatus.Text = order.PAYMENT.EASYPAY_PAYMENT.TransactionStatus;
                        lblEPTransaction.Text = order.PAYMENT.EASYPAY_PAYMENT.TransactionID;
                        lblEPRate.Text = order.PAYMENT.EASYPAY_PAYMENT.Rate.ToString("N2");
                        radioBtnEasypay.Checked = true;
                        radioBtnCash.Enabled = false;
                        radioBtnPaypal.Enabled = false;
                        accPaneCash.Visible = false;
                        accPanePaypal.Visible = false;
                        accPaneEasyPay.Visible = true;
                    }

                    updPayment.Update();
                    // Paypal or other payments to be handled

                }
                btnAddNote.Visible = true;
            }
            catch (Exception ex)
            {
                writeResult(ex.Message, true);
                Util.Log(ex, ex.Message, ex.StackTrace, "OrderNew.loadOrder");
                return false;
            }
            return true;
        }

        protected void populateStatus()
        {
            ddlStatus.DataSource = ApplicationContext.Current.Orders.GetStatuses();
            ddlStatus.DataBind();
        }

        protected void populateShippings()
        {
            List<SHIPPING> shippings = ApplicationContext.Current.Orders.GetShippings();
            ddlShipping.DataSource = shippings;
            if (shippings != null && shippings.Count > 0)
            {
                txtShippingCost.Text = shippings.ElementAt(0).ShippingCost.ToString("N2");
            }

            ddlShipping.DataBind();
        }

        protected void loadNotes()
        {
            List<ORDER_NOTES> notes = ApplicationContext.Current.Orders.GetNotes(OrderID);

            var query = from n in notes
                        select new { n.ID, n.CreatedOn, n.DisplayToCustomer, n.Text, n.USER.Login };

            gridNotes.DataSource = query;
            gridNotes.DataBind();
            updNotes.Update();
        }

        protected void btnAddNote_Click(object sender, EventArgs e)
        {
            addOrderNote(txtNote.Text);
        }

        private void addOrderNote(string Note)
        {
            // creating a new note for insertion on db
            ORDER_NOTES note = new ORDER_NOTES();
            note.CreatedOn = DateTime.Now;
            note.DisplayToCustomer = chkDisplayToCustomer.Checked;
            note.OrderID = OrderID;
            note.Text = Note;

            USER user = null;
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                user = ApplicationContext.Current.Users.GetByUserName(User.Identity.Name);
            }
            else
            {
                return;
            }
            note.UserID = user.ID;
            ApplicationContext.Current.Orders.InsertNote(note);

            // claring fields
            chkDisplayToCustomer.Checked = false;
            txtNote.Text = String.Empty;

            // reloading notes
            loadNotes();
        }

        protected void ddlShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShipping.SelectedValue != String.Empty)
            {
                int shippingType;
                if (Int32.TryParse(ddlShipping.SelectedValue, out shippingType))
                {
                    decimal previousCost;
                    Decimal.TryParse(txtShippingCost.Text, out previousCost);
                    SHIPPING ship = ApplicationContext.Current.Orders.GetShipping(shippingType);
                    txtShippingCost.Text = ship.ShippingCost.ToString("N2");
                    TotalAmount -= previousCost;
                    TotalAmount += ship.ShippingCost;
                    updateAmount(false);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                saveOrder();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            // clearing viewstate
            OrderID = 0;
            CustomerID = 0;
            BonusUsed = 0;
            TotalAmount = 0;
            BillingAddressID = 0;
            ShippingAddressID = 0;

            // clearing textboxes, literals, etc
            txtAmount.Text = String.Empty;
            txtComments.Text = String.Empty;
            txtCustomer.Text = String.Empty;
            txtPaid.Text = String.Empty;
            txtSearchName.Text = String.Empty;
            litNumber.Text = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, OrderID);
            lblDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            litTotalBonus.Text = BonusUsed.ToString("N2");
            litTotalOrder.Text = TotalAmount.ToString("N2");
            txtShippingDetails.Text = String.Empty;
            txtDateShipped.Text = String.Empty;
            txtTimeShipped.Text = String.Empty;
            txtDeliveryDate.Text = String.Empty;
            txtDeliveryTime.Text = String.Empty;
            txtTracking.Text = String.Empty;

            chkCanceled.Checked = false;
            chkCompleted.Checked = false;

            // clearing errors
            lblBonusError.Visible = false;
            lblProductError.Text = String.Empty;
            lblErrors.Text = String.Empty;
            lblErrors.Visible = false;
            lblProductError.Visible = false;

            // clearing payment info
            txtReceiver.Text = String.Empty;
            txtPaidOn.Text = String.Empty;
            txtCashComments.Text = String.Empty;

            // clearing bonuses
            clearAndHideBonus();

            // clearing the carts and the session
            cart.Destroy();

            ddlStatus.SelectedIndex = 0;

            // reset addresses
            address1.ResetControl(true);
            address1.Visible = false;

            ddlShippingAddress.Items.Clear();
            ddlShippingAddress.Enabled = false;

            ddlBillingAddress.Items.Clear();
            ddlBillingAddress.Enabled = false;

            address2.ResetControl(true);
            address2.Visible = false;
            bAddress.Visible = false;

            lnkAddAddress.Visible = false;
            updAddresses.Update();

            //clearing products
            gridProduct.DataSource = null;
            gridProduct.DataBind();
            updModalProducts.Update();

            //clearing customers
            gridCustomer.DataSource = null;
            gridCustomer.DataBind();
            updModal.Update();

            //clearing bonuses
            gridBonus.DataSource = null;
            gridBonus.DataBind();
            updModalBonus.Update();
        }

        protected void saveOrder()
        {
            ORDERS order = new ORDERS();
            decimal d = 0;

            if (Decimal.TryParse(txtAmount.Text, out d))
            {
                order.TotalAmount = d;
            }

            if (OrderID == 0 && (cart.CartSession == null || cart.CartSession.Count == 0))
            {
                writeResult(" Products must be added before proceeding with the order.", true);
                return;
            }

            //if (ddlShippingAddress.SelectedValue == "0")
            //{
            //    writeResult("Shipping address must be specified!", true);
            //    return;
            //}

            //if (ddlBillingAddress.SelectedValue == "0")
            //{
            //    //writeResult("Billing address must be specified!", true);
            //    //return;
            //}

            if (!String.IsNullOrEmpty(txtDateShipped.Text))
            {
                order.DateShipped = DateTime.Parse(txtDateShipped.Text + " " + txtTimeShipped.Text);
            }

            if (!String.IsNullOrEmpty(txtDeliveryDate.Text))
            {
                order.DateDelivered = DateTime.Parse(txtDeliveryDate.Text + " " + txtDeliveryTime.Text);
            }

            order.TrackingNumber = txtTracking.Text;
            order.ShippingDetails = txtShippingDetails.Text;

            d = 0;
            if (!String.IsNullOrEmpty(lnkBonus.Text) && Decimal.TryParse(lnkBonus.Text, out d) && lnkBonus.Text == d.ToString("N2"))
            {
                order.BonusUsed = d;
                if (order.TotalAmount == TotalAmount && d == BonusUsed && txtPaid.Text == (order.TotalAmount - order.BonusUsed.Value).ToString("N2"))
                {
                    order.AmountPaid = order.TotalAmount - order.BonusUsed.Value;
                }
                else
                {
                    writeResult("Consistency check failed. Please check bonus, total amount and paid fields!", true);
                    return;
                }
            }
            else
            {
                order.AmountPaid = order.TotalAmount;
            }

            order.Canceled = chkCanceled.Checked;
            order.Completed = chkCompleted.Checked;
            order.CustomerID = CustomerID;
            order.Comments = txtComments.Text;
            int i;
            if (Int32.TryParse(ddlStatus.SelectedValue, out i) && i != 0)
            {
                order.Status = i;
            }

            i = 0;

            if (Int32.TryParse(ddlShipping.SelectedValue, out i) && i != 0)
            {
                order.ShippingID = i;
            }

            try
            {
                string retString = String.Empty;
                string operation;
                if (OrderID == 0)
                {
                    order.Verified = true;
                    if (Int32.TryParse(selectedPayment.Value, out i) && i == 1)
                    {
                        radioBtnCash.Checked = true;
                        updPayment.Update();
                        if (!String.IsNullOrWhiteSpace(txtReceiver.Text) && !String.IsNullOrWhiteSpace(txtCashComments.Text))
                        {
                            order.PAYMENT = new PAYMENT() { Type = i };
                            order.PAYMENT.CASH_PAYMENT = new CASH_PAYMENT() { Amount = TotalAmount - BonusUsed, PaidOn = DateTime.Now, Receiver = txtReceiver.Text, Comments = txtCashComments.Text };
                            order.Completed = true;

                            chkCompleted.Checked = true;
                            chkCanceled.Checked = false;
                        }
                        else
                        {
                            Page.Validate("CashValidation");
                            radioBtnCash.Checked = true;
                            updPayment.Update();
                            return;
                        }
                    }
                    else
                    {
                        writeResult("Selected payment method is not supported.", true);
                        return;
                    }

                    bool details = cart.CartSession != null && cart.CartSession.Count > 0;

                    // setting used bonuses in the order object
                    setBonus(order);
                    order.DateCreated = DateTime.Now;

                    // addresses
                    if (address1.AddrID != 0)
                    {
                        ADDRESSINFO shipping = new ADDRESSINFO(address1.GetAddress());
                        order.ADDRESSINFO = shipping;

                    }

                    if (address2.AddrID != 0)
                    {
                        ADDRESSINFO billing = new ADDRESSINFO(address2.GetAddress());
                        order.ADDRESSINFO1 = billing;
                    }

                    //inserting and not saving if there are any details, as the saving will be done in the details insertion
                    retString = ApplicationContext.Current.Orders.Insert(order, false, !details);

                    // inserting the products of this order
                    if (details)
                    {
                        List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(cart.CartSession.First().ID);
                        ApplicationContext.Current.Orders.InsertDetailsFromCart(order, carts, false);
                    }
                    litNumber.Text = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, order.ID)
                        + " (" + order.VerificationNumber + ")";
                    operation = "inserted";

                    OrderID = order.ID;
                    if (order.ADDRESSINFO != null)
                    {
                        ShippingAddressID = order.ADDRESSINFO.ID;
                    }
                    else
                    {
                        shAddress.Visible = false;
                    }

                    if (order.ADDRESSINFO1 != null)
                    {
                        BillingAddressID = order.ADDRESSINFO1.ID;
                    }
                    else
                    {
                        bAddress.Visible = false;
                    }

                    // disabling any further edit
                    disableEdit(ApplicationContext.Current.Orders.GetDetails(OrderID), false);
                    divOnEdit.Visible = true;
                    btnAddNote.Visible = true;
                    updNotes.Update();
                }
                else
                {
                    order.ID = OrderID;
                    // navigation properties involved, the association should be set after the ID is set
                    //setBonus(order);

                    if (Int32.TryParse(selectedPayment.Value, out i) && i == 1)
                    {
                        if (!String.IsNullOrWhiteSpace(txtReceiver.Text) && !String.IsNullOrWhiteSpace(txtCashComments.Text))
                        {
                            order.PAYMENT = new PAYMENT() { Type = i };
                            order.PAYMENT.CASH_PAYMENT = new CASH_PAYMENT() { Amount = TotalAmount - BonusUsed, PaidOn = DateTime.Now, Receiver = txtReceiver.Text, Comments = txtCashComments.Text };
                        }
                        else
                        {
                            //Page.Validate("CashValidation");
                            radioBtnCash.Checked = true;
                            updPayment.Update();
                            //return;
                        }
                    }

                    order.Verified = true;
                    address1.DisableOriginal = true;
                    address1.AddrID = ShippingAddressID;
                    if (address1.IsValid())
                    {
                        address1.Save(true);
                    }
                    else
                    {
                        writeResult("Shipping address incomplete", true);
                    }
                    if (ShippingAddressID != 0)
                    {
                        order.ShippingAddress = ShippingAddressID;
                    }
                    address2.DisableOriginal = true;
                    address2.AddrID = BillingAddressID;
                    if (address2.IsValid())
                    {
                        address2.Save(true);
                    }
                    else
                    {
                        writeResult("Billing address incomplete", true);
                    }
                    if (BillingAddressID != 0)
                    {
                        order.BillingAddress = BillingAddressID;
                    }
                    retString = ApplicationContext.Current.Orders.Update(order);
                    operation = "updated";
                    updAddresses.Update();
                }
                if (!String.IsNullOrWhiteSpace(txtNote.Text))
                {
                    btnAddNote_Click(null, null);
                }

                buttExportPdf.Visible = true;
                btnReset.Enabled = false;
                writeResult("Order " + operation + " successfully. " + retString, false);
            }
            catch (Exception e)
            {
                writeResult(e.Message, true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationContext.Current.Orders.CancelOrderById(OrderID);
                chkCanceled.Checked = true;
                chkCanceled.Enabled = false;
                chkCompleted.Enabled = false;
                chkCompleted.Checked = false;
                ddlStatus.SelectedValue = "6";
                pnlCustomerBonus.Update();

                btnCancel.Visible = false;
                updPnl.Update();

                addOrderNote("Anulluar");

                disablePayment();
            }
            catch (Exception ex)
            {
                writeResult(ex.Message, true);
                Util.Log(ex, ex.Message, ex.StackTrace, "OrderNew.btnCancel");
            }
        }

        private void disablePayment()
        {
            txtReceiver.Enabled = false;
            txtPaidOn.Enabled = false;
            txtCashComments.Enabled = false;
            radioBtnCash.Checked = true;
            updPayment.Update();
        }

        private void disableEdit(List<ORDER_DETAIL> details, bool FirstLoad)
        {
            btnSelectCustomer.Visible = false;
            gridBonus.Visible = false;
            btnRemoveBonus.Visible = false;
            btnConfirmSaving.Enabled = false;
            ddlShipping.Enabled = false;
            pnlCustomerBonus.Update();
            lnkAddAddress.Visible = false;
            updPnl.Update();

            cart.ReadOnly = true;
            if (!FirstLoad)
            {
                loadDetails(details, false);
            }
            lnkAddProduct.Visible = false;
            updProducts.Update();

            ddlShippingAddress.Visible = false;
            ddlBillingAddress.Visible = false;
            address1.DisableOriginal = true;
            //address1.ReloadControl(false);
            address2.DisableOriginal = true;
            //address2.ReloadControl(false);
            updAddresses.Update();

            //if (OrderID != 0)
            //{
            //    txtReceiver.Attributes.Add("readonly", "readonly");
            //    txtCashComments.Attributes.Add("readonly", "readonly");
            //}
            updPayment.Update();
        }

        private SortedList<int, byte[]> getBonusVersionList()
        {
            if (gridBonus.Rows.Count > 0)
            {
                SortedList<int, byte[]> list = new SortedList<int, byte[]>();
                int id;
                foreach (GridViewRow row in gridBonus.Rows)
                {
                    if (Int32.TryParse(row.Cells[5].Text, out id))
                    {
                        list.Add(id, (byte[])gridBonus.DataKeys[row.RowIndex].Value);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        private void setBonus(ORDERS order)
        {
            ORDER_BONUS ordBonus;
            int bonusID;
            decimal bonusValue;
            BONUS bonus;
            foreach (ListItem item in chkListBonuses.Items)
            {
                if (!String.IsNullOrWhiteSpace(item.Value) && Int32.TryParse(item.Value, out bonusID)
                    && Decimal.TryParse(item.Text.Trim(), out bonusValue))
                {
                    foreach (GridViewRow row in gridBonus.Rows)
                    {
                        if (row.Cells[5].Text == bonusID.ToString())
                        {
                            bonus = new BONUS() { ID = bonusID, Version = (byte[])gridBonus.DataKeys[row.RowIndex].Value };

                            ordBonus = new ORDER_BONUS() { OrderID = this.OrderID, BonusID = bonusID, Value = bonusValue, ORDERS = order, BONUS = bonus };
                            order.ORDER_BONUS.Add(ordBonus);
                        }
                    }
                }
            }
        }

        private void loadUsedBonus()
        {
            ORDERS order = new ORDERS() { ID = OrderID };
            List<ORDER_BONUS> bonuses = ApplicationContext.Current.Orders.GetOrderBonuses(order);
            if (bonuses.Count > 0)
            {
                foreach (ORDER_BONUS ordBon in bonuses)
                {
                    chkListBonuses.Items.Add(new ListItem(" " + ordBon.Value.Value.ToString("N2"), ordBon.BonusID.ToString()));
                }
                litTotalBonus.Text = BonusUsed.ToString("N2");
            }
        }

        #endregion

        #region Customer handling

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            if (OrderID != 0)
            {
                return;
            }
            txtSearchName.Text = String.Empty;
            SearchObject = null;
            dataBind("ID", 0, gridCustomer);
            updModal.Update();
            popupCustomer.Show();
        }

        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridCustomer);
            updModal.Update();
        }

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridCustomer);
            updModal.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new CUSTOMER();

            if (!String.IsNullOrWhiteSpace(txtSearchName.Text))
            {
                SearchObject.Name = txtSearchName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }
            base.dataBind(gridCustomer.SortExpression, gridCustomer.PageIndex, gridCustomer);
            updModal.Update();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            popupCustomer.Hide();
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int cId = 0;
            if (Int32.TryParse(btn.CommandArgument, out cId))
            {
                CUSTOMER customer = ApplicationContext.Current.Customers.GetById(cId);
                CustomerID = cId;
                txtCustomer.Text = customer.Name + " " + customer.Surname;
                pnlCustomerBonus.Update();
                lblErrors.Visible = false;
                updPnl.Update();

                // clearing and updating the fields of the addresses
                clearAndHideAddresses();
            }
            clearAndHideBonus();
            popupCustomer.Hide();
        }

        #endregion

        #region Bonus handling

        private void clearAndHideBonus()
        {
            chkListBonuses.Items.Clear();
            divSelectedBonuses.Visible = false;
            BonusUsed = 0;
            litTotalBonus.Text = BonusUsed.ToString("N2");
            lnkBonus.Text = BonusUsed.ToString("N2");
            txtPaid.Text = txtAmount.Text;
        }

        protected void btnCloseBonus_Click(object sender, EventArgs e)
        {
            popupBonus.Hide();
            if (OrderID == 0)
            {
                lnkBonus.Text = BonusUsed.ToString("N2");
                updateAmount(false);
            }
        }

        protected void btnRemoveBonus_lick(object sender, EventArgs e)
        {
            int changes = 0;
            for (int i = chkListBonuses.Items.Count - 1; i >= 0; i--)
            {
                if (chkListBonuses.Items[i].Selected)
                {
                    decimal totalBonus, newBonus = 0;
                    if (Decimal.TryParse(litTotalBonus.Text, out totalBonus) && Decimal.TryParse(chkListBonuses.Items[i].Text.Trim(), out newBonus))
                    {
                        litTotalBonus.Text = (totalBonus - newBonus).ToString("N2");
                        BonusUsed -= newBonus;
                    }

                    foreach (GridViewRow row in gridBonus.Rows)
                    {
                        if (row.Cells[5].Text == chkListBonuses.Items[i].Value)
                        {
                            Decimal.TryParse(row.Cells[2].Text, out totalBonus);
                            row.Cells[2].Text = (totalBonus + newBonus).ToString();
                        }
                    }
                    chkListBonuses.Items.RemoveAt(i);
                    changes++;
                }
            }

            if (chkListBonuses.Items.Count == 0)
            {
                divSelectedBonuses.Visible = false;
            }

            if (changes > 0)
            {
                updModalBonus.Update();
            }
        }

        protected void gridBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBonus.CurrentPageIndex = e.NewPageIndex;
            dataBindBonuses(gridBonus.SortExp, e.NewPageIndex);
            updModal.Update();
        }

        protected void gridBonus_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (gridBonus.SortExp == e.SortExpression)
            {
                gridBonus.ChangeSorting();
            }
            else
            {
                gridBonus.SortOrder = FashionZone.BL.Util.SortDirection.Ascending;
            }

            gridBonus.SortExp = e.SortExpression;
            dataBindBonuses(e.SortExpression, gridBonus.CurrentPageIndex);
            updModal.Update();
        }

        protected void dataBindBonuses(string sortExp, int pageIndex)
        {
            try
            {
                int totalRecords = 0;
                gridBonus.PageSize = Utils.Configuration.PageSize;
                BONUS bonus = new BONUS() { CustomerID = this.CustomerID, Validity = DateTime.Today };
                List<BONUS> list = ApplicationContext.Current.Bonuses.Search(bonus, Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, gridBonus.SortOrder);

                if (chkListBonuses.Items.Count > 0)
                {
                    int bId;
                    decimal bonusValue;
                    foreach (ListItem item in chkListBonuses.Items)
                    {
                        Int32.TryParse(item.Value, out bId);
                        Decimal.TryParse(item.Text.Trim(), out bonusValue);
                        BONUS bon = list.Where(x => x.ID == bId).FirstOrDefault();

                        if (bon != null)
                        {
                            bon.ValueRemainder -= bonusValue;
                        }
                    }
                }
                gridBonus.DataSource = list;
                gridBonus.CustomCustomVirtualItemCount = totalRecords;
                gridBonus.DataBind();
            }
            catch (Exception e)
            {
                writeResult(e.Message, true);
            }
        }

        protected void lnkBonus_Click(object sender, EventArgs e)
        {
            if (OrderID == 0)
            {
                dataBindBonuses("ID", 0);
                litTotalOrder.Text = TotalAmount.ToString();
            }
            else
            {
                gridBonus.Visible = false;
                btnRemoveBonus.Visible = false;
            }

            if (chkListBonuses.Items.Count > 0)
            {
                divSelectedBonuses.Visible = true;
            }
            updModalBonus.Update();
            popupBonus.Show();
        }

        protected void gridBonus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btn = e.CommandSource as LinkButton;
            int bId = 0;
            string[] idValue = e.CommandArgument.ToString().Split('|');
            if (Int32.TryParse(idValue[0], out bId))
            {
                // checking if already exists
                var existingItems = chkListBonuses.Items.Cast<ListItem>()
                                   .Where(item => item.Value == bId.ToString()).ToList();
                if (existingItems.Count == 0)
                {

                    decimal newBonus, remainderBonus = 0;
                    if (BonusUsed < TotalAmount && Decimal.TryParse(idValue[1], out newBonus) && newBonus > 0)
                    {
                        if (BonusUsed + newBonus > TotalAmount)
                        {
                            remainderBonus = newBonus - (TotalAmount - BonusUsed);
                            newBonus = TotalAmount - BonusUsed;
                        }
                        // adding bonus to the used list
                        chkListBonuses.Items.Add(new ListItem(" " + newBonus.ToString("N2"), idValue[0]));

                        // updating total bonus used
                        litTotalBonus.Text = (BonusUsed + newBonus).ToString("N2");
                        BonusUsed += newBonus;

                        // update bonus remainder on grid view (this is for visual effect only, no db persisting)
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        row.Cells[2].Text = remainderBonus.ToString("N2");

                        // putting to visible the div that contains used bonus list
                        if (!divSelectedBonuses.Visible)
                        {
                            divSelectedBonuses.Visible = true;
                        }
                        lblBonusError.Visible = false;
                    }
                    else
                    {
                        lblBonusError.Visible = true;
                        lblBonusError.Text = "This bonus can't be used!";
                    }
                    updModalBonus.Update();
                }
            }
        }

        #endregion

        #region Details handling

        private void loadDetails(ICollection<ORDER_DETAIL> details, bool FirstLoad)
        {
            if (details != null && details.Count > 0)
            {
                //
                List<SHOPPING_CART> cartList = new List<SHOPPING_CART>();

                SHOPPING_CART c;
                foreach (ORDER_DETAIL detail in details)
                {
                    c = new SHOPPING_CART(detail);
                    cartList.Add(c);
                }


                cart.ReadOnly = true;
                cart.DataBind(cartList);
                cart.Visible = true;

                updProducts.Update();
            }
        }

        /// <summary>
        /// Opens the product grid popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddProduct_Click(object sender, EventArgs e)
        {
            if (OrderID == 0)
            {
                if (CustomerID != 0)
                {
                    dataBindProducts(null, 0);
                    updModalProducts.Update();
                    popupProduct.Show();
                    lblErrors.Text = string.Empty;
                    updPnl.Update();
                }
                else
                {
                    writeResult("You must choose a customer to continue", true);
                }
            }
        }

        protected void btnCloseProduct_Click(object sender, EventArgs e)
        {
            cart.Versions = null;
            popupProduct.Hide();
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridProduct.CurrentPageIndex = e.NewPageIndex;
            dataBindProducts(gridProduct.SortExp, e.NewPageIndex);
            updModalProducts.Update();
        }

        protected void gridProduct_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (gridProduct.SortExp == e.SortExpression)
            {
                gridProduct.ChangeSorting();
            }
            else
            {
                gridProduct.SortOrder = FashionZone.BL.Util.SortDirection.Ascending;
            }

            gridProduct.SortExp = e.SortExpression;
            dataBindProducts(e.SortExpression, gridProduct.CurrentPageIndex);
            updModalProducts.Update();
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idcampainSelectedVal = 0;
            if (Int32.TryParse(ddlNewCampaign.SelectedValue, out idcampainSelectedVal))
            {
                dataBindProducts(null, 0);
                updModalProducts.Update();
            }
        }

        protected void dataBindProducts(string sortExp, int pageIndex)
        {
            try
            {
                int totalRecords = 0;
                gridProduct.PageSize = 5;
                int selectedCampaign;
                Int32.TryParse(ddlNewCampaign.SelectedValue, out selectedCampaign);
                PRODUCT product = new PRODUCT() { CampaignID = selectedCampaign };
                List<PRODUCT> list = ApplicationContext.Current.Products.Search(product, 5, pageIndex, out totalRecords, sortExp, gridProduct.SortOrder);
                gridProduct.DataSource = list;
                gridProduct.CustomCustomVirtualItemCount = totalRecords;
                gridProduct.DataBind();
                // storing the product-attribute versions
                storeVersionInSession(list);

            }
            catch (Exception e)
            {
                writeResult(e.Message, true);
            }
        }

        private void storeVersionInSession(List<PRODUCT> list)
        {
            cart.Versions = null;
            if (list.Count > 0)
            {
                SortedList<int, SortedList<int, byte[]>> versionList = new SortedList<int, SortedList<int, byte[]>>();
                SortedList<int, byte[]> singleAttrVersionList;
                // looping for the product-attributes versions
                foreach (PRODUCT prod in list)
                {
                    if (prod.PRODUCT_ATTRIBUTE != null && prod.PRODUCT_ATTRIBUTE.Count > 0)
                    {
                        singleAttrVersionList = new SortedList<int, byte[]>();

                        foreach (PRODUCT_ATTRIBUTE prodAttr in prod.PRODUCT_ATTRIBUTE)
                        {
                            singleAttrVersionList.Add(prodAttr.ID, prodAttr.Version);
                        }

                        versionList.Add(prod.ID, singleAttrVersionList);
                    }
                }
                cart.Versions = versionList;
            }
        }

        protected void gridProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btn = e.CommandSource as LinkButton;
            int pId = 0;
            if (btn != null)
            {
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                if (Int32.TryParse(gridProduct.DataKeys[row.RowIndex].Value.ToString(), out pId))
                {
                    TableCell cellSize = row.Cells[6];
                    DropDownList ddlSize = (DropDownList)cellSize.FindControl("ddlSizeGrid");

                    int selectedAttr;
                    if (ddlSize.SelectedValue == "-1")
                    {
                        writeProductResult("You must select a size!", true);
                        return;
                    }
                    Int32.TryParse(ddlSize.SelectedValue, out selectedAttr);

                    TableCell cellQty = row.Cells[7];
                    DropDownList ddlQty = (DropDownList)cellQty.FindControl("ddlQtyGrid");
                    int selectedQty;

                    if (ddlQty.SelectedValue == "0" || ddlQty.SelectedValue == String.Empty)
                    {
                        writeProductResult("You must select a quantity!", true);
                        return;
                    }
                    Int32.TryParse(ddlQty.SelectedValue, out selectedQty);
                    int selectedCampaign;
                    Int32.TryParse(ddlNewCampaign.SelectedValue, out selectedCampaign);

                    try
                    {
                        cart.AddProductToCart(CustomerID, selectedCampaign, pId, selectedAttr, selectedQty, ddlNewBrand.SelectedItem.Text);
                        updProducts.Update();
                        //cart.Versions = null;
                        cart.Visible = true;
                        popupProduct.Hide();

                        updateAmount(true);
                    }
                    catch (Exception ex)
                    {
                        writeProductResult(ex.Message, true);
                        Util.Log(ex, ex.Message, ex.StackTrace, "OrderNew.gridProduct_RowCommand");
                        dataBindProducts(null, 0);
                        updModalProducts.Update();
                    }
                }
            }
        }

        /// <summary>
        /// Populates the Quantity list according to the availability on db of the selected product-attribute
        /// A version information is maintained in Version DataKey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSizeGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            int prodAttrId;
            if (ddl.SelectedValue != "0")
            {
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                if (Int32.TryParse(ddl.SelectedValue, out prodAttrId))
                {
                    List<int> qtyList;
                    PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(prodAttrId, out qtyList);

                    TableCell cell = row.Cells[7];
                    DropDownList ddlQty = (DropDownList)cell.FindControl("ddlQtyGrid");
                    ddlQty.Enabled = true;
                    ddlQty.DataSource = qtyList;
                    ddlQty.DataBind();
                    updModalProducts.Update();
                }
            }
        }

        protected void cart_NeedRefresh(object sender, EventArgs e)
        {
            updProducts.Update();
            updateAmount(true);
        }

        private void updateAmount(bool fromCart)
        {
            if (fromCart)
            {
                decimal shippingCost;
                Decimal.TryParse(txtShippingCost.Text, out shippingCost);
                TotalAmount = cart.TotalAmount() + shippingCost;
            }
            txtAmount.Text = TotalAmount.ToString("N2");
            txtPaid.Text = (TotalAmount - BonusUsed).ToString("N2");
            txtPaymentAmount.Text = (TotalAmount - BonusUsed).ToString("N2");

            int i;

            if (Int32.TryParse(selectedPayment.Value, out i))
            {
                if (i == 1)
                {
                    radioBtnCash.Checked = true;
                }
                else if (i == 2)
                {
                    radioBtnPaypal.Checked = true;
                }
            }
            updPayment.Update();
            pnlCustomerBonus.Update();
        }

        /// <summary>
        /// Populates the related Size drop down lists for each product in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int id;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow row = (GridViewRow)e.Row;
                FZDropDownList ddlSize = (FZDropDownList)e.Row.FindControl("ddlSizeGrid");
                // using more than one key for product
                object val = gridProduct.DataKeys[e.Row.RowIndex].Values["ID"];
                if (val != null && Int32.TryParse(val.ToString(), out id))
                {
                    List<FZAttributeAvailability> list = ApplicationContext.Current.Products.GetProductAttributeValues(id);
                    list.Insert(0, new FZAttributeAvailability() { Id = 0, Value = "Size", Availability = 0 });
                    ddlSize.DataSource = list;
                    ddlSize.DataBind();
                    anotateSizeItems(list, ddlSize);
                }
            }
        }

        /// <summary>
        /// Anotates items of the size list with availability information and disables the items with no availability
        /// </summary>
        /// <param name="items"></param>
        /// <param name="ddlSize"></param>
        private void anotateSizeItems(List<FZAttributeAvailability> items, DropDownList ddlSize)
        {
            var lessThanItems = items.Where(x => x.Availability < FashionZone.BL.Configuration.MaxOrderQuantityPerProduct);
            foreach (var item in lessThanItems)
            {
                ListItem lItem = ddlSize.Items.FindByValue(item.Id.ToString());
                if (item.Availability > 0)
                {
                    lItem.Text = lItem.Text + " - Only " + item.Availability;
                }
                else
                {
                    lItem.Attributes.Add("style", "color:gray;");
                    lItem.Attributes.Add("disabled", "true");
                    if (item.Value != "Size")
                    {
                        lItem.Text = lItem.Text + " - Exhausted";
                    }
                    lItem.Value = "-1";
                }
            }
        }

        private void writeProductResult(string message, bool isError)
        {
            lblProductError.Visible = true;
            if (isError)
            {
                lblProductError.ForeColor = Color.Red;
                message = "Error occurred: " + message;
            }
            else
            {
                lblProductError.ForeColor = Color.Green;
            }
            lblProductError.Text = message;
            updModalProducts.Update();
        }

        #endregion

        #region Address handling

        protected void ddlShippingAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShippingAddress.SelectedValue != "0")
            {
                loadShippingAddress(Int32.Parse(ddlShippingAddress.SelectedValue), true);
                bAddress.Visible = true;
                address1.ResetErrors();
            }
            else
            {
                address1.ResetControl(true);
                address1.Visible = false;
            }
            updAddresses.Update();
        }

        protected void loadShippingAddress(int AddressID, bool reload)
        {
            address1.AddrID = AddressID;
            address1.CustomerID = CustomerID;
            address1.ChangeSelectedPanel(0);

            if (reload)
            {
                address1.ReloadControl(false);
            }
            address1.Visible = true;
        }

        protected void ddlBillingAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBillingAddress.SelectedValue != "0")
            {
                loadBillingAddress(Int32.Parse(ddlBillingAddress.SelectedValue), true);
            }
            else
            {
                address2.ResetControl(true);
                address2.Visible = false;
            }
            updAddresses.Update();
        }

        protected void loadBillingAddress(int AddressID, bool reload)
        {
            address2.AddrID = AddressID;
            address2.CustomerID = CustomerID;
            address2.ChangeSelectedPanel(0);
            if (reload)
            {
                address2.ReloadControl(false);
            }
            address2.Visible = true;
            bAddress.Visible = true;
        }

        protected void loadCustomerAddresses(int? selectedShipping, int? selectedBilling)
        {
            if (CustomerID != 0)
            {
                CUSTOMER customer = ApplicationContext.Current.Customers.GetById(CustomerID);

                if (customer.ADDRESS != null && customer.ADDRESS.Count > 0)
                {
                    List<ADDRESS> addresses = customer.ADDRESS.ToList();
                    addresses.Insert(0, new ADDRESS() { ID = 0 });

                    ddlShippingAddress.DataSource = addresses;
                    ddlShippingAddress.DataBind();
                    ddlShippingAddress.Enabled = true;
                    if (selectedShipping.HasValue && selectedShipping.Value != 0)
                    {
                        ddlShippingAddress.SelectedValue = selectedShipping.Value.ToString();
                    }
                    else if (address1.AddrID != 0)
                    {
                        ddlShippingAddress.SelectedValue = address1.AddrID.ToString();
                    }

                    ddlBillingAddress.DataSource = addresses;
                    ddlBillingAddress.DataBind();
                    ddlBillingAddress.Enabled = true;
                    if (selectedBilling.HasValue && selectedBilling.Value != 0)
                    {
                        ddlBillingAddress.SelectedValue = selectedBilling.Value.ToString();
                    }
                    else if (address2.AddrID != 0)
                    {
                        ddlBillingAddress.SelectedValue = address2.AddrID.ToString();
                    }

                    if (customer.ADDRESS.Count < 5 && OrderID == 0)
                    {
                        lnkAddAddress.Visible = true;
                    }

                }
                else
                {
                    if (OrderID == 0)
                    {
                        lnkAddAddress.Visible = true;
                    }
                }
            }
            updAddresses.Update();
        }

        protected void lnkAddAddress_Click(object sender, EventArgs e)
        {
            if (OrderID == 0)
            {
                address1.ResetControl(true);
                ddlShippingAddress.Enabled = false;
                ddlBillingAddress.Enabled = false;
                address1.CustomerID = CustomerID;
                address1.Visible = true;
                address1.ChangeSelectedPanel(1);
                updAddresses.Update();
            }
        }

        protected void address1_Saving(object sender, EventArgs e)
        {
            if (OrderID == 0)
            {
                if (address1.AddrID != 0 && address1.AddrID == address2.AddrID)
                {
                    address2.ReloadControl(false);
                }

                if (!bAddress.Visible)
                {
                    bAddress.Visible = true;
                }
                loadCustomerAddresses(address1.AddrID, null);
            }
        }

        protected void address2_Saving(object sender, EventArgs e)
        {
            if (OrderID == 0)
            {
                if (address1.AddrID != 0 && address1.AddrID == address2.AddrID)
                {
                    address1.ReloadControl(false);
                }

                loadCustomerAddresses(address2.AddrID, null);
            }
        }

        private void clearAndHideAddresses()
        {
            address1.ResetControl(true);
            address2.ResetControl(true);

            address1.ReloadControl(false);
            address2.ReloadControl(false);

            address1.Visible = false;
            bAddress.Visible = false;
            address2.Visible = false;

            ddlShippingAddress.Items.Clear();

            ddlBillingAddress.Items.Clear();

            loadCustomerAddresses(null, null);
        }

        #endregion

        #region PDF export

        protected void buttExportPdf_Click(object sender, EventArgs e)
        {
            int id;
            if (Int32.TryParse(Request["ID"], out id))
            {
                Response.Redirect("Download.aspx?IDORDER=" + id);
                //string nurl = "~/Download.aspx?IDORDER=" + id;
                //Response.Write("<script>");
                //Response.Write("window.open('" + nurl + " '_blank')");
                //Response.Write("<" + "/script>");
            }


        }

        private void OutputDocPdf(ORDERS order)
        {
            using (var memoryF = FashionZone.BL.PDF.PdfGenerator.DocPDF(order))
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(memoryF.GetBuffer(), 0, memoryF.GetBuffer().Length);
                Response.OutputStream.Close();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        #endregion
    }
}