CREATE PROCEDURE [dbo].[spUserRoles_Insert]
	@UserId int,
	@RoleId int
AS
BEGIN
	insert into dbo.[UserRoles] (UserId, RoleId)
	values (@UserId, @RoleId)
END