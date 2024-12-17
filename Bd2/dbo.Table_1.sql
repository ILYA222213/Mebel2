CREATE TABLE [dbo].[Material] (
    [name]    NVARCHAR (50) NOT NULL,
    [country] NVARCHAR (50) NOT NULL,
    [price]   MONEY         NOT NULL,
    CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED ([name] ASC)
);