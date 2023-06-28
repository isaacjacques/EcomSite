CREATE TABLE [dbo].[Customers] (
    [CustomerID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (255) NOT NULL,
    [LastName]     VARCHAR (255) NOT NULL,
    [Email]        VARCHAR (255) NOT NULL,
    [PasswordSalt] VARCHAR (64)  NOT NULL,
    [PasswordHash] VARCHAR (64)  NOT NULL,
    [CreationTime] DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [UC_Customers_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
CREATE NONCLUSTERED INDEX [NCI_Customers_Email]
    ON [dbo].[Customers]([Email] ASC);

