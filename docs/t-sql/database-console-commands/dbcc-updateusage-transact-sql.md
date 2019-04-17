---
title: "DBCC UPDATEUSAGE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "UPDATEUSAGE"
  - "UPDATEUSAGE_TSQL"
  - "DBCC_UPDATEUSAGE_TSQL"
  - "DBCC UPDATEUSAGE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "inaccurate page or row counts [SQL Server]"
  - "space [SQL Server], usage reports"
  - "updating space usage information"
  - "updating row counts"
  - "disk space [SQL Server], inaccurate counts"
  - "counting pages"
  - "reporting count inaccuracies"
  - "updating page counts"
  - "synchronization [SQL Server], inaccurate counts"
  - "incorrect space usage reports [SQL Server]"
  - "DBCC UPDATEUSAGE statement"
  - "integrity [SQL Server], database objects"
  - "counting rows"
  - "row count accuracy [SQL Server]"
  - "page count accuracy [SQL Server]"
ms.assetid: b8752ecc-db45-4e23-aee7-13b8bc3cbae2
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC UPDATEUSAGE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Reports and corrects pages and row count inaccuracies in the catalog views. These inaccuracies may cause incorrect space usage reports returned by the sp_spaceused system stored procedure.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC UPDATEUSAGE   
(   { database_name | database_id | 0 }   
    [ , { table_name | table_id | view_name | view_id }   
    [ , { index_name | index_id } ] ]   
) [ WITH [ NO_INFOMSGS ] [ , ] [ COUNT_ROWS ] ]   
```  
  
## Arguments  
*database_name* | *database_id* | 0  
Is the name or ID of the database for which to report and correct space usage statistics. If 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
*table_name* | *table_id* | *view_name* | *view_id*  
Is the name or ID of the table or indexed view for which to report and correct space usage statistics. Table and view names must comply with the rules for identifiers.  
  
*index_id* | *index_name*  
Is the ID or name of the index to use. If not specified, the statement processes all indexes for the specified table or view.  
  
WITH  
Allows options to be specified.  
  
NO_INFOMSGS  
Suppresses all informational messages.  
  
COUNT_ROWS  
Specifies that the row count column is updated with the current count of the number of rows in the table or view.  
  
## Remarks  
DBCC UPDATEUSAGE corrects the rows, used pages, reserved pages, leaf pages and data page counts for each partition in a table or index. If there are no inaccuracies in the system tables, DBCC UPDATEUSAGE returns no data. If inaccuracies are found and corrected and WITH NO_INFOMSGS is not used, DBCC UPDATEUSAGE returns the rows and columns being updated in the system tables.
  
DBCC CHECKDB has been enhanced to detect when page or row counts become negative. When detected, the DBCC CHECKDB output contains a warning and a recommendation to run DBCC UPDATEUSAGE to address the issue.
  
## Best Practices  
We recommend the following:
-   Do not run DBCC UPDATEUSAGE routinely. Because DBCC UPDATEUSAGE can take some time to run on large tables or databases, it should not be used only unless you suspect incorrect values are being returned by sp_spaceused.
-   Consider running DBCC UPDATEUSAGE routinely (for example, weekly) only if the database undergoes frequent Data Definition Language (DDL) modifications, such as CREATE, ALTER, or DROP statements.  
  
## Result Sets  
DBCC UPDATEUSAGE returns (values may vary):
  
`DBCC execution completed. If DBCC printed error messages, contact your system administrator.`
  
## Permissions  
Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.
  
## Examples  
  
### A. Updating page or row counts or both for all objects in the current database  
The following example specifies `0` for the database name and `DBCC UPDATEUSAGE` reports updated page or row count information for the current database.
  
```sql
DBCC UPDATEUSAGE (0);  
GO  
```  
  
### B. Updating page or row counts or both for AdventureWorks, and suppressing informational messages  
The following example specifies [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the database name and suppresses all informational messages.
  
```sql
DBCC UPDATEUSAGE (AdventureWorks2012) WITH NO_INFOMSGS;   
GO  
```  
  
### C. Updating page or row counts or both for the Employee table  
The following example reports updated page or row count information for the `Employee` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql
DBCC UPDATEUSAGE (AdventureWorks2012,'HumanResources.Employee');  
GO  
```  
  
### D. Updating page or row counts or both for a specific index in a table  
 The following example specifies `IX_Employee_ManagerID` as the index name.  
  
```sql
DBCC UPDATEUSAGE (AdventureWorks2012, 'HumanResources.Employee', IX_Employee_OrganizationLevel_OrganizationNode);  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)  
[UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md)
  
  
