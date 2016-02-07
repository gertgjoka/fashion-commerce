CREATE TABLE [dbo].[D_ATTRIBUTE] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [Description] VARCHAR (150) NULL,
    CONSTRAINT [PK_D_ATTRIBUTE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

