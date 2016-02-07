CREATE TABLE [dbo].[USER] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50)   NOT NULL,
    [Email]    VARCHAR (50)   NOT NULL,
    [Login]    VARCHAR (50)   NOT NULL,
    [Password] NVARCHAR (250) NOT NULL,
    [RoleID]   INT            NULL,
    [Enabled]  BIT            CONSTRAINT [DF_USER_Enabled] DEFAULT ((1)) NOT NULL,
    [LastIP]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_USER_ROLE] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[ROLE] ([ID]),
    CONSTRAINT [IX_USER] UNIQUE NONCLUSTERED ([Login] ASC)
);

