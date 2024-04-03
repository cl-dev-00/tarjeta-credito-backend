USE TARJETA_DE_CREDITO_TEST
GO

INSERT INTO CLIENTE (codigoCliente, nombreCliente, apellidoCliente, estado, creadoPor, fechaHoraCreacion) VALUES
    ('FOTS123456', 'Fernando',  'Torres',       'ACTIVO', 'DEPARTAMENTO TI', GETDATE()),
    ('MSVA123456', 'Marcos',    'Villanueva',   'INACTIVO', 'DEPARTAMENTO TI', GETDATE())
GO

INSERT INTO TIPO_TARJETA (idTipoTarjeta, nombreTipoTarjeta, descripcionTipoTarjeta, limiteCredito, estado, creadoPor, fechaHoraCreacion) VALUES
    ('TARJECOM', 'Tarjeta EconoMía',                                'Tarjeta para todas las economías',     600,    'ACTIVO',      'DEPARTAMENTO TI', GETDATE()),
    ('TARJCLAS', 'Tarjeta Clásica Mastercard',                      'Tarjeta de credito clásica',           1500,   'ACTIVO',      'DEPARTAMENTO TI', GETDATE()),
    ('TARJAEXB', 'Tarjeta American Express BLUE',                   'Tarjeta especial',                     12000,  'INACTIVO',    'DEPARTAMENTO TI', GETDATE()),
    ('TARJAPRE', 'Tarjeta AAdvantage® Prestige',					'Tarjeta para las economías solidas',   10000,  'ACTIVO',      'DEPARTAMENTO TI', GETDATE())
GO

INSERT INTO TARJETA (idCliente, idTipoTarjeta, numeroTarjeta, saldoActual, saldoDisponible, estado, creadoPor, fechaHoraCreacion) VALUES
    ((SELECT idCliente FROM CLIENTE WHERE codigoCliente = 'FOTS123456'), 'TARJCLAS', '6570252874171727', 0, (SELECT limiteCredito FROM TIPO_TARJETA WHERE idTipoTarjeta = 'TARJCLAS'),'ACTIVO', 'DEPARTAMENTO TI', GETDATE()),
    ((SELECT idCliente FROM CLIENTE WHERE codigoCliente = 'FOTS123456'), 'TARJAPRE', '3619486603801731', 0, (SELECT limiteCredito FROM TIPO_TARJETA WHERE idTipoTarjeta = 'TARJAPRE'),'ACTIVO', 'DEPARTAMENTO TI', GETDATE())
GO

INSERT INTO CONFIGURACION (idConfiguracion, nombreConfiguracion, descripcionConfiguracion, estado, creadoPor, fechaHoraCreacion) VALUES
    ('CONFPORC', 'Configuración de porcentajes', 'Configuración para la configuración de intereses, saldo mínimo, etc.', 'ACTIVO', 'DEPARTAMENTO TI', GETDATE())
GO

INSERT INTO CONFIGURACION_DETALLE (idConfiguracionDetalle, idConfiguracion, nombreConfiguracionDetalle, valor1, valor2, estado, creadoPor, fechaHoraCreacion) VALUES
    ('PORCINTE',  'CONFPORC', 'Porcentaje de Interés',                       '0.25', '25%',  'ACTIVO', 'DEPARTAMENTO TI', GETDATE()),
    ('PRINTSMI',  'CONFPORC', 'Porcentaje de Interés para el Saldo Mínimo',  '0.05', '5%',   'ACTIVO', 'DEPARTAMENTO TI', GETDATE())
GO