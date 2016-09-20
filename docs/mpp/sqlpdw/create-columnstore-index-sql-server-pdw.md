---
title: "CREATE COLUMNSTORE INDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 13fb3d5d-8908-430b-b1eb-ed5bbd83e6d6
caps.latest.revision: 15
author: BarbKess
---
# CREATE COLUMNSTORE INDEX (SQL Server PDW)
Creates or rebuilds an xVelocity memory-optimized clustered columnstore index on a SQL Server PDW table. Use this primarily to rebuild all partitions of a clustered columnstore index or to convert a small rowstore dimension table to a columnstore table.  
  
To better understand clustered columnstore indexes, see [Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE CLUSTERED COLUMNSTORE INDEX index_name   
    ON [ database_name . [ schema_name ] . | schema_name . ] table_name  
    [ WITH ( DROP_EXISTING = { ON | OFF } ) ]  
[;]  
```  
  
## Arguments  
*index_name*  
Is the name of the clustered columnstore index. When DROP_EXISTING = ON is used, *index_name*  is the name of an existing index.  
  
Index names must follow the rules of [identifiers](assetId:///171291bb-f57f-4ad1-8cea-0b092d5d150c).  
  
*database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
Specifies the one-, two-, or three-part name of the table that will have the index. The table can be a regular table or a temporary table.  
  
DROP_EXISTING  
Specifies whether to drop and rebuild and existing index, or to create a new index.  
  
ON  
Specifies to drop and then rebuild the existing index, specified by *index_name* , as a clustered columnstore index. The existing index can be a clustered index or a clustered columnstore index.  
  
OFF  
Specifies to create a new clustered columnstore index. All indexes must be dropped before using this option.  
  
## Permissions  
Requires **ALTER** permission on the table, or membership in one of the following roles:  
  
-   **sysadmin** fixed server role  
  
-   **db_ddladmin** fixed database role  
  
-   **db_owner** fixed database role  
  
## General Remarks  
CREATE COLUMNSTORE INDEX with the DROP_EXISTING = ON option is the same as calling ALTER INDEX with the REBUILD PARTITION = ALL option. The difference between the two statements is that with ALTER INDEX you can specify one or more partitions, whereas with CREATE COLUMNSTORE INDEX you must rebuild the entire index.  
  
For a distributed table, creating a clustered columnstore index applies after a table is created and the rows are distributed and partitioned. It does not affect the way the rows are distributed and partitioned.  
  
For a replicated table, the clustered columnstore index applies after a table is created and replicated to the Compute nodes. It does not affect replication.  
  
## Limitations and Restrictions  
You can create up to 16 clustered columnstore indexes concurrently on the appliance, as long as each index is created on a different table. You can queue additional requests for clustered columnstore indexes up to the SQL Server PDW request limit of 1024 total requests that can be running or waiting on the appliance.  
  
Only one clustered columnstore index can exist on a table at any given time. If two or more clustered columnstore index requests are issued concurrently on the same table, they will block until the one that is currently running finishes. You can cancel the requests that are waiting, if desired.  
  
Non-clustered indexes are not allowed on a table that has a clustered columnstore index.  
  
Like other DDL statements in SQL Server PDW, you cannot use CREATE COLUMNSTORE INDEX within an explicit transaction. However, the PDW does guarantee the atomicity of each CREATE COLUMNSTORE INDEX statement. If a failure occurs while SQL Server PDW is creating a clustered columnstore index, all intermediate steps will be rolled back and the data will remain unchanged.  
  
## Locking Behavior  
All read and write operations on the table are blocked while a clustered columnstore index is being created or rebuilt.  
  
## Metadata  
When CREATE COLUMNSTORE INDEX converts a rowstore table is converted to a columnstore table, SQL Server PDW retains all metadata and objects associated with the rowstore table. For example, column definitions, default constraints, and permissions, are retained. Objects such as views and statistics on the table are retained.  
  
All of the columns in a clustered columnstore index are stored in the metadata as included columns. The clustered columnstore index does not have key columns.  
  
Related system catalog views:  
  
-   [sys.indexes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-indexes-sql-server-pdw.md)  
  
-   [sys.index_columns &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-index-columns-sql-server-pdw.md)  
  
-   [sys.partitions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-partitions-sql-server-pdw.md)  
  
Related dynamic management views (DMVs):  
  
-   [sys.pdw_nodes_column_store_segments &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-nodes-column-store-segments-sql-server-pdw.md)  
  
-   [sys.pdw_nodes_column_store_dictionaries &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-nodes-column-store-dictionaries-sql-server-pdw.md)  
  
-   [sys.pdw_nodes_column_store_row_groups &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-nodes-column-store-row-groups-sql-server-pdw.md)  
  
## Performance  
CREATE COLUMNSTORE INDEX runs in parallel across the Compute nodes for both replicated and distributed tables, and runs serially across the distributions for each Compute node which is slow for large table. However, CREATE TABLE AS SELECT performs in parallel across the Compute nodes and in parallel across the distributions within each Compute node. This is one of the reasons why we recommend using CREATE TABLE AS SELECT instead of CREATE COLUMNSTORE INDEX for converting large tables from rowstore to columnstore.  
  
## Examples  
  
### A. Change a clustered index to a clustered columnstore index  
By using the CREATE CLUSTERED COLUMNSTORE INDEX statement with DROP_EXISTING = ON, you can:  
  
-   Change a clustered index into a clustered columnstore index.  
  
-   Rebuild a clustered columnstore index.  
  
This example creates the xDimProduct table as a rowstore table with a clustered index, and then uses CREATE CLUSTERED COLUMNSTORE INDEX to change the table from a rowstore table to a columnstore table.  
  
```  
USE AdventureWorksPDW2012;  
  
IF EXISTS (SELECT name FROM sys.tables  
    WHERE name = N'xDimProduct'  
    AND object_id = OBJECT_ID (N'xDimProduct'))  
DROP TABLE xDimProduct;  
  
--Create a distributed table with a clustered index.  
CREATE TABLE xDimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey)  
WITH ( DISTRIBUTION = HASH(ProductKey),  
    CLUSTERED INDEX (ProductKey) )  
