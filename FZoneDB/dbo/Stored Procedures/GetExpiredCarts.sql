-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GetExpiredCarts]
AS
BEGIN

-- returns timed out shopping cart sessions
select ID
from SHOPPING_CART
group by ID
having DATEADD(minute, 30, MAX(DateAdded)) < SYSDATETIME()

END
