---
title: "ROUTINES (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "ROUTINES_TSQL"
  - "ROUTINES"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ROUTINES view"
  - "INFORMATION_SCHEMA.ROUTINES view"
ms.assetid: c75561b2-c9a1-48a1-9afa-a5896b6454cf
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ROUTINES (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row for each stored procedure and function that can be accessed by the current user in the current database. The columns that describe the return value apply only to functions. For stored procedures, these columns will be NULL.  
  
 To retrieve information from these views, specify the fully qualified name of INFORMATION_SCHEMA.*view_name*.  
  
> [!NOTE]  
>  The ROUTINE_DEFINITION column contains the source statements that created the function or stored procedure. These source statements are likely to contain embedded carriage returns. If you are returning this column to an application that displays the results in a text format, the embedded carriage returns in the ROUTINE_DEFINITION results may affect the formatting of the overall result set. If you select the ROUTINE_DEFINITION column, you must adjust for the embedded carriage returns; for example, by returning the result set into a grid or returning ROUTINE_DEFINITION into its own text box.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|SPECIFIC_CATALOG|**nvarchar(**128**)**|Specific name of the catalog. This name is the same as ROUTINE_CATALOG.|  
|SPECIFIC_SCHEMA|**nvarchar(**128**)**|Specific name of the schema.<br /><br /> **\*\* Important \*\*** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|SPECIFIC_NAME|**nvarchar(**128**)**|Specific name of the catalog. This name is the same as ROUTINE_NAME.|  
|ROUTINE_CATALOG|**nvarchar(**128**)**|Catalog name of the function.|  
|ROUTINE_SCHEMA|**nvarchar(**128**)**|Name of the schema that contains this function.<br /><br /> **\*\* Important \*\*** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|ROUTINE_NAME|**nvarchar(**128**)**|Name of the function.|  
|ROUTINE_TYPE|**nvarchar(**20**)**|Returns PROCEDURE for stored procedures, and FUNCTION for functions.|  
|MODULE_CATALOG|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|MODULE_SCHEMA|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|MODULE_NAME|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|UDT_CATALOG|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|UDT_SCHEMA|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|UDT_NAME|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|DATA_TYPE|**nvarchar(**128**)**|Data type of the return value of the function. Returns **table** if a table-valued function.|  
|CHARACTER_MAXIMUM_LENGTH|**int**|Maximum length in characters, if the return type is a character type.<br /><br /> -1 for **xml** and large-value type data.|  
|CHARACTER_OCTET_LENGTH|**int**|Maximum length in bytes, if the return type is a character type.<br /><br /> -1 for **xml** and large-value type data.|  
|COLLATION_CATALOG|**nvarchar(**128**)**|Always returns NULL.|  
|COLLATION_SCHEMA|**nvarchar(**128**)**|Always returns NULL.|  
|COLLATION_NAME|**nvarchar(**128**)**|Collation name of the return value. For noncharacter types, returns NULL.|  
|CHARACTER_SET_CATALOG|**nvarchar(**128**)**|Always returns NULL.|  
|CHARACTER_SET_SCHEMA|**nvarchar(**128**)**|Always returns NULL.|  
|CHARACTER_SET_NAME|**nvarchar(**128**)**|Name of the character set of the return value. For noncharacter types, returns NULL.|  
|NUMERIC_PRECISION|**smallint**|Numeric precision of the return value. For the nonnumeric types, returns NULL.|  
|NUMERIC_PRECISION_RADIX|**smallint**|Numeric precision radix of the return value. For nonnumeric types, returns NULL.|  
|NUMERIC_SCALE|**smallint**|Scale of the return value. For nonnumeric types, returns NULL.|  
|DATETIME_PRECISION|**smallint**|Fractional precision of a second if the return value is of type **datetime**. Otherwise, returns NULL.|  
|INTERVAL_TYPE|**nvarchar(**30**)**|NULL. Reserved for future use.|  
|INTERVAL_PRECISION|**smallint**|NULL. Reserved for future use.|  
|TYPE_UDT_CATALOG|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|TYPE_UDT_SCHEMA|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|TYPE_UDT_NAME|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|SCOPE_CATALOG|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|SCOPE_SCHEMA|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|SCOPE_NAME|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|MAXIMUM_CARDINALITY|**bigint**|NULL. Reserved for future use.|  
|DTD_IDENTIFIER|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|ROUTINE_BODY|**nvarchar(**30**)**|Returns SQL for a [!INCLUDE[tsql](../../includes/tsql-md.md)] function and EXTERNAL for an externally written function.<br /><br /> Functions will always be SQL.|  
|ROUTINE_DEFINITION|**nvarchar(**4000**)**|Returns the first 4000 characters of the definition text of the function or stored procedure if the function or stored procedure is not encrypted. Otherwise, returns NULL.<br /><br /> To ensure that you obtain the complete definition, query the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) function or the definition column in the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view.|  
|EXTERNAL_NAME|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|EXTERNAL_LANGUAGE|**nvarchar(**30**)**|NULL. Reserved for future use.|  
|PARAMETER_STYLE|**nvarchar(**30**)**|NULL. Reserved for future use.|  
|IS_DETERMINISTIC|**nvarchar(**10**)**|Returns YES if the routine is deterministic.<br /><br /> Returns NO if the routine is nondeterministic.<br /><br /> Always returns NO for stored procedures.|  
|SQL_DATA_ACCESS|**nvarchar(**30**)**|Returns one of the following values:<br /><br /> NONE = Function does not contain SQL.<br /><br /> CONTAINS = Function possibly contains SQL.<br /><br /> READS = Function possibly reads SQL data.<br /><br /> MODIFIES = Function possibly modifies SQL data.<br /><br /> Returns READS for all functions, and MODIFIES for all stored procedures.|  
|IS_NULL_CALL|**nvarchar(**10**)**|Indicates whether the routine will be called if any one of its arguments is NULL.|  
|SQL_PATH|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|SCHEMA_LEVEL_ROUTINE|**nvarchar(**10**)**|Returns YES if schema-level function, or NO if not a schema-level function.<br /><br /> Always returns YES.|  
|MAX_DYNAMIC_RESULT_SETS|**smallint**|Maximum number of dynamic result sets returned by routine.<br /><br /> Returns 0 if functions.|  
|IS_USER_DEFINED_CAST|**nvarchar(**10**)**|Returns YES if user-defined cast function, and NO if not a user-defined cast function.<br /><br /> Always returns NO.|  
|IS_IMPLICITLY_INVOCABLE|**nvarchar(**10**)**|Returns YES if the routine can be implicitly invoked, and NO if function cannot be implicitly invoked.<br /><br /> Always returns NO.|  
|CREATED|**datetime**|Time when the routine was created.|  
|LAST_ALTERED|**datetime**|The last time the function was modified.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.procedures &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-procedures-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)  
  
  
