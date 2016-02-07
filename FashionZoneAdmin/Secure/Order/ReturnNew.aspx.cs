using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using Configuration = FashionZone.Admin.Utils.Configuration;

namespace FashionZone.Admin.Secure.Order
{
    public partial class ReturnNew : FZBasePage<CUSTOMER>
    {
        private static readonly int VERIFICATIONNUMBERLENGTH = int.Parse(ConfigurationManager.AppSettings["VerificationNumberLength"]);


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
            set { ViewState["CustomerID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idOrder = Request["IDORDER"];
                string idReturn = Request["IDRETURN"];
                string from = Request["FROM"];
                if (!string.IsNullOrEmpty(idOrder) && !string.IsNullOrEmpty(idReturn))
                {
                    int num;
                    if (int.TryParse(idOrder, out num))
                    {
                        divDatail.Visible = FieldRetDet.Visible = true; //= FildOrderDat.Visible = FieldsetOrderDett.Visible 
                        btnSelectCustomer.Visible = plhOrdertet.Visible = false;
                        var singleOrder = ApplicationContext.Current.Orders.GetById(num);
                        if (singleOrder.CustomerID.HasValue)
                        {
                            setCustomerFullName(singleOrder.CustomerID.Value);
                            //popolateReturn(singleOrder);

                            if (int.TryParse(idReturn, out num))
                            {
                                var ret = ApplicationContext.Current.Returns.GetById(num);
                                plhdateRequest.Visible = true;
                                txtRequestDate.Text = ret.RequestDate.HasValue
                                                          ? ret.RequestDate.Value.ToString(string.Format("d"))
                                                          : "";
                                txtReceivedDate.Text = ret.ReceivedDate.HasValue
                                                           ? ret.ReceivedDate.Value.ToString(string.Format("d"))
                                                           : "";
                                txtComment.Text = ret.Comments;
                                litReturnID.Text = ret.ID.ToString();
                                litOrderID.Text = ret.OrderID.HasValue ? string.Format(@"<a href=""/Secure/Order/OrderNew.aspx?ID={0}"" >{0}</a>",
                                    String.Format(FashionZone.BL.Configuration.OrderNrFormatting, ret.OrderID.Value)) : "";
                                litVerificationNumber.Text = ret.VerificationNumber;
                                repRetDet.DataSource = ret.RETURN_DETAILS;
                                repRetDet.DataBind();

                                if (!string.IsNullOrEmpty(from) && from.Equals("RETURN"))
                                    writeResult("Return Save with Verification Number: " + ret.VerificationNumber, false);
                            }
                        }
                    }
                }
                populateStatus();
            }
            else
                GetOrderDetails();
        }

        private void GetOrderDetails()
        {
            //if (IsPostBack)
            //{
            if (Request.Form["__EVENTTARGET"] == "lnkSelectOrder")
            {
                string idOrder = Request.Form["__EVENTARGUMENT"];
                int orderId;
                if (Int32.TryParse(idOrder, out orderId))
                {
                    FieldsetOrderDett.Visible = true;
                    var singleOrder = ApplicationContext.Current.Orders.GetById(orderId);
                    popolateReturn(singleOrder);
                }
            }
            //}
        }

        private void popolateReturn(ORDERS singleOrder)
        {
            litNumber.Text = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, singleOrder.ID);
            chkCompleted.Checked = singleOrder.Completed;
            chkVerified.Checked = singleOrder.Verified;
            txtAmount.Text = singleOrder.TotalAmount.ToString("N2");
            if (singleOrder.Status.HasValue)
                ddlStatus.SelectedValue = singleOrder.Status.Value.ToString();

            repOrderDett.DataSource = singleOrder.ORDER_DETAIL;
            repOrderDett.DataBind();
        }

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            txtSearchName.Text = String.Empty;
            SearchObject = null;
            dataBind("ID", 0, gridCustomer);
            divDatail.Visible = FildOrderDat.Visible = pnlCustomer.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new CUSTOMER();

            if (!String.IsNullOrWhiteSpace(txtSearchName.Text))
                SearchObject.Name = txtSearchName.Text;
            else
                SearchObject.Name = null;

