---
title: "sp_syscollector_start_collection_set (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_start_collection_set_TSQL"
  - "sp_syscollector_start_collection_set"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_start_collection_set"
ms.assetid: d8357180-f51e-4681-99f9-0596fe2d2b53
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_start_collection_set (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Starts a collection set if the collector is already enabled and the collection set is not running. If the collector is not enabled, enable the collector by running [sp_syscollector_enable_collector](../../relational-databases/system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md) and then use this stored procedure to start a collection set.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_start_collection_set   
    [ [ @collection_set_id = ] collection_set_id ]  
    , [[ @name = ] 'name' ]   
```  
  
## Arguments  
`[ @collection_set_id = ] collection_set_id`
 Is the unique local identifier for the collection set. *collection_set_id* is **int** with a default value of NULL. *collection_set_id* must have a value if *name* is NULL.  
  
`[ @name = ] 'name'`
 Is the name of the collection set. *name* is **sysname** with a default value of NULL. *name* must have a value if *collection_set_id* is NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_syscollector_create_collection_set must be run in the context of the msdb system database and SQL Server Agent must be enabled.  
  
 This procedure fails when run against a collection set that does not have a schedule. If the collection set does not have a schedule (because its collection mode is set to non-cached, for example), use the [sp_syscollector_run_collection_set](../../relational-databases/system-stored-procedures/sp-syscollector-run-collection-set-transact-sql.md) stored procedure to start the collection set.  
  
 This procedure enables the collection and upload jobs for the specified collection set, and will immediately start the collection agent job if the collection set has its collection mode set to cached (0). For more information, see [sp_syscollector_create_collection_set](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-set-transact-sql.md).  
  
 If the collection set does not contain any collection items, this operation has no effect. Error 14685 is returned as a warning.  
  
## Permissions  
 Requires membership in the dc_operator fixed database role to execute this procedure. If the collection set does not have a proxy account, membership in the sysadmin fixed server role is required.  
  
## Examples  
 The following example starts a collection set using its identifier.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_start_collection_set @collection_set_id = 1;  
```  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [syscollector_collection_sets &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-sets-transact-sql.md)  
  
  
