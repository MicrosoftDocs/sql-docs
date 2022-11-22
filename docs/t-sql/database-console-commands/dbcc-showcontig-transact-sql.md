---
title: DBCC SHOWCONTIG (Transact-SQL)
description: "DBCC SHOWCONTIG (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/17/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC_SHOWCONTIG_TSQL"
  - "DBCC SHOWCONTIG"
  - "SHOWCONTIG"
  - "SHOWCONTIG_TSQL"
helpviewer_keywords:
  - "displaying defragmentation information"
  - "DBCC SHOWCONTIG statement"
  - "defragmenting indexes"
  - "leaf level defragmenting"
  - "fragmentation [SQL Server]"
  - "index defragmenting [SQL Server]"
dev_langs:
  - "TSQL"
---

# DBCC SHOWCONTIG (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Displays fragmentation information for the data and indexes of the specified table or view.
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) instead.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level))
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC SHOWCONTIG   
[ (   
    { table_name | table_id | view_name | view_id }   
    [ , index_name | index_id ]   
) ]   
    [ WITH   
        {   
         [ , [ ALL_INDEXES ] ]   
         [ , [ TABLERESULTS ] ]   
         [ , [ FAST ] ]  
         [ , [ ALL_LEVELS ] ]   
         [ NO_INFOMSGS ]  
         }  
    ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *table_name* \| *table_id* \| *view_name* \| *view_id*  
 Is the table or view to check for fragmentation information. If not specified, all tables and indexed views in the current database are checked. To obtain the table or view ID, use the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) function.  
  
 *index_name* \| *index_id*  
 Is the index to check for fragmentation information. If not specified, the statement processes the base index for the specified table or view. To obtain the index ID, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.  
  
 WITH  
 Specifies options for the type of information returned by the DBCC statement.  
  
 FAST  
 Specifies whether to perform a fast scan of the index and output minimal information. A fast scan does not read the leaf or data level pages of the index.  
  
 ALL_INDEXES  
 Displays results for all the indexes for the specified tables and views, even if a particular index is specified.  
  
 TABLERESULTS  
 Displays results as a rowset, with additional information.  
  
 ALL_LEVELS  
 Maintained for backward compatibility only. Even if ALL_LEVELS is specified, only the index leaf level or table data level is processed.  
  
 NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
The following table describes the information in the result set.
  
|Statistic|Description|  
|---|---|
|**Pages Scanned**|Number of pages in the table or index.|  
|**Extents Scanned**|Number of extents in the table or index.|  
|**Extent Switches**|Number of times the DBCC statement moved from one extent to another while the statement traversed the pages of the table or index.|  
|**Avg. Pages per Extent**|Number of pages per extent in the page chain.|  
|**Scan Density [Best Count: Actual Count]**|Is a percentage. It is the ratio **Best Count** to **Actual Count**. This value is 100 if everything is contiguous; if this value is less than 100, some fragmentation exists.<br /><br /> **Best Count** is the ideal number of extent changes if everything is contiguously linked. **Actual Count** is the actual number of extent changes.|  
|**Logical Scan Fragmentation**|Percentage of out-of-order pages returned from scanning the leaf pages of an index. This number is not relevant to heaps. An out-of-order page is a page for which the next physical page allocated to the index is not the page pointed to by the next-pag*e* pointer in the current leaf page.|  
|**Extent Scan Fragmentation**|Percentage of out-of-order extents in scanning the leaf pages of an index. This number is not relevant to heaps. An out-of-order extent is one for which the extent that contains the current page for an index is not physically the next extent after the extent that contains the previous page for an index.<br /><br /> Note: This number is meaningless when the index spans multiple files.|  
|**Avg. Bytes Free per Page**|Average number of free bytes on the pages scanned. The larger the number, the less full the pages are. Lower numbers are better if the index will not have many random inserts. This number is also affected by row size; a large row size can cause a larger number.|  
|**Avg. Page density (full)**|Average page density, as a percentage. This value takes into account row size. Therefore, the value is a more accurate indication of how full your pages are. The larger the percentage, the better.|  
  
When *table_id* and FAST are specified, DBCC SHOWCONTIG returns a result set with only the following columns.
-   **Pages Scanned**  
-   **Extent Switches**  
-   **Scan Density [Best Count:Actual Count]**  
-   **Extent Scan Fragmentation**  
-   **Logical Scan Fragmentation**  
  
When TABLERESULTS is specified, DBCC SHOWCONTIG returns the following columns and also the nine columns described in the previous table.
  
