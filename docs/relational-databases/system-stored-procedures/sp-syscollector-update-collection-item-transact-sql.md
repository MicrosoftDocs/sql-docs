---
description: "sp_syscollector_update_collection_item (Transact-SQL)"
title: "sp_syscollector_update_collection_item (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_syscollector_update_collection_item"
  - "sp_syscollector_update_collection_item_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_update_collection_item"
ms.assetid: 7a0d36c8-c6e9-431d-a5a4-6c1802bce846
author: markingmyname
ms.author: maghan
---
# sp_syscollector_update_collection_item (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Used to modify the properties of a user-defined collection item or to rename a user-defined collection item.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_update_collection_item   
      [ [ @collection_item_id = ] collection_item_id ]  
    , [ [ @name = ] 'name' ]  
    , [ [ @new_name = ] 'new_name' ]  
    , [ [ @frequency = ] frequency ]  
    , [ [ @parameters = ] 'parameters' ]  
```  
  
## Arguments  
 [ @collection_item_id = ] *collection_item_id*  
 Is the unique identifer that identifies the collection item. *collection_item_id* is **int** with a default value of NULL. *collection_item_id* must have a value if *name* is NULL.  
  
 [ @name = ] '*name*'  
 Is the name of the collection item. *name* is **sysname** with a default value of NULL. *name* must have a value if *collection_item_id* is NULL.  
  
 [ @new_name = ] '*new_name*'  
 Is the new name for the collection item. *new_name* is **sysname**, and if used, cannot be an empty string.  
  
 *new_name* must be unique. For a list of current collection item names, query the syscollector_collection_items system view.  
  
 [ @frequency = ] *frequency*  
 Is the frequency (in seconds) that data is collected by this collection item. *frequency* is **int**, with a default of 5, the minimum value that can be specified.  
  
 [ @parameters = ] '*parameters*'  
 The input parameters for the collection item. *parameters* is **xml** with a default of NULL. The *parameters* schema must match the parameters schema of the collector type.  
  
## Return Code Values  
 **0** (success) or 1 (failure)  
  
## Remarks  
 If the collection set is set to non-cached mode, changing the frequency is ignored because this mode causes both data collection and upload to occur at the schedule specified for the collection set. To view the status of the collection set, run the following query. Replace `<collection_item_id>` with the ID of the collection item to be updated.  
  
```  
USE msdb;  
GO  
SELECT cs.collection_set_id, collection_set_uid, cs.name   
    ,'is running' = CASE WHEN is_running =  0 THEN 'No' ELSE 'Yes' END  
    ,'cache mode' = CASE WHEN collection_mode = 0 THEN 'Cached mode' ELSE 'Non-cached mode' END  
FROM syscollector_collection_sets AS cs  
JOIN syscollector_collection_items AS ci   
ON ci.collection_set_id = cs.collection_set_id  
WHERE collection_item_id = <collection_item_id>;  
```  
  
## Permissions  
 Requires membership in the dc_admin or the dc_operator (with EXECUTE permission) fixed database role to execute this procedure. Although dc_operator can run this stored procedure, members of this role are limited in the properties that they can change. The following properties can only be changed by dc_admin:  
  
-   @new_name  
  
-   @parameters  
  
## Examples  
 The following examples are based on the collection item created in the example defined in [sp_syscollector_create_collection_item &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-item-transact-sql.md).  
  
### A. Changing the collection frequency  
 The following example changes the collection frequency for the specified collection item.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_update_collection_item   
@name = N'My custom TSQL query collector item',  
@frequency = 3000;  
GO  
```  
  
### B. Renaming a collection item  
 The following example renames a collection item.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_update_collection_item   
@name = N'My custom TSQL query collector item',  
@new_name = N'My modified TSQL item';  
GO  
```  
  
### C. Changing the parameters of a collection item  
 The following example changes the parameters associated with the collection item. The statement defined within the `<Value>` attribute is changed and the `UseSystemDatabases` attribute is set to false. To view the current parameters for this item, query the parameters column in the syscollector_collection_items system view. You may need to modify the value for `@collection_item_id`.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_update_collection_item   
@collection_item_id = 9,   
@parameters = '  
    \<ns:TSQLQueryCollector xmlns:ns="DataCollectorType">  
        <Query>  
            <Value>SELECT * FROM sys.dm_db_index_usage_stats</Value>  
            <OutputTable>MyOutputTable</OutputTable>  
        </Query>  
        <Databases>  
            <Database> UseSystemDatabases = "false"   
                       UseUserDatabases = "true"</Database>  
        </Databases>  
    \</ns:TSQLQueryCollector>';  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [sp_syscollector_create_collection_item &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-item-transact-sql.md)   
 [syscollector_collection_items &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-items-transact-sql.md)  
  
  
