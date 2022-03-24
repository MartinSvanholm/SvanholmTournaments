CREATE PROCEDURE [dbo].[spUser_Get]
	@Id int
AS
BEGIN
	select Id, FirstName, LastName
	from dbo.[Users]
	where Id = @Id and Archived != 1;
END