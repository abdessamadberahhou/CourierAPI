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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161626_init-2')
BEGIN
    CREATE TABLE [files] (
        [ID_FILE] uniqueidentifier NOT NULL,
        [ID_COURRIER] uniqueidentifier NOT NULL,
        [FILE] varbinary(max) NOT NULL,
        [FileName] varchar(200) NULL,
        [FileExtention] varchar(10) NULL,
        CONSTRAINT [PK__image__2C7C2D9405D45317] PRIMARY KEY ([ID_FILE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161626_init-2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607161626_init-2', N'5.0.16');
END;
GO

COMMIT;
GO

