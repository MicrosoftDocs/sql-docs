---
title: "sys.all_sql_modules (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "all_sql_modules_TSQL"
  - "sys.all_sql_modules"
  - "all_sql_modules"
  - "sys.all_sql_modules_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.all_sql_modules catalog view"
ms.assetid: 7477a3fe-afb3-44c8-bb2c-c6e1d9bdee6f
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.all_sql_modules (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the union of **sys.sql_modules** and **sys.system_sql_modules**.  
  
 The view returns a row for each natively compiled, scalar user-defined function. For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object of the containing object. Is unique within a database.|  
|**definition**|**nvarchar(max)**|SQL text that defines this module.<br /><br /> NULL = Encrypted|  
|**uses_ansi_nulls**|**bit**|Module was created with SET ANSI_NULLS ON.|  
|**uses_quoted_identifier**|**bit**|Module was created with SET QUOTED_IDENTIFIER ON.|  
|**is_schema_bound**|**bit**|Module was created with the SCHEMABINDING option.|  
|**uses_database_collation**|**bit**|1 = Schema-bound module definition depends on the default-collation of the database for correct evaluation; otherwise, 0. Such a dependency prevents changing the default collation of the database.|  
|**is_recompiled**|**bit**|Procedure was created using the WITH RECOMPILE option.|  
|**null_on_null_input**|**bit**|Module was declared to produce a NULL output on any NULL input.|  
|**execute_as_principal_id**|**int**|ID of the EXECUTE AS database principal.<br /><br /> NULL by default or if EXECUTE AS CALLER.<br /><br /> ID of the specified principal if EXECUTE AS SELF or EXECUTE AS \<principal>.<br /><br /> -2 = EXECUTE AS OWNER.|  
|**uses_native_compilation**|bit|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 0 = not natively compiled<br /><br /> 1 = is natively compiled<br /><br /> The default value is 0.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.system_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-sql-modules-transact-sql.md)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
