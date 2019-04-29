---
title: "DBCC CHECKCONSTRAINTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC CHECKCONSTRAINTS"
  - "DBCC_CHECKCONSTRAINTS_TSQL"
  - "CHECKCONSTRAINTS"
  - "CHECKCONSTRAINTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DBCC CHECKCONSTRAINTS statement"
  - "consistency [SQL Server], constraints"
  - "checking constraint consistency"
  - "constraints [SQL Server], consistency checks"
  - "integrity [SQL Server], constraints"
ms.assetid: da6c9cee-6687-46e8-b504-738551f9068b
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC CHECKCONSTRAINTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Checks the integrity of a specified constraint or all constraints on a specified table in the current database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC CHECKCONSTRAINTS  
[   
    (   
    table_name | table_id | constraint_name | constraint_id   
    )  
]  
    [ WITH   
    [ { ALL_CONSTRAINTS | ALL_ERRORMSGS } ]  
    [ , ] [ NO_INFOMSGS ]   
    ]  
```  
  
## Arguments  
 *table_name* | *table_id* | *constraint_name* | *constraint_id*  
 Is the table or constraint to be checked. When *table_name* or *table_id* is specified, all enabled constraints on that table are checked. When *constraint_name* or *constraint_id* is specified, only that constraint is checked. If neither a table identifier nor a constraint identifier is specified, all enabled constraints on all tables in the current database are checked.  
 A constraint name uniquely identifies the table to which it belongs. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 WITH  
 Enables options to be specified.  
  
 ALL_CONSTRAINTS  
 Checks all enabled and disabled constraints on the table if the table name is specified or if all tables are checked; otherwise, checks only the enabled constraint. ALL_CONSTRAINTS has no effect when a constraint name is specified.  
  
 ALL_ERRORMSGS  
 Returns all rows that violate constraints in the table that is checked. The default is the first 200 rows.  
  
 NO_INFOMSGS  
 Suppresses all informational messages.  
  
## Remarks  
DBCC CHECKCONSTRAINTS constructs and executes a query for all FOREIGN KEY constraints and CHECK constraints on a table.
  
For example, a foreign key query is of the following form:
  
```sql
SELECT <columns>  
FROM <table_being_checked> LEFT JOIN <referenced_table>  
    ON <table_being_checked.fkey1> = <referenced_table.pkey1>   
    AND <table_being_checked.fkey2> = <referenced_table.pkey2>  
WHERE <table_being_checked.fkey1> IS NOT NULL   
    AND <referenced_table.pkey1> IS NULL  
    AND <table_being_checked.fkey2> IS NOT NULL  
    AND <referenced_table.pkey2> IS NULL  
```  
  
The query data is stored in a temp table. After all requested tables or constraints have been checked, the result set is returned.
DBCC CHECKCONSTRAINTS checks the integrity of FOREIGN KEY and CHECK constraints but does not check the integrity of the on-disk data structures of a table. These data structure checks can be performed by using [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).
  
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
If *table_name* or *table_id* is specified and it is enabled for system versioning, DBCC CHECKCONSTRAINTS also performs temporal data consistency checks on the specified table. When *NO_INFOMSGS* is not specified, this command will return each consistency violation in the output on a separate line. The format of the output will be ([pkcol1], [pkcol2]..) = (\<pkcol1_value>, \<pkcol2_value>...) AND \<what is wrong with temporal table record>.
  
|Check|Additional info in output if check failed|  
|-----------|-----------------------------------------------|  
|PeriodEndColumn ≥ PeriodStartColumn (current)|[sys_end] = '{0}' AND MAX(DATETIME2) = '9999-12-31 23:59:59.99999'|  
|PeriodEndColumn ≥ PeriodStartColumn (current, history)|[sys_start] = '{0}' AND [sys_end] = '{1}'|  
|PeriodStartColumn < current_utc_time (current)|[sys_start] = '{0}' AND SYSUTCTIME|  
|PeriodEndColumn < current_utc_time (history)|[sys_end] = '{0}' AND SYSUTCTIME|  
|Overlaps|(sys_start1, sys_end1) , (sys_start2, sys_end2) for two overlapping records.<br /><br /> If there are more than 2 overlapping records, output will have multiple rows each showing a pair of overlaps.|  
  
There is no way to specify constraint_name or constraint_id in order to run only temporal consistency checks.
  
## Result Sets  
DBCC CHECKCONSTRAINTS return a rowset with the following columns.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Table Name|**varchar**|Name of the table.|  
|Constraint Name|**varchar**|Name of the constraint that is violated.|  
|Where|**varchar**|Column value assignments that identify the row or rows violating the constraint.<br /><br /> The value in this column can be used in a WHERE clause of a SELECT statement querying for rows that violate the constraint.|  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.
  
## Examples  
  
### A. Checking a table  
The following example checks the constraint integrity of the table `Table1` in the `AdventureWorks` database.
  
```sql  
USE AdventureWorks2012;  
GO  
CREATE TABLE Table1 (Col1 int, Col2 char (30));  
GO  
INSERT INTO Table1 VALUES (100, 'Hello');  
GO  
ALTER TABLE Table1 WITH NOCHECK ADD CONSTRAINT chkTab1 CHECK (Col1 > 100);  
GO  
DBCC CHECKCONSTRAINTS(Table1);  
GO  
```  
  
### B. Checking a specific constraint  
The following example checks the integrity of the `CK_ProductCostHistory_EndDate` constraint.
  
```sql  
USE AdventureWorks2012;  
GO  
DBCC CHECKCONSTRAINTS ('Production.CK_ProductCostHistory_EndDate');  
GO  
```  
  
### C. Checking all enabled and disabled constraints on all tables  
 The following example checks the integrity of all enabled and disabled constraints on all tables in the current database.  
  
```sql  
DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS;  
GO  
```  
  
## See Also  
[DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[DBCC CHECKTABLE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  
