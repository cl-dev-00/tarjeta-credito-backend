USE TARJETA_DE_CREDITO_TEST
GO

CREATE PROCEDURE ObtenerHistorialDeLaTarjeta
	@idTarjeta int
	AS 
	BEGIN
		SELECT CONCAT('P', p.idPago) AS IdTransaccion, 'Pago a Tarjeta  de Cr�dito' AS Descripcion, p.monto AS Monto, p.fecha AS Fecha
			FROM TARJETA t
			INNER JOIN PAGO p ON p.idTarjeta = t.idTarjeta 
			WHERE t.idTarjeta = @idTarjeta AND t.estado = 'ACTIVO' AND p.estado = 'ACTIVO'
		UNION
		SELECT CONCAT('C', c.idCompra) AS IdTransaccion, c.descripcion AS Descripcion, c.monto AS Monto, c.fecha AS Fecha
			FROM TARJETA t
			INNER JOIN COMPRA c ON c.idTarjeta = t.idTarjeta
			WHERE t.idTarjeta = @idTarjeta AND t.estado = 'ACTIVO' AND c.estado = 'ACTIVO'
		ORDER BY fecha DESC
		 FOR JSON PATH;
	END
GO

