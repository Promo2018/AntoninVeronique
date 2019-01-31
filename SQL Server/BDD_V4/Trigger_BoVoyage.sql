use BoVoyage_VAV2;


-------------Possible que si pas de reference dans d'autres tables------------
DROP TRIGGER DossierValide;

CREATE TRIGGER DossierValide ON Dossiers
AFTER INSERT, UPDATE 
AS 
IF exists (SELECT numeroCB FROM Dossiers WHERE LEN(numeroCB) != 16)
BEGIN 
DELETE FROM Dossiers WHERE LEN(numeroCB) != 16;
PRINT 'Les coordonnées bancaires pour ce client sont invalides';
RETURN
END;
GO


SELECT * FROM Dossiers;
SELECT * FROM Participer;
SELECT * FROM Souscrire;
SELECT * FROM Destinations WHERE ID_Destination = 56;

---------------------------TRIGER Suppression Dossier------------------------------
--DROP TRIGGER SuppressionDossier;

CREATE TRIGGER SuppressionDossier ON Dossiers
FOR DELETE 
AS 
IF exists (SELECT ID_Dossier FROM Dossiers WHERE EtatDossResa = 'Refusé')
BEGIN
DELETE FROM Dossiers WHERE EtatDossResa = 'Refusé'
PRINT 'Dossiers supprimés';
RETURN
END;
GO



