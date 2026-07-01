create database Agenda;
GO

use Agenda;
GO

CREATE TABLE Usuarios(
	UsuarioId INT IDENTITY(1, 1) PRIMARY KEY,
	Nombre VARCHAR(100),
	ApellidoPaterno VARCHAR(100),
	ApellidoMaterno VARCHAR(100),
	CorreoElectronico VARCHAR(256),
	NombreUsuario VARCHAR(50),
	Contrasena VARCHAR(256),
	ExtensionImagen VARCHAR(5)
);
GO

INSERT INTO Usuarios
VALUES('Brandon', 'Arroyo', 'Olalde', 'brandon@gmail.com','brandon13', '123456', 'jpg');
GO

SELECT * FROM Usuarios;
GO

CREATE TABLE Contactos(
	ContactoId INT IDENTITY(1, 1) PRIMARY KEY,
	Nombre VARCHAR(100),
	ApellidoPaterno VARCHAR(100),
	ApellidoMaterno VARCHAR(100),
	FechaNacimiento DATE,
	CorreoElectronico VARCHAR(100),
	ExtensionImagen VARCHAR(5),
	UsuarioId INT NOT NULL,

	CONSTRAINT FK_Contactos_Ususarios FOREIGN KEY (UsuarioId)
	REFERENCES Usuarios(UsuarioId)
);
GO

INSERT INTO Contactos (Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, CorreoElectronico, ExtensionImagen, UsuarioId)
VALUES ('Alan', 'Benitez', 'Arroyo', '1998-04-15', 'alan@example.com', '.jpg', 1);
GO

SELECT * FROM Contactos;

CREATE TABLE TiposContacto(
	TipoContactoId INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(50) NOT NULL
);
GO

INSERT INTO TiposContacto (Descripcion)
VALUES ('Telefono'),
	   ('LinkedIn'),
	   ('Facebook'),
	   ('Instagram');
GO



CREATE TABLE ContactoMedios (
	ContactoMedioId INT IDENTITY(1, 1) PRIMARY KEY,
	ContactoId INT NOT NULL,
	TipoContactoId INT NOT NULL,
	Valor VARCHAR(256) NOT NULL, -- URL o numero

	-- Llaves foraneas
	CONSTRAINT FK_ContactoMedios_Contactos FOREIGN KEY (ContactoId)
	REFERENCES Contactos(ContactoId),
	CONSTRAINT FK_ContactoMedios_TiposContacto FOREIGN KEY (TipoContactoId)
	REFERENCES TiposContacto(TipoContactoId)
);
GO

INSERT INTO ContactoMedios (ContactoId, TipoContactoId, Valor) VALUES
    (1, 1, '55 5658 11 11'),
    (1, 2, 'https://www.linkedin.com/in/alan-benitez'),
    (1, 3, 'https://www.facebook.com/alan-benitez'),
    (1, 4, 'https://www.instagram.com/alan-benitez');


SELECT
    -- Datos del usuario
    u.UsuarioId,
    u.Nombre            AS UsuarioNombre,
    u.NombreUsuario,
    u.CorreoElectronico AS UsuarioCorreo,

    -- Datos del contacto
    c.ContactoId,
    CONCAT(c.Nombre, ' ', c.ApellidoPaterno, ' ', c.ApellidoMaterno) AS ContactoNombreCompleto,
    c.FechaNacimiento,
    DATEDIFF(YEAR, c.FechaNacimiento, GETDATE()) AS Edad,
    c.CorreoElectronico AS ContactoCorreo,

    -- Datos del medio de contacto
    tc.Descripcion      AS TipoContacto,
    cm.Valor

FROM Usuarios u
    LEFT JOIN Contactos      c  ON c.UsuarioId      = u.UsuarioId
    LEFT JOIN ContactoMedios cm ON cm.ContactoId    = c.ContactoId
    LEFT JOIN TiposContacto  tc ON tc.TipoContactoId = cm.TipoContactoId

WHERE u.UsuarioId = 1  -- filtra por usuario específico
ORDER BY c.ContactoId, tc.Descripcion;
