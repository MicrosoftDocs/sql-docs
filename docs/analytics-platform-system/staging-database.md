---
title: Using a staging database
description: SQL Server Parallel Data Warehouse (PDW) uses a staging database to store data temporarily during the load process.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Using a staging database in Parallel Data Warehouse (PDW)
SQL Server Parallel Data Warehouse (PDW) uses a staging database to store data temporarily during the load process. By default, SQL Server PDW uses the destination database as the staging database, which can cause table fragmentation. To reduce table fragmentation, you can create a user-defined staging database. Or, when rollback from a load failure is not a concern, you can use the fastappend loading mode to improve performance by skipping the temporary table and loading directly into the destination table.  
  
## <a name="StagingDatabase"></a>Staging database basics  
A *staging database* is a user-created PDW database that stores data temporarily while it is loaded into the appliance. When a staging database is specified for a load, the appliance first copies the data to the staging database and then copies the data from temporary tables in the staging database to permanent tables in the destination database.  
  
When a staging database is not specified for a load, SQL ServerPDW creates the temporary tables in the destination database and uses them to store the loaded data before it inserts the loaded data into the permanent destination tables.  
  
When a load uses the *fastappend mode*, SQL ServerPDW skips using temporary tables altogether and appends the data directly to the target table. The fastappend mode improves load performance for ELT scenarios where data is loaded into a table that is a temporary table from the application standpoint. For example, an ELT process could load data into a temporary table, process the data by cleansing and de-duping, and then insert the data into the target fact table. In this case, it is not necessary for PDW to first load the data into an internal temporary table before inserting the data into the application's temporary table. The fastappend mode avoids the extra load step, which significantly improves the load performance. To use the fastappend mode, you must use multi-transaction mode, which means that recovery from a failed or aborted load must be handled by your own load process.  
  
**Staging database benefits**  
  
The primary benefit of a staging database is to reduce table fragmentation. If a staging database is not used, the data is loaded into temporary tables in the destination database. When temporary tables get created and dropped in the destination database, the pages for the temporary tables and permanent tables become interleaved. Over time, table fragmentation occurs and degrades performance. In contrast, a staging database ensures that temporary tables are created and dropped in a separate file space than the permanent tables.  
  
**Staging database table structures**  
  
The storage structure for each database table depends on the destination table.  
  
-   For loads into a heap or clustered columnstore index, the staging table is a heap.  
  
-   For loads into a rowstore clustered index, the staging table is a rowstore clustered index.  
  
## <a name="Permissions"></a>Permissions  
Requires CREATE permission (for creating a temporary table) on the staging database. 

<!-- MISSING LINKS

For more information, see [Grant Permissions to load data](grant-permissions-to-load-data.md).  

-->
  
## <a name="CreatingStagingDatabase"></a>Best practices for creating a staging database  
  
1.  There should only be one staging database per appliance. The staging database can be shared by all load jobs for all destination databases.  
  
2.  The size of the staging database is customer-specific. Initially, when first populating the appliance, the staging database should be large enough to accommodate the initial load jobs. These load jobs tend to be large because multiple loads can occur concurrently. After initial load jobs have completed and the system is in production, the size of each load job is likely to be smaller. When loads are small, you can reduce the size of the staging database to accommodate the smaller load sizes. To reduce the size, you can drop the staging database and create it again with smaller size allocations, or you can use the [ALTER DATABASE](../t-sql/statements/alter-database-transact-sql.md?tabs=sqlpdw) statement.  
  
    When creating the staging database, use the following guidelines.  
  
    -   The replicated table size should be the estimated size, per Compute node, of all the replicated tables that will load concurrently. The size is typically 25-30 GB.  
  
    -   The distributed table size should be the estimated size, per appliance, of all the distributed tables that will load concurrently.  
  
    -   The log size is typically similar to the replicated table size.  
  
## <a name="Examples"></a>Examples  
  
### A. Create a staging database 
The following example creates a staging database, Stagedb, for use with all loads on the appliance. Suppose you estimate that five replicated tables of size 5 GB each will load concurrently. This concurrency results in allocating at least 25 GB for the replicated size. Suppose you estimate that six distributed tables of sizes 100, 200, 400, 500, 500, and 550 GB will load concurrently. This concurrency results in allocating at least 2250 GB for the distributed table size.  
  
```sql  
CREATE DATABASE Stagedb  
WITH (  
  
    AUTOGROW = ON,  
  
    REPLICATED_SIZE = 25 GB,  
  
    DISTRIBUTED_SIZE = 2250 GB,  
  
    LOG_SIZE = 25 GB  
  
);  
```  

<!-- MISSING LINKS
 
## See Also  
[Common metadata query examples](metadata-query-examples.md)  

-->
  
