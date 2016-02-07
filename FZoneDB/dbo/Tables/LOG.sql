CREATE TABLE [dbo].[LOG] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Type]       INT           NULL,
    [Message]    VARCHAR (250) NULL,
    [StackTrace] VARCHAR (500) NULL,
    [Class]      VARCHAR (50)  NULL,
    [Method]     VARCHAR (50)  NULL,
    [LineNumber] INT           NULL,
    CONSTRAINT [PK_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);

