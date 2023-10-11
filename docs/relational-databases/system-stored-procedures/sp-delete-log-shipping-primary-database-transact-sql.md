---
title: "sp_delete_log_shipping_primary_database (Transact-SQL)"
description: "sp_delete_log_shipping_primary_database (Transact-SQL)"
author: MashaMSFT
ms.author: mathoma
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_primary_database"
  - "sp_delete_log_shipping_primary_database_TSQL"
helpviewer_keywords:
  - "sp_delete_log_shipping_primary_database"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_primary_database (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This stored procedure removes log shipping of primary database including backup job as well as local and remote history. Only use this stored procedure after you have removed the secondary databases using **sp_delete_log_shipping_primary_secondary**.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_log_shipping_primary_database  
[ @database = ] 'database'  
```  
  
## Arguments  
`[ @database = ] 'database'`
 Is the name of the log shipping primary database. *database* is **sysname**, with no default, and cannot be NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None.  
  
## Remarks  
 **sp_delete_log_shipping_primary_database** must be run from the **master** database on the primary server. This stored procedure does the following:  
  
1.  Deletes the backup job for the specified primary database.  
  
2.  Removes the local monitor record in **log_shipping_monitor_primary** on the primary server.  
  
3.  Removes corresponding entries in **log_shipping_monitor_history_detail** and **log_shipping_monitor_error_detail**.  
  
4.  If the monitor server is different from the primary server, removes the monitor record in **log_shipping_monitor_primary** on the monitor server.  
  
5.  Removes corresponding entries in **log_shipping_monitor_history_detail** and **log_shipping_monitor_error_detail** on the monitor server.  
  
6.  Removes the entry in **log_shipping_primary_databases** for this primary database.  
  
7.  Calls **sp_delete_log_shipping_alert_job** on the monitor server.  

## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example illustrates using **sp_delete_log_shipping_primary_database** to delete the primary database [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)].  
  
```  
EXEC master.dbo.sp_delete_log_shipping_primary_database @database = N'AdventureWorks2022';  
GO  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
