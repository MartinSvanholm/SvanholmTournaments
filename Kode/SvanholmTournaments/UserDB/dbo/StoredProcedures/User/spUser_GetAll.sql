﻿CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN
	select Id, FirstName, LastName, Username, CreatedDate
	from dbo.[Users]
	where Archived != 1;
END