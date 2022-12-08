---
description: "sp_syscollector_delete_collection_item (Transact-SQL)"
title: "sp_syscollector_delete_collection_item (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_syscollector_delete_collection_item"
  - "sp_syscollector_delete_collection_item_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_delete_collecton_item"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 9c2b0990-1d3d-4a59-94a0-3cca6fef4681
author: markingmyname
ms.author: maghan
---
# sp_syscollector_delete_collection_item (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Deletes a collection item from a collection set.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_delete_collection_item [[ @collection_item_id = ] collection_item_id ]  
    , [[ @name = ] 'name' ]   
```  
  
## Arguments  
 [ @collection_item_id = ] *collection_item_id*  
 Is the unique identifier for the collection item. *collection_item_id* is **int** with a default of NULL. *collection_item_id* must have a value if *name* is NULL.  
  
 [ @name = ] '*name*'  
 Is the name of the collection item. *name* is **sysname** with a default value of NULL. *name* must have a value if *collection_item_id* is NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_syscollector_delete_collection_item must be run in the context of the msdb system database. Collection items cannot be deleted from system collection sets.  
  
 The collection set that contains the collection item is stopped and restarted during this operation.  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
 The following example deletes a collection item named `MyCollectionItem1`.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_delete_collection_item @name = 'MyCollectionItem1';  
```  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [sp_syscollector_create_collection_item &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-item-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [syscollector_collection_items &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-items-transact-sql.md)  
  
  
