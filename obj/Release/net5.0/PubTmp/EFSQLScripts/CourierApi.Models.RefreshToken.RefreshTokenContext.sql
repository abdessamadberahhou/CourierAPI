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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161823_init-2')
BEGIN
    CREATE TABLE [refresh_tokens] (
        [id] uniqueidentifier NOT NULL,
        [RefreshToken] varchar(1500) NULL,
        [User_Id] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_refresh_tokens] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161823_init-2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607161823_init-2', N'5.0.16');
END;
GO

COMMIT;
GO

