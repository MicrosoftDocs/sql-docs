---
title: "Azure Synapse Link change feed for SQL Server 2022 and Azure SQL Database"
description: Learn about the Azure Synapse Link for SQL Server change feed, introduced for SQL Server 2022 and Azure SQL Database to allow for real-time analytics of data from SQL Server or Azure SQL Database to Azure Synapse.
ms.date: 05/24/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
ms.custom:
---

# Azure Synapse Link change feed

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article includes detail on how the Azure Synapse Link change feed works. This new SQL Server 2022 and Azure SQL Database feature is currently in preview. 

[!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] introduces a new feature that allows connectivity between SQL Server tables and the Microsoft Azure Synapse platform, called Azure Synapse Link. Azure Synapse Link provides automatic change feeds that capture the changes within SQL Server and load them into Azure Synapse Analytics. 

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- To get started quickly, see [Get started with Azure Synapse Link for SQL Server 2022 (Preview)](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022) or [Get started with Synapse Link for Azure SQL Database (Preview)](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-database).

While Azure Synapse Link for Azure SQL Database involves user-provisioned Azure resources including an ADLS Gen2 storage account, the Azure Synapse Link for Azure SQL Database is entirely managed, including provisioning of the landing zone, and uses similar change detection processes as described in this article. For more information, see [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link). 

This feature is not currently available for Azure SQL Managed Instance.

## Change feed feature

The high-level architecture includes a multi-threaded approach for change capture, change publish, and change batch commit. 

- Change captures are responsible for harvesting the transaction log, determining what changes have occurred, and placing these changes in a queue. 
- Change publishers are responsible for consuming the work in the queue, serializing the changes to CSV format, and then writing the serialized data to the landing zone in Azure Storage, in an Azure Data Lake Storage Gen2 container.
- The commit thread is responsible for committing batches to the landing zone, once all writes for that batch have completed.

