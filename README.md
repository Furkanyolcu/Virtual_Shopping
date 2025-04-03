## Teknolojiler ve Araçlar

![C#](https://img.shields.io/badge/-CSharp-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/-dotNET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MSSQL](https://img.shields.io/badge/-MS%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![SMTP](https://img.shields.io/badge/-SMTP-FF9900?style=for-the-badge&logo=maildotru&logoColor=white)
![Visual Studio](https://img.shields.io/badge/-Visual%20Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)

![Ekran görüntüsü 2025-04-03 183815](https://github.com/user-attachments/assets/85d521e4-6f6e-48b3-aed3-fad0f9ffa1ba)
![Ekran görüntüsü 2025-04-03 183953](https://github.com/user-attachments/assets/9a847a4d-f0d2-48f7-8009-57ed0e3af932)
![Ekran görüntüsü 2025-04-03 184045](https://github.com/user-attachments/assets/9388078f-d934-49d0-b52f-bb4bcf23277e)
![Ekran görüntüsü 2025-04-03 184100](https://github.com/user-attachments/assets/5d20d5ad-459b-40b3-b563-0b220015a9a9)
![Ekran görüntüsü 2025-04-03 184126](https://github.com/user-attachments/assets/7b1253b4-0a2c-4931-91fd-e08cf92395e4)


## Database Setup

Aşağıdaki SQL komutlarını kullanarak veritabanınızı oluşturabilirsiniz.

```sql
CREATE DATABASE [ShoppingDB]
GO

USE [ShoppingDB]
GO

CREATE TABLE [Admins](
    [AdminID] INT IDENTITY(1,1) PRIMARY KEY,
    [AdminName] NVARCHAR(MAX) NOT NULL,
    [AdminEmail] NVARCHAR(MAX) NOT NULL,
    [AdminPassword] NVARCHAR(MAX) NOT NULL
);
GO

CREATE TABLE [Categories](
    [CategoryID] INT IDENTITY(1,1) PRIMARY KEY,
    [CategoryName] NVARCHAR(MAX) NOT NULL,
    [CategoryDescription] NVARCHAR(MAX) NOT NULL
);
GO

CREATE TABLE [Customers](
    [CustomerID] INT IDENTITY(1,1) PRIMARY KEY,
    [CustomerName] NVARCHAR(MAX) NOT NULL,
    [CustomerEmail] NVARCHAR(MAX) NOT NULL,
    [CustomerPassword] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT NOT NULL
);
GO

CREATE TABLE [Notifications](
    [NotificationID] INT IDENTITY(1,1) PRIMARY KEY,
    [NotificationMessage] NVARCHAR(MAX) NOT NULL,
    [CreateTime] DATETIME2 NULL,
    [IsRead] BIT NOT NULL
);
GO

CREATE TABLE [Products](
    [ProductID] INT IDENTITY(1,1) PRIMARY KEY,
    [ProductName] NVARCHAR(MAX) NOT NULL,
    [ProductDescription] NVARCHAR(MAX) NOT NULL,
    [ProductPrice] REAL NOT NULL,
    [ProductCategoryID] INT NOT NULL,
    [ProductImageURL] NVARCHAR(MAX) NOT NULL
);
GO

CREATE TABLE [ProductsSeller](
    [SellerProductsProductID] INT NOT NULL,
    [SellersSellerID] INT NOT NULL,
    PRIMARY KEY ([SellerProductsProductID], [SellersSellerID])
);
GO

CREATE TABLE [Sellers](
    [SellerID] INT IDENTITY(1,1) PRIMARY KEY,
    [SellerName] NVARCHAR(MAX) NOT NULL,
    [SellerEmail] NVARCHAR(MAX) NOT NULL,
    [SellerPassword] NVARCHAR(MAX) NOT NULL
);
GO

