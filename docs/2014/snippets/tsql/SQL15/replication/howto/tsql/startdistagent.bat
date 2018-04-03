REM -- Starts the Distribution Agent to replicate transactional 
REM -- publication data from AdventureWorks2012 to AdventureWorks2012Replica
REM -- Declare variables
SET DistPub=%computername%
SET Subscriber=%computername%
SET PubDB=AdventureWorks2012
SET SubDB=AdventureWorks2012Replica 

REM --Start the Distribution Agent.
"C:\Program Files\Microsoft SQL Server\90\COM\DISTRIB.EXE" -Subscriber %Subscriber% -SubscriberDB %SubDB% -Publisher %DistPub% -PublisherDB %PubDB% -Distributor %DistPub% -DistributorSecurityMode 1 -Continuous -SubscriptionStreams 4
PAUSE