For more information on the landing zone for Azure Synapse Link for SQL Server, see [Azure Synapse Link for SQL Server landing zone](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link#landing-zone).

An administrator of SQL Server can enable Azure Synapse Link on a table that is empty, or one that already contains data. 

- If on a table that already contains data, we will seed the landing zone with an initial full snapshot of the source table, as described in [Publish Full Snapshot](#publish-full-snapshot). 
- The table must have a primary key.
- In both scenarios, however, the Synapse Link supports low latency pushing of source table(s) changes to the landing zone in Azure Storage.

The change feed uses a CSV file for publishing these changes to Azure Synapse. This tabular format naturally aligns with writing row-granular data changes at a high cadence (on the order of seconds). Most CSV files should be relatively small.

:::image type="content" source="media/synapse-link-sql-server-change-feed/azure-synapse-link-sql-server-change-feed.png" alt-text="A diagram of the Azure Synapse Link change feed data flow.":::

At most, one change capture thread will work on any one database at a given time. At most, one change publisher thread will work on a specific table at a given time, however, different publish threads could be concurrently handling distinct tables in the same database. The single committer thread will commit changes for one database/batch at a time.

## Change capture

Capturing changes for Azure Synapse Link is similar to the existing Change Data Capture (CDC) technology. The source of change data is the SQL Server transaction log. The change feed reads the log and adds information about changes to the landing zone.

Let's compare the existing SQL Server CDC to Azure Synapse Link for SQL Server. The primary difference with Azure Synapse Link is how the change data is handled after being harvested from the log. 

CDC works by harvesting the transaction log to capture all modifications performed on the source table(s). 

- In CDC, the change data is populated internally to a sibling table in the database.
- In Azure Synapse Link, the data will be cached in memory to be consumed by another task (change publishing) and eventually written to a landing zone in Azure Storage. 
- To parallelize further, CDC also has a cleanup job that is responsible for removing old entries from change tables, once they have expired or are no longer needed. Synapse Link will not require this; cleanup is the responsibility of the landing zone service in Azure Storage.
 
For Azure Synapse Link, the column values will be cached in a queue to be consumed by change publish workers. Each execution of the change capture process produces one batch of incremental changes for a database. The batch size is controlled by the number of transactions that the log reader processes, which is a configurable value. 

### Change buffer and caching 

One capture scan produces exactly one change batch for the landing zone in Azure Storage, and therefore requires exactly one commit to occur. 

Each change batch may contain many transactions that modify any number of tables. We will buffer the change data in-memory so it can be easily consumed by change publish workers, with the following characteristics:
 
* Change data for a table will be appended to the landing zone in the same order that the modifications were performed. This ordering requirement only applies to changes within a given table; serial write ordering across distinct tables does not need to be maintained because each table is modeled as a unique entity in the landing zone. 
* Within a batch, change data for all tables must be written to the landing zone before committing the batch. 

The above are critical from both a correctness and performance perspective. To guarantee that table changes are serialized to the landing zone in order, we make sure only one thread publishes changes for a table at a time. Additionally, to maximize performance and make sure our I/O to the landing zone can keep up with the OLTP log rate, we concurrently publish changes for different tables. 

### Change publish 

Once the change capture has been performed, the change data lives in a cache to be consumed by change publisher(s). The publishers have a simple responsibility: transform the change data to a serialized format (CSV) and write the serialized data to the landing zone.

When the change publisher detects that all the change data has been successfully written to the landing zone and the change capture has completed for the batch, it will append an item to the commit queue and the commit worker will eventually commit the batch. Change publishers do not need to wait for previous batches to be committed before beginning to publish subsequent batches.  

To summarize, the change publisher worker operates as follows: 

1. If no is work available, wait on a partition to be added to work queue.
2. For each change data row in partition, serialize data to CSV.
3. Write serialized data to the landing zone in Azure Storage.
4. If last change row in batch and change capture job is complete.
5. Add batch to commit job queue.

Each Azure Synapse Link for SQL Server table group in the instance has its own set of partitions, and partition on each table. This is an intentional decision to minimize the effect of an Azure Storage outage. Each table group publishes to a distinct landing zone. When a storage outage occurs, it can cause a landing zone to become unavailable which will block publications to that landing zone. Guaranteeing that each partition only spans tables in a single table group will prevent a single landing zone outage from blocking publications for other table groups. 

### Change commit 

The change batch commit worker is responsible for writing the manifest file to the landing zone. The manifest file indicates that all incremental changes have been successfully published to the landing zone. A batch can only be committed after all changes in the batch have been captured and successfully written to the landing zone. 

When the capture thread has completely finished the batch and all changes for the batch have been written by change publisher, the change commit worker will append a commit record to the manifest file. 

If the Azure Synapse dedicated pool is paused, there is no impact to the source SQL Server system. Changes accumulate in the landing zone and are cleaned up 24 hours after being consumed.

## Publish full snapshot

When an existing SQL Server table containing data is added to the Azure Synapse Link, a full snapshot of the initial set of data is generated. The overall workflow to publish the full snapshot is: 

1. Customer enables a table (i.e `tab1`) for Synapse publishing. This adds a row in metadata table `MSSynapseLinkTables` with the starting state as "Enabled". 
2. A timer thread queries the metadata table periodically to poll for any tables with "Enabled" state. With some throttling mechanism, it starts the export process in another thread: 
    1. Update the state of `tab1` to be "Exporting". 
    1. Call landing zone library to signal start of full snapshot of `tab1`.  
    1. Inform capture process to start capture changes for the tables. 
    1. Extract schema from live schema and write to landing zone. 
    1. Take brief shared table lock and retrieve the start LSN for reconciliation.  
    1. Calls data extraction T-SQL to extract table to the destination. 
    1. Update the state of `tab1` to be "Exported" state. 
    1. Get end of log again for end LSN of reconciliation.  
    1. Update the state of `tab1` to be "Active". 

Step 2.b. and 2.h. are the steps to write the start/end snapshot manifest to landing zone. 

### High availability support

Azure Synapse Link for SQL Server is compatible with Always On availability groups and failover cluster instances (FCI).

When a SQL Server fails over in FCI or synchronous availability group setup:

* If the tables are in the "Exporting" state, it will restart their export process from the start, moving them back to the "Enabled" state. 
* If the full snapshot export is interrupted between step 2g and 2i (as previously described), it will simply write the manifest record again. 

## See also

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [What's new in SQL Server 2022?](../what-s-new-in-sql-server-2022.md)
- [Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link)
- [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link)
- [Synapse Link for Azure Cosmos DB](/azure/cosmos-db/synapse-link)
- [Synapse Link for Dataverse](/powerapps/maker/data-platform/export-to-data-lake)

## Next steps

- [Get started with Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
