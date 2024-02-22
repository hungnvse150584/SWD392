CREATE DATABASE BKPS
GO

use BKPS;
GO
CREATE TABLE Account (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
	UserName nvarchar(255),
    Password NVARCHAR(255),
	FullName nvarchar(255),
    Email NVARCHAR(255),
	PhoneNumber NVARCHAR(255),
    AvatarUrl nvarchar(500),
	CreatedDate Date,
    Address nvarchar(255),
	Role int,
	Status nvarchar(255),
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


