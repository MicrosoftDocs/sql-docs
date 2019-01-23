---
title: "Configure SQL Server distribution database in availability group | Microsoft Docs"
ms.custom: ""
ms.date: "01/16/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], distribution"
  - "distribution configuration [SQL Server replication]"
  - "remote Distributors [SQL Server replication]"
  - "transactional replication, configuring distribution"
  - "distribution databases [SQL Server replication], sizing"
  - "Distributors [SQL Server replication], configuring"
  - "distribution databases [SQL Server replication], about distribution databases"
  - "distribution databases [SQL Server replication]"
  - "merge replication [SQL Server replication], configuring distribution"
ms.assetid: 94d52169-384e-4885-84eb-2304e967d9f7
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set up replication distribution database in Always On availability group

This article explains how to set up a SQL Server replication distribution databases in an Always On availability group (AG).

SQL Server 2017 CU6 and SQL Server 2016 SP2-CU3 introduces support for replication distribution database in an AG through the following mechanisms:

- The distribution database AG needs to have a listener. When the publisher adds the distributor, it uses the listener name as the distributor name.
- The replication jobs are created with the listener name as the distributor name. Replication snapshot, log reader and distribution agent (push subscription) jobs created on the distribution server gets created on all secondary replicas of the AG for Distribution DB.
 >[!NOTE]
 >Distribution agent jobs for pull susbcriptions are created on the subscriber server and not on the distribution server. 
- A new job monitors the state (primary or secondary in AG) of the distribution databases and disables or enables the replication jobs based on the distribution databases state.

After a distribution database in the AG is configured based on the steps described below, replication configuration and run time jobs can run properly before and after distribution database AG failover.

## Supported scenarios

- Configuring distribution database to be included in an AG.
- Configuring replication such as publications and subscriptions before and after AG failover.
- Replication jobs functional before and after failover.
- Removing replication at distributor and publisher when distribution database is in AG.
- Adding or removing nodes to existing distribution database AG.
- A distributor may have multiple distribution databases. Each distribution database can be in its own AG and can be not in any AG. Multiple distribution databases can share an AG.
- Publisher and distributor need to be on separate SQL Server instances.
- If the listener for the availability group hosting the distribution database is configured to use a non-default port, then its required to setup an alias for the listener and the non-default port.

## Limitations or exclusions

- Local distributor is not supported. For example, publisher and distributor must be different SQL Server instances. A publisher using itself as distributor (a.k.a. local distributor) cannot support distribution databases in an AG.
- Oracle publisher is not supported.
- Merge replication is not supported.
- Transactional replication with immediate or queued updating subscriber is not supported.
- Peer to peer replication is not supported.
- All SQL Server instances hosting distribution database replicas must be SQL Server 2017 CU 6 or later. 
- All SQL Server instances hosting distribution database replicas must be the same version, except during the narrow timeframe when upgrade takes place.
- The distribution database must be in full recovery mode.
- For recovery and to allow transaction log truncation, configure full and transaction log backups.
- The distribution database AG must have a listener configured.
- Secondary replicas in a distribution database AG can be synchronous or asynchronous. Synchronous mode is recommended and preferred.
- Bidirectional transactional replication is not supported.
- SSMS does not show Distribution Database as synchronizing/synchronized, when distribution database is added to an availability group.


   >[!NOTE]
   >Before executing any of replication stored procedures (for example - `sp_dropdistpublisher`, `sp_dropdistributiondb`, `sp_dropdistributor`, `sp_adddistributiondb`, `sp_adddistpublisher`) on secondary replica, make sure the replica is fully synchronized.

- All secondary replicas in a distribution database AG must be readable.
- All the nodes in the distribution database AG need to use the same domain account to run SQL Server Agent, and this domain account needs to have the same privilege on each node.
- If any replication agents run under a proxy account, the proxy account needs to exist in every node in the distribution database AG and have the same privilege on each node.
- Make changes to distributor or distribution database properties in all replicas participating in distribution database AG.
- Make replication jobs changes through msdb stored procedures or SQL Server Management Studio in all replicas participating in distribution database AG.
- Configuring distributor on the publisher needs to be done with scripts. The replication wizard cannot be used. Replication wizards and property sheets for other purposes are supported.
- Configuring the AG for distribution databases can only be done through scripts.
- Setting up distribution databases in an AG needs to be a new replication configuration. Switching an existing distribution database to an AG is not supported. Also once a distribution database is taken out an AG, it can no longer function as a valid distribution database and should be dropped.

