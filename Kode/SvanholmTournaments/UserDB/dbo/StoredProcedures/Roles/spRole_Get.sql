CREATE PROCEDURE [dbo].[spRole_Get]
	@RoleName nvarchar(50)
AS
Begin
	SELECT Id, RoleName
	from dbo.[Roles]
	where RoleName = @RoleName
END