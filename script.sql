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
[LockoutEnd]           DateTimeOffset(7)            NULL,
FirstName NVARCHAR (256) Null,
LastName nvarchar (256) null,
Dob date null,

CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE Party (
    PartyId INT IDENTITY(1,1) PRIMARY KEY,
	PartyHostId UNIQUEIDENTIFIER,
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
	PartyHostId UNIQUEIDENTIFIER,
	ProductName nvarchar(255),
    	ProductUrl nvarchar(1000),
	ProductType int,
	ProductStyle nvarchar(100),
	Price float,
	ProductStatus nvarchar(500),
	Description nvarchar(1000),
);

CREATE TABLE Room (
    	RoomId INT IDENTITY(1,1) PRIMARY KEY,
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
	ParentId UNIQUEIDENTIFIER,
	PartyId int,
    	PartyHostId UNIQUEIDENTIFIER,
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
    	ParentId UNIQUEIDENTIFIER,
	RoomId int,
	Total float,
	ListRoomStatus nvarchar(500),
);

CREATE TABLE AppConfig (
    [Key] nvarchar(50) PRIMARY KEY,
    Value nvarchar(500),
);

CREATE TABLE Feedback (
    FeedBackId INT IDENTITY(1,1) PRIMARY KEY,
	ParentId UNIQUEIDENTIFIER,
	PartyId int,
	Score int,
	Feedback nvarchar(2000),
);

