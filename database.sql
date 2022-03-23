USE [master]
GO
/****** Object:  Database [StockDataDB]    Script Date: 23-Mar-22 11:44:46 AM ******/
CREATE DATABASE [StockDataDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StockDataDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\StockDataDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StockDataDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\StockDataDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [StockDataDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StockDataDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StockDataDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StockDataDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StockDataDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StockDataDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StockDataDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StockDataDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [StockDataDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StockDataDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StockDataDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StockDataDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StockDataDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StockDataDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StockDataDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StockDataDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StockDataDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [StockDataDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StockDataDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StockDataDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StockDataDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StockDataDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StockDataDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [StockDataDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StockDataDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StockDataDB] SET  MULTI_USER 
GO
ALTER DATABASE [StockDataDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StockDataDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StockDataDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StockDataDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StockDataDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StockDataDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [StockDataDB] SET QUERY_STORE = OFF
GO
USE [StockDataDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23-Mar-22 11:44:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 23-Mar-22 11:44:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] NOT NULL,
	[TradeCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockPrices]    Script Date: 23-Mar-22 11:44:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[TradeCode] [nvarchar](max) NULL,
	[LastTradingPrice] [nvarchar](max) NULL,
	[High] [nvarchar](max) NULL,
	[Low] [nvarchar](max) NULL,
	[ClosePrice] [nvarchar](max) NULL,
	[YesterdayClosePrice] [nvarchar](max) NULL,
	[Change] [nvarchar](max) NULL,
	[Trade] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[Volume] [nvarchar](max) NULL,
	[Time] [nvarchar](max) NULL,
 CONSTRAINT [PK_StockPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_StockPrices_CompanyId]    Script Date: 23-Mar-22 11:44:46 AM ******/
CREATE NONCLUSTERED INDEX [IX_StockPrices_CompanyId] ON [dbo].[StockPrices]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StockPrices]  WITH CHECK ADD  CONSTRAINT [FK_StockPrices_Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StockPrices] CHECK CONSTRAINT [FK_StockPrices_Companies_CompanyId]
GO
USE [master]
GO
ALTER DATABASE [StockDataDB] SET  READ_WRITE 
GO
