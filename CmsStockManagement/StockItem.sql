CREATE TABLE [dbo].[StockItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RegNo] varchar(20) NOT NULL,
	[Make] varchar(255) NOT NULL,
	[Model] varchar(255) NOT NULL,
	[ModelYear] int NOT NULL,
	[KMS] int not null,
	[Colour] varchar(20) not null,
	[VIN] varchar(17) not null,
	[RetailPrice] decimal(19, 4) not null,
	[CostPrice] decimal(19, 4) not null,
	[DTCreated] datetime default(getdate()) not null,
	[DTUpdated] datetime null
)
