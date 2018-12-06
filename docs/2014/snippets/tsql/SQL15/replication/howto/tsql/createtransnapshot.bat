REM -- Generate the snapshot for AdvWorksSalesOrdersMerge
REM -- Declare variables
SET DistPub=%computername%
SET PubDB=AdventureWorks2012 
SET PubName=AdvWorksProductTran 
SET SnapshotFolder=%computername%\repldata

REM --Start the Snapshot Agent to generate the snapshot for AdvWorksProductTran.
"C:\Program Files\Microsoft SQL Server\90\COM\SNAPSHOT.EXE" -Publication %PubName%  -Publisher %DistPub%  -Distributor  %DistPub%  -PublisherDB %PubDB%  -ReplicationType 1  -OutputVerboseLevel 2  -DistributorSecurityMode 1
PAUSE