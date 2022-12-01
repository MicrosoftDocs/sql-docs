---
title: DBCC INDEXDEFRAG (Transact-SQL)
description: "DBCC INDEXDEFRAG (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "INDEXDEFRAG"
  - "DBCC INDEXDEFRAG"
  - "DBCC_INDEXDEFRAG_TSQL"
  - "INDEXDEFRAG_TSQL"
helpviewer_keywords:
  - "defragmenting indexes"
  - "DBCC INDEXDEFRAG statement"
  - "leaf level defragmenting"
  - "fragmentation [SQL Server]"
  - "index defragmenting [SQL Server]"
dev_langs:
  - "TSQL"
---

# DBCC INDEXDEFRAG (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Defragments indexes of the specified table or view.
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) instead.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level))
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC INDEXDEFRAG  
(  
    { database_name | database_id | 0 }   
    , { table_name | table_id | view_name | view_id }   
    [ , { index_name | index_id } [ , { partition_number | 0 } ] ]  
)  
    [ WITH NO_INFOMSGS ]   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name* \| *database_id* \| 0  
 Is the database containing the index to defragment. If 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 *table_name* \| *table_id* \| *view_name* \| *view_id*  
 Is the table or view containing the index to defragment. Table and view names must comply with the rules for identifiers.  
  
 *index_name* \| *index_id*  
 Is the name or ID of the index to defragment. If not specified, the statement defragments all indexes of the specified table or view. Index names must comply with the rules for identifiers.  
  
 *partition_number* \| 0  
 Is the partition number of the index to defragment. If not specified or if 0 is specified, the statement defragments all partitions in the specified index.  
  
 WITH NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Remarks  
DBCC INDEXDEFRAG defragments the leaf level of an index so that the physical order of the pages matches the left-to-right logical order of the leaf nodes, therefore improving index-scanning performance.
  
> [!NOTE]  
>  When DBCC INDEXDEFRAG is run, index defragmentation occurs serially. This means that the operation on a single index is performed using a single thread. No parallelism occurs. Also, operations on multiple indexes from the same DBCC INDEXDEFRAG statement are performed on one index at a time.  
  
DBCC INDEXDEFRAG also compacts the pages of an index, taking into consideration the fill factor specified when the index was created. Any empty pages created because of this compaction are removed. For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).
  
If an index spans more than one file, DBCC INDEXDEFRAG defragments one file at a time. Pages do not migrate between files.
  
DBCC INDEXDEFRAG reports the estimated percentage completed every five minutes. DBCC INDEXDEFRAG can be stopped at any point in the process, and any completed work is retained.
  
Unlike DBCC DBREINDEX, or the index building operation generally, DBCC INDEXDEFRAG is an online operation. It does not hold locks long term. Therefore, DBCC INDEXDEFRAG does not block running queries or updates. Because the time to defragment is related to the level of fragmentation, a relatively unfragmented index can be defragmented faster than a new index can be built. A very fragmented index might take considerably longer to defragment than to rebuild.
  
The defragmentation is always fully logged, regardless of the database recovery model setting. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md). The defragmentation of a very fragmented index can generate more log than a fully logged index creation. However, the defragmentation is performed as a series of short transactions, so a large log is unnecessary if log backups are taken frequently or if the recovery model setting is SIMPLE.
  
## Restrictions  
DBCC INDEXDEFRAG shuffles index leaf pages in place. Therefore, if an index is interleaved with other indexes on disk, running DBCC INDEXDEFRAG against that index does not make all leaf pages in the index contiguous. To improve the clustering of pages, rebuild the index.
DBCC INDEXDEFRAG cannot be used to defragment the following indexes:
-   A disabled index.  
-   An index with page locking set to OFF.  
-   A spatial index.  
  
DBCC INDEXDEFRAG is not supported for use on system tables.
  
## Result Sets  
DBCC INDEXDEFRAG returns the following result set (values may vary) if an index is specified in the statement (unless WITH NO_INFOMSGS is specified):
  
