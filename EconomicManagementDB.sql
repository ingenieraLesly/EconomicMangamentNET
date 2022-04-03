USE master
GO

DROP DATABASE IF EXISTS [EconomicManagementDB]
GO

CREATE DATABASE [EconomicManagementDB]
GO

USE [EconomicManagementDB]
GO

-- StandarEmail será el email en mayusculas
CREATE TABLE [Users](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[StandarEmail] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
)
GO

-- Estas operaciones pueden ser: ingresos o gastos
CREATE TABLE [OperationTypes](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
)
GO

-- ejemplo: bancarias, prestamos, efectivo, credito, etc.
CREATE TABLE [AccountTypes](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[OrderAccount] [int] NOT NULL,
	CONSTRAINT [FK_AccountTypesUsers] FOREIGN KEY (UserId) REFERENCES Users(Id)
)
GO

CREATE TABLE [Accounts](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](1000) NULL,
    CONSTRAINT [FK_AccountType] FOREIGN KEY (AccountTypeId) REFERENCES AccountTypes(Id)
)
GO

-- Cada usuario puede tener sus propias categorias para clasificar sus transacciones.
CREATE TABLE Categories(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OperationTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
    CONSTRAINT [FK_CategoriesOperations] FOREIGN KEY (OperationTypeId) REFERENCES OperationTypes(Id),
) 
GO

CREATE TABLE [Transactions](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[UserId] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[OperationTypeId] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[AccountId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
    CONSTRAINT [FK_TransactionsUsers] FOREIGN KEY (UserId) REFERENCES Users(Id),
	CONSTRAINT [FK_TransactiosOperationType] FOREIGN KEY (OperationTypeId) REFERENCES OperationTypes(Id),
	CONSTRAINT [FK_TransactionsAccount] FOREIGN KEY (AccountId) REFERENCES Accounts(Id) ON DELETE CASCADE,
	CONSTRAINT [FK_TransactionsCategories] FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)
GO

CREATE PROCEDURE SP_AccountType_Insert
	@Name nvarchar(50),
	@UserId int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Order int;
	SELECT @Order = COALESCE(MAX(OrderAccount), 0)+1
	FROM AccountTypes
	WHERE UserId = @UserId

	INSERT INTO AccountTypes(Name, UserId, OrderAccount)
	VALUES (@Name, @UserId, @Order);

	SELECT SCOPE_IDENTITY();
END
GO

INSERT INTO OperationTypes VALUES
('Ingreso'),
('Gasto')