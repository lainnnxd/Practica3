USE PracticaS12;
GO

CREATE PROCEDURE ObtenerComprasPendientes
AS
BEGIN
    SELECT Id_Compra, Descripcion, Saldo
    FROM Principal
    WHERE Estado = 'Pendiente';
END
GO

DROP PROCEDURE IF EXISTS ObtenerComprasPendientes;
GO

CREATE PROCEDURE RegistrarAbono
    @Id_Compra BIGINT,
    @Monto DECIMAL(18, 2)
AS
BEGIN
    BEGIN TRANSACTION;

    INSERT INTO Abonos (Id_Compra, Monto, Fecha)
    VALUES (@Id_Compra, @Monto, GETDATE());

    UPDATE Principal
    SET Saldo = Saldo - @Monto
    WHERE Id_Compra = @Id_Compra;

    UPDATE Principal
    SET Estado = 'Cancelado'
    WHERE Id_Compra = @Id_Compra AND Saldo <= 0;

    COMMIT TRANSACTION;
END;
GO

CREATE PROCEDURE ConsultaPrincipal
AS
BEGIN
    SELECT Id_Compra, Precio, Saldo, Descripcion, Estado
    FROM Principal
    ORDER BY 
        CASE 
            WHEN Estado = 'Pendiente' THEN 0
            ELSE 1
        END, 
        Id_Compra;
END
GO

SELECT * FROM Principal WHERE Estado = 'Pendiente';
EXEC ObtenerComprasPendientes;

SELECT * FROM Principal;
SELECT * FROM Abonos;

EXEC ConsultaPrincipal;

