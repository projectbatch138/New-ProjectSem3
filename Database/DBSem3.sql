USE [ProjectSem3]
GO
/****** Object:  Database [DBProjectSem3]    Script Date: 3/4/2019 10:24:38 PM ******/
CREATE DATABASE [DBProjectSem3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBProjectSem3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DBProjectSem3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBProjectSem3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DBProjectSem3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DBProjectSem3] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectSem3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBProjectSem3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBProjectSem3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBProjectSem3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBProjectSem3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBProjectSem3] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBProjectSem3] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBProjectSem3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBProjectSem3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBProjectSem3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBProjectSem3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBProjectSem3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBProjectSem3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBProjectSem3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBProjectSem3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBProjectSem3] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBProjectSem3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBProjectSem3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBProjectSem3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBProjectSem3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBProjectSem3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBProjectSem3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBProjectSem3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBProjectSem3] SET RECOVERY FULL 
GO
ALTER DATABASE [DBProjectSem3] SET  MULTI_USER 
GO
ALTER DATABASE [DBProjectSem3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBProjectSem3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBProjectSem3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBProjectSem3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBProjectSem3] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBProjectSem3', N'ON'
GO
ALTER DATABASE [DBProjectSem3] SET QUERY_STORE = OFF
GO
USE [DBProjectSem3]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DBProjectSem3]
GO
/****** Object:  Table [dbo].[Airlines]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airlines](
	[AirlineId] [int] IDENTITY(1,1) NOT NULL,
	[AirlineName] [varchar](50) NOT NULL,
	[LogoImage] [varchar](150) NULL,
	[Slogan] [varchar](250) NULL,
 CONSTRAINT [PK_Airlines] PRIMARY KEY CLUSTERED 
(
	[AirlineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Airports]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airports](
	[AirportId] [int] IDENTITY(1,1) NOT NULL,
	[AirportName] [nvarchar](50) NOT NULL,
	[AirportIATACode] [varchar](6) NULL,
	[LocationId] [int] NOT NULL,
 CONSTRAINT [PK_Airports] PRIMARY KEY CLUSTERED 
(
	[AirportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Booking_Ticket]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking_Ticket](
	[Booking_TicketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FlightId] [int] NOT NULL,
	[PriceId] [int] NOT NULL,
	[DiscountId] [int] NULL,
	[DiscountBySeatClassId] [int] NULL,
	[DiscountBySkymiles] [int] NULL,
	[DiscountByEarly] [int] NULL,
	[SeatDetailByFlightId] [int] NULL,
	[ReservationModId] [int] NOT NULL,
 CONSTRAINT [PK_Booking_Ticket] PRIMARY KEY CLUSTERED 
(
	[Booking_TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiscountDetail]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountDetail](
	[DiscountDetailId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[TypeDiscountId] [int] NOT NULL,
	[CreatedDate] [date] NULL,
	[modifyDate] [date] NULL,
 CONSTRAINT [PK_DiscountDetail] PRIMARY KEY CLUSTERED 
(
	[DiscountDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Flights]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flights](
	[Flightid] [int] IDENTITY(1,1) NOT NULL,
	[RouterId] [int] NOT NULL,
	[Dept_Time] [datetime] NOT NULL,
	[Arr_Time] [datetime] NOT NULL,
	[Status] [int] NULL,
	[PlaneId] [int] NOT NULL,
 CONSTRAINT [PK_Flights_1] PRIMARY KEY CLUSTERED 
(
	[Flightid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Locations]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NULL,
	[CountryIATACode] [varchar](3) NULL,
	[CityIATACode] [varchar](6) NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Planes]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planes](
	[PlaneId] [int] IDENTITY(1,1) NOT NULL,
	[PlaneName] [varchar](50) NOT NULL,
	[AirlineId] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[CodeIATAPlane] [varchar](3) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Planes] PRIMARY KEY CLUSTERED 
(
	[PlaneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prices]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prices](
	[PriceId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[SeatClassId] [int] NOT NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Prices] PRIMARY KEY CLUSTERED 
(
	[PriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuantitySeatClass]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuantitySeatClass](
	[QuantitySeatClassId] [int] NOT NULL,
	[PlaneId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SeatClassId] [int] NOT NULL,
 CONSTRAINT [PK_QuantitySeatClass] PRIMARY KEY CLUSTERED 
(
	[QuantitySeatClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReservationMod]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationMod](
	[ReservationModId] [int] IDENTITY(1,1) NOT NULL,
	[ReservationModName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ReservationMod] PRIMARY KEY CLUSTERED 
(
	[ReservationModId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Routers]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routers](
	[RouterId] [int] IDENTITY(1,1) NOT NULL,
	[Depart] [int] NOT NULL,
	[Arrival] [int] NOT NULL,
	[Distances] [int] NULL,
 CONSTRAINT [PK_Routers] PRIMARY KEY CLUSTERED 
(
	[RouterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeatClasses]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatClasses](
	[SeatClassId] [int] IDENTITY(1,1) NOT NULL,
	[SeatClassName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_SeatClasses] PRIMARY KEY CLUSTERED 
(
	[SeatClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeatDetailByFlight]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatDetailByFlight](
	[SeatDetailByFlightId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[SeatNumberId] [int] NOT NULL,
	[SeatStatus] [bit] NOT NULL,
 CONSTRAINT [PK_SeatDetailByFlight] PRIMARY KEY CLUSTERED 
(
	[SeatDetailByFlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeatNumbers]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatNumbers](
	[SeatNumberId] [int] IDENTITY(1,1) NOT NULL,
	[SeatNumber] [int] NOT NULL,
	[SeatClassId] [int] NOT NULL,
	[PlaneId] [int] NOT NULL,
 CONSTRAINT [PK_SeatNumbers] PRIMARY KEY CLUSTERED 
(
	[SeatNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeDiscount]    Script Date: 3/4/2019 10:24:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeDiscount](
	[TypeDiscountId] [int] IDENTITY(1,1) NOT NULL,
	[TypeDiscountName] [varchar](150) NOT NULL,
	[Discount] [int] NULL,
 CONSTRAINT [PK_TypeDiscount] PRIMARY KEY CLUSTERED 
(
	[TypeDiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Airports]  WITH CHECK ADD  CONSTRAINT [FK_Airports_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[Airports] CHECK CONSTRAINT [FK_Airports_Locations]
GO
ALTER TABLE [dbo].[Booking_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Ticket_Flights] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flightid])
GO
ALTER TABLE [dbo].[Booking_Ticket] CHECK CONSTRAINT [FK_Booking_Ticket_Flights]
GO
ALTER TABLE [dbo].[Booking_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Ticket_Flights1] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flightid])
GO
ALTER TABLE [dbo].[Booking_Ticket] CHECK CONSTRAINT [FK_Booking_Ticket_Flights1]
GO
ALTER TABLE [dbo].[Booking_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Ticket_Prices] FOREIGN KEY([PriceId])
REFERENCES [dbo].[Prices] ([PriceId])
GO
ALTER TABLE [dbo].[Booking_Ticket] CHECK CONSTRAINT [FK_Booking_Ticket_Prices]
GO
ALTER TABLE [dbo].[Booking_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Ticket_ReservationMod] FOREIGN KEY([ReservationModId])
REFERENCES [dbo].[ReservationMod] ([ReservationModId])
GO
ALTER TABLE [dbo].[Booking_Ticket] CHECK CONSTRAINT [FK_Booking_Ticket_ReservationMod]
GO
ALTER TABLE [dbo].[Booking_Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Ticket_SeatDetailByFlight] FOREIGN KEY([SeatDetailByFlightId])
REFERENCES [dbo].[SeatDetailByFlight] ([SeatDetailByFlightId])
GO
ALTER TABLE [dbo].[Booking_Ticket] CHECK CONSTRAINT [FK_Booking_Ticket_SeatDetailByFlight]
GO
ALTER TABLE [dbo].[DiscountDetail]  WITH CHECK ADD  CONSTRAINT [FK_DiscountDetail_Flights] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flightid])
GO
ALTER TABLE [dbo].[DiscountDetail] CHECK CONSTRAINT [FK_DiscountDetail_Flights]
GO
ALTER TABLE [dbo].[DiscountDetail]  WITH CHECK ADD  CONSTRAINT [FK_DiscountDetail_TypeDiscount] FOREIGN KEY([TypeDiscountId])
REFERENCES [dbo].[TypeDiscount] ([TypeDiscountId])
GO
ALTER TABLE [dbo].[DiscountDetail] CHECK CONSTRAINT [FK_DiscountDetail_TypeDiscount]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Flights_Planes] FOREIGN KEY([PlaneId])
REFERENCES [dbo].[Planes] ([PlaneId])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Flights_Planes]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Flights_Routers] FOREIGN KEY([RouterId])
REFERENCES [dbo].[Routers] ([RouterId])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Flights_Routers]
GO
ALTER TABLE [dbo].[Planes]  WITH CHECK ADD  CONSTRAINT [FK_Planes_Airlines] FOREIGN KEY([AirlineId])
REFERENCES [dbo].[Airlines] ([AirlineId])
GO
ALTER TABLE [dbo].[Planes] CHECK CONSTRAINT [FK_Planes_Airlines]
GO
ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Flights] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flightid])
GO
ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Flights]
GO
ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_SeatClasses] FOREIGN KEY([SeatClassId])
REFERENCES [dbo].[SeatClasses] ([SeatClassId])
GO
ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_SeatClasses]
GO
ALTER TABLE [dbo].[QuantitySeatClass]  WITH CHECK ADD  CONSTRAINT [FK_QuantitySeatClass_Planes] FOREIGN KEY([PlaneId])
REFERENCES [dbo].[Planes] ([PlaneId])
GO
ALTER TABLE [dbo].[QuantitySeatClass] CHECK CONSTRAINT [FK_QuantitySeatClass_Planes]
GO
ALTER TABLE [dbo].[QuantitySeatClass]  WITH CHECK ADD  CONSTRAINT [FK_QuantitySeatClass_SeatClasses] FOREIGN KEY([SeatClassId])
REFERENCES [dbo].[SeatClasses] ([SeatClassId])
GO
ALTER TABLE [dbo].[QuantitySeatClass] CHECK CONSTRAINT [FK_QuantitySeatClass_SeatClasses]
GO
ALTER TABLE [dbo].[Routers]  WITH CHECK ADD  CONSTRAINT [FK_Routers_Airports] FOREIGN KEY([Depart])
REFERENCES [dbo].[Airports] ([AirportId])
GO
ALTER TABLE [dbo].[Routers] CHECK CONSTRAINT [FK_Routers_Airports]
GO
ALTER TABLE [dbo].[Routers]  WITH CHECK ADD  CONSTRAINT [FK_Routers_Airports1] FOREIGN KEY([Arrival])
REFERENCES [dbo].[Airports] ([AirportId])
GO
ALTER TABLE [dbo].[Routers] CHECK CONSTRAINT [FK_Routers_Airports1]
GO
ALTER TABLE [dbo].[SeatDetailByFlight]  WITH CHECK ADD  CONSTRAINT [FK_SeatDetailByFlight_Flights] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flights] ([Flightid])
GO
ALTER TABLE [dbo].[SeatDetailByFlight] CHECK CONSTRAINT [FK_SeatDetailByFlight_Flights]
GO
ALTER TABLE [dbo].[SeatDetailByFlight]  WITH CHECK ADD  CONSTRAINT [FK_SeatDetailByFlight_SeatNumbers] FOREIGN KEY([SeatNumberId])
REFERENCES [dbo].[SeatNumbers] ([SeatNumberId])
GO
ALTER TABLE [dbo].[SeatDetailByFlight] CHECK CONSTRAINT [FK_SeatDetailByFlight_SeatNumbers]
GO
ALTER TABLE [dbo].[SeatNumbers]  WITH CHECK ADD  CONSTRAINT [FK_SeatNumbers_Planes] FOREIGN KEY([PlaneId])
REFERENCES [dbo].[Planes] ([PlaneId])
GO
ALTER TABLE [dbo].[SeatNumbers] CHECK CONSTRAINT [FK_SeatNumbers_Planes]
GO
ALTER TABLE [dbo].[SeatNumbers]  WITH CHECK ADD  CONSTRAINT [FK_SeatNumbers_SeatClasses] FOREIGN KEY([SeatClassId])
REFERENCES [dbo].[SeatClasses] ([SeatClassId])
GO
ALTER TABLE [dbo].[SeatNumbers] CHECK CONSTRAINT [FK_SeatNumbers_SeatClasses]
GO
USE [master]
GO
ALTER DATABASE [DBProjectSem3] SET  READ_WRITE 
GO
