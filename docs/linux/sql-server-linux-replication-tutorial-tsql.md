---
title: "Tutorial: Configure replication (T-SQL)"
titleSuffix: SQL Server on Linux
description: Configure SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL (T-SQL).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Configure Replication with T-SQL

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

In this tutorial, configure SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL (T-SQL). The publisher and distributor are on the same instance, and the subscriber is on a separate instance.

> [!div class="checklist"]
> - Enable SQL Server replication agents on Linux
> - Create a sample database
> - Configure snapshot folder for SQL Server agents access
> - Configure the distributor
> - Configure the publisher
> - Configure publication and articles
> - Configure subscriber  
> - Run the replication jobs

All replication configurations can be configured with [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

## Prerequisites

To complete this tutorial, you need:

- Two instances of SQL Server with the latest version of SQL Server on Linux
- A tool to issue T-SQL queries to set up replication, such as [sqlcmd](../tools/sqlcmd/sqlcmd-utility.md) or [SQL Server Management Studio](../ssms/sql-server-management-studio-ssms.md)

  See [Use SQL Server Management Studio on Windows to manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

  > [!NOTE]  
  > SQL Server Replication is supported on Linux in [!INCLUDE [SQL Server 2017](../includes/sssql17-md.md)] ([CU18](https://support.microsoft.com/help/4527377)) and later versions.

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

   CREATE TABLE Customer (
       [CustomerID] INT NOT NULL,
       [SalesAmount] DECIMAL NOT NULL
   );
   GO

   INSERT INTO Customer (CustomerID, SalesAmount)
   VALUES (1, 100), (2, 200), (3, 300);
   GO
   ```

   On the other SQL Server instance, the subscriber, create the database to receive the articles.

   ```sql
   CREATE DATABASE Sales;
   GO
   ```

1. Create the snapshot folder for SQL Server Agents to read/write to on the distributor, create the snapshot folder and grant access to 'mssql' user

   ```bash
   sudo mkdir /var/opt/mssql/data/ReplData/
   sudo chown mssql /var/opt/mssql/data/ReplData/
   sudo chgrp mssql /var/opt/mssql/data/ReplData/
   ```

1. Configure distributor. In this example, the publisher is also the distributor. Run the following commands on the publisher to configure the instance for distribution as well.

   ```sql
   DECLARE @distributor AS SYSNAME;
   DECLARE @distributorlogin AS SYSNAME;
   DECLARE @distributorpassword AS SYSNAME;

   -- Specify the distributor name. Use 'hostname' command on in terminal to find the hostname
   SET @distributor = N'<distributor instance name>'; --in this example, it will be the name of the publisher
   SET @distributorlogin = N'<distributor login>';
   SET @distributorpassword = N'<distributor password>';

   -- Specify the distribution database.
   USE master

   EXEC sp_adddistributor @distributor = @distributor -- this should be the hostname

   -- Log into distributor and create Distribution Database. In this example, our publisher and distributor is on the same host
   EXEC sp_adddistributiondb @database = N'distribution',
       @log_file_size = 2,
       @deletebatchsize_xact = 5000,
       @deletebatchsize_cmd = 2000,
       @security_mode = 0,
       @login = @distributorlogin,
       @password = @distributorpassword;
   GO

   DECLARE @snapshotdirectory AS NVARCHAR(500);
   SET @snapshotdirectory = N'/var/opt/mssql/data/ReplData/';

   -- Log into distributor and create Distribution Database. In this example, our publisher and distributor is on the same host
   USE [distribution];
   GO

   IF (NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'UIProperties' AND type = 'U'))
       CREATE TABLE UIProperties (id INT);

   IF (EXISTS (SELECT * FROM::fn_listextendedproperty('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', NULL, NULL)))
       EXEC sp_updateextendedproperty N'SnapshotFolder',
           @snapshotdirectory,
           'user',
           dbo,
           'table',
           'UIProperties';
   ELSE
       EXEC sp_addextendedproperty N'SnapshotFolder',
           @snapshotdirectory,
           'user',
           dbo,
           'table',
           'UIProperties';
   GO
   ```

1. Configure publisher. Run the following T-SQL commands on the publisher.

   ```sql
   DECLARE @publisher AS SYSNAME;
   DECLARE @distributorlogin AS SYSNAME;
   DECLARE @distributorpassword AS SYSNAME;

   -- Specify the distributor name. Use 'hostname' command on in terminal to find the hostname
   SET @publisher = N'<instance name>';
   SET @distributorlogin = N'<distributor login>';
   SET @distributorpassword = N'<distributor password>';

   -- Specify the distribution database.
   -- Adding the distribution publishers
   EXEC sp_adddistpublisher @publisher = @publisher,
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

1. Configure publication job. Run the following T-SQL commands on the publisher.

   ```sql
   DECLARE @replicationdb AS SYSNAME;
   DECLARE @publisherlogin AS SYSNAME;
   DECLARE @publisherpassword AS SYSNAME;

   SET @replicationdb = N'Sales';
   SET @publisherlogin = N'<Publisher login>';
   SET @publisherpassword = N'<Publisher Password>';

   USE [Sales];
   GO

   EXEC sp_replicationdboption @dbname = N'Sales',
       @optname = N'publish',
       @value = N'true';

   -- Add the snapshot publication
   EXEC sp_addpublication @publication = N'SnapshotRepl',
       @description = N'Snapshot publication of database ''Sales'' from Publisher ''<PUBLISHER HOSTNAME>''.',
       @retention = 0,
       @allow_push = N'true',
       @repl_freq = N'snapshot',
       @status = N'active',
       @independent_agent = N'true';

   EXEC sp_addpublication_snapshot @publication = N'SnapshotRepl',
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

1. Create articles from the Sales table.

   Run the following T-SQL commands on the publisher.

   ```sql
   USE [Sales];
   GO

   EXEC sp_addarticle @publication = N'SnapshotRepl',
       @article = N'customer',
       @source_owner = N'dbo',
       @source_object = N'customer',
       @type = N'logbased',
       @description = NULL,
       @creation_script = NULL,
       @pre_creation_cmd = N'drop',
       @schema_option = 0x000000000803509D,
       @identityrangemanagementoption = N'manual',
       @destination_table = N'customer',
       @destination_owner = N'dbo',
       @vertical_partition = N'false';
   ```

1. Configure Subscription. Run the following T-SQL commands on the publisher.

   ```sql
   DECLARE @subscriber AS SYSNAME;
   DECLARE @subscriber_db AS SYSNAME;
   DECLARE @subscriberLogin AS SYSNAME;
   DECLARE @subscriberPassword AS SYSNAME;

   SET @subscriber = N'<Instance Name>'; -- for example, MSSQLSERVER
   SET @subscriber_db = N'Sales';
   SET @subscriberLogin = N'<Subscriber Login>';
   SET @subscriberPassword = N'<Subscriber Password>';

   USE [Sales];
   GO

   EXEC sp_addsubscription @publication = N'SnapshotRepl',
       @subscriber = @subscriber,
       @destination_db = @subscriber_db,
       @subscription_type = N'Push',
       @sync_type = N'automatic',
       @article = N'all',
       @update_mode = N'read only',
       @subscriber_type = 0;

   EXEC sp_addpushsubscription_agent @publication = N'SnapshotRepl',
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
   SELECT name, date_modified
   FROM msdb.dbo.sysjobs
   ORDER BY date_modified DESC;
   ```

   Run the Snapshot replication job to generate the snapshot:

   ```sql
   USE msdb;
   GO

   --generate snapshot of publications, for example
   EXEC dbo.sp_start_job N'PUBLISHER-PUBLICATION-SnapshotRepl-1';
   GO
   ```

   Run the snapshot replication job to generate the snapshot:

   ```sql
   USE msdb;
   GO
   --distribute the publication to subscriber, for example
   EXEC dbo.sp_start_job N'DISTRIBUTOR-PUBLICATION-SnapshotRepl-SUBSCRIBER';
   GO
   ```

1. Connect subscriber and query replicated data.

   On the subscriber, check that the replication is working by running the following query:

   ```sql
   SELECT * from [Sales].[dbo].[Customer];
   ```

In this tutorial, you configured SQL Server snapshot replication on Linux with two instances of SQL Server using T-SQL.

> [!div class="checklist"]
> - Enable SQL Server replication agents on Linux
> - Create a sample database
> - Configure snapshot folder for SQL Server agents access
> - Configure the distributor
> - Configure the publisher
> - Configure publication and articles
> - Configure subscriber  
> - Run the replication jobs

## Related content

- [SQL Server Replication](../relational-databases/replication/sql-server-replication.md)
- [SQL Server replication on Linux](sql-server-linux-replication.md)
- [Replication stored procedures (Transact-SQL)](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)
