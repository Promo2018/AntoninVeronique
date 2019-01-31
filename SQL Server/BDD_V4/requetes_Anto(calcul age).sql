/*
use BoVoyage_VAV2;

select * from Dossiers;



select * from dossiers where ID_Dossier = 18;
select * from assurances;
select * from souscrire where ID_Dossier = 18;
select * from participer where ID_Dossier = 18;


Select * from AssurancesDossier(28);

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



------------FONCTION pour Calculer l'age des Participants au jour près-------------------
CREATE FUNCTION CalcAge(@ID_P INT)
RETURNS INT
AS 
BEGIN
RETURN 
(


);
END;
GO

SELECT dbo.CalcAge(3);

select * from participants;


where ID_Participant = 17;
*/


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



Select * From Participants;

UPDATE Participants SET DateNaissance = '01/01/1967' WHERE ID_Participant = 103;