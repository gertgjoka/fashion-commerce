CREATE TABLE [dbo].[PRODUCT] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100)  NOT NULL,
    [Description]   VARCHAR (MAX)  NOT NULL,
    [OurPrice]      DECIMAL (6, 2) NOT NULL,
    [OriginalPrice] DECIMAL (6, 2) NOT NULL,
    [Discount]      AS             (CONVERT([int],(([OriginalPrice]-[OurPrice])/[OriginalPrice])*(100),(0))),
    [Approved]      BIT            CONSTRAINT [DF_PRODUCT_Approved] DEFAULT ((0)) NULL,
    [ApprovedBy]    INT            NULL,
    [Closed]        BIT            CONSTRAINT [DF_PRODUCT_ReadOnly] DEFAULT ((0)) NULL,
    [Code]          VARCHAR (50)   NULL,
    [SupplierPrice] DECIMAL (6, 2) NULL,
    CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRODUCT_USER] FOREIGN KEY ([ApprovedBy]) REFERENCES [dbo].[USER] ([ID])
);

