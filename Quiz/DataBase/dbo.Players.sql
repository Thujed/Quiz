﻿CREATE TABLE [dbo].[Players]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PlayerName] NCHAR(10) NOT NULL, 
    [PlayerPoints] INT NOT NULL, 
    [PlayerColor] NCHAR(10) NOT NULL
)
