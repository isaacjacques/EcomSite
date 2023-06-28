CREATE TABLE [dbo].[OrderDetails] (
    [DetailID]       BIGINT   IDENTITY (1, 1) NOT NULL,
    [OrderID]      BIGINT   NOT NULL,
    [ProductID]    BIGINT   NOT NULL,
    [ProductQty]   INT      NOT NULL,
    [CreationTime] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([DetailID] ASC),
    FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID])
);

