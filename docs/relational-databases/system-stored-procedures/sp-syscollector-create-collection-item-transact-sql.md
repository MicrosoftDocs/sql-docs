---
title: "sp_syscollector_create_collection_item (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_create_collection_item"
  - "sp_syscollector_create_collection_item_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_create_collection_item"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 60dacf13-ca12-4844-b417-0bc0a8bf0ddb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_create_collection_item (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a collection item in a user-defined collection set. A collection item defines the data to be collected and the frequency with which the data is collected.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_create_collection_item   
      [ @collection_set_id = ] collection_set_id   
    , [ @collector_type_uid = ] 'collector_type_uid'  
    , [ @name = ] 'name'   
    , [ [ @frequency = ] frequency ]  
    , [ @parameters = ] 'parameters'  
    , [ @collection_item_id = ] collection_item_id OUTPUT  
```  
  
## Arguments  
 [ @collection_set_id = ] *collection_set_id*  
 Is the unique local identifier for the collection set. *collection_set_id* is **int**.  
  
 [ @collector_type_uid = ] '*collector_type_uid*'  
 Is the GUID that identifies the collector type to use for this item *collector_type_uid* is **uniqueidentifier** with no default value.. For a list of collector types, query the syscollector_collector_types system view.  
  
 [ @name = ] '*name*'  
 Is the name of the collection item. *name* is **sysname** and cannot be an empty string or NULL.  
  
 *name* must be unique. For a list of current collection item names, query the syscollector_collection_items system view.  
  
 [ @frequency = ] *frequency*  
 Is used to specify (in seconds) how frequently data is collected by this collection item. *frequency* is **int**, with a default of 5. The minimum value that can be specified is 5 seconds.  
  
 If the collection set is set to non-cached mode, the frequency is ignored because this mode causes both data collection and upload to occur at the schedule specified for the collection set. To view the collection mode of the collection set, query the [syscollector_collection_sets](../../relational-databases/system-catalog-views/syscollector-collection-sets-transact-sql.md) system view.  
  
 [ @parameters = ] '*parameters*'  
 The input parameters for the collector type. *parameters* is **xml** with a default of NULL. The *parameters* schema must match the parameters schema of the collector type.  
  
 [ @collection_item_id = ] *collection_item_id*  
 Is the unique identifer that identifies the collection set item. *collection_item_id* is **int** and has OUTPUT.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_syscollector_create_collection_item must be run in the context of the msdb system database.  
  
 The collection set to which the collection item is being added must be stopped before creating the collection item. Collection items cannot be added to system collection sets.  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
 The following example creates a collection item based on the collection type `Generic T-SQL Query Collector Type` and adds it to the collection set named `Simple collection set test 2`. To create the specified collection set, run example B in [sp_syscollector_create_collection_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-set-transact-sql.md).  
  
```  
USE msdb;  
GO  
DECLARE @collection_item_id int;  
DECLARE @collection_set_id int = (SELECT collection_set_id   
                                  FROM syscollector_collection_sets  
                                  WHERE name = N'Simple collection set test 2');  
DECLARE @collector_type_uid uniqueidentifier =   
    (SELECT collector_type_uid  
     FROM syscollector_collector_types  
     WHERE name = N'Generic T-SQL Query Collector Type');  
DECLARE @params xml =   
    CONVERT(xml, N'\<ns:TSQLQueryCollector xmlns:ns="DataCollectorType">  
            <Query>  
                <Value>SELECT * FROM sys.objects</Value>  
                <OutputTable>MyOutputTable</OutputTable>  
            </Query>  
            <Databases>   
                <Database> UseSystemDatabases = "true"   
                           UseUserDatabases = "true"  
                </Database>  
            </Databases>  
         \</ns:TSQLQueryCollector>');  
  
EXEC sp_syscollector_create_collection_item  
    @collection_set_id = @collection_set_id,  
    @collector_type_uid = @collector_type_uid,  
    @name = 'My custom TSQL query collector item',  
    @frequency = 6000,  
    @parameters = @params,  
    @collection_item_id = @collection_item_id OUTPUT;  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [sp_syscollector_update_collection_item &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-update-collection-item-transact-sql.md)   
 [sp_syscollector_delete_collection_item &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-delete-collection-item-transact-sql.md)   
 [syscollector_collector_types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collector-types-transact-sql.md)   
 [sp_syscollector_create_collection_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-create-collection-set-transact-sql.md)   
 [syscollector_collection_items &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-collection-items-transact-sql.md)  
  
  
