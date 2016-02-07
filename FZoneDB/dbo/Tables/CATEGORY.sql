CREATE TABLE [dbo].[CATEGORY] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [CampaignID]  INT           NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (250) NULL,
    [ParentID]    INT           NULL,
    [AttributeID] INT           NULL,
    [Ordering]    INT           NULL,
    [NameEng]     VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_CATEGORY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CATEGORY_CAMPAIGN] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[CAMPAIGN] ([ID]),
    CONSTRAINT [FK_CATEGORY_CATEGORY] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[CATEGORY] ([ID]),
    CONSTRAINT [FK_CATEGORY_D_ATTRIBUTE] FOREIGN KEY ([AttributeID]) REFERENCES [dbo].[D_ATTRIBUTE] ([ID])
);

