CREATE PROCEDURE [dbo].[spUserRoles_Delete]
	@UserId int,
	@RoleId int
AS
BEGIN
	delete from dbo.[UserRoles]
	where UserId = @UserId and RoleId = @RoleId
END