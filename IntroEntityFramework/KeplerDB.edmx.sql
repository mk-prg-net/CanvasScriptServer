
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2016 15:40:53
-- Generated from EDMX file: C:\Users\marti_000\Documents\prj\KeplerDB\DB.Kepler.EF60\KeplerDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [KeplerDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RaumschiffeTab_HimmelskoerperTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RaumschiffeTab] DROP CONSTRAINT [FK_RaumschiffeTab_HimmelskoerperTab];
GO
IF OBJECT_ID(N'[dbo].[FK_Sterne_Planeten_MondeTab_HimmelskoerperTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sterne_Planeten_MondeTab] DROP CONSTRAINT [FK_Sterne_Planeten_MondeTab_HimmelskoerperTab];
GO
IF OBJECT_ID(N'[dbo].[FK_HimmelskoerperHimmelskoerperTyp]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HimmelskoerperTab] DROP CONSTRAINT [FK_HimmelskoerperHimmelskoerperTyp];
GO
IF OBJECT_ID(N'[dbo].[FK_HimmelskoerperUrlSammlung]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UrlSammlungenTab] DROP CONSTRAINT [FK_HimmelskoerperUrlSammlung];
GO
IF OBJECT_ID(N'[dbo].[FK_TrabantUmlaufbahn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UmlaufbahnenTab] DROP CONSTRAINT [FK_TrabantUmlaufbahn];
GO
IF OBJECT_ID(N'[dbo].[FK_UmlaufbahnZentralobjekt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UmlaufbahnenTab] DROP CONSTRAINT [FK_UmlaufbahnZentralobjekt];
GO
IF OBJECT_ID(N'[dbo].[FK_LandRaumschiffe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RaumschiffeTab] DROP CONSTRAINT [FK_LandRaumschiffe];
GO
IF OBJECT_ID(N'[dbo].[FK_RaumschiffAufgabenTab_AufgabenTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RaumschiffAufgabenTab] DROP CONSTRAINT [FK_RaumschiffAufgabenTab_AufgabenTab];
GO
IF OBJECT_ID(N'[dbo].[FK_RaumschiffAufgabenTab_RaumschiffeTab]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RaumschiffAufgabenTab] DROP CONSTRAINT [FK_RaumschiffAufgabenTab_RaumschiffeTab];
GO
IF OBJECT_ID(N'[dbo].[FK_HimmelskoerperBild]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BildTab] DROP CONSTRAINT [FK_HimmelskoerperBild];
GO
IF OBJECT_ID(N'[dbo].[FK_HimmelskoerperSpektralklasse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HimmelskoerperTab] DROP CONSTRAINT [FK_HimmelskoerperSpektralklasse];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AufgabenTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AufgabenTab];
GO
IF OBJECT_ID(N'[dbo].[HimmelskoerperTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HimmelskoerperTab];
GO
IF OBJECT_ID(N'[dbo].[HimmelskoerperTypenTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HimmelskoerperTypenTab];
GO
IF OBJECT_ID(N'[dbo].[LaenderTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LaenderTab];
GO
IF OBJECT_ID(N'[dbo].[RaumschiffeTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RaumschiffeTab];
GO
IF OBJECT_ID(N'[dbo].[Sterne_Planeten_MondeTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sterne_Planeten_MondeTab];
GO
IF OBJECT_ID(N'[dbo].[UmlaufbahnenTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UmlaufbahnenTab];
GO
IF OBJECT_ID(N'[dbo].[UrlSammlungenTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UrlSammlungenTab];
GO
IF OBJECT_ID(N'[dbo].[BildTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BildTab];
GO
IF OBJECT_ID(N'[dbo].[SpektralklasseTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpektralklasseTab];
GO
IF OBJECT_ID(N'[dbo].[RaumschiffAufgabenTab]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RaumschiffAufgabenTab];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AufgabenTab'
CREATE TABLE [dbo].[AufgabenTab] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Aufgabenbeschreibung] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HimmelskoerperTab'
CREATE TABLE [dbo].[HimmelskoerperTab] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Masse_in_kg] float  NOT NULL,
    [HimmelskoerperTyp_ID] int  NOT NULL,
    [SpektralklasseId] int  NULL
);
GO

-- Creating table 'HimmelskoerperTypenTab'
CREATE TABLE [dbo].[HimmelskoerperTypenTab] (
    [ID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LaenderTab'
CREATE TABLE [dbo].[LaenderTab] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Laenderkennzeichen] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RaumschiffeTab'
CREATE TABLE [dbo].[RaumschiffeTab] (
    [Start_der_Mission] datetime  NOT NULL,
    [HimmelskoerperID] int  NOT NULL,
    [Land_ID] int  NOT NULL,
    [RaumschiffAufgaben_ID] int  NOT NULL
);
GO

-- Creating table 'Sterne_Planeten_MondeTab'
CREATE TABLE [dbo].[Sterne_Planeten_MondeTab] (
    [Aequatordurchmesser_in_km] float  NOT NULL,
    [Polardurchmesser_in_km] float  NOT NULL,
    [Oberflaechentemperatur_in_K] float  NOT NULL,
    [Rotationsperiode_in_Stunden] float  NOT NULL,
    [Fallbeschleunigung_in_meter_pro_sec] float  NOT NULL,
    [Rotationsachsenneigung_in_Grad] float  NOT NULL,
    [HimmelskoerperID] int  NOT NULL,
    [Leuchtkraft_in_Lsonne] float  NOT NULL
);
GO

-- Creating table 'UmlaufbahnenTab'
CREATE TABLE [dbo].[UmlaufbahnenTab] (
    [Laenge_grosse_Halbachse_in_km] float  NOT NULL,
    [Exzentritzitaet] float  NOT NULL,
    [Umlaufdauer_in_Tagen] float  NOT NULL,
    [Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec] float  NOT NULL,
    [TrabantID] int  NOT NULL,
    [Zentralobjekt_ID] int  NOT NULL
);
GO

-- Creating table 'UrlSammlungenTab'
CREATE TABLE [dbo].[UrlSammlungenTab] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Kurzbeschreibung] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [UrlTyp] int  NOT NULL,
    [Himmelskoerper_ID] int  NOT NULL
);
GO

-- Creating table 'BildTab'
CREATE TABLE [dbo].[BildTab] (
    [HimmelskoerperID] int IDENTITY(1,1) NOT NULL,
    [Bilddaten] varbinary(max)  NOT NULL,
    [Himmelskoerper_ID] int  NOT NULL
);
GO

-- Creating table 'SpektralklasseTab'
CREATE TABLE [dbo].[SpektralklasseTab] (
    [ID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Farbe] nvarchar(max)  NOT NULL,
    [Tmin] float  NOT NULL,
    [Tmax] float  NOT NULL,
    [Masse_Hauptreihenstern_in_Sonnenmassen] float  NOT NULL
);
GO

-- Creating table 'RaumschiffAufgabenTab'
CREATE TABLE [dbo].[RaumschiffAufgabenTab] (
    [Aufgaben_ID] int  NOT NULL,
    [Raumschiffe_HimmelskoerperID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'AufgabenTab'
ALTER TABLE [dbo].[AufgabenTab]
ADD CONSTRAINT [PK_AufgabenTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'HimmelskoerperTab'
ALTER TABLE [dbo].[HimmelskoerperTab]
ADD CONSTRAINT [PK_HimmelskoerperTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'HimmelskoerperTypenTab'
ALTER TABLE [dbo].[HimmelskoerperTypenTab]
ADD CONSTRAINT [PK_HimmelskoerperTypenTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'LaenderTab'
ALTER TABLE [dbo].[LaenderTab]
ADD CONSTRAINT [PK_LaenderTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [HimmelskoerperID] in table 'RaumschiffeTab'
ALTER TABLE [dbo].[RaumschiffeTab]
ADD CONSTRAINT [PK_RaumschiffeTab]
    PRIMARY KEY CLUSTERED ([HimmelskoerperID] ASC);
GO

-- Creating primary key on [HimmelskoerperID] in table 'Sterne_Planeten_MondeTab'
ALTER TABLE [dbo].[Sterne_Planeten_MondeTab]
ADD CONSTRAINT [PK_Sterne_Planeten_MondeTab]
    PRIMARY KEY CLUSTERED ([HimmelskoerperID] ASC);
GO

-- Creating primary key on [TrabantID] in table 'UmlaufbahnenTab'
ALTER TABLE [dbo].[UmlaufbahnenTab]
ADD CONSTRAINT [PK_UmlaufbahnenTab]
    PRIMARY KEY CLUSTERED ([TrabantID] ASC);
GO

-- Creating primary key on [ID] in table 'UrlSammlungenTab'
ALTER TABLE [dbo].[UrlSammlungenTab]
ADD CONSTRAINT [PK_UrlSammlungenTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [HimmelskoerperID] in table 'BildTab'
ALTER TABLE [dbo].[BildTab]
ADD CONSTRAINT [PK_BildTab]
    PRIMARY KEY CLUSTERED ([HimmelskoerperID] ASC);
GO

-- Creating primary key on [ID] in table 'SpektralklasseTab'
ALTER TABLE [dbo].[SpektralklasseTab]
ADD CONSTRAINT [PK_SpektralklasseTab]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Aufgaben_ID], [Raumschiffe_HimmelskoerperID] in table 'RaumschiffAufgabenTab'
ALTER TABLE [dbo].[RaumschiffAufgabenTab]
ADD CONSTRAINT [PK_RaumschiffAufgabenTab]
    PRIMARY KEY CLUSTERED ([Aufgaben_ID], [Raumschiffe_HimmelskoerperID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [HimmelskoerperID] in table 'RaumschiffeTab'
ALTER TABLE [dbo].[RaumschiffeTab]
ADD CONSTRAINT [FK_RaumschiffeTab_HimmelskoerperTab]
    FOREIGN KEY ([HimmelskoerperID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [HimmelskoerperID] in table 'Sterne_Planeten_MondeTab'
ALTER TABLE [dbo].[Sterne_Planeten_MondeTab]
ADD CONSTRAINT [FK_Sterne_Planeten_MondeTab_HimmelskoerperTab]
    FOREIGN KEY ([HimmelskoerperID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [HimmelskoerperTyp_ID] in table 'HimmelskoerperTab'
ALTER TABLE [dbo].[HimmelskoerperTab]
ADD CONSTRAINT [FK_HimmelskoerperHimmelskoerperTyp]
    FOREIGN KEY ([HimmelskoerperTyp_ID])
    REFERENCES [dbo].[HimmelskoerperTypenTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HimmelskoerperHimmelskoerperTyp'
CREATE INDEX [IX_FK_HimmelskoerperHimmelskoerperTyp]
ON [dbo].[HimmelskoerperTab]
    ([HimmelskoerperTyp_ID]);
GO

-- Creating foreign key on [Himmelskoerper_ID] in table 'UrlSammlungenTab'
ALTER TABLE [dbo].[UrlSammlungenTab]
ADD CONSTRAINT [FK_HimmelskoerperUrlSammlung]
    FOREIGN KEY ([Himmelskoerper_ID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HimmelskoerperUrlSammlung'
CREATE INDEX [IX_FK_HimmelskoerperUrlSammlung]
ON [dbo].[UrlSammlungenTab]
    ([Himmelskoerper_ID]);
GO

-- Creating foreign key on [TrabantID] in table 'UmlaufbahnenTab'
ALTER TABLE [dbo].[UmlaufbahnenTab]
ADD CONSTRAINT [FK_TrabantUmlaufbahn]
    FOREIGN KEY ([TrabantID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Zentralobjekt_ID] in table 'UmlaufbahnenTab'
ALTER TABLE [dbo].[UmlaufbahnenTab]
ADD CONSTRAINT [FK_UmlaufbahnZentralobjekt]
    FOREIGN KEY ([Zentralobjekt_ID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UmlaufbahnZentralobjekt'
CREATE INDEX [IX_FK_UmlaufbahnZentralobjekt]
ON [dbo].[UmlaufbahnenTab]
    ([Zentralobjekt_ID]);
GO

-- Creating foreign key on [Land_ID] in table 'RaumschiffeTab'
ALTER TABLE [dbo].[RaumschiffeTab]
ADD CONSTRAINT [FK_LandRaumschiffe]
    FOREIGN KEY ([Land_ID])
    REFERENCES [dbo].[LaenderTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LandRaumschiffe'
CREATE INDEX [IX_FK_LandRaumschiffe]
ON [dbo].[RaumschiffeTab]
    ([Land_ID]);
GO

-- Creating foreign key on [Aufgaben_ID] in table 'RaumschiffAufgabenTab'
ALTER TABLE [dbo].[RaumschiffAufgabenTab]
ADD CONSTRAINT [FK_RaumschiffAufgabenTab_AufgabenTab]
    FOREIGN KEY ([Aufgaben_ID])
    REFERENCES [dbo].[AufgabenTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Raumschiffe_HimmelskoerperID] in table 'RaumschiffAufgabenTab'
ALTER TABLE [dbo].[RaumschiffAufgabenTab]
ADD CONSTRAINT [FK_RaumschiffAufgabenTab_RaumschiffeTab]
    FOREIGN KEY ([Raumschiffe_HimmelskoerperID])
    REFERENCES [dbo].[RaumschiffeTab]
        ([HimmelskoerperID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RaumschiffAufgabenTab_RaumschiffeTab'
CREATE INDEX [IX_FK_RaumschiffAufgabenTab_RaumschiffeTab]
ON [dbo].[RaumschiffAufgabenTab]
    ([Raumschiffe_HimmelskoerperID]);
GO

-- Creating foreign key on [Himmelskoerper_ID] in table 'BildTab'
ALTER TABLE [dbo].[BildTab]
ADD CONSTRAINT [FK_HimmelskoerperBild]
    FOREIGN KEY ([Himmelskoerper_ID])
    REFERENCES [dbo].[HimmelskoerperTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HimmelskoerperBild'
CREATE INDEX [IX_FK_HimmelskoerperBild]
ON [dbo].[BildTab]
    ([Himmelskoerper_ID]);
GO

-- Creating foreign key on [SpektralklasseId] in table 'HimmelskoerperTab'
ALTER TABLE [dbo].[HimmelskoerperTab]
ADD CONSTRAINT [FK_HimmelskoerperSpektralklasse]
    FOREIGN KEY ([SpektralklasseId])
    REFERENCES [dbo].[SpektralklasseTab]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HimmelskoerperSpektralklasse'
CREATE INDEX [IX_FK_HimmelskoerperSpektralklasse]
ON [dbo].[HimmelskoerperTab]
    ([SpektralklasseId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------