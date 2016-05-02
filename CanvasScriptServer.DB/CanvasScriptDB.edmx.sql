
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/28/2016 23:49:42
-- Generated from EDMX file: C:\Users\marti_000\Documents\prj\NET\C#\CanvasScriptServer\CanvasScriptServer.DB\CanvasScriptDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CanvasScriptsDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsersScripts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScriptsSet] DROP CONSTRAINT [FK_UsersScripts];
GO
IF OBJECT_ID(N'[dbo].[FK_UserNamesUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNamesSet] DROP CONSTRAINT [FK_UserNamesUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[ScriptsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScriptsSet];
GO
IF OBJECT_ID(N'[dbo].[UserNamesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserNamesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Created] datetime  NOT NULL
);
GO

-- Creating table 'ScriptsSet'
CREATE TABLE [dbo].[ScriptsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ScriptAsJson] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [Created] datetime  NOT NULL,
    [Modified] datetime  NOT NULL
);
GO

-- Creating table 'UserNamesSet'
CREATE TABLE [dbo].[UserNamesSet] (
    [Name] nvarchar(256)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScriptsSet'
ALTER TABLE [dbo].[ScriptsSet]
ADD CONSTRAINT [PK_ScriptsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Name] in table 'UserNamesSet'
ALTER TABLE [dbo].[UserNamesSet]
ADD CONSTRAINT [PK_UserNamesSet]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'ScriptsSet'
ALTER TABLE [dbo].[ScriptsSet]
ADD CONSTRAINT [FK_UsersScripts]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersScripts'
CREATE INDEX [IX_FK_UsersScripts]
ON [dbo].[ScriptsSet]
    ([UserId]);
GO

-- Creating foreign key on [User_Id] in table 'UserNamesSet'
ALTER TABLE [dbo].[UserNamesSet]
ADD CONSTRAINT [FK_UserNamesUsers]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNamesUsers'
CREATE INDEX [IX_FK_UserNamesUsers]
ON [dbo].[UserNamesSet]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------