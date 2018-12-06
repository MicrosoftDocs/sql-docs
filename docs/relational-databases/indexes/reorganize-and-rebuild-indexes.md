---
title: "Reorganize and Rebuild Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.index.rebuild.f1"
  - "sql13.swb.indexproperties.fragmentation.f1"
  - "sql13.swb.index.reorg.f1"
helpviewer_keywords: 
  - "large object defragmenting"
  - "indexes [SQL Server], reorganizing"
  - "index reorganization [SQL Server]"
  - "reorganizing indexes"
  - "defragmenting large object data types"
  - "index fragmentation [SQL Server]"
  - "index rebuilding [SQL Server]"
  - "rebuilding indexes"
  - "indexes [SQL Server], rebuilding"
  - "defragmenting indexes"
  - "nonclustered indexes [SQL Server], defragmenting"
  - "fragmentation [SQL Server]"
  - "index defragmenting [SQL Server]"
  - "LOB data [SQL Server], defragmenting"
  - "clustered indexes, defragmenting"
ms.assetid: a28c684a-c4e9-4b24-a7ae-e248808b31e9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Reorganize and Rebuild Indexes
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

This topic describes how to reorganize or rebuild a fragmented index in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] automatically modifies indexes whenever insert, update, or delete operations are made to the underlying data. Over time these modifications can cause the information in the index to become scattered in the database (fragmented). Fragmentation exists when indexes have pages in which the logical ordering, based on the key value, does not match the physical ordering inside the data file. Heavily fragmented indexes can degrade query performance and cause your application to respond slowly, especially scan operations.  
  
You can remedy index fragmentation by reorganizing or rebuilding an index. For partitioned indexes built on a partition scheme, you can use either of these methods on a complete index or a single partition of an index.    
Rebuilding an index drops and re-creates the index. This removes fragmentation, reclaims disk space by compacting the pages based on the specified or existing fill factor setting, and reorders the index rows in contiguous pages. When `ALL` is specified, all indexes on the table are dropped and rebuilt in a single transaction.   
Reorganizing an index uses minimal system resources. It defragments the leaf level of clustered and nonclustered indexes on tables and views by physically reordering the leaf-level pages to match the logical, left to right, order of the leaf nodes. Reorganizing also compacts the index pages. Compaction is based on the existing fill factor value.  
   
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Fragmentation"></a> Detecting Fragmentation  
The first step in deciding which defragmentation method to use is to analyze the index to determine the degree of fragmentation. By using the system function [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md), you can detect fragmentation in a specific index, all indexes on a table or indexed view, all indexes in a database, or all indexes in all databases. For partitioned indexes, **sys.dm_db_index_physical_stats** also provides fragmentation information for each partition.  
  
The result set returned by the **sys.dm_db_index_physical_stats** function includes the following columns.  
  
|Column|Description|  
|------------|-----------------|  
|**avg_fragmentation_in_percent**|The percent of logical fragmentation (out-of-order pages in the index).|  
|**fragment_count**|The number of fragments (physically consecutive leaf pages) in the index.|  
|**avg_fragment_size_in_pages**|Average number of pages in one fragment in an index.|  
  
After the degree of fragmentation is known, use the following table to determine the best method to correct the fragmentation.  
  
|**avg_fragmentation_in_percent** value|Corrective statement|  
|-----------------------------------------------|--------------------------|  
|> 5% and < = 30%|ALTER INDEX REORGANIZE|  
|> 30%|ALTER INDEX REBUILD WITH (ONLINE = ON) <sup>1</sup>|  
  
<sup>1</sup> Rebuilding an index can be executed online or offline. Reorganizing an index is always executed online. To achieve availability similar to the reorganize option, you should rebuild indexes online.  
  
These values provide a rough guideline for determining the point at which you should switch between `ALTER INDEX REORGANIZE` and `ALTER INDEX REBUILD`. However, the actual values may vary from case to case. It is important that you experiment to determine the best threshold for your environment.    
Very low levels of fragmentation (less than 5 percent) should typically not be addressed by either of these commands, because the benefit from removing such a small amount of fragmentation is almost always vastly outweighed by the cost of reorganizing or rebuilding the index. For more information about `ALTER INDEX REORGANIZE` and `ALTER INDEX REBUILD`, refer to [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).   
  
