---
description: "sp_fkeys (Transact-SQL)"
title: "sp_fkeys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_fkeys"
  - "sp_fkeys_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fkeys"
ms.assetid: 18110444-d38d-4cff-90d2-d1fc6236668b
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fkeys (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns logical foreign key information for the current environment. This procedure shows foreign key relationships including disabled foreign keys.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
sp_fkeys [ @pktable_name = ] 'pktable_name'   
     [ , [ @pktable_owner = ] 'pktable_owner' ]   
     [ , [ @pktable_qualifier = ] 'pktable_qualifier' ]   
     { , [ @fktable_name = ] 'fktable_name' }   
     [ , [ @fktable_owner = ] 'fktable_owner' ]   
     [ , [ @fktable_qualifier = ] 'fktable_qualifier' ]  
```  
  
## Arguments  
 [ @pktable_name=] '*pktable_name*'  
 Is the name of the table, with the primary key, used to return catalog information. *pktable_name* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. This parameter or the *fktable_name* parameter, or both, must be supplied.  
  
 [ @pktable_owner=] '*pktable_owner*'  
 Is the name of the owner of the table (with the primary key) used to return catalog information. *pktable_owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *pktable_owner* is not specified, the default table visibility rules of the underlying DBMS apply.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, that table's columns are returned. If *pktable_owner* is not specified and the current user does not own a table with the specified *pktable_name*, the procedure looks for a table with the specified *pktable_name* owned by the database owner. If one exists, that table's columns are returned.  
  
 [ @pktable_qualifier =] '*pktable_qualifier*'  
 Is the name of the table (with the primary key) qualifier. *pktable_qualifier* is sysname, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier.owner.name*). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the qualifier represents the database name. In some products, it represents the server name of the table's database environment.  
  
 [ @fktable_name=] '*fktable_name*'  
 Is the name of the table (with a foreign key) used to return catalog information. *fktable_name* is sysname, with a default of NULL. Wildcard pattern matching is not supported. This parameter or the *pktable_name* parameter, or both, must be supplied.  
  
 [ @fktable_owner =] '*fktable_owner*'  
 Is the name of the owner of the table (with a foreign key) used to return catalog information. *fktable_owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *fktable_owner* is not specified, the default table visibility rules of the underlying DBMS apply.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, that table's columns are returned. If *fktable_owner* is not specified and the current user does not own a table with the specified *fktable_name*, the procedure looks for a table with the specified *fktable_name* owned by the database owner. If one exists, that table's columns are returned.  
  
 [ @fktable_qualifier= ] '*fktable_qualifier*'  
 Is the name of the table (with a foreign key) qualifier. *fktable_qualifier* is **sysname**, with a default of NULL. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the qualifier represents the database name. In some products, it represents the server name of the table's database environment.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|PKTABLE_QUALIFIER|**sysname**|Name of the table (with the primary key) qualifier. This field can be NULL.|  
|PKTABLE_OWNER|**sysname**|Name of the table (with the primary key) owner. This field always returns a value.|  
|PKTABLE_NAME|**sysname**|Name of the table (with the primary key). This field always returns a value.|  
|PKCOLUMN_NAME|**sysname**|Name of the primary key columns, for each column of the TABLE_NAME returned. This field always returns a value.|  
|FKTABLE_QUALIFIER|**sysname**|Name of the table (with a foreign key) qualifier. This field can be NULL.|  
|FKTABLE_OWNER|**sysname**|Name of the table (with a foreign key) owner. This field always returns a value.|  
|FKTABLE_NAME|**sysname**|Name of the table (with a foreign key). This field always returns a value.|  
|FKCOLUMN_NAME|**sysname**|Name of the foreign key column, for each column of the TABLE_NAME returned. This field always returns a value.|  
|KEY_SEQ|**smallint**|Sequence number of the column in a multicolumn primary key. This field always returns a value.|  
|UPDATE_RULE|**smallint**|Action applied to the foreign key when the SQL operation is an update.  Possible values:<br /> 0=CASCADE changes to foreign key.<br /> 1=NO ACTION changes if foreign key is present.<br />	2 = set null <br />	3 = set default |  
|DELETE_RULE|**smallint**|Action applied to the foreign key when the SQL operation is a deletion. Possible values:<br /> 0=CASCADE changes to foreign key.<br /> 1=NO ACTION changes if foreign key is present.<br />	2 = set null <br />	3 = set default |  
|FK_NAME|**sysname**|Foreign key identifier. It is NULL if not applicable to the data source. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns the FOREIGN KEY constraint name.|  
|PK_NAME|**sysname**|Primary key identifier. It is NULL if not applicable to the data source. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns the PRIMARY KEY constraint name.|  
  
 The results returned are ordered by FKTABLE_QUALIFIER, FKTABLE_OWNER, FKTABLE_NAME, and KEY_SEQ.  
  
## Remarks  
 Application coding that includes tables with disabled foreign keys can be implemented by the following:  
  
-   Temporarily disabling constraint checking (ALTER TABLE NOCHECK or CREATE TABLE NOT FOR REPLICATION) while working with the tables, and then enabling it again later.  
  
-   Using triggers or application code to enforce relationships.  
  
If the primary key table name is supplied and the foreign key table name is NULL, sp_fkeys returns all tables that include a foreign key to the given table. If the foreign key table name is supplied and the primary key table name is NULL, sp_fkeys returns all tables related by a primary key/foreign key relationship to foreign keys in the foreign key table.  
  
The sp_fkeys stored procedure is equivalent to SQLForeignKeys in ODBC.  
  
## Permissions  
 Requires `SELECT` permission on the schema.  
  
## Examples  
 The following example retrieves a list of foreign keys for the `HumanResources.Department` table in the `AdventureWorks2012` database.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_fkeys @pktable_name = N'Department'  
    ,@pktable_owner = N'HumanResources';  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example retrieves a list of foreign keys for the `DimDate` table in the `AdventureWorksPDW2012` database. No rows are returned because [!INCLUDE[ssDW](../../includes/ssdw-md.md)] does not support foreign keys.  
  
```sql  
EXEC sp_fkeys @pktable_name = N'DimDate';  
```  
  
## See Also  
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_pkeys &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-pkeys-transact-sql.md)  
  
  

