

--CREATE DATABASE BoVoyage_VAV2;

--USE BoVoyage_VAV2;

/*

DROP TABLE Participer;
DROP TABLE Souscrire;
DROP TABLE Dossiers;
DROP TABLE Assurances;
DROP TABLE Voyages;
DROP TABLE Destinations;
DROP TABLE Participants;

*/
-----------------------------------------FOREIGN KEY EXEMPLE


------------------------TABLE PARTICIPANTS-------------------------
CREATE TABLE Participants (
    ID_Participant INT IDENTITY(1,1),
    Civilite NVARCHAR(8) not null,
    Nom NVARCHAR(32) not null,
    Prenom NVARCHAR(32) not null,
	Adresse NVARCHAR(64),
	Telephone NVARCHAR(32),
	DateNaissance DATE,
	Age INT, -- Calculer avec une methode
	Client BIT,
	Email NVARCHAR(64),
	PRIMARY KEY(ID_Participant)
);

------------------------TABLE DESTINATIONS----------------------
CREATE TABLE Destinations  
(
ID_Destination INT IDENTITY(1,1),
Continent NVARCHAR(16) NOT NULL,
Pays NVARCHAR(32) NOT NULL,
Region NVARCHAR(32),
DescriptionVoyage NVARCHAR(512),
PRIMARY KEY(ID_Destination)
)

------------------------TABLE VOYAGES---------------------------
CREATE TABLE Voyages  
(
ID_Voyage INT IDENTITY(1,1),
DateAller DATE NOT NULL,
DateRetour DATE NOT NULL,
PlacesDisponibles INT NOT NULL,
TarifTTC MONEY,
AgenceVoyage NVARCHAR(64),
ID_Destination INT, --FOREIGN KEY
PRIMARY KEY(ID_Voyage)
)

ALTER TABLE Voyages ADD CONSTRAINT Fk_1Destination FOREIGN KEY(ID_Destination) REFERENCES Destinations(ID_Destination);

------------------------TABLE ASSURANCES--------------------------
CREATE TABLE Assurances 
(
ID_Assurance INT IDENTITY(1,1),
Type_Assurance NVARCHAR(64),
PrixAss MONEY,
PRIMARY KEY(ID_Assurance)
)

------------------------TABLE DOSSIERS----------------------------
CREATE TABLE Dossiers  
(
ID_Dossier INT IDENTITY(1,1),
numeroCB NVARCHAR(32) NOT NULL,
PrixTotal MONEY, -- Calculer avec une methode
RaisonAnnulDoss NVARCHAR(32), -- CHECK (RaisonAnnulDoss IN('Solvabilite Client','Agence Voyage','Annulation Client')),
EtatDossResa NVARCHAR(16) NOT NULL, -- CHECK (EtatDossResa IN('En attente','En cours','Refusé','Accepté')),
ID_Voyage INT, --FOREIGN KEY
ID_Client INT, --FOREIGN KEY
PRIMARY KEY(ID_Dossier)
)

ALTER TABLE Dossiers ADD CONSTRAINT Fk_2Client FOREIGN KEY(ID_Client) REFERENCES Participants(ID_Participant);
ALTER TABLE Dossiers ADD CONSTRAINT Fk_3Voyage FOREIGN KEY(ID_Voyage) REFERENCES Voyages(ID_Voyage) ON DELETE CASCADE;
ALTER TABLE Dossiers ADD CONSTRAINT CHK_AnnulDossier_status CHECK (RaisonAnnulDoss ='clients' OR RaisonAnnulDoss='placesInsuffisantes');
ALTER TABLE Dossiers ADD CONSTRAINT CHK_EtatDossResa_status CHECK (EtatDossResa='enAttente' OR EtatDossResa='enCours' OR EtatDossResa='refusee' OR EtatDossResa='acceptee' OR EtatDossResa='Clos' OR EtatDossResa='Supprimé');

------------------------TABLE SOUSCRIRE -------------------------
CREATE TABLE Souscrire  
(
ID_Assurance INT, --FOREIGN KEY
ID_Dossier INT, --FOREIGN KEY
)

ALTER TABLE Souscrire ADD CONSTRAINT Fk_4Assurance FOREIGN KEY(ID_Assurance) REFERENCES Assurances(ID_Assurance);
ALTER TABLE Souscrire ADD CONSTRAINT Fk_5Dossier FOREIGN KEY(ID_Dossier) REFERENCES Dossiers(ID_Dossier) ON DELETE CASCADE;

------------------------TABLE PARTICIPER--------------------------
CREATE TABLE Participer
(
ID_Participant INT, --FOREIGN KEY
ID_Dossier INT, --FOREIGN KEY
)

ALTER TABLE Participer ADD CONSTRAINT Fk_6Participant FOREIGN KEY(ID_Participant) REFERENCES Participants(ID_Participant);
ALTER TABLE Participer ADD CONSTRAINT Fk_7Dossier FOREIGN KEY(ID_Dossier) REFERENCES Dossiers(ID_Dossier) ON DELETE CASCADE;


---------------------VISUALISATION DES TABLES-------------------------------------
SELECT * FROM Participants;
SELECT * FROM Destinations;
SELECT * FROM Voyages;
SELECT * FROM Assurances;
SELECT * FROM Dossiers;
SELECT * FROM Souscrire;
SELECT * FROM Participer;


