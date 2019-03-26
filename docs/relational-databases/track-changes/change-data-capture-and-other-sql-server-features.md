---
title: "Change Data Capture and Other SQL Server Features | Microsoft Docs"
ms.date: "01/02/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change data capture [SQL Server], other SQL Server features and"
ms.assetid: 7dfcb362-1904-4578-8274-da16681a960e
author: rothja
ms.author: jroth
manager: craigg
---
# Change Data Capture and Other SQL Server Features
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]
  This topic describes how the following features interact with change data capture:  
  
-   [Change tracking](#ChangeTracking)  
  
-   [Database mirroring](#DatabaseMirroring)  
  
-   [Transactional replication](#TransReplication)  
  
-   [Restoring or Attaching a Database Enabled for Change Data Capture](#RestoreOrAttach)

-   [Contained Databases](#Contained)
  
##  <a name="ChangeTracking"></a> Change Tracking  
 Change data capture and [change tracking](../../relational-databases/track-changes/about-change-tracking-sql-server.md) can be enabled on the same database. No special considerations are required. For more information, see [Work with Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/work-with-change-tracking-sql-server.md).  
  
##  <a name="DatabaseMirroring"></a> Database Mirroring  
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
  
##  <a name="RestoreOrAttach"></a> Restoring or Attaching a Database Enabled for Change Data Capture  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the following logic to determine if change data capture remains enabled after a database is restored or attached:  
  
-   If a database is restored to the same server with the same database name, change data capture remains enabled.  
  
-   If a database is restored to another server, by default change data capture is disabled and all related metadata is deleted.  
  
     To retain change data capture, use the **KEEP_CDC** option when restoring the database. For more information about this option, see [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).  
  
-   If a database is detached and attached to the same server or another server, change data capture remains enabled.  
  
-   If a database is attached or restored with the **KEEP_CDC** option to any edition other than Enterprise, the operation is blocked because change data capture requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise. Error message 934 is displayed:  
  
     `SQL Server cannot load database '%.*ls' because Change Data Capture is enabled. The currently installed edition of SQL Server does not support Change Data Capture. Either restore database without KEEP_CDC option, or upgrade the instance to one that supports Change Data Capture.`  
  
 You can use [sys.sp_cdc_disable_db](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) to remove change data capture from a restored or attached database.  
  
##  <a name="Contained"></a> Contained Databases  
 Change data capture is not supported in [contained databases](../../relational-databases/databases/contained-databases.md).
  
## Change Data Capture and Always On  
 When you use Always On, change enumeration should be done on the Secondary replication to reduce the disk load on the primary.  
  
## See Also  
 [Administer and Monitor Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
  
