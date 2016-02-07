using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.Drawing;

namespace FashionZone.Admin.Secure.Product
{
    public partial class Attributes : FZBasePage<D_ATTRIBUTE>
    {
        protected int AttributeID
        {
            get
            {
                int id;
                if (!String.IsNullOrWhiteSpace(attrID.Value) && Int32.TryParse(attrID.Value, out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                attrID.Value = value.ToString();
            }
        }

        protected int ValueID
        {
            get
            {
                int id;
                if (!String.IsNullOrWhiteSpace(valID.Value) && Int32.TryParse(valID.Value, out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                valID.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e, gridAttributes);
            if (!IsPostBack)
            {
                updPanel.Update();
                gridAttributes.SortExp = "Name";
            }
        }


        void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            D_ATTRIBUTE attribute = new D_ATTRIBUTE();
            attribute.Name = txtName.Text;
            attribute.Description = txtDesc.Text;
            string operation;
            try
            {
                if (AttributeID != 0)
                {
                    attribute.ID = AttributeID;
                    ApplicationContext.Current.Attributes.Update(attribute);
                    operation = " updated ";
                }
                else
                {
                    ApplicationContext.Current.Attributes.Insert(attribute);
                    AttributeID = attribute.ID;
                    operation = " inserted ";
                }
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Green;
                lblErrors.Text = "Attribute" + operation + "correctly.";
                // rebind grid to reflect changes
                gridAttributes.PageIndex = gridAttributes.CurrentPageIndex;
                base.dataBind(gridAttributes.SortExpression, gridAttributes.CurrentPageIndex, gridAttributes);
                updPanel.Update();
            }
            catch (Exception ex)
            {
                writeError(ex.Message);
            }
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int aId = 0;
            if (Int32.TryParse(btn.CommandArgument, out aId))
            {
                reset();
                attrID.Value = aId.ToString();
                divDetail.Visible = true;
                D_ATTRIBUTE attribute = loadValues();
                txtName.Text = attribute.Name;
                txtDesc.Text = attribute.Description;
                divValueEdit.Visible = false;
            }
        }

        protected D_ATTRIBUTE loadValues()
        {
            D_ATTRIBUTE attribute = ApplicationContext.Current.Attributes.GetById(AttributeID);
            chkValueList.DataSource = attribute.D_ATTRIBUTE_VALUE.OrderBy(v => v.ShowOrder);
            chkValueList.DataBind();
            return attribute;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new D_ATTRIBUTE();

            if (!String.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                SearchObject.Name = txtNameSearch.Text;
            }
            else
            {
                SearchObject.Name = null;
            }

            base.dataBind(gridAttributes.SortExpression, gridAttributes.PageIndex, gridAttributes);
            reset();
            divDetail.Visible = false;
            updPanel.Update();
        }

        private void reset()
        {
            AttributeID = 0;
            ValueID = 0;
            chkValueList.Items.Clear();
            divValueEdit.Visible = false;
            txtName.Text = String.Empty;
            txtDesc.Text = String.Empty;
            txtValue.Text = String.Empty;
            txtOrder.Text = String.Empty;
            lblErrors.Visible = false;
            lblErrors.Text = String.Empty;
            lblResultValue.Visible = false;
            lblResultValue.Text = String.Empty;
        }

        protected void gridAttributes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridAttributes);
            reset();
            divDetail.Visible = false;
            updPanel.Update();
        }

        protected void gridAttributes_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridAttributes);
            updPanel.Update();
            reset();
            divDetail.Visible = false;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridAttributes);
            updPanel.Update();
        }

        protected void btnAddAttribute_Click(object sender, EventArgs e)
        {
            reset();
            divDetail.Visible = true;
        }

        protected void btnDelValues_Click(object sender, EventArgs e)
        {
            List<String> list = getCheckedValues();
            if (list.Count == 0)
            {
                writeValueError("At least a value should be selected for deletion.");
            }
            else
            {
                foreach (String key in list)
                {
                    ApplicationContext.Current.Attributes.DeleteValueById(Int32.Parse(key));
                    loadValues();
                }
            }
        }

        protected void btnEditValue_Click(object sender, EventArgs e)
        {
            lblResultValue.Visible = false;
            List<String> list = getCheckedValues();
            if (list.Count != 1)
            {
                writeValueError("Only one value must be selected for edit.");
            }
            else
            {
                ValueID = Int32.Parse(list.First());
                D_ATTRIBUTE_VALUE value = ApplicationContext.Current.Attributes.GetValueById(ValueID);
                if (value != null)
                {
                    txtValue.Text = value.Value;
                    txtOrder.Text = value.ShowOrder.ToString();
                    divValueEdit.Visible = true;
                }
            }

        }

        protected void btnNewValue_Click(object sender, EventArgs e)
        {
            if (AttributeID != 0)
            {
                lblResultValue.Visible = false;
                divValueEdit.Visible = true;
                txtValue.Text = String.Empty;
                txtOrder.Text = String.Empty;
                ValueID = 0;
            }
            else
            {
                writeError("Attribute must be saved before values are added.");
            }
        }

        private void writeError(string errorMessage)
        {
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Red;
            lblErrors.Text = "Error occurred: " + errorMessage;
        }

        private void writeValueError(string errorMessage)
        {
            lblResultValue.Visible = true;
            lblResultValue.ForeColor = Color.Red;
            lblResultValue.Text = "Error occurred: " + errorMessage;
        }

        protected List<String> getCheckedValues()
        {
            List<String> list = new List<String>();
            foreach (ListItem item in chkValueList.Items)
            {
                if (item.Selected)
                {
                    list.Add(item.Value);
                }
            }
            return list;
        }

        protected void btnSaveValue_Click(object sender, EventArgs e)
        {
            D_ATTRIBUTE_VALUE value = new D_ATTRIBUTE_VALUE();
            value.AttributeID = AttributeID;
            value.Value = txtValue.Text;
            int order = 0;
            Int32.TryParse(txtOrder.Text, out order);
            value.ShowOrder = order;

            string operation;
            try
            {
                if (ValueID != 0)
                {
                    value.ID = ValueID;
                    ApplicationContext.Current.Attributes.UpdateValue(value);
                    operation = " updated ";
                }
                else
                {
                    ApplicationContext.Current.Attributes.InsertValue(value);
                    ValueID = value.ID;
                    operation = " inserted ";
                }
                lblResultValue.Visible = true;
                lblResultValue.ForeColor = Color.Green;
                lblResultValue.Text = "Value" + operation + "correctly.";
                loadValues();
            }
            catch (Exception ex)
            {
                writeValueError(ex.Message);
            }
        }
    }
}