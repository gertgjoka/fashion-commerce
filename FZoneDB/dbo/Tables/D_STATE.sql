CREATE TABLE [dbo].[D_STATE] (
    [ID]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (50) NULL,
    [Enabled] BIT          CONSTRAINT [DF_D_STATE_Enabled] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_D_STATE] PRIMARY KEY CLUSTERED ([ID] ASC)
);

