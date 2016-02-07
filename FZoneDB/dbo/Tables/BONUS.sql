CREATE TABLE [dbo].[BONUS] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [CustomerID]     INT            NULL,
    [Value]          DECIMAL (6, 2) NULL,
    [ValueRemainder] DECIMAL (6, 2) NULL,
    [DateAssigned]   DATE           NULL,
    [Validity]       DATE           NULL,
    [Description]    VARCHAR (250)  NULL,
    [Version]        ROWVERSION     NULL,
    CONSTRAINT [PK_BONUS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BONUS_CUSTOMER] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CUSTOMER] ([ID])
);

