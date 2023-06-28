CREATE TABLE [dbo].[Inventory] (
    [InventoryID]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [InventoryStatus] VARCHAR (16) NOT NULL,
    [ProductID]       BIGINT       NOT NULL,
    [ProductQty]      INT          NOT NULL,
    [LPN]             VARCHAR (32) NOT NULL,
    [CreationTime]    DATETIME     DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([InventoryID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]),
    CONSTRAINT [UC_Inventory_LPN] UNIQUE NONCLUSTERED ([LPN] ASC)
);


GO
CREATE NONCLUSTERED INDEX [NCI_Inventory_LPN]
    ON [dbo].[Inventory]([LPN] ASC);

