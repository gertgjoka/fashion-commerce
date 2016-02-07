CREATE TABLE [dbo].[INVITATION] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [CustomerID]       INT           NULL,
    [InvitedMail]      VARCHAR (50)  NULL,
    [Registered]       BIT           NULL,
    [RegistrationDate] DATE          NULL,
    [IP]               NVARCHAR (50) NULL,
    [Cookie]           NVARCHAR (50) NULL,
    CONSTRAINT [PK_INVITATION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_INVITATION_CUSTOMER] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CUSTOMER] ([ID]),
    CONSTRAINT [IX_INVITATION] UNIQUE NONCLUSTERED ([CustomerID] ASC, [InvitedMail] ASC)
);

