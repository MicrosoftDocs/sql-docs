---
title: "DROP INDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f59cab43-9f40-41b4-bfdb-d90e80e9bf32
caps.latest.revision: 33
author: BarbKess
---
# DROP INDEX (SQL Server PDW)
Removes and deletes an index in SQL Server PDW.  
  
To drop an index you can also use [CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md) with the DROP_EXISTING clause.  This converts an existing index to an index of a different type and keeps the same name.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP INDEX index_name ON [ database_name . [schema_name ] . | schema_name . ] table_name  
[;]  
```  
  
## Arguments  
*index_name*  
The name of the index to be dropped. The index can be of any type including a rowstore clustered index, a rowstore non-clustered index, or a clustered columnstore index.  
  
[ *database_name* . [*schema_name* ] . | *schema_name* . ] *table_name*  
The three-part name of the table that has the index to drop. The table name can optionally include the schema or the database and schema.  
  
## Permissions  
Requires **ALTER** permission on the table or membership in one of the following roles:  
  
-   **sysadmin** fixed server role.  
  
-   **db_ddladmin** fixed database role.  
  
-   **db_owner** fixed database role.  
  
## General Remarks  
When an index is dropped, SQL Server PDW removes the index and deletes all metadata for the index. Table values are not deleted.  
  
When a clustered columnstore index is dropped:  
  
-   The columnstore table is converted into a rowstore heap.  
  
-   All auto-created statistics are dropped.  
  
-   Statistics created with the CREATE STATISTICS statement are not dropped.  
  
For replicated tables, dropping a clustered index, both rowstore and columnstore, runs in parallel across the Compute nodes. If the table is distributed, dropping an index runs in parallel across the Compute nodes and across the distributions within each Compute node.  
  
## Limitations and Restrictions  
Each table can have only one DROP INDEX command running. Additional DROP INDEX requests will wait until the current request finished.  
  
DROP INDEX cannot run within a transaction.  
  
## Locking  
Takes an exclusive lock on the TABLE object. Takes a shared lock on the DATABASE object.  
  
## Examples  
  
### A. Dropping an index on a table  
The following example deletes the index `VendorIDIndex` on the `ProductVendor` table.  
  
```  
DROP INDEX VendorIDIndex ON ProductVendor;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md)  
[ALTER INDEX &#40;SQL Server PDW&#41;](../sqlpdw/alter-index-sql-server-pdw.md)  
  
