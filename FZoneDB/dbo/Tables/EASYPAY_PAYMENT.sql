CREATE TABLE [dbo].[EASYPAY_PAYMENT] (
    [ID]                INT             NOT NULL,
    [TransactionID]     VARCHAR (17)    NOT NULL,
    [MerchantUsername]  VARCHAR (50)    NOT NULL,
    [ResponseCode]      INT             NOT NULL,
    [TransactionStatus] VARCHAR (50)    NOT NULL,
    [OriginalResponse]  VARCHAR (500)   NOT NULL,
    [Amount]            DECIMAL (18, 2) NOT NULL,
    [Rate]              DECIMAL (18, 2) NOT NULL,
    [Date]              DATETIME        NOT NULL,
    [Fee]               DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_EASYPAY_PAYMENT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EASYPAY_PAYMENT_PAYMENT] FOREIGN KEY ([ID]) REFERENCES [dbo].[PAYMENT] ([ID])
);