```
Pages Scanned Pages Moved Pages Removed  
------------- ----------- -------------  
359           346         8  
  
(1 row(s) affected)  
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Caller must own the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.
  
## Examples  
### A. Using DBCC INDEXDEFRAG to defragment an index  
The following example defragments all partitions of the `PK_Product_ProductID` index in the `Production.Product` table in the `AdventureWorks` database.
  
```sql  
DBCC INDEXDEFRAG (AdventureWorks2012, 'Production.Product', PK_Product_ProductID);  
GO  
```  
  
### B. Using DBCC SHOWCONTIG and DBCC INDEXDEFRAG to defragment the indexes in a database  
 The following example shows a simple way to defragment all indexes in a database that are fragmented above a declared threshold.  
  
```sql  
/*Perform a 'USE <database name>' to select the database in which to run the script.*/  
-- Declare variables  
SET NOCOUNT ON;  
DECLARE @tablename VARCHAR(255);  
DECLARE @execstr   VARCHAR(400);  
DECLARE @objectid  INT;  
DECLARE @indexid   INT;  
DECLARE @frag      DECIMAL;  
DECLARE @maxfrag   DECIMAL;  
  
-- Decide on the maximum fragmentation to allow for.  
SELECT @maxfrag = 30.0;  
  
-- Declare a cursor.  
DECLARE tables CURSOR FOR  
   SELECT TABLE_SCHEMA + '.' + TABLE_NAME  
   FROM INFORMATION_SCHEMA.TABLES  
   WHERE TABLE_TYPE = 'BASE TABLE';  
  
-- Create the table.  
CREATE TABLE #fraglist (  
   ObjectName CHAR(255),  
   ObjectId INT,  
   IndexName CHAR(255),  
   IndexId INT,  
   Lvl INT,  
   CountPages INT,  
   CountRows INT,  
   MinRecSize INT,  
   MaxRecSize INT,  
   AvgRecSize INT,  
   ForRecCount INT,  
   Extents INT,  
   ExtentSwitches INT,  
   AvgFreeBytes INT,  
   AvgPageDensity INT,  
   ScanDensity DECIMAL,  
   BestCount INT,  
   ActualCount INT,  
   LogicalFrag DECIMAL,  
   ExtentFrag DECIMAL);  
  
-- Open the cursor.  
OPEN tables;  
  
-- Loop through all the tables in the database.  
FETCH NEXT  
   FROM tables  
   INTO @tablename;  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
-- Do the showcontig of all indexes of the table  
   INSERT INTO #fraglist   
   EXEC ('DBCC SHOWCONTIG (''' + @tablename + ''')   
      WITH FAST, TABLERESULTS, ALL_INDEXES, NO_INFOMSGS');  
   FETCH NEXT  
      FROM tables  
      INTO @tablename;  
END;  
  
-- Close and deallocate the cursor.  
CLOSE tables;  
DEALLOCATE tables;  
  
-- Declare the cursor for the list of indexes to be defragged.  
DECLARE indexes CURSOR FOR  
   SELECT ObjectName, ObjectId, IndexId, LogicalFrag  
   FROM #fraglist  
   WHERE LogicalFrag >= @maxfrag  
      AND INDEXPROPERTY (ObjectId, IndexName, 'IndexDepth') > 0;  
  
-- Open the cursor.  
OPEN indexes;  
  
-- Loop through the indexes.  
FETCH NEXT  
   FROM indexes  
   INTO @tablename, @objectid, @indexid, @frag;  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
   PRINT 'Executing DBCC INDEXDEFRAG (0, ' + RTRIM(@tablename) + ',  
      ' + RTRIM(@indexid) + ') - fragmentation currently '  
       + RTRIM(CONVERT(varchar(15),@frag)) + '%';  
   SELECT @execstr = 'DBCC INDEXDEFRAG (0, ' + RTRIM(@objectid) + ',  
       ' + RTRIM(@indexid) + ')';  
   EXEC (@execstr);  
  
   FETCH NEXT  
      FROM indexes  
      INTO @tablename, @objectid, @indexid, @frag;  
END;  
  
-- Close and deallocate the cursor.  
CLOSE indexes;  
DEALLOCATE indexes;  
  
-- Delete the temporary table.  
DROP TABLE #fraglist;  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)
  
