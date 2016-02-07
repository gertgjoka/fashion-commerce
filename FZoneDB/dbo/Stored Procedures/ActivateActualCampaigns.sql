-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActivateActualCampaigns]
AS
BEGIN

UPDATE CAMPAIGN
set Active = 1
where CONVERT(VARCHAR(10),StartDate,111)= CONVERT(VARCHAR(10),GETDATE(),111) and Approved = 1
and Active = 0;
SELECT @@ROWCOUNT

END