## Configuration architecture

The following server names and settings are used in the examples in this article.

- DIST1, DIST2, DIST3 are distributor servers;
- PUB is publisher server;
- After distribution database AG is formed, the listener name is DISTLISTENER;
- DIST1 is intended to be the initial primary replica of distribution database AG.

## Configure distributor, distribution database, and publisher

This example configures a new distributor and publisher and puts the distribution database in an AG.

### Distributors workflow

1. Configure DIST1, DIST2, DIST3 as distributor with `sp_adddistributor @@servername`. Specify the password for `distributor_admin` through the `@password`. The `@password` should be identical across DIST1, DIST2, DIST3.
2. Create the distribution database on DIST1 with `sp_adddistributiondb`. The name of the distribution database is `distribution`. Change the recovery mode of `distribution` database from simple to full.
3. Create an AG for `distribution` database with replicas on DIST1, DIST2, and DIST3. Preferably all the replicas are synchronous. Configure secondary replicas to be readable or allow read. At this time, the distribution databases are the AG, DIST1 is the primary replica, and DIST2 and DIST3 are secondary replicas.
4. Configure a listener named `DISTLISTENER` for the AG.
5. For recovery and to allow transaction log truncation, configure full and transaction log backups.
6. On DIST2 and DIST3, run:

   ```sql
   sp_adddistributiondb 'distribution'
   ```

1. To add `PUB` as publisher on DIST1, run:
   
   ```sql
   sp_adddistpublisher @publisher= 'PUB', @distribution_db= 'distribution', @working_directory= '<network path>'
   ```

   The value of `@working_directory` should be a network path independent of DIST1, DIST2, and DIST3.

1. On DIST2 and DIST3, run:  

   ```sql
   sp_adddistpublisher @publisher= 'PUB', @distribution_db= 'distribution', @working_directory= '<network path>'
   ```

   The value of `@working_directory` should be the same as the previous step.

### Publisher workflow

To add the `distribution` database AG listener as the distributor, on PUB, run: 

   ```sql
   sp_adddistributor @distributor = 'DISTLISTENER', @password = <distributor_admin password> 
   ```

   The value of @password should be the one that was specified when distributors were configured in the distributor workflow.

## Remove distributor and publisher

This example removes publisher and distributor when distribution database is in AG.

### Publisher workflow

On PUB, drop all the subscriptions and publications for this publisher then call `sp_dropdistributor`.

### Distributors workflow

In this example, DIST1 is the current primary of `distribution` database AG. DIST2 and DIST3 are secondary replicas.

1. On DIST2 and DIST3, run:

   ```sql
   sp_dropdistpublisher 'PUB',  @no_checks = 1
   ```

1. On DIST1, run:

   ```sql
   sp_dropdistpublisher 'PUB'
   ```

1. Delete the AG.
2. On DIST2 and DIST3, change the `distribution` database to read_write mode by restoring the database with recovery.
   
   ```sql
   RESTORE DATABASE distribution WITH RECOVERY, KEEP_REPLICATION
   ```

1. To drop `distribution` database and to retain the snapshot directory, run: 

   ```sql
   sp_dropdistributiondb 'distribution' , @former_ag_secondary=1
   ```

  This procedure removes all the dangling jobs on this replica.

1. To drop `distribution` database on DIST1, run

   ```sql
   sp_dropdistributiondb 'distribution'
   ``` 

1. If there are no other distribution databases in AG, run `sp_dropdistributor` on DIST1, DIST2, and DIST3.

## Add a replica to distribution database AG

This example adds a new distributor to an existing replication configuration with distribution database in AG. In this example, an existing distribution database is in an AG. DIST1 and DIST2 are the distributors, `distribution` is the distribution database in AG, and PUB is the publisher. Add DIST3 as a replica in the AG.

### Distributors workflow

1. DIST3 should be configured as distributor through `sp_adddistributor @@servername`. The password for `distributor_admin` should be specified through @password parameter. The password should be the same as what was specified for DIST1 and DIST2.
2. Add DIST3 to the AG for current distribution database.
3. On DIST3, run:

   ```sql
   sp_adddistributiondb 'distribution'
   ```