> [!NOTE]
> Rebuilding or reorganizing small indexes often does not reduce fragmentation. The pages of small indexes are sometimes stored on mixed extents. Mixed extents are shared by up to eight objects, so the fragmentation in a small index might not be reduced after reorganizing or rebuilding it.  
  
### <a name="Restrictions"></a> Limitations and Restrictions  
  
Indexes with more than 128 extents are rebuilt in two separate phases: logical and physical. In the logical phase, the existing allocation units used by the index are marked for deallocation, the data rows are copied and sorted, then moved to new allocation units created to store the rebuilt index. In the physical phase, the allocation units previously marked for deallocation are physically dropped in short transactions that happen in the background, and do not require many locks. For more information about extents, refer to the [Pages and Extents Architecture Guide](../../relational-databases/pages-and-extents-architecture-guide.md). 
  
The `ALTER INDEX REORGANIZE` statement requires the data file containing the index to have space available, because the operation can only allocate temporary work pages on the same file, not another file within the filegroup. So although the filegroup might have free pages available, the user can still encounter error 1105: `Could not allocate space for object '###' in database '###' because the '###' filegroup is full. Create disk space by deleting unneeded files, dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.`
  
Creating and rebuilding non-aligned indexes on a table with more than 1,000 partitions is possible, but is not recommended. Doing so may cause degraded performance or excessive memory consumption during these operations.

An index cannot be reorganized or rebuilt if the filegroup in which it is located is offline or set to read-only. When the keyword `ALL` is specified and one or more indexes are in an offline or read-only filegroup, the statement fails.
  
> [!IMPORTANT]
> When an index is created or rebuilt in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], statistics are created or updated by scanning all the rows in the table.
> 
> However, starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], statistics are not created or updated by scanning all the rows in the table when a partitioned index is created or rebuilt. Instead, the query optimizer uses the default sampling algorithm to generate these statistics. To obtain statistics on partitioned indexes by scanning all the rows in the table, use `CREATE STATISTICS` or `UPDATE STATISTICS` with the `FULLSCAN` clause.  
  
### <a name="Security"></a> Security  
  
#### <a name="Permissions"></a> Permissions  
Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
## <a name="SSMSProcedureFrag"></a> Check index fragmentation using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
#### To check the fragmentation of an index  
  
1.  In Object Explorer, Expand the database that contains the table on which you want to check an index's fragmentation.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table on which you want to check an index's fragmentation.  
  
4.  Expand the **Indexes** folder.  
  
5.  Right-click the index of which you want to check the fragmentation and select **Properties**.  
  
6.  Under **Select a page**, select **Fragmentation**.  
  
     The following information is available on the **Fragmentation** page:  
  
     **Page fullness**  
     Indicates average fullness of the index pages, as a percentage. 100% means the index pages are completely full. 50% means that, on average, each index page is half full.  
  
     **Total fragmentation**  
     The logical fragmentation percentage. This indicates the number of pages in an index that are not stored in order.  
  
     **Average row size**  
     The average size of a leaf level row.  
  
     **Depth**  
     The number of levels in the index, including the leaf level.  
  
     **Forwarded records**  
     The number of records in a heap that have forward pointers to another data location. (This state occurs during an update, when there is not enough room to store the new row in the original location.)  
  
     **Ghost rows**  
     The number of rows that are marked as deleted but not yet removed. These rows will be removed by a clean-up thread, when the server is not busy. This value does not include rows that are being retained due to an outstanding snapshot isolation transaction.  
  
     **Index type**  
     The type of index. Possible values are **Clustered index**, **Nonclustered index**, and **Primary XML**. Tables can also be stored as a heap (without indexes), but then this Index Properties page cannot be opened.  
  
     **Leaf-level rows**  
     The number of leaf level rows.  
  
     **Maximum row size**  
     The maximum leaf-level row size.  
  
     **Minimum row size**  
     The minimum leaf-level row size.  
  
     **Pages**  
     The total number of data pages.  
  
     **Partition ID**  
     The partition ID of the b-tree containing the index.  
  
     **Version ghost rows**  
     The number of ghost records that are being retained due to an outstanding snapshot isolation transaction.  
  
##  <a name="TsqlProcedureFrag"></a> Check index fragmentation using [!INCLUDE[tsql](../../includes/tsql-md.md)]  
  
