-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeActivatePastCampaigns]
AS
BEGIN

UPDATE CAMPAIGN
set Active = 0
where EndDate <= GETDATE()
and Active = 1;

SELECT @@ROWCOUNT

END
