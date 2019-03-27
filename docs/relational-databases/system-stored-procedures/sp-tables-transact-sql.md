---
title: "sp_tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_tables"
  - "sp_tables_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_tables"
ms.assetid: 787a2fa5-87a1-49bd-938b-6043c245f46b
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a list of objects that can be queried in the current environment. This means any table or view, except synonym objects.  
  
> [!NOTE]  
>  To determine the name of the base object of a synonym, query the [sys.synonyms](../../relational-databases/system-catalog-views/sys-synonyms-transact-sql.md) catalog view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
sp_tables [ [ @table_name = ] 'name' ]   
     [ , [ @table_owner = ] 'owner' ]   
     [ , [ @table_qualifier = ] 'qualifier' ]   
     [ , [ @table_type = ] "type" ]   
     [ , [@fUsePattern = ] 'fUsePattern'];  
```  
  
## Arguments  
`[ @table_name = ] 'name'`
 Is the table used to return catalog information. *name* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported.  
  
`[ @table_owner = ] 'owner'`
 Is the table owner of the table used to return catalog information. *owner* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported. If the owner is not specified, the default table visibility rules of the underlying DBMS apply.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the current user owns a table with the specified name, the columns of that table are returned. If the owner is not specified and the current user does not own a table with the specified name, this procedure looks for a table with the specified name owned by the database owner. If one exists, the columns of that table are returned.  
  
`[ @table_qualifier = ] 'qualifier'`
 Is the name of the table qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables (_qualifier_**.**_owner_**.**_name_). In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment.  
  
``[ , [ @table_type = ] "'type', 'type'" ]``
 Is a list of values, separated by commas, that gives information about all tables of the table types that are specified. These include **TABLE**, **SYSTEMTABLE**, and **VIEW**. *type* is **varchar(100)**, with a default of NULL.  
  
> [!NOTE]  
>  Single quotation marks must enclose each table type, and double quotation marks must enclose the whole parameter. Table types must be uppercase. If SET QUOTED_IDENTIFIER is ON, each single quotation mark must be doubled and the whole parameter must be enclosed in single quotation marks.  
  
`[ @fUsePattern = ] 'fUsePattern'`
 Determines whether the underscore ( _ ), percent ( % ), and bracket ( [ or ] ) characters are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *fUsePattern* is **bit**, with a default of 1.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_QUALIFIER**|**sysname**|Table qualifier name. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. This field can be NULL.|  
|**TABLE_OWNER**|**sysname**|Table owner name. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value.|  
|**TABLE_NAME**|**sysname**|Table name. This field always returns a value.|  
|**TABLE_TYPE**|**varchar(32)**|Table, system table, or view.|  
|**REMARKS**|**varchar(254)**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value for this column.|  
  
## Remarks  
 For maximum interoperability, the gateway client should assume only SQL-92-standard SQL pattern matching (the % and _ wildcard characters).  
  
 Privilege information about the current user's read or write access to a specific table is not always checked. Therefore access is not guaranteed. This result set includes not only tables and views, but also synonyms and aliases for gateways to DBMS products that support those types. If the server attribute **ACCESSIBLE_TABLES** is Y in the result set for **sp_server_info**, only tables that can be accessed by the current user are returned.  
  
 **sp_tables** is equivalent to **SQLTables** in ODBC. The results returned are ordered by **TABLE_TYPE**, **TABLE_QUALIFIER**, **TABLE_OWNER**, and **TABLE_NAME**.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
  
### A. Returning a list of objects that can be queried in the current environment  
 The following example returns a list of objects that can be queries in the current environment.  
  
```  
EXEC sp_tables ;  
```  
  
### B. Returning information about the tables in a specified schema  
 The following example returns information about the tables that belong to the `Person` schema in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_tables   
   @table_name = '%',  
   @table_owner = 'Person',  
   @table_qualifier = 'AdventureWorks2012';  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Returning a list of objects that can be queried in the current environment  
 The following example returns a list of objects that can be queries in the current environment.  
  
```  
EXEC sp_tables ;  
```  
  
### D. Returning information about the tables in a specified schema  
 The following example returns information about the dimension tables in the `AdventureWorksPDW201` database.  
  
```  
-- Uses AdventureWorks  
  
EXEC sp_tables   
   @table_name = 'Dim%',  
   @table_owner = 'dbo',  
   @table_qualifier = 'AdventureWorksPDW2012';  
```  
  
## See Also  
 [sys.synonyms &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-synonyms-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  

