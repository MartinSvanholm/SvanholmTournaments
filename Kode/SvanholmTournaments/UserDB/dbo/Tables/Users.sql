CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [PasswordHash] VARBINARY(MAX) NULL, 
    [PasswordSalt] VARBINARY(MAX) NULL, 
    [Archived] INT NOT NULL DEFAULT 0
)