---
title: "Tutorial: Configure Replication (Transact-SQL)"
titleSuffix: SQL Server on Linux
description: Configure SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL (T-SQL).
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Configure replication with Transact-SQL

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

In this tutorial, configure SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL (T-SQL). The publisher and distributor are on the same instance, and the subscriber is on a separate instance.

> [!div class="checklist"]
> - Enable SQL Server replication agents on Linux
> - Create a sample database
> - Configure snapshot folder for SQL Server agents access
> - Configure the distributor
> - Configure the publisher
> - Configure the publication and articles
> - Configure the subscriber  
> - Run the replication jobs

You can configure all replication components with [replication stored procedures](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

## Prerequisites

To complete this tutorial, you need:

- Two instances of SQL Server with the latest version of SQL Server on Linux
- A tool to issue T-SQL queries to set up replication, such as [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md) or [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms)

  See [Use SQL Server Management Studio on Windows to manage SQL Server on Linux](../sql-server-linux-manage-ssms.md).

  > [!NOTE]  
  > SQL Server Replication is supported on Linux in [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)] ([CU 18](/troubleshoot/sql/releases/sqlserver-2017/cumulativeupdate18)) and later versions.

## Detailed steps

1. Enable SQL Server replication agents on Linux. On both host machines, run the following commands in the terminal.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
   sudo systemctl restart mssql-server
   ```

1. Create the sample database and table. On the publisher, create a sample database and table that will act as the articles for a publication.

   ```sql
   CREATE DATABASE Sales;
   GO

   USE [Sales];
   GO

   CREATE TABLE Customer
   (
       [CustomerID] INT NOT NULL,
       [SalesAmount] DECIMAL NOT NULL
   );
   GO

   INSERT INTO Customer (CustomerID, SalesAmount)
   VALUES (1, 100),
       (2, 200),
       (3, 300);
   GO
   ```

   On the other SQL Server instance, the subscriber, create the database to receive the articles.

   ```sql
   CREATE DATABASE Sales;
   GO
   ```

1. On the distributor, create the snapshot folder for SQL Server agents to read from and write to, and grant access to the `mssql` user:

   ```bash
   sudo mkdir /var/opt/mssql/data/ReplData/
   sudo chown mssql /var/opt/mssql/data/ReplData/
   sudo chgrp mssql /var/opt/mssql/data/ReplData/
   ```

1. Configure the distributor. In this example, the publisher is also the distributor. Run the following commands on the publisher to configure the instance for distribution as well.

   ```sql
   DECLARE @distributor AS SYSNAME;
   DECLARE @distributorlogin AS SYSNAME;
   DECLARE @distributorpassword AS SYSNAME;

   -- Specify the distributor name. Use the 'hostname' command in the terminal to find the hostname.
   SET @distributor = N'<distributor instance name>'; -- In this example, it will be the name of the publisher
   SET @distributorlogin = N'<distributor login>';
   SET @distributorpassword = N'<distributor password>';

   -- Specify the distribution database.
   USE master;

   EXECUTE sp_adddistributor
       @distributor = @distributor; -- this should be the hostname

   -- Log into the distributor and create the distribution database.
   -- In this example, the publisher and distributor are on the same host.
   EXECUTE sp_adddistributiondb
       @database = N'distribution',
       @log_file_size = 2,
       @deletebatchsize_xact = 5000,
       @deletebatchsize_cmd = 2000,
       @security_mode = 0,
       @login = @distributorlogin,
       @password = @distributorpassword;
   GO

   -- Log into the distributor and configure the snapshot directory.
   -- In this example, the publisher and distributor are on the same host.
   USE [distribution];
   GO

   DECLARE @snapshotdirectory AS NVARCHAR (500) = N'/var/opt/mssql/data/ReplData/';

   IF (NOT EXISTS (SELECT * FROM sysobjects
       WHERE name = 'UIProperties' AND type = 'U'))
   CREATE TABLE UIProperties(id INT);

   IF (EXISTS (SELECT *
               FROM ::fn_listextendedproperty ('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', NULL, NULL)))
       EXECUTE sp_updateextendedproperty N'SnapshotFolder', @snapshotdirectory, 'user', dbo, 'table', 'UIProperties';
   ELSE
       EXECUTE sp_addextendedproperty N'SnapshotFolder', @snapshotdirectory, 'user', dbo, 'table', 'UIProperties';
   GO
   ```

1. Configure the publisher. Run the following T-SQL commands on the publisher.

   ```sql
   DECLARE @publisher AS SYSNAME;
   DECLARE @distributorlogin AS SYSNAME;
   DECLARE @distributorpassword AS SYSNAME;

   -- Specify the publisher name. Use the 'hostname' command in the terminal to find the hostname.
   SET @publisher = N'<instance name>';
   SET @distributorlogin = N'<distributor login>';
   SET @distributorpassword = N'<distributor password>';

   -- Specify the distribution database.
   -- Adding the distribution publishers
   EXECUTE sp_adddistpublisher
       @publisher = @publisher,
       @distribution_db = N'distribution',
       @security_mode = 0,
       @login = @distributorlogin,
       @password = @distributorpassword,
       @working_directory = N'/var/opt/mssql/data/ReplData',
       @trusted = N'false',
       @thirdparty_flag = 0,
       @publisher_type = N'MSSQLSERVER';
   GO
   ```

1. Configure the publication and snapshot agent job. Run the following T-SQL commands on the publisher.

   ```sql
   USE [Sales];
   GO

   DECLARE @publisherlogin AS SYSNAME;
   DECLARE @publisherpassword AS SYSNAME;

   SET @publisherlogin = N'<publisher login>';
   SET @publisherpassword = N'<publisher password>';

   EXECUTE sp_replicationdboption
       @dbname = N'Sales',
       @optname = N'publish',
       @value = N'true';

   -- Add the snapshot publication
   EXECUTE sp_addpublication
       @publication = N'SnapshotRepl',
       @description = N'Snapshot publication of database ''Sales'' from Publisher ''<PUBLISHER HOSTNAME>''.',
       @retention = 0,
       @allow_push = N'true',
       @repl_freq = N'snapshot',
       @status = N'active',
       @independent_agent = N'true';

   EXECUTE sp_addpublication_snapshot
       @publication = N'SnapshotRepl',
       @frequency_type = 1,
       @frequency_interval = 1,
       @frequency_relative_interval = 1,
       @frequency_recurrence_factor = 0,
       @frequency_subday = 8,
       @frequency_subday_interval = 1,
       @active_start_time_of_day = 0,
       @active_end_time_of_day = 235959,
       @active_start_date = 0,
       @active_end_date = 0,
       @publisher_security_mode = 0,
       @publisher_login = @publisherlogin,
       @publisher_password = @publisherpassword;
   ```

1. Create the `Customer` article from the `Customer` table.

   Run the following T-SQL commands on the publisher.

   ```sql
   USE [Sales];
   GO

   EXECUTE sp_addarticle
       @publication = N'SnapshotRepl',
       @article = N'Customer',
       @source_owner = N'dbo',
       @source_object = N'Customer',
       @type = N'logbased',
       @description = NULL,
       @creation_script = NULL,
       @pre_creation_cmd = N'drop',
       @schema_option = 0x000000000803509D,
       @destination_table = N'Customer',
       @destination_owner = N'dbo',
       @identityrangemanagementoption = N'manual',
       @vertical_partition = N'false';
   ```

1. Configure the subscription. Run the following T-SQL commands on the publisher.

   ```sql
   USE [Sales];
   GO

   DECLARE @subscriber AS SYSNAME;
   DECLARE @subscriber_db AS SYSNAME;
   DECLARE @subscriberLogin AS SYSNAME;
   DECLARE @subscriberPassword AS SYSNAME;

   SET @subscriber = N'<instance name>'; -- for example, MSSQLSERVER
   SET @subscriber_db = N'Sales';
   SET @subscriberLogin = N'<subscriber login>';
   SET @subscriberPassword = N'<subscriber password>';

   EXECUTE sp_addsubscription
       @publication = N'SnapshotRepl',
       @subscriber = @subscriber,
       @destination_db = @subscriber_db,
       @subscription_type = N'Push',
       @sync_type = N'automatic',
       @article = N'all',
       @update_mode = N'read only',
       @subscriber_type = 0;

   EXECUTE sp_addpushsubscription_agent
       @publication = N'SnapshotRepl',
       @subscriber = @subscriber,
       @subscriber_db = @subscriber_db,
       @subscriber_security_mode = 0,
       @subscriber_login = @subscriberLogin,
       @subscriber_password = @subscriberPassword,
       @frequency_type = 1,
       @frequency_interval = 0,
       @frequency_relative_interval = 0,
       @frequency_recurrence_factor = 0,
       @frequency_subday = 0,
       @frequency_subday_interval = 0,
       @active_start_time_of_day = 0,
       @active_end_time_of_day = 0,
       @active_start_date = 0,
       @active_end_date = 19950101;
   GO
   ```

1. Run replication agent jobs. Run the following query to get a list of jobs:

   ```sql
   SELECT name,
          date_modified
   FROM msdb.dbo.sysjobs
   ORDER BY date_modified DESC;
   ```

   Start the snapshot agent job to generate the snapshot:

   ```sql
   USE msdb;
   GO

   -- Generate the publication snapshot, for example.
   EXECUTE dbo.sp_start_job N'PUBLISHER-PUBLICATION-SnapshotRepl-1';
   GO
   ```

   Start the distribution agent job to distribute the publication to the subscriber:

   ```sql
   USE msdb;
   GO

   -- Distribute the publication to the subscriber.
   EXECUTE dbo.sp_start_job N'DISTRIBUTOR-PUBLICATION-SnapshotRepl-SUBSCRIBER';
   GO
   ```

1. Connect to the subscriber and query the replicated data.

   On the subscriber, check that the replication is working by running the following query:

   ```sql
   SELECT *
   FROM [Sales].[dbo].[Customer];
   ```

In this tutorial, you configured SQL Server snapshot replication on Linux with two instances of SQL Server using T-SQL.

> [!div class="checklist"]
> - Enable SQL Server replication agents on Linux
> - Create a sample database
> - Configure snapshot folder for SQL Server agents access
> - Configure the distributor
> - Configure the publisher
> - Configure the publication and articles
> - Configure the subscriber  
> - Run the replication jobs

## Related content

- [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)
- [SQL Server replication on Linux](overview.md)
- [Replication stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)
