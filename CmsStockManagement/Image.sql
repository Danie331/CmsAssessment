CREATE TABLE [dbo].[Image]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[StockItemId] int foreign key references StockItem([Id]) not null,
	[Name] varchar(255) not null,
	[Data] varbinary(max) not null 
)
