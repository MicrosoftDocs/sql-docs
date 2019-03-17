---
title: "sp_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_columns_TSQL"
  - "sp_columns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_columns"
ms.assetid: 2dec79cf-2baf-4c0f-8cbb-afb1a8654e1e
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns column information for the specified objects that can be queried in the current environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
sp_columns [ @table_name = ] object  
     [ , [ @table_owner = ] owner ]   
     [ , [ @table_qualifier = ] qualifier ]   
     [ , [ @column_name = ] column ]   
     [ , [ @ODBCVer = ] ODBCVer ]  
```  
  
## Arguments  
 [ **\@table_name=**] *object*  
 Is the name of the object that is used to return catalog information. *object* can be a table, view, or other object that has columns such as table-valued functions. *object* is **nvarchar(384)**, with no default. Wildcard pattern matching is supported.  
  
 [ **\@table_owner=**] *owner*  
 Is the object owner of the object that is used to return catalog information. *owner* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported. If *owner* is not specified, the default object visibility rules of the underlying DBMS apply.  
  
 If the current user owns an object with the specified name, the columns of that object are returned. If *owner* is not specified and the current user does not own an object with the specified *object*, **sp_columns** looks for an object with the specified *object* owned by the database owner. If one exists, that object's columns are returned.  
  
 [ **\@table_qualifier=**] *qualifier*  
 Is the name of the object qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for objects (_qualifier_**.**_owner_**.**_name_). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the object's database environment.  
  
 [ **\@column_name=**] *column*  
 Is a single column and is used when only one column of catalog information is wanted. *column* is **nvarchar(384)**, with a default of NULL. If *column* is not specified, all columns are returned. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], *column* represents the column name as listed in the **syscolumns** table. Wildcard pattern matching is supported. For maximum interoperability, the gateway client should assume only SQL-92 standard pattern matching (the % and _ wildcard characters).  
  
 [ **\@ODBCVer=**] *ODBCVer*  
 Is the version of ODBC that is being used. *ODBCVer* is **int**, with a default of 2. This indicates ODBC Version 2. Valid values are 2 or 3. For the behavior differences between versions 2 and 3, see the ODBC **SQLColumns** specification.  
  
## Return Code Values  
 None  
  
## Result Sets  
 The **sp_columns** catalog stored procedure is equivalent to **SQLColumns** in ODBC. The results returned are ordered by **TABLE_QUALIFIER**, **TABLE_OWNER**, and **TABLE_NAME**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_QUALIFIER**|**sysname**|Object qualifier name. This field can be NULL.|  
|**TABLE_OWNER**|**sysname**|Object owner name. This field always returns a value.|  
|**TABLE_NAME**|**sysname**|Object name. This field always returns a value.|  
|**COLUMN_NAME**|**sysname**|Column name, for each column of the **TABLE_NAME** returned. This field always returns a value.|  
|**DATA_TYPE**|**smallint**|Integer code for ODBC data type. If this is a data type that cannot be mapped to an ODBC type, it is NULL. The native data type name is returned in the **TYPE_NAME** column.|  
|**TYPE_NAME**|**sysname**|String representing a data type. The underlying DBMS presents this data type name.|  
|**PRECISION**|**int**|Number of significant digits. The return value for the **PRECISION** column is in base 10.|  
|**LENGTH**|**int**|Transfer size of the data.<sup>1</sup>|  
|**SCALE**|**smallint**|Number of digits to the right of the decimal point.|  
|**RADIX**|**smallint**|Base for numeric data types.|  
|**NULLABLE**|**smallint**|Specifies nullability.<br /><br /> 1 = NULL is possible.<br /><br /> 0 = NOT NULL.|  
|**REMARKS**|**varchar(254)**|This field always returns NULL.|  
|**COLUMN_DEF**|**nvarchar(4000)**|Default value of the column.|  
|**SQL_DATA_TYPE**|**smallint**|Value of the SQL data type as it appears in the TYPE field of the descriptor. This column is the same as the **DATA_TYPE** column, except for the **datetime** and SQL-92 **interval** data types. This column always returns a value.|  
|**SQL_DATETIME_SUB**|**smallint**|Subtype code for **datetime** and SQL-92 **interval** data types. For other data types, this column returns NULL.|  
|**CHAR_OCTET_LENGTH**|**int**|Maximum length in bytes of a character or integer data type column. For all other data types, this column returns NULL.|  
|**ORDINAL_POSITION**|**int**|Ordinal position of the column in the object. The first column in the object is 1. This column always returns a value.|  
|**IS_NULLABLE**|**varchar(254)**|Nullability of the column in the object. ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS cannot return an empty string.<br /><br /> YES = Column can include NULLS.<br /><br /> NO = Column cannot include NULLS.<br /><br /> This column returns a zero-length string if nullability is unknown.<br /><br /> The value returned for this column is different from the value returned for the **NULLABLE** column.|  
|**SS_DATA_TYPE**|**tinyint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type used by extended stored procedures. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).|  
  
 <sup>1</sup> For more information, see the Microsoft ODBC documentation.  
  
## Permissions  
 Requires SELECT and VIEW DEFINITION permissions on the schema.  
  
## Remarks  
 **sp_columns** follows the requirements for delimited identifiers. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
## Examples  
 The following example returns column information for a specified table.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_columns @table_name = N'Department',  
   @table_owner = N'HumanResources';  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns column information for a specified table.  
  
```  
-- Uses AdventureWorks  
  
EXEC sp_columns @table_name = N'DimEmployee',  
   @table_owner = N'dbo';  
```  
  
## See Also  
 [sp_tables &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tables-transact-sql.md)   
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  


