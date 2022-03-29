CREATE PROCEDURE [dbo].[spRole_Insert]
	@RoleName nvarchar(50)
AS
BEGIN
	insert into dbo.[Roles] (RoleName)
	values (@RoleName)
END