|Statistic|Description|  
|---|---|
|**Object Name**|Name of the table or view processed.|  
|**ObjectId**|ID of the object name.|  
|**IndexName**|Name of the index processed. Is NULL for a heap.|  
|**IndexId**|ID of the index. Is 0 for a heap.|  
|**Level**|Level of the index. Level 0 is the leaf, or data, level of the index.<br /><br /> Level is 0 for a heap.|  
|**Pages**|Number of pages that make up that level of the index or whole heap.|  
|**Rows**|Number of data or index records at that level of the index. For a heap, this value is the number of data records in the whole heap.<br /><br /> For a heap, the number of records returned from this function might not match the number of rows that are returned by running a SELECT COUNT(*) against the heap. This is because a row may contain multiple records. For example, under some update situations, a single heap row may have a forwarding record and a forwarded record as a result of the update operation. Also, most large LOB rows are split into multiple records in LOB_DATA storage.|  
|**MinimumRecordSize**|Minimum record size in that level of the index or whole heap.|  
|**MaximumRecordSize**|Maximum record size in that level of the index or whole heap.|  
|**AverageRecordSize**|Average record size in that level of the index or whole heap.|  
|**ForwardedRecords**|Number of forwarded records in that level of the index or whole heap.|  
|**Extents**|Number of extents in that level of the index or whole heap.|  
|**ExtentSwitches**|Number of times the DBCC statement moved from one extent to another while the statement traversed the pages of the table or index.|  
|**AverageFreeBytes**|Average number of free bytes on the pages scanned. The larger the number, the less full the pages are. Lower numbers are better if the index will not have many random inserts. This number is also affected by row size; a large row size can cause a larger number.|  
|**AveragePageDensity**|Average page density, as a percentage. This value takes into account row size. Therefore, the value is a more accurate indication of how full your pages are. The larger the percentage, the better.|  
|**ScanDensity**|Is a percentage. It is the ratio **BestCount** to **ActualCount**. This value is 100 if everything is contiguous; if this value is less than 100, some fragmentation exists.|  
|**BestCount**|Is the ideal number of extent changes if everything is contiguously linked.|  
|**ActualCount**|Is the actual number of extent changes.|  
|**LogicalFragmentation**|Percentage of out-of-order pages returned from scanning the leaf pages of an index. This number is not relevant to heaps. An out-of-order page is a page for which the next physical page allocated to the index is not the page pointed to by the next-pag*e* pointer in the current leaf page.|  
|**ExtentFragmentation**|Percentage of out-of-order extents in scanning the leaf pages of an index. This number is not relevant to heaps. An out-of-order extent is one for which the extent that contains the current page for an index is not physically the next extent after the extent that contains the previous page for an index.<br /><br /> Note: This number is meaningless when the index spans multiple files.|  
  
When WITH TABLERESULTS and FAST are specified, the result set is the same as when WITH TABLERESULTS is specified, except the following columns will have null values:

| Rows| Extents |
|---|---|
|**MinimumRecordSize**|**AverageFreeBytes**|  
|**MaximumRecordSize**|**AveragePageDensity**|  
|**AverageRecordSize**|**ExtentFragmentation**|  
|**ForwardedRecords**||  
  
## Remarks  
The DBCC SHOWCONTIG statement traverses the page chain at the leaf level of the specified index when *index_id* is specified. If only *table_id* is specified or if *index_id* is 0, the data pages of the specified table are scanned. The operation only requires an intent-shared (IS) table lock. This way all updates and inserts can be performed, except those that require an exclusive (X) table lock. This allows for a tradeoff between speed of execution and no reduction in concurrency against the number of statistics returned. However, if the command is being used only to gauge fragmentation, we recommend that you use the WITH FAST option for optimal performance. A fast scan does not read the leaf or data level pages of the index. The WITH FAST option does not apply to a heap.
  
## Restrictions  
DBCC SHOWCONTIG does not display data with **ntext**, **text**, and **image** data types. This is because text indexes that store text and image data no longer exist.
  
Also, DBCC SHOWCONTIG does not support some new features. For example:
-   If the specified table or index is partitioned, DBCC SHOWCONTIG only displays the first partition of the specified table or index.  
-   DBCC SHOWCONTIG does not display row-overflow storage information and other new off-row data types, such as **nvarchar(max)**, **varchar(max)**, **varbinary(max)**, and **xml**.  
-   Spatial indexes are not supported by DBCC SHOWCONTIG.  
  
All new features are fully supported by the [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) dynamic management view.
  
