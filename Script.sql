
create table Cliente
(
id_cliente int primary key not null IDENTITY(1,1),
primer_nombre varchar(20) not null,
segundo_nombre varchar(20),
primer_apellido varchar(20) not null,
segundo_apellido varchar(20),
cedula varchar(20) not null,
dirreccion varchar(100),
telefono VARCHAR(10),
correo VARCHAR(100)
);

create table Empleado
(
id_empleado int primary key not null IDENTITY(1,1),
primer_nombre varchar(20) not null,
segundo_nombre varchar(20),
primer_apellido varchar(20) not null,
segundo_apellido varchar(20),
cedula varchar(20) not null,
dirreccion varchar(100),
telefono VARCHAR(10) NOT null,
correo VARCHAR(100)
);


create table Categoria
(
id_categoria int primary key not null  IDENTITY(1,1),
nombre varchar(100) NOT null,
descripcion varchar(150),
estado BIT DEFAULT 1 NOT NULL
);

create table Modelo_Vehiculo
(
id_modelo int primary key not null IDENTITY(1,1),
id_categoria INT FOREIGN KEY REFERENCES dbo.Categoria(id_categoria) not NULL,
marca varchar(30) NOT NULL,
modelo varchar(20) NOT NULL,
combustible varchar(20) NOT NULL,
tipo_carroceria varchar(20),
estado BIT DEFAULT 1 NOT NULL
);


create table Vehiculo
(
id_vehiculo INT primary key not null IDENTITY(1,1),
id_modelo INT FOREIGN KEY REFERENCES dbo.Modelo_Vehiculo(id_modelo) not NULL,
placa varchar(10) NOT null,
no_Chasis NVARCHAR(100) NOT null,
motor NVARCHAR(100) NOT null,
color varchar(30),
Costo_dia FLOAT NOT NULL,
estado varchar(20)
);


create table Renta
(
id_renta INT primary key not null IDENTITY(1,1),
id_cliente INT FOREIGN KEY REFERENCES dbo.Cliente(id_cliente) NOT NULL,
id_empleado INT FOREIGN KEY REFERENCES dbo.Empleado(id_empleado) NOT NULL,
fecha DATETIME NOT null,
estado BIT DEFAULT 1 NOT NULL
);

create table Detalle_Renta
(
id_detalle_renta int primary key not null IDENTITY(1,1),
id_renta INT FOREIGN KEY REFERENCES dbo.Renta(id_renta) not null,
id_vehiculo INT FOREIGN KEY REFERENCES dbo.Vehiculo(id_vehiculo) not null,
fecha_ini DATE NOT NULL,
fecha_fin DATE NOT NULL
);

CREATE TABLE Bitacora 
(
	id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	accion NVARCHAR(100) NOT null, --crear,ver,modificar,etc
	controlador NVARCHAR(100) NOT null, --cliente,empleado,etc
	fecha datetime NOT NULL,
	usuario NVARCHAR(100) NOT NULL
)



/*
CREATE TABLE rol 
(
	id_rol INT IDENTITY(1,1) PRIMARY KEY NOT null,
	nombre NVARCHAR(50) NOT NULL UNIQUE,
	estado BIT DEFAULT 1 NOT NULL,
	usuario_registro NVARCHAR(100) NOT null,
	fecha_registro DATETIME NOT NULL,
	usuario_modificacion NVARCHAR(100),
	fecha_modificacion DATETIME,
)


CREATE TABLE usuario
(
	id_usuario INT  IDENTITY(1,1) PRIMARY KEY NOT null,
	id_rol INT FOREIGN KEY REFERENCES dbo.rol(id_rol) NOT NULL,
	nombre_usuario NVARCHAR(50) NOT NULL,
	password_hash VARBINARY(MAX) NOT NULL,
	password_salt VARBINARY(MAX) NOT NULL,
	estado BIT DEFAULT 1 NOT NULL,
	usuario_registro NVARCHAR(100) NOT null,
	fecha_registro DATETIME NOT NULL,
	usuario_modificacion NVARCHAR(100),
	fecha_modificacion DATETIME,
)
*/