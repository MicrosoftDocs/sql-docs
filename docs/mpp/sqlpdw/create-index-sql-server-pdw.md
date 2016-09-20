---
title: "CREATE INDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 711b1828-927d-4b94-9cb5-e9d3b0dc5b32
caps.latest.revision: 41
author: BarbKess
---
# CREATE INDEX (SQL Server PDW)
Creates a rowstore index on a table in SQL Server PDW. Use this statement to improve query performance.  
  
A *rowstore index* stores and runs your table data by rows. Previously, this was the only way to store and run data in SQL Server PDW. Starting with this release, you can use a clustered columnstore index to run your data by columns. The clustered columnstore index gives tremendous benefits for both performance and data compression.  
  
For more information on storing, accessing, and updating columnar data, see [CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-columnstore-index-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE [ CLUSTERED | NONCLUSTERED ] INDEX index_name   
    ON [ database_name . [ schema_name ] . | schema_name . ] table_name( { column [ ASC | DESC ] } [ ,...n ] )  
    WITH ( DROP_EXISTING = { ON | OFF } )  
[;]  
```  
  
## Arguments  
CLUSTERED | **NONCLUSTERED**  
Specifies whether the index is clustered or nonclustered.  
  
CLUSTERED  
Specifies to create an index in which the actual data is stored according to the sorted order of the column values.  Each table can have only one clustered index at the same time.  
  
To avoid rebuilding nonclustered indexes, create the clustered index first and then create any nonclustered indexes. All existing nonclustered indexes on a table are rebuilt when a clustered index is created.  
  
For distributed tables, a clustered index affects the way data is stored within each distribution across the Compute nodes; it does not affect which rows are assigned to each distribution.  For replicated tables, the clustered index affects the way the data is stored within each replicated table; it does not affect where the replicated tables are stored.  
  
NONCLUSTERED  
Specifies to create an index which stores and sorts the index values separately from the full table data. With a nonclustered index, the physical order of the actual data rows is independent of the indexed order.  
  
*index_name*  
The name of the index to be created. Index names must be unique per table. For more information on permitted index names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
[ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
The three-part name of the table to be indexed. The table name can optionally include the schema, or the database and schema.  
  
The table can be empty or contain data. Either way, the index will be updated when data is added, deleted, or modified.  
  
*column_name* [ **ASC** | DESC ] } [ **,**...*n* ]  
The list of key columns to include in the index. For each column, you can specify ascending or descending sort order. Default is ASC.  
  
The key columns are combined to form a single composite index key and should be listed in sort-priority order. For the maximum number of key columns and the maximum allowable size of the combined index values, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
DROP_EXISTING = { ON | **OFF** }  
Is an option to rebuild an existing index with modified column specifications, and keep the same name for the index.  
  
ON  
Specifies to drop and rebuild the existing index, which must have the same name as the parameter *index_name*.  
  
OFF  
Specifies not to drop and rebuild the existing index. SQL Server PDW will display an error if the specified index name already exists.  
  
With DROP_EXISTING, you can change:  
  
-   A nonclustered rowstore index to a clustered rowstore index.  
  
With DROP_EXISTING, you cannot change:  
  
-   A clustered rowstore index to a nonclustered rowstore index.  
  
-   A clustered columnstore index to any type of rowstore index.  
  
## Permissions  
Requires **ALTER** permission on the table or membership in the **db_ddladmin** fixed database role.  
  
## General Remarks  
SQL Server PDW uses SQL Server to create query optimization statistics for each index.  
  
To view information on existing indexes, you can query the [sys.indexes](../../mpp/sqlpdw/sys-indexes-sql-server-pdw.md) catalog view.  
  
## Limitations and Restrictions  
You cannot create a rowstore index on a SQL Server PDW table when a columnstore index already exists. This behavior is different from SMP SQL Server which allows both rowstore and nonclustered columnstore indexes to co-exist on the same table.  
  
Indexes cannot be created on views.  
  
For information on minimum and maximum constraints on indexes, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
## Locking  
Takes an exclusive lock on the TABLE object. Takes a shared lock on the DATABASE object.  
  
## Examples  
  
### A. Basic syntax  
  
```  
CREATE INDEX IX_VendorID   
    ON ProductVendor (VendorID);  
CREATE INDEX IX_VendorID   
    ON dbo.ProductVendor (VendorID DESC, Name ASC, Address DESC);  
CREATE INDEX IX_VendorID   
    ON Purchasing..ProductVendor (VendorID);  
```  
  
### B. Create a non-clustered index on a table in the current database  
The following example creates a non-clustered index on the `VendorID` column of the `ProductVendor` table.  
  
```  
CREATE INDEX IX_ProductVendor_VendorID   
    ON ProductVendor (VendorID);  
```  
  
### C. Create a clustered index on a table in another database  
The following example creates a non-clustered index on the `VendorID` column of the `ProductVendor` table in the `Purchasing` database.  
  
```  
CREATE CLUSTERED INDEX IX_ProductVendor_VendorID   
    ON Purchasing..ProductVendor (VendorID);  
```  
  
### D. Add a column to an index  
The following example creates index IX_FF with two columns from the dbo.FactFinance table.  The next statement demonstrates rebuilding that index with the same name and one more column.  
  
```  
USE AdventureWorksPDW2012;  
CREATE INDEX IX_FF ON dbo.FactFinance (  
    FinanceKey ASC, DateKey ASC );  
  
--Rebuild and add the OrganizationKey  
CREATE INDEX IX_FF ON dbo.FactFinance (  
    FinanceKey, DateKey, OrganizationKey DESC)  
WITH ( DROP_EXISTING = ON );  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER INDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-index-sql-server-pdw.md)  
[DROP INDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-index-sql-server-pdw.md)  
  
