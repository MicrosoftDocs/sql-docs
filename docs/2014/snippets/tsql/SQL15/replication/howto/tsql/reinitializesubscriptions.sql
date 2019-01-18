:setvar PubServer N'GLENGATEST2' 
:setvar SubServer N'GLENGATEST2' 


-- Merge Pull Subscriptions
--<snippetsp_reinitmergepullsub>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
SET @publicationDB = N'AdventureWorks2012';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Subscriber to reinitialize the pull subscription. 
-- Pending changes at the Subscrber are lost.
EXEC sp_reinitmergepullsubscription 
    @publisher = $(PubServer),
    @publisher_db = @publicationDB,
    @publication = @publication,
    @upload_first = N'false';
GO

-- Start the Merge Agent.

--</snippetsp_reinitmergepullsub>

--<snippetsp_reinitmergepullsubwithupload>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
SET @publicationDB = N'AdventureWorks2012';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Subscriber to reinitialize the pull subscription, 
-- and upload pending changes at the Subscriber. 
EXEC sp_reinitmergepullsubscription 
    @publisher = $(PubServer),
    @publisher_db = @publicationDB,
    @publication = @publication,
    @upload_first = N'true';
GO

-- Start the Merge Agent.

--</snippetsp_reinitmergepullsubwithupload>

-- Merge Push Subscriptions
--<snippetsp_reinitmergepushsub>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @subscriptionDB AS sysname;
DECLARE @publication AS sysname;
SET @subscriptionDB = N'AdventureWorks2012Replica';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Publisher to reinitialize the push subscription. 
-- Pending changes at the Subscrber are lost.
EXEC sp_reinitmergesubscription 
    @subscriber = $(SubServer),
    @subscriber_db = @subscriptionDB,
    @publication = @publication,
    @upload_first = N'false';
GO

-- Start the Merge Agent.

--</snippetsp_reinitmergepushsub>

--<snippetsp_reinitmergepushsubwithupload>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @subscriptionDB AS sysname;
DECLARE @publication AS sysname;
SET @subscriptionDB = N'AdventureWorks2012Replica';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Publisher to reinitialize the push subscription, 
-- and upload pending changes at the Subscriber. 
EXEC sp_reinitmergesubscription 
    @subscriber = $(SubServer),
    @subscriber_db = @subscriptionDB,
    @publication = @publication,
    @upload_first = N'true';
GO

-- Start the Merge Agent.

--</snippetsp_reinitmergepushsubwithupload>

-- Trans push subscription
--<snippetsp_reinittranpushsub>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @subscriptionDB AS sysname;
DECLARE @publication AS sysname;
SET @subscriptionDB = N'AdventureWorks2012Replica';
SET @publication = N'AdvWorksProductTran';

USE [AdventureWorks2012Replica]

-- Execute at the Publisher to reinitialize the push subscription.
EXEC sp_reinitsubscription 
    @subscriber = $(SubServer),
    @destination_db = @subscriptionDB,
    @publication = @publication;
GO

-- Start the Distribution Agent.

--</snippetsp_reinittranpushsub>

--<snippetsp_reinitpullsub>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
SET @publicationDB = N'AdventureWorks2012';
SET @publication = N'AdvWorksProductTran';

USE [AdventureWorks2012Replica]

-- Execute at the Subscriber to reinitialize the pull subscription. 
EXEC sp_reinitpullsubscription 
    @publisher = $(PubServer),
    @publisher_db = @publicationDB,
    @publication = @publication;
GO

-- Start the Distribution Agent.

--</snippetsp_reinitpullsub>