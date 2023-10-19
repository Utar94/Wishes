BEGIN TRANSACTION;
GO

CREATE TABLE [Items] (
    [ItemId] int NOT NULL IDENTITY,
    [WishlistId] int NOT NULL,
    [Id] nvarchar(32) NOT NULL,
    [DisplayName] nvarchar(50) NOT NULL,
    [Summary] nvarchar(100) NULL,
    [PictureUrl] nvarchar(2048) NULL,
    [Rank] tinyint NOT NULL,
    [RankCategory] tinyint NOT NULL,
    [AveragePrice] float NULL,
    [MinimumPrice] float NULL,
    [MaximumPrice] float NULL,
    [PriceCategory] tinyint NULL,
    [ContentText] nvarchar(max) NULL,
    [ContentType] nvarchar(13) NULL,
    [Gallery] nvarchar(max) NULL,
    [Links] nvarchar(max) NULL,
    [Version] bigint NOT NULL,
    [CreatedBy] nvarchar(255) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(255) NOT NULL,
    [UpdatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId]),
    CONSTRAINT [FK_Items_Wishlists_WishlistId] FOREIGN KEY ([WishlistId]) REFERENCES [Wishlists] ([WishlistId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Items_AveragePrice] ON [Items] ([AveragePrice]);
GO

CREATE INDEX [IX_Items_CreatedBy] ON [Items] ([CreatedBy]);
GO

CREATE INDEX [IX_Items_CreatedOn] ON [Items] ([CreatedOn]);
GO

CREATE INDEX [IX_Items_DisplayName] ON [Items] ([DisplayName]);
GO

CREATE INDEX [IX_Items_MaximumPrice] ON [Items] ([MaximumPrice]);
GO

CREATE INDEX [IX_Items_MinimumPrice] ON [Items] ([MinimumPrice]);
GO

CREATE INDEX [IX_Items_PriceCategory] ON [Items] ([PriceCategory]);
GO

CREATE INDEX [IX_Items_Rank] ON [Items] ([Rank]);
GO

CREATE INDEX [IX_Items_RankCategory] ON [Items] ([RankCategory]);
GO

CREATE INDEX [IX_Items_Summary] ON [Items] ([Summary]);
GO

CREATE INDEX [IX_Items_UpdatedBy] ON [Items] ([UpdatedBy]);
GO

CREATE INDEX [IX_Items_UpdatedOn] ON [Items] ([UpdatedOn]);
GO

CREATE INDEX [IX_Items_Version] ON [Items] ([Version]);
GO

CREATE UNIQUE INDEX [IX_Items_WishlistId_Id] ON [Items] ([WishlistId], [Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231017203125_CreateItemTable', N'7.0.12');
GO

COMMIT;
GO
