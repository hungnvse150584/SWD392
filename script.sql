CREATE DATABASE BKPS;
GO

use BKPS;
GO
CREATE TABLE [dbo].[AspNetUsers] (
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

CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
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
	ProductType int,
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

CREATE TABLE ProductType (
	Id INT PRIMARY KEY,
	ProductTypeName nvarchar(255),
	Status nvarchar(500),

)

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

CREATE TABLE [dbo].[AspNetRoles] (
[Id]   UNIQUEIDENTIFIER NOT NULL,
[Name] NVARCHAR (256) NOT NULL,
[NormalizedUserName] NVARCHAR(MAX) NULL,
[ConcurrencyStamp] NVARCHAR(MAX) NULL, 

CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
ON [dbo].AspNetRoles([Name] ASC);

CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
ON [dbo].[AspNetUsers]([UserName] ASC);

CREATE TABLE [dbo].[AspNetUserRoles] (
[UserId] UNIQUEIDENTIFIER  NOT NULL,
[RoleId] UNIQUEIDENTIFIER  NOT NULL,
CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE

 );

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].[AspNetUserRoles]([UserId] ASC);

CREATE NONCLUSTERED INDEX [IX_RoleId]
ON [dbo].[AspNetUserRoles]([RoleId] ASC);

CREATE TABLE [dbo].AspNetUserLogins (
[LoginProvider] NVARCHAR (128) NOT NULL,
[ProviderKey]   NVARCHAR (128) NOT NULL,
[UserId]       UNIQUEIDENTIFIER  NOT NULL,
CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].AspNetUserLogins([UserId] ASC);



CREATE TABLE [dbo].AspNetUserClaims (
[Id]         INT            IDENTITY (1, 1) NOT NULL,
[UserId]    UNIQUEIDENTIFIER  NOT NULL,
[ClaimType]  NVARCHAR (MAX) NULL,
[ClaimValue] NVARCHAR (MAX) NULL,
CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_UserId]
ON [dbo].[AspNetUserClaims]([UserId] ASC);

ALTER TABLE ListParty
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (RoomId) REFERENCES Room(RoomId);

ALTER TABLE ListProduct
ADD FOREIGN KEY (ProductId) REFERENCES Product(ProductId);

ALTER TABLE Product
ADD FOREIGN KEY (ProductType) REFERENCES ProductType(Id);

ALTER TABLE ListRoom
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

ALTER TABLE ListRoom
ADD FOREIGN KEY (RoomId) REFERENCES Room(RoomId);

ALTER TABLE Feedback
ADD FOREIGN KEY (PartyId) REFERENCES Party(PartyId);

-- Insert dữ liệu mẫu vào bảng AspNetUsers
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [NormalizedUserName], [ConcurrencyStamp], [NormalizedEmail], [LockoutEnd])
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'example1@example.com', 1, 'password_hash_1', 'security_stamp_1', NULL, 0, 0, NULL, 1, 0, 'user1', 'USER1', 'concurrency_stamp_1', 'example1@example.com', NULL),
    ('22222222-2222-2222-2222-222222222222', 'example2@example.com', 1, 'password_hash_2', 'security_stamp_2', NULL, 0, 0, NULL, 1, 0, 'user2', 'USER2', 'concurrency_stamp_2', 'example2@example.com', NULL);

-- Insert dữ liệu mẫu vào bảng Party
INSERT INTO Party (PartyHostId, PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus, CreatedDate)
VALUES
    (1, 'Party 1', 'Description for Party 1', '123456789', 'Location 1', 4.5, 'thumbnail_url_1.jpg', '2024-03-01', '2024-03-02', 'Active', '2024-02-29'),
    (2, 'Party 2', 'Description for Party 2', '987654321', 'Location 2', 4.8, 'thumbnail_url_2.jpg', '2024-03-03', '2024-03-04', 'Active', '2024-02-29');

	INSERT INTO dbo.ProductType(Id,ProductTypeName,Status)
