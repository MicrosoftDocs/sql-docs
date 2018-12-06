---
title: "sp_dbmmonitordropalert (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dbmmonitordropalert_TSQL"
  - "sp_dbmmonitordropalert"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database mirroring [SQL Server], monitoring"
  - "sp_dbmmonitordropalert"
ms.assetid: fe4a134b-25bf-464e-a5c4-358de215b65a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_dbmmonitordropalert (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops the warning for a specified performance metric, by setting the threshold to NULL.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitordropalert database_name   
    [ , alert_id ]   
```  
  
## Arguments  
 *database_name*  
 Specifies the database for which to drop the specified warning threshold.  
  
 *alert_id*  
 An integer value that identifies the warning to be dropped. If this argument is omitted, all warnings on the database are dropped. To drop the warning for a specific performance metric, specify one of the following values:  
  
|Value|Performance metric|Warning threshold|  
|-----------|------------------------|-----------------------|  
|1|Oldest unsent transaction|Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and it is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|2|Unsent log|Specifies how many kilobytes (KB) of unsent log generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and it is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|3|Unrestored log|Specifies how many KB of unrestored log generate a warning on the mirror server instance. This warning helps measure failover time. *Failover time* consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short additional time.|  
|4|Mirror commit overhead|Specifies the number of milliseconds of average delay per transaction that are tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode.|  
|5|Retention period|Metadata that controls how long rows in the database mirroring status table are preserved.|  
  
> [!NOTE]  
>  This procedure drops warning thresholds, regardless of whether they were specified using **sp_dbmmonitorchangealert** or Database Mirroring Monitor.  
  
 For information about the event IDs corresponding to the warnings, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).  
  
## Return Code Values  
 None  
  
## Result Sets  
 None  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example drops the retention period setting of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
EXEC sp_dbmmonitordropalert AdventureWorks2012, 5;  
```  
  
 The following example drops all of the warning thresholds and the retention period of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
EXEC sp_dbmmonitordropalert AdventureWorks2012 ;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorchangealert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangealert-transact-sql.md)  
  
  
