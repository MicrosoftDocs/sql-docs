---
title: "sys.sql_modules (Transact-SQL)"
description: sys.sql_modules (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/20/2021
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sql_modules_TSQL"
  - "sql_modules"
  - "sql_modules_TSQL"
  - "sys.sql_modules"
helpviewer_keywords:
  - "sys.sql_modules catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sql_modules (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row for each object that is an SQL language-defined module in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], including natively compiled scalar user-defined function. Objects of type P, RF, V, TR, FN, IF, TF, and R have an associated SQL module. Stand-alone defaults, objects of type D, also have an SQL module definition in this view. For a description of these types, see the `type` column in the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view.  
  
 For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object of the containing object. Is unique within a database.|  
|**definition**|**nvarchar(max)**|SQL text that defines this module. This value can also be obtained using the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function.<br /><br /> NULL = Encrypted.|  
|**uses_ansi_nulls**|**bit**|Module was created with SET ANSI_NULLS ON.<br /><br /> Will always be = 0 for rules and defaults.|  
|**uses_quoted_identifier**|**bit**|Module was created with SET QUOTED_IDENTIFIER ON.|  
|**is_schema_bound**|**bit**|Module was created with SCHEMABINDING option.<br /><br /> Always contains a value of 1 for natively compiled stored procedures.|  
|**uses_database_collation**|**bit**|1 = Schema-bound module definition depends on the default-collation of the database for correct evaluation; otherwise, 0. Such a dependency prevents changing the database's default collation.|  
|**is_recompiled**|**bit**|Procedure was created WITH RECOMPILE option.|  
|**null_on_null_input**|**bit**|Module was declared to produce a `NULL` output on any `NULL` input.|  
|**execute_as_principal_id**|**Int**|ID of the EXECUTE AS database principal.<br /><br /> `NULL` by default or if EXECUTE AS CALLER.<br /><br /> ID of the specified principal if EXECUTE AS SELF or EXECUTE AS \<principal>.<br /><br /> -2 = EXECUTE AS OWNER.|  
|**uses_native_compilation**|**bit**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].<br /><br /> 0 = not natively compiled<br /><br /> 1 = is natively compiled<br /><br /> The default value is 0.|  
|**is_inlineable**|**bit**|**Applies to**: [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later.<br/><br />Indicates whether the module is inlineable or not. Inlineability is based on the conditions specified [here](../user-defined-functions/scalar-udf-inlining.md#requirements).<br /><br /> 0 = not inlineable<br /><br /> 1 = is inlineable. <br /><br /> For scalar user-defined functions (UDFs), the value will be 1 if the UDF is inlineable, and 0 otherwise. It always contains a value of 1 for inline table-valued functions (TVFs), and 0 for all other module types.<br />|  
|**inline_type**|**bit**|**Applies to**: [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later.<br /><br />Indicates whether inlining is turned on for the module currently. <br /><br />0 = inlining is turned off<br /><br /> 1 = inlining is turned on.<br /><br /> For scalar user-defined functions (UDFs), the value will be 1 if inlining is turned on (explicitly or implicitly). The value will always be 1 for inline table-valued functions (TVFs), and 0 for other module types.<br />|  

  
## Remarks  
 The SQL expression for a DEFAULT constraint, object of type D, is found in the [sys.default_constraints](../../relational-databases/system-catalog-views/sys-default-constraints-transact-sql.md) catalog view. The SQL expression for a CHECK constraint, object of type C, is found in the [sys.check_constraints](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md) catalog view.  
  
 This information is also described in [sys.dm_db_uncontained_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql.md).  
 
Renaming a stored procedure, function, view, or trigger won't change the name of the corresponding object in the definition column of the `sys.sql_modules` catalog view or the definition returned by the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function. For this reason, we recommend that `sp_rename` not be used to rename these object types. Instead, drop and re-create the object with its new name. Learn more in [sp_rename (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example returns the object_id, schema name, object name, object type, and definition of each module in the current database.  
  
```sql  
SELECT 
          sm.object_id
        , ss.[name] as [schema]
        , o.[name] as object_name
        , o.[type]
        , o.[type_desc]
        , sm.[definition]  
FROM sys.sql_modules AS sm     
JOIN sys.objects AS o 
    ON sm.object_id = o.object_id  
JOIN sys.schemas AS ss
    ON o.schema_id = ss.schema_id  
ORDER BY 
      o.[type]
    , ss.[name]
    , o.[name];  
```  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)
