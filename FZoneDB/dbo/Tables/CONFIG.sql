CREATE TABLE [dbo].[CONFIG] (
    [ConfigKey]      VARCHAR (10)  NOT NULL,
    [ConfigValue]    VARCHAR (150) NULL,
    [ConfigEngValue] VARCHAR (150) NULL,
    CONSTRAINT [PK_CONFIG] PRIMARY KEY CLUSTERED ([ConfigKey] ASC)
);

