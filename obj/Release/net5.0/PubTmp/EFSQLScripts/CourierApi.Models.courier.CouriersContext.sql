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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161446_init-1')
BEGIN
    CREATE TABLE [courrier] (
        [ID] uniqueidentifier NOT NULL,
        [TYPE_COURRIER] varchar(100) NULL,
        [OBJET_COURRIER] varchar(100) NULL,
        [EXPIDITEUR_COURRIER] varchar(100) NULL,
        [DESTINATAIRE_COURRIER] varchar(100) NULL,
        [DATE_COURRIER] date NULL,
        [TAGS_COURRIER] varchar(200) NULL,
        [COURRIER_FAVORISER] int NULL,
        [COURRIER_ARCHIVER] int NULL,
        [COURRIER_URGENT] int NULL,
        [ID_USER] uniqueidentifier NULL,
        CONSTRAINT [PK_courrier] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607161446_init-1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607161446_init-1', N'5.0.16');
END;
GO

COMMIT;
GO

