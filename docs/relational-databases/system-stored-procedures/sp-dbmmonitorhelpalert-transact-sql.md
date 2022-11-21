---
description: "sp_dbmmonitorhelpalert (Transact-SQL)"
title: "sp_dbmmonitorhelpalert (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_dbmmonitorhelpalert_TSQL"
  - "sp_dbmmonitorhelpalert"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dbmmonitorhelpalert"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: 43911660-b4e4-4934-8c02-35221160aaec
author: markingmyname
ms.author: maghan
---
# sp_dbmmonitorhelpalert (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about warning thresholds on one or all of several key database mirroring monitor performance metrics.  
 
  ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitorhelpalert database_name   
    [ , alert_id ]   
```  
  
## Arguments  
 *database_name*  
 Specifies the database.  
  
 [ *alert_id* ]  
 An integer value that identifies the warning to be returned. If this argument is omitted, all the warnings are returned, but not the retention period.  
  
 To return a specific warning, specify one of the following values:  
  
|Value|Performance metric|Warning threshold|  
|-----------|------------------------|-----------------------|  
|1|Oldest unsent transaction|Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and it is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|2|Unsent log|Specifies how many kilobytes (KB) of unsent log generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and it is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected.|  
|3|Unrestored log|Specifies how many KB of unrestored log generate a warning on the mirror server instance. This warning helps measure failover time. *Failover time* consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short additional time.|  
|4|Mirror commit overhead|Specifies the number of milliseconds of average delay per transaction that are tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode.|  
|5|Retention period|Metadata that controls how long rows in the database mirroring status table are preserved.|  
  
 For information about the event IDs corresponding to the warnings, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).  
  
## Return Code Values  
 None  
  
## Result Sets  
 For each returned alert, returns a row containing the following columns:  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**alert_id**|**int**|The  table below lists the **alert_id** value for each performance metric and the unit of measurement of the metric displayed in the **sp_dbmmonitorresults** result set:|  
|**threshold**|**int**|The threshold value for the warning. If a value above this threshold is returned when the mirroring status is updated, an entry is entered into the Windows event log. This value represents KB, minutes, or milliseconds, depending on the warning. If the threshold is currently not set, the value is NULL.<br /><br /> **Note:** To view the current values, run the [sp_dbmmonitorresults](../../relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql.md) stored procedure.|  
|**enabled**|**bit**|0 = Event is disabled.<br /><br /> 1 = Event is enabled.<br /><br /> **Note:** Retention period is always enabled.|  
  
|Value|Performance metric|Unit|  
|-----------|------------------------|----------|  
|1|Oldest unsent transaction|Minutes|  
|2|Unsent log|KB|  
|3|Unrestored log|KB|  
|4|Mirror commit overhead|Milliseconds|  
|5|Retention period|Hours|  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example returns a row that indicates whether a warning is enabled for the oldest unsent transaction performance metric on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
EXEC sp_dbmmonitorhelpalert AdventureWorks2012, 1 ;  
```  
  
 The following example returns a row for each performance metric that indicates whether it is enabled on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
EXEC sp_dbmmonitorhelpalert AdventureWorks2012;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorchangealert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangealert-transact-sql.md)   
 [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql.md)   
 [sp_dbmmonitordropalert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitordropalert-transact-sql.md)   
 [sp_dbmmonitorupdate &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorupdate-transact-sql.md)   
 [sp_dbmmonitorhelpmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql.md)   
 [sp_dbmmonitorresults &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql.md)  
  
  
