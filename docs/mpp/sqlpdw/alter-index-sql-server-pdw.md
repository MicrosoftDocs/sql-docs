---
title: "ALTER INDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 81ba715f-ea9f-4f68-a622-14fda6489d7a
caps.latest.revision: 28
author: BarbKess
---
# ALTER INDEX (SQL Server PDW)
Rebuilds or reorganizes an index or index partition on a SQL Server PDW table. This can also disable an index.  
  
Use ALTER INDEX REBUILD to:  
  
-   Defragment a rowstore or columnstore index or an index partition.  
  
-   Merge rowstore and columnstore data into the columnstore for a clustered columnstore index.  
  
-   Reduce the storage size of a fragmented clustered columnstore index.  
  
Use ALTER INDEX REORGANIZE to:  
  
-   Improve query performance for a clustered columnstore index by moving CLOSED rowgroups into the columnstore.  
  
-   Reorganize the data in the leaf level of a rowstore index while keeping the index online.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER INDEX {index_name | ALL }  
    ON [ database_name . [ schema_name ] . | schema_name. ] table_name  
{  
    REBUILD [ PARTITION = { ALL | partition_number } ]  
    | DISABLE  
    | REORGANIZE [ PARTITION = partition_number ]  
}  
[;]  
```  
  
## Arguments  
*index_name |* ALL  
Specifies the name of the index. ALL rebuilds all indexes on the table.  
  
[ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
Specifies the one-, two-, or three-part name of the table that contains the index to alter.  *table_name* can also be a  temporary table.  
  
REBUILD [ PARTITION = { ALL | *partition_number* } ]  
Specifies to rebuild the index without changing the index definition.  
  
REBUILD  
Specifies to rebuild the entire index.  
  
To rebuild a clustered columnstore index, SQL Server PDW:  
  
-   Acquires an exclusive lock on the table or partition while the rebuild occurs. The data is “offline” and unavailable during the rebuild.  
  
-   Defragments the columnstore by physically deleting rows that have been logically deleted from the table; the deleted bytes are reclaimed on the physical media.  
  
-   Merges the rowstore data, stored in the deltastore, with the data in the columnstore before it rebuilds the index. When the rebuild is finished, all data is stored in columnstore format, and the deltastore, which is a rowstore, contains 0 rows.  
  
-   Re-compresses all data into the columnstore. Two copies of the columnstore index exist while the rebuild is taking place. When the rebuild is finished, SQL Server deletes the original columnstore index.  
  
REBUILD PARTITION = ALL  
Rebuilds all partitions of the index.  
  
REBUILD PARTITION = *partition_number*  
Rebuilds only a specific partition of the index. *partition_number* identifies the number of the partition to rebuild.  
  
For a clustered columnstore index, REBUILD and REBUILD PARTITION = ALL accomplish the same results.  
  
DISABLE  
Marks the index as disabled and unavailable for use. Any index can be disabled. When an index is disabled, the index definition remains in the system catalog and is visible in the catalog views, however, all index data gets deleted. The table data is not deleted, only the index data is deleted.  
  
After disabling a clustered index, you cannot access the underlying table data until the index is enabled. Disabling a clustered columnstore index causes all table data to be unavailable until the index is enabled.  
  
To enable an index use ALTER INDEX REBUILD or [CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md) with the DROP_EXISTING clause.  
  
REORGANIZE [ PARTITION = *partition_number* ]  
For clustered columnstore indexes, this specifies to move all CLOSED rowgroups into the columnstore.  
  
For rowstore indexes, this specifies to reorganize the data in the leaf level of the index while keeping the index online.  Operations on the table, such as updates and queries, can continue while an index is being reorganized. Reorganizing an index is useful for indexes that have a small amount of defragmentation.  
  
The REORGANIZE operation cannot be specified for a disabled index.  
  
ALLOW_PAGE_LOCKS is always ON in SQL Server PDW and will not block you from disabling an index.  
  
PARTITION = *partition_number*  
Reorganizes only the partition numbered as *partition_number*.  
  
## Permissions  
Requires ALTER permission on the table, or membership in one of the following roles:  
  
-   **sysadmin** fixed server role  
  
-   **db_ddladmin** fixed database role  
  
-   **db_owner** fixed database role.  
  
## General Remarks  
When to reorganize a clustered columnstore index:  
  
-   Reorganize a clustered columnstore index after one or more data loads to achieve query performance benefits as quickly as possible. Reorganizing will initially require additional CPU resources to compress the data, which could slow overall system performance. However, as soon as the data is compressed, query performance can improve.  
  
Reorganizing is not required for moving CLOSED rowgroups into the columnstore. The tuple-mover process will eventually find all CLOSED rowgroups and move them. However, the tuple-mover is single-threaded and might not move rowgroups fast enough for your workload.  
  
When to rebuild a clustered columnstore index:  
  
-   Use caution before rebuilding an entire clustered columnstore index because this takes a long time if the index is large, and it requires enough disk space to store an additional copy of the index while it is rebuilding. Usually it is only necessary to rebuild the most recently used partition  
  
-   Rebuild a partition after heavy DML operations to defragment the index and reduce disk storage. For example, deleting rows marks them for deletion but does not actually delete the row from the columnstore until the index is rebuilt. Delting rows also occurs during an update operation; to update a row, SQL Server PDW inserts a new row, and then marks the old row for deletion.  
  
-   Rebuild a partition after loading data to ensure all data is stored in the columnstore. This is useful, especially if the loaded rows land in one or more deltastores of the individual distributions. Note that if multiple loads occur at the same time, each distribution could end up having multiple deltastores.  
  
ALTER INDEX runs in parallel across the Compute nodes. For distributed tables, ALTER INDEX also runs in parallel across the distributions on each Compute node.  
  
## Locking Behavior  
Takes an exclusive lock on the TABLE object. Takes a shared lock on the DATABASE object.  
  
You cannot rebuild an index when the index or the table that contains the index is in use by another user.  
  
REBUILD runs as an offline operation.  
  
REORGANIZE runs as an online operation. While the index is reorganizing, reads are allowed on the index, and writes are not allowed.  
  
## Examples  
  
### A. Basic Syntax  
<pre>ALTER INDEX index1 ON table1 REBUILD;  
ALTER INDEX ALL ON table1 REBUILD;  
ALTER INDEX ALL ON dbo.table1 REBUILD;</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md)  
[DROP INDEX &#40;SQL Server PDW&#41;](../sqlpdw/drop-index-sql-server-pdw.md)  
  
