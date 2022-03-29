CREATE PROCEDURE [dbo].[spUserRoles_Get]
	@UserId int,
	@RoleId int
AS
BEGIN
	SELECT RoleId
	from dbo.[UserRoles]
	where UserId = @UserId
END