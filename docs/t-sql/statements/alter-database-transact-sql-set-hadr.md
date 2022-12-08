---
title: "ALTER DATABASE SET HADR (Transact-SQL)"
description: ALTER DATABASE SET HADR (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET HADR"
  - "SET_HADR_TSQL"
  - "HADR_TSQL"
  - "HADR"
helpviewer_keywords:
  - "ALTER DATABASE statement, AlwaysOn Availability Group"
  - "ALTER DATABASE statement, Always On Availability Group"
  - "ALTER DATABASE statement, SET HADR options"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], Transact-SQL statements"
  - "Availability Groups [SQL Server], databases"
dev_langs:
  - "TSQL"
---
# ALTER DATABASE (Transact-SQL) SET HADR 
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic contains the ALTER DATABASE syntax for setting [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] options on a secondary database. Only one SET HADR option is permitted per ALTER DATABASE statement. These options are supported only on secondary replicas.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER DATABASE database_name  
   SET HADR   
   {  
        { AVAILABILITY GROUP = group_name | OFF }  
   | { SUSPEND | RESUME }  
   }  
[;]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name*  
 Is the name of the secondary database to be modified.  
  
 SET HADR  
 Executes the specified [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] command on the specified database.  
  
 { AVAILABILITY GROUP **=**_group_name_ | OFF }  
 Joins or removes the availability database from the specified availability group, as follows:  
  
 *group_name*  
 Joins the specified database on the secondary replica that is hosted by the server instance on which you execute the command  to the availability group specified by group_name.  
  
 The prerequisites for this operation are as follows:  
  
-   The database must already have been added to the availability group on the primary replica.  
  
-   The primary replica must be active. For information about how troubleshoot an inactive primary replica, see [Troubleshooting Always On Availability Groups Configuration (SQL Server)](/previous-versions/sql/sql-server-2012/ff878308(v=sql.110)).  
  
-   The primary replica must be online, and the secondary replica must be connected to the primary replica.  
  
-   The secondary database must have been restored  using WITH NORECOVERY from recent database and log backups of the primary database, ending with a log backup that is recent enough to permit the secondary database to catch up to the primary database.  
  
    > [!NOTE]  
    >  To add a database to the availability group, connect to the server instance that hosts the primary replica, and use the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)*group_name* ADD DATABASE *database_name* statement.  
  
 For more information, see [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
 OFF  
 Removes the specified secondary database from the availability group.  
  
 Removing a secondary database can be useful if it has fallen far behind the primary database, and you do not want to wait for the secondary database to catch up. After removing the secondary database, you can update it by restoring a sequence of backups ending with a recent log backup (using RESTORE ... WITH NORECOVERY).  
  
> [!IMPORTANT]  
>  To completely remove an availability database from an availability group, connect to the server instance that hosts the primary replica, and use the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)*group_name* REMOVE DATABASE *availability_database_name* statement. For more information, see [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md).  
  
 SUSPEND  
 Suspends data movement on a secondary database. A SUSPEND command returns as soon as it has been accepted by the replica that hosts the target database, but actually suspending the database occurs asynchronously.  
  
 The scope of the impact depends on where you execute the ALTER DATABASE statement:  
  
-   If you suspend a secondary database on a secondary replica, only the local secondary database is suspended. Existing connections on the readable secondary remain usable. New connections to the suspended database on the readable secondary are not allowed until data movement is resumed.  
  
-   If you suspend a database on the primary replica, data movement is suspended to the corresponding secondary databases on every secondary replica. Existing connections on a readable secondary remain usable, and new read-intent connections will not connect to readable secondary replicas.  
  
-   When data movement is suspended due to a forced manual failover, connections to the new secondary replica are not allowed while data movement is suspended.  
  
 When a database on a secondary replica is suspended, both the database and replica become unsynchronized and are marked as NOT SYNCHRONIZED.  
  
> [!IMPORTANT]  
>  While a secondary database is suspended, the send queue of the corresponding primary database will accumulate unsent transaction log records. Connections to the secondary replica return data that was available at the time the data movement was suspended.  
  
> [!NOTE]  
>  Suspending and resuming an Always On secondary database does not directly affect the availability of the primary database, though suspending a secondary database can impact redundancy and failover capabilities for the primary database, until the suspended secondary database is resumed. This is in contrast to database mirroring, where the mirroring state is suspended on both the mirror database and the principal database until mirroring is resumed. Suspending an Always On primary database suspends data movement on all the corresponding secondary databases, and redundancy and failover capabilities cease for that database until the primary database is resumed.  
  
 For more information, see [Suspend an Availability Database &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/suspend-an-availability-database-sql-server.md).  
  
 RESUME  
 Resumes suspended data movement on the specified secondary database. A RESUME command returns as soon as it has been accepted by the replica that hosts the target database, but actually resuming the database occurs asynchronously.  
  
 The scope of the impact depends on where you execute the ALTER DATABASE statement:  
  
-   If you resume a secondary database on a secondary replica, only the local secondary database is resumed. Data movement is resumed unless the database has also been suspended on the primary replica.  
  
-   If you resume a database on the primary replica, data movement is resumed to every secondary replica on which the corresponding secondary database has not also been suspended locally. To resume a secondary database that was individually suspended on a secondary replica, connect to the server instance that hosts the secondary replica and resume the database there.  
  
     Under synchronous-commit mode, the database state changes to SYNCHRONIZING. If no other database is currently suspended, the replica state also changes to SYNCHRONIZING.  
  
     For more information, see [Resume an Availability Database &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/resume-an-availability-database-sql-server.md).  
  
## Database States  
 When a secondary database is joined to an availability group, the local secondary replica changes the state of that secondary database from RESTORING to ONLINE. If a secondary database is removed from the availability group, it is set back to the RESTORING state by the local secondary replica. This allows you to apply subsequent log backups from the primary database to that secondary database.  
  
## Restrictions  
 Execute ALTER DATABASE statements outside of both transactions and batches.  
  
## Security  
  
### Permissions  
 Requires ALTER permission on the database. Joining a database to an availability group requires membership in the **db_owner** fixed database role.  
  
## Examples  
 The following example joins the secondary database, `AccountsDb1`, to the local secondary replica of the `AccountsAG` availability group.  
  
```sql  
ALTER DATABASE AccountsDb1 SET HADR AVAILABILITY GROUP = AccountsAG;  
```  
  
> [!NOTE]  
>  To see this [!INCLUDE[tsql](../../includes/tsql-md.md)] statement used in context, see [Create an Availability Group &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md).  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) 
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md) 
  
