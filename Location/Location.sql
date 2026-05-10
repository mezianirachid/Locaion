
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/26/2022 09:37:41
-- Generated from EDMX file: C:\Users\admin\Desktop\Location\Location\ModelApplication.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Location];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BauxImmeubles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Baux] DROP CONSTRAINT [FK_BauxImmeubles];
GO
IF OBJECT_ID(N'[dbo].[FK_BauxLocataires]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Baux] DROP CONSTRAINT [FK_BauxLocataires];
GO
IF OBJECT_ID(N'[dbo].[FK_BauxLocateurs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Baux] DROP CONSTRAINT [FK_BauxLocateurs];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_ImmeublesBlocs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appartements] DROP CONSTRAINT [FK_ImmeublesBlocs];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionAppareils_Appareils]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionAppareils] DROP CONSTRAINT [FK_InclusionAppareils_Appareils];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionAppareils_Baux]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionAppareils] DROP CONSTRAINT [FK_InclusionAppareils_Baux];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionAutres_Autres]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionAutres] DROP CONSTRAINT [FK_InclusionAutres_Autres];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionAutres_Baux]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionAutres] DROP CONSTRAINT [FK_InclusionAutres_Baux];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionMeubles_Baux]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionMeubles] DROP CONSTRAINT [FK_InclusionMeubles_Baux];
GO
IF OBJECT_ID(N'[dbo].[FK_InclusionMeubles_Meubles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InclusionMeubles] DROP CONSTRAINT [FK_InclusionMeubles_Meubles];
GO
IF OBJECT_ID(N'[dbo].[FK_PayementBaux]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payements] DROP CONSTRAINT [FK_PayementBaux];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[Appareils]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appareils];
GO
IF OBJECT_ID(N'[dbo].[Appartements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appartements];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[AutresObjets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AutresObjets];
GO
IF OBJECT_ID(N'[dbo].[Baux]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Baux];
GO
IF OBJECT_ID(N'[dbo].[Blocs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Blocs];
GO
IF OBJECT_ID(N'[dbo].[InclusionAppareils]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InclusionAppareils];
GO
IF OBJECT_ID(N'[dbo].[InclusionAutres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InclusionAutres];
GO
IF OBJECT_ID(N'[dbo].[InclusionMeubles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InclusionMeubles];
GO
IF OBJECT_ID(N'[dbo].[Locataires]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locataires];
GO
IF OBJECT_ID(N'[dbo].[Locateurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locateurs];
GO
IF OBJECT_ID(N'[dbo].[Meubles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Meubles];
GO
IF OBJECT_ID(N'[dbo].[Payements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payements];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Appareils'
CREATE TABLE [dbo].[Appareils] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Description] nvarchar(500)  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Appartements'
CREATE TABLE [dbo].[Appartements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Adresse] nvarchar(500)  NULL,
    [Ville] nvarchar(100)  NULL,
    [CodePostal] nvarchar(100)  NULL,
    [TypeImmeuble] nvarchar(10)  NULL,
    [NbSalleBain] int  NULL,
    [NbEtages] int  NULL,
    [ChauffeOuiNon] nvarchar(1)  NULL,
    [Annee] int  NULL,
    [nbBalcons] int  NULL,
    [nbStationnements] int  NULL,
    [nbGarages] int  NULL,
    [Orientation] nvarchar(10)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModidiedBy] nchar(10)  NULL,
    [ModifiedOn] datetime  NULL,
    [BlocId] int  NULL,
    [NumeroEtage] int  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Nom] nvarchar(100)  NULL,
    [Prenom] nvarchar(100)  NULL
);
GO

-- Creating table 'AutresObjets'
CREATE TABLE [dbo].[AutresObjets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Description] nvarchar(500)  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Baux'
CREATE TABLE [dbo].[Baux] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NumeroBail] nvarchar(10)  NULL,
    [AppartementId] int  NOT NULL,
    [LocataireId] int  NOT NULL,
    [LocateurId] int  NOT NULL,
    [Prix] decimal(18,2)  NULL,
    [ModePayement] nvarchar(30)  NULL,
    [LieuPayement] nvarchar(100)  NULL,
    [ReglementImmeuble] bit  NULL,
    [DateDebut] datetime  NULL,
    [DateFin] datetime  NULL,
    [StationnementExt] bit  NULL,
    [NbPlacesExt] int  NULL,
    [StationnementInt] bit  NULL,
    [NbPlacesInt] int  NULL,
    [Emplacement] nvarchar(100)  NULL,
    [RemiseEspaceRangenment] nvarchar(100)  NULL,
    [Autre] nvarchar(500)  NULL,
    [MeublesInclus] bit  NULL,
    [AppareilsInclus] bit  NULL,
    [Deneigement] bit  NULL,
    [TailleGazon] bit  NULL,
    [MontantDepot] decimal(18,2)  NULL,
    [DateOccupation] datetime  NULL,
    [DateRevision] datetime  NULL,
    [Observation] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModidiedBy] nchar(10)  NULL,
    [ModifiedOn] datetime  NULL,
    [DatePayement] datetime  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Blocs'
CREATE TABLE [dbo].[Blocs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Description] nvarchar(500)  NULL,
    [Adresse] nvarchar(500)  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'InclusionAppareils'
CREATE TABLE [dbo].[InclusionAppareils] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AppareilId] int  NOT NULL,
    [Observation] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModidiedBy] nchar(10)  NULL,
    [ModifiedOn] datetime  NULL,
    [BauxId] int  NULL,
    [NbAppareilsInclus] int  NULL
);
GO

-- Creating table 'InclusionAutres'
CREATE TABLE [dbo].[InclusionAutres] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AutreObjetId] int  NOT NULL,
    [Observation] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModidiedBy] nchar(10)  NULL,
    [ModifiedOn] datetime  NULL,
    [BauxId] int  NULL,
    [NbAutresInclus] int  NULL
);
GO

-- Creating table 'InclusionMeubles'
CREATE TABLE [dbo].[InclusionMeubles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MeubleId] int  NOT NULL,
    [Observation] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModidiedBy] nchar(10)  NULL,
    [ModifiedOn] datetime  NULL,
    [BauxId] int  NULL,
    [NbMeublesInclus] int  NULL
);
GO

