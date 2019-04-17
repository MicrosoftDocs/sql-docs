SET instancename=GLENGATEST2

REM<snippetbat_syncmergepushsub>
REM -- Declare the variables.
SET Publisher=%instancename%
SET Subscriber=%instancename%
SET PublicationDB=AdventureWorks2012
SET SubscriptionDB=AdventureWorks2012Replica 
SET Publication=AdvWorksSalesOrdersMerge

REM -- Start the Merge Agent.
REM -- The following command must be supplied without line breaks.
"C:\Program Files\Microsoft SQL Server\90\COM\REPLMERG.EXE"  -Publisher %Publisher% 
-Subscriber  %Subscriber%  -Distributor %Publisher% -PublisherDB  %PublicationDB% 
-SubscriberDB %SubscriptionDB% -Publication %Publication% -PublisherSecurityMode 1 
-OutputVerboseLevel 3  -Output -SubscriberSecurityMode 1  -SubscriptionType 0 
-DistributorSecurityMode 1 
REM</snippetbat_syncmergepushsub>

PAUSE