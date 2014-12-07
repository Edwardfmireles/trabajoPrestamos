use master

go 

drop database Prestamista

go

create database Prestamista

go

use prestamista 

go

use Prestamista

go

create table prestamos(
idPrestamo int identity primary key,
monto int not null,
interes int not null,
cuotas int not null,
periodoPago varchar(10) not null,
moraPrestamo int not null,
fechaInicial date not null,
fechaFinal varchar(30) not null
)


go

create table clientes(
idCliente int identity primary key,
nombre varchar(30) not null,
cedula char(13) not null,
direccion varchar(50) not null,
telefono char(12) not null
)
go

create table facturacion(
idFactura int identity primary key,
idCliente int not null,
idPrestamo int not null

foreign key(idCliente) references clientes(idCliente),
foreign key(idPrestamo) references prestamos(idPrestamo) 
)


go

create table empleados(
nombre varchar(20) not null,
contrasena varchar(20) not null
)

go

create table intervalos(
idIntervaloPago int identity primary key,
idCliente int not null,
idFactura int not null,
intervaloFecha varchar(30) not null,
intervaloPago int not null,
estado varchar(10) default 'NO PAGO'

foreign key(idCliente) references clientes(idCliente)
)

go

insert into empleados(nombre, contrasena) values('admin','123')