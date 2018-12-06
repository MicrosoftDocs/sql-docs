---
title: "sp_dbmmonitorupdate (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dbmmonitorupdate"
  - "sp_dbmmonitorupdate_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dbmmonitorupdate"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: 9ceb9611-4929-44ee-a406-c39ba2720fd5
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_dbmmonitorupdate (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Updates the database mirroring monitor status table by inserting a new table row for each mirrored database, and truncates rows older than the current retention period. The default retention period is 7 days (168 hours). When updating the table, **sp_dbmmonitorupdate** evaluates the performance metrics.  
  
> [!NOTE]  
>  The first time **sp_dbmmonitorupdate** runs, it creates the database mirroring status table and the **dbm_monitor** fixed database role in the **msdb** database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitorupdate [ database_name ]  
```  
  
## Arguments  
 *database_name*  
 The name of the database for which to update mirroring status. If *database_name* is not specified, the procedure updates the status table for every mirrored database on the server instance.  
  
## Return Code Values  
 None  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_dbmmonitorupdate** can be executed only in the context of the **msdb** database.  
  
 If a column of the status table does not apply to the role of a partner, the value is NULL on that partner. A column would also have a NULL value if the relevant information is unavailable, such as  during a failover or server restart.  
  
 After **sp_dbmmonitorupdate** creates the **dbm_monitor** fixed database role in the **msdb** database, members of the **sysadmin** fixed server role can add any user to the **dbm_monitor** fixed database role. The **dbm_monitor** role enables its members to view database mirroring status, but not update it but not view or configure database mirroring events.  
  
 When updating the mirroring status of a database, **sp_dbmmonitorupdate** inspects the latest value of any mirroring performance metric for which a warning threshold has been specified. If the value exceeds the threshold, the procedure adds an informational event to the event log. All rates are averages since the last update. For more information, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example updates the mirroring status for just the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE msdb;  
EXEC sp_dbmmonitorupdate AdventureWorks2012 ;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorchangealert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangealert-transact-sql.md)   
 [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql.md)   
 [sp_dbmmonitordropalert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitordropalert-transact-sql.md)   
 [sp_dbmmonitorhelpalert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorhelpalert-transact-sql.md)   
 [sp_dbmmonitorhelpmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql.md)   
 [sp_dbmmonitorresults &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql.md)  
  
  
