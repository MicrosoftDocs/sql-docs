---
title: "Transact-SQL statements for availability groups"
description: Introduces Transact-SQL (T-SQL) statements that support deploying, creating, and managing Always On availability groups.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Availability Groups [SQL Server], about"
  - "Availability Groups [SQL Server], Transact-SQL statements"
---
# Transact-SQL statements for Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic introduces the [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements that support deploying [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] and creating and managing an given availability group, availability replica and availability database.  
  
 
##  <a name="CreateEndpoint"></a> CREATE ENDPOINT  
 [CREATE ENDPOINT ... FOR DATABASE_MIRRORING](../../../t-sql/statements/create-endpoint-transact-sql.md) creates a database mirroring endpoint, if none exists on the server instance. Every server instance on which you intend to deploy [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] or database mirroring requires a database mirroring endpoint.  
  
 Execute this statement on the server instance on which you are creating the endpoint. You can create only one database mirroring endpoint on a given server instance. For more information, see [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md).  
  
##  <a name="CreateAG"></a> CREATE AVAILABILITY GROUP  
 [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md) creates a new availability group and optionally an availability group listener. Minimally, you must specify your local server instance, which will become the initial primary replica. Optionally, you can also specify up to four secondary replicas.  
  
 Execute CREATE AVAILABILITY GROUP on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that you want to host the initial primary replica of your new availability group. This server instance must reside on a node of a Windows Server Failover Cluster (WSFC) (for more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
##  <a name="AlterAG"></a> ALTER AVAILABILITY GROUP  
 [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) supports changing an existing availability group or availability group listener and for failing over an availability group.  
  
 Execute ALTER AVAILABILITY GROUP on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts the current primary replica.  
  
##  <a name="AlterDb"></a> ALTER DATABASE ... SET HADR ...  
 The options of the [SET HADR](../../../t-sql/statements/alter-database-transact-sql-set-hadr.md) clause of the ALTER DATABASE statement enables you to join a secondary database to the availability group of the corresponding primary database, remove a joined database, and suspend data synchronization on a joined database, and resume data synchronization.  
  
##  <a name="DropAG"></a> DROP AVAILABILITY GROUP  
 [DROP AVAILABILITY GROUP](../../../t-sql/statements/drop-availability-group-transact-sql.md) removes a specified availability group and all of its replicas. DROP AVAILABILITY GROUP can be run from any [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] node in the WSFC failover cluster.  
  
##  <a name="Restrictions"></a> Restrictions on the AVAILABILITY GROUP Transact-SQL Statements  
 The CREATE AVAILABILITY GROUP, ALTER AVAILABILITY GROUP, and DROP AVAILABILITY GROUP [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements have the following limitations:  
  
-   With the exception of DROP AVAILABILITY GROUP, executing these statements requires that the HADR service is enabled on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).  
  
-   These statements cannot be executed within transactions or batches.  
  
-   Though they make a best effort to clean up after a failure, these statements do not guarantee that they will roll back all changes on failure. However, systems should be able cleanly handle and then ignore partial failures.  
  
-   These statements do not support expressions or variables.  
  
-   If a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement is executed while another availability group action or recovery is in process, the statement returns an error. Wait for the action or recovery to complete, and retry the statement, if necessary.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
