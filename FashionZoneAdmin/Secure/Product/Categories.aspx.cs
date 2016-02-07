using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.Text;

namespace FashionZone.Admin.Secure.Product
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //Save Categories
                CATEGORY newCat = new CATEGORY();
                int id;
                int idForEdit;
                int.TryParse(hdnIdNode.Value, out idForEdit);
                if (idForEdit != 0)
                    newCat = new CATEGORY() { ID = idForEdit};
                newCat.Name = txtName.Text;
                newCat.NameEng = txtNameEng.Text;
                newCat.Description = txtDesc.Text;

                Int32.TryParse(dll_campain.SelectedValue, out id);
                newCat.CampaignID = id;

                if (!string.IsNullOrWhiteSpace(ddl_Attributes.SelectedValue))
                {
                    Int32.TryParse(ddl_Attributes.SelectedValue, out id);
                    newCat.AttributeID = id;
                }
                if (!string.IsNullOrWhiteSpace(txtOrder.Text))
                {
                    int.TryParse(txtOrder.Text, out id);
                    newCat.Ordering = id;
                }

                switch (TreeView1.CheckedNodes.Count)
                {
                    case 0:
                        resetFields();
                        pnlInfo.Visible = txtName.Visible = txtNameEng.Visible = txtDesc.Visible = txtOrder.Visible = false;
                        break;
                    case 1:
                        TreeNode selectedNode = new TreeNode();
                        selectedNode = TreeView1.CheckedNodes[0];
                        int.TryParse(selectedNode.Value, out id);
                        newCat.ParentID = id;
                        pnlInfo.Visible = txtName.Visible = txtNameEng.Visible = txtDesc.Visible = txtOrder.Visible = true;
                        break;
                    default:
                        sendMsg("Zgjidh vetem NJE kategori!!");
                        break;
                }
                
                if (idForEdit == 0)
                {
                    ApplicationContext.Current.Categories.Insert(newCat);
                    sendMsg("Insert successful!");
                }
                else
                {
                    ApplicationContext.Current.Categories.Update(newCat);
                    sendMsg("Update successful!");
                }
                resetFields();
                popola();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            lblAddError.Text = txtName.Text = txtNameEng.Text = txtDesc.Text = hdnIdNode.Value = txtOrder.Text = "";
        }

        protected void btnAddNewCamp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddl_brand.SelectedValue)
                && !string.IsNullOrEmpty(dll_campain.SelectedValue))
            {
                switch (TreeView1.CheckedNodes.Count)
                {
                    case 0:
                        resetFields();
                        pnlInfo.Visible = txtName.Visible = txtNameEng.Visible = txtDesc.Visible = txtOrder.Visible = true;
                        break;
                    case 1:
                        TreeNode selectedNode = new TreeNode();
                        selectedNode = TreeView1.CheckedNodes[0];
                        hdnIdNode.Value = selectedNode.Value;
                        resetFields();
                        pnlInfo.Visible = txtName.Visible = txtNameEng.Visible = txtDesc.Visible = txtOrder.Visible = true;
                        break;
                    default:
                        sendMsg("Zgjidh vetem NJE kategori!!");
                        break;
                }
                populateDdlAttr(null);
            }
            else
            {
                lblAddError.Text = "Choose any Brand and Campaign!";
                lblAddError.Visible = true;
            }
        }

        private void populateDdlAttr(int? IdSelecterd)
        {
            var attrs = ApplicationContext.Current.Attributes.GetAll();            
            ddl_Attributes.DataSource = attrs;
            ddl_Attributes.DataTextField = "NAME";
            ddl_Attributes.DataValueField = "ID";
            ddl_Attributes.DataBind();
            if (IdSelecterd.HasValue)
            {
                ddl_Attributes.SelectedValue = IdSelecterd.Value.ToString();
            }
            else
            {
                ddl_Attributes.Items.Insert(0, new ListItem("--Choice", string.Empty));
                ddl_Attributes.SelectedIndex = 0;
            }
        }

        private void sendMsg(string msg)
        {
            lblAddError.Text = msg;
            lblAddError.Visible = true;
           
        }

        private void popola()
        {
            TreeView1.Nodes.Clear();
            TreeNode aNode = new TreeNode(ddl_brand.SelectedItem.Text, ddl_brand.SelectedValue);
            aNode.NavigateUrl = "BrandNew.aspx?ID=" + ddl_brand.SelectedValue;
            TreeView1.Nodes.Add(aNode);

            var myNode = new TreeNode(dll_campain.SelectedItem.Text, dll_campain.SelectedValue);
            myNode.NavigateUrl = "CampaignNew.aspx?ID=" + dll_campain.SelectedValue;
            aNode.ChildNodes.Add(myNode);

            int idcampainSelectedVal;
            Int32.TryParse(dll_campain.SelectedValue, out idcampainSelectedVal);

            List<CATEGORY> catAllList = ApplicationContext.Current.Categories.GetCategoryListByCampaign(idcampainSelectedVal);
            if (catAllList != null)
            {
                var catFatherL = catAllList.Where(a => a.ParentID == null);
                foreach (var singleC in catFatherL)
                {
                    popoloaTreeView(myNode, singleC, catAllList);
                }
                TreeView1.Visible = true;
            }
        }

        private static void popoloaTreeView(TreeNode myNode, CATEGORY singleC, List<CATEGORY> allCats)
        {
            var catChChildren = allCats.Where(c => c.ParentID == singleC.ID).OrderBy(c2 => c2.Ordering).ToList();
            TreeNode nFather = addNodeToTree(myNode, singleC);
            if (catChChildren.Count > 0)
            {
                foreach (var c in catChChildren)
                    popoloaTreeView(nFather, c, allCats);
            }
            catChChildren.Clear();
        }

        private static TreeNode addNodeToTree(TreeNode myNode, CATEGORY singleC)
        {
            var node = new TreeNode(singleC.Name, singleC.ID.ToString());
            node.ShowCheckBox = true;
            node.SelectAction = TreeNodeSelectAction.Select;
            myNode.ChildNodes.Add(node);
            return node;
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            var trv = sender as TreeView;
            txtName.Text = trv.SelectedNode.Text;
            int idCat;
            Int32.TryParse(trv.SelectedNode.Value, out idCat);
            CATEGORY editCat = ApplicationContext.Current.Categories.GetById(idCat);

            txtNameEng.Text = editCat.NameEng;
            txtDesc.Text = editCat.Description;
            txtOrder.Text = editCat.Ordering.ToString();
            hdnIdNode.Value = editCat.ID.ToString();

            populateDdlAttr(editCat.AttributeID);

            pnlInfo.Visible = txtName.Visible = txtNameEng.Visible = txtDesc.Visible = txtOrder.Visible = true;
        }

        protected void dll_campain_SelectedIndexChanged(object sender, EventArgs e)
        {
            popola();
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            TreeNodeCollection colNodes = TreeView1.CheckedNodes;
            if (colNodes.Count > 0)
            {
                foreach (TreeNode node in colNodes)
                {
                    int idCat;
                    Int32.TryParse(node.Value, out idCat);
                    CATEGORY c = ApplicationContext.Current.Categories.GetById(idCat);
                    DeleteCat(c);
                }
                popola();
                sendMsg("The element/s are Deleted successful!");
            }
            else
            {
                sendMsg("Select at least a category to delete.");
            }
        }

        private static void DeleteCat(CATEGORY c)
        {
            ApplicationContext.Current.Categories.Delete(c);
            List<CATEGORY> listChildrenCat = ApplicationContext.Current.Categories.GetChildrenCategoryList(c.ID);
            if (listChildrenCat.Count > 0)
                foreach (var cat in listChildrenCat)
                    DeleteCat(cat);
        }
    }
}