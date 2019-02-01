USE BoVoyage_VAV2;

------------FONCTION pour Calculer l'âge des Participants au jour près-------------------
CREATE FUNCTION CalcAge(@ID_P INT)
RETURNS INT
AS 
BEGIN
RETURN (SELECT (DATEDIFF (DAY, (Select DateNaissance from Participants Where ID_Participant = @ID_P) ,GETDATE())/365));
END;
GO
----- Proc pour age autre que celle Antonin

CREATE PROCEDURE MajAge2
AS
	DECLARE C3 CURSOR FOR SELECT ID_Participant FROM Participants 
		OPEN C3;
		DECLARE @Index INT, @Age int;
			FETCH NEXT FROM C3 INTO @Index;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					SET @Age = (SELECT dbo.CalcAge(@Index));
					UPDATE Participants SET Age = @Age WHERE ID_Participant = @Index;
			PRINT @Index;
			FETCH NEXT FROM C3 INTO @Index;
			END;
		CLOSE C3;
	DEALLOCATE C3;
GO



SELECT dbo.CalcAge(1);


------------FONCTION pour Calculer la différence entre le nombre de Places dispo et le nombre de Participant sur Dossier-
--DROP FUNCTION CalcDiffPlace;

CREATE FUNCTION CalcDiffPlace(@ID_D INT)
RETURNS INT
AS 
BEGIN
DECLARE @resultP INT, @nbParticipantDossier INT, @nbPlaceVoyage INT;
SET @nbParticipantDossier = (SELECT COUNT (DISTINCT ID_Participant) FROM Participer P, Dossiers D WHERE D.ID_Dossier = P.ID_Dossier and D.ID_Dossier = @ID_D);
SET @nbPlaceVoyage = (SELECT V.PlacesDisponibles FROM Voyages V, Dossiers D WHERE D.ID_Voyage = V.ID_Voyage and D.ID_Dossier = @ID_D);
SET @resultP = @nbParticipantDossier - @nbPlaceVoyage;
RETURN @resultP ;
END;
GO

SELECT * FROM dbo.CalcDiffPlace(6);

---------------------------------------------PROCEDURE pour repérer les Voyages terminés ---------------------------------
/*
DROP PROCEDURE TagRetour;
CREATE PROCEDURE TagRetour
AS
	DECLARE C1 CURSOR FOR SELECT ID_Dossier FROM Dossiers 
		OPEN C1;
		DECLARE @Index INT, @Critere int;
			FETCH NEXT FROM C1 INTO @Index;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					SELECT @Critere = (DATEDIFF (DAY, (SELECT DateRetour FROM Voyages V, Dossiers D WHERE V.ID_Voyage = D.ID_Voyage and D.ID_Dossier =  @Index) ,GETDATE())/365)
					BEGIN
					IF  @Critere > 0 
						PRINT N'Date non passée';  
					ELSE  
						PRINT @Critere;  
					--UPDATE Dossiers SET EtatDossResa='Refusé' WHERE @Critere < 0 and ID_Dossier = @Index;
					END;
			--PRINT @Index;
			FETCH NEXT FROM C1 INTO @Index;
			END;
		CLOSE C1;
	DEALLOCATE C1;
GO
*/
--------------------------- Procedure pour mettre le dossier en refusé quand date voyage passée------------------------------
CREATE PROCEDURE ChangerStatutDossierDateRetour
AS
	DECLARE C1 CURSOR FOR SELECT ID_Dossier FROM Dossiers 
		OPEN C1;
		DECLARE @Index INT, @Critere int;
			FETCH NEXT FROM C1 INTO @Index;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					SELECT @Critere = (DATEDIFF (DAY, (SELECT DateRetour FROM Voyages V, Dossiers D WHERE V.ID_Voyage = D.ID_Voyage and D.ID_Dossier =  @Index) ,GETDATE())/365)
					BEGIN
					IF  @Critere < 0 
						PRINT N'Date non passée';  
					ELSE  
						UPDATE Dossiers SET EtatDossResa='Refusé' WHERE @Critere < 0 and ID_Dossier = @Index;

					END;
			--PRINT @Index;
			FETCH NEXT FROM C1 INTO @Index;
			END;
		CLOSE C1;
	DEALLOCATE C1;
GO

--------------------------------REQUETES ANTO---------------------------------------

