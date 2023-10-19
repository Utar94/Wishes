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

CREATE TABLE [Actors] (
    [ActorId] int NOT NULL IDENTITY,
    [Id] nvarchar(255) NOT NULL,
    [Type] nvarchar(255) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [DisplayName] nvarchar(50) NOT NULL,
    [EmailAddress] nvarchar(255) NULL,
    [PictureUrl] nvarchar(2048) NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([ActorId])
);
GO

CREATE TABLE [Wishlists] (
    [WishlistId] int NOT NULL IDENTITY,
    [DisplayName] nvarchar(50) NOT NULL,
    [PictureUrl] nvarchar(2048) NULL,
    [ItemCount] int NOT NULL,
    [AggregateId] nvarchar(255) NOT NULL,
    [Version] bigint NOT NULL,
    [CreatedBy] nvarchar(255) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(255) NOT NULL,
    [UpdatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Wishlists] PRIMARY KEY ([WishlistId])
);
GO

CREATE INDEX [IX_Actors_DisplayName] ON [Actors] ([DisplayName]);
GO

CREATE INDEX [IX_Actors_EmailAddress] ON [Actors] ([EmailAddress]);
GO

CREATE UNIQUE INDEX [IX_Actors_Id] ON [Actors] ([Id]);
GO

CREATE INDEX [IX_Actors_IsDeleted] ON [Actors] ([IsDeleted]);
GO

CREATE INDEX [IX_Actors_Type] ON [Actors] ([Type]);
GO

CREATE UNIQUE INDEX [IX_Wishlists_AggregateId] ON [Wishlists] ([AggregateId]);
GO

CREATE INDEX [IX_Wishlists_CreatedBy] ON [Wishlists] ([CreatedBy]);
GO

CREATE INDEX [IX_Wishlists_CreatedOn] ON [Wishlists] ([CreatedOn]);
GO

CREATE INDEX [IX_Wishlists_DisplayName] ON [Wishlists] ([DisplayName]);
GO

CREATE INDEX [IX_Wishlists_ItemCount] ON [Wishlists] ([ItemCount]);
GO

CREATE INDEX [IX_Wishlists_UpdatedBy] ON [Wishlists] ([UpdatedBy]);
GO

CREATE INDEX [IX_Wishlists_UpdatedOn] ON [Wishlists] ([UpdatedOn]);
GO

CREATE INDEX [IX_Wishlists_Version] ON [Wishlists] ([Version]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231016183728_InitialMigration', N'7.0.12');
GO

COMMIT;
GO
