CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Username nvarchar(50),
	@CreatedDate datetime2(7),
	@PasswordHash varbinary(MAX),
	@PasswordSalt varbinary(MAX)
AS
BEGIN
	update dbo.[Users]
	set FirstName = @FirstName, 
		LastName = @LastName, 
		Username = @Username, 
		CreatedDate = @CreatedDate,
		PasswordHash = @PasswordHash,
		PasswordSalt = @PasswordSalt
	where Id = @Id;
END