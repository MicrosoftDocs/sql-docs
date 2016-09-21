---
title: "sp_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8bf39582-1073-47c4-a4db-b48013ba792f
caps.latest.revision: 8
author: BarbKess
---
# sp_columns (SQL Server PDW)
Returns column information for the specified objects that can be queried in the current environment.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_columns [ @table_name= ] object    
     [ , [ @table_owner= ] owner ]   
     [ , [ @table_qualifier= ] qualifier ]   
     [ , [ @column_name= ] column ]   
     [ , [ @ODBCVer= ] ODBCVer ]  
```  
  
## Arguments  
[ **@table_name=**] *object*  
Is the name of the object that is used to return catalog information. *object* can be a table, view, or other object that has columns such as table-valued functions. *object* is **nvarchar(384)**, with no default. Wildcard pattern matching is supported.  
  
[ **@table_owner****=**] *owner*  
Is the object owner of the object that is used to return catalog information. *owner* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported. If *owner* is not specified, the default object visibility rules of the underlying DBMS apply.  
  
If the current user owns an object with the specified name, the columns of that object are returned. If *owner* is not specified and the current user does not own an object with the specified *object*, **sp_columns** looks for an object with the specified *object* owned by the database owner. If one exists, that object's columns are returned.  
  
[ **@table_qualifier****=**] *qualifier*  
Is the name of the object qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for objects (*qualifier***.***owner***.***name*). In SQL Server, this column represents the database name. In some products, it represents the server name of the object's database environment.  
  
[ **@column_name=**] *column*  
Is a single column and is used when only one column of catalog information is wanted. *column* is **nvarchar(384)**, with a default of NULL. If *column* is not specified, all columns are returned. In SQL Server, *column* represents the column name as listed in the **syscolumns** table. Wildcard pattern matching is supported. For maximum interoperability, the gateway client should assume only SQL-92 standard pattern matching (the % and _ wildcard characters).  
  
[ **@ODBCVer=**] *ODBCVer*  
Is the version of ODBC that is being used. *ODBCVer* is **int**, with a default of 2. This indicates ODBC Version 2. Valid values are 2 or 3. For the behavior differences between versions 2 and 3, see the ODBC **SQLColumns** specification.  
  
## Return Code Values  
None  
  
## Result Sets  
The **sp_columns** catalog stored procedure is equivalent to **SQLColumns** in ODBC. The results returned are ordered by **TABLE_QUALIFIER**, **TABLE_OWNER**, and **TABLE_NAME**.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
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
|**NULLABLE**|**smallint**|Specifies nullability.<br /><br />1 = NULL is possible.<br /><br />0 = NOT NULL.|  
|**REMARKS**|**varchar(254)**|This field always returns NULL.|  
|**COLUMN_DEF**|**nvarchar(4000)**|Default value of the column.|  
|**SQL_DATA_TYPE**|**smallint**|Value of the SQL data type as it appears in the TYPE field of the descriptor. This column is the same as the **DATA_TYPE** column, except for the **datetime** and SQL-92 **interval** data types. This column always returns a value.|  
|**SQL_DATETIME_SUB**|**smallint**|Subtype code for **datetime** and SQL-92 **interval** data types. For other data types, this column returns NULL.|  
|**CHAR_OCTET_LENGTH**|**int**|Maximum length in bytes of a character or integer data type column. For all other data types, this column returns NULL.|  
|**ORDINAL_POSITION**|**int**|Ordinal position of the column in the object. The first column in the object is 1. This column always returns a value.|  
|**IS_NULLABLE**|**varchar(254)**|Nullability of the column in the object. ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS cannot return an empty string.<br /><br />YES = Column can include NULLS.<br /><br />NO = Column cannot include NULLS.<br /><br />This column returns a zero-length string if nullability is unknown.<br /><br />The value returned for this column is different from the value returned for the **NULLABLE** column.|  
|**SS_DATA_TYPE**|**tinyint**|SQL Server data type used by extended stored procedures. For more information, see [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).|  
  
<sup>1</sup> For more information, see the Microsoft ODBC documentation.  
  
## Permissions  
Requires SELECT permission on the schema.  
  
## Remarks  
**sp_columns** follows the requirements for delimited identifiers. For more information, see [Database Identifiers](http://msdn.microsoft.com/en-us/library/ms175874.aspx).  
  
## Examples  
The following example returns column information for a specified table.  
  
```  
USE AdventureWorksPDW2012;  
GO  
EXEC sp_columns @table_name = N'DimEmployee',  
   @table_owner = N'dbo';  
```  
  
## See Also  
[sp_tables &#40;SQL Server PDW&#41;](../sqlpdw/sp-tables-sql-server-pdw.md)  
[Procedures &#40;SQL Server PDW&#41;](../sqlpdw/procedures-sql-server-pdw.md)  
  
