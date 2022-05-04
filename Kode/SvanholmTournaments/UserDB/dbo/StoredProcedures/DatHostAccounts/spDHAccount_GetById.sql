CREATE PROCEDURE [dbo].[spDHAccount_GetById]
	@Id int
AS
BEGIN
	SELECT Id, Email, Password
	from dbo.[DatHostAccounts]
	where Id = @Id
END
