USE BoVoyage_VAV2;

------------FONCTION pour Calculer l'age des Participants au jour près-------------------
CREATE FUNCTION CalcAge(@ID_P INT)
RETURNS INT
AS 
BEGIN
RETURN (SELECT (DATEDIFF (DAY, (Select DateNaissance from Participants Where ID_Participant = @ID_P) ,GETDATE())/365));
END;
GO

SELECT dbo.CalcAge(1);


------------FONCTION pour Calculer le différence entre le nombre de Places dispo et le nombre de Participant sur Dossier-

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

SELECT dbo.CalcDiffPlace(8);

SELECT DISTINCT P.ID_Participant, P.ID_Dossier FROM Participer P, Dossiers D WHERE D.ID_Dossier = P.ID_Dossier and D.ID_Dossier = 56;

SELECT DISTINCT S.ID_Assurance, S.ID_Dossier FROM Souscrire S, Dossiers D WHERE D.ID_Dossier = S.ID_Dossier and D.ID_Dossier = 18;

