---
description: "core.sp_purge_data (Transact-SQL)"
title: "core.sp_purge_data (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_purge_data_TSQL"
  - "sp_purge_data"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_purge_data"
  - "management data warehouse, data collector stored procedures"
  - "core.sp_purge_data stored procedure"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 056076c3-8adf-4f51-8a1b-ca39696ac390
author: markingmyname
ms.author: maghan
---
# core.sp_purge_data (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes data from the management data warehouse based on a retention policy. This procedure is executed daily by the mdw_purge_data [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job against the management data warehouse associated with the specified instance. You can use this stored procedure to perform an on-demand removal of data from the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
core.sp_purge_data  
    [ [ @retention_days = ] retention_days ]  
    [ , [ @instance_name = ] 'instance_name' ]  
    [ , [ @collection_set_uid = ] 'collection_set_uid' ]  
    [ , [ @duration = ] duration ]  
```  
  
## Arguments  
 [@retention_days =] *retention_days*  
 The number of days to retain data in the management data warehouse tables. Data with a time stamp older than *retention_days* is removed. *retention_days* is **smallint**, with a default of NULL. If specified, the value must be positive. When NULL, the value in the valid_through column in the core.snapshots view determines the rows that are eligible for removal.  
  
 [@instance_name = ] '*instance_name*'  
 The name of the instance for the collection set. *instance_name* is **sysname**, with a default of NULL.  
  
 *instance_name* must be the fully qualified instance name, which consists of the computer name and the instance name in the form *computername*\\*instancename*. When NULL, the default instance on the local server is used.  
  
 [@collection_set_uid = ] '*collection_set_uid*'  
 The GUID for the collection set. *collection_set_uid* is **uniqueidentifier**, with a default of NULL. When NULL, qualifying rows from all collection sets are removed. To obtain this value, query the syscollector_collection_sets catalog view.  
  
 [@duration = ] *duration*  
 The maximum number of minutes the purge operation should run. *duration* is **smallint**, with a default of NULL. If specified, the value must be zero or a positive integer. When NULL, the operation runs until all qualified rows are removed or the operation is manually stopped.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 This procedure selects rows in the core.snapshots view that qualify for removal based on a retention period. All rows that qualify for removal are deleted from the core.snapshots_internal table. Deleting the preceding rows triggers a cascading delete action in all of the management data warehouse tables. This is done by using the ON DELETE CASCADE clause, which is defined for all the tables that store collected data.  
  
 Each snapshot and its associated data are deleted within an explicit transaction and then committed. Therefore, if the purge operation is manually stopped, or the value specified for @duration is exceeded, only the uncommitted data remains. This data can be removed the next time the job runs.  
  
 The procedure must be executed in the context of the management data warehouse database.  
  
## Permissions  
 Requires membership in the **mdw_admin** (with EXECUTE permission) fixed database role.  
  
## Examples  
  
### A. Running sp_purge_data with no parameters  
 The following example executes core.sp_purge_data without specifying any parameters. Therefore, the default value of NULL is used for all parameters, with the associated behavior.  
  
```  
USE <management_data_warehouse>;  
EXECUTE core.sp_purge_data;  
GO  
```  
  
### B. Specifying retention and duration values  
 The following example removes data from the management data warehouse that is older than 7 days. In addition, the @duration parameter is specified so that the operation will run no longer than 5 minutes.  
  
```  
USE <management_data_warehouse>;  
EXECUTE core.sp_purge_data @retention_days = 7, @duration = 5;  
GO  
```  
  
### C. Specifying an instance name and collection set  
 The following example removes data from the management data warehouse for a given collection set on the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Because @retention_days is not specified, the value in the valid_through column in the core.snapshots view is used to determine the rows for the collection set that are eligible for removal.  
  
```  
USE <management_data_warehouse>;  
GO  
-- Get the collection set unique identifier for the Disk Usage system collection set.  
DECLARE @disk_usage_collection_set_uid uniqueidentifier = (SELECT collection_set_uid   
    FROM msdb.dbo.syscollector_collection_sets WHERE name = N'Disk Usage');   
  
EXECUTE core.sp_purge_data @instance_name = @@SERVERNAME, @collection_set_uid = @disk_usage_collection_set_uid;  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)  
  
  
