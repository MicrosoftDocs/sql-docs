@ECHO OFF
SET InstanceName=%computername%
REM<snippetstartmergesnapshot>
REM -- Declare variables
SET Publisher=%InstanceName%
SET PublicationDB=AdventureWorks2012 
SET Publication=AdvWorksSalesOrdersMerge 

REM --Start the Snapshot Agent to generate the snapshot for AdvWorksSalesOrdersMerge.
"C:\Program Files\Microsoft SQL Server\120\COM\SNAPSHOT.EXE" -Publication %Publication% 
-Publisher %Publisher% -Distributor %Publisher% -PublisherDB %PublicationDB% 
-ReplicationType 2 -OutputVerboseLevel 1 -DistributorSecurityMode 1 
REM</snippetstartmergesnapshot>

PAUSE