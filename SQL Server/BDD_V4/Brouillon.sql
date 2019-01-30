--use BoVoyage_VAV2;
/*
SELECT * FROM Participants;
SELECT * FROM Destinations;
SELECT * FROM Voyages;
SELECT * FROM Assurances;
SELECT COUNT(*) FROM Dossiers;
SELECT * FROM Souscrire;
SELECT * FROM Participer;

*/
CREATE TRIGGER Dossiervalide ON Dossiers
AFTER INSERT, UPDATE 
AS 
IF exists (SELECT numeroCB FROM Dossiers WHERE LEN(numeroCB) != 16)
BEGIN 
DELETE FROM Dossiers WHERE LEN(numeroCB) != 16;
PRINT 'Les coordonnées bancaires pour ce client sont invalides';
RETURN
END;
GO
