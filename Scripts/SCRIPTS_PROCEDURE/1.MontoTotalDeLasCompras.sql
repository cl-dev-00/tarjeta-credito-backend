USE TARJETA_DE_CREDITO_TEST
GO

CREATE PROCEDURE MontoTotalDeLasCompras
	@idTarjeta int,
	@mesActual int OUTPUT,  
	@mesAnterior int OUTPUT,
	@montoMesActual decimal OUTPUT,
	@montoMesAnterior decimal OUTPUT
	AS 
	BEGIN
		SELECT @montoMesActual = SUM(c.monto)  FROM COMPRA c WHERE idTarjeta = @idTarjeta AND MONTH(c.fecha) = MONTH(GETDATE()) GROUP BY MONTH(c.fecha);

		SELECT @montoMesAnterior = SUM(c.monto)  FROM COMPRA c WHERE idTarjeta = @idTarjeta AND MONTH(c.fecha) = MONTH(GETDATE()) - 1 GROUP BY MONTH(c.fecha);
		
		SET @mesActual = MONTH(GETDATE());
		SET @mesAnterior = MONTH(GETDATE()) - 1;

		IF @montoMesActual IS NULL
			BEGIN
				SET @montoMesActual = 0;
			END
		IF @montoMesAnterior IS NULL
			BEGIN
				SET @montoMesAnterior = 0;
			END
	END
GO

