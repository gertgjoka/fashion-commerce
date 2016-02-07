-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignProducts]
@CampaignID int,
@CategoryID int = null
AS
BEGIN

SET NOCOUNT ON;

select distinct P.* 
from PRODUCT P left join PRODUCT_CATEGORY PC on P.ID = PC.ProductID left join CATEGORY C ON PC.CategoryID = C.ID
where CampaignID = @CampaignID AND PC.CategoryID = COALESCE(NULLIF(@CategoryID, ''), PC.CategoryID)

END