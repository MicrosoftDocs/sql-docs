---
description: "core.sp_update_data_source (Transact-SQL)"
title: "core.sp_update_data_source (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_update_data_source"
  - "sp_update_data_source_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_update_data_source"
  - "management data warehouse, data collector stored procedures"
  - "core.sp_update_data_source stored procedure"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 66b95f96-6df7-4657-9b3c-86a58c788ca5
author: markingmyname
ms.author: maghan
---
# core.sp_update_data_source (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Updates an existing row or inserts a new row in the management data warehouse core.source_info_internal table. This procedure is called by the data collector run-time component every time an upload package starts uploading data to the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
core.sp_update_data_source [ @collection_set_uid = ] 'collection_set_uid'  
    ,[ @machine_name = ] 'machine_name'  
    , [ @named_instance = ] 'named_instance'  
    , [ @days_until_expiration = ] days_until_expiration  
    , [ @source_id = ] source_id OUTPUT  
```  
  
## Arguments  
 [ @collection_set_uid = ] '*collection_set_uid*'  
 The GUID for the collection set. *collection_set_uid* is **uniqueidentifier**, with no default value. To obtain the GUID, query the dbo.syscollector_collection_sets view in the msdb database.  
  
 [ @machine_name = ] '*machine_name*'  
 The name of the server that the collection set resides on. *machine_name* is **sysname** with no default value.  
  
 [ @named_instance = ] '*named_instance*'  
 The name of the instance for the collection set. *named_instance* is **sysname**, with no default value.  
  
> [!NOTE]  
>  *named_instance* must be the fully qualified instance name, which consists of the computer name and the instance name in the form *computername*\\*instancename*.  
  
 [ @days_until_expiration = ] *days_until_expiration*  
 The number of days remaining in the snapshot data retention period. *days_until_expiration* is **smallint**.  
  
 [ @source_id = ] *source_id*  
 The unique identifier for the source of the update. *source_id* is **int** and is returned as OUTPUT.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Every time an upload package starts uploading data to the management data warehouse, the data collector run-time component calls core.sp_update_data_source. The core.source_info_internal table is updated if one of the following changes has happened since the last upload:  
  
-   A new collection set was added.  
  
-   The value for days_until_expiration has changed.  
  
## Permissions  
 Requires membership in the **mdw_writer** (with EXECUTE permission) fixed database role.  
  
## Examples  
 The following example updates the data source (in this case the Disk Usage collection set), sets the number of days until expiration, and returns the identifier for the source. In the example, the default instance is used.  
  
```  
USE <management_data_warehouse>;  
GO  
DECLARE @source_id int;  
EXEC core.sp_update_data_source   
@collection_set_uid = '7B191952-8ECF-4E12-AEB2-EF646EF79FEF',   
@machine_name = '<computername>',  
@named_instance = 'MSSQLSERVER',  
@days_until_expiration = 10,  
@source_id = @source_id OUTPUT;  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Management Data Warehouse](../../relational-databases/data-collection/management-data-warehouse.md)  
  
  
