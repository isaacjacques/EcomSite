CREATE TABLE [dbo].[Brands] (
    [BrandID] INT          IDENTITY (1, 1) NOT NULL,
    [Brand]   VARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([BrandID] ASC),
    CONSTRAINT [UC_Brands_Brand] UNIQUE NONCLUSTERED ([Brand] ASC)
);

