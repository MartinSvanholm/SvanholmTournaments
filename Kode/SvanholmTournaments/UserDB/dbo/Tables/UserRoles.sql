CREATE TABLE [dbo].[UserRoles]
(
	[UserId] INT NOT NULL FOREIGN KEY ([UserId]) REFERENCES Users(Id), 
    [RoleId] INT NOT NULL FOREIGN KEY ([RoleId]) REFERENCES Roles(Id)
)