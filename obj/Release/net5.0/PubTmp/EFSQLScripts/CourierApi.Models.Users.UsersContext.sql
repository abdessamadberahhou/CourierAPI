IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607160516_init')
BEGIN
    CREATE TABLE [users] (
        [id] uniqueidentifier NOT NULL,
        [email] varchar(150) NULL,
        [first_name] varchar(150) NULL,
        [last_name] varchar(150) NULL,
        [birth_day] varchar(12) NULL,
        [cin] varchar(10) NULL,
        [password] varchar(1500) NULL,
        [AVATAR] varbinary(max) NULL,
        [NUM_TELE] varchar(14) NULL,
        [IS_ADMIN] int NOT NULL,
        [is_accepted] int NOT NULL,
        CONSTRAINT [PK_users] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607160516_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607160516_init', N'5.0.16');
END;
GO

COMMIT;
GO

