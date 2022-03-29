CREATE PROCEDURE [dbo].[spUser_Insert]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Username nvarchar(50),
	@CreatedDate datetime2(7),
	@PasswordHash varbinary(MAX),
	@PasswordSalt varbinary(MAX)
AS
BEGIN
	insert into dbo.[Users] (FirstName, LastName, Username, CreatedDate, PasswordHash, PasswordSalt)
	values (@FirstName, @LastName, @Username, @CreatedDate, @PasswordHash, @PasswordSalt);
END