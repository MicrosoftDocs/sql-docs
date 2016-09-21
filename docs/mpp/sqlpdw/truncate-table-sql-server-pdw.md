---
title: "TRUNCATE TABLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 964f5740-2ee6-412a-8c14-bcdf39cda8bd
caps.latest.revision: 10
author: BarbKess
---
# TRUNCATE TABLE (SQL Server PDW)
Removes all rows from a SQL Server PDW table, and does not log the individual row deletions.  
  
## Syntax  
  
```vb  
TRUNCATE TABLE [ { database_name . [ schema_name ] . | schema_name . ] table_name  
[;]  
```  
  
## Arguments  
[ *database_name* . [*schema_name* ] . | *schema_name* . ] *table_name*.  
The name of the table to truncate. The table name can optionally include the schema, or the database and schema.  
  
## General Remarks  
The table can be distributed or replicated.  
  
The table can be permanent or temporary.  
  
The table can be rowstore or columnstore.  
  
Both TRUNCATE TABLE and DELETE without the WHERE clause result in removing all rows from the table. TRUNCATE TABLE performs faster than DELETE because it does not log the individual row deletions.  
  
Truncating the table removes all pages in the table. This is true for both distributed and replicated tables.  
  
Batch statements and stored procedures can contain TRUNCATE TABLE.  
  
When TRUNCATE TABLE is cancelled before completion, the table remains intact with no data changes.  
  
When truncating a table, SQL Server PDW keeps and updates all statistics objects, both user-defined and auto-generated. Indexes on the table are also updated. After TRUNCATE TABLE completes, all statistics are updated by using the default row count (1000).  
  
While TRUNCATE TABLE is running, all other operations on the table will be blocked.  
  
## Limitations and Restrictions  
TRUNCATE TABLE is not allowed within the EXPLAIN statement.  
  
TRUNCATE TABLE is not allowed within a user-defined transaction.  
  
## Logging Behavior  
Truncating a columnstore table is minimally logged.  
  
## Performance  
For a replicated table, TRUNCATE TABLE is executed in parallel across the compute nodes.  
  
For a distributed table, TRUNCATE TABLE is executed in parallel across the compute nodes, and serially across the distributions within each Compute node.  
  
## Permissions  
Requires ALTER permission on the target table, table ownership, membership in one of the following roles:  
  
-   **sysadmin** fixed server role  
  
-   **db_owner** fixed database role  
  
-   **db_ddladmin** fixed database role  
  
## Differences from SQL Server  
  
|Behavior|SQL Server|SQL Server PDW|  
|------------|-------------------------------------------------------|-----------------------------------------|  
|Run TRUNCATE TABLE within a transaction.|yes|no|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Definition Statements &#40;SQL Server PDW&#41;](../sqlpdw/data-definition-statements-sql-server-pdw.md)  
[SQL Reference &#40;SQL Server PDW&#41;](../sqlpdw/sql-reference-sql-server-pdw.md)  
  
