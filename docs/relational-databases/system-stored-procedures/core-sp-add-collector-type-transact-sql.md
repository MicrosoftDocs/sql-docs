---
title: "core.sp_add_collector_type (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_collector_type"
  - "sp_add_collector_type_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "core.sp_add_collector_type stored procedure"
  - "management data warehouse, data collector stored procedures"
  - "sp_add_collector_type"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 1d981037-2147-464e-a456-7d8e479bce89
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# core.sp_add_collector_type (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new entry to the core.supported_collector_types view in the management data warehouse database. The procedure must be executed in the context of the management data warehouse database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
core.sp_add_collector_type [ @collector_type_uid = ] 'collector_type_uid'  
```  
  
## Arguments  
 [ @collector_type_uid = ] '*collector_type_uid*'  
 The GUID for the collector type. *collector_type_uid* is **uniqueidentifier**, with no default value.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Permissions  
 Requires membership in the **mdw_admin** (with EXECUTE permission) fixed database role.  
  
## Examples  
 The following example adds the Generic T-SQL Query collector type to the core.supported_collector_types view. By default, the Generic T-SQL Query collector type already exists. Therefore, if you run this code on a default installation, you will receive a message that the collector type already exists.  
  
 This code would run successfully if you had removed the Generic T-SQL Query collector type by using the core.sp_remove_collector_type stored procedure, and then wanted to re-add it as a registered collector type that can upload data to the management data warehouse.  
  
```  
USE <management_data_warehouse>;  
GO  
DECLARE @RC int;  
DECLARE @collector_type_uid uniqueidentifier;  
SELECT @collector_type_uid = (SELECT collector_type_uid FROM msdb.dbo.syscollector_collector_types WHERE name = N'Generic T-SQL Query Collector Type');  
EXECUTE @RC = core.sp_add_collector_type @collector_type_uid;  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Management Data Warehouse](../../relational-databases/data-collection/management-data-warehouse.md)  
  
  
