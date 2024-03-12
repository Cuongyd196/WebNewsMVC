CREATE DATABASE WebNews;
GO
USE WebNews;

-- Tạo bảng Users trong schema "WebNews"
CREATE TABLE Users (
    User_ID INT IDENTITY(1,1) PRIMARY KEY,
    User_Name NVARCHAR(255),
    User_Phone NVARCHAR(20),
    User_Email NVARCHAR(255),
    User_UserName NVARCHAR(50),
    User_Password NVARCHAR(255),
    User_Role NVARCHAR(50)
);

-- Tạo bảng Categorys trong schema "WebNews"
CREATE TABLE Categorys (
    Category_ID INT IDENTITY(1,1) PRIMARY KEY,
    Category_Name NVARCHAR(255),
    Category_Note NVARCHAR(255),
    Category_Status INT
);

-- Tạo bảng News trong schema "WebNews"
CREATE TABLE News (
    New_ID INT IDENTITY(1,1) PRIMARY KEY,
    Category_ID INT,
    New_Title NVARCHAR(255),
    New_Summary NVARCHAR(MAX),
    New_img NVARCHAR(255),
    New_Content NVARCHAR(MAX),
    New_Date NVARCHAR(50),
    New_Status INT,
    New_User_Name NVARCHAR(255),
    FOREIGN KEY (Category_ID) REFERENCES Categorys(Category_ID)
);
