
--USE BoVoyage_VA;

--FONCTION recherche nombre de champs + liste noms des champs;
--DROP FUNCTION Champs;

CREATE FUNCTION Champs(@bdd nvarchar(32),@tablette nvarchar(32))
RETURNS TABLE
AS
RETURN (SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_CATALOG = @bdd AND TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = @tablette);
GO

/*
CREATE FUNCTION ListeParams()

DECLARE @var_a nvarchar(32);

RETURNS  
AS
RETURN ();
GO

select * from Champs('BoVoyage_VA','Individus');
select COUNT(*) from Champs('BoVoyage_VA','Individus');


--###########################
-- Pour insérer des données
CREATE PROCEDURE Insertion()

AS


GO

SET IDENTITY_INSERT Individus OFF

INSERT into Individus (select * from Champs('BoVoyage_VA','Individus')) values (Mme,Krtefee,Reanie,2000-05-21,5 rue de la grue,0798562485);



select * from Individus;
*/

declare @champs nvarchar(32);
declare C1 cursor for select * from Champs('BoVoyage_VA','Individus');
open C1;
fetch next from C1 into @champs;
print @champs;
close C1;
deallocate C1;