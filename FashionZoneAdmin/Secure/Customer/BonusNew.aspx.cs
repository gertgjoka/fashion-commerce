using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.Admin.Utils;
using System.Drawing;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class BonusNew : FZBasePage<CUSTOMER>
    {
        protected byte[] Version
        {
            get
            {
                if (ViewState["Version"] != null)
                    return (byte[])ViewState["Version"];
                else
                    return null;
            }
            set
            {
                ViewState["Version"] = value;
            }
        }

        protected bool Used
        {
            get
            {
                if (ViewState["Used"] != null)
                    return (bool)ViewState["Used"];
                else
                    return false;
            }
            set
            {
                ViewState["Used"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string idBonus = Request["IDBonus"];
            string IdCustomer = Request["IDCustomer"];
            int id;
            //bool result = Int32.TryParse(idBrand, out id);

            if (!IsPostBack)
            {
                BONUS bns = null;
                if (!String.IsNullOrWhiteSpace(idBonus) && Int32.TryParse(idBonus, out id) && id != 0)
                {
                    bns = getBonus(id);
                }
                gridCustomer.SortExp = "Name";
            }

            base.Page_Load(sender, e, gridCustomer);

            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtValid.Attributes.Add("readonly", "readonly");
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int numValue;
                int.TryParse(IdBonus.Value, out numValue);
                try
                {
                    if (numValue == 0)
                    {
                        //Save new Bonuses
                        Save();
                    }
                    else
                    {
                        Update(numValue);
                    }
                }
                catch (Exception ex)
                {
                    writeResult(ex.Message, true);
                }
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

        private void Update(int numValue)
        {
            BONUS newBonus = new BONUS();
            
            newBonus.ID = numValue;
            newBonus.DateAssigned = DateTime.Parse(txtDateFrom.Text);

            int.TryParse(txtValid.Text, out numValue);
            newBonus.Validity = DateTime.Parse(txtValid.Text);

            newBonus.Description = txtDesc.Text;

            decimal value;
            if (decimal.TryParse(txtBonusValue.Text, out value))
            {
                newBonus.Value = value;
                if (!Used)
                {
                    newBonus.ValueRemainder = value;
                }
                else
                {
                    newBonus.ValueRemainder = Decimal.Parse(txtBonusRemainder.Text);
                }
            }

            int.TryParse(hdnCustomer.Value, out numValue);
            newBonus.CustomerID = numValue;

            // set old version for concurrency check
            newBonus.Version = Version;
            ApplicationContext.Current.Bonuses.Update(newBonus);
            writeResult("Update successful!", false);

            // update to new version
            Version = newBonus.Version;
            if (!Used)
            {
                txtBonusRemainder.Text = newBonus.ValueRemainder.Value.ToString("N2");
            }
        }

        private void Save()
        {
            int numValue;

            var selectedItems = CheckSelectedCustomer.Items.Cast<ListItem>().ToList();
            foreach (var customer in selectedItems)
            {
                BONUS newBonus = new BONUS();

                DateTime date;

                if (DateTime.TryParse(txtDateFrom.Text, out date))
                {
                    newBonus.DateAssigned = date;
                }
                if (DateTime.TryParse(txtValid.Text, out date))
                {
                    newBonus.Validity = date;
                }

                newBonus.Description = txtDesc.Text;

                decimal d;
                if (Decimal.TryParse(txtBonusValue.Text, out d))
                {
                    newBonus.Value = d;
                    newBonus.ValueRemainder = d;
                }

                int.TryParse(customer.Value, out numValue);
                newBonus.CustomerID = numValue;

                ApplicationContext.Current.Bonuses.Insert(newBonus);
                writeResult("Insert successful!", false);
            }
        }

        private BONUS getBonus(int IdB)
        {
            BONUS bonus = null;
            bonus = ApplicationContext.Current.Bonuses.GetById(IdB);
            if (bonus != null)
            {
                IdBonus.Value = bonus.ID.ToString();
                Version = bonus.Version;
                plhCustomerAssoc.Visible = false;
                plhBonusData.Visible = plhCustomerInfoEdit.Visible = true;
                hdnCustomer.Value = bonus.CustomerID.ToString();
                var customer = ApplicationContext.Current.Customers.GetById(bonus.CustomerID.Value);

                // used in the following orders
                dataBind2("OrderID", 0);

                if (Used)
                {
                    disableWhenUsed();
                }
                hplCustomer.Text = customer.Name + " " + customer.Surname;
                hplCustomer.Target = "_blank";
                hplCustomer.NavigateUrl = string.Format("CustomerNew.aspx?ID={0}", customer.ID);
                hplCustomer.ToolTip = string.Format("Click fo Edit {0}", hplCustomer.Text);
                txtDateFrom.Text = bonus.DateAssigned.Value.ToString("dd/MM/yyyy");
                //caloco dirni rimenenti per il bonus...
                txtValid.Text = bonus.Validity.Value.ToString("dd/MM/yyyy");
                txtBonusValue.Text = bonus.Value.Value.ToString("N2");
                txtBonusRemainder.Text = bonus.ValueRemainder.Value.ToString("N2");
                txtDesc.Text = bonus.Description;
                CalExtDate.StartDate = bonus.DateAssigned.Value;
                //CalExtDate.EndDate = bonus.Validity.Value;

                txtDateFrom.Enabled = true;
            }
            return bonus;
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int bId = 0;
            string[] idName = btn.CommandArgument.Split('|');
            if (Int32.TryParse(idName[0], out bId))
            {
                var existingItems = CheckSelectedCustomer.Items.Cast<ListItem>()
                                   .Where(item => item.Value == bId.ToString()).ToList();
                if (existingItems.Count == 0)
                {
                    CheckSelectedCustomer.Items.Add(new ListItem(idName[1], idName[0]));
                    if (!CheckSelectedCustomer.Visible)
                        setVisible();
                    if (!plhBonusData.Visible)
                        plhBonusData.Visible = true;
                }
            }
        }

        private void setVisible()
        {
            btnDeleteCusomer.Visible = litCustomers.Visible = CheckSelectedCustomer.Visible = true;
        }

        protected void btnDeleteCusomer_lick(object sender, EventArgs e)
        {
            for (int i = CheckSelectedCustomer.Items.Count - 1; i >= 0; i--)
            {
                if (CheckSelectedCustomer.Items[i].Selected)
                    CheckSelectedCustomer.Items.RemoveAt(i);
            }
        }

        #region GridCustomers Code
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new CUSTOMER();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.Name = txtName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }

            if (!String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                SearchObject.Email = txtEmail.Text;
            }
            else
            {
                SearchObject.Email = null;
            }
            base.dataBind(gridCustomer.SortExpression, gridCustomer.PageIndex, gridCustomer);
        }

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridCustomer);
        }
        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridCustomer);
        }
        #endregion

        #region Grid bonuses
        protected void gridOrderUsed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridOrderUsed.CurrentPageIndex = e.NewPageIndex;
            dataBind2(gridOrderUsed.SortExp, e.NewPageIndex);
        }

        protected void gridOrderUsed_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (gridOrderUsed.SortExp == e.SortExpression)
            {
                gridOrderUsed.ChangeSorting();
            }
            else
            {
                gridOrderUsed.SortOrder = FashionZone.BL.Util.SortDirection.Ascending;
            }

            gridOrderUsed.SortExp = e.SortExpression;
            dataBind2(e.SortExpression, gridOrderUsed.CurrentPageIndex);
        }

        private void disableWhenUsed()
        {
            pnlOrdersUsed.Visible = true;
            txtBonusValue.Enabled = false;
            txtDateFrom.Enabled = false;
            CalExtDate.Enabled = false;
        }

        protected void dataBind2(string sortExp, int pageIndex)
        {
            try
            {
                int totalRecords = 0;
                gridOrderUsed.PageSize = Utils.Configuration.PageSize;
                List<ORDER_BONUS> list = ApplicationContext.Current.Orders.SearchOrderBonuses(Int32.Parse(IdBonus.Value), Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, gridOrderUsed.SortOrder);
                if (list.Count > 0)
                {
                    Used = true;
                    gridOrderUsed.DataSource = list;
                    gridOrderUsed.CustomCustomVirtualItemCount = totalRecords;
                    gridOrderUsed.DataBind();
                }
                else
                {
                    pnlOrdersUsed.Visible = false;
                }
            }
            catch (Exception e)
            {
                writeResult(e.Message, true);
            }
        }
        #endregion

    }
}