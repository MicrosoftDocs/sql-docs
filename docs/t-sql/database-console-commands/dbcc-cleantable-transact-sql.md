---
title: "DBCC CLEANTABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CLEANTABLE_TSQL"
  - "DBCC_CLEANTABLE_TSQL"
  - "DBCC CLEANTABLE"
  - "CLEANTABLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "disk space [SQL Server], reclaiming"
  - "reclaiming space"
  - "reallocating space"
  - "removing columns"
  - "DBCC CLEANTABLE statement"
  - "space [SQL Server], reclaiming"
  - "deleting columns"
  - "dropping columns"
ms.assetid: 0dbbc956-15b1-427b-812c-618a044d07fa
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC CLEANTABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
Reclaims space from dropped variable-length columns in tables or indexed views.
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
  
DBCC CLEANTABLE  
(  
    { database_name | database_id | 0 }  
    , { table_name | table_id | view_name | view_id }  
    [ , batch_size ]  
)  
[ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
 *database_name* | *database_id* | 0  
 Is the database in which the table to be cleaned belongs. If 0 is specified, the current database is used. Database names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 *table_name* | *table_id* | *view_name*| *view_id*  
 Is the table or indexed view to be cleaned.  
  
 *batch_size*  
 Is the number of rows processed per transaction. If not specified, or if 0 is specified, the statement processes the whole table in one transaction.  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages.  
  
## Remarks  
DBCC CLEANTABLE reclaims space after a variable-length column is dropped. A variable-length column can be one of the following data types: **varchar**, **nvarchar**, **varchar(max)**, **nvarchar(max)**, **varbinary**, **varbinary(max)**, **text**, **ntext**, **image**, **sql_variant**, and **xml**. The command does not reclaim space after a fixed-length column is dropped.
If the dropped columns were stored in-row, DBCC CLEANTABLE reclaims space from the IN_ROW_DATA allocation unit of the table. If the columns were stored off-row, space is reclaimed from either the ROW_OVERFLOW_DATA or the LOB_DATA allocation unit depending on the data type of the dropped column. If reclaiming space from a ROW_OVERFLOW_DATA or LOB_DATA page results in an empty page, DBCC CLEANTABLE removes the page.
DBCC CLEANTABLE runs as one or more transactions. If a batch size is not specified, the command processes the whole table in one transaction and the table is exclusively locked during the operation. For some large tables, the length of the single transaction and the log space required may be too much. If a batch size is specified, the command runs in a series of transactions, each including the specified number of rows. DBCC CLEANTABLE cannot be run as a transaction inside another transaction.
This operation is fully logged.
DBCC CLEANTABLE is not supported for use on system tables, temporary tables, or the xVelocity memory optimized columnstore index portion of a table.
  
## Best Practices  
DBCC CLEANTABLE should not be executed as a routine maintenance task. Instead, use DBCC CLEANTABLE after you make significant changes to variable-length columns in a table or indexed view and you need to immediately reclaim the unused space. Alternatively, you can rebuild the indexes on the table or view; however, doing so is a more resource-intensive operation.
  
## Result Sets  
DBCC CLEANTABLE returns:
  
```sql
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
 Caller must own the table or indexed view, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.  
  
## Examples  
### A. Using DBCC CLEANTABLE to reclaim space  
The following example executes DBCC CLEANTABLE for the `Production.Document` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.
  
```sql  
DBCC CLEANTABLE (AdventureWorks2012,'Production.Document', 0)  
WITH NO_INFOMSGS;  
GO  
```  
  
### B. Using DBCC CLEANTABLE and verifying results  
The following example creates and populates a table with several variable-length columns. Two of the columns are then dropped and DBCC CLEANTABLE is run to reclaim the unused space. A query is run to verify the page counts and space used values before and after the DBCC CLEANTABLE command is executed.
  
```sql  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID ('dbo.CleanTableTest', 'U') IS NOT NULL  
    DROP TABLE dbo.CleanTableTest;  
GO  
CREATE TABLE dbo.CleanTableTest  
    (FileName nvarchar(4000),   
    DocumentSummary nvarchar(max),  
    Document varbinary(max)  
    );  
GO  
-- Populate the table with data from the Production.Document table.  
INSERT INTO dbo.CleanTableTest  
    SELECT REPLICATE(FileName, 1000),   
           DocumentSummary,   
           Document  
    FROM Production.Document;  
GO  
-- Verify the current page counts and average space used in the dbo.CleanTableTest table.  
DECLARE @db_id SMALLINT;  
DECLARE @object_id INT;  
SET @db_id = DB_ID(N'AdventureWorks2012');  
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');  
SELECT alloc_unit_type_desc,   
       page_count,   
       avg_page_space_used_in_percent,   
       record_count  
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');  
GO  
-- Drop two variable-length columns from the table.  
ALTER TABLE dbo.CleanTableTest  
DROP COLUMN FileName, Document;  
GO  
-- Verify the page counts and average space used in the dbo.CleanTableTest table  
-- Notice that the values have not changed.  
DECLARE @db_id SMALLINT;  
DECLARE @object_id INT;  
SET @db_id = DB_ID(N'AdventureWorks2012');  
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');  
SELECT alloc_unit_type_desc,   
       page_count,   
       avg_page_space_used_in_percent,   
       record_count  
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');  
GO  
-- Run DBCC CLEANTABLE.  
DBCC CLEANTABLE (AdventureWorks2012,'dbo.CleanTableTest');  
GO  
-- Verify the values in the dbo.CleanTableTest table after the DBCC CLEANTABLE command.  
DECLARE @db_id SMALLINT;  
DECLARE @object_id INT;  
SET @db_id = DB_ID(N'AdventureWorks2012');  
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');  
SELECT alloc_unit_type_desc,   
       page_count,   
       avg_page_space_used_in_percent,   
       record_count  
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
 [sys.allocation_units &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md)  
  
  
