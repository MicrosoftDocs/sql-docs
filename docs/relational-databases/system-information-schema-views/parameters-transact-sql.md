---
title: "PARAMETERS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "PARAMETERS_TSQL"
  - "PARAMETERS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PARAMETERS view"
  - "INFORMATION_SCHEMA.PARAMETERS view"
ms.assetid: 06ded0ca-7d21-4400-864a-b801e855b257
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PARAMETERS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns one row for each parameter of a user-defined function or stored procedure that can be accessed by the current user in the current database. For functions, this view also returns one row with return value information.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA.**_view_name_.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**SPECIFIC_CATALOG**|**nvarchar(**128**)**|Catalog name of the routine for which this is a parameter.|  
|**SPECIFIC_SCHEMA**|**nvarchar(**128**)**|Name of the schema of the routine for which this is a parameter.<br /><br /> <strong>\*\* Important \*\*</strong> Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|**SPECIFIC_NAME**|**nvarchar(**128**)**|Name of the routine for which this is a parameter.|  
|**ORDINAL_POSITION**|**int**|Ordinal position of the parameter starting at 1. For the return value of a function, this is a 0.|  
|**PARAMETER_MODE**|**nvarchar(**10**)**|Returns IN if an input parameter, OUT if an output parameter, and INOUT if an input/output parameter.|  
|**IS_RESULT**|**nvarchar(**10**)**|Returns YES if indicates result of the routine that is a function. Otherwise, returns NO.|  
|**AS_LOCATOR**|**nvarchar(**10**)**|Returns YES if declared as locator. Otherwise, returns NO.|  
|**PARAMETER_NAME**|**nvarchar(**128**)**|Name of the parameter. NULL if this corresponds to the return value of a function.|  
|**DATA_TYPE**|**nvarchar(**128**)**|System-supplied data type.|  
|**CHARACTER_MAXIMUM_LENGTH**|**int**|Maximum length in characters for binary or character data types.<br /><br /> -1 for **xml** and large-value type data. Otherwise, returns NULL.|  
|**CHARACTER_OCTET_LENGTH**|**int**|Maximum length, in bytes, for binary or character data types.<br /><br /> -1 for **xml** and large-value type data. Otherwise, returns NULL.|  
|**COLLATION_CATALOG**|**nvarchar(**128**)**|Always returns NULL.|  
|**COLLATION_SCHEMA**|**nvarchar(**128**)**|Always returns NULL.|  
|**COLLATION_NAME**|**nvarchar(**128**)**|Name of the collation of the parameter. If not one of the character types, returns NULL.|  
|**CHARACTER_SET_CATALOG**|**nvarchar(**128**)**|Catalog name of the character set of the parameter. If not one of the character types, returns NULL.|  
|**CHARACTER_SET_SCHEMA**|**nvarchar(**128**)**|Always returns NULL.|  
|**CHARACTER_SET_NAME**|**nvarchar(**128**)**|Name of the character set of the parameter. If not one of the character types, returns NULL.|  
|**NUMERIC_PRECISION**|**tinyint**|Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**NUMERIC_PRECISION_RADIX**|**smallint**|Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**NUMERIC_SCALE**|**tinyint**|Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**DATETIME_PRECISION**|**smallint**|Precision in fractional seconds if the parameter type is **datetime** or **smalldatetime**. Otherwise, returns NULL.|  
|**INTERVAL_TYPE**|**nvarchar(**30**)**|NULL. Reserved for future use.|  
|**INTERVAL_PRECISION**|**smallint**|NULL. Reserved for future use.|  
|**USER_DEFINED_TYPE_CATALOG**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|**USER_DEFINED_TYPE_SCHEMA**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|**USER_DEFINED_TYPE_NAME**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|**SCOPE_CATALOG**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|**SCOPE_SCHEMA**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
|**SCOPE_NAME**|**nvarchar(**128**)**|NULL. Reserved for future use.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md)  
  
  
