CREATE TABLE [dbo].[ORDER_NOTES] (
    [OrderID]           INT           NOT NULL,
    [CreatedOn]         SMALLDATETIME NULL,
    [DisplayToCustomer] BIT           NULL,
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Text]              VARCHAR (150) NULL,
    [UserID]            INT           NOT NULL,
    CONSTRAINT [PK_ORDER_NOTES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ORDER_NOTES_ORDERS] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[ORDERS] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ORDER_NOTES_USER] FOREIGN KEY ([UserID]) REFERENCES [dbo].[USER] ([ID])
);

