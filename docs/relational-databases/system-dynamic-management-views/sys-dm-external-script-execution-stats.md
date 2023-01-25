---
title: "sys.dm_external_script_execution_stats"
description: sys.dm_external_script_execution_stats
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/25/2020"
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
f1_keywords:
  - "sys.dm_external_script_execution_stats"
  - "sys.dm_external_script_execution_stats_TSQL"
  - "dm_external_script_execution_stats"
  - "dm_external_script_execution_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_external_script_execution_stats dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 2e99f026-ceb2-42a2-a549-c71d31ed0cf4
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# sys.dm_external_script_execution_stats
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

Returns one row for each type of external script request. The external script requests are grouped by the supported external script language. One row is generated for each registered external script functions. Arbitrary external script functions are not recorded unless sent by a parent process, such as `rxExec`.
  
> [!NOTE]  
> This dynamic management view (DMV) is available only if you have installed and enabled the feature that supports external script execution. For more information, see [R Services in SQL Server 2016](../../machine-learning/r/sql-server-r-services.md), [Machine Learning Services (R, Python) in SQL Server 2017 and later](../../machine-learning/sql-server-machine-learning-services.md) and [Azure Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|language|**nvarchar**|Name of the registered external script language. Each external script must specify the language in the script request to start the associated launcher. |  
|counter_name|**nvarchar**|Name of a registered external script function. Is not nullable.|  
|counter_value|**integer**|Total number of instances that the registered external script function has been called on the server. This value is cumulative, beginning with the time that the feature was installed on the instance, and cannot be reset.|  

## Permissions

 Requires VIEW SERVER STATE permission on server.  
  
> [!NOTE]  
> Users who run external scripts must have the additional permission EXECUTE ANY EXTERNAL SCRIPT, however, this DMV can be used by administrators without this permission.
  
## Remarks

  This DMV is provided for internal telemetry, to monitor overall usage of the new external script execution feature provided in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. The telemetry service starts when LaunchPad does and increments a disk-based counter each time a registered external script function is called.

Generally speaking, performance counters are valid only as long as the process that generated them is active. Therefore, a query on a DMV cannot show detailed data for  services that have stopped running. For example, if a launcher executes external script and yet completes them very quickly, a conventional DMV might not show any data.

Therefore, the counters tracked by this DMV are kept running, and state for sys.dm_external_script_requests is preserved by using writes to disk, even if the instance is shut down.

### Counter Values

In SQL Server 2016 the only external language supported is R and the external script requests are handled by [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)]. In SQL Server 2017 and later, and on Azure SQL Managed Instance, both R and Python are supported external languages and the external script requests are handled by [!INCLUDE[rsql_productname_md](../../includes/rsql-productnamenew-md.md)].

For R, this DMV tracks the number of R calls that are made on an instance. For example, if `rxLinMod` is called and run in parallel, the counter is incremented by 1.

For the R language, the counter values displayed in the *counter_name* field represent the names of registered ScaleR functions. The values in the *counter_value* field represent the cumulative number of instances that the specific ScaleR function. 

For Python, this DMV tracks the number of Python calls that are made on an instance.

The count begins when the feature is installed and enabled on the instance and is cumulative until the file that maintains state is deleted or overwritten by an administrator. Therefore, it is generally not possible to reset the values in *counter_value*. If you want to monitor usage by session, calendar times, or other intervals, we recommend that you capture the counts to a table.

### Registration of external script functions in R

R supports arbitrary scripts, and the R community provides many thousand packages, each with their own functions and methods. However, this DMV monitors only the ScaleR functions that are installed with SQL Server 2016 R Services.

Registration of these functions is performed when the feature is installed, and registered functions cannot be added or deleted.

## Examples  
  
### Viewing the number of R scripts run on the server

 The following example displays the cumulative number of external script executions for the R language.  
  
```sql
SELECT counter_name, counter_value
FROM sys.dm_external_script_execution_stats
WHERE language = 'R';
```  

### Viewing the number of Python scripts run on the server

The following example displays the cumulative number of external script executions for the Python language.  
  
```sql
SELECT counter_name, counter_value
FROM sys.dm_external_script_execution_stats
WHERE language = 'Python';
```  

## See also

+ [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
+ [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)
+ [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md)  
+ [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)  
