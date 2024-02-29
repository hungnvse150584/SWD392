CREATE DATABASE BKPS
GO

use BKPS;
GO
CREATE TABLE [dbo].[Account] (
[Id]                   UNIQUEIDENTIFIER  NOT NULL,
[Email]                NVARCHAR (256) NULL,
[EmailConfirmed]       BIT            NOT NULL,
[PasswordHash]         NVARCHAR (MAX) NULL,
[SecurityStamp]        NVARCHAR (MAX) NULL,
[PhoneNumber]          NVARCHAR (MAX) NULL,
[PhoneNumberConfirmed] BIT            NOT NULL,
[TwoFactorEnabled]     BIT            NOT NULL,
[LockoutEndDateUtc]    DATETIME       NULL,
[LockoutEnabled]       BIT            NOT NULL,
[AccessFailedCount]    INT            NOT NULL,
[UserName]             NVARCHAR (256) NOT NULL,
[NormalizedUserName]   NVARCHAR (MAX) NULL,
[ConcurrencyStamp]   NVARCHAR (MAX) NULL,
[NormalizedEmail]   NVARCHAR (MAX) NULL,
[LockoutEnd]           DateTimeOffset(7)            NULL   

CONSTRAINT [PK_dbo.Account] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE Party (
    PartyId INT IDENTITY(1,1) PRIMARY KEY,
	PartyHostId int,
	PartyName nvarchar(255),
    Description nvarchar(1000),
	PhoneContact nvarchar(50),
	Place nvarchar(255),
	Rate float,
	ThumbnailUrl nvarchar(500),
	DayStart date,
	DayEnd date,
	PartyStatus nvarchar(500),
	CreatedDate date,
);

CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
	PartyHostId int,
	ProductName nvarchar(255),
    ProductUrl nvarchar(1000),
	ProductType nvarchar(50),
	ProductStyle nvarchar(100),
	Price float,
	ProductStatus nvarchar(500),
);

CREATE TABLE Room (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
	PartyId int,
	RoomName nvarchar(255),
    RoomUrl nvarchar(1000),
	RoomType nvarchar(50),
	Price float,
	RoomStatus nvarchar(500),
);


CREATE TABLE ListParty (
    ListPartyId INT IDENTITY(1,1) PRIMARY KEY,
	ParentId int,
	PartyId int,
    PartyHostId int,
	ListPartyStatus nvarchar(500),
);

CREATE TABLE ListProduct (
    ListProductId INT IDENTITY(1,1) PRIMARY KEY,
	PartyId int,
    RoomId int,
	ProductId int,
	Quantity int,
	ListProductStatus nvarchar(500),
);

CREATE TABLE ListRoom (
    ListRoomId INT IDENTITY(1,1) PRIMARY KEY,
	PartyId int,
    ParentId int,
	RoomId int,
	ListRoomStatus nvarchar(500),
);

CREATE TABLE AppConfig (
    [Key] nvarchar(50) PRIMARY KEY,
    Value nvarchar(500),
);

CREATE TABLE Feedback (
    	FeedBackId INT IDENTITY(1,1) PRIMARY KEY,
	ParentId int,
	PartyId int,
    	PartyHostId int,
	Score int,
	Feedback nvarchar(2000),
);

CREATE TABLE [dbo].[Roles] (
[Id]   UNIQUEIDENTIFIER  NOT NULL,
[Name] NVARCHAR (256) NOT NULL,
CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
ON [dbo].[Roles]([Name] ASC);

CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
ON [dbo].[Account]([UserName] ASC);

CREATE TABLE [dbo].[AspNetUserRoles] (
[UserId] UNIQUEIDENTIFIER  NOT NULL,
[RoleId] UNIQUEIDENTIFIER  NOT NULL,
CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Account] ([Id]) ON DELETE CASCADE
 );

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].[AspNetUserRoles]([UserId] ASC);

CREATE NONCLUSTERED INDEX [IX_RoleId]
ON [dbo].[AspNetUserRoles]([RoleId] ASC);

CREATE TABLE [dbo].[UserLogins] (
[LoginProvider] NVARCHAR (128) NOT NULL,
[ProviderKey]   NVARCHAR (128) NOT NULL,
[UserId]       UNIQUEIDENTIFIER  NOT NULL,
CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Account] ([Id]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].[UserLogins]([UserId] ASC);



CREATE TABLE [dbo].[UserClaims] (
[Id]         INT            IDENTITY (1, 1) NOT NULL,
[UserId]    UNIQUEIDENTIFIER  NOT NULL,
[ClaimType]  NVARCHAR (MAX) NULL,
[ClaimValue] NVARCHAR (MAX) NULL,
CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Account] ([Id]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].[UserClaims]([UserId] ASC);

ALTER TABLE ListParty
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (RoomId) REFERENCES Room(RoomId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (ProductId) REFERENCES Product(ProductId);


ALTER TABLE ListRoom
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListRoom
ADD FOREIGN KEY (RoomId) REFERENCES Room(RoomId);

ALTER TABLE Feedback
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);





INSERT INTO Roles (Id, Name) VALUES (1, N'Party Host');
INSERT INTO Roles (Id, Name) VALUES (2, N'Parent');
INSERT INTO Roles (Id, Name) VALUES (3, N'Admin');
