
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/04/2019 22:06:25
-- Generated from EDMX file: E:\Users\jiang\source\repos\MyTgNet2\Diary.Data\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Diaries];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Diary_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Diary] DROP CONSTRAINT [FK_Diary_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Diary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Diary];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Diary'
CREATE TABLE [dbo].[Diary] (
    [DiaryId] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NULL,
    [UserId] int  NOT NULL,
    [IsPrivate] bit  NOT NULL,
    [IsDel] bit  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [DiaryId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DiaryId] in table 'Diary'
ALTER TABLE [dbo].[Diary]
ADD CONSTRAINT [PK_Diary]
    PRIMARY KEY CLUSTERED ([DiaryId] ASC);
GO

-- Creating primary key on [UserId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Diary'
ALTER TABLE [dbo].[Diary]
ADD CONSTRAINT [FK_Diary_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Diary_User'
CREATE INDEX [IX_FK_Diary_User]
ON [dbo].[Diary]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------