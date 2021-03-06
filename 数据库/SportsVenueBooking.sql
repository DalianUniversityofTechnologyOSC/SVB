USE [master]
GO
/****** Object:  Database [SportsVenueBooking]    Script Date: 2015/3/20 17:58:48 ******/
CREATE DATABASE [SportsVenueBooking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SportsVenueBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SportsVenueBooking.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SportsVenueBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SportsVenueBooking_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SportsVenueBooking] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SportsVenueBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SportsVenueBooking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET ARITHABORT OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SportsVenueBooking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SportsVenueBooking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SportsVenueBooking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SportsVenueBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SportsVenueBooking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET RECOVERY FULL 
GO
ALTER DATABASE [SportsVenueBooking] SET  MULTI_USER 
GO
ALTER DATABASE [SportsVenueBooking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SportsVenueBooking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SportsVenueBooking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SportsVenueBooking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SportsVenueBooking', N'ON'
GO
USE [SportsVenueBooking]
GO
/****** Object:  Table [dbo].[Durations]    Script Date: 2015/3/20 17:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Durations](
	[duration_Id] [int] IDENTITY(1,1) NOT NULL,
	[duration_Name] [varchar](10) NOT NULL,
	[duration_StartTime] [time](7) NOT NULL,
	[duration_EndTime] [time](7) NOT NULL,
	[duration_User] [bigint] NOT NULL,
	[duration_IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Durations] PRIMARY KEY CLUSTERED 
(
	[duration_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 2015/3/20 17:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[reservation_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[reservation_User] [bigint] NOT NULL,
	[reservation_StartTime] [datetime] NOT NULL,
	[reservation_EndTime] [datetime] NOT NULL,
	[reservation_IsBilling] [bit] NOT NULL,
	[reservation_Snooker] [bigint] NOT NULL,
	[reservation_IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[reservation_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Snookers]    Script Date: 2015/3/20 17:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snookers](
	[snooker_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[snooker_Space] [int] NOT NULL,
	[snooker_Number] [int] NOT NULL,
	[snooker_User] [bigint] NOT NULL,
	[snooker_Price] [float] NOT NULL,
	[snooker_IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Snookers] PRIMARY KEY CLUSTERED 
(
	[snooker_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Spaces]    Script Date: 2015/3/20 17:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Spaces](
	[space_Id] [int] IDENTITY(1,1) NOT NULL,
	[space_User] [bigint] NOT NULL,
	[space_Name] [varchar](20) NOT NULL,
	[space_IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Spaces] PRIMARY KEY CLUSTERED 
(
	[space_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2015/3/20 17:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[user_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_UserNumber] [varchar](10) NOT NULL,
	[user_Password] [varchar](50) NOT NULL,
	[user_Type] [int] NOT NULL,
	[user_Class] [varchar](20) NULL,
	[user_Name] [varchar](20) NOT NULL,
	[user_Status] [bit] NOT NULL,
	[user_Remark] [varchar](50) NULL,
	[user_IsDel] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [Users_UserIsDel]    Script Date: 2015/3/20 17:58:48 ******/
CREATE NONCLUSTERED INDEX [Users_UserIsDel] ON [dbo].[Users]
(
	[user_IsDel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Users_UserNumber]    Script Date: 2015/3/20 17:58:48 ******/
CREATE NONCLUSTERED INDEX [Users_UserNumber] ON [dbo].[Users]
(
	[user_UserNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Users_UserType]    Script Date: 2015/3/20 17:58:48 ******/
CREATE NONCLUSTERED INDEX [Users_UserType] ON [dbo].[Users]
(
	[user_Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Durations]  WITH CHECK ADD  CONSTRAINT [Durations_Users_UserId] FOREIGN KEY([duration_User])
REFERENCES [dbo].[Users] ([user_Id])
GO
ALTER TABLE [dbo].[Durations] CHECK CONSTRAINT [Durations_Users_UserId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [Reservations_Snookers_SnookerId] FOREIGN KEY([reservation_Snooker])
REFERENCES [dbo].[Snookers] ([snooker_Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [Reservations_Snookers_SnookerId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [Reservations_Users_UserId] FOREIGN KEY([reservation_User])
REFERENCES [dbo].[Users] ([user_Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [Reservations_Users_UserId]
GO
ALTER TABLE [dbo].[Snookers]  WITH CHECK ADD  CONSTRAINT [Snookers_Spaces_SpaceId] FOREIGN KEY([snooker_Space])
REFERENCES [dbo].[Spaces] ([space_Id])
GO
ALTER TABLE [dbo].[Snookers] CHECK CONSTRAINT [Snookers_Spaces_SpaceId]
GO
ALTER TABLE [dbo].[Snookers]  WITH CHECK ADD  CONSTRAINT [Snookers_Users_UserId] FOREIGN KEY([snooker_User])
REFERENCES [dbo].[Users] ([user_Id])
GO
ALTER TABLE [dbo].[Snookers] CHECK CONSTRAINT [Snookers_Users_UserId]
GO
ALTER TABLE [dbo].[Spaces]  WITH CHECK ADD  CONSTRAINT [Spaces_Users_UserId] FOREIGN KEY([space_User])
REFERENCES [dbo].[Users] ([user_Id])
GO
ALTER TABLE [dbo].[Spaces] CHECK CONSTRAINT [Spaces_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [SportsVenueBooking] SET  READ_WRITE 
GO
