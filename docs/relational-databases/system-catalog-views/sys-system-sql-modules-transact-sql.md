---
title: "sys.system_sql_modules (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "system_sql_modules_TSQL"
  - "sys.system_sql_modules"
  - "sys.system_sql_modules_TSQL"
  - "system_sql_modules"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.system_sql_modules catalog view"
ms.assetid: ad3548bc-4780-4821-b962-b421d52daed9
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.system_sql_modules (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row per system object that contains an SQL language-defined module. System objects of type FN, IF, P, PC, TF, V have an associated SQL module. To identify the containing object, you can join this view to [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|Object identification number of the containing object, unique within a database.|  
|**definition**|**nvarchar(max)**|SQL text that defines this module.|  
|**uses_ansi_nulls**|**bit**|1 = Module was created with the SET ANSI_NULLS database option ON.<br /><br /> Always returns 1.|  
|**uses_quoted_identifier**|**bit**|1 = Module was created with SET QUOTED_IDENTIFIER ON.<br /><br /> Always returns 1.|  
|**is_schema_bound**|**bit**|0 = Module was not created with the SCHEMABINDING option.<br /><br /> Always returns 0.|  
|**uses_database_collation**|**bit**|0 = Module does not depend on the default collation of the database.<br /><br /> Always returns 0.|  
|**is_recompiled**|**bit**|0 = Procedure was not created by using the WITH RECOMPILE option.<br /><br /> Always returns 0.|  
|**null_on_null_input**|**bit**|0 = Module was not created to produce a NULL output on any NULL input.<br /><br /> Always returns 0.|  
|**execute_as_principal_id**|**int**|Always returns NULL|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.all_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-sql-modules-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)  
  
  
