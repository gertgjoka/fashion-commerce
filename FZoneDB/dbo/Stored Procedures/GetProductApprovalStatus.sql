-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProductApprovalStatus]
@ID int
AS
BEGIN

SET NOCOUNT ON;

Select Approved
From PRODUCT
where ID = @ID;

END