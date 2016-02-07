use priv;
select p.ID, p.Name, p.OurPrice, pa.Availability, d.Value, S.Quantity OrderedQuantity
, pa.AttributeValueID
from SHOPPING_CART s left join PRODUCT_ATTRIBUTE pa on s.ProdAttrID = pa.ID 
left join PRODUCT p on pa.ProductID = p.ID 
left join D_ATTRIBUTE_VALUE d on d.ID = pa.AttributeValueID
