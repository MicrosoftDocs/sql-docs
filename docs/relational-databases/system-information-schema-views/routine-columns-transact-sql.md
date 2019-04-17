---
title: "ROUTINE_COLUMNS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "ROUTINE_COLUMNS_TSQL"
  - "ROUTINE_COLUMNS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ROUTINE_COLUMNS view"
  - "INFORMATION_SCHEMA.ROUTINE_COLUMNS view"
ms.assetid: 91dbc61b-e4c0-4826-976c-b2fce88b7793
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ROUTINE_COLUMNS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns one row for each column returned by the table-valued functions that can be accessed by the current user in the current database.  
  
 To retrieve information from this view, specify the fully qualified name of **INFORMATION_SCHEMA.**_view_name_.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Catalog or database name of the table-valued function.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of the schema that contains the table-valued function.<br /><br /> <strong>\*\* Important \*\*</strong> Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|**TABLE_NAME**|**nvarchar(**128**)**|Name of the table-valued function.|  
|**COLUMN_NAME**|**nvarchar(**128**)**|Column name.|  
|**ORDINAL_POSITION**|**int**|Column identification number.|  
|**COLUMN_DEFAULT**|**nvarchar(**4000**)**|Default value of the column.|  
|**IS_NULLABLE**|**varchar(**3**)**|If this column allows for NULL, returns YES. Otherwise, returns NO.|  
|**DATA_TYPE**|**nvarchar(**128**)**|System-supplied data type.|  
|**CHARACTER_MAXIMUM_LENGTH**|**int**|Maximum length, in characters, for binary data, character data, or text and image data.<br /><br /> -1 for **xml** and large-value type data. Otherwise, returns NULL. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).|  
|**CHARACTER_OCTET_LENGTH**|**int**|Maximum length, in bytes, for binary data, character data, or text and image data.<br /><br /> -1 for **xml** and large-value type data. Otherwise, returns NULL.|  
|**NUMERIC_PRECISION**|**tinyint**|Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**NUMERIC_PRECISION_RADIX**|**smallint**|Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**NUMERIC_SCALE**|**tinyint**|Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, returns NULL.|  
|**DATETIME_PRECISION**|**smallint**|Subtype code for **datetime** and ISO**integer** data types. For other data types, returns NULL.|  
|**CHARACTER_SET_CATALOG**|**varchar(**6**)**|Returns **master**. This indicates the database in which the character set is located if the column is character data or **text** data type. Otherwise, returns NULL.|  
|**CHARACTER_SET_SCHEMA**|**varchar(**3**)**|Always returns NULL.|  
|**CHARACTER_SET_NAME**|**nvarchar(**128**)**|Returns the unique name for the character set if this column is character data or **text** data type. Otherwise, returns NULL.|  
|**COLLATION_CATALOG**|**varchar(**6**)**|Always returns NULL.|  
|**COLLATION_SCHEMA**|**varchar(**3**)**|Always returns NULL.|  
|**COLLATION_NAME**|**nvarchar(**128**)**|Returns the unique name for the sort order if the column is character data or **text** data type. Otherwise, returns NULL.|  
|**DOMAIN_CATALOG**|**nvarchar(**128**)**|If the column is an alias data type, this column is the database name in which the user-defined data type was created. Otherwise, returns NULL.|  
|**DOMAIN_SCHEMA**|**nvarchar(**128**)**|If the column is a user-defined data type, this column is the name of the schema that contains the user-defined data type. Otherwise, returns NULL.<br /><br /> <strong>\*\* Important \*\*</strong> Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.|  
|**DOMAIN_NAME**|**nvarchar(**128**)**|If the column is a user-defined data type, this column is the name of the user-defined data type. Otherwise, returns NULL.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)  
  
  
