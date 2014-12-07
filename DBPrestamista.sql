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
insert into clientes(nombre,cedula,direccion,telefono) values('cliente','cedula','direc','tel')
insert into clientes(nombre,cedula,direccion,telefono) values('cliente 2','cedula','direc','tel')
insert into clientes(nombre,cedula,direccion,telefono) values('cliente 3','cedula','direc','tel')
insert into clientes(nombre,cedula,direccion,telefono) values('cliente 4','cedula','direc','tel')
insert into clientes(nombre,cedula,direccion,telefono) values('edward','22222222','direc','tel')
insert into prestamos (monto,interes,cuotas,periodoPago,moraPrestamo,fechaInicial,fechaFinal) values (1000,100,250,15,15,SYSDATETIME(),SYSDATETIME())
insert into prestamos (monto,interes,cuotas,periodoPago,moraPrestamo,fechaInicial,fechaFinal) values (2000,200,33,15,15,SYSDATETIME(),SYSDATETIME())
insert into facturacion(idCliente, idPrestamo) values(1,1)
insert into intervalos (idCliente,intervaloFecha,intervaloPago) values(4,SYSDATETIME(),41588)




select clientes.nombre,clientes.cedula,intervalos.intervaloFecha,intervalos.intervaloPago,intervalos.estado
from clientes inner join intervalos
on intervalos.idCliente = 3 and clientes.idCliente = 3

delete from intervalos where intervalos.idCliente=1

--------------------------------------------

select * from facturacion
select * from prestamos
select * from clientes
select * from intervalos

--------------------------------------------


update clientes set nombre='asdf' where clientes.idCliente = 1

--  DataGridView  INTÉRVALOS

-- 	| idIntervaloPago | Cliente | Fechas de Pago | Cuotas | estado |


-- cuotas = intervalos.intervaloPago
-- estado = intervalos.estado



select * from intervalos

SELECT idIntervaloPago, nombre, cedula
FROM dbo.clientes inner join dbo.intervalos
on intervalos.idCliente=clientes.idCliente 
where  exists (select fechaInicial, fechaFinal from prestamos, facturacion where facturacion.idPrestamo=prestamos.idPrestamo)

SELECT idIntervaloPago, nombre, cedula 
FROM dbo.clientes right join dbo.intervalos 
on clientes.idCliente=intervalos.idCliente 
WHERE exists (select fechaInicial, FechaFinal from prestamos, facturacion where prestamos.idPrestamo=facturacion.idPrestamo)
use prestamista
SELECT clientes.idCliente, nombre, cedula, monto, cuotas, interes, moraPrestamo, fechaInicial, fechaFinal FROM dbo.clientes,dbo.facturacion, prestamos where clientes.idCliente=facturacion.idCliente --intervalos.idCliente WHERE exists (select fechaInicial, FechaFinal from prestamos, facturacion where prestamos.idPrestamo=facturacion.idPrestamo)