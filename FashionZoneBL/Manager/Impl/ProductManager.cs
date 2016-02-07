using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;
using System.Transactions;
using System.Data.Objects;
using FashionZone.BL.Util;

namespace FashionZone.BL.Manager.Impl
{
    public class ProductManager : BaseManager, IProductManager
    {
        private IProductDAO _productDAO;
        private ICategoryDAO _categoryDAO;
        private IAttributeDAO _attributeDAO;


        public ProductManager(IContextContainer container)
            : base(container)
        {
            _productDAO = new ProductDAO(container);
            _categoryDAO = new CategoryDAO(container);
            _attributeDAO = new AttributeDAO(container);
        }

        public List<PROD_IMAGES> GetImages(PRODUCT Product)
        {
            return _productDAO.GetImages(Product);
        }

        public List<PROD_IMAGES> GetImages(int ProductId)
        {
            return _productDAO.GetImages(ProductId);
        }

        public void DeleteImageById(int ImageId)
        {
            _productDAO.DeleteImageById(ImageId);
            Context.SaveChanges();
        }

        public List<PRODUCT_ATTRIBUTE> GetProductAttributes(int ProductID)
        {
            return _productDAO.GetProductAttributes(ProductID);
        }

        public void AddProductAttribute(PRODUCT_ATTRIBUTE Attribute)
        {
            _productDAO.AddProductAttribute(Attribute);
            Context.SaveChanges();
        }

        public void UpdateProductAttribute(PRODUCT_ATTRIBUTE Attribute)
        {
            _productDAO.UpdateProductAttribute(Attribute);
            Context.SaveChanges();

            // EF maintains objects in context, so if during the same request the same object is read, it will not be loaded from db
            // but instead the context version will be used. Here the quantity property isn't updated, so context will have a 0 value for it
            // a refresh is needed to get the last version
            Context.Refresh(RefreshMode.StoreWins, Attribute);
        }

        public void UpdateImage(PROD_IMAGES Image)
        {
            _productDAO.UpdateImage(Image);
            Context.SaveChanges();
        }

        public void InsertImage(PROD_IMAGES Image)
        {
            _productDAO.InsertImage(Image);
            Context.SaveChanges();
        }

