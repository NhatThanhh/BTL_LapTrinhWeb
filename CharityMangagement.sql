USE [master]
GO
/****** Object:  Database [CharityManagement]    Script Date: 12/12/2024 8:26:39 AM ******/
CREATE DATABASE [CharityManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CharityManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS02\MSSQL\DATA\CharityManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CharityManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS02\MSSQL\DATA\CharityManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CharityManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CharityManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CharityManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CharityManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CharityManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CharityManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CharityManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [CharityManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CharityManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CharityManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CharityManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CharityManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CharityManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CharityManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CharityManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CharityManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CharityManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CharityManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CharityManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CharityManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CharityManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CharityManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CharityManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CharityManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CharityManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CharityManagement] SET  MULTI_USER 
GO
ALTER DATABASE [CharityManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CharityManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CharityManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CharityManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CharityManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CharityManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CharityManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [CharityManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CharityManagement]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[UserAdmin] [varchar](50) NOT NULL,
	[PassAdmin] [varchar](10) NULL,
	[NameAdmin] [nvarchar](100) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[UserAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHIENDICH]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHIENDICH](
	[MaCD] [int] IDENTITY(1,1) NOT NULL,
	[TenCD] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[MaQL] [int] NOT NULL,
	[AnhBia] [varchar](50) NOT NULL,
	[TongQuy] [decimal](18, 0) NULL,
	[NgayTao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETQUYENGOP]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETQUYENGOP](
	[MaQG] [int] NOT NULL,
	[MaCD] [int] NOT NULL,
	[SoTienQG] [decimal](18, 0) NULL,
 CONSTRAINT [PK_CTQuyenGop] PRIMARY KEY CLUSTERED 
(
	[MaQG] ASC,
	[MaCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LIENHE]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LIENHE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenLH] [nvarchar](60) NULL,
	[SDTLH] [varchar](20) NULL,
	[EmailLH] [nvarchar](100) NULL,
	[NgayGui] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NGUOIDUNG]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NGUOIDUNG](
	[MaND] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[TaiKhoan] [varchar](50) NULL,
	[MatKhau] [varchar](20) NOT NULL,
	[Email] [varchar](100) NULL,
	[SDT] [varchar](20) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[NgaySinh] [datetime] NULL,
	[NgayDangKi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUANLY]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUANLY](
	[MaQL] [int] IDENTITY(1,1) NOT NULL,
	[TenQL] [nvarchar](100) NOT NULL,
	[EmailQL] [nvarchar](100) NULL,
	[SDTQL] [varchar](20) NULL,
	[TieuSu] [nvarchar](max) NULL,
	[AnhQL] [varchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaQL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUYENGOP]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUYENGOP](
	[MaQG] [int] IDENTITY(1,1) NOT NULL,
	[MaND] [int] NULL,
	[SoTien] [decimal](18, 0) NULL,
	[NgayQuyenGop] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaQG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[THUVIEN]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THUVIEN](
	[MaCD] [int] NOT NULL,
	[MaHA] [int] IDENTITY(1,1) NOT NULL,
	[Anh] [varchar](50) NOT NULL,
	[NgayDang] [datetime] NULL,
 CONSTRAINT [pk_THUVIEN] PRIMARY KEY CLUSTERED 
(
	[MaCD] ASC,
	[MaHA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ADMIN] ([UserAdmin], [PassAdmin], [NameAdmin]) VALUES (N'admin', N'123456', N'Nguyễn Văn A')
INSERT [dbo].[ADMIN] ([UserAdmin], [PassAdmin], [NameAdmin]) VALUES (N'user', N'13579', N'Nguyễn Văn B')
GO
SET IDENTITY_INSERT [dbo].[CHIENDICH] ON 

INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (1, N'Mái ấm tình thương', N'Xây nhà tình thương cho các gia đình khó khăn', 3, N'maiamtinhthuong.jpg', CAST(103000000 AS Decimal(18, 0)), CAST(N'2024-11-21T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (3, N'Thắp sáng ước mơ', N'Cung cấp học bổng cho học sinh nghèo hiếu học', 4, N'thapsanguocmo1.png', CAST(250050000 AS Decimal(18, 0)), CAST(N'2024-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (4, N'Cùng em đến trường', N'Xây dựng trường học cho trẻ em ở vùng sâu vùng xa, vùng khó khăn', 5, N'cungemdentruong.png', CAST(161100000 AS Decimal(18, 0)), CAST(N'2024-11-19T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (5, N'Bữa ăn ấm áp', N'Cung cấp các bữa ăn dinh dưỡng cho trẻ em nghèo', 6, N'nauanchoem.jpg', CAST(100840000 AS Decimal(18, 0)), CAST(N'2024-11-18T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (11, N'Từ trái tim đến trái tim', N'<p><strong>"Từ Trái Tim Đến Trái Tim"</strong> là một quỹ từ thiện đặc biệt với sứ mệnh cao cả: mang lại cuộc sống mới cho các trẻ em nghèo mắc bệnh tim. Được thành lập bởi những trái tim nhân ái, quỹ này tập trung vào việc quyên góp và hỗ trợ chi phí phẫu thuật tim cho các em nhỏ không có điều kiện tài chính.</p>', 4, N'tutraitimdentraitim.jpg', CAST(120000000 AS Decimal(18, 0)), CAST(N'2024-11-29T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (16, N'màu xanh tuổi trẻ', N'<p>Chiến dịch <i><strong>Màu Xanh Tuổi Trẻ</strong></i> là một sáng kiến tuyệt vời được khởi xướng với mục đích đem lại sự tươi mới và hy vọng cho cộng đồng. Hướng tới việc bảo vệ môi trường và nâng cao ý thức về lối sống xanh, chiến dịch kêu gọi sự tham gia của các bạn trẻ trong việc trồng cây, làm sạch bãi biển, công viên và các khu vực công cộng. Không chỉ tạo ra những thay đổi tích cực cho môi trường, "Màu Xanh Tuổi Trẻ" còn là cơ hội để các bạn trẻ kết nối, chia sẻ đam mê và truyền cảm hứng cho nhau trong hành trình xây dựng một tương lai bền vững và xanh sạch. Hãy cùng tham gia và góp phần vào sứ mệnh cao đẹp này, vì một môi trường sống trong lành hơn cho chúng ta và các thế hệ mai sau!</p>', 3, N'IMG_8866.jpg', CAST(5000 AS Decimal(18, 0)), CAST(N'2024-12-01T00:00:00.000' AS DateTime))
INSERT [dbo].[CHIENDICH] ([MaCD], [TenCD], [MoTa], [MaQL], [AnhBia], [TongQuy], [NgayTao]) VALUES (17, N'trái tim yêu thương', N'<p>Chiến dịch <i><strong>"Màu Xanh Tuổi Trẻ"</strong></i> là một sáng kiến tuyệt vời được khởi xướng với mục đích đem lại sự tươi mới và hy vọng cho cộng đồng. Hướng tới việc bảo vệ môi trường và nâng cao ý thức về lối sống xanh, chiến dịch kêu gọi sự tham gia của các bạn trẻ trong việc trồng cây, làm sạch bãi biển, công viên và các khu vực công cộng. Không chỉ tạo ra những thay đổi tích cực cho môi trường, "Màu Xanh Tuổi Trẻ" còn là cơ hội để các bạn trẻ kết nối, chia sẻ đam mê và truyền cảm hứng cho nhau trong hành trình xây dựng một tương lai bền vững và xanh sạch. Hãy cùng tham gia và góp phần vào sứ mệnh cao đẹp này, vì một môi trường sống trong lành hơn cho chúng ta và các thế hệ mai sau!</p>', 5, N'_DSC0497(1).jpg', CAST(10000000 AS Decimal(18, 0)), CAST(N'2023-06-02T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[CHIENDICH] OFF
GO
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (1, 1, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (2, 1, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (2, 4, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (3, 4, CAST(15000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (4, 3, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (5, 4, CAST(25000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (6, 4, CAST(60000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (7, 1, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (8, 1, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (9, 1, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (10, 3, CAST(50000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (11, 4, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (12, 5, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (13, 5, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (14, 1, CAST(4000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (15, 1, CAST(10000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (16, 1, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (16, 4, CAST(5000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (17, 1, CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (17, 4, CAST(5000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (18, 1, CAST(30000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (19, 4, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (20, 4, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (24, 1, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (24, 4, CAST(200000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (28, 1, CAST(460000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (29, 5, CAST(300000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (30, 3, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (31, 4, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (32, 1, CAST(2000000 AS Decimal(18, 0)))
INSERT [dbo].[CHITIETQUYENGOP] ([MaQG], [MaCD], [SoTienQG]) VALUES (33, 4, CAST(50000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[LIENHE] ON 

INSERT [dbo].[LIENHE] ([ID], [TenLH], [SDTLH], [EmailLH], [NgayGui]) VALUES (9, N'Thanh', N'0345677894', N'thaoxinh@gmail.com', CAST(N'2024-12-11T20:17:52.027' AS DateTime))
INSERT [dbo].[LIENHE] ([ID], [TenLH], [SDTLH], [EmailLH], [NgayGui]) VALUES (10, N'Thảo', N'0354894561', N'thuyoi@gmail.com', CAST(N'2024-12-11T20:17:56.113' AS DateTime))
INSERT [dbo].[LIENHE] ([ID], [TenLH], [SDTLH], [EmailLH], [NgayGui]) VALUES (11, N'Hiển', N'0345677894', N'thaoxinh@gmail.com', CAST(N'2024-12-11T20:18:01.067' AS DateTime))
INSERT [dbo].[LIENHE] ([ID], [TenLH], [SDTLH], [EmailLH], [NgayGui]) VALUES (12, N'Hiển', N'0345677894', N'thuyoi@gmail.com', CAST(N'2024-12-11T20:18:06.610' AS DateTime))
SET IDENTITY_INSERT [dbo].[LIENHE] OFF
GO
SET IDENTITY_INSERT [dbo].[NGUOIDUNG] ON 

INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (1, N'Nguyễn Nhật Thanh', N'Thanhnguyen', N'123456', N'thanhblink@gmail.com', N'0376704109', N'32/13, Võ Văn Hát, Long Trường', CAST(N'2004-08-20T00:00:00.000' AS DateTime), CAST(N'2023-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (2, N'Nguyễn Văn Linh', N'vanlinh', N'02468', N'linh@gmail.com', N'0376704107', N'32/15, Võ Văn Hát, Long Trường', CAST(N'1999-01-25T00:00:00.000' AS DateTime), CAST(N'2023-10-25T00:00:00.000' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (3, N'Nguyễn Ngọc Hải', N'hainguyen123', N'123456', N'hai@gmail.com', N'0983587789', N'42 Lã Xuân Oai, Thủ Đức', CAST(N'1998-11-23T00:00:00.000' AS DateTime), CAST(N'2023-11-28T00:00:00.000' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (6, N'Mai Thanh Hiển', N'thanhhienne', N'123456', N'hien@gmail.com', N'0123456788', N'Đặng Văn Bi, Thủ Đức', CAST(N'1999-10-04T00:00:00.000' AS DateTime), CAST(N'2023-12-06T13:27:48.423' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (9, N'Đinh Kim Yến Nhi', N'yennhi', N'123456', N'yennhi@gmail.com', N'0376704165', N'32/24, Võ Văn Hát, Long Trường', CAST(N'2003-10-07T00:00:00.000' AS DateTime), CAST(N'2023-12-06T22:46:06.043' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (10, N'Nguyễn Lê Gia Mỹ', N'giamy', N'123456', N'giamy@gmail.com', N'0376704235', N'43 Lã Xuân Oai, Thủ Đức', CAST(N'2001-06-02T00:00:00.000' AS DateTime), CAST(N'2023-12-06T22:48:43.600' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (11, N'Trần Thị Hồng', N'hongxe', N'123456', N'hongxe@gmial.com', N'0123456769', N'Đặng Văn Bi, Thủ Đức', CAST(N'1998-06-04T00:00:00.000' AS DateTime), CAST(N'2023-12-06T23:50:45.820' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (12, N'mệt', N'metghe', N'123456', N'met@gmail.com', N'0376704107', N'32/13, Võ Văn Hát, Long Trường', CAST(N'2004-06-07T00:00:00.000' AS DateTime), CAST(N'2024-12-07T14:41:08.450' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (13, N'Thảo', N'thaoxinh', N'2468', N'thao@gmail.com', N'0123456789', N'Lã Xuân Oai', CAST(N'2004-10-10T00:00:00.000' AS DateTime), CAST(N'2024-12-07T14:50:59.497' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (14, N'Trần Thị Hà', N'hatran', N'123456', N'ha@gmail.com', N'0376704109', N'32/13, Võ Văn Hát, Long Trường', CAST(N'2002-11-06T00:00:00.000' AS DateTime), CAST(N'2024-12-07T14:52:38.897' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (16, N'Nguyễn Thu Thảo', N'thuthao22', N'123456', N'thuthao@gmail.com', N'0376704109', N'32/13, Võ Văn Hát, Long Trường', CAST(N'2003-10-06T00:00:00.000' AS DateTime), CAST(N'2024-12-08T20:49:56.773' AS DateTime))
INSERT [dbo].[NGUOIDUNG] ([MaND], [HoTen], [TaiKhoan], [MatKhau], [Email], [SDT], [DiaChi], [NgaySinh], [NgayDangKi]) VALUES (17, N'Mai Chí Công', N'chicong', N'cong123', N'congcong@gmail.com', N'0376704107', N'42 Lã Xuân Oai, Thủ Đức', CAST(N'2003-06-02T00:00:00.000' AS DateTime), CAST(N'2024-12-11T15:30:21.937' AS DateTime))
SET IDENTITY_INSERT [dbo].[NGUOIDUNG] OFF
GO
SET IDENTITY_INSERT [dbo].[QUANLY] ON 

INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (3, N'Phạm Hải', N'phamhai@gmail.com', N'0376704102', N'Phạm Hải, một người đàn ông 30 tuổi với trái tim nhiệt huyết và đam mê giúp đỡ cộng đồng, hiện là leader của chiến dịch từ thiện "Charity Life". Với nền tảng học vấn vững chắc và nhiều năm kinh nghiệm làm việc trong lĩnh vực phi lợi nhuận, Hải đã dành nhiều thời gian và công sức để mang lại sự thay đổi tích cực cho những người có hoàn cảnh khó khăn.
Là người lãnh đạo chiến dịch, Hải không chỉ dẫn dắt đội ngũ tình nguyện viên bằng tầm nhìn chiến lược mà còn bằng lòng nhân ái và sự cam kết không ngừng nghỉ. Anh luôn tin rằng mỗi hành động nhỏ đều có thể góp phần tạo nên sự khác biệt lớn, và anh không ngần ngại dấn thân vào những công việc khó khăn để đem lại niềm hy vọng cho cộng đồng.
Dưới sự dẫn dắt của Hải, "Charity Life" đã tổ chức thành công nhiều hoạt động từ thiện như cung cấp thực phẩm cho các gia đình nghèo, hỗ trợ giáo dục cho trẻ em khó khăn, và xây dựng những chương trình phát triển bền vững. Sự nhiệt huyết và tinh thần cống hiến của Hải đã truyền cảm hứng cho nhiều người khác tham gia vào sứ mệnh chung của tổ chức, cùng nhau xây dựng một tương lai tốt đẹp hơn cho tất cả mọi người.
', N'ql1.png', N'Người sáng lập')
INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (4, N'Nguyễn Thị Hoa', N'hoanguyen@gmail.com', N'0983587792', N'Nguyễn Thị Hoa là một thành viên tích cực và tận tâm của tổ chức từ thiện "Charity Life". Ở độ tuổi 30, Hoa đã dành nhiều năm để cống hiến cho cộng đồng và giúp đỡ những người có hoàn cảnh khó khăn. Với trái tim nhân ái và lòng nhiệt huyết, Hoa đã góp phần quan trọng vào thành công của nhiều dự án từ thiện do tổ chức thực hiện.

Hoa luôn tin rằng mỗi hành động nhỏ đều có thể mang lại sự thay đổi lớn. Cô tham gia vào nhiều hoạt động từ việc phân phát thực phẩm, quần áo cho người nghèo, đến tổ chức các chương trình giáo dục và hỗ trợ y tế cho trẻ em và người cao tuổi. Khả năng giao tiếp xuất sắc và tinh thần đồng đội của Hoa đã giúp cô kết nối và phối hợp hiệu quả với các tình nguyện viên khác trong tổ chức.

Với sự nhiệt tình và cống hiến của mình, Nguyễn Thị Hoa không chỉ là một thành viên xuất sắc của "Charity Life" mà còn là nguồn cảm hứng cho nhiều người khác tham gia vào các hoạt động từ thiện. Cô luôn mong muốn đem lại niềm vui và hy vọng cho những người xung quanh, góp phần xây dựng một cộng đồng vững mạnh và nhân ái hơn.', N'ql2.png', N'Thành viên')
INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (5, N'Trần Ngọc Bích', N'bich123@gmail.com', N'0376587923', N'Trần Ngọc Bích là một thành viên năng động và nhiệt huyết của tổ chức từ thiện "Charity Life". Ở độ tuổi 30, Bích đã dành nhiều năm để tham gia và cống hiến cho các hoạt động thiện nguyện. Với tấm lòng nhân ái và sự cam kết sâu sắc với cộng đồng, Bích luôn mang đến niềm vui và hy vọng cho những người đang gặp khó khăn.

Trong tổ chức, Bích nổi bật với khả năng tổ chức và quản lý các dự án từ thiện một cách hiệu quả. Cô tham gia vào việc lên kế hoạch, điều phối và thực hiện nhiều chương trình hỗ trợ như cung cấp thực phẩm, chăm sóc sức khỏe, và giáo dục cho trẻ em nghèo. Sự nhạy bén và tinh thần trách nhiệm của Bích đã giúp cô đạt được nhiều thành tựu đáng kể trong công việc.

Không chỉ là một tình nguyện viên xuất sắc, Trần Ngọc Bích còn là một người truyền cảm hứng mạnh mẽ, khích lệ và dẫn dắt các tình nguyện viên khác trong tổ chức. Cô luôn tin rằng mỗi hành động thiện nguyện, dù nhỏ bé, cũng có thể tạo nên sự thay đổi lớn và mang lại những giá trị tốt đẹp cho xã hội.', N'ql3.png', N'Thành viên')
INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (6, N'Nguyễn Ngọc Sơn', N'ngson@gmail.com', N'0987654123', N'Nguyễn Ngọc Sơn, ở độ tuổi 30, là một thành viên cốt lõi của tổ chức từ thiện "Charity Life". Với lòng nhiệt huyết và đam mê giúp đỡ cộng đồng, Sơn đã dành nhiều năm để tham gia và cống hiến cho các hoạt động thiện nguyện. Anh luôn tin rằng mỗi hành động thiện nguyện dù nhỏ cũng có thể tạo nên sự thay đổi lớn cho xã hội.

Trong vai trò của mình tại "Charity Life", Sơn tham gia vào nhiều dự án từ cung cấp thực phẩm và chỗ ở cho những người có hoàn cảnh khó khăn, đến tổ chức các chương trình giáo dục và hỗ trợ y tế. Khả năng lãnh đạo và tinh thần trách nhiệm của Sơn đã giúp anh đảm nhận nhiều vai trò quan trọng và đạt được nhiều thành tựu trong công việc.

Sơn không chỉ là một tình nguyện viên xuất sắc, mà còn là một người truyền cảm hứng mạnh mẽ cho những người xung quanh. Anh luôn khích lệ và dẫn dắt các tình nguyện viên khác, góp phần xây dựng một đội ngũ vững mạnh và đầy nhiệt huyết. Với sự cống hiến và lòng nhân ái của mình, Nguyễn Ngọc Sơn đã và đang mang lại niềm vui và hy vọng cho rất nhiều người trong cộng đồng.', N'ql4.png', N'Thành viên')
INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (7, N'Phạm Hữu Cường', N'cuong@gmail.com', N'0123789546', N'<p>Phạm Hữu Cường là một người đàn ông trung niên, khoảng 50 tuổi, nổi tiếng với lòng nhiệt huyết và sự cống hiến không ngừng nghỉ trong lĩnh vực từ thiện. Ông hiện đang làm việc trong một tổ chức từ thiện với sứ mệnh mang lại sự hỗ trợ và cơ hội mới cho những người có hoàn cảnh khó khăn.</p><p>Sinh ra và lớn lên trong một gia đình bình dị, ông Cường từ sớm đã thấu hiểu sâu sắc những khó khăn của cuộc sống. Chính những trải nghiệm thời thơ ấu đã khơi nguồn cho tình yêu thương và sự đồng cảm với những mảnh đời kém may mắn. Sau khi tốt nghiệp đại học, ông đã dành phần lớn sự nghiệp của mình để làm việc tại các tổ chức phi lợi nhuận và từ thiện.</p><p>Tại tổ chức từ thiện nơi ông đang công tác, ông Cường giữ vai trò quản lý các dự án hỗ trợ giáo dục và y tế cho trẻ em nghèo. Ông không chỉ giám sát các hoạt động quyên góp và phân phối tài trợ, mà còn trực tiếp tham gia vào các chương trình, mang lại niềm vui và hy vọng cho các em nhỏ. Sự tận tụy và lòng nhân ái của ông đã truyền cảm hứng cho nhiều người, góp phần xây dựng một cộng đồng đoàn kết và tràn đầy tình thương.</p><p>Với tinh thần làm việc không ngừng nghỉ và trái tim ấm áp, Phạm Hữu Cường là một tấm gương sáng trong công tác từ thiện, luôn hết mình vì một xã hội công bằng và tốt đẹp hơn.</p>', N'u50.jpg', N'Thành viên')
INSERT [dbo].[QUANLY] ([MaQL], [TenQL], [EmailQL], [SDTQL], [TieuSu], [AnhQL], [ChucVu]) VALUES (14, N'Phạm Hữu Thanh', N'minh1@gmail.com', N'0123789548', N'<p>tao mệt lắm òi nha huhuhu</p>', N'DSC09114.JPG', N'Thành viên')
SET IDENTITY_INSERT [dbo].[QUANLY] OFF
GO
SET IDENTITY_INSERT [dbo].[QUYENGOP] ON 

INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (1, 1, CAST(300000 AS Decimal(18, 0)), CAST(N'2023-09-23T18:47:48.140' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (2, 1, CAST(500000 AS Decimal(18, 0)), CAST(N'2023-09-24T10:43:38.657' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (3, 1, CAST(15000 AS Decimal(18, 0)), CAST(N'2023-10-24T15:35:21.360' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (4, 1, CAST(30000 AS Decimal(18, 0)), CAST(N'2023-10-24T15:37:55.807' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (5, 1, CAST(25000 AS Decimal(18, 0)), CAST(N'2023-10-24T15:50:30.507' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (6, 1, CAST(60000 AS Decimal(18, 0)), CAST(N'2023-10-24T19:54:40.323' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (7, 1, CAST(300000 AS Decimal(18, 0)), CAST(N'2023-10-24T20:06:40.873' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (8, 1, CAST(300000 AS Decimal(18, 0)), CAST(N'2023-11-24T20:11:05.457' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (9, 1, CAST(200000 AS Decimal(18, 0)), CAST(N'2023-11-24T20:12:27.657' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (10, 1, CAST(50000000 AS Decimal(18, 0)), CAST(N'2023-11-24T21:28:17.410' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (11, 1, CAST(200000 AS Decimal(18, 0)), CAST(N'2023-11-24T21:39:47.897' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (12, 1, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-11-24T21:44:18.153' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (13, 1, CAST(200000 AS Decimal(18, 0)), CAST(N'2024-11-24T21:44:37.647' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (14, 2, CAST(4000000 AS Decimal(18, 0)), CAST(N'2024-11-24T21:57:35.263' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (15, 1, CAST(10000000 AS Decimal(18, 0)), CAST(N'2024-11-24T22:18:23.900' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (16, 2, CAST(20000000 AS Decimal(18, 0)), CAST(N'2024-11-24T22:22:04.197' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (17, 2, CAST(20000000 AS Decimal(18, 0)), CAST(N'2024-11-24T22:24:04.883' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (18, 3, CAST(30000000 AS Decimal(18, 0)), CAST(N'2024-11-25T11:48:54.737' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (19, 3, CAST(300000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:08:39.270' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (20, 3, CAST(300000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:17:36.853' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (21, 3, CAST(300000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:32:17.453' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (22, 3, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:32:40.697' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (23, 3, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:34:05.677' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (24, 3, CAST(400000 AS Decimal(18, 0)), CAST(N'2024-11-25T15:57:17.447' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (28, 2, CAST(460000 AS Decimal(18, 0)), CAST(N'2024-12-05T19:08:29.447' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (29, 14, CAST(300000 AS Decimal(18, 0)), CAST(N'2024-12-10T17:06:04.800' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (30, 14, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-12-10T22:49:18.997' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (31, 14, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-12-10T22:51:38.507' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (32, 1, CAST(2000000 AS Decimal(18, 0)), CAST(N'2024-12-10T23:06:16.003' AS DateTime))
INSERT [dbo].[QUYENGOP] ([MaQG], [MaND], [SoTien], [NgayQuyenGop]) VALUES (33, 14, CAST(50000 AS Decimal(18, 0)), CAST(N'2024-12-11T12:02:46.800' AS DateTime))
SET IDENTITY_INSERT [dbo].[QUYENGOP] OFF
GO
SET IDENTITY_INSERT [dbo].[THUVIEN] ON 

INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 1, N'nhatinhthuong1.jpg', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 5, N'nhatinhthuong2.jpg', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 6, N'nhatinhthuong3.jpg', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 7, N'nhatinhthuong4.jpg', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 8, N'nhatinhthuong5.jpg', CAST(N'2024-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 15, N'nhatinhthuong6.jpg', CAST(N'2024-01-02T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 16, N'nhatinhthuong7.jpg', CAST(N'2024-01-02T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 17, N'nhatinhthuong8.jpg', CAST(N'2024-01-02T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 18, N'maiamtinhthuong9.jpg', CAST(N'2024-01-04T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 19, N'nhatinhthuong10.jpg', CAST(N'2024-01-04T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 20, N'nhatinhthuong11.jpg', CAST(N'2024-01-04T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (1, 21, N'nhatinhthuong12.jpg', CAST(N'2024-01-04T00:00:00.000' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (3, 80, N'thapsanguocmo1.jpg', CAST(N'2024-12-02T15:35:30.577' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (3, 81, N'thapsanguocmo2.1.jpg', CAST(N'2024-12-02T15:35:30.650' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (3, 82, N'thapsanguocmo3.1.jpg', CAST(N'2024-12-02T15:35:30.653' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (3, 83, N'thapsanguocmo4.jpg', CAST(N'2024-12-02T15:35:30.653' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (3, 84, N'thapsanguocmo5.jpg', CAST(N'2024-12-02T15:35:30.657' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 85, N'xaydung1.jpeg', CAST(N'2024-12-05T15:13:07.220' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 86, N'xaydung2.jpeg', CAST(N'2024-12-05T15:13:07.303' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 87, N'xaydung3.jpeg', CAST(N'2024-12-05T15:13:07.307' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 88, N'xaydung4.jpeg', CAST(N'2024-12-05T15:13:07.310' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 89, N'xaydung5.jpeg', CAST(N'2024-12-05T15:13:07.313' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 90, N'xaydung6.jpeg', CAST(N'2024-12-05T15:13:07.317' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 91, N'xaydung7.jpeg', CAST(N'2024-12-05T15:13:07.320' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 92, N'xaydung8.jpeg', CAST(N'2024-12-05T15:13:07.323' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 93, N'xaydung9.jpeg', CAST(N'2024-12-05T15:13:07.327' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 94, N'xaydung10.jpeg', CAST(N'2024-12-05T15:13:07.327' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 95, N'xaydung11.jpeg', CAST(N'2024-12-05T15:13:07.330' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 96, N'xaydung12.jpeg', CAST(N'2024-12-05T15:13:07.333' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 97, N'xaydung13.jpeg', CAST(N'2024-12-05T15:13:07.337' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 98, N'xaydung14.jpeg', CAST(N'2024-12-05T15:13:07.337' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 99, N'xaydung15.jpeg', CAST(N'2024-12-05T15:13:07.340' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 100, N'xaydung16.jpeg', CAST(N'2024-12-05T15:13:07.340' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 101, N'xaydung17.jpeg', CAST(N'2024-12-05T15:13:07.343' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 102, N'xaydung18.jpeg', CAST(N'2024-12-05T15:13:07.343' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 103, N'xaydung19.jpeg', CAST(N'2024-12-05T15:13:07.350' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (4, 104, N'xaydung20.jpeg', CAST(N'2024-12-05T15:13:07.353' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 49, N'nauan6.jpg', CAST(N'2024-12-02T14:54:33.663' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 50, N'nauan7.jpg', CAST(N'2024-12-02T14:54:33.663' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 51, N'nauan8.jpg', CAST(N'2024-12-02T14:54:33.663' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 52, N'nauan9.jpg', CAST(N'2024-12-02T14:54:33.673' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 53, N'nauan10.jpg', CAST(N'2024-12-02T14:54:33.673' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 54, N'nauan11.jpg', CAST(N'2024-12-02T14:54:33.677' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 55, N'nauan12.jpg', CAST(N'2024-12-02T14:54:33.680' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 56, N'nauan13.jpg', CAST(N'2024-12-02T14:54:33.680' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 57, N'nauan14.jpg', CAST(N'2024-12-02T14:54:33.683' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 67, N'nauan4.jpg', CAST(N'2024-12-02T14:54:33.703' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 68, N'nauan5.jpg', CAST(N'2024-12-02T14:54:33.703' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 69, N'nauan15.jpg', CAST(N'2024-12-02T15:15:13.503' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 70, N'nauan16.jpg', CAST(N'2024-12-02T15:15:13.520' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 71, N'nauan17.jpg', CAST(N'2024-12-02T15:15:13.520' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 72, N'nauan18.jpg', CAST(N'2024-12-02T15:15:13.520' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 73, N'nauan19.jpg', CAST(N'2024-12-02T15:15:13.523' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 74, N'nauan20.jpg', CAST(N'2024-12-02T15:15:13.527' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 75, N'nauan1.jpg', CAST(N'2024-12-02T15:15:13.527' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 76, N'nauan2.jpg', CAST(N'2024-12-02T15:15:13.530' AS DateTime))
INSERT [dbo].[THUVIEN] ([MaCD], [MaHA], [Anh], [NgayDang]) VALUES (5, 77, N'nauan3.jpg', CAST(N'2024-12-02T15:15:13.530' AS DateTime))
SET IDENTITY_INSERT [dbo].[THUVIEN] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__NGUOIDUN__A9D105343DEBD857]    Script Date: 12/12/2024 8:26:40 AM ******/
ALTER TABLE [dbo].[NGUOIDUNG] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__NGUOIDUN__D5B8C7F0B3C29B3A]    Script Date: 12/12/2024 8:26:40 AM ******/
ALTER TABLE [dbo].[NGUOIDUNG] ADD UNIQUE NONCLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__QUANLY__7ED955FC6ADCA53F]    Script Date: 12/12/2024 8:26:40 AM ******/
ALTER TABLE [dbo].[QUANLY] ADD UNIQUE NONCLUSTERED 
(
	[EmailQL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LIENHE] ADD  DEFAULT (getdate()) FOR [NgayGui]
GO
ALTER TABLE [dbo].[NGUOIDUNG] ADD  DEFAULT (getdate()) FOR [NgayDangKi]
GO
ALTER TABLE [dbo].[THUVIEN] ADD  CONSTRAINT [DF_THUVIEN_NgayDang]  DEFAULT (getdate()) FOR [NgayDang]
GO
ALTER TABLE [dbo].[CHIENDICH]  WITH CHECK ADD FOREIGN KEY([MaQL])
REFERENCES [dbo].[QUANLY] ([MaQL])
GO
ALTER TABLE [dbo].[CHITIETQUYENGOP]  WITH CHECK ADD  CONSTRAINT [FK_ChienDich] FOREIGN KEY([MaCD])
REFERENCES [dbo].[CHIENDICH] ([MaCD])
GO
ALTER TABLE [dbo].[CHITIETQUYENGOP] CHECK CONSTRAINT [FK_ChienDich]
GO
ALTER TABLE [dbo].[CHITIETQUYENGOP]  WITH CHECK ADD  CONSTRAINT [FK_QuyenGop] FOREIGN KEY([MaQG])
REFERENCES [dbo].[QUYENGOP] ([MaQG])
GO
ALTER TABLE [dbo].[CHITIETQUYENGOP] CHECK CONSTRAINT [FK_QuyenGop]
GO
ALTER TABLE [dbo].[QUYENGOP]  WITH CHECK ADD FOREIGN KEY([MaND])
REFERENCES [dbo].[NGUOIDUNG] ([MaND])
GO
ALTER TABLE [dbo].[THUVIEN]  WITH CHECK ADD  CONSTRAINT [fk_CD] FOREIGN KEY([MaCD])
REFERENCES [dbo].[CHIENDICH] ([MaCD])
GO
ALTER TABLE [dbo].[THUVIEN] CHECK CONSTRAINT [fk_CD]
GO
ALTER TABLE [dbo].[CHIENDICH]  WITH CHECK ADD CHECK  (([TongQuy]>=(0)))
GO
ALTER TABLE [dbo].[CHITIETQUYENGOP]  WITH CHECK ADD CHECK  (([SoTienQG]>=(0)))
GO
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD CHECK  (([SDT] like '[0-9]%'))
GO
ALTER TABLE [dbo].[QUANLY]  WITH CHECK ADD CHECK  (([SDTQL] like '[0-9]%'))
GO
ALTER TABLE [dbo].[QUYENGOP]  WITH CHECK ADD CHECK  (([SoTien]>(0)))
GO
/****** Object:  StoredProcedure [dbo].[sp_QuyenGopTheoThang]    Script Date: 12/12/2024 8:26:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_QuyenGopTheoThang]
AS
BEGIN
    SELECT 
        YEAR(NgayQuyenGop) AS Nam,
        MONTH(NgayQuyenGop) AS Thang,
        COUNT(*) AS SoLuotQuyenGop
    FROM QUYENGOP
    GROUP BY YEAR(NgayQuyenGop), MONTH(NgayQuyenGop)
    ORDER BY Nam, Thang;
END
GO
USE [master]
GO
ALTER DATABASE [CharityManagement] SET  READ_WRITE 
GO
