---
title: "sp_fkeys (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5e9ff92d-4a37-45c2-badc-cf0731bc2152
caps.latest.revision: 6
author: BarbKess
---
# sp_fkeys (SQL Server PDW)
SQL Server PDW does not support foreign keys, but it supports this procedure to allow integration of tools and applications. Returns logical foreign key information for the current database. This procedure shows foreign key relationships including disabled foreign keys.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_fkeys [ @pktable_name= ] 'pktable_name'   
     [ , [ @pktable_owner= ] 'pktable_owner' ]   
     [ , [ @pktable_qualifier= ] 'pktable_qualifier' ]   
     { , [ @fktable_name= ] 'fktable_name' }   
     [ , [ @fktable_owner= ] 'fktable_owner' ]   
     [ , [ @fktable_qualifier= ] 'fktable_qualifier' ]  
```  
  
## Arguments  
[ @pktable_name=] '*pktable_name*'  
Is the name of the table, with the primary key, used to return catalog information. *pktable_name* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. This parameter or the *fktable_name* parameter, or both, must be supplied.  
  
[ @pktable_owner=] '*pktable_owner*'  
Is the name of the owner of the table (with the primary key) used to return catalog information. *pktable_owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *pktable_owner* is not specified, the default table visibility rules of the underlying DBMS apply.  
  
If the current user owns a table with the specified name, that table's columns are returned. If *pktable_owner* is not specified and the current user does not own a table with the specified *pktable_name*, the procedure looks for a table with the specified *pktable_name* owned by the database owner. If one exists, that table's columns are returned.  
  
[ @pktable_qualifier =] '*pktable_qualifier*'  
Is the name of the table (with the primary key) qualifier. *pktable_qualifier* is sysname, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier.owner.name*). The qualifier represents the database name. In some products, it represents the server name of the table's database environment.  
  
[ @fktable_name=] '*fktable_name*'  
Is the name of the table (with a foreign key) used to return catalog information. *fktable_name* is sysname, with a default of NULL. Wildcard pattern matching is not supported. This parameter or the *pktable_name* parameter, or both, must be supplied.  
  
[ @fktable_owner =] '*fktable_owner*'  
Is the name of the owner of the table (with a foreign key) used to return catalog information. *fktable_owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *fktable_owner* is not specified, the default table visibility rules of the underlying DBMS apply.  
  
If the current user owns a table with the specified name, that table's columns are returned. If *fktable_owner* is not specified and the current user does not own a table with the specified *fktable_name*, the procedure looks for a table with the specified *fktable_name* owned by the database owner. If one exists, that table's columns are returned.  
  
[ @fktable_qualifier= ] '*fktable_qualifier*'  
Is the name of the table (with a foreign key) qualifier. *fktable_qualifier* is **sysname**, with a default of NULL. The qualifier represents the database name. In some products, it represents the server name of the table's database environment.  
  
## Return Code Values  
None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|PKTABLE_QUALIFIER|**sysname**|Name of the table (with the primary key) qualifier. This field can be NULL.|  
|PKTABLE_OWNER|**sysname**|Name of the table (with the primary key) owner. This field always returns a value.|  
|PKTABLE_NAME|**sysname**|Name of the table (with the primary key). This field always returns a value.|  
|PKCOLUMN_NAME|**sysname**|Name of the primary key columns, for each column of the TABLE_NAME returned. This field always returns a value.|  
|FKTABLE_QUALIFIER|**sysname**|Name of the table (with a foreign key) qualifier. This field can be NULL.|  
|FKTABLE_OWNER|**sysname**|Name of the table (with a foreign key) owner. This field always returns a value.|  
|FKTABLE_NAME|**sysname**|Name of the table (with a foreign key). This field always returns a value.|  
|FKCOLUMN_NAME|**sysname**|Name of the foreign key column, for each column of the TABLE_NAME returned. This field always returns a value.|  
|KEY_SEQ|**smallint**|Sequence number of the column in a multicolumn primary key. This field always returns a value.|  
|UPDATE_RULE|**smallint**|Action applied to the foreign key when the SQL operation is an update. SQL Server returns 0 or 1 for these columns:<br /><br />0=CASCADE changes to foreign key.<br /><br />1=NO ACTION changes if foreign key is present.|  
|DELETE_RULE|**smallint**|Action applied to the foreign key when the SQL operation is a deletion. SQL Server returns 0 or 1 for these columns:<br /><br />0=CASCADE changes to foreign key.<br /><br />1=NO ACTION changes if foreign key is present.|  
|FK_NAME|**sysname**|Foreign key identifier. It is NULL if not applicable to the data source. SQL Server returns the FOREIGN KEY constraint name.|  
|PK_NAME|**sysname**|Primary key identifier. It is NULL if not applicable to the data source. SQL Server returns the PRIMARY KEY constraint name.|  
  
The results returned are ordered byFKTABLE_QUALIFIER, FKTABLE_OWNER, FKTABLE_NAME, and KEY_SEQ.  
  
## Remarks  
Application coding that includes tables with disabled foreign keys can be implemented by the following:  
  
-   Temporarily disabling constraint checking (ALTER TABLE NOCHECK or CREATE TABLE NOT FOR REPLICATION) while working with the tables, and then enabling it again later.  
  
-   Using triggers or application code to enforce relationships.  
  
If the primary key table name is supplied and the foreign key table name is NULL, sp_fkeys returns all tables that include a foreign key to the given table. If the foreign key table name is supplied and the primary key table name is NULL, sp_fkeys returns all tables related by a primary key/foreign key relationship to foreign keys in the foreign key table.  
  
The sp_fkeys stored procedure is equivalent to SQLForeignKeys in ODBC.  
  
## Permissions  
Requires SELECT permission on the schema.  
  
## Examples  
The following example retrieves a list of foreign keys for the `DimDate` table in the `AdventureWorksPDW2012` database. No rows are returned because SQL Server PDW does not support foreign keys.  
  
```  
EXEC sp_fkeys @pktable_name = N'DimDate;  
```  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/procedures-sql-server-pdw.md)  
[sp_pkeys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pkeys-sql-server-pdw.md)  
  
