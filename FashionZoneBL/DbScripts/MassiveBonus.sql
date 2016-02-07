
insert into BONUS (CustomerID, DateAssigned, [Description], Validity, Value, ValueRemainder)
select ID, getdate(), 'Dhurate nga FZone', DATEADD(DAY, 30, GETDATE()), 5.00, 5.00
from CUSTOMER
where ID not in
(
select customerId
from bonus)


--bonus to use in specific orders
use FZTest;

declare @bon int;
declare @oid int;
declare @cid int;
declare @bval decimal(18, 2);
set @cid = 7;
set @oid = 153;
set @bval = 5.00;

insert into BONUS (CustomerID, DateAssigned, Validity, Value, ValueRemainder, [Description])
values (@cid, '2013-01-24', '2013-01-25', @bval, 0, 'fushata paguaj 1 merr 2');
set @bon = @@IDENTITY;

insert into ORDER_BONUS(OrderID, BonusID, Value)
values(@oid, @bon, @bval);

update ORDERS
set AmountPaid = AmountPaid-@bval,
BonusUsed = @bval
where ID = @oid;

update CASH_PAYMENT
set Amount = Amount-@bval
where ID = @oid;


select o.id, o.CustomerID, o.AmountPaid, O.TotalAmount, O.BonusUsed from ORDERS O
where O.ID = @oid