            base.dataBind(gridCustomer.SortExpression, gridCustomer.PageIndex, gridCustomer);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            getReturnWithDettails(btn.CommandArgument);
        }

        private void getReturnWithDettails(string orderId)
        {
            int cId;
            if (Int32.TryParse(orderId, out cId))
            {
                setCustomerFullName(cId);
                GetOrdersList(cId);
                pnlCustomer.Visible = false;
            }
        }

        private void setCustomerFullName(int cId)
        {
            CUSTOMER customer = ApplicationContext.Current.Customers.GetById(cId);
            CustomerID = cId;
            if (customer != null)
                txtCustomer.Text = customer.Name + " " + customer.Surname;
        }

        private void GetOrdersList(int cId)
        {
            pnlCustomer.Visible = true;
            var res = ApplicationContext.Current.Orders.GetOrdersForReturn(cId,
                                                                           DateTime.Now.AddDays(
                                                                               Configuration.MaxReturnDay));
            RepeatOrder.DataSource = res;
            RepeatOrder.DataBind();
        }

        protected void populateStatus()
        {
            ddlStatus.DataSource = ApplicationContext.Current.Orders.GetStatuses();
            ddlStatus.DataBind();
        }

        #region Grid Customer

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            grid_Sorting(sender, e, gridCustomer);
            //updModal.Update();
        }

        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_PageIndexChanging(sender, e, gridCustomer);
            //updModal.Update();
        }

        #endregion Grid Customer

        protected void repOrderDett_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ddlQuantity = item.FindControl("ddlQuantity") as DropDownList;
                var lblQuant = item.FindControl("lblQuantity") as Label;
                int maxQuantity;
                if (lblQuant != null && int.TryParse(lblQuant.Text, out maxQuantity))
                {
                    lblQuant.Visible = false;
                    for (int i = 0; i <= maxQuantity; i++)
                    {
                        ddlQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }
                var ddlMotivation = item.FindControl("ddlMotivation") as DropDownList;
                if (ddlMotivation != null)
                {
                    ddlMotivation.DataSource = ApplicationContext.Current.Returns.GetAllReturnMotivation();
                    ddlMotivation.DataTextField = "Name";
                    ddlMotivation.DataValueField = "ID";
                    ddlMotivation.DataBind();
                }
            }
        }

        protected void repRetDet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //var ddlMotivation = item.FindControl("ddlMotivRet") as DropDownList;
                var lblMotId = item.FindControl("lblMotRet") as Label;
                int motId;
                if (lblMotId != null && !string.IsNullOrEmpty(lblMotId.Text))
                {
                    if(int.TryParse(lblMotId.Text, out motId))
                    {
                        var mot = ApplicationContext.Current.Returns.GetAllReturnMotivationById(motId);
                        lblMotId.Text = mot.Name;
                    }
                }
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            bool isSelect = !string.IsNullOrEmpty(Request["IDORDER"]) && !string.IsNullOrEmpty(Request["IDRETURN"]);
            var ret = new RETURN();
            var retDetList = new List<RETURN_DETAILS>();
            foreach (RepeaterItem ri in repOrderDett.Items)
            {
                var cb = ri.FindControl("chkbSlect") as CheckBox;
                if (cb != null && cb.Checked)
                {
                    isSelect = true;
                    int num;
                    decimal price;
                    var idOrderDett = ri.FindControl("hdnOrderDettID") as HiddenField;
                    var unitPrice = ri.FindControl("lblUnitPrice") as Label;
                    var ddlQ = ri.FindControl("ddlQuantity") as DropDownList;
                    var ddlM = ri.FindControl("ddlMotivation") as DropDownList;

                    var retDett = new RETURN_DETAILS();
                    if (ddlM != null && int.TryParse(ddlM.SelectedValue, out num))
                        retDett.MotivationID = num;
                    if (idOrderDett != null && int.TryParse(idOrderDett.Value, out num))
                        retDett.OrderDetailID = num;
                    if (unitPrice != null && Decimal.TryParse(unitPrice.Text, out price))
                        retDett.Price = price;
                    if (ddlQ != null && int.TryParse(ddlQ.SelectedValue, out num))
                        retDett.Quantity = num;
                    retDett.ReturnID = ret.ID;

                    retDetList.Add(retDett);
                }
            }

            if (isSelect)
            {
                int idOrder;
                DateTime date;
                if (int.TryParse(litNumber.Text, out idOrder))
                    ret.OrderID = idOrder;
                ret.RETURN_DETAILS = retDetList;
                ret.Comments = txtComment.Text;
                if (DateTime.TryParse(txtReceivedDate.Text, out date))
                    ret.ReceivedDate = date;
                //ret.Received = "";

                try
                {
                    int idRet;
                    string idReturn = Request["IDRETURN"];
                    if (int.TryParse(idReturn, out idRet))
                    {
                        ret.ID = idRet;
                        if (int.TryParse(Request["IDORDER"], out idRet))
                            ret.OrderID = idRet;
                        ret.VerificationNumber = litVerificationNumber.Text;
                        if (DateTime.TryParse(txtRequestDate.Text, out date))
                            ret.RequestDate = date;
                        ApplicationContext.Current.Returns.Update(ret);
                        writeResult("Return Update Successful!", false);
                    }
                    else
                    {
                        ret.RequestDate = DateTime.Now;
                        ret.VerificationNumber = generateVerificationNumber(VERIFICATIONNUMBERLENGTH);
                        ApplicationContext.Current.Returns.Insert(ret);
                        writeResult("Return Save with Verification Number: " + ret.VerificationNumber, false);

                        Response.Redirect(string.Format("/Secure/Order/ReturnNew.aspx?IDRETURN={0}&IDORDER={1}&FROM={2}", ret.ID, ret.OrderID, "RETURN"));
                    }
                }
                catch (Exception ex)
                {
                    writeResult("Error:" + ex.Message, true);
                }
            }
            else
            {
                writeResult("No selected items!", true);
            }
        }

        private void writeResult(string message, bool isError)
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
        }

        private string generateVerificationNumber(int length)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var builder = new StringBuilder();
            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            string vN = builder.ToString();
            //check if exist
            if (ApplicationContext.Current.Returns.ExistVerificationNumber(vN))
                generateVerificationNumber(VERIFICATIONNUMBERLENGTH);

            return vN;
        }

    }
}