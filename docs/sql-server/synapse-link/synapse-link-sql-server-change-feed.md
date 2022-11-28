---
title: "Azure Synapse Link change feed for SQL Server 2022 and Azure SQL Database"
description: Learn about the Azure Synapse Link for SQL change feed, introduced for SQL Server 2022 and Azure SQL Database, to allow for real-time analytics of data from SQL Server or Azure SQL Database to Azure Synapse.
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: data-movement
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
ms.custom:
- event-tier1-build-2022
---

# Azure Synapse Link for SQL change feed

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article includes detail on how the Azure Synapse Link for SQL change feed works, a feature new to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and Azure SQL Database. 

[!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] introduces a new feature that allows connectivity between SQL Server tables and the Microsoft Azure Synapse platform, called Azure Synapse Link for SQL. Azure Synapse Link for SQL provides automatic change feeds that capture the changes within SQL Server and load them into Azure Synapse Analytics. 

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- For more information, see:
    - [Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link).
    - [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link).
- To get started quickly, see: 
    - [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022) 
    - [Get started with Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-database)

This feature is not currently available for Azure SQL Managed Instance.

> [!NOTE]
> Enabling Azure Synapse Link for SQL will create a `changefeed` database user, a `changefeed` schema, and several tables within the `changefeed` schema in your source database. Please do not alter any of these objects - they are system-managed.

## Landing zone

For more information on the landing zone for Azure Synapse Link for SQL Server, see [Azure Synapse Link for SQL Server landing zone](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link#landing-zone).

While Azure Synapse Link for SQL Server involves user-provisioned Azure resources including an ADLS Gen2 storage account, the Azure Synapse Link for Azure SQL Database is entirely managed, including provisioning of the landing zone, and uses similar change detection processes as described in this article. For more information, see [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link). 

For SQL Server, the landing zone Azure Storage location is customer-managed and visible, but it is not recommended or supported to consume or modify the files in the landing zone. 

## Change feed feature

An administrator of SQL Server can enable Azure Synapse Link on a table that is empty, or one that already contains data. The source table must have a primary key.

If on a table that already contains data, Azure Synapse Link will seed the landing zone with an initial full snapshot of the source table. When an existing SQL Server table containing data is added to the Azure Synapse Link, a full snapshot of the initial set of data is generated. The initial snapshot file is a .parquet format file that is transmitted to the landing zone in ADLS Gen2. 

Azure Synapse Link supports low latency pushing of source table(s) changes to the landing zone in Azure Storage. The change feed uses a CSV file for publishing these changes to Azure Synapse. This tabular format naturally aligns with writing row-granular data changes at a high cadence (on the order of seconds). Most CSV files should be relatively small.

## Change capture

Capturing changes for Azure Synapse Link is similar to the existing Change Data Capture (CDC) technology. The source of change data is the SQL Server transaction log. The change feed reads the log and adds information about changes to the landing zone.

CDC works by harvesting the transaction log to capture all modifications performed on the source table(s). 

- In CDC, the change data is populated internally to a sibling table in the database.
- In Azure Synapse Link, the data will be read directly from the database transaction log, cached in memory, and eventually written to the landing zone in Azure Storage. 

If a storage outage occurs, it can cause the landing zone to become unavailable, which will block publications to that landing zone. Similar to the behavior if the SQL Server CDC log reader agent fails or is not running, the source database transaction log cannot be truncated. In the case of a prolonged storage outage or storage configuration change that causes it to become inaccessible, stop the Synapse Link through the Synapse Studio.

### High availability support

Azure Synapse Link for SQL Server is compatible with Always On availability groups and failover cluster instances (FCI).

If an initial snapshot was in progress and is interrupted for any reason, the initial snapshot export process will restart. This is the case if a SQL Server fails over in FCI or synchronous availability group.

## See also

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [What's new in SQL Server 2022?](../what-s-new-in-sql-server-2022.md)
- [Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link)
- [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link)
- [Azure Synapse Link for Azure Cosmos DB](/azure/cosmos-db/synapse-link)
- [Azure Synapse Link for Dataverse](/powerapps/maker/data-platform/export-to-data-lake)

## Next steps

- [Get started with Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