-- Creating table 'Locataires'
CREATE TABLE [dbo].[Locataires] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [Prenom] nvarchar(500)  NULL,
    [Civilite] nvarchar(20)  NULL,
    [Adresse] nvarchar(500)  NULL,
    [Ville] nvarchar(100)  NULL,
    [CodePostal] nvarchar(20)  NULL,
    [DateNaissance] datetime  NULL,
    [NAS] nvarchar(500)  NULL,
    [TelPrincipal] nvarchar(20)  NULL,
    [TelSecondaire] nvarchar(20)  NULL,
    [Courriel] nvarchar(500)  NULL,
    [NomContactUrgence] nvarchar(500)  NULL,
    [TelContactUrgence] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Locateurs'
CREATE TABLE [dbo].[Locateurs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [Prenom] nvarchar(500)  NULL,
    [Civilite] nvarchar(20)  NULL,
    [Adresse] nvarchar(500)  NULL,
    [Ville] nvarchar(100)  NULL,
    [CodePostal] nvarchar(20)  NULL,
    [DateNaissance] datetime  NULL,
    [NAS] nvarchar(500)  NULL,
    [TelPrincipal] nvarchar(20)  NULL,
    [TelSecondaire] nvarchar(20)  NULL,
    [Courriel] nvarchar(500)  NULL,
    [NomContactUrgence] nvarchar(500)  NULL,
    [TelContactUrgence] nvarchar(500)  NULL,
    [Signature] varbinary(max)  NULL,
    [DateInscription] datetime  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Meubles'
CREATE TABLE [dbo].[Meubles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [Description] nvarchar(500)  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'Payements'
CREATE TABLE [dbo].[Payements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Annee] int  NULL,
    [Mois] int  NULL,
    [DatePayement] datetime  NULL,
    [LieuPayement] nvarchar(500)  NULL,
    [Description] nvarchar(500)  NULL,
    [ModePayement] nvarchar(500)  NULL,
    [CreatedBy] nvarchar(500)  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedBy] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [BauxId] int  NULL,
    [Montant] decimal(18,2)  NULL,
    [Statut] nchar(1)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'Appareils'
ALTER TABLE [dbo].[Appareils]
ADD CONSTRAINT [PK_Appareils]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Appartements'
ALTER TABLE [dbo].[Appartements]
ADD CONSTRAINT [PK_Appartements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AutresObjets'
ALTER TABLE [dbo].[AutresObjets]
ADD CONSTRAINT [PK_AutresObjets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Baux'
ALTER TABLE [dbo].[Baux]
ADD CONSTRAINT [PK_Baux]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Blocs'
ALTER TABLE [dbo].[Blocs]
ADD CONSTRAINT [PK_Blocs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InclusionAppareils'
ALTER TABLE [dbo].[InclusionAppareils]
ADD CONSTRAINT [PK_InclusionAppareils]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InclusionAutres'
ALTER TABLE [dbo].[InclusionAutres]
ADD CONSTRAINT [PK_InclusionAutres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InclusionMeubles'
ALTER TABLE [dbo].[InclusionMeubles]
ADD CONSTRAINT [PK_InclusionMeubles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locataires'
ALTER TABLE [dbo].[Locataires]
ADD CONSTRAINT [PK_Locataires]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locateurs'
ALTER TABLE [dbo].[Locateurs]
ADD CONSTRAINT [PK_Locateurs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Meubles'
ALTER TABLE [dbo].[Meubles]
ADD CONSTRAINT [PK_Meubles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payements'
ALTER TABLE [dbo].[Payements]
ADD CONSTRAINT [PK_Payements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AppareilId] in table 'InclusionAppareils'
ALTER TABLE [dbo].[InclusionAppareils]
ADD CONSTRAINT [FK_InclusionAppareils_Appareils]
    FOREIGN KEY ([AppareilId])
    REFERENCES [dbo].[Appareils]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionAppareils_Appareils'
CREATE INDEX [IX_FK_InclusionAppareils_Appareils]
ON [dbo].[InclusionAppareils]
    ([AppareilId]);
GO

-- Creating foreign key on [AppartementId] in table 'Baux'
ALTER TABLE [dbo].[Baux]
ADD CONSTRAINT [FK_BauxImmeubles]
    FOREIGN KEY ([AppartementId])
    REFERENCES [dbo].[Appartements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BauxImmeubles'
CREATE INDEX [IX_FK_BauxImmeubles]
ON [dbo].[Baux]
    ([AppartementId]);
GO

-- Creating foreign key on [BlocId] in table 'Appartements'
ALTER TABLE [dbo].[Appartements]
ADD CONSTRAINT [FK_ImmeublesBlocs]
    FOREIGN KEY ([BlocId])
    REFERENCES [dbo].[Blocs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImmeublesBlocs'
CREATE INDEX [IX_FK_ImmeublesBlocs]
ON [dbo].[Appartements]
    ([BlocId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AutreObjetId] in table 'InclusionAutres'
ALTER TABLE [dbo].[InclusionAutres]
ADD CONSTRAINT [FK_InclusionAutres_Autres]
    FOREIGN KEY ([AutreObjetId])
    REFERENCES [dbo].[AutresObjets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionAutres_Autres'
CREATE INDEX [IX_FK_InclusionAutres_Autres]
ON [dbo].[InclusionAutres]
    ([AutreObjetId]);
GO

-- Creating foreign key on [LocataireId] in table 'Baux'
ALTER TABLE [dbo].[Baux]
ADD CONSTRAINT [FK_BauxLocataires]
    FOREIGN KEY ([LocataireId])
    REFERENCES [dbo].[Locataires]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BauxLocataires'
CREATE INDEX [IX_FK_BauxLocataires]
ON [dbo].[Baux]
    ([LocataireId]);
GO

-- Creating foreign key on [LocateurId] in table 'Baux'
ALTER TABLE [dbo].[Baux]
ADD CONSTRAINT [FK_BauxLocateurs]
    FOREIGN KEY ([LocateurId])
    REFERENCES [dbo].[Locateurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BauxLocateurs'
CREATE INDEX [IX_FK_BauxLocateurs]
ON [dbo].[Baux]
    ([LocateurId]);
GO

-- Creating foreign key on [BauxId] in table 'InclusionAppareils'
ALTER TABLE [dbo].[InclusionAppareils]
ADD CONSTRAINT [FK_InclusionAppareils_Baux]
    FOREIGN KEY ([BauxId])
    REFERENCES [dbo].[Baux]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionAppareils_Baux'
CREATE INDEX [IX_FK_InclusionAppareils_Baux]
ON [dbo].[InclusionAppareils]
    ([BauxId]);
GO

-- Creating foreign key on [BauxId] in table 'InclusionAutres'
ALTER TABLE [dbo].[InclusionAutres]
ADD CONSTRAINT [FK_InclusionAutres_Baux]
    FOREIGN KEY ([BauxId])
    REFERENCES [dbo].[Baux]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionAutres_Baux'
CREATE INDEX [IX_FK_InclusionAutres_Baux]
ON [dbo].[InclusionAutres]
    ([BauxId]);
GO

-- Creating foreign key on [BauxId] in table 'InclusionMeubles'
ALTER TABLE [dbo].[InclusionMeubles]
ADD CONSTRAINT [FK_InclusionMeubles_Baux]
    FOREIGN KEY ([BauxId])
    REFERENCES [dbo].[Baux]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionMeubles_Baux'
CREATE INDEX [IX_FK_InclusionMeubles_Baux]
ON [dbo].[InclusionMeubles]
    ([BauxId]);
GO

-- Creating foreign key on [BauxId] in table 'Payements'
ALTER TABLE [dbo].[Payements]
ADD CONSTRAINT [FK_PayementBaux]
    FOREIGN KEY ([BauxId])
    REFERENCES [dbo].[Baux]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PayementBaux'
CREATE INDEX [IX_FK_PayementBaux]
ON [dbo].[Payements]
    ([BauxId]);
GO

-- Creating foreign key on [MeubleId] in table 'InclusionMeubles'
ALTER TABLE [dbo].[InclusionMeubles]
ADD CONSTRAINT [FK_InclusionMeubles_Meubles]
    FOREIGN KEY ([MeubleId])
    REFERENCES [dbo].[Meubles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InclusionMeubles_Meubles'
CREATE INDEX [IX_FK_InclusionMeubles_Meubles]
ON [dbo].[InclusionMeubles]
    ([MeubleId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------