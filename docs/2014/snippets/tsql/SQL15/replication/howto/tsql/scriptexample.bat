REM<snippetbat_example>
REM ----------------------Script to synchronize merge subscription ----------------------
REM -- Creates subscription database and 
REM -- synchronizes the subscription to MergeSalesPerson.
REM -- Current computer acts as both Publisher and Subscriber.
REM -------------------------------------------------------------------------------------

SET Publisher=%computername%
SET Subscriber=%computername%
SET PubDb=AdventureWorks2012
SET SubDb=AdventureWorks2012Replica
SET PubName=AdvWorksSalesOrdersMerge

REM -- Drop and recreate the subscription database at the Subscriber
sqlcmd /S%Subscriber% /E /Q"USE master IF EXISTS (SELECT * FROM sysdatabases WHERE name='%SubDb%' ) DROP DATABASE %SubDb%"
sqlcmd /S%Subscriber% /E /Q"USE master CREATE DATABASE %SubDb%"

REM -- Add a pull subscription at the Subscriber
sqlcmd /S%Subscriber% /E /Q"USE %SubDb% EXEC sp_addmergepullsubscription @publisher = %Publisher%, @publication = %PubName%, @publisher_db = %PubDb%"
sqlcmd /S%Subscriber% /E /Q"USE %SubDb%  EXEC sp_addmergepullsubscription_agent @publisher = %Publisher%, @publisher_db = %PubDb%, @publication = %PubName%, @subscriber = %Subscriber%, @subscriber_db = %SubDb%, @distributor = %Publisher%"

REM -- This batch file starts the merge agent at the Subscriber to 
REM -- synchronize a pull subscription to a merge publication.
REM -- The following must be supplied on one line.
"\Program Files\Microsoft SQL Server\120\COM\REPLMERG.EXE"  -Publisher  %Publisher% -Subscriber  %Subscriber%  -Distributor %Publisher%  -PublisherDB  %PubDb% -SubscriberDB %SubDb% -Publication %PubName% -PublisherSecurityMode 1 -OutputVerboseLevel 1  -Output  -SubscriberSecurityMode 1  -SubscriptionType 1 -DistributorSecurityMode 1 -Validate 3
REM</snippetbat_example>