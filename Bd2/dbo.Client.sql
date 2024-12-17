CREATE TABLE [dbo].[Client] (
    [client_id] VARCHAR(50) NOT NULL,
    [client_fio] NVARCHAR(50) NOT NULL,
    [phone_number] DECIMAL(18) NOT NULL,
    [adress] VARCHAR(50) NOT NULL,
    [email] NVARCHAR(50) NOT NULL,
    [order_number] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([client_id] ASC)
);
