CREATE PROCEDURE [dbo].[spUser_Archive]
	@Id int
AS
BEGIN
	update dbo.[Users]
	set Archived = 1
	where Id = @Id;
END