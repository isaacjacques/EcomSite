CREATE TABLE [dbo].[CustomerCart] (
    [CartID]       BIGINT   IDENTITY (1, 1) NOT NULL,
    [CustomerID]   BIGINT   NOT NULL,
    [ProductID]    BIGINT   NOT NULL,
    [ProductQty]   INT      NOT NULL,
    [CreationTime] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([CartID] ASC),
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID])
);

