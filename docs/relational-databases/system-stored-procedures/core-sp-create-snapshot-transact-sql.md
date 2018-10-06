---
title: "core.sp_create_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_create_snapshot"
  - "sp_create_snapshot_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "management data warehouse, data collector stored procedures"
  - "data collector [SQL Server], stored procedures"
  - "core.sp_create_snapshot stored procedure"
  - "sp_create_snapshot"
ms.assetid: ff297bda-0ee2-4fda-91c8-7000377775e3
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# core.sp_create_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Inserts a row in the management data warehouse core.snapshots view. This procedure is called every time an upload package starts uploading data to the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
core.sp_create_snapshot [ @collection_set_uid = ] 'collection_set_uid'  
    , [ @collector_type_uid = ] 'collector_type_uid'  
    ,[ @machine_name = ] 'machine_name'  
    , [ @named_instance = ] 'named_instance'  
    , [ @log_id = ] log_id  
    , [ @snapshot_id = ] snapshot_id OUTPUT  
```  
  
## Arguments  
 [ @collection_set_uid = ] '*collection_set_uid*'  
 The GUID for the collection set. *collection_set_uid* is **uniqueidentifier** with no default value. To obtain the GUID, query the dbo.syscollector_collection_sets view in the msdb database.  
  
 [ @collector_type_uid = ] '*collector_type_uid*'  
 The GUID for a collector type. *collector_type_uid* is **uniqueidentifier** with no default value. To obtain the GUID, query the dbo.syscollector_collector_types view in the msdb database.  
  
 [ @machine_name= ] '*machine_name*'  
 The name of the server that the collection set resides on. *machine_name* is **sysname**, with no default value.  
  
 [ @named_instance= ] '*named_instance*'  
 The name of the instance for the collection set. *named_instance* is **sysname**, with no default value.  
  
 [ @log_id = ] *log_id*  
 The unique identifier that maps to the collection set event log on the server that collected the data. *log_id* is **bigint** with no default value. To obtain the value for *log_id*, query the dbo.syscollector_execution_log view in the msdb database.  
  
 [ @snapshot_id = ] *snapshot_id*  
 The unique identifier for a row that is inserted into the core.snapshots view. *snapshot_id* is **int** and is returned as OUTPUT.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Every time an upload package starts uploading data to the management data warehouse, the data collector run-time component calls core.sp_create_snapshot.  
  
 This procedure checks to see if:  
  
-   The collection_set_uid matches an existing entry in the core.source_info_internal table.  
  
-   The collector_type_uid matches an existing entry in the core.supported_collector_types view.  
  
 If either of the preceding checks fails, the procedure fails and returns an error.  
  
## Permissions  
 Requires membership in the **mdw_writer** (with EXECUTE permission) fixed database role.  
  
## Examples  
 The following example creates a snapshot for the Disk Usage collection set, adds it to the management data warehouse, and returns the snapshot identifier. In the example, the default instance is used.  
  
```  
USE <management_data_warehouse>;  
DECLARE @snapshot_id int;  
EXEC core.sp_create_snapshot   
    @collection_set_uid = '7B191952-8ECF-4E12-AEB2-EF646EF79FEF',   
    @collector_type_uid = '302E93D1-3424-4BE7-AA8E-84813ECF2419',  
    @machine_name = '<computername>',  
    @named_instance = 'MSSQLSERVER',  
    @log_id = 11, -- ID of the log for the collection set  
    @snapshot_id = @snapshot_id OUTPUT;  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Management Data Warehouse](../../relational-databases/data-collection/management-data-warehouse.md)  
  
  
