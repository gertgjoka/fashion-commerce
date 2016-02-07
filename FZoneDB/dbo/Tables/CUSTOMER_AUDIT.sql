CREATE TABLE [dbo].[CUSTOMER_AUDIT] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Type]        VARCHAR (50)   NULL,
    [Description] VARCHAR (1000) NULL,
    [IP]          VARCHAR (50)   NULL,
    [CustomerID]  INT            NULL,
    CONSTRAINT [PK_STAT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_STAT_CUSTOMER] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CUSTOMER] ([ID])
);

