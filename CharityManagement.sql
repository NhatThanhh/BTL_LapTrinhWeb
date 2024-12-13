create database CharityManagement
use CharityManagement

CREATE TABLE NGUOIDUNG (
    MaND INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) not null,
    TaiKhoan varchar(50) unique,
    MatKhau varchar(20) not null,
    Email VARCHAR(100) UNIQUE,
    SDT VARCHAR(20) CHECK (SDT LIKE '[0-9]%'),
    DiaChi NVARCHAR(255),
    NgaySinh DATETIME
);
ALTER TABLE NGUOIDUNG
ALTER COLUMN NgaySinh DATETIME;
ALTER TABLE NGUOIDUNG
ADD  NgayDangKi DATETIME default getdate();


CREATE TABLE QUANLY(
	MaQL INT IDENTITY (1,1) PRIMARY KEY,
	TenQL NVARCHAR(100) NOT NULL,
	EmailQL NVARCHAR(100) UNIQUE,
	SDTQL VARCHAR(20) CHECK (SDTQL LIKE '[0-9]%'),
	TieuSu NVARCHAR(MAX),
	AnhQL VARCHAR(50)
);

CREATE TABLE CHIENDICH(
	MaCD INT IDENTITY(1,1) PRIMARY KEY,
	TenCD NVARCHAR(MAX) NOT NULL,
	MoTa NVARCHAR(MAX),
	MaQL INT NOT NULL,
	AnhBia VARCHAR(50) NOT NULL,
	TongQuy DECIMAL(18,0) CHECK(TongQuy >= 0),
	NgayTao DATETIME,
	FOREIGN KEY (MaQL) REFERENCES QUANLY(MaQL)
);

CREATE TABLE QUYENGOP(
    MaQG INT IDENTITY(1,1) PRIMARY KEY,
    MaND INT, 
    MaCD INT, 
    HoTen NVARCHAR(100) NOT NULL, 
    SoTien DECIMAL(18,0) CHECK (SoTien > 0), 
    NgayQuyenGop DATETIME, 
    FOREIGN KEY (MaND) REFERENCES NGUOIDUNG(MaND), 
    FOREIGN KEY (MaCD) REFERENCES CHIENDICH(MaCD) 
);

CREATE TABLE CHITIETQUYENGOP(
	MaQG INT ,
	MaCD INT ,
	SoTienQG DECIMAL(18,0) CHECK(SoTienQG >= 0),
	CONSTRAINT PK_CTQuyenGop PRIMARY KEY(MaQG, MaCD),
	CONSTRAINT FK_QuyenGop FOREIGN KEY (MaQG)
	REFERENCES QUYENGOP(MaQG),
	CONSTRAINT FK_ChienDich FOREIGN KEY (MaCD)
	REFERENCES CHIENDICH(MaCD)
);

CREATE TABLE THUVIEN(
	MaCD int not null,
	MaHA int identity(1,1) not null,
	Anh varchar(50) not null,
	NgayDang datetime,
	constraint pk_THUVIEN primary key (MaCD, MaHA),
	constraint fk_CD foreign key (MaCD) references CHIENDICH(MaCD)
);
ALTER TABLE THUVIEN
ADD CONSTRAINT DF_THUVIEN_NgayDang DEFAULT GETDATE() FOR NgayDang;


create table LIENHE(
	ID int identity(1,1) primary key,
	TenLH nvarchar(60),
	SDTLH VARCHAR(20),
	EmailLH NVARCHAR(100),
	NgayGui Datetime default getdate()
);
alter table LIENHE
ADD ID INT IDENTITY (1,1) primary key;












CREATE PROCEDURE sp_QuyenGopTheoThang
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

exec sp_QuyenGopTheoThang