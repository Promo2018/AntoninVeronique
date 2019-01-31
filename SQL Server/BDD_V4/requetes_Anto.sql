
use BoVoyage_VAV2;

select * from Dossiers;



select * from dossiers where ID_Dossier = 18;
select * from assurances;
select * from souscrire where ID_Dossier = 18;
select * from participer where ID_Dossier = 18;


Select * from AssurancesDossier(18);

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