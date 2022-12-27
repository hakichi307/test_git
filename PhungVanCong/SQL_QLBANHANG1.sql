use master
go
create database QLBANHANG
go
use QLBANHANG

create table LoaiSanPham(
	MaLoai nchar(10) not null primary key,
	TenLoai nvarchar(50)
)
create table SanPham(
	MaSP	nchar(10) not null primary key,
	TenSP	nvarchar(50),
	MaLoai	nchar(10) not null,
	DonGia	float,
	SoLuong	int,
	constraint fk_LoaiSanPham_SanPham foreign key (MaLoai) references LoaiSanPham(MaLoai) on update cascade on delete cascade
)

insert into LoaiSanPham values ('01',N'Quần áo'),
							('02',N'Điện tử')
insert into SanPham values	('01',N'ao so mi nam','01',4900,150),
							('02',N'quan bo nu','01',4500,200),
							('03',N'Lo vi song','02',5300,100),
							('04',N'Noi com dien','02',5900,170)

select * from LoaiSanPham
select * from SanPham

drop table LoaiSanPham
drop table SanPham