        public List<PRODUCT> Search(PRODUCT Product, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _productDAO.Search(Product, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<PRODUCT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _productDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public PRODUCT GetById(int id)
        {
            return _productDAO.GetById(id);
        }

        public void Insert(PRODUCT Entity)
        {
            _productDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void Update(PRODUCT Entity, bool Attach = true)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                // a transaction scope is required to rollback the executeStoreCommand done in productDAO.
                // the command is executed immediately so can't be handled by the default transactionality in EF
                // default isolation level is Serializable, which is too high for our needs so the normal read committed is set
                _productDAO.Update(Entity, Attach);
                Context.SaveChanges();
                scope.Complete();
            }
        }

        public void Update(PRODUCT Entity)
        {
            Update(Entity, true);
        }

        public void Delete(PRODUCT Entity)
        {
            _productDAO.Delete(Entity);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _productDAO.DeleteById(Id);
            Context.SaveChanges();
        }


        public List<FZAttributeAvailability> GetProductAttributeValues(int ProductID)
        {
            return _productDAO.GetProductAttributeValues(ProductID);
        }


        public PRODUCT_ATTRIBUTE GetProductAvailability(int AttributeValueID, out List<int> AvailabilityList, int? AlreadyInCart = null)
        {
            return _productDAO.GetProductAvailability(AttributeValueID, out AvailabilityList, AlreadyInCart);
        }


        public void ChangeApproval(PRODUCT Product)
        {
            _productDAO.ChangeApproval(Product);
        }

        public bool? GetApprovalStatus(int ProductId)
        {
            return _productDAO.GetApprovalStatus(ProductId);
        }


        public List<PRODUCT> GetProductsByCampaign(int CampaignID, int? CategoryID)
        {
            return _productDAO.GetProductsByCampaign(CampaignID, CategoryID);
        }


        public void ImportProducts(int CampaignId, string Path)
        {
            var productList = ProductExcelImporter.ReadProductExcel(Path);
            string encryptedCampId = FashionZone.BL.Util.Encryption.Encrypt(CampaignId.ToString());
            PRODUCT product;

            // preparing categories
            List<CATEGORY> categoriesList = _categoryDAO.GetCategoryListByCampaign(CampaignId);

            int? categoryParentF = null, categoryParentM = null;

            CATEGORY cat, categoryParent = null;
            categoryParent = categoriesList.Where(o => o.NameEng.Contains("Woman")).FirstOrDefault();
            if (categoryParent != null)
            {
                cat = new CATEGORY() { ID = categoryParent.ID };
                categoryParentF = categoryParent.ID;
            }

            categoryParent = categoriesList.Where(o => o.NameEng.Contains("Man")).FirstOrDefault();
            if (categoryParent != null)
            {
                cat = new CATEGORY() { ID = categoryParent.ID };
                categoryParentM = categoryParent.ID;
            }
            var query = (from c in categoriesList
                         select new { c.ID, c.Name, c.NameEng, c.ParentID }).ToList();

            foreach (FZExcelProduct exProd in productList)
            {
                product = new PRODUCT();
                product.Name = exProd.Title;
                product.Code = exProd.Code;
                decimal our, original, suppl = 0;
                if (Decimal.TryParse(exProd.Sell, out our))
                {
                    product.OurPrice = our;
                }
                if (Decimal.TryParse(exProd.Retail, out original))
                {
                    product.OriginalPrice = original;
                }

                if (Decimal.TryParse(exProd.Buy, out suppl))
                {
                    product.SupplierPrice = suppl;
                }

                product.Description = "Produkti <br /><br />" + exProd.Title + "<br /><br />" + exProd.Desc.Replace("\n", "<br />");

                setCategories(CampaignId, query, product, exProd.Category, exProd.Sex, categoryParentF, categoryParentM);

                // inserting the product entity, we are on a "transaction" so everything will
                // be rolled back in case the other statements aren't successful
                Insert(product);
                setAttributes(categoriesList, product, exProd);
                setImages(product, exProd, encryptedCampId);
            }
        }

        private void setCategories(int CampaignId, IEnumerable<dynamic> query, PRODUCT product, string Categories, string FMU,
            int? categoryParentF, int? categoryParentM)
        {
            CATEGORY cat;
            string[] cats = Categories.Split(',');
            int count = 0;

            foreach (string c in cats)
            {
                if (FMU == "F" || FMU == "U")
                {
                    // setting the macro category only once
                    if (count == 0)
                    {
                        if (categoryParentF.HasValue)
                        {
                            cat = Context.CATEGORY.Where(o => o.ID == categoryParentF.Value).First();
                            product.CATEGORY.Add(cat);
                        }
                    }
                    //setting the right category
                    var anonCat = query.Where(o => o.Name.Contains(c.Trim()) && o.ParentID == categoryParentF.Value).First();
                    int id = anonCat.ID;
                    if (anonCat != null)
                    {
                        cat = Context.CATEGORY.Where(o => o.ID == id).First();
                        cat.ParentID = categoryParentF;
                        product.CATEGORY.Add(cat);
                    }
                }
                else if (FMU == "M" || FMU == "U")
                {
                    // setting the macro category only once
                    if (count == 0)
                    {
                        if (categoryParentM.HasValue)
                        {
                            cat = Context.CATEGORY.Where(o => o.ID == categoryParentM.Value).First();
                            product.CATEGORY.Add(cat);
                        }
                    }

                    //setting the right category
                    var anonCat = query.Where(o => o.Name.Contains(c.Trim()) && o.ParentID == categoryParentM).First();
                    int id = anonCat.ID;
                    if (anonCat != null)
                    {
                        cat = Context.CATEGORY.Where(o => o.ID == id).First();
                        cat.ParentID = categoryParentM;
                        product.CATEGORY.Add(cat);
                    }
                }
                count++;
            }
        }

        private void setAttributes(List<CATEGORY> categoriesList, PRODUCT product, FZExcelProduct exProd)
        {
            // TODO when different attributes used, this should be changed to be dynamic!
            int? sizeAttribute = categoriesList.FirstOrDefault().AttributeID;

            List<D_ATTRIBUTE_VALUE> attrValueList = _attributeDAO.GetAttributeValues(sizeAttribute.Value);
            PRODUCT_ATTRIBUTE prodAttr;
            int availability = 0;
            int attrValId = 0;

            // TODO this should be generalized for other attributes

            // XS size
            if (int.TryParse(exProd.SizeXS, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("XS")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }

            // S size
            if (int.TryParse(exProd.SizeS, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("S")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }

            // M size
            if (int.TryParse(exProd.SizeM, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("M")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }

            // L size
            if (int.TryParse(exProd.SizeL, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("L")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }

            // XL size
            if (int.TryParse(exProd.SizeXL, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("XL")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }

            // XXL size
            if (int.TryParse(exProd.SizeXXL, out availability) && availability != 0)
            {
                attrValId = attrValueList.Where(a => a.Value.Equals("XXL")).FirstOrDefault().ID;
                prodAttr = new PRODUCT_ATTRIBUTE()
                {
                    AttributeValueID = attrValId,
                    ProductID = product.ID,
                    Availability = availability,
                    Quantity = availability,
                };
                AddProductAttribute(prodAttr);
            }
        }

        private void setImages(PRODUCT product, FZExcelProduct exProd, string encryptedCampId)
        {

            string startFolder = Configuration.ImagesUploadPath + "\\" + encryptedCampId;

            // Take a snapshot of the file system.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //Create the query
            IEnumerable<System.IO.FileInfo> fileQuery =
                from file in fileList
                where file.Name.ToLower().Contains(exProd.Code.ToLower())
                orderby file.Name
                select file;

            //Execute the query
            PROD_IMAGES img;
            int count = 0;
            foreach (System.IO.FileInfo fi in fileQuery)
            {
                img = new PROD_IMAGES()
                {
                    ProductID = product.ID,
                    Image = "MED" + fi.Name,
                    LargeImage = fi.Name,
                    Thumbnail = "SMALL" + fi.Name,
                    Principal = false
                };

                if (count == 0)
                {
                    img.Principal = true;
                }
                fi.CopyTo(System.IO.Path.Combine(Configuration.ImagesUploadPath + fi.Name));
                // physical saving of all image versions
                GraphicsUtil.SaveProductImages(fi.Name, img.Principal.Value);

                InsertImage(img);
                Console.WriteLine(fi.FullName);
                count++;
            }
        }
    }
}
