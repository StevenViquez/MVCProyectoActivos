USE [master]
GO
/****** Object:  Database [activos]    Script Date: 2/8/2020 13:01:01 ******/
CREATE DATABASE [activos]
 CONTAINMENT = NONE
 GO
ALTER DATABASE [activos] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [activos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
USE [activos]


CREATE TABLE [dbo].[Vendedor]
(
  [VendedorID] [int] IDENTITY(1,1),
  [CedulaJuridica] [VARCHAR] (30) NOT NULL,
  [Descripcion] [VARCHAR](100) NOT NULL,
  [CorreoElectronico] [VARCHAR](50) not null,
  [NombreContacto] [VARCHAR](20) not null
)

create table [dbo].[TelefonoVendedor]
(
	TelefonoID [int] IDENTITY(1,1), 
	VendedorID [int] NOT NULL, 
	Telefono [VARCHAR](20) not null
)


CREATE TABLE [dbo].[TipoActivo]
(
  [TipoActivoID] [int] IDENTITY(1,1),
  [Descripcion] [VARCHAR](50) NOT NULL,
)

CREATE TABLE [dbo].[Marca]
(
  [MarcaID] [int] IDENTITY(1,1),
  [Descripcion] [VARCHAR](50) NOT NULL,
)

CREATE TABLE [dbo].[Asegurador]
(
  [AseguradorID] [int] IDENTITY(1,1),
  [Descripcion] [VARCHAR](50) NOT NULL,
)

CREATE TABLE [dbo].[Depreciacion]
(
  [DepreciacionID] [int] IDENTITY(1,1),
  [Fecha] [DATE] NOT NULL, 
  [Valor] [VARCHAR](50) NOT NULL, --Valor del activo con la de la misma fila
  [Activo][int]
)

CREATE TABLE [dbo].[Activos]
(
  [ActivoID][int] IDENTITY(1,1),
  [NumeroSerie] [VARCHAR](50) NOT NULL,
  [Modelo] [VARCHAR](50) NOT NULL, 
  [FechaCompra] [DATE] NOT NULL, 
  [FechaVencimientoSeguro] [DATE] NOT NULL, 
  [FechaVencimientoGarantia] [DATE] NOT NULL, 
  [CostoColones] [DECIMAL](18, 2)NOT NULL, 
  [CostoDolares] [DECIMAL](18, 2)NOT NULL, 
  [Descripcion] [VARCHAR](50) NOT NULL,
  [CondicionActivo] [VARCHAR](50) NOT NULL, --Excelente, Bueno, Regular & Malo
  [FotoFactura] [BINARY] NOT NULL, 
  [FotoActivo] [BINARY] NOT NULL, 
  [Marca] [int] NOT NULL, --[dbo].[Marca]
  [Vendedor] [int] NOT NULL, --[dbo].[Vendedor]
  [Asegurador] [int] NOT NULL, --[dbo].[Asegurador]
  [TipoActivo] [int] NOT NULL, --[dbo].[TipoActivo] 
  [ValorActual] [int] -- [dbo].[Depreciacion]
)

CREATE TABLE [dbo].[Usuario]
(
  [Login] [CHAR](20) NOT NULL, 
  [Nombre] [VARCHAR](50) NOT NULL,
  [Apellidos] [VARCHAR](50) NOT NULL,
  [Password] [VARCHAR](50) NOT NULL,
  [Estado] [VARCHAR](50) NOT NULL, --Administrador, Procesos & Reportes
  [IdRol] [int] NOT NULL
)

CREATE TABLE [dbo].[Rol]
(
  [IdRol] [int] IDENTITY(1,1),
  [Descripcion] [VARCHAR](50) NOT NULL
)

--VENDEDOR:
ALTER TABLE [dbo].[Vendedor] ADD CONSTRAINT PK_VENDEDOR PRIMARY KEY (VendedorID)

--TELEFONOVENDEDOR:
ALTER TABLE [dbo].[TelefonoVendedor] ADD CONSTRAINT PK_TELEFONO PRIMARY KEY (TelefonoID)

--TIPOACTIVO:
ALTER TABLE [dbo].[TipoActivo] ADD CONSTRAINT PK_TIPO_ACTIVO PRIMARY KEY (TipoActivoID)

--MARCA:
ALTER TABLE [dbo].[Marca] ADD CONSTRAINT PK_MARCA PRIMARY KEY (MarcaID)

--ASEGURADOR:
ALTER TABLE [dbo].[Asegurador] ADD CONSTRAINT PK_ASEGURADOR PRIMARY KEY (AseguradorID)

--DEPRECIACION:
ALTER TABLE [dbo].[Depreciacion] ADD CONSTRAINT PK_DEPRECIACION PRIMARY KEY (DepreciacionID)

--ACTIVOS:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT PK_ACTIVOS PRIMARY KEY (ActivoID)

--ACTIVOS <> TIPOACTIVO:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT FK_TIPO_ACTIVO FOREIGN KEY (TipoActivo) REFERENCES [dbo].[TipoActivo]

--ACTIVOS <> ASEGURADOR:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT FK_ASEGURADOR FOREIGN KEY (Asegurador) REFERENCES [dbo].[Asegurador]

--ACTIVOS <> MARCA:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT FK_MARCA FOREIGN KEY (Marca) REFERENCES [dbo].[Marca]

--ACTIVOS <> VENDEDOR:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT FK_VENDEDOR FOREIGN KEY (Vendedor) REFERENCES [dbo].[Vendedor]

--ACTIVOS <> DEPRECIACION:
ALTER TABLE [dbo].[Activos] ADD CONSTRAINT FK_DEPRECIACION FOREIGN KEY (ValorActual) REFERENCES [dbo].[Depreciacion]

--DEPRECIACION <> ACTIVOS:
ALTER TABLE [dbo].[Depreciacion] ADD CONSTRAINT FK_ACTIVO FOREIGN KEY (Activo) REFERENCES [dbo].[Activos]

--TELEFONOVENDEDOR <> VENDEDOR:
ALTER TABLE [dbo].[TelefonoVendedor] ADD CONSTRAINT FK_VENDEDOR_TELEFONO FOREIGN KEY (VendedorID) references [dbo].[Vendedor]

--USUARIO:
ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT PK_USUARIO PRIMARY KEY (Login)

--ROL:
ALTER TABLE [dbo].[Rol] ADD CONSTRAINT PK_ROL PRIMARY KEY (IdRol)


--USUARIO <> ROL:
ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT FK_ROL FOREIGN KEY (IdRol) REFERENCES [dbo].[Rol]


USE [activos]
GO


--ASEGURADORES:
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('INS')
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('ASSA')
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('MAPFRE')
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('CCSS')
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('ADISA')
INSERT INTO [dbo].[Asegurador] ([Descripcion]) VALUES('COOSEGUROS')


--MARCAS:
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Apple')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Samsung')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Huawei')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Toyota')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Hyundai')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Chevrolet')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('MSI')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Dell')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('HP')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Nike')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Adidas')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Oster')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Black and Decker')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Atlas')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Amazon')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Casio')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Rolex')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Pandora')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Michelin')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Ferrari')
INSERT INTO [dbo].[Marca] ([Descripcion])VALUES('Beats')

