-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Execute this batch at the Publisher.
DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
SET @publication = N'AdvWorksSalesOfferMergeFtp';
SET @subscriber = $(SubServer);
SET @subscriptionDB = N'AdventureWorksReplica';

-- At the Publisher, register the subscription, using the defaults.
EXEC sp_addmergesubscription 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscriptionDB, 
	@subscription_type = N'pull', 
	@subscriber_type = N'local', 
	@subscription_priority = 0, 
	@sync_type = N'Automatic';
GO