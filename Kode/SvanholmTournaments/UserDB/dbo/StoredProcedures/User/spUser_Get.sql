CREATE PROCEDURE [dbo].[spUser_Get]
	@Username nvarchar(50)
AS
BEGIN
	select Id, FirstName, LastName, Username, CreatedDate, PasswordHash, PasswordSalt
	from dbo.[Users]
	where Username = @Username and Archived != 1
END