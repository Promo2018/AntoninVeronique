--use BoVoyage_VAV2;
/*
SELECT * FROM Participants;
SELECT * FROM Destinations;
SELECT * FROM Voyages;
SELECT * FROM Assurances;
SELECT COUNT(*) FROM Dossiers;
SELECT * FROM Souscrire;
SELECT * FROM Participer;



CREATE FUNCTION CalcPrix( @ID_P INT)
RETURNS MONEY
AS 
BEGIN
RETURN ;
END;
GO

SELECT dbo.CalcPrix(1);

SELECT * FROM Voyages;
SELECT DateNaissance, Age FROM Participants;
SELECT * FROM Dossiers WHERE ID_Dossier = 8;
SELECT * FROM Participer WHERE ID_Dossier = 8;
SELECT * FROM Souscrire WHERE ID_Dossier = 8;




*/
/*
--FONCTION :  nb de Participant pour un dossier à comparer avec nombre de place + declenchement trigger pour mettre refusé

CREATE FUNCTION CalcDiffPlace(@ID_D INT)
RETURNS INT
AS 
BEGIN
DECLARE @resultP INT, @nbParticipantDossier INT, @nbPlaceVoyage INT;
SET @nbParticipantDossier = (SELECT COUNT (DISTINCT ID_Participant) FROM Participer P, Dossiers D WHERE D.ID_Dossier = P.ID_Dossier and D.ID_Dossier = @ID_D);
SET @nbPlaceVoyage = (SELECT V.PlacesDisponibles FROM Voyages V, Dossiers D WHERE D.ID_Voyage = V.ID_Voyage and D.ID_Dossier = @ID_D);
SET @resultP = @nbPlaceVoyage - @nbParticipantDossier ;
RETURN @resultP;
END;
GO

SELECT dbo.CalcDiffPlace(1);
*/

/* EN COURS ----------------------------------------------------------------------
DROP TRIGGER MettreDossierEnRefuse;

CREATE TRIGGER MettreDossierEnRefuse ON Dossiers
FOR INSERT,  UPDATE 
AS 
DECLARE @ID_D INT;
SELECT @ID_D = ID_Dossier FROM Dossiers;
IF exists (SELECT * FROM Dossiers WHERE dbo.CalcDiffPlace(@ID_D) > 0 and ID_Dossier = @ID_D )
BEGIN
UPDATE Dossiers SET EtatDossResa = 'Refusé' WHERE dbo.CalcDiffPlace(@ID_D) < 0 and ID_Dossier = @ID_D;
PRINT 'Plus de Places disponibles';
RETURN
END;
GO


DROP TRIGGER MettreDossierEnRefuse2;


CREATE TRIGGER MettreDossierEnRefuse2 ON Dossiers 
AFTER INSERT,  UPDATE 
AS 
DECLARE @ID_D INT;
SET @ID_D = IDENT_CURRENT();
IF exists (SELECT * FROM Dossiers D, Voyages V WHERE dbo.CalcDiffPlace(12) < 0 and ID_Dossier = 12 )
BEGIN
UPDATE Dossiers SET EtatDossResa = 'Clos' WHERE ID_Dossier = @ID_D;
PRINT 'Plus de Places disponibles HAHA trop tard' (SELECT @ID_D) ;
RETURN
END;
GO


*/


 DROP  TRIGGER MettreDossierEnRefuse2 ;

CREATE TRIGGER MettreDossierEnRefuse2 ON Dossiers 
AFTER INSERT,  UPDATE 
AS 
declare C1 cursor for SELECT ID_Dossier FROM Dossiers;
open C1;
DECLARE @indexD INT;
fetch next from C1 into @indexD;
WHILE (@@FETCH_STATUS=0)
BEGIN
--SET @ID_D = IDENT_CURRENT();
IF exists (SELECT * FROM Dossiers D, Voyages V WHERE dbo.CalcDiffPlace(@indexD) < 0 and ID_Dossier = @indexD )
BEGIN
UPDATE Dossiers SET EtatDossResa = 'Clos' WHERE ID_Dossier = @indexD;
PRINT 'Plus de Places disponibles HAHA trop tard' (SELECT @indexD) ;
print @indexD;
fetch next from C1 into @indexD;
END;
close C1;
deallocate C1;
RETURN
END;
GO


insert into Dossiers (numeroCB , PrixTotal , RaisonAnnulDoss , EtatDossResa , ID_Voyage, ID_Client ) values ('3573422175830980', '€26721,64', null, 'Supprimé', 89, 20);
insert into Dossiers (numeroCB , PrixTotal , RaisonAnnulDoss , EtatDossResa , ID_Voyage, ID_Client ) values ('3564617237866739', '€57723,82', null, 'Supprimé', 94, 93);


SELECT dbo.CalcDiffPlace(87) ;

SELECT V.ID_Voyage FROM Dossiers D, Voyages V WHERE D.ID_Voyage = V.ID_Voyage and ID_Dossier = 43;
UPDATE Voyages SET PlacesDisponibles = 87  WHERE ID_Voyage = 87 ;

SELECT @@;

Select EtatDossResa, ID_Dossier FROM Dossiers WHERE EtatDossResa = 'Supprimé' ;
UPDATE Dossiers SET EtatDossResa = 'Clos' WHERE ID_Dossier = 41 ;
Select EtatDossResa, ID_Dossier FROM Dossiers WHERE ID_Dossier = 12;
Select EtatDossResa, ID_Dossier FROM Dossiers WHERE EtatDossResa = 'Supprimé' ;

*/

/*

declare C1 cursor for SELECT ID_Dossier FROM Dossiers;
open C1;
declare @indexD nvarchar(32);
fetch next from C1 into @indexD;
WHILE (@@FETCH_STATUS=0)
BEGIN
print @indexD;
close C1;
deallocate C1;
*/