CREATE FUNCTION AssurancesDossier(@Dossier INT)
RETURNS TABLE
AS
RETURN( 
select distinct S.ID_Assurance, A.Type_Assurance
from 
Assurances A, Dossiers Do, 
Voyages V, Souscrire S
where 
Do.ID_Dossier = 18
and A.ID_Assurance = S.ID_Assurance			
and Do.ID_Dossier = S.ID_Dossier
);
GO



Select * from ParticipantsDossier(6);

CREATE FUNCTION ParticipantsDossier(@Dossier INT)
RETURNS TABLE
AS
RETURN( 
select distinct Pnt.Client AS Client, Per.ID_Participant, Pnt.Civilite, Pnt.Prenom, Pnt.Nom, Pnt.Adresse, Pnt.Telephone, Pnt.Email, Pnt.DateNaissance, Pnt.Age
from 
Dossiers Do, Participer Per, Participants Pnt
where 
Do.ID_Dossier = @Dossier
and Do.ID_Dossier = Per.ID_Dossier            
and Per.ID_Participant = Pnt.ID_Participant
);
GO

select * from dossiers;
Select * from VoyageDossier(18);

CREATE FUNCTION VoyageDossier(@Dossier INT)
RETURNS TABLE
AS
RETURN( 
select distinct V.PlacesDisponibles, V.ID_Voyage, V.DateAller, V.DateRetour, V.TarifTTC, V.AgenceVoyage, V.ID_Destination, De.Continent, De.Pays, De.Region, De.DescriptionVoyage
from 
Destinations De, Dossiers Do, Voyages V
where 
Do.ID_Dossier = @Dossier
and Do.ID_Voyage = V.ID_Voyage				
and De.ID_Destination = V.ID_Destination  
);


------------PROCEDURE  pour Calculer la différence entre le nombre de Places dispo et le nombre de Participant sur Dossier direct à partir des selects-

CREATE PROCEDURE MajAge
AS
	DECLARE C1 CURSOR FOR SELECT ID_Participant FROM Participants 
		OPEN C1;
		DECLARE @Index INT, @Now  datetime, @Dob datetime, @Age int;
			FETCH NEXT FROM C1 INTO @Index;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					SELECT @Now=GETDATE(), @Dob=(SELECT DateNaissance FROM Participants WHERE ID_Participant = @Index);
					SELECT @Age = (CONVERT(int,CONVERT(char(8),@Now,112))-CONVERT(char(8),@Dob,112))/10000;
					UPDATE Participants SET Age = @Age WHERE ID_Participant = @Index;
			PRINT @Index;
			FETCH NEXT FROM C1 INTO @Index;
			END;
		CLOSE C1;
	DEALLOCATE C1;
GO

EXECUTE MajAge;

------------PROCEDURE  pour Calculer la différence entre le nombre de Places dispo et le nombre de Participant sur Dossier avec fonction CalcAge-

CREATE PROCEDURE MajAge2
AS
	DECLARE C3 CURSOR FOR SELECT ID_Participant FROM Participants 
		OPEN C3;
		DECLARE @Index INT, @Age int;
			FETCH NEXT FROM C3 INTO @Index;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					SET @Age = (SELECT dbo.CalcAge(@Index));
					UPDATE Participants SET Age = @Age WHERE ID_Participant = @Index;
			PRINT @Index;
			FETCH NEXT FROM C3 INTO @Index;
			END;
		CLOSE C3;
	DEALLOCATE C3;
GO



-------------Possible que si FK est optionné avec ON DELETE CASCADE dans les tables dé)pendantes------------
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

/*
---------------------------VOIR SI ON SUPPRIME CE TRIGGER------------------------------
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
*/
---------------------------TRIGER Suppression Dossier------------------------------
--DROP TRIGGER SuppressionDossier;

CREATE TRIGGER SupprimerDossier ON Dossiers
AFTER INSERT, UPDATE  
AS 
IF exists (SELECT ID_Dossier FROM Dossiers WHERE EtatDossResa = 'Refusé')
BEGIN
DELETE FROM Dossiers WHERE EtatDossResa = 'Refusé'
PRINT 'Dossiers supprimés';
RETURN
END;
GO

---------------------------TRIGER Suppression Dossier------------------------------
--DROP TRIGGER RefuserDossier;

