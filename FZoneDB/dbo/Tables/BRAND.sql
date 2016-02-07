CREATE TABLE [dbo].[BRAND] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [ShowName]    NVARCHAR (50) NOT NULL,
    [Description] VARCHAR (250) NULL,
    [Contact]     VARCHAR (50)  NULL,
    [Telephone]   NVARCHAR (50) NULL,
    [Website]     VARCHAR (150) NULL,
    [Email]       VARCHAR (50)  NULL,
    [Shop]        BIT           CONSTRAINT [DF_BRAND_Shop] DEFAULT ((0)) NULL,
    [Address]     VARCHAR (150) NULL,
    CONSTRAINT [PK_BRAND] PRIMARY KEY CLUSTERED ([ID] ASC)
);