AS SELECT ProductKey, ProductAlternateKey, ProductSubcategoryKey FROM DimProduct;  
  
--Change the existing clustered index   
--to a clustered columnstore index with the same name.  
--Look up the name of the index before running this statement.  
CREATE CLUSTERED COLUMNSTORE INDEX <index_name>   
ON xdimProduct   
WITH ( DROP_EXISTING = ON );  
```  
  
### B. Rebuild a clustered columnstore index  
Building on the previous example, this example uses CREATE CLUSTERED COLUMNSTORE INDEX to rebuild the existing clustered columnstore index called cci_xDimProduct.  
  
```  
--Rebuild the existing clustered columnstore index.  
CREATE CLUSTERED COLUMNSTORE INDEX cci_xDimProduct   
ON xdimProduct   
WITH ( DROP_EXISTING = ON );  
```  
  
### C. Change the name of a clustered columnstore index  
To change the name of a clustered columnstore index, drop the existing clustered columnstore index, and then recreate the index with a new name.  
  
We recommend only doing this operation with a small table or an empty table. It will take a long time to drop a large clustered columnstore index and rebuild with a different name.  
  
Using the cci_xDimProduct clustered columnstore index from the previous example, this example drops the cci_xDimProduct clustered columnstore index and then recreates the clustered columnstore index with the name mycci_xDimProduct.  
  
```  
--For illustration purposes, drop the clustered columnstore index.   
--The table continues to be distributed, but changes to a heap.  
DROP INDEX cci_xdimProduct ON xDimProduct;  
  
--Create a clustered index with a new name, mycci_xDimProduct.  
CREATE CLUSTERED COLUMNSTORE INDEX mycci_xDimProduct  
ON xdimProduct  
WITH ( DROP_EXISTING = OFF );  
```  
  
### D. Convert a columnstore table to a rowstore table with a clustered index  
There might be a situation for which you want to drop a clustered columnstore index and create a clustered index. This stores the table in rowstore format. This example converts a columnstore table to a rowstore table with a clustered index with the same name. None of the data is lost. All data goes to the rowstore table and the columns listed become the key columns in the clustered index.  
  
```  
--Drop the clustered columnstore index and create a clustered rowstore index.   
--All of the columns will be stored in the rowstore clustered index.   
--The columns listed will be the included columns in the index.  
CREATE CLUSTERED INDEX cci_xDimProduct    
ON xdimProduct (ProductKey, ProductAlternateKey, ProductSubcategoryKey, WeightUnitMeasureCode)  
WITH ( DROP_EXISTING = ON);  
```  
  
### E. Convert a columnstore table back to a rowstore heap  
Use [DROP INDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-index-sql-server-pdw.md) to drop the clustered columnstore index and convert the table to a rowstore heap. This example converts the cci_xDimProduct table to a rowstore heap. The table continues to be distributed, but is stored as a heap.  
  
```  
--Drop the clustered columnstore index. The table continues to be distributed, but changes to a heap.  
DROP INDEX cci_xdimProduct ON xdimProduct;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
