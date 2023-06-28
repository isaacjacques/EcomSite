CREATE TABLE [dbo].[Products] (
    [ProductID]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [SKU]          VARCHAR (32)  NOT NULL,
    [UPC]          VARCHAR (32)  NOT NULL,
    [ColorID]      INT           NULL,
    [SizeID]       INT           NULL,
    [BrandID]      INT           NULL,
    [PackSize]     INT           NULL,
    [Description]  VARCHAR (255) NOT NULL,
    [CreationTime] DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([BrandID]) REFERENCES [dbo].[Brands] ([BrandID]),
    FOREIGN KEY ([ColorID]) REFERENCES [dbo].[Colors] ([ColorID]),
    FOREIGN KEY ([SizeID]) REFERENCES [dbo].[Sizes] ([SizeID]),
    CONSTRAINT [UC_Products_SKU_UPC] UNIQUE NONCLUSTERED ([SKU] ASC, [UPC] ASC)
);