4. On DIST3, run: 

   ```sql
   sp_adddistpublisher @publisher= 'PUB', @distribution_db= 'distribution', @working_directory= '<network path>'
   ```

   The value of `@working_directory` should be the same as what was specified for DIST1 and DIST2.

4. On DIST3, you must recreate Linked Servers to the subscribers.

## Remove a replica from distribution database AG

This example removes a distributor from a current distribution database AG while the rest of the replicas in distribution database AG are not affected. In this example, a distribution database is in AG. DIST1, DIST2, and DIST3 are the distributors, `distribution` is the distribution database in AG, and PUB is the publisher. Remove DIST3 from the AG.

### Distributors workflow

1. Make sure DIST3 is a secondary for the `distribution` database AG.
2. Remove DIST3 from the `distribution` database AG.
3. On DIST3, change the `distribution` database to read_write mode by restoring the database with recovery. For example, run the following command:  
   
   ```sql
   RESTORE DATABASE distribution WITH RECOVERY, KEEP_REPLICATION.
   ```
   
1. To remove all the orphaned jobs on DIST3 run: 

   ```sql
   sp_dropdistpublisher 'PUB', @no_checks = 1
   ```

1. On DIST3, run:

   ```sql
   sp_dropdistributiondb 'distribution', @former_ag_secondary=1
   ```

1. On DIST3, run: 

   ```sql
   sp_dropdistributor
   ```

## Remove a publisher from distribution database AG

This example removes a publisher from a distributor’s current distribution database AG while the rest of the publishers served by this distribution database AG are not affected. In this example, an existing configuration has distribution database in an AG. DIST1, DIST2, and DIST3 are the distributors, `distribution` is the distribution database in AG, and PUB1 and PUB2 are the publishers served by `distribution` database. The example removes PUB1 from these distributors.

### Publisher workflow

On PUB1, drop all the subscriptions and publications for this publisher, and then call `sp_dropdistributor`.

### Distributor workflow

DIST1 is the current primary of `distribution` database AG.

1. On DIST2 and DIST3, run:

   ```sql
   sp_dropdistpublisher 'PUB1',  @no_checks = 1
   ```

1. On DIST1, run:

   ```sql
   sp_dropdistpublisher 'PUB1'
   ```

1. At this point, there may be orphaned jobs related to PUB1 on DIST2 or DIST3. Whenever a failover occurs to DIST2 and DIST3, orphaned jobs related to all the publications of PUB1 will be removed by the `Monitor and sync replication agent jobs` job.

## Add subscription

This example is about properly configuring subscriber information among distributors. The example adds a subscriber. DIST1 is the current primary replica of distribution database in the AG, DIST2 and DIST3 are secondary replicas of distribution database in AG. The subscriber name is SUB.

### Publisher workflow

On PUB, add subscription as you would normally do to subscriber `SUB`.

### Distributor workflow

On DIST2 and DIST3, add a linked server for 'SUB' if it is not previously registered with DIST2 or DIST3. Below is a sample TSQL for linked server creation -

   ```sql 
   EXEC master.dbo.sp_addlinkedserver@server =N'SUB', @srvproduct=N'SQL Server'
   /* For security reasons the linked server remote logins password is changed with ######## */
   EXEC master.dbo.sp_addlinkedsrvlogin@rmtsrvname=N'SUB', @useself=N'True',@locallogin=NULL,@rmtuser=NULL,@rmtpassword=NULL
   ```

## Add a pull subscription

### Subscriber workflow

To add a pull subscription for a publication with the distribution database in an AG, use the AG listener name in the `@distributor` parameter of `sp_addpullsubscription_agent`.

## Sample T-SQL Create distribution DB in AG

The following script enables a distribution database in an availability group. 

