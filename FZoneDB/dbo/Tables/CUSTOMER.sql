CREATE TABLE [dbo].[CUSTOMER] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (50)   NOT NULL,
    [Surname]          VARCHAR (50)   NULL,
    [Email]            VARCHAR (50)   NOT NULL,
    [Password]         NVARCHAR (250) NULL,
    [BirthDate]        DATE           NULL,
    [Gender]           CHAR (1)       NULL,
    [Active]           BIT            CONSTRAINT [DF_CUSTOMER_Active] DEFAULT ((1)) NOT NULL,
    [Newsletter]       BIT            CONSTRAINT [DF_CUSTOMER_Newsletter] DEFAULT ((1)) NULL,
    [RegistrationDate] DATE           CONSTRAINT [DF_CUSTOMER_RegistrationDate] DEFAULT (getdate()) NULL,
    [InvitedFrom]      INT            NULL,
    [Telephone]        VARCHAR (50)   NULL,
    [Mobile]           VARCHAR (50)   NULL,
    [FirstBuy]         BIT            NULL,
    [FBId]             VARCHAR (20)   NULL,
    CONSTRAINT [PK_CUSTOMER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CUSTOMER_CUSTOMER] FOREIGN KEY ([InvitedFrom]) REFERENCES [dbo].[CUSTOMER] ([ID]),
    CONSTRAINT [IX_CUSTOMER] UNIQUE NONCLUSTERED ([Email] ASC)
);

