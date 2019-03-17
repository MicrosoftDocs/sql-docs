---
title: "sp_datatype_info_90 (SQL Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.service: sql-data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 1d043964-dc6e-4c3e-ab61-bc444d5e25ae
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sp_datatype_info_90 (SQL Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Returns information about the data types supported by the current environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
sp_datatype_info_90 [ [ @data_type = ] data_type ]   
     [ , [ @ODBCVer = ] odbc_version ]   
```  
  
## Arguments  
 [ **@data_type=** ] *data_type*  
 Is the code number for the specified data type. To obtain a list of all data types, omit this parameter. *data_type* is **int**, with a default of 0.  
  
 [ **@ODBCVer=** ] *odbc_version*  
 Is the version of ODBC that is used. *odbc_version* is **tinyint**, with a default of 2.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|TYPE_NAME|**sysname**|DBMS-dependent data type.|  
|DATA_TYPE|**smallint**|Code for the ODBC type to which all columns of this type are mapped.|  
|PRECISION|**int**|Maximum precision of the data type on the data source. NULL is returned for data types for which precision is not applicable. The return value for the PRECISION column is in base 10.|  
|LITERAL_PREFIX|**varchar(**32**)**|Character or characters used before a constant. For example, a single quotation mark (**'**) for character types and 0x for binary.|  
|LITERAL_SUFFIX|**varchar(**32**)**|Character or characters used to terminate a constant. For example, a single quotation mark (**'**) for character types and no quotation marks for binary.|  
|CREATE_PARAMS|**varchar(**32**)**|Description of the creation parameters for this data type. For example, **decimal** is "precision, scale", **float** is NULL, and **varchar** is "max_length".|  
|NULLABLE|**smallint**|Specifies nullability.<br /><br /> 1 = Allows null values.<br /><br /> 0 = Does not allow null values.|  
|CASE_SENSITIVE|**smallint**|Specifies case sensitivity.<br /><br /> 1 = All columns of this type are case-sensitive (for collations).<br /><br /> 0 = All columns of this type are case-insensitive.|  
|SEARCHABLE|**smallint**|Specifies the search capability of the column type:<br /><br /> 1 = Cannot be searched.<br /><br /> 2 = Searchable with LIKE.<br /><br /> 3 = Searchable with WHERE.<br /><br /> 4 = Searchable with WHERE or LIKE.|  
|UNSIGNED_ATTRIBUTE|**smallint**|Specifies the sign of the data type.<br /><br /> 1 = Data type unsigned.<br /><br /> 0 = Data type signed.|  
|MONEY|**smallint**|Specifies the **money** data type.<br /><br /> 1 = **money** data type.<br /><br /> 0 = Not a **money** data type.|  
|AUTO_INCREMENT|**smallint**|Specifies autoincrementing.<br /><br /> 1 = Autoincrementing.<br /><br /> 0 = Not autoincrementing.<br /><br /> NULL = Attribute not applicable.<br /><br /> An application can insert values into a column that has this attribute, but the application cannot update the values in the column. With the exception of the **bit** data type, AUTO_INCREMENT is valid only for data types that belong to the Exact Numeric and Approximate Numeric data type categories.|  
|LOCAL_TYPE_NAME|**sysname**|Localized version of the data source-dependent name of the data type. For example, DECIMAL is DECIMALE in French. NULL is returned if a localized name is not supported by the data source.|  
|MINIMUM_SCALE|**smallint**|Minimum scale of the data type on the data source. If a data type has a fixed scale, the MINIMUM_SCALE and MAXIMUM_SCALE columns both contain this value. NULL is returned where scale is not applicable.|  
|MAXIMUM_SCALE|**smallint**|Maximum scale of the data type on the data source. If the maximum scale is not defined separately on the data source, but is instead defined to be the same as the maximum precision, this column contains the same value as the PRECISION column.|  
|SQL_DATA_TYPE|**smallint**|Value of the SQL data type as it appears in the TYPE field of the descriptor. This column is the same as the DATA_TYPE column, except for the **datetime** and ANSI **interval** data types. This field always returns a value.|  
|SQL_DATETIME_SUB|**smallint**|**datetime** or ANSI **interval** subcode if the value of SQL_DATA_TYPE is SQL_DATETIME or SQL_INTERVAL. For data types other than **datetime** and ANSI **interval**, this field is NULL.|  
|NUM_PREC_RADIX|**int**|Number of bits or digits for calculating the maximum number that a column can hold. If the data type is an approximate numeric data type, this column contains the value 2 to indicate several bits. For exact numeric types, this column contains the value 10 to indicate several decimal digits. Otherwise, this column is NULL. By combining the precision with radix, the application can calculate the maximum number that the column can hold.|  
|INTERVAL_PRECISION|**smallint**|Value of interval leading precision if *data_type* is **interval**; otherwise NULL.|  
|USERTYPE|**smallint**|**usertype** value from the systypes table.|  
  
## Remarks  
 sp_datatype_info is equivalent to SQLGetTypeInfo in ODBC. The results returned are ordered by DATA_TYPE and then by how closely the data type maps to the corresponding ODBC SQL data type.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example retrieves information for the **sysname** and **nvarchar** data types by specifying the *data_type* value of `-9`.  
  
```  
USE master;  
GO  
EXEC sp_datatype_info_90 -9;  
GO  
```  
  
## See Also  
 [SQL Data Warehouse Stored Procedures](../../relational-databases/system-stored-procedures/sql-data-warehouse-stored-procedures.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  

