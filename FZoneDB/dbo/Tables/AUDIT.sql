CREATE TABLE [dbo].[AUDIT] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [UserID]      INT           NULL,
    [Action]      VARCHAR (50)  NULL,
    [Description] VARCHAR (500) NULL,
    [TimeStamp]   DATETIME      NULL,
    CONSTRAINT [PK_AUDIT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AUDIT_USER] FOREIGN KEY ([UserID]) REFERENCES [dbo].[USER] ([ID])
);

