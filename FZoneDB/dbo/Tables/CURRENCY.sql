CREATE TABLE [dbo].[CURRENCY] (
    [Date]         DATE            NOT NULL,
    [CurrencyRate] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CURRENCY] PRIMARY KEY CLUSTERED ([Date] ASC)
);

