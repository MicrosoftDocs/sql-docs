---
title: "DROP TABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/12/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_TABLE_TSQL"
  - "DROP TABLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "removing indexes"
  - "table removal [SQL Server]"
  - "deleting indexes"
  - "dropping data"
  - "deleting tables"
  - "dropping indexes"
  - "removing constraints"
  - "removing permissions"
  - "DROP TABLE statement"
  - "removing tables"
  - "deleting triggers"
  - "removing data"
  - "dropping tables"
  - "deleting permissions"
  - "dropping triggers"
  - "deleting constraints"
  - "removing triggers"
  - "deleting data"
  - "dropping constraints"
  - "dropping permissions"
ms.assetid: 0b6f2b6f-3aa3-4767-943f-43df3c3c5cfd
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP TABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more table definitions and all data, indexes, triggers, constraints, and permission specifications for those tables. Any view or stored procedure that references the dropped table must be explicitly dropped by using [DROP VIEW](../../t-sql/statements/drop-view-transact-sql.md) or [DROP PROCEDURE](../../t-sql/statements/drop-procedure-transact-sql.md). To report the dependencies on a table, use [sys.dm_sql_referencing_entities](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
DROP TABLE [ IF EXISTS ] [ database_name . [ schema_name ] . | schema_name . ]  
table_name [ ,...n ]  
[ ; ]  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
DROP TABLE [ database_name . [ schema_name ] . | schema_name . ] table_name   
[;]  
```  
  
## Arguments  
 *database_name*  
 Is the name of the database in which the table was created.  
  
 Windows Azure SQL Database supports the three-part name format database_name.[schema_name].object_name when the database_name is the current database or the database_name is tempdb and the object_name starts with #. Windows Azure SQL Database does not support four-part names.  
  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the table only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the table belongs.  
  
 *table_name*  
 Is the name of the table to be removed.  
  
## Remarks  
 DROP TABLE cannot be used to drop a table that is referenced by a FOREIGN KEY constraint. The referencing FOREIGN KEY constraint or the referencing table must first be dropped. If both the referencing table and the table that holds the primary key are being dropped in the same DROP TABLE statement, the referencing table must be listed first.  
  
 Multiple tables can be dropped in any database. If a table being dropped references the primary key of another table that is also being dropped, the referencing table with the foreign key must be listed before the table holding the primary key that is being referenced.  
  
 When a table is dropped, rules or defaults on the table lose their binding, and any constraints or triggers associated with the table are automatically dropped. If you re-create a table, you must rebind the appropriate rules and defaults, re-create any triggers, and add all required constraints.  
  
 If you delete all rows in a table by using DELETE *tablename* or use the TRUNCATE TABLE statement, the table exists until it is dropped.  
  
 Large tables and indexes that use more than 128 extents are dropped in two separate phases: logical and physical. In the logical phase, the existing allocation units used by the table are marked for deallocation and locked until the transaction commits. In the physical phase, the IAM pages marked for deallocation are physically dropped in batches.  
  
 If you drop a table that contains a VARBINARY(MAX) column with the FILESTREAM attribute, any data stored in the file system will not be removed.  
  
> [!IMPORTANT]  
>  DROP TABLE and CREATE TABLE should not be executed on the same table in the same batch. Otherwise an unexpected error may occur.  
  
## Permissions  
 Requires ALTER permission on the schema to which the table belongs, CONTROL permission on the table, or membership in the **db_ddladmin** fixed database role.  
  
## Examples  
  
### A. Dropping a table in the current database  
 The following example removes the `ProductVendor1` table and its data and indexes from the current database.  
  
```  
DROP TABLE ProductVendor1 ;  
```  
  
### B. Dropping a table in another database  
 The following example drops the `SalesPerson2` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The example can be executed from any database on the server instance.  
  
```  
DROP TABLE AdventureWorks2012.dbo.SalesPerson2 ;  
```  
  
### C. Dropping a temporary table  
 The following example creates a temporary table, tests for its existence, drops it, and tests again for its existence. This example does not use the **IF EXISTS** syntax which is available beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
```  
CREATE TABLE #temptable (col1 int);  
GO  
INSERT INTO #temptable  
VALUES (10);  
GO  
SELECT * FROM #temptable;  
GO  
IF OBJECT_ID(N'tempdb..#temptable', N'U') IS NOT NULL   
DROP TABLE #temptable;  
GO  
--Test the drop.  
SELECT * FROM #temptable;  
  
```  
  
### D. Dropping a table using IF EXISTS  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 The following example creates a table named T1. Then the second statement drops the table. The third statement performs no action because the table is already deleted, however it does not cause an error.  
  
```  
CREATE TABLE T1 (Col1 int);  
GO  
DROP TABLE IF EXISTS T1;  
GO  
DROP TABLE IF EXISTS T1;  
```  
  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [TRUNCATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/truncate-table-transact-sql.md)   
 [DROP VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/drop-view-transact-sql.md)   
 [DROP PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-procedure-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
  
