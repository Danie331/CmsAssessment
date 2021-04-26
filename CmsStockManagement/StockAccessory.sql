CREATE TABLE [dbo].[StockAccessory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[StockItemId] int foreign key references StockItem([Id]) not null,
	[Name] varchar(255) not null,
	[Description] varchar(255) null
)
