-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderCampaigns]
@OrderID int
AS
BEGIN

SET NOCOUNT ON;

DECLARE @Names VARCHAR(200);
select @Names = COALESCE(@Names + ', ', '') + b.Name from
ORDER_DETAIL O left join CAMPAIGN C on O.CampaignID = C.ID LEft join BRAND B on C.BrandID = B.ID
where OrderID = @OrderID
group by b.Name;
select @Names as Campaigns;
END