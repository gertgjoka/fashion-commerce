-- returns timed out shopping cart sessions
select ID
from SHOPPING_CART
group by ID
having DATEADD(minute, 28, MAX(DateAdded)) < SYSDATETIME()