## Table Fragmentation  
DBCC SHOWCONTIG determines whether the table is heavily fragmented. Table fragmentation occurs through the process of data modifications (INSERT, UPDATE, and DELETE statements) made against the table. Because these modifications are not ordinarily distributed equally among the rows of the table, the fullness of each page can vary over time. For queries that scan part or all of a table, such table fragmentation can cause additional page reads. This hinders parallel scanning of data.
  
When an index is heavily fragmented, the following choices are available for reducing fragmentation:
-   Drop and re-create a clustered index.  
     Re-creating a clustered index reorganizes the data, and causes full data pages. The level of fullness can be configured by using the FILLFACTOR option in CREATE INDEX. The drawbacks of this method are that the index is offline during the drop or re-create cycle, and that the operation is atomic. If the index creation is interrupted, the index is not re-created.  
-   Reorder the leaf-level pages of the index in a logical order.  
     Use ALTER INDEX...REORGANIZE to reorder the leaf-level pages of the index in a logical order. Because this operation is an online operation, the index is available when the statement is running. The operation is also interruptible without loss of completed work. The drawback of this method is that the method does not do as good a job of reorganizing the data as a clustered index drop or re-create operation.  
-   Rebuild the index.  
     Use ALTER INDEX with REBUILD to rebuild the index. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
The **Avg. Bytes free per page** and **Avg. Page density (full)** statistic in the result set indicate the fullness of index pages. The **Avg. Bytes free per page** number should be low and the **Avg. Page density (full)** number should be high for an index that will not have many random inserts. Dropping and re-creating an index with the FILLFACTOR option specified can improve the statistics. Also, ALTER INDEX with REORGANIZE will compact an index, taking into account its FILLFACTOR, and will improve the statistics.
  
> [!NOTE]  
>  An index that has many random inserts and very full pages will have an increased number of page splits. This causes more fragmentation.  
  
The fragmentation level of an index can be determined in the following ways:
-   By comparing the values of **Extent Switches** and **Extents Scanned**.  
     The value of **Extent Switches** should be as close as possible to that of **Extents Scanned**. This ratio is calculated as the **Scan Density** value. This value should be as high as possible, and can be improved by reducing index fragmentation.  
  
    > [!NOTE]  
    >  This method does not work if the index spans multiple files.  
  
-   By understanding **Logical Scan Fragmentation** and **Extent Scan Fragmentation** values.  
     **Logical Scan Fragmentation** and, to a lesser extent, **Extent Scan Fragmentation** values are the best indicators of the fragmentation level of a table. Both these values should be as close to zero as possible, although a value from 0 through 10 percent may be acceptable.  
  
    > [!NOTE]  
    >  The **Extent Scan Fragmentation** value will be high if the index spans multiple files. To reduce these values, you must reduce the index fragmentation.  
  
## Permissions  
User must own the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.
  
## Examples  
### A. Displaying fragmentation information for a table  
The following example displays fragmentation information for the `Employee` table.
  
```sql  
USE AdventureWorks2012;  
GO  
DBCC SHOWCONTIG ('HumanResources.Employee');  
GO  
```  
  
### B. Using OBJECT_ID to obtain the table ID and sys.indexes to obtain the index ID  
The following example uses `OBJECT_ID` and the `sys.indexes` catalog view to obtain the table ID and index ID for the `AK_Product_Name` index of the `Production.Product` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @id INT, @indid INT  
SET @id = OBJECT_ID('Production.Product')  
SELECT @indid = index_id   
FROM sys.indexes  
WHERE object_id = @id   
   AND name = 'AK_Product_Name'  
DBCC SHOWCONTIG (@id, @indid);  
GO  
```  
  
### C. Displaying an abbreviated result set for a table  
The following example returns an abbreviated result set for the `Product` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql  
USE AdventureWorks2012;  
GO  
DBCC SHOWCONTIG ('Production.Product', 1) WITH FAST;  
GO  
```  
  
### D. Displaying the full result set for every index on every table in a database  
The following example returns a full table result set for every index on every table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql  
USE AdventureWorks2012;  
GO  
DBCC SHOWCONTIG WITH TABLERESULTS, ALL_INDEXES;  
GO  
```  
  
### E. Using DBCC SHOWCONTIG and DBCC INDEXDEFRAG to defragment the indexes in a database  
The following example shows a simple way to defragment all indexes in a database that is fragmented above a declared threshold.
  
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
[ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)  
[CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)  
[sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
[OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md)  
[sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
  