```sql
--- WorkFlow to Enable Distribution Database In AG.

-- SECTION 1 ---- CONFIGURE THE DISTRIBUTOR SERVERS

-- Step1 - Configure the Distribution DB nodes (AG Replicas) to act as a distributor
:Connect SQLNode1
sp_adddistributor @distributor = @@ServerName, @password = 'Pass@word1'
Go 
:Connect SQLNode2
sp_adddistributor @distributor = @@ServerName, @password = 'Pass@word1'
Go

-- Step2 - Configure the Distribution Database
:Connect SQLNode1
USE master
EXEC sp_adddistributiondb @database = 'DistributionDB', @security_mode = 1;
GO
Alter Database [DistributionDB] Set Recovery Full
Go
Backup Database [DistributionDB] to Disk = 'Nul'
Go
-- Step 3 - Create AG for the Distribution DB.
:Connect SQLNode1
USE [master]
GO
CREATE ENDPOINT [Hadr_endpoint] 
	STATE=STARTED
	AS TCP (LISTENER_PORT = 5022, LISTENER_IP = ALL)   
	FOR DATA_MIRRORING (ROLE = ALL, AUTHENTICATION = WINDOWS NEGOTIATE
, ENCRYPTION = REQUIRED ALGORITHM AES)
GO

:Connect SQLNode2
USE [master]
GO
CREATE ENDPOINT [Hadr_endpoint] 
	STATE=STARTED
	AS TCP (LISTENER_PORT = 5022, LISTENER_IP = ALL)   
	FOR DATA_MIRRORING (ROLE = ALL, AUTHENTICATION = WINDOWS NEGOTIATE
, ENCRYPTION = REQUIRED ALGORITHM AES)
GO

:Connect SQLNode1
-- Create the Availability Group
CREATE AVAILABILITY GROUP [DistributionDB_AG]
FOR DATABASE [DistributionDB]
REPLICA ON 'SQLNode1'
WITH (ENDPOINT_URL = N'TCP://SQLNode1.contoso.com:5022', 
		 FAILOVER_MODE = AUTOMATIC, 
		 AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
		 BACKUP_PRIORITY = 50, 
		 SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL), 
		 SEEDING_MODE = AUTOMATIC),
N'SQLNode2' WITH (ENDPOINT_URL = N'TCP://SQLNode2.contoso.com:5022', 
	 FAILOVER_MODE = AUTOMATIC, 
	 AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
	 BACKUP_PRIORITY = 50, 
	 SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL), 
	 SEEDING_MODE = AUTOMATIC);
 GO


:Connect SQLNode2
ALTER AVAILABILITY GROUP [DistributionDB_AG] JOIN
GO  
ALTER AVAILABILITY GROUP [DistributionDB_AG] GRANT CREATE ANY DATABASE
Go

--STEP4 - Create the Listener for the Availability Group. This is very important.
:Connect SQLNode1

USE [master]
GO
ALTER AVAILABILITY GROUP [DistributionDB_AG]
ADD LISTENER N'DistributionDBList' (
WITH IP
((N'10.0.0.8', N'255.255.255.0')) , PORT=1500);
GO

-- STEP 5 - Enable SQLNode1 also as a Distributor
:CONNECT SQLNODE2
EXEC sp_adddistributiondb @database = 'DistributionDB', @security_mode = 1;
GO

--STEP 6 - On all Distributor Nodes Configure the Publisher Details 
:CONNECT SQLNODE1
EXEC sp_addDistPublisher @publisher = 'SQLNode4', @distribution_db = 'DistributionDB', 
	@working_directory = '\\sqlfileshare\Dist_Work_Directory\'
GO
:CONNECT SQLNODE2
EXEC sp_addDistPublisher @publisher = 'SQLNode4', @distribution_db = 'DistributionDB', 
	@working_directory = '\\sqlfileshare\Dist_Work_Directory\'
GO

-- SECTION 2 ---- CONFIGURE THE PUBLISHER SERVER
:CONNECT SQLNODE4
EXEC sp_addDistributor @distributor = 'DistributionDBList', -- Listener for the Distribution DB.	
	@password = 'Pass@word1'
Go

-- SECTION 3 ---- CONFIGURE THE SUBSCRIBERS 
-- On Publisher, create the publication as one would normally do.
-- On the Secondary replicas of the Distribution DB, add the Subscriber as a linked server.
:CONNECT SQLNODE2
EXEC master.dbo.sp_addlinkedserver @server = N'SQLNODE5', @srvproduct=N'SQL Server'
 /* For security reasons the linked server remote logins password is changed with ######## */
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SQLNODE5',@useself=N'True',@locallogin=NULL,@rmtuser=NULL,@rmtpassword=NULL 
```

## See Also  
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md)  
  
## Next steps
 [View and Modify Distributor and Publisher Properties](view-and-modify-distributor-and-publisher-properties.md)  
 [Disable Publishing and Distribution](disable-publishing-and-distribution.md)  
 [Enable a Database for Replication (SQL Server Management Studio)](enable-a-database-for-replication-sql-server-management-studio.md) 
