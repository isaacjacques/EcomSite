CREATE TABLE [dbo].[Colors] (
    [ColorID] INT          IDENTITY (1, 1) NOT NULL,
    [Color]   VARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([ColorID] ASC),
    CONSTRAINT [UC_Colors_Color] UNIQUE NONCLUSTERED ([Color] ASC)
);

