---
title: "DBCC DBREINDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC DBREINDEX"
  - "DBREINDEX_TSQL"
  - "DBREINDEX"
  - "DBCC_DBREINDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "index rebuilding [SQL Server]"
  - "rebuilding indexes"
  - "dynamic index rebuilding [SQL Server]"
  - "DBCC DBREINDEX statement"
ms.assetid: 6e929d09-ccb5-4855-a6af-b616022bc8f6
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC DBREINDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
Rebuilds one or more indexes for a table in the specified database.
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] Use [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) instead.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658))
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC DBREINDEX   
(   
    table_name   
    [ , index_name [ , fillfactor ] ]  
)  
    [ WITH NO_INFOMSGS ]   
```  
  
## Arguments  
 *table_name*  
 Is the name of the table containing the specified index or indexes to rebuild. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md)*.*  
  
 *index_name*  
 Is the name of the index to rebuild. Index names must comply with the rules for identifiers. If *index_name* is specified, *table_name* must be specified. If *index_name* is not specified or is " ", all indexes for the table are rebuilt.  
  
 *fillfactor*  
 Is the percentage of space on each index page for storing data when the index is created or rebuilt. *fillfactor* replaces the fill factor when the index was created, becoming the new default for the index and for any other nonclustered indexes rebuilt because a clustered index is rebuilt.  
 When *fillfactor* is 0, DBCC DBREINDEX uses the fill factor value last specified for the index. This value is stored in the **sys.indexes** catalog view.   
 If *fillfactor* is specified, *table_name* and *index_name* must be specified. If *fillfactor* is not specified, the default fill factor, 100, is used. For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Remarks  
DBCC DBREINDEX rebuilds an index for a table or all indexes defined for a table. By allowing an index to be rebuilt dynamically, indexes enforcing either PRIMARY KEY or UNIQUE constraints can be rebuilt without having to drop and re-create those constraints. This means that an index can be rebuilt without knowing the structure of a table or its constraints. This might occur after a bulk copy of data into the table.

DBCC DBREINDEX can rebuild all the indexes for a table in one statement. This is easier than coding multiple DROP INDEX and CREATE INDEX statements. Because the work is performed by one statement, DBCC DBREINDEX is automatically atomic, whereas individual DROP INDEX and CREATE INDEX statements must be included in a transaction to be atomic. Also, DBCC DBREINDEX offers more optimizations than individual DROP INDEX and CREATE INDEX statements.

Unlike DBCC INDEXDEFRAG, or ALTER INDEX with the REORGANIZE option, DBCC DBREINDEX is an offline operation. If a nonclustered index is being rebuilt, a shared lock is held on the table in question for the duration of the operation. This prevents modifications to the table. If the clustered index is being rebuilt, an exclusive table lock is held. This prevents any table access, therefore effectively making the table offline. To perform an index rebuild online, or to control the degree of parallelism during the index rebuild operation, use the ALTER INDEX REBUILD statement with the ONLINE option.

For more information about selecting a method to rebuild or reorganize an index, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md) .
  
## Restrictions  
DBCC DBREINDEX is not supported for use on the following objects:
-   System tables  
-   Spatial indexes  
-   xVelocity memory optimized columnstore indexes  
  
## Result Sets  
Unless NO_INFOMSGS is specified (the table name must be specified), DBCC DBREINDEX always returns:
  
```sql
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Caller must own the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.
  
## Examples  
### A. Rebuilding an index  
The following example rebuilds the `Employee_EmployeeID` clustered index with a fill factor of `80` on the `Employee` table in the `AdventureWorks` database.
  
```sql  
USE AdventureWorks2012;   
GO  
DBCC DBREINDEX ('HumanResources.Employee', PK_Employee_BusinessEntityID,80);  
GO  
```  
  
### B. Rebuilding all indexes  
The following example rebuilds all indexes on the `Employee` table in `AdventureWorks` by using a fill factor value of `70`.
  
```sql
USE AdventureWorks2012;   
GO  
DBCC DBREINDEX ('HumanResources.Employee', ' ', 70);  
GO  
```  
  
## See Also  
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
[sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
[ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)  
  
  

