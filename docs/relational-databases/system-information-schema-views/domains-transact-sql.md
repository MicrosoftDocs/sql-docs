---
title: "DOMAINS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "DOMAINS_TSQL"
  - "DOMAINS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DOMAINS view"
  - "INFORMATION_SCHEMA.DOMAINS view"
ms.assetid: f0b734d5-816f-4b10-a60c-615931b515c2
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DOMAINS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns one row for each alias data type that can be accessed by the current user in the current database.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA.**_view_name_.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**DOMAIN_CATALOG**|**nvarchar(**128**)**|Database in which the alias data type exists.|  
|**DOMAIN_SCHEMA**|**nvarchar(**128**)**|Name of the schema that contains the alias data type.<br /><br /> **&#42;&#42; Important &#42;&#42;** Do not use INFORMATION_SCHEMA views to determine the schema of a data type. The only reliable way to find the schema of a type is to use the TYPEPROPERTY function.|  
|**DOMAIN_NAME**|**sysname**|Alias data type.|  
|**DATA_TYPE**|**sysname**|System-supplied data type.|  
|**CHARACTER_MAXIMUM_LENGTH**|**int**|Maximum length, in characters, for binary data, character data, or text and image data.<br /><br /> -1 for **xml** and large-value type data. Otherwise, NULL is returned. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).|  
|**CHARACTER_OCTET_LENGTH**|**int**|Maximum length, in bytes, for binary data, character data, or text and image data.<br /><br /> -1 for **xml** and large-value type data. Otherwise, NULL is returned.|  
|**COLLATION_CATALOG**|**varchar(**6**)**|Always returns NULL.|  
|**COLLATION_SCHEMA**|**varchar(**3**)**|Always returns NULL.|  
|**COLLATION_NAME**|**nvarchar(**128**)**|Returns the unique name for the sort order if the column is character data or **text** data type. Otherwise, NULL is returned.|  
|**CHARACTER_SET_CATALOG**|**varchar(**6**)**|Returns **master**. This indicates the database in which the character set is located, if the column is character data or **text** data type. Otherwise, NULL is returned.|  
|**CHARACTER_SET_SCHEMA**|**varchar(**3**)**|Always returns NULL.|  
|**CHARACTER_SET_NAME**|**nvarchar(**128**)**|Returns the unique name for the character set if this column is character data or **text** data type. Otherwise, NULL is returned.|  
|**NUMERIC_PRECISION**|**tinyint**|Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.|  
|**NUMERIC_PRECISION_RADIX**|**smallint**|Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.|  
|**NUMERIC_SCALE**|**tinyint**|Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.|  
|**DATETIME_PRECISION**|**smallint**|Subtype code for **datetime** and ISO **interval** data type. For other data types, this column returns a NULL.|  
|**DOMAIN_DEFAULT**|**nvarchar(**4000**)**|Actual text of the definition [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.syscharsets &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syscharsets-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)   
 [sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)  
  
  
