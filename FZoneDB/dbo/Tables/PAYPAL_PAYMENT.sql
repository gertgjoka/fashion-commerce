CREATE TABLE [dbo].[PAYPAL_PAYMENT] (
    [ID]                INT             NOT NULL,
    [Amount]            DECIMAL (18, 2) NULL,
    [PaidOn]            DATETIME        NULL,
    [Currency]          CHAR (3)        NULL,
    [PaypalEmail]       VARCHAR (50)    NULL,
    [PayerEmail]        VARCHAR (50)    NULL,
    [TransactionKey]    VARCHAR (17)    NULL,
    [TransactionStatus] VARCHAR (50)    NULL,
    [CartID]            CHAR (36)       NULL,
    [Response]          VARCHAR (MAX)   NULL,
    [PayerStatus]       VARCHAR (50)    NULL,
    [Fee]               DECIMAL (18, 2) NULL,
    [PayerName]         VARCHAR (50)    NULL,
    CONSTRAINT [PK_PAYPAL_PAYMENT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PAYPAL_PAYMENT_PAYMENT1] FOREIGN KEY ([ID]) REFERENCES [dbo].[PAYMENT] ([ID]),
    CONSTRAINT [IX_PAYPAL_PAYMENT] UNIQUE NONCLUSTERED ([TransactionKey] ASC)
);

