CREATE TABLE [dbo].[CAMPAIGN] (
    [ID]                INT            IDENTITY (1, 1) NOT NULL,
    [BrandID]           INT            NOT NULL,
    [Name]              VARCHAR (50)   NULL,
    [Description]       VARCHAR (250)  NULL,
    [StartDate]         DATETIME       NOT NULL,
    [EndDate]           DATETIME       NOT NULL,
    [DeliveryStartDate] DATE           NULL,
    [DeliveryEndDate]   DATE           NULL,
    [Logo]              NVARCHAR (100) NULL,
    [ImageHome]         NVARCHAR (100) NULL,
    [ImageDetail]       NVARCHAR (100) NULL,
    [ImageListHeader]   NVARCHAR (100) NULL,
    [Active]            BIT            CONSTRAINT [DF_CAMPAIGN_Active] DEFAULT ((1)) NOT NULL,
    [GenericFile]       NVARCHAR (100) NULL,
    [Approved]          BIT            CONSTRAINT [DF_CAMPAIGN_Approved] DEFAULT ((0)) NULL,
    [ApprovedBy]        INT            NULL,
    CONSTRAINT [PK_CAMPAIGN_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CAMPAIGN_BRAND] FOREIGN KEY ([BrandID]) REFERENCES [dbo].[BRAND] ([ID]),
    CONSTRAINT [FK_CAMPAIGN_USER] FOREIGN KEY ([ApprovedBy]) REFERENCES [dbo].[USER] ([ID])
);

