@ECHO OFF
SET instancename=GLENGATEST2

REM<snippetbat_syncmergepullsub_10>
REM -- Declare the variables.
SET Publisher=%instancename%
SET Subscriber=%instancename%
SET PublicationDB=AdventureWorks2012
SET SubscriptionDB=AdventureWorks2012Replica 
SET Publication=AdvWorksSalesOrdersMerge

REM --Start the Merge Agent with concurrent upload and download processes.
REM -- The following command must be supplied without line breaks.
"C:\Program Files\Microsoft SQL Server\120\COM\REPLMERG.EXE" -Publication %Publication%  
-Publisher %Publisher%  -Subscriber  %Subscriber%  -Distributor %Publisher%  
-PublisherDB %PublicationDB%  -SubscriberDB %SubscriptionDB% -PublisherSecurityMode 1  
-OutputVerboseLevel 2  -SubscriberSecurityMode 1  -SubscriptionType 1 -DistributorSecurityMode 1  
-Validate 3  -ParallelUploadDownload 1 
REM</snippetbat_syncmergepullsub_10> 

PAUSE
