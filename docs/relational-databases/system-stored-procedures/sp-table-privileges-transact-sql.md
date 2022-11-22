---
description: "sp_table_privileges (Transact-SQL)"
title: "sp_table_privileges (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_table_privileges"
  - "sp_table_privileges_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_table_privileges"
ms.assetid: 0512e688-4fc0-4557-8dc8-016672c1e3fe
author: markingmyname
ms.author: maghan
---
# sp_table_privileges (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a list of table permissions (such as INSERT, DELETE, UPDATE, SELECT, REFERENCES) for the specified table or tables.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_table_privileges [ @table_name = ] 'table_name'     
     [ , [ @table_owner = ] 'table_owner' ]   
     [ , [ @table_qualifier = ] 'table_qualifier' ]   
     [ , [ @fUsePattern = ] 'fUsePattern' ]  
```  
  
## Arguments  
 [ @table_name= ] '*table_name*'  
 Is the table used to return catalog information. *table_name* is **nvarchar(**384**)**, with no default. Wildcard pattern matching is supported.  
  
 [ @table_owner= ] '*table_owner*'  
 Is the table owner of the table used to return catalog information. *table_owner*is **nvarchar(**384**)**, with a default of NULL. Wildcard pattern matching is supported. If the owner is not specified, the default table visibility rules of the underlying DBMS apply.  
  
 If the current user owns a table with the specified name, the columns of that table are returned. If *owner* is not specified and the current user does not own a table with the specified *name*, this procedure looks for a table with the specified *table_name* owned by the database owner. If one exists, the columns of that table are returned.  
  
 [ @table_qualifier= ] '*table_qualifier*'  
 Is the name of the table qualifier. *table_qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (*qualifier.owner.name*). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment.  
  
 [ @fUsePattern= ] '*fUsePattern*'  
 Determines whether the underscore (_), percent (%), and bracket ([ or ]) characters are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *fUsePattern* is **bit**, with a default of 1.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|TABLE_QUALIFIER|**sysname**|Table qualifier name. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. This field can be NULL.|  
|TABLE_OWNER|**sysname**|Table owner name. This field always returns a value.|  
|TABLE_NAME|**sysname**|Table name. This field always returns a value.|  
|GRANTOR|**sysname**|Database username that has granted permissions on this TABLE_NAME to the listed GRANTEE. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as the TABLE_OWNER. This field always returns a value. Also, the GRANTOR column may be either the database owner (TABLE_OWNER) or a user to whom the database owner granted permission by using the WITH GRANT OPTION clause in the GRANT statement.|  
|GRANTEE|**sysname**|Database username that has been granted permissions on this TABLE_NAME by the listed GRANTOR. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column always includes a database user from the sys.database_principalssystem view. This field always returns a value.|  
|PRIVILEGE|**sysname**|One of the available table permissions. Table permissions can be one of the following values (or other values supported by the data source when implementation is defined):<br /><br /> SELECT = GRANTEE can retrieve data for one or more of the columns.<br /><br /> INSERT = GRANTEE can provide data for new rows for one or more of the columns.<br /><br /> UPDATE = GRANTEE can modify existing data for one or more of the columns.<br /><br /> DELETE = GRANTEE can remove rows from the table.<br /><br /> REFERENCES = GRANTEE can reference a column in a foreign table in a primary key/foreign key relationship. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], primary key/foreign key relationships are defined with table constraints.<br /><br /> The scope of action given to the GRANTEE by a given table privilege is data source-dependent. For example, the UPDATE privilege may permit the GRANTEE to update all columns in a table on one data source and only those columns for which the GRANTOR has UPDATE privilege on another data source.|  
|IS_GRANTABLE|**sysname**|Indicates whether or not the GRANTEE is permitted to grant permissions to other users (often referred to as "grant with grant" permission). Can be YES, NO, or NULL. An unknown (or NULL) value refers to a data source for which "grant with grant" is not applicable.|  
  
## Remarks  
 The sp_table_privileges stored procedure is equivalent to SQLTablePrivileges in ODBC. The results returned are ordered by TABLE_QUALIFIER, TABLE_OWNER, TABLE_NAME, and PRIVILEGE.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
 The following example returns privilege information about all tables with names beginning with the word `Contact`.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_table_privileges   
   @table_name = 'Contact%';  
```  
  
## See Also  
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