CREATE TABLE [dbo].[AspNetRoles] (
[Id]   UNIQUEIDENTIFIER NOT NULL,
[Name] NVARCHAR (256) NOT NULL,
[NormalizedName] NVARCHAR(MAX) NULL,
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

CREATE TABLE AspNetRoleClaims(
[Id]         INT            IDENTITY (1, 1) NOT NULL,
RoleId 		UNIQUEIDENTIFIER NOT NULL,
ClaimType NVarchar(max) Null,
ClaimValue Nvarchar(max) Null,
CONSTRAINT [PK_dbo.AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
)


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

-- Birthday Party
--INSERT INTO Party (PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus)
--VALUES ('Birthday Bash!', 'Celebrate a birthday with fun games, music, and cake!', '123-456-7890', 'Party Venue', '50.00', 'https://example.com/birthday_party.jpg', '2024-04-15', '2024-04-15', 'Active');

---- Pool Party
--INSERT INTO Party (PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus)
--VALUES ('Pool Party Fun!', 'Enjoy a day by the pool with food, drinks, and games!', '987-654-3210', 'Host Name House', '25.00', 'https://example.com/pool_party.jpg', '2024-05-25', '2024-05-25', 'Active');

---- Karaoke Night
--INSERT INTO Party (PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus)
--VALUES ('Lets Sing! Karaoke Night', 'Belt out your favorite tunes with friends!', '555-123-4567', 'Karaoke Bar Name', '15.00', 'https://example.com/karaoke_night.jpg', '2024-06-10', '2024-06-10', 'Active');

---- Movie Night
--INSERT INTO Party (PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus)
--VALUES ('Cozy Movie Night In', 'Relax with friends and watch a movie!', 'Host Phone Number', 'Host Address', '100', 'https://example.com/movie_night.jpg', '2024-07-04', '2024-07-04', 'Active');

---- Themed Party (Halloween)
--INSERT INTO Party (PartyName, Description, PhoneContact, Place, Rate, ThumbnailUrl, DayStart, DayEnd, PartyStatus)
--VALUES ('Spooky Halloween Party', 'Dress up in costumes and join the Halloween fun!', '777-321-6543', 'Party Venue', '30.00', 'https://example.com/halloween_party.jpg', '2023-10-31', '2023-10-31', 'Completed');



INSERT INTO dbo.ProductType(Id,ProductTypeName,Status)
VALUES ('1','Food','Active'),
       ('2','Drink','Active');

-- Insert dữ liệu mẫu vào bảng Product
--INSERT INTO Product (PartyHostId, ProductName, ProductUrl, ProductType, ProductStyle, Price, ProductStatus, Description)
--VALUES
--    ('11111111-1111-1111-1111-111111111111', 'Pizza', 'https://firebasestorage.googleapis.com/v0/b/bpks-ee4a1.appspot.com/o/images%2FProductImage%2F1d517f87-9f0a-468b-94e0-788b703982d8.jpg?alt=media&token=e6750202-ede8-4ffb-9099-64b78b940fea', '1', 'Style 1', 2452000, 'Active', 'ngon'),
--    ('11111111-1111-1111-1111-111111111111', 'Bánh kem', 'https://firebasestorage.googleapis.com/v0/b/bpks-ee4a1.appspot.com/o/images%2FProductImage%2F1d517f87-9f0a-468b-94e0-788b703982d8.jpg?alt=media&token=e6750202-ede8-4ffb-9099-64b78b940fea', '1', 'Style 2', 150000, 'Active', 'ngon'),
--    ('22222222-2222-2222-2222-222222222222', 'Nước ép cam', 'https://firebasestorage.googleapis.com/v0/b/bpks-ee4a1.appspot.com/o/images%2FProductImage%2Fd529ac06-28a8-4466-a50b-baee6f004c03.jpg?alt=media&token=e75d2510-afe2-4dc8-9248-246858ef83c7', '2', 'Style 3', 130000, 'Active', 'ngon'),
--    ('22222222-2222-2222-2222-222222222222', 'Nước trái cây', 'https://firebasestorage.googleapis.com/v0/b/bpks-ee4a1.appspot.com/o/images%2FProductImage%2F4509c624-336c-4a4a-8673-41a542a97a4a.jpg?alt=media&token=6edb8680-a7c0-41a4-baa3-c090ef3e870a', '2', 'Style 3', 130000, 'Active', 'ngon'),
--	('22222222-2222-2222-2222-222222222222', 'Bò nướng', 'https://firebasestorage.googleapis.com/v0/b/bpks-ee4a1.appspot.com/o/images%2FProductImage%2Fe04e7c3b-fbc4-470f-a639-a57d019f9d86.jpg?alt=media&token=4f23d9cd-d1c3-48c8-bdc3-48a4299b48b5', '1', 'Style 3', 450000, 'Active', 'ngon');

-- Insert dữ liệu mẫu vào bảng Room
--INSERT INTO Room (RoomName, RoomUrl, RoomType, Price, RoomStatus)
--VALUES
--    ('Room 1', 'room_url_1', 'Type 1', 50.0, 'Active'),
--    ('Room 2', 'room_url_2', 'Type 2', 75.0, 'Active'),
--    ('Room 3', 'room_url_3', 'Type 3', 100.0, 'Active');

-- Insert dữ liệu mẫu vào bảng ListParty
--INSERT INTO ListParty (PartyId, PartyHostId, ListPartyStatus)
--VALUES
--    ( 1, '11111111-1111-1111-1111-111111111111', 'Active'),
--    ( 2, '11111111-1111-1111-1111-111111111111', 'Pending');

-- Insert dữ liệu mẫu vào bảng ListProduct
--INSERT INTO ListProduct (PartyId, RoomId, ProductId, Quantity, ListProductStatus)
--VALUES
--    (1, 1, 1, 2, 'Active'),
--    (1, 2, 2, 1, 'Active'),
--    (2, 2, 3, 3, 'Active');

-- Insert dữ liệu mẫu vào bảng ListRoom
--INSERT INTO ListRoom (PartyId, ParentId, RoomId, ListRoomStatus,Total)
--VALUES
--    (1, NULL, 1, 'Confirmed',10000),
--    (1, NULL, 2, 'Confirmed',10000),
--    (2, NULL, 1, 'Pending',10000);

-- Insert dữ liệu mẫu vào bảng AppConfig
INSERT INTO AppConfig ([Key], Value)
VALUES
    ('HomeDescription', 'This is description of Booking'),
    ('HomeKeyword', 'This is keyword of Booking'),
    ('HomeTitle', 'This is home page of Booking');


-- Insert dữ liệu mẫu vào bảng Feedback
--INSERT INTO Feedback (ParentId, PartyId, Score, Feedback)
--VALUES
--    ('99999999-9999-9999-9999-999999999999', 1, 4, 'Good experience'),
--    ('99999999-9999-9999-9999-999999999999', 2, 5, 'Excellent service');

-- Insert dữ liệu mẫu vào bảng AspNetRoles
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES
  ('11111111-1111-1111-1111-111111111111', 'admin', 'ADMIN', 'concurrency_stamp_2'),
  ('22222222-2222-2222-2222-222222222222', 'parent', 'PARENT', 'concurrency_stamp_3'),
  ('33333333-3333-3333-3333-333333333333', 'partyhost', 'PARTYHOST', 'concurrency_stamp_1');


  -- Insert dữ liệu mẫu vào bảng AspNetUserRoles

--INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
--VALUES
--  ('11111111-1111-1111-1111-111111111111', '33333333-3333-3333-3333-333333333333'),
--  ('22222222-2222-2222-2222-222222222222', '44444444-4444-4444-4444-444444444444');

-- Insert dữ liệu mẫu vào bảng AspNetUserLogins
--INSERT INTO [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId])
--VALUES 
--    ('login_provider_1', 'provider_key_1', '11111111-1111-1111-1111-111111111111'),
--    ('login_provider_2', 'provider_key_2', '22222222-2222-2222-2222-222222222222');

-- Insert dữ liệu mẫu vào bảng AspNetUserClaims
--INSERT INTO [dbo].[AspNetUserClaims] ([UserId], [ClaimType], [ClaimValue])
--VALUES 
--    ('11111111-1111-1111-1111-111111111111', 'claim_type_1', 'claim_value_1'),
--    ('22222222-2222-2222-2222-222222222222', 'claim_type_2', 'claim_value_2');



