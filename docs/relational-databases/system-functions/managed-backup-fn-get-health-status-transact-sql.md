---
description: "managed_backup.fn_get_health_status (Transact-SQL)"
title: "managed_backup.fn_get_health_status (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_get_health_status_TSQL"
  - "smart_admin.fn_get_health_status_TSQL"
  - "smart_admin.fn_get_health_status"
  - "fn_get_health_status"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "smart_admin.fn_get_health_status"
  - "fn_get_health_status"
ms.assetid: b376711d-444a-4b5e-b483-8df323b4e31f
author: MikeRayMSFT
ms.author: mikeray
---
# managed_backup.fn_get_health_status (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns a table of 0, one or more rows of aggregated count of the errors reported by Extended Events for a specified period of time.  
  
 The function is used to report health status of services under Smart Admin.  Currently [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is supported under the Smart Admin umbrella. So the errors returned are related to [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.fn_get_health_status([@begin_time = ] 'time_1' , [ @end_time = ] 'time_2')  
```  
  
##  <a name="Arguments"></a> Arguments  
 [@begin_time]  
 The start of the time period from which the aggregated count of errors is calculated.  The @begin_time parameter is DATETIME. The default value is NULL. When the value is NULL the function will process events reported as early as 30 minutes before current time.  
  
 [ @end_time]  
 The end of the time period from which the aggregated count of errors is calculated. The @end_time  parameter is DATETIME with a default value of NULL. When the value is NULL the function will process extended events as up to the current time.  
  
## Table Returned  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|number_of_storage_connectivity_errors|int|Number of connection errors when the program connects to the Azure storage account.|  
|number_of_sql_errors|int|Number of errors returned when the program connects to SQL Server Engine.|  
|number_of_invalid_credential_errors|int|Number of errors returned when the program tries to authenticate using SQL Credentials.|  
|number_of_other_errors|int|Number of errors in other categories besides connectivity, SQL, or credential.|  
|number_of_corrupted_or_deleted_backups|int|Number of deleted or corrupted backup files.|  
|number_of_backup_loops|int|The number of times backup agent scans all the databases configured with [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].|  
|number_of_retention_loops|int|The number of times the databases are scanned to assess set retention period.|  
  
## Best Practices  
 These aggregated counts can be used to monitor system health. For example, if the number_ of_retention_loops column is 0 in 30 minutes, it is possible that the retention management is taking long time or even not working correctly. Non-zero error columns may indicate problems and Extended events logs should be checked to learn of the any problems. Alternately, use the stored procedure **managed_backup.sp_get_backup_diagnostics** to get a list of Extended events to find the details of the error.  
  
## Security  
  
### Permissions  
 Requires **SELECT** permissions on the function.  
  
## Examples  
  
-   The following example returns aggregated error counts for the last 30 minutes from the time it was executed.  
  
    ```  
    SELECT *  
    FROM managed_backup.fn_get_health_status(NULL, NULL)  
  
    ```  
  
-   The following example returns the aggregated error counts for the current week:  
  
    ```  
    Use msdb  
    Go  
    DECLARE @startofweek datetime  
    DECLARE @endofweek datetime  
    SET @startofweek = DATEADD(Day, 1-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)   
    SET @endofweek = DATEADD(Day, 7-DATEPART(WEEKDAY, CURRENT_TIMESTAMP), CURRENT_TIMESTAMP)  
    SELECT *  
    FROM managed_backup.fn_get_health_status(@startofweek, @endofweek)  
  
    ```  
  
  
