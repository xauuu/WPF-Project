
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/25/2022 22:37:11
-- Generated from EDMX file: D:\C#\Xauuu\StoreManagement\StoreManagement\Entities\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBStoreManagement];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Bill__CashierID__1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bill] DROP CONSTRAINT [FK__Bill__CashierID__1ED998B2];
GO
IF OBJECT_ID(N'[dbo].[FK__Bill__DiscountCo__45F365D3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bill] DROP CONSTRAINT [FK__Bill__DiscountCo__45F365D3];
GO
IF OBJECT_ID(N'[dbo].[FK__BillDetai__BillI__1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillDetail] DROP CONSTRAINT [FK__BillDetai__BillI__1ED998B2];
GO
IF OBJECT_ID(N'[dbo].[FK__BillDetai__Produ__21B6055D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillDetail] DROP CONSTRAINT [FK__BillDetai__Produ__21B6055D];
GO
IF OBJECT_ID(N'[dbo].[FK__Product__BrandID__15502E78]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK__Product__BrandID__15502E78];
GO
IF OBJECT_ID(N'[dbo].[FK__Product__TypeId__164452B1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK__Product__TypeId__164452B1];
GO
IF OBJECT_ID(N'[dbo].[FK__Users__RoleId__1B0907CE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK__Users__RoleId__1B0907CE];
GO
IF OBJECT_ID(N'[dbo].[FK__Warranty__PhoneN__267ABA7A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Warranty] DROP CONSTRAINT [FK__Warranty__PhoneN__267ABA7A];
GO
IF OBJECT_ID(N'[dbo].[FK__Warranty__Produc__276EDEB3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Warranty] DROP CONSTRAINT [FK__Warranty__Produc__276EDEB3];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bill];
GO
IF OBJECT_ID(N'[dbo].[BillDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillDetail];
GO
IF OBJECT_ID(N'[dbo].[Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Brand];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[CodePromotion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodePromotion];
GO
IF OBJECT_ID(N'[dbo].[CustomerInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerInfo];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Warranty]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Warranty];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bills'
CREATE TABLE [dbo].[Bills] (
    [BillID] int IDENTITY(1,1) NOT NULL,
    [BillDate] datetime  NOT NULL,
    [CashierID] int  NOT NULL,
    [TotalPrice] float  NOT NULL,
    [DiscountCode] nvarchar(50)  NULL,
    [LastTotalPrice] float  NOT NULL
);
GO

-- Creating table 'BillDetails'
CREATE TABLE [dbo].[BillDetails] (
    [BillID] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [Amount] int  NOT NULL,
    [UnitPrice] float  NOT NULL
);
GO

-- Creating table 'Brands'
CREATE TABLE [dbo].[Brands] (
    [BrandID] int IDENTITY(1,1) NOT NULL,
    [BrandName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryID] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CodePromotions'
CREATE TABLE [dbo].[CodePromotions] (
    [Code] nvarchar(50)  NOT NULL,
    [CodeName] nvarchar(50)  NOT NULL,
    [CodePercent] float  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'CustomerInfoes'
CREATE TABLE [dbo].[CustomerInfoes] (
    [PhoneNumber] nvarchar(10)  NOT NULL,
    [CustomerName] nvarchar(50)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DisplayName] nvarchar(max)  NOT NULL,
    [BrandID] int  NOT NULL,
    [CategoryID] int  NOT NULL,
    [Price] float  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [ImageURL] nvarchar(max)  NOT NULL,
    [OriginalPrice] float  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [DisplayName] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [IdentityCard] nvarchar(10)  NULL,
    [Birthdate] datetime  NULL,
    [Address] nvarchar(max)  NULL
);
GO

-- Creating table 'Warranties'
CREATE TABLE [dbo].[Warranties] (
    [WarrantyID] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(10)  NOT NULL,
    [ProductID] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BillID] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [PK_Bills]
    PRIMARY KEY CLUSTERED ([BillID] ASC);
GO

-- Creating primary key on [BillID], [ProductId] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [PK_BillDetails]
    PRIMARY KEY CLUSTERED ([BillID], [ProductId] ASC);
GO

-- Creating primary key on [BrandID] in table 'Brands'
ALTER TABLE [dbo].[Brands]
ADD CONSTRAINT [PK_Brands]
    PRIMARY KEY CLUSTERED ([BrandID] ASC);
GO

-- Creating primary key on [CategoryID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryID] ASC);
GO

-- Creating primary key on [Code] in table 'CodePromotions'
ALTER TABLE [dbo].[CodePromotions]
ADD CONSTRAINT [PK_CodePromotions]
    PRIMARY KEY CLUSTERED ([Code] ASC);
GO

-- Creating primary key on [PhoneNumber] in table 'CustomerInfoes'
ALTER TABLE [dbo].[CustomerInfoes]
ADD CONSTRAINT [PK_CustomerInfoes]
    PRIMARY KEY CLUSTERED ([PhoneNumber] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [WarrantyID], [PhoneNumber], [ProductID] in table 'Warranties'
ALTER TABLE [dbo].[Warranties]
ADD CONSTRAINT [PK_Warranties]
    PRIMARY KEY CLUSTERED ([WarrantyID], [PhoneNumber], [ProductID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CashierID] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK__Bill__CashierID__1ED998B2]
    FOREIGN KEY ([CashierID])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Bill__CashierID__1ED998B2'
CREATE INDEX [IX_FK__Bill__CashierID__1ED998B2]
ON [dbo].[Bills]
    ([CashierID]);
GO

-- Creating foreign key on [DiscountCode] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK__Bill__DiscountCo__45F365D3]
    FOREIGN KEY ([DiscountCode])
    REFERENCES [dbo].[CodePromotions]
        ([Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Bill__DiscountCo__45F365D3'
CREATE INDEX [IX_FK__Bill__DiscountCo__45F365D3]
ON [dbo].[Bills]
    ([DiscountCode]);
GO

-- Creating foreign key on [BillID] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [FK__BillDetai__BillI__1ED998B2]
    FOREIGN KEY ([BillID])
    REFERENCES [dbo].[Bills]
        ([BillID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductId] in table 'BillDetails'
ALTER TABLE [dbo].[BillDetails]
ADD CONSTRAINT [FK__BillDetai__Produ__21B6055D]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BillDetai__Produ__21B6055D'
CREATE INDEX [IX_FK__BillDetai__Produ__21B6055D]
ON [dbo].[BillDetails]
    ([ProductId]);
GO

-- Creating foreign key on [BrandID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK__Product__BrandID__15502E78]
    FOREIGN KEY ([BrandID])
    REFERENCES [dbo].[Brands]
        ([BrandID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Product__BrandID__15502E78'
CREATE INDEX [IX_FK__Product__BrandID__15502E78]
ON [dbo].[Products]
    ([BrandID]);
GO

-- Creating foreign key on [CategoryID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK__Product__TypeId__164452B1]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Categories]
        ([CategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Product__TypeId__164452B1'
CREATE INDEX [IX_FK__Product__TypeId__164452B1]
ON [dbo].[Products]
    ([CategoryID]);
GO

-- Creating foreign key on [PhoneNumber] in table 'Warranties'
ALTER TABLE [dbo].[Warranties]
ADD CONSTRAINT [FK__Warranty__PhoneN__267ABA7A]
    FOREIGN KEY ([PhoneNumber])
    REFERENCES [dbo].[CustomerInfoes]
        ([PhoneNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Warranty__PhoneN__267ABA7A'
CREATE INDEX [IX_FK__Warranty__PhoneN__267ABA7A]
ON [dbo].[Warranties]
    ([PhoneNumber]);
GO

-- Creating foreign key on [ProductID] in table 'Warranties'
ALTER TABLE [dbo].[Warranties]
ADD CONSTRAINT [FK__Warranty__Produc__276EDEB3]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Warranty__Produc__276EDEB3'
CREATE INDEX [IX_FK__Warranty__Produc__276EDEB3]
ON [dbo].[Warranties]
    ([ProductID]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK__Users__RoleId__1B0907CE]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Users__RoleId__1B0907CE'
CREATE INDEX [IX_FK__Users__RoleId__1B0907CE]
ON [dbo].[Users]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------