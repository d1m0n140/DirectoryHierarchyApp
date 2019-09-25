
SET QUOTED_IDENTIFIER OFF;
GO
USE [DirectoryHierarchyDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- Creating table 'Directories'
CREATE TABLE [dbo].[Directories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Path] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Relations'
CREATE TABLE [dbo].[Relations] (
    [ParentId] int  NOT NULL,
    [ChildId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Directories'
ALTER TABLE [dbo].[Directories]
ADD CONSTRAINT [PK_Directories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ParentId], [DirectoryId] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [PK_Relations]
    PRIMARY KEY CLUSTERED ([ParentId], [ChildId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ParentId] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [FK_ParentRelation]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Directories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DirectoryId] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [FK_ChildRelation]
    FOREIGN KEY ([ChildId])
    REFERENCES [dbo].[Directories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------