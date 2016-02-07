use priv;
select P.ID, P.Name, sum(p.SupplierPrice * d.quantity) TotSupplierPrice , sum(p.OurPrice * d.Quantity) Amount , sum(d.Quantity)
from ORDER_DETAIL D, ORDERS O, PRODUCT_ATTRIBUTE A, PRODUCT P
where D.ProdAttrID = A.ID
AND A.ProductID = P.ID
AND D.OrderID = O.ID
and O.Status = 5
and o.Completed = 1
group by P.ID, P.Name


use priv;
select distinct d.OrderID, d.ID, p.Name, p.SupplierPrice, p.OurPrice, d.Quantity
from ORDER_DETAIL D, ORDERS O, PRODUCT_ATTRIBUTE A, PRODUCT P
where D.ProdAttrID = A.ID
AND A.ProductID = P.ID
AND D.OrderID = O.ID
and O.Status = 5

-- porosite, nr i produkteve, bonuset dhe leket e arketuara cdo muaj
select A.Year, A.Month, TotalMoney, Bonus, Paid, TotalOrders, TotItems from 
(select DATEPART(YYYY, DateCreated) [Year], DATEPART(mm, DateCreated) [Month], sum(d.Quantity) TotItems
from ORDERS O, ORDER_DETAIL D, PAYMENT P
where o.ID = D.OrderID and O.ID = P.ID
and Completed = 1 and Status = 5 and P.Type = 2
group by DATEPART(YEAR, DateCreated), DATEPART(MONTH, DateCreated)) as A
join 

(select DATEPART(YYYY, DateCreated) [Year], DATEPART(mm, DateCreated) [Month], SUM(TotalAmount) [TotalMoney], SUM(BonusUsed) Bonus,(SUM(TotalAmount) - SUM(BonusUsed)) Paid , count(*) TotalOrders
from ORDERS O, PAYMENT P
where Completed = 1 and Status = 5 and O.ID = P.ID and P.Type = 2
group by DATEPART(YEAR, DateCreated), DATEPART(MONTH, DateCreated)) as B on A.Year = B.Year and A.Month = B.Month
order by A.[Year], A.[Month]

