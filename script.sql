CREATE DATABASE BPKS;
GO

USE BPKS;
GO

-- Bảng Parent
CREATE TABLE Parent (
    ParentId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(50),
    Email VARCHAR(50),
    FullName VARCHAR(50),
    Phonenumber VARCHAR(50),
    Address VARCHAR(100),
    UserUrl VARCHAR(500),
    CreatedDate DATE,
    Status VARCHAR(50)
);

-- Bảng PartyHost
CREATE TABLE PartyHost (
    PartyHostId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(50),
    Email VARCHAR(50),
    FullName VARCHAR(50),
    Phonenumber VARCHAR(50),
    Address VARCHAR(100),
    UserUrl VARCHAR(500),
    CreatedDate DATE,
    Status VARCHAR(50)
);

-- Bảng Admin
CREATE TABLE Admin (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(50),
    Email VARCHAR(50),
    FullName VARCHAR(50),
    Phonenumber VARCHAR(50),
    Address VARCHAR(100),
    UserUrl VARCHAR(500),
    CreatedDate DATE,
    Status VARCHAR(50)
);



-- Bảng Party
CREATE TABLE Party (
    PartyId INT IDENTITY(1,1) PRIMARY KEY,
    PartyName VARCHAR(500),
    Description VARCHAR(1000),
    PhoneContact VARCHAR(50),
    Place VARCHAR(100),
    Rate FLOAT,
    ThumbnailUrl VARCHAR(500),
    DayStart DATE,
    DayEnd DATE,
    CreatedDate DATE,
    PartyStatus VARCHAR(50)
);



-- Bảng Image
CREATE TABLE Image (
    ImageId INT IDENTITY(1,1) PRIMARY KEY,
    PartyHostId INT,
    ImageName VARCHAR(100),
    ImageUrl VARCHAR(500),
    ImageType VARCHAR(100),
    ImageStyle VARCHAR(100),
    ImageStatus VARCHAR(100)
);


-- Bảng Product
CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    PartyHostId INT,
    ProductName VARCHAR(100),
    ProductUrl VARCHAR(500),
    ProductType VARCHAR(100),
    Price decimal,
    ProductStatus VARCHAR(100)
);

-- Bảng Room
CREATE TABLE Room (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
    PartyHostId INT,
    RoomName VARCHAR(100),
    RoomType VARCHAR(100),
    RoomStyle VARCHAR(100),
    Price decimal,
    RoomStatus VARCHAR(100)
);

-- Bảng Payment
CREATE TABLE Payment (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(500),
    CardNo INT,
    ExpiryDate VARCHAR(100),
    CvvNo INT,
    Address VARCHAR(100),
    PaymentStatus VARCHAR(100)
);
-- Bảng ListParty
CREATE TABLE ListParty (
    ListPartyId INT IDENTITY(1,1) PRIMARY KEY,
    ParentId INT,
    PartyHostId INT,
    PartyId INT,
    ListPartyStatus VARCHAR(100),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId),
    FOREIGN KEY (PartyHostId) REFERENCES PartyHost(PartyHostId),
    FOREIGN KEY (PartyId) REFERENCES Party(PartyId)
);
-- Bảng PaymentMode
CREATE TABLE PaymentMode (
    PaymentModeId INT IDENTITY(1,1) PRIMARY KEY,
    ParentId INT,
    PaymentId INT,
    PaymentModeStatus VARCHAR(100),
    FOREIGN KEY (ParentId) REFERENCES Parent(ParentId),
    FOREIGN KEY (PaymentId) REFERENCES Payment(PaymentId)
);

-- Bảng ListProduct
CREATE TABLE ListProduct (
    ListProductId INT IDENTITY(1,1) PRIMARY KEY,
    PartyId INT,
    ProductId INT,
    Quantity INT,
    ListProductStatus VARCHAR(100),
    FOREIGN KEY (PartyId) REFERENCES Party(PartyId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

-- Bảng ListRoom
CREATE TABLE ListRoom (
    ListRoomId INT IDENTITY(1,1) PRIMARY KEY,
    PartyId INT,
    RoomId INT,
    RoomStatus VARCHAR(50),
    FOREIGN KEY (PartyId) REFERENCES Party(PartyId),
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId)
);
-- Bảng Category
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    PartyId INT,
    ImageId INT,
    CategoryStatus VARCHAR(100),
    FOREIGN KEY (PartyId) REFERENCES Party(PartyId),
    FOREIGN KEY (ImageId) REFERENCES Image(ImageId)
);