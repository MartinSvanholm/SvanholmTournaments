﻿CREATE TABLE [dbo].[DatHostAccounts]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] INT NOT NULL FOREIGN KEY ([UserId]) REFERENCES Users([Id]), 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL
)
