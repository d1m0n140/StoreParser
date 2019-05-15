
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2019 14:36:51
-- Generated from EDMX file: E:\Projects\StoreParser\StoreParser\Models\StoreParserDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StoreParserDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Goods'
CREATE TABLE [dbo].[Goods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
	[Description] nvarchar(max)
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Img] image,
    [GoodId] int  NOT NULL,
	[ImageLink] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Prices'
CREATE TABLE [dbo].[Prices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [GoodId] int  NOT NULL,
	[DateTime] datetime  NOT NULL,
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Goods'
ALTER TABLE [dbo].[Goods]
ADD CONSTRAINT [PK_Goods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [PK_Prices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GoodId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_GoodImage]
    FOREIGN KEY ([GoodId])
    REFERENCES [dbo].[Goods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodImage'
CREATE INDEX [IX_FK_GoodImage]
ON [dbo].[Images]
    ([GoodId]);
GO

-- Creating foreign key on [GoodId] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [FK_GoodPrice]
    FOREIGN KEY ([GoodId])
    REFERENCES [dbo].[Goods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodPrice'
CREATE INDEX [IX_FK_GoodPrice]
ON [dbo].[Prices]
    ([GoodId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------