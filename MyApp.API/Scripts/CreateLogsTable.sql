-- NLog için Logs tablosu ve index'ler
-- Bu script'i veritabanınızda çalıştırın

USE [MyAppDb]
GO

-- Logs tablosu oluştur
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logs]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Logs] (
        [Id] [bigint] IDENTITY(1,1) NOT NULL,
        [Application] [nvarchar](50) NOT NULL,
        [Logged] [datetime] NOT NULL,
        [Level] [nvarchar](50) NOT NULL,
        [Message] [nvarchar](max) NOT NULL,
        [Logger] [nvarchar](250) NULL,
        [Callsite] [nvarchar](max) NULL,
        [Exception] [nvarchar](max) NULL,
        [Properties] [nvarchar](max) NULL,
        CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    PRINT 'Logs tablosu oluşturuldu.'
END
ELSE
BEGIN
    PRINT 'Logs tablosu zaten mevcut.'
END
GO

-- Index'ler oluştur (performans için)

-- Logged index (tarih bazlı sorgular için)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Logs_Logged' AND object_id = OBJECT_ID('Logs'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Logs_Logged] ON [dbo].[Logs] ([Logged] DESC)
    PRINT 'IX_Logs_Logged index oluşturuldu.'
END
ELSE
BEGIN
    PRINT 'IX_Logs_Logged index zaten mevcut.'
END
GO

-- Level index (log seviyesi bazlı sorgular için)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Logs_Level' AND object_id = OBJECT_ID('Logs'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Logs_Level] ON [dbo].[Logs] ([Level])
    PRINT 'IX_Logs_Level index oluşturuldu.'
END
ELSE
BEGIN
    PRINT 'IX_Logs_Level index zaten mevcut.'
END
GO

-- Logger index (logger bazlı sorgular için)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Logs_Logger' AND object_id = OBJECT_ID('Logs'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Logs_Logger] ON [dbo].[Logs] ([Logger])
    PRINT 'IX_Logs_Logger index oluşturuldu.'
END
ELSE
BEGIN
    PRINT 'IX_Logs_Logger index zaten mevcut.'
END
GO

-- Composite index (Logged + Level) - En çok kullanılan sorgu pattern'i
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Logs_Logged_Level' AND object_id = OBJECT_ID('Logs'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Logs_Logged_Level] ON [dbo].[Logs] ([Logged] DESC, [Level])
    PRINT 'IX_Logs_Logged_Level composite index oluşturuldu.'
END
ELSE
BEGIN
    PRINT 'IX_Logs_Logged_Level index zaten mevcut.'
END
GO

PRINT 'Tüm index''ler oluşturuldu.'
GO



