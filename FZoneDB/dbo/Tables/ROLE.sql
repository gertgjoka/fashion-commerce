CREATE TABLE [dbo].[ROLE] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [Description] VARCHAR (250) NULL,
    CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

