REM<snippetsp_MergeDynGenSnapshot_10>
REM Line breaks are added to improve readability. 
REM In a batch file, commands must be made in a single line.
REM Run the Snapshot agent from the command line to generate the standard snapshot 
REM schema and other files. 
SET DistPub=%computername%
SET PubDB=AdventureWorks2012 
SET PubName=AdvWorksSalesPersonMerge

"C:\Program Files\Microsoft SQL Server\120\COM\SNAPSHOT.EXE" -Publication %PubName%  
-Publisher %DistPub% -Distributor  %DistPub%  -PublisherDB %PubDB%  -ReplicationType 2  
-OutputVerboseLevel 1  -DistributorSecurityMode 1

PAUSE
REM</snippetsp_MergeDynGenSnapshot_10>

REM<snippetsp_MergeDynGenPartSnapshot_10>
REM Run the Snapshot agent from the command line, this time to generate 
REM the bulk copy (.bcp) data for each Subscriber partition.  
SET DistPub=%computername%
SET PubDB=AdventureWorks2012 
SET PubName=AdvWorksSalesPersonMerge
SET SnapshotDir=\\%DistPub%\repldata\unc\fernando

MD %SnapshotDir%

"C:\Program Files\Microsoft SQL Server\120\COM\SNAPSHOT.EXE" -Publication %PubName%  
-Publisher %DistPub%  -Distributor  %DistPub%  -PublisherDB %PubDB%  -ReplicationType 2  
-OutputVerboseLevel 1  -DistributorSecurityMode 1  -DynamicFilterHostName "adventure-works\Fernando"  
-DynamicSnapshotLocation %SnapshotDir%

PAUSE
REM</snippetsp_MergeDynGenPartSnapshot_10>

REM<snippetsp_MergeDynRunPartMerge_10>
REM Run the Merge Agent for each subscription to apply the partitioned 
REM snapshot for each Subscriber.  
SET Publisher = %computername%
SET Subscriber = %computername%
SET PubDB = AdventureWorks2012 
SET SubDB = AdventureWorks2012Replica 
SET PubName = AdvWorksSalesPersonMerge 
SET SnapshotDir=\\%DistPub%\repldata\unc\fernando

"C:\Program Files\Microsoft SQL Server\120\COM\REPLMERG.EXE" -Publisher  %Publisher%  
-Subscriber  %Subscriber%  -Distributor %Publisher%  -PublisherDB %PubDB%  
-SubscriberDB %SubDB% -Publication %PubName%  -PublisherSecurityMode 1  -OutputVerboseLevel 3  
-Output -SubscriberSecurityMode 1  -SubscriptionType 3 -DistributorSecurityMode 1  
-Hostname "adventure-works\Fernando"  -DynamicSnapshotLocation %SnapshotDir%

PAUSE
REM</snippetsp_MergeDynRunPartMerge_10>