CREATE TRIGGER RefuserDossier ON Dossiers 
AFTER INSERT,  UPDATE 
AS 
DECLARE C2 CURSOR FOR SELECT ID_Dossier FROM Dossiers; 
BEGIN
	OPEN C2;
	DECLARE @IndexDoss INT;
		FETCH NEXT FROM C2 INTO @IndexDoss;
			WHILE (@@FETCH_STATUS=0)
				BEGIN
					IF exists (SELECT * FROM Dossiers D, Voyages V WHERE dbo.CalcDiffPlace(@IndexDoss) < 0 and ID_Dossier = @IndexDoss )
					BEGIN
					UPDATE Dossiers SET EtatDossResa = 'Refusé' WHERE ID_Dossier = @IndexDoss;
					END;
				PRINT @IndexDoss ;
				FETCH NEXT FROM C2 INTO @IndexDoss;
				END;
		CLOSE C2;
DEALLOCATE C2;
RETURN
END;
GO

DROP Function dbo.PrixTotal;

SELECT dbo.PrixTotal(45);


-----------FONCTION CALCUL PRIX TOTAL (A MODIF AVEC ASSURANCE) ----------------------------------------------
CREATE FUNCTION PrixTotal(@idDoss INT)
RETURNS MONEY
AS
BEGIN
DECLARE @prixTot MONEY, @idPart INT, @now DATETIME, @index INT,
@dob DATETIME, @age INT, @nbPartDoss INT, @reduc FLOAT,
@nbPartReduc FLOAT, @tarif MONEY, @assurTot MONEY;
--compter nb participants OK

SET @nbPartDoss = (SELECT COUNT (DISTINCT ID_Participant) 
FROM Participer Per, Dossiers D WHERE D.ID_Dossier = Per.ID_Dossier and D.ID_Dossier = @idDoss);
--selectionner tarifTTC OK
SET @tarif = (SELECT TarifTTC FROM Voyages V, Dossiers D WHERE D.ID_Voyage = V.ID_Voyage
AND D.ID_Dossier = @idDoss);
--calculer age OK
SELECT @now=GETDATE(), @dob=(SELECT DateNaissance FROM Participants WHERE ID_Participant = @idPart);
SELECT @age = (CONVERT(int,CONVERT(char(8),@now,112))-CONVERT(char(8),@dob,112))/10000;
/*
@assurTot = (SELECT SUM(PrixAssurance) FROM Assurances A, Dossiers D, Souscrire S 
WHERE A.ID_Assurance = S.ID_Assurance and S.ID_Dossier = D.ID_Dossier
and D.ID_Dossier = idDoss;
*/
SET @reduc = 0;
--calculer age chacun
DECLARE C20 CURSOR FOR SELECT DISTINCT ID_Participant 
FROM Participer Per, Dossiers D 
WHERE D.ID_Dossier = Per.ID_Dossier and D.ID_Dossier = @idDoss; 
	OPEN C20;
		FETCH NEXT FROM C20 INTO @index;
			WHILE (@@FETCH_STATUS=0)
				BEGIN
					SELECT @now = GETDATE(), @dob=(SELECT DateNaissance FROM Participants WHERE ID_Participant = @index);
					SELECT @age = (CONVERT(int,CONVERT(char(8),@now,112))-CONVERT(char(8),@dob,112))/10000;
					IF @age<12.0 (SELECT @reduc += 1) 
		FETCH NEXT FROM C20 INTO @index;
		END;
	CLOSE C20;
DEALLOCATE C20;
SET @nbPartReduc = ((@nbPartDoss - @reduc)*1) + (@reduc*0.4);
SET @prixTot = @nbPartReduc * @tarif; -- + @assurTot
RETURN (@prixTot);
END;
GO


-------------PROCEDURE POUR MAJ TOUS LES PRIX TOTAUX (fonctionne moyen mais pas trop le temps)---------
CREATE PROCEDURE MajPrixTot
AS
	DECLARE C21 CURSOR FOR SELECT ID_Dossier FROM Dossiers
		OPEN C21;
		DECLARE @IDDoss INT;
			FETCH NEXT FROM C21 INTO @IDDoss;
				WHILE (@@FETCH_STATUS=0)
				BEGIN
					UPDATE Dossiers SET PrixTotal = dbo.PrixTotal(@IDDoss) WHERE ID_Dossier = @IDDoss;
			PRINT @IDDoss;
			FETCH NEXT FROM C21 INTO @IDDoss;
			END;
		CLOSE C21;
	DEALLOCATE C21;
GO


DROP PROCEDURE dbo.MajPrixTot;
Execute dbo.MajPrixTot;


SELECT * FROM Dossiers;
SELECT dbo.PrixTotal(19);