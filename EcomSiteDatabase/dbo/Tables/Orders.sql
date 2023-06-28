CREATE TABLE [dbo].[Orders] (
    [OrderID]      BIGINT       IDENTITY (1, 1) NOT NULL,
    [OrderStatus]  VARCHAR (16) NOT NULL,
    [CustomerID]   BIGINT       NOT NULL,
    [CreationTime] DATETIME     DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderID] ASC),
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);

