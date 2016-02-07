USE priv;
	select a.Name Campaign, prod_id, a.Code, a.prod_name Product, a.price SellingPrice, a.SupplierPrice,  SUM(a.Quantity) * a.price As TotSellPrice, SUM(a.Quantity) * a.SupplierPrice TotBuyPrice, sum(a.Availability) Remaining
from (
	select CA.name, p.id prod_id, p.Code, p.Name prod_name, p.OurPrice price, p.SupplierPrice, pa.id att_id, pa.Quantity, pa.Availability
	from PRODUCT P, PRODUCT_CATEGORY PC, CATEGORY C, PRODUCT_ATTRIBUTE PA, CAMPAIGN CA
	where P.ID = PC.ProductID
	and PC.CategoryID = C.ID 
	and P.ID = PA.ProductID
	and c.CampaignID = ca.ID
	and CampaignID in (20, 21, 22, 23, 24, 25, 26, 27)
	group by CA.name, p.id, p.Code, p.Name, p.OurPrice,p.SupplierPrice, pa.id, pa.Quantity, pa.Availability
) a
group by a.prod_id, a.Code, a.Name, a.prod_name, a.price, a.SupplierPrice
having SUM(Availability) > 0