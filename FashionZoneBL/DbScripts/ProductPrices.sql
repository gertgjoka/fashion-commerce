select SUM(b.TotBuyPrice) TotBuy, SUM(b.TotSellPrice) TotSell
from (
select a.Name Campaign, a.prod_name Product, a.price SellingPrice, a.SupplierPrice, SUM(a.Quantity) Total, SUM(a.Quantity) * a.price As TotSellPrice, SUM(a.Quantity) * a.SupplierPrice TotBuyPrice
from (
	select CA.name, p.id prod_id, p.Name prod_name, p.OurPrice price, p.SupplierPrice, pa.id att_id, pa.Quantity
	from PRODUCT P, PRODUCT_CATEGORY PC, CATEGORY C, PRODUCT_ATTRIBUTE PA, CAMPAIGN CA
	where P.ID = PC.ProductID
	and PC.CategoryID = C.ID 
	and P.ID = PA.ProductID
	and c.CampaignID = ca.ID
	and CampaignID in (23, 22)
	group by CA.name, p.id, p.Name, p.OurPrice,p.SupplierPrice, pa.id, pa.Quantity
) a
group by a.Name, a.prod_name, a.price, a.SupplierPrice
) b