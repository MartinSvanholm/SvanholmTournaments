CREATE PROCEDURE [dbo].[spDHAccount_Insert]
	@Email nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	insert into dbo.[DatHostAccounts] (Email, Password)
	values (@Email, @Password)
END