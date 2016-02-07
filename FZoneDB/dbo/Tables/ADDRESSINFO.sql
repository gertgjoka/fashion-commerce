CREATE TABLE [dbo].[ADDRESSINFO] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50)  NULL,
    [Address]   VARCHAR (150) NULL,
    [Telephone] VARCHAR (20)  NULL,
    [Location]  VARCHAR (50)  NULL,
    [City]      INT           NULL,
    [PostCode]  VARCHAR (10)  NULL,
    [Type]      INT           NULL,
    CONSTRAINT [PK_ADDRESSINFO_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADDRESSINFO_D_ADDRESS_TYPE] FOREIGN KEY ([Type]) REFERENCES [dbo].[D_ADDRESS_TYPE] ([ID]),
    CONSTRAINT [FK_ADDRESSINFO_D_CITY] FOREIGN KEY ([City]) REFERENCES [dbo].[D_CITY] ([ID])
);

