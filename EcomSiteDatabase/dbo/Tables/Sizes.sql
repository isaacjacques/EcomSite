CREATE TABLE [dbo].[Sizes] (
    [SizeID] INT          IDENTITY (1, 1) NOT NULL,
    [Size]   VARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([SizeID] ASC),
    CONSTRAINT [UC_Sizes_Size] UNIQUE NONCLUSTERED ([Size] ASC)
);

