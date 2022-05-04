CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN
	SELECT * 
	FROM Users
	LEFT JOIN 
		DatHostAccounts
	on  Users.Id = DatHostAccounts.UserId
	Order by Users.Id
END