#### To check the fragmentation of an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    -- Find the average fragmentation percentage of all indexes  
    -- in the HumanResources.Employee table.   
    SELECT a.index_id, name, avg_fragmentation_in_percent  
    FROM sys.dm_db_index_physical_stats (DB_ID(N'AdventureWorks2012'), 
          OBJECT_ID(N'HumanResources.Employee'), NULL, NULL, NULL) AS a  
        JOIN sys.indexes AS b 
          ON a.object_id = b.object_id AND a.index_id = b.index_id;   
    GO  
    ```  
  
     The statement above might return a result set similar to the following.  
  
    ```  
    index_id    name                                                  avg_fragmentation_in_percent  
    ----------- ----------------------------------------------------- ----------------------------  
    1           PK_Employee_BusinessEntityID                          0  
    2           IX_Employee_OrganizationalNode                        0  
    3           IX_Employee_OrganizationalLevel_OrganizationalNode    0  
    5           AK_Employee_LoginID                                   66.6666666666667  
    6           AK_Employee_NationalIDNumber                          50  
    7           AK_Employee_rowguid                                   0  
  
    (6 row(s) affected)  
    ```  
  
For more information, see [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md).  
  
##  <a name="SSMSProcedureReorg"></a> Remove fragmentation using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
#### To reorganize or rebuild an index  
  
1.  In Object Explorer, Expand the database that contains the table on which you want to reorganize an index.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table on which you want to reorganize an index.  
  
4.  Expand the **Indexes** folder.  
  
5.  Right-click the index you want to reorganize and select **Reorganize**.  
  
6.  In the **Reorganize Indexes** dialog box, verify that the correct index is in the **Indexes to be reorganized** grid and click **OK**.  
  
7.  Select the **Compact large object column data** check box to specify that all pages that contain large object (LOB) data are also compacted.  
  
8.  Click **OK.**  
  
#### To reorganize all indexes in a table  
  
1.  In Object Explorer, Expand the database that contains the table on which you want to reorganize the indexes.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table on which you want to reorganize the indexes.  
  
4.  Right-click the **Indexes** folder and select **Reorganize All**.  
  
5.  In the **Reorganize Indexes** dialog box, verify that the correct indexes are in the **Indexes to be reorganized**. To remove an index from the **Indexes to be reorganized** grid, select the index and then press the Delete key.  
  
6.  Select the **Compact large object column data** check box to specify that all pages that contain large object (LOB) data are also compacted.  
  
7.  Click **OK.**  
  
#### To rebuild an index  
  
1.  In Object Explorer, Expand the database that contains the table on which you want to reorganize an index.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table on which you want to reorganize an index.  
  
4.  Expand the **Indexes** folder.  
  
5.  Right-click the index you want to reorganize and select **Rebuild**.  
  
6.  In the **Rebuild Indexes** dialog box, verify that the correct index is in the **Indexes to be rebuilt** grid and click **OK**.  
  
7.  Select the **Compact large object column data** check box to specify that all pages that contain large object (LOB) data are also compacted.  
  
8.  Click **OK.**  
  
## <a name="TsqlProcedureReorg"></a> Remove fragmentation using [!INCLUDE[tsql](../../includes/tsql-md.md)] 
  
#### To reorganize a fragmented index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;   
    GO  
    -- Reorganize the IX_Employee_OrganizationalLevel_OrganizationalNode 
    -- index on the HumanResources.Employee table.   
  
    ALTER INDEX IX_Employee_OrganizationalLevel_OrganizationalNode 
      ON HumanResources.Employee  
    REORGANIZE ;   
    GO  
    ```  
  
#### To reorganize all indexes in a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;   
    GO  
    -- Reorganize all indexes on the HumanResources.Employee table.  
    ALTER INDEX ALL ON HumanResources.Employee  
    REORGANIZE ;   
    GO  
    ```  
  
#### To rebuild a fragmented index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example rebuilds a single index on the `Employee` table.  
  
     [!code-sql[IndexDDL#AlterIndex1](../../relational-databases/indexes/codesnippet/tsql/reorganize-and-rebuild-i_1.sql)]  
  
#### To rebuild all indexes in a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query The example specifies the keyword `ALL`. This rebuilds all indexes associated with the table. Three options are specified.  
  
     [!code-sql[IndexDDL#AlterIndex2](../../relational-databases/indexes/codesnippet/tsql/reorganize-and-rebuild-i_2.sql)]  
  
For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
 
### Automatic index and statistics management

Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.
  
## See Also  
  [SQL Server Index Design Guide](../../relational-databases/sql-server-index-design-guide.md)   
  [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
  [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag)   
  [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)   
  [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md)   
  
