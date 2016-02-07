CREATE TABLE [dbo].[SHIPPING] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [ShippingType] VARCHAR (50)   NOT NULL,
    [ShippingCost] DECIMAL (6, 2) NOT NULL,
    CONSTRAINT [PK_SHIPPING] PRIMARY KEY CLUSTERED ([ID] ASC)
);

