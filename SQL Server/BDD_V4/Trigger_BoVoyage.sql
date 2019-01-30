use BoVoyage_VAV2;

/*
-------------Possible que si pas de reference dans d'autres tables------------
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

insert into Dossiers (numeroCB , PrixTotal , RaisonAnnulDoss , EtatDossResa , ID_Voyage, ID_Client ) values ('372301389090996', '€17726,68', null, 'Supprimé', 17, 84);
insert into Dossiers (numeroCB , PrixTotal , RaisonAnnulDoss , EtatDossResa , ID_Voyage, ID_Client ) values ('5610746400333852681', '€22564,23', null, 'Accepté', 87, 14);

SELECT * FROM Dossiers;
SELECT * FROM Participer;
SELECT * FROM Souscrire;
SELECT * FROM Destinations WHERE ID_Destination = 56;

---------------------------TRIGER Suppression Dossier------------------------------
--DROP TRIGGER SuppressionDossier;

CREATE TRIGGER SuppressionDossier ON Dossiers
FOR DELETE 
AS 
IF exists (SELECT ID_Dossier FROM Dossiers WHERE EtatDossResa = 'Supprimé')
BEGIN
DELETE FROM Dossiers WHERE EtatDossResa = 'Supprimé'
PRINT 'Dossiers supprimés';
RETURN
END;
GO


