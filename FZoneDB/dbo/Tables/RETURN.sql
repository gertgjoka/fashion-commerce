CREATE TABLE [dbo].[RETURN] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [OrderID]            INT           NULL,
    [VerificationNumber] VARCHAR (20)  NULL,
    [RequestDate]        DATE          NULL,
    [ReceivedDate]       DATE          NULL,
    [Received]           NCHAR (10)    NULL,
    [Comments]           VARCHAR (250) NULL,
    CONSTRAINT [PK_RETURN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RETURN_ORDERS] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[ORDERS] ([ID]),
    CONSTRAINT [IX_RETURN] UNIQUE NONCLUSTERED ([OrderID] ASC)
);

