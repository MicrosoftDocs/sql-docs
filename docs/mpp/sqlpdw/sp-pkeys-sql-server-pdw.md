---
title: "sp_pkeys (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3dcba9cc-b420-45aa-be99-0b6a779825d3
caps.latest.revision: 6
author: BarbKess
---
# sp_pkeys (SQL Server PDW)
Returns primary key information for a single table in the current environment.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_pkeys [ @table_name= ] 'name'       
      [ , [ @table_owner= ] 'owner' ]   
   [ , [ @table_qualifier= ] 'qualifier' ]  
```  
  
## Arguments  
[ @table_name= ] '*name*'  
Is the table for which to return information. *name* is **sysname**, with no default. Wildcard pattern matching is not supported.  
  
[ @table_owner= ] '*owner*'  
Specifies the table owner of the specified table. *owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *owner* is not specified, the default table visibility rules of the underlying DBMS apply.  
  
In SQL Server, if the current user owns a table with the specified name, the columns of that table are returned. If the *owner* is not specified and the current user does not own a table with the specified *name*, this procedure looks for a table with the specified *name* owned by the database owner. If one exists, the columns of that table are returned.  
  
[ @table_qualifier= ] '*qualifier*'  
Is the table qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier***.***owner***.***name*). In SQL Server, this column represents the database name. In some products, it represents the server name of the database environment of the table.  
  
## Return Code Values  
None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|TABLE_QUALIFIER|**sysname**|Name of the table qualifier. This field can be NULL.|  
|TABLE_OWNER|**sysname**|Name of the table owner. This field always returns a value.|  
|TABLE_NAME|**sysname**|Name of the table. In SQL Server, this column represents the table name as listed in the sysobjects table. This field always returns a value.|  
|COLUMN_NAME|**sysname**|Name of the column, for each column of the TABLE_NAME returned. In SQL Server, this column represents the column name as listed in the sys.columns table. This field always returns a value.|  
|KEY_SEQ|**smallint**|Sequence number of the column in a multicolumn primary key.|  
|PK_NAME|**sysname**|Primary key identifier. Returns NULL if not applicable to the data source.|  
  
## Remarks  
sp_pkeys returns information about columns explicitly defined with a PRIMARY KEY constraint. Because not all systems support explicitly named primary keys, the gateway implementer determines what constitutes a primary key. Note that the term primary key refers to a logical primary key for a table. It is expected that every key listed as being a logical primary key has a unique index defined on it. This unique index is also returned in sp_statistics.  
  
The sp_pkeys stored procedure is equivalent to SQLPrimaryKeys in ODBC. The results returned are ordered by TABLE_QUALIFIER, TABLE_OWNER, TABLE_NAME, and KEY_SEQ.  
  
## Permissions  
Requires SELECT permission on the schema.  
  
## Examples  
The following example retrieves the primary key for the `DimAccount` table in the `AdventureWorksPDW2012` database. It returns zero rows indicating that the table does not have a primary key.  
  
```  
USE AdventureWorksPDW2012;  
GO  
EXEC sp_pkeys @table_name = N'DimAccount;  
```  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/procedures-sql-server-pdw.md)  
[sp_fkeys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-fkeys-sql-server-pdw.md)  
  
