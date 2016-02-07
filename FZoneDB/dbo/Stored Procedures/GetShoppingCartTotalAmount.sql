-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetShoppingCartTotalAmount]
	@ID char(36)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Select SUM(S.Quantity * P.OurPrice)
	from SHOPPING_CART S Join PRODUCT_ATTRIBUTE A On S.ProdAttrID = A.ID Join PRODUCT P on A.ProductID = P.ID
	where S.ID = @ID
	group by S.ID;
END
