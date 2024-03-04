DROP DATABASE IF EXISTS Tienda;
CREATE DATABASE Tienda;

CREATE TABLE Fabricante(
	Id INT  IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Producto(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR (50) NOT NULL,
	Precio DECIMAL NOT NULL,
	Id_fabricante INT NOT NULL,
	FOREIGN KEY (Id_fabricante) REFERENCES Fabricante(Id)
);

ALTER TABLE Producto
ALTER COLUMN Id INT IDENTITY(1,1);

INSERT INTO fabricante VALUES('Asus');
INSERT INTO fabricante VALUES('Lenovo');
INSERT INTO fabricante VALUES('Hewlett-Packard');
INSERT INTO fabricante VALUES('Samsung');
INSERT INTO fabricante VALUES('Seagate');
INSERT INTO fabricante VALUES('Crucial');
INSERT INTO fabricante VALUES('Gigabyte');
INSERT INTO fabricante VALUES('Huawei');
INSERT INTO fabricante VALUES('Xiaomi');

INSERT INTO producto VALUES('Disco duro SATA3 1TB', 86.99, 5);
INSERT INTO producto VALUES('Memoria RAM DDR4 8GB', 120, 6);
INSERT INTO producto VALUES('Disco SSD 1 TB', 150.99, 4);
INSERT INTO producto VALUES('GeForce GTX 1050Ti', 185, 7);
INSERT INTO producto VALUES('GeForce GTX 1080 Xtreme', 755, 6);
INSERT INTO producto VALUES('Monitor 24 LED Full HD', 202, 1);
INSERT INTO producto VALUES('Monitor 27 LED Full HD', 245.99, 1);
INSERT INTO producto VALUES('Port�til Yoga 520', 559, 2);
INSERT INTO producto VALUES('Port�til Ideapd 320', 444, 2);
INSERT INTO producto VALUES('Impresora HP Deskjet 3720', 59.99, 3);
INSERT INTO producto VALUES('Impresora HP Laserjet Pro M26nw', 180, 3);

use Tienda;
DROP PROCEDURE IF EXISTS sp_GestorProducto;
CREATE PROCEDURE sp_GestorProducto
	@ACCION int,
	@IdPrducto int= null,
	@NombreProducto varchar(50)= null,
	@PrecioProducto decimal=null,
	@IDFabricante int=null,
	@NuevoID int OUTPUT

AS 
BEGIN
	IF @ACCION=1
		BEGIN
			SELECT p.Id, p.Nombre,p.Precio,f.Id as IdFabricante,f.Nombre as NombreFabricante FROM Producto p
			inner join Fabricante f on f.Id=p.Id_fabricante;
		END 
	ELSE IF @ACCION=2
		BEGIN 
			IF EXISTS (SELECT 1 FROM Producto WHERE Nombre = @NombreProducto)
				BEGIN
					  RAISERROR ('El nombre del Producto ya existe en la tabla.', 16, 1)
				RETURN;
				END
				ELSE
				BEGIN 
					INSERT INTO Producto (Nombre,Precio,Id_fabricante) values (@NombreProducto,@PrecioProducto,@IDFabricante)
					SET @NuevoID= SCOPE_IDENTITY();
				END	
		END
	ELSE IF @ACCION=3
		BEGIN
			UPDATE Producto SET Nombre=@NombreProducto, Precio=@PrecioProducto,Id_fabricante=@IDFabricante WHERE Id=@IdPrducto
		END
	ELSE IF @ACCION=4
		BEGIN
			DELETE FROM Producto WHERE Id=@IdPrducto
		END
	ELSE
		BEGIN
		 RAISERROR('Acción no válida', 16, 1);
		 END

END






use Tienda;
DROP PROCEDURE IF EXISTS sp_GestorFabricante;
CREATE PROCEDURE sp_GestorFabricante
	@ACCION int,
	@IdFabricante int= null,
	@NombreFabricante varchar(50)= null,
	@NuevoID int OUTPUT

AS 
BEGIN
	IF @ACCION=1
		BEGIN
			SELECT * FROM Fabricante
		END 
	ELSE IF @ACCION=2
		BEGIN 
			IF EXISTS (SELECT 1 FROM Fabricante WHERE Nombre = @NombreFabricante)
				BEGIN
					  RAISERROR ('El nombre del fabricante ya existe en la tabla.', 16, 1)
				RETURN;
				END
				ELSE
				BEGIN 
					INSERT INTO Fabricante (Nombre) values (@NombreFabricante)
					SET @NuevoID= SCOPE_IDENTITY();
				END	
		END
	ELSE IF @ACCION=3
		BEGIN
			UPDATE Fabricante SET Nombre=@NombreFabricante WHERE Id=@IdFabricante
		END
	ELSE IF @ACCION=4
		BEGIN
			DELETE FROM Fabricante WHERE Id=@IdFabricante
		END
	ELSE
		BEGIN
		 RAISERROR('Acci�n no v�lida', 16, 1);
		 END

END