--TIPO DE ACTIVO:
INSERT INTO [dbo].[TipoActivo]([Descripcion])VALUES ('Electrodoméstico')
INSERT INTO [dbo].[TipoActivo]([Descripcion])VALUES ('Vivienda')
INSERT INTO [dbo].[TipoActivo]([Descripcion])VALUES ('Vehículo')
INSERT INTO [dbo].[TipoActivo]([Descripcion])VALUES ('Electrónico')
INSERT INTO [dbo].[TipoActivo]([Descripcion])VALUES ('Joyería')


DBCC CHECKIDENT ('[dbo].[Vendedor]', RESEED, 0)  
----VENDEDOR:
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685191','Venta de autos','ventadeautoscr@gmail.com' ,'Jose Ortiz')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685192','ICON','iconcr@gmail.com' ,'Maria Antonieta')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685193','Artelec','arteleccr@gmail.com' ,'Nicole Zamora')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685194','Ropa deportiva CR','ropadeportivacr@gmail.com' ,'Pedro Perez')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685196','Gollo','gollocr@gmail.com' ,'Adriana Diaz')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685197','Joyería CR','joyeriacr@gmail.com' ,'Maureen Mora')
INSERT INTO [dbo].[Vendedor] ([CedulaJuridica],[Descripcion],[CorreoElectronico],[NombreContacto]) VALUES ('3101685198','INTELEC','inteleccr@gmail.com' ,'Daniel Juarez')

--TELEFONO VENDEDOR:
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(1,'84952412')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(1,'85852413')


INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(2,'94852415')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(2,'34852416')

INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(3,'74852418')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(3,'64852419')


INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(4,'848524950')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(4,'848544448')


INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(5,'84850419')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(5,'84552410')


INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(6,'84356417')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(6,'84555414')

INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(7,'84952412')
INSERT INTO [dbo].[TelefonoVendedor]([VendedorID],[Telefono]) VALUES(7,'85852413')


--ROL:
INSERT INTO [dbo].[Rol]([Descripcion]) VALUES ('Administrador')
INSERT INTO [dbo].[Rol]([Descripcion]) VALUES ('Procesos')
INSERT INTO [dbo].[Rol]([Descripcion]) VALUES ('Reportes')


--USUARIO: Administrador, Procesos & Reportes
INSERT INTO [dbo].[Usuario]([Login],[Nombre]  ,[Apellidos],[Password] ,[Estado] ,[IdRol]) VALUES ('steve@gmail.com','Steve','Viquez Benavides','123','1', 1)
INSERT INTO [dbo].[Usuario]([Login],[Nombre]  ,[Apellidos],[Password] ,[Estado] ,[IdRol]) VALUES ('mau@gmail.com','Maureen','Mora Lezacno','123','1', 2)
INSERT INTO [dbo].[Usuario]([Login],[Nombre]  ,[Apellidos],[Password] ,[Estado] ,[IdRol]) VALUES ('sharon@gmail.com','Sharon','Viquez Benavides','123','1', 3)



--USE [activos]
--GO  
--ALTER TABLE [dbo].[Activos]  
--DROP CONSTRAINT FK_VENDEDOR;   
--GO 


--USE [activos]
--GO

--/****** Object:  Table [dbo].[Vendedor]    Script Date: 8/7/2020 8:43:40 PM ******/
--DROP TABLE [dbo].[Vendedor]
--GO


--DBCC CHECKIDENT ('[dbo].[Vendedor]', RESEED, 0)  