---
title: "sp_sproc_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_sproc_columns"
  - "sp_sproc_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_sproc_columns"
ms.assetid: 62c18c21-35c5-4772-be0d-ffdcc19c97ab
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_sproc_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns column information for a single stored procedure or user-defined function in the current environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
sp_sproc_columns [[@procedure_name = ] 'name']   
    [ , [@procedure_owner = ] 'owner']   
    [ , [@procedure_qualifier = ] 'qualifier']   
    [ , [@column_name = ] 'column_name']  
    [ , [@ODBCVer = ] 'ODBCVer']  
    [ , [@fUsePattern = ] 'fUsePattern']  
```  
  
## Arguments  
 [ **@procedure_name =** ] **'***name***'**  
 Is the name of the procedure used to return catalog information. *name* is **nvarchar(**390**)**, with a default of %, which means all tables in the current database. Wildcard pattern matching is supported.  
  
 [ **@procedure_owner =**] **'***owner***'**  
 Is the name of the owner of the procedure. *owner*is **nvarchar(**384**)**, with a default of NULL. Wildcard pattern matching is supported. If *owner* is not specified, the default procedure visibility rules of the underlying DBMS apply.  
  
 If the current user owns a procedure with the specified name, information about that procedure is returned. If *owner*is not specified and the current user does not own a procedure with the specified name, **sp_sproc_columns** looks for a procedure with the specified name that is owned by the database owner. If the procedure exists, information about its columns is returned.  
  
 [ **@procedure_qualifier =**] **'***qualifier***'**  
 Is the name of the procedure qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier.owner.name*). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this parameter represents the database name. In some products, it represents the server name of the table's database environment.  
  
 [ **@column_name =**] **'***column_name***'**  
 Is a single column and is used when only one column of catalog information is desired. *column_name* is **nvarchar(**384**)**, with a default of NULL. If *column_name* is omitted, all columns are returned. Wildcard pattern matching is supported. For maximum interoperability, the gateway client should assume only ISO standard pattern matching (the % and _ wildcard characters).  
  
 [ **@ODBCVer =**] **'***ODBCVer***'**  
 Is the version of ODBC being used. *ODBCVer* is **int**, with a default of 2, which indicates ODBC version 2.0. For more information about the difference between ODBC version 2.0 and ODBC version 3.0, refer to the ODBC **SQLProcedureColumns** specification for ODBC version 3.0  
  
 [ **@fUsePattern =**] **'***fUsePattern***'**  
 Determines whether the underscore (_), percent (%), and bracket ([ ]) characters are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *fUsePattern* is **bit**, with a default of 1.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**PROCEDURE_QUALIFIER**|**sysname**|Procedure qualifier name. This column can be NULL.|  
|**PROCEDURE_OWNER**|**sysname**|Procedure owner name. This column always returns a value.|  
|**PROCEDURE_NAME**|**nvarchar(**134**)**|Procedure name. This column always returns a value.|  
|**COLUMN_NAME**|**sysname**|Column name for each column of the **TABLE_NAME** returned. This column always returns a value.|  
|**COLUMN_TYPE**|**smallint**|This field always returns a value:<br /><br /> 0 = SQL_PARAM_TYPE_UNKNOWN<br /><br /> 1 = SQL_PARAM_TYPE_INPUT<br /><br /> 2 = SQL_PARAM_TYPE_OUTPUT<br /><br /> 3 = SQL_RESULT_COL<br /><br /> 4 = SQL_PARAM_OUTPUT<br /><br /> 5 = SQL_RETURN_VALUE|  
|**DATA_TYPE**|**smallint**|Integer code for an ODBC data type. If this data type cannot be mapped to an ISO type, the value is NULL. The native data type name is returned in the **TYPE_NAME** column.|  
|**TYPE_NAME**|**sysname**|String representation of the data type. This is the data type name as presented by the underlying DBMS.|  
|**PRECISION**|**int**|Number of significant digits. The return value for the **PRECISION** column is in base 10.|  
|**LENGTH**|**int**|Transfer size of the data.|  
|**SCALE**|**smallint**|Number of digits to the right of the decimal point.|  
|**RADIX**|**smallint**|Is the base for numeric types.|  
|**NULLABLE**|**smallint**|Specifies nullability:<br /><br /> 1 = Data type can be created allowing null values.<br /><br /> 0 = Null values are not allowed.|  
|**REMARKS**|**varchar(**254**)**|Description of the procedure column. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value for this column.|  
|**COLUMN_DEF**|**nvarchar(**4000**)**|Default value of the column.|  
|**SQL_DATA_TYPE**|**smallint**|Value of the SQL data type as it appears in the **TYPE** field of the descriptor. This column is the same as the **DATA_TYPE** column, except for the **datetime** and ISO **interval** data types. This column always returns a value.|  
|**SQL_DATETIME_SUB**|**smallint**|The **datetime** ISO **interval** subcode if the value of **SQL_DATA_TYPE** is **SQL_DATETIME** or **SQL_INTERVAL**. For data types other than **datetime** and ISO **interval**, this field is NULL.|  
|**CHAR_OCTET_LENGTH**|**int**|Maximum length in bytes of a **character** or **binary** data type column. For all other data types, this column returns a NULL.|  
|**ORDINAL_POSITION**|**int**|Ordinal position of the column in the table. The first column in the table is 1. This column always returns a value.|  
|**IS_NULLABLE**|**varchar(254)**|Nullability of the column in the table. ISO rules are followed to determine nullability. An ISO compliant DBMS cannot return an empty string.<br /><br /> Displays YES if the column can include NULLS and NO if the column cannot include NULLS.<br /><br /> This column returns a zero-length string if nullability is unknown.<br /><br /> The value returned for this column is different from the value returned for the NULLABLE column.|  
|**SS_DATA_TYPE**|**tinyint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type used by extended stored procedures. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).|  
  
## Remarks  
 **sp_sproc_columns** is equivalent to **SQLProcedureColumns** in ODBC. The results returned are ordered by **PROCEDURE_QUALIFIER**, **PROCEDURE_OWNER**, **PROCEDURE_NAME**, and the order that the parameters appear in the procedure definition.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## See Also  
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
