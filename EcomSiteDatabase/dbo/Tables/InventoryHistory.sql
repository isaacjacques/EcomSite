CREATE TABLE [dbo].[InventoryHistory] (
    [InventoryHistoryID] BIGINT   IDENTITY (1, 1) NOT NULL,
    [InventoryID]        BIGINT   NOT NULL,
    [OrderID]            BIGINT   NOT NULL,
    [OrderQty]           INT      NOT NULL,
    [CreationTime]       DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([InventoryHistoryID] ASC),
    FOREIGN KEY ([InventoryID]) REFERENCES [dbo].[Inventory] ([InventoryID]),
    FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID])
);