VALUES ('1','Food','active'),
       ('2','Drink','active');

-- Insert dữ liệu mẫu vào bảng Product
INSERT INTO Product (PartyHostId, ProductName, ProductUrl, ProductType, ProductStyle, Price, ProductStatus)
VALUES
    (1, 'Product 1', 'product_url_1', '1', 'Style 1', 10.5, 'Active'),
    (1, 'Product 2', 'product_url_2', '2', 'Style 2', 15.75, 'Active'),
    (2, 'Product 3', 'product_url_3', '2', 'Style 3', 20.0, 'Active');

-- Insert dữ liệu mẫu vào bảng Room
INSERT INTO Room (PartyId, RoomName, RoomUrl, RoomType, Price, RoomStatus)
VALUES
    (1, 'Room 1', 'room_url_1', 'Type 1', 50.0, 'Available'),
    (1, 'Room 2', 'room_url_2', 'Type 2', 75.0, 'Available'),
    (2, 'Room 3', 'room_url_3', 'Type 3', 100.0, 'Available');

-- Insert dữ liệu mẫu vào bảng ListParty
INSERT INTO ListParty (ParentId, PartyId, PartyHostId, ListPartyStatus)
VALUES
    (1, 1, 1, 'Confirmed'),
    (2, 2, 2, 'Pending');

-- Insert dữ liệu mẫu vào bảng ListProduct
INSERT INTO ListProduct (PartyId, RoomId, ProductId, Quantity, ListProductStatus)
VALUES
    (1, 1, 1, 2, 'Confirmed'),
    (1, 2, 2, 1, 'Confirmed'),
    (2, NULL, 3, 3, 'Pending');

-- Insert dữ liệu mẫu vào bảng ListRoom
INSERT INTO ListRoom (PartyId, ParentId, RoomId, ListRoomStatus)
VALUES
    (1, NULL, 1, 'Confirmed'),
    (1, NULL, 2, 'Confirmed'),
    (2, NULL, NULL, 'Pending');

-- Insert dữ liệu mẫu vào bảng AppConfig
INSERT INTO AppConfig ([Key], Value)
VALUES
    ('HomeDescription', 'This is description of Booking'),
    ('HomeKeyword', 'This is keyword of Booking'),
    ('HomeTitle', 'This is home page of Booking');


-- Insert dữ liệu mẫu vào bảng Feedback
INSERT INTO Feedback (ParentId, PartyId, PartyHostId, Score, Feedback)
VALUES
    (1, 1, 1, 4, 'Good experience'),
    (2, 2, 2, 5, 'Excellent service');

-- Insert dữ liệu mẫu vào bảng AspNetRoles
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedUserName], [ConcurrencyStamp])
VALUES
  ('33333333-3333-3333-3333-333333333333', 'RoleName1', 'normalized_username_1', 'concurrency_stamp_1'),
  ('44444444-4444-4444-4444-444444444444', 'RoleName2', 'normalized_username_2', 'concurrency_stamp_2'),
  ('55555555-5555-5555-5555-555555555555', 'RoleName3', 'normalized_username_3', 'concurrency_stamp_3');

  -- Insert dữ liệu mẫu vào bảng AspNetUserRoles

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
VALUES
  ('11111111-1111-1111-1111-111111111111', '33333333-3333-3333-3333-333333333333'),
  ('22222222-2222-2222-2222-222222222222', '44444444-4444-4444-4444-444444444444');

-- Insert dữ liệu mẫu vào bảng AspNetUserLogins
INSERT INTO [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId])
VALUES 
    ('login_provider_1', 'provider_key_1', '11111111-1111-1111-1111-111111111111'),
    ('login_provider_2', 'provider_key_2', '22222222-2222-2222-2222-222222222222');

-- Insert dữ liệu mẫu vào bảng AspNetUserClaims
INSERT INTO [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue])
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'claim_type_1', 'claim_value_1'),
    ('22222222-2222-2222-2222-222222222222', 'claim_type_2', 'claim_value_2');



