USE TARJETA_DE_CREDITO_TEST
GO

CREATE TABLE CLIENTE (
    idCliente INT NOT NULL IDENTITY(1, 1),
    codigoCliente VARCHAR(10) NOT NULL,
    nombreCliente VARCHAR(200) NOT NULL,
    apellidoCliente VARCHAR(200) NOT NULL,

    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_CLIENTE   PRIMARY KEY NONCLUSTERED (idCliente)
)
GO

CREATE TABLE TIPO_TARJETA (
    idTipoTarjeta VARCHAR(8) NOT NULL,
    nombreTipoTarjeta VARCHAR(30) NOT NULL,
    descripcionTipoTarjeta VARCHAR(100) NOT NULL,
    limiteCredito DECIMAL(9,3) NOT NULL,

    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_TIPO_TARJETA PRIMARY KEY NONCLUSTERED (idTipoTarjeta)
)
GO

CREATE TABLE TARJETA (
    idTarjeta INT NOT NULL IDENTITY(1, 1),
    idCliente INT NOT NULL,
    idTipoTarjeta VARCHAR(8) NOT NULL,
    numeroTarjeta VARCHAR(16) NOT NULL,
    saldoActual DECIMAL(9,3) NOT NULL,
    saldoDisponible DECIMAL(9,3) NOT NULL,

    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_TARJETA PRIMARY KEY NONCLUSTERED (idTarjeta, idCliente),
    CONSTRAINT FK_CLIENTE_TARJETA FOREIGN KEY (idCliente) REFERENCES CLIENTE (idCliente) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_TIPO_TARJETA_TARJETA FOREIGN KEY (idTipoTarjeta) REFERENCES TIPO_TARJETA (idTipoTarjeta) ON DELETE CASCADE ON UPDATE CASCADE
)
GO


CREATE TABLE COMPRA (
    idCompra INT NOT NULL IDENTITY(1, 1),
    idTarjeta INT NOT NULL,
    idCliente INT NOT NULL,
    descripcion VARCHAR(255) NOT NULL,
    monto DECIMAL(9,3) NOT NULL,
    fecha DATE NOT NULL,
    
    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_COMPRA PRIMARY KEY NONCLUSTERED (idCompra, idTarjeta, idCliente),
    CONSTRAINT FK_TARJETA_COMPRA FOREIGN KEY (idTarjeta, idCliente) REFERENCES TARJETA (idTarjeta, idCliente) ON DELETE CASCADE ON UPDATE CASCADE
)
GO

CREATE TABLE PAGO (
    idPago INT NOT NULL IDENTITY(1, 1),
    idTarjeta INT NOT NULL,
    idCliente INT NOT NULL,
    monto DECIMAL(9,3) NOT NULL,
    fecha DATE NOT NULL,
    
    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_PAGO PRIMARY KEY NONCLUSTERED (idPago, idTarjeta, idCliente),
    CONSTRAINT FK_TARJETA_PAGO FOREIGN KEY (idTarjeta, idCliente) REFERENCES TARJETA (idTarjeta, idCliente) ON DELETE CASCADE ON UPDATE CASCADE
)
GO

CREATE TABLE CONFIGURACION (
    idConfiguracion VARCHAR(8) NOT NULL,
    nombreConfiguracion VARCHAR(30) NOT NULL,
    descripcionConfiguracion VARCHAR(100) NOT NULL,

    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_CONFIGURACION PRIMARY KEY NONCLUSTERED (idConfiguracion)
)
GO

CREATE TABLE CONFIGURACION_DETALLE (
    idConfiguracionDetalle VARCHAR(8) NOT NULL,
    idConfiguracion VARCHAR(8) NOT NULL,
    nombreConfiguracionDetalle VARCHAR(100) NOT NULL,
    valor1 VARCHAR(100) NOT NULL,
    valor2 VARCHAR(100),

    estado VARCHAR(10) NOT NULL,
    creadoPor VARCHAR(50) NOT NULL,
    fechaHoraCreacion DATETIME NOT NULL,
    modificadoPor VARCHAR(50),
    fechaHoraModificacion DATETIME,

    CONSTRAINT PK_CONFIGURACION_DETALLE PRIMARY KEY NONCLUSTERED (idConfiguracionDetalle, idConfiguracion),
    CONSTRAINT FK_CONFIG_CONFIG_DETALLE FOREIGN KEY (idConfiguracion) REFERENCES CONFIGURACION (idConfiguracion) ON DELETE CASCADE ON UPDATE CASCADE
)
GO

CREATE NONCLUSTERED INDEX INDEX_CLIENTE ON CLIENTE (idCliente, codigoCliente)
GO

CREATE NONCLUSTERED INDEX INDEX_TIPO_TARJETA ON TIPO_TARJETA (idTipoTarjeta)
GO

CREATE NONCLUSTERED INDEX INDEX_TARJETA ON TARJETA (idTarjeta, idCliente, idTipoTarjeta, numeroTarjeta)
GO

CREATE NONCLUSTERED INDEX INDEX_COMPRA ON COMPRA (idCompra, idTarjeta, idCliente, fecha)
GO

CREATE NONCLUSTERED INDEX INDEX_PAGO ON PAGO (idPago, idTarjeta, idCliente, fecha)
GO

CREATE NONCLUSTERED INDEX INDEX_CONFIGURACION ON CONFIGURACION (idConfiguracion)
GO

CREATE NONCLUSTERED INDEX INDEX_CONFIGURACION_DETALLE ON CONFIGURACION_DETALLE (idConfiguracionDetalle, idConfiguracion)
GO