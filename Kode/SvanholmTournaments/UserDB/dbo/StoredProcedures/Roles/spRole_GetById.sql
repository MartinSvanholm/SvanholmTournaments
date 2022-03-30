CREATE PROCEDURE [dbo].[spRole_GetById]
	@Id int
AS
Begin
	SELECT Id, RoleName
	from dbo.[Roles]
	where Id = @Id
END