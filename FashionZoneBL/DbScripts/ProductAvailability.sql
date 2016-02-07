USE priv;
select p.id, p.Name, p.OurPrice, pa.Availability, d.Value, pa.AttributeValueID, pa.ID, pa.Quantity
from PRODUCT P, PRODUCT_ATTRIBUTE PA, D_ATTRIBUTE_VALUE D
where p.ID = pa.ProductID and pa.AttributeValueID = D.ID
and pa.AttributeValueID in (1, 28, 12, 23, 24);
