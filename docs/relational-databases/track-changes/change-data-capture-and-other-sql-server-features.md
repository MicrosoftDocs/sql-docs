---
description: "Learn how change data capture and functions with other features such as change tracking and database mirroring."
title: "Change data capture and Other Features"
ms.custom: seo-dt-2019
ms.date: "01/02/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change data capture, other features and"
ms.assetid: 7dfcb362-1904-4578-8274-da16681a960e
author: MikeRayMSFT
ms.author: mikeray
---
# Change data capture and other features
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This topic describes how the following features interact with change data capture:  
    
##  <a name="ChangeTracking"></a> Change tracking  
 Change data capture and [change tracking](../../relational-databases/track-changes/about-change-tracking-sql-server.md) can be enabled on the same database. No special considerations are required. For more information, see [Work with Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/work-with-change-tracking-sql-server.md).  
  
##  <a name="DatabaseMirroring"></a> Database mirroring  
 A database that is enabled for change data capture can be mirrored. To ensure that capture and cleanup happen automatically after a failover, follow these steps:  
  
1.  Ensure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running on the new principal server instance.  
  
2.  Create the capture job and cleanup job on the new principal database (the former mirror database). To create the jobs, use the [sp_cdc_add_job](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md) stored procedure.  
  
 To view the current configuration of a cleanup or capture job, use the [sys.sp_cdc_help_jobs](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-jobs-transact-sql.md) stored procedure on the new principal server instance. For a given database, the capture job is named cdc.*database\_name*\_capture, and the cleanup job is named cdc.*database\_name*\_cleanup, where *database_name* is the name of the database.  
  
 To change the configuration of a job, use the [sys.sp_cdc_change_job](../../relational-databases/system-stored-procedures/sys-sp-cdc-change-job-transact-sql.md) stored procedure.  
  
 For information about database mirroring, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
##  <a name="TransReplication"></a> Transactional Replication  
 Change data capture and transactional replication can coexist in the same database, but population of the change tables is handled differently when both features are enabled. Change data capture and transactional replication always use the same procedure, [sp_replcmds](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md), to read changes from the transaction log. When change data capture is enabled on its own, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job calls **sp_replcmds**. When both features are enabled on the same database, the Log Reader Agent calls **sp_replcmds**. This agent populates both the change tables and the distribution database tables. For more information, see [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md).  
  
 Consider a scenario in which change data capture is enabled on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, and two tables are enabled for capture. To populate the change tables, the capture job calls **sp_replcmds**. The database is enabled for transactional replication, and a publication is created. Now, the Log Reader Agent is created for the database and the capture job is deleted. The Log Reader Agent continues to scan the log from the last log sequence number that was committed to the change table. This ensures data consistency in the change tables. If transactional replication is disabled in this database, the Log Reader Agent is removed and the capture job is re-created.  
  
> [!NOTE]  
>  When the Log Reader Agent is used for both change data capture and transactional replication, replicated changes are first written to the distribution database. Then, captured changes are written to the change tables. Both operations are committed together. If there is any latency in writing to the distribution database, there will be a corresponding latency before changes appear in the change tables.  
  
 The **proc exec** option of transactional replication is not available when change data capture is enabled.  
  
##  <a name="RestoreOrAttach"></a> Database restore or attach
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the following logic to determine if change data capture remains enabled after a database is restored or attached:  
  
-   If a database is restored to the same server with the same database name, change data capture remains enabled.  
  
-   If a database is restored to another server, by default change data capture is disabled and all related metadata is deleted.  
  
     To retain change data capture, use the **KEEP_CDC** option when restoring the database. For more information about this option, see [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).  
  
-   If a database is detached and attached to the same server or another server, change data capture remains enabled.  
  
-   If a database is attached or restored with the **KEEP_CDC** option to any edition other than Standard, Enterprise, or SQL Managed Instance, the operation is blocked because change data capture requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard, Enterprise, or SQL Managed Instance editions. Error message 934 is displayed:  
  
     `SQL Server cannot load database '%.*ls' because change data capture is enabled. The currently installed edition of SQL Server does not support change data capture. Either restore database without KEEP_CDC option, or upgrade the instance to one that supports change data capture.`  
  
 You can use [sys.sp_cdc_disable_db](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) to remove change data capture from a restored or attached database.  
 
 After restoring a database on Azure SQL Managed Instance, CDC will remain enabled, but you must ensure that the scan and cleanup jobs are added and running. You can manually add the jobs by running [sys.sp_cdc_add_job](../system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md). 
  
##  <a name="Contained"></a> Contained databases  
 Change data capture is not supported in [contained databases](../../relational-databases/databases/contained-databases.md).
 
##  <a name="Serverless"></a> Serverless databases  
  If a serverless database is enabled for Change Data Capture (CDC) is in pause state, CDC will not run. The CDC scan shall not affect auto-pause. 
  
## <a name="AlwaysOn"></a> Availability groups 
 
 When you use Always On availability groups, change enumeration should be done on the secondary replica to reduce disk load on the primary.  

## <a name="FailoverAzureSQLDB"></a> Failover (local & GeoDR) in Azure SQL Database 
 
In case of local as well as GeoDR failover scenarios, if the database is enabled for change data capture (CDC), capture and cleanup happen automatically on the new primary, following the failover. 

##  <a name="Point-in-time-restore"></a> Point-in-time-restore (PITR) in Azure SQL Database

If you enabled change data capture (CDC) on your Azure SQL Database as SQL user, point-in-time-restore (PITR) retains the CDC as well in the restored DB, unless it is restored to sub-core SLO. If restored to sub-core SLO, CDC artifacts will not be available.

If you enabled CDC on your Azure SQL Database as an Azure AD user, PITR retains the CDC if restored to same or higher SLO than the source database. PITR to sub-core SLO will fail as mentioned under [known limitations and issues](./about-change-data-capture-sql-server.md?#known-limitations-and-issues).

##  <a name="AzureActiveDirectory"></a> Azure Active Directory in Azure SQL Database 

If you create a database in Azure SQL Database as an Azure AD user and enable change data capture on it, a SQL user (e.g. even sys admin role) will not be able to disable/make changes to change data capture artifacts. However, another Azure AD user will be able to enable/disable change data capture on the same database. 

Similarly, if you create an Azure SQL Database as a SQL user, enabling/disabling change data capture as an Azure AD user won't work. 


## See Also  
 [Administer and Monitor change data capture &#40;SQL Server&#41;](../../relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
