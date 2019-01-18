---
title: "sp_dbmmonitorresults (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dbmmonitorresults"
  - "sp_dbmmonitorresults_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dbmmonitorresults"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: d575e624-7d30-4eae-b94f-5a7b9fa5427e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_dbmmonitorresults (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns status rows for a monitored database from the status table in which database mirroring monitoring history is stored and allows you to choose whether the procedure obtains the latest status beforehand.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitorresults database_name   
   , rows_to_return  
    , update_status   
```  
  
## Arguments  
 *database_name*  
 Specifies the database for which to return mirroring status.  
  
 *rows_to_return*  
 Specifies the quantity of rows returned:  
  
 0 = Last row  
  
 1 = Rows last two hours  
  
 2 = Rows last four hours  
  
 3 = Rows last eight hours  
  
 4 = Rows last day  
  
 5 = Rows last two days  
  
 6 = Last 100 rows  
  
 7 = Last 500 rows  
  
 8 = Last 1,000 rows  
  
 9 = Last 1,000,000 rows  
  
 *update_status*  
 Specifies that before returning results the procedure:  
  
 0 = Does not update the status for the database. The results are computed using just the last two rows, the age of which depends on when the status table was refreshed.  
  
 1 = Updates the status for the database by calling **sp_dbmmonitorupdate** before computing the results. However, if the status table has been updated within the previous 15 seconds, or the user is not a member of the **sysadmin** fixed server role, **sp_dbmmonitorresults** runs without updating the status.  
  
## Return Code Values  
 None  
  
## Result Sets  
 Returns the requested number of rows of history status for the specified database. Each row contains the following information:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**sysname**|Name of a mirrored database.|  
|**role**|**int**|Current mirroring role of the server instance:<br /><br /> 1 = Principal<br /><br /> 2 = Mirror|  
|**mirroring_state**|**int**|State of the database:<br /><br /> 0 = Suspended<br /><br /> 1 = Disconnected<br /><br /> 2 = Synchronizing<br /><br /> 3 = Pending Failover<br /><br /> 4 = Synchronized|  
|**witness_status**|**int**|Connection status of the witness in the database mirroring session of the database, can be:<br /><br /> 0 = Unknown<br /><br /> 1 = Connected<br /><br /> 2 = Disconnected|  
|**log_generation_rate**|**int**|Amount of log generated since preceding update of the mirroring status of this database in kilobytes/sec.|  
|**unsent_log**|**int**|Size of the unsent log in the send queue on the principal in kilobytes.|  
|**send_rate**|**int**|Send rate of log from the principal to the mirror in kilobytes/sec.|  
|**unrestored_log**|**int**|Size of the redo queue on the mirror in kilobytes.|  
|**recovery_rate**|**int**|Redo rate on the mirror in kilobytes/sec.|  
|**transaction_delay**|**int**|Total delay for all transactions in milliseconds.|  
|**transactions_per_sec**|**int**|Number of transactions that are occurring per second on the principal server instance.|  
|**average_delay**|**int**|Average delay on the principal server instance for each transaction because of database mirroring. In high-performance mode (that is, when the SAFETY property is set to OFF), this value is generally 0.|  
|**time_recorded**|**datetime**|Time at which the row was recorded by the database mirroring monitor. This is the system clock time of the principal.|  
|**time_behind**|**datetime**|Approximate system-clock time of the principal to which the mirror database is currently caught up. This value is meaningful only on the principal server instance.|  
|**local_time**|**datetime**|System clock time on the local server instance when this row was updated.|  
  
## Remarks  
 **sp_dbmmonitorresults** can be executed only in the context of the **msdb** database.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role or in the **dbm_monitor** fixed database role in the **msdb** database. The **dbm_monitor** role enables its members to view database mirroring status, but not update it but not view or configure database mirroring events.  
  
> [!NOTE]  
>  The first time that **sp_dbmmonitorupdate** executes, it creates the **dbm_monitor** fixed database role in the **msdb** database. Members of the **sysadmin** fixed server role can add any user to the **dbm_monitor** fixed database role.  
  
## Examples  
 The following example returns the rows recorded during the preceding two hours without updating the status of the database.  
  
```  
USE msdb;  
EXEC sp_dbmmonitorresults AdventureWorks2012, 2, 0;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql.md)   
 [sp_dbmmonitoraddmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitoraddmonitoring-transact-sql.md)   
 [sp_dbmmonitordropmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitordropmonitoring-transact-sql.md)   
 [sp_dbmmonitorhelpmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql.md)   
 [sp_dbmmonitorupdate &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorupdate-transact-sql.md)  
  
  
