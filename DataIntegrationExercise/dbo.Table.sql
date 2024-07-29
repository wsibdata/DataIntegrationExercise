CREATE TABLE [dbo].[FundDataTable]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Date] NCHAR(10) NULL, 
    [TransactionType] NVARCHAR(100) NULL, 
    [Fund] NVARCHAR(50) NULL, 
    [Value] NVARCHAR(50) NULL
)
