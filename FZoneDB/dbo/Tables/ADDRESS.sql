CREATE TABLE [dbo].[ADDRESS] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50)  NULL,
    [CustomerID] INT           NOT NULL,
    [Address]    VARCHAR (150) NULL,
    [Location]   VARCHAR (50)  NULL,
    [CityID]     INT           NULL,
    [Telephone]  VARCHAR (20)  NULL,
    [TypeID]     INT           NULL,
    [PostCode]   VARCHAR (10)  NULL,
    CONSTRAINT [PK_ADDRESS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADDRESS_CUSTOMER] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CUSTOMER] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ADDRESS_D_ADDRESS_TYPE] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[D_ADDRESS_TYPE] ([ID]),
    CONSTRAINT [FK_ADDRESS_D_CITY] FOREIGN KEY ([CityID]) REFERENCES [dbo].[D_CITY] ([ID])
);

