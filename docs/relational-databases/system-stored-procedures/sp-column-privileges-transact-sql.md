---
description: "sp_column_privileges (Transact-SQL)"
title: "sp_column_privileges (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_column_privileges_TSQL"
  - "sp_column_privileges"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_column_privileges"
ms.assetid: a3784301-2517-4b1d-bbd9-47404483fad0
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_column_privileges (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns column privilege information for a single table in the current environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_column_privileges [ @table_name = ] 'table_name'   
     [ , [ @table_owner = ] 'table_owner' ]   
     [ , [ @table_qualifier = ] 'table_qualifier' ]   
     [ , [ @column_name = ] 'column' ]  
```  
  
## Arguments  
 [ @table_name= ] '*table_name*'  
 Is the table used to return catalog information. *table_name* is **sysname**, with no default. Wildcard pattern matching is not supported.  
  
 [ @table_owner= ] '*table_owner*'  
 Is the owner of the table used to return catalog information. *table_owner* is **sysname**, with a default of NULL. Wildcard pattern matching is not supported. If *table_owner* is not specified, the default table visibility rules of the underlying database management system (DBMS) apply.  
  
 If the current user owns a table with the specified name, that table's columns are returned. If *table_owner* is not specified and the current user does not own a table with the specified *table_name*, sp_column privileges looks for a table with the specified *table_name* owned by the database owner. If one exists, that table's columns are returned.  
  
 [ @table_qualifier= ] '*table_qualifier*'  
 Is the name of the table qualifier. *table_qualifier* is *sysname*, with a default of NULL. Various DBMS products support three-part naming for tables (_qualifier_**.**_owner_**.**_name_). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment.  
  
 [ @column_name= ] '*column*'  
 Is a single column used when only one column of catalog information is being obtained. *column* is **nvarchar(**384**)**, with a default of NULL. If *column* is not specified, all columns are returned. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], *column* represents the column name as listed in the sys.columns table. *column* can include wildcard characters using wildcard matching patterns of the underlying DBMS. For maximum interoperability, the gateway client should assume only ISO standard pattern matching (the % and _ wildcard characters).  
  
## Result Sets  
 sp_column_privileges is equivalent to SQLColumnPrivileges in ODBC. The results returned are ordered by TABLE_QUALIFIER, TABLE_OWNER, TABLE_NAME, COLUMN_NAME, and PRIVILEGE.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|TABLE_QUALIFIER|**sysname**|Table qualifier name. This field can be NULL.|  
|TABLE_OWNER|**sysname**|Table owner name. This field always returns a value.|  
|TABLE_NAME|**sysname**|Table name. This field always returns a value.|  
|COLUMN_NAME|**sysname**|Column name, for each column of the TABLE_NAME returned. This field always returns a value.|  
|GRANTOR|**sysname**|Database user name that has granted permissions on this COLUMN_NAME to the listed GRANTEE. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as the TABLE_OWNER. This field always returns a value.<br /><br /> The GRANTOR column can be either the database owner (TABLE_OWNER) or a user to whom the database owner granted permissions by using the WITH GRANT OPTION clause in the GRANT statement.|  
|GRANTEE|**sysname**|Database user name that has been granted permissions on this COLUMN_NAME by the listed GRANTOR. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column always includes a database user from the sysusers table. This field always returns a value.|  
|PRIVILEGE|**varchar(**32**)**|One of the available column permissions. Column permissions can be one of the following values (or other values supported by the data source when implementation is defined):<br /><br /> SELECT = GRANTEE can retrieve data for the columns.<br /><br /> INSERT = GRANTEE can provide data for this column when new rows are inserted (by the GRANTEE) into the table.<br /><br /> UPDATE = GRANTEE can modify existing data in the column.<br /><br /> REFERENCES = GRANTEE can reference a column in a foreign table in a primary key/foreign key relationship. Primary key/foreign key relationships are defined by using table constraints.|  
|IS_GRANTABLE|**varchar(**3**)**|Indicates whether the GRANTEE is permitted to grant permissions to other users (often referred to as "grant with grant" permission). Can be YES, NO, or NULL. An unknown, or NULL, value refers to a data source for which "grant with grant" is not applicable.|  
  
## Remarks  
 With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], permissions are granted with the GRANT statement and taken away by the REVOKE statement.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
 The following example returns column privilege information for a specific column.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_column_privileges @table_name = 'Employee'   
    ,@table_owner = 'HumanResources'  
    ,@table_qualifier = 'AdventureWorks2012'  
    ,@column_name = 'SalariedFlag';  
```  
  
## See Also  
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
