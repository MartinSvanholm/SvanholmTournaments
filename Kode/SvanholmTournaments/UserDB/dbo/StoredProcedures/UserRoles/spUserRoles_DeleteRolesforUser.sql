CREATE PROCEDURE [dbo].[spUserRoles_DeleteRolesForUser]
	@UserId int
AS
BEGIN
	delete from dbo.[UserRoles]
	where UserId = @UserId
END