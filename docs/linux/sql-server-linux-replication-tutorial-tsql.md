---
title: Configure SQL Server replication on Linux | Microsoft Docs
description: This tutorial shows how to configure SQL Server snapshot replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Configure Replication with T-SQL

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)] 

In this tutorial you will configure SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL. The publisher and distributor will be the same instance, and the subscriber will be on a separate instance.

> [!div class="checklist"]
> * Enable SQL Server replication agents on Linux
> * Create a sample database
> * Configure snapshot folder for SQL Server agents access
> * Configure the distributor
> * Configure the publisher
> * Configure publication and articles
> * Configure subscriber 
> * Run the replication jobs

All replication configurations can be configured with [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

## Prerequisites  
To complete this tutorial, you will need:

- Two instances of SQL Server with the latest version of SQL Server on Linux
- A tool to issue T-SQL queries to set up replication such as SQLCMD or SSMS

  See [Use SSMS to Manage SQL Server on Linux](./sql-server-linux-manage-ssms.md).

## Detailed steps

1. Enable SQL Server Replication Agents on Linux
  Enable SQL Server Agent to use Replication Agents. On both host machines, run the following commands in the terminal. 

  ```bash
  sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true 
  sudo systemctl restart mssql-server
  ```

1. Create Sample Database and Table
  On your publisher create a sample database and table that will act as the articles for a publication.

  ```sql
  CREATE DATABASE Sales
  GO
  USE [SALES]
  GO 
  CREATE TABLE CUSTOMER([CustomerID] [int] NOT NULL, [SalesAmount] [decimal] NOT NULL)
  GO 
  INSERT INTO CUSTOMER (CustomerID, SalesAmount) VALUES (1,100),(2,200),(3,300)
  ```

  On the other SQL Server instance, the subscriber, create the database to receive the articles.

  ```sql
  CREATE DATABASE Sales
  GO
  ```

1. Create Snapshot folder for SQL Server Agents to read/write to
  On the distributor, create the snapshot folder and grant access to 'mssql' user 

  ```bash
  sudo mkdir /var/opt/mssql/data/ReplData/
  sudo chown mssql /var/opt/mssql/data/ReplData/
  sudo chgrp mssql /var/opt/mssql/data/ReplData/
  ```

1. Configure distributor
  In this example, the publisher will also be the distributor. Run the following commands on the publisher to configure
the instance for distribution as well.

  ```sql
  DECLARE @distributor AS sysname
  DECLARE @distributorlogin AS sysname
  DECLARE @distributorpassword AS sysname
  -- Specify the distributor name. Use 'hostname' command on in terminal to find the hostname
  SET @distributor = N'<distributor instance name>'--in this example, it will be the name of the publisher
  SET @distributorlogin = N'<distributor login>'
  SET @distributorpassword = N'<distributor password>'
  -- Specify the distribution database. 
  
  use master
  exec sp_adddistributor @distributor = @distributor -- this should be the hostname

  -- Log into distributor and create Distribution Database. In this example, our publisher and distributor is on the same host
  exec sp_adddistributiondb @database = N'distribution', @log_file_size = 2, @deletebatchsize_xact = 5000, @deletebatchsize_cmd = 2000, @security_mode = 0, @login = @distributorlogin, @password = @distributorpassword
  GO

  DECLARE @snapshotdirectory AS nvarchar(500)
  SET @snapshotdirectory = N'/var/opt/mssql/data/ReplData/'

  -- Log into distributor and create Distribution Database. In this example, our publisher and distributor is on the same host
  use [distribution] 
  if (not exists (select * from sysobjects where name = 'UIProperties' and type = 'U ')) 
         create table UIProperties(id int) 
  if (exists (select * from ::fn_listextendedproperty('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', null, null))) 
         EXEC sp_updateextendedproperty N'SnapshotFolder', @snapshotdirectory, 'user', dbo, 'table', 'UIProperties' 
  else 
         EXEC sp_addextendedproperty N'SnapshotFolder', @snapshotdirectory, 'user', dbo, 'table', 'UIProperties'
  GO
  ```

1. Configure publisher
  Run the following TSQL commands on the publisher.

  ```sql
  DECLARE @publisher AS sysname
  DECLARE @distributorlogin AS sysname
  DECLARE @distributorpassword AS sysname
  -- Specify the distributor name. Use 'hostname' command on in terminal to find the hostname
  SET @publisher = N'<instance name>' 
  SET @distributorlogin = N'<distributor login>'
  SET @distributorpassword = N'<distributor password>'
  -- Specify the distribution database. 

  -- Adding the distribution publishers
  exec sp_adddistpublisher @publisher = @publisher, 
  @distribution_db = N'distribution', 
  @security_mode = 0, 
  @login = @distributorlogin, 
  @password = @distributorpassword, 
  @working_directory = N'/var/opt/mssql/data/ReplData', 
  @trusted = N'false', 
  @thirdparty_flag = 0, 
  @publisher_type = N'MSSQLSERVER'
  GO
  ```

1. Configure publication Job
  Run the following TSQL commands on the publisher.

  ```sql
  DECLARE @replicationdb AS sysname
  DECLARE @publisherlogin AS sysname
  DECLARE @publisherpassword AS sysname
  SET @replicationdb = N'Sales'
  SET @publisherlogin = N'<Publisher login>'
  SET @publisherpassword = N'<Publisher Password>'

  use [Sales]
  exec sp_replicationdboption @dbname = N'Sales', @optname = N'publish', @value = N'true'
  
  -- Add the snapshot publication
  exec sp_addpublication 
  @publication = N'SnapshotRepl', 
  @description = N'Snapshot publication of database ''Sales'' from Publisher ''<PUBLISHER HOSTNAME>''.',
  @retention = 0, 
  @allow_push = N'true', 
  @repl_freq = N'snapshot', 
  @status = N'active', 
  @independent_agent = N'true'

  exec sp_addpublication_snapshot @publication = N'SnapshotRepl', 
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
  @publisher_password = @publisherpassword
  ```

1. Create articles from the sales table
  Run the following TSQL commands on the publisher.

  ```sql
  use [Sales]
  exec sp_addarticle 
  @publication = N'SnapshotRepl', 
  @article = N'customer', 
  @source_owner = N'dbo', 
  @source_object = N'customer', 
  @type = N'logbased', 
  @description = null, 
  @creation_script = null, 
  @pre_creation_cmd = N'drop', 
  @schema_option = 0x000000000803509D,
  @identityrangemanagementoption = N'manual', 
  @destination_table = N'customer', 
  @destination_owner = N'dbo', 
  @vertical_partition = N'false'
  ```

1. Configure Subscription
  Run the following TSQL commands on the publisher.

  ```sql
  DECLARE @subscriber AS sysname
  DECLARE @subscriber_db AS sysname
  DECLARE @subscriberLogin AS sysname
  DECLARE @subscriberPassword AS sysname
  SET @subscriber = N'<Instance Name>' -- for example, MSSQLSERVER
  SET @subscriber_db = N'Sales'
  SET @subscriberLogin = N'<Subscriber Login>'
  SET @subscriberPassword = N'<Subscriber Password>'

  use [Sales]
  exec sp_addsubscription 
  @publication = N'SnapshotRepl', 
  @subscriber = @subscriber,
  @destination_db = @subscriber_db, 
  @subscription_type = N'Push', 
  @sync_type = N'automatic', 
  @article = N'all', 
  @update_mode = N'read only', 
  @subscriber_type = 0

  exec sp_addpushsubscription_agent 
  @publication = N'SnapshotRepl', 
  @subscriber = @subscriber,
  @subscriber_db = @subscriber_db, 
  @subscriber_security_mode = 0, 
  @subscriber_login =  @subscriberLogin,
  @subscriber_password =  @subscriberPassword,
  @frequency_type = 1,
  @frequency_interval = 0, 
  @frequency_relative_interval = 0, 
  @frequency_recurrence_factor = 0, 
  @frequency_subday = 0, 
  @frequency_subday_interval = 0, 
  @active_start_time_of_day = 0, 
  @active_end_time_of_day = 0, 
  @active_start_date = 0, 
  @active_end_date = 19950101
  GO
  ```

1. Run Replication Agent Jobs

  Run the following query to get a list of jobs:

  ```sql
  SELECT name, date_modified FROM msdb.dbo.sysjobs order by date_modified desc
  ```

  Run the Snapshot replication job to generate the snapshot:

  ```sql
  USE msdb;  
  --generate snapshot of publications, for example
  EXEC dbo.sp_start_job N'PUBLISHER-PUBLICATION-SnapshotRepl-1'
  GO
  ```

  Run the Snapshot replication job to generate the snapshot:

  ```sql
  USE msdb;  
  --distribute the publication to subscriber, for example
  EXEC dbo.sp_start_job N'DISTRIBUTOR-PUBLICATION-SnapshotRepl-SUBSCRIBER'
  GO
  ```

1. Connect subscriber and query replicated data	

  On the subscriber, check that the replication is working by running the following query:

  ```sql
  SELECT * from [Sales].[dbo].[CUSTOMER]
  ```

In this tutorial, you configured SQL Server snapshot replication on Linux with two instances of SQL Server using Transact-SQL.

> [!div class="checklist"]
> * Enable SQL Server replication agents on Linux
> * Create a sample database
> * Configure snapshot folder for SQL Server agents access
> * Configure the distributor
> * Configure the publisher
> * Configure publication and articles
> * Configure subscriber 
> * Run the replication jobs

## See also

For detailed information about replication, see [SQL Server replication documentation](../relational-databases/replication/sql-server-replication.md).

## Next steps

[Concepts: SQL Server replication on Linux](sql-server-linux-replication.md)

[Replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).
