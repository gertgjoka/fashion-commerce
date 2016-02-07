-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignApprovalStatus]
@ID int
AS
BEGIN

SET NOCOUNT ON;

Select Approved
From CAMPAIGN
where ID = @ID;

END