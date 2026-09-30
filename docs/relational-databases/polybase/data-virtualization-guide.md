---
title: Connect, Query, and Export Data with PolyBase
description: Guide to PolyBase and data virtualization across SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. Describes feature availability, connection methods, how to choose between OPENROWSET, external tables, and BULK INSERT, type mapping, and quick start examples.
author: markingmyname
ms.author: maghan
ms.reviewer: hudequei, randolphwest
ms.date: 09/10/2026
ms.service: sql
ms.subservice: polybase
ms.topic: concept-article
ai-usage: ai-assisted
monikerRange: ">=sql-server-2016 || >=sql-server-linux-ver15 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# Connect, query, and export data with PolyBase

[!INCLUDE [SQL Server 2016 Azure SQL Database Azure SQL Managed Instance SQL database in Fabric](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

*Data virtualization* lets you run [!INCLUDE [tsql-md](../../includes/tsql-md.md)] (T-SQL) queries over external data without loading it into your database. You define an external data source, optional file format, and external table, and then query the external table with `SELECT` like any other table.

This guide helps you:

> [!div class="checklist"]
> - Understand which PolyBase features your SQL platform and version support.
> - Choose between `OPENROWSET`, external tables, and `BULK INSERT` for querying or ingesting data.
> - Follow step-by-step links for common scenarios.
> - Review performance, troubleshooting, and best practices for production workloads.

## Platform support

- PolyBase is a [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) feature that implements data virtualization.
- PolyBase is supported in SQL Server 2016 and later versions on Windows and in [SQL Server 2019 and later versions on Linux](polybase-linux-setup.md).
    - PolyBase isn't supported in SQL Server 2017 on Linux.
- PolyBase isn't supported in Azure SQL Database, but Azure SQL Database provides related data virtualization capabilities through `OPENROWSET` and external tables. For more information, see [Data virtualization with Azure SQL Database (Preview)](/azure/azure-sql/database/data-virtualization-overview).
- PolyBase isn't a feature in Azure SQL Managed Instance by name, but Azure SQL Managed Instance has data virtualization features that operate similarly. For more information, see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview?view=azuresql-mi&preserve-view=true).
- PolyBase isn't supported in SQL database in Fabric, but SQL database in Fabric provides its own data virtualization capabilities for data in OneLake. For more information, see [Data virtualization in SQL database in Fabric](/fabric/database/sql/data-virtualization).
- PolyBase isn't a feature in Fabric Data Warehouse. For data virtualization in Fabric Data Warehouse, consider [Fabric OneLake Shortcuts](/fabric/onelake/onelake-shortcuts). For data loading articles in Fabric Data Warehouse, see [Data ingestion with T-SQL](/fabric/data-warehouse/ingest-data-tsql) and [Dimensional modeling: Load tables](/fabric/data-warehouse/dimensional-modeling-load-tables).

## Common use cases

The following table describes possible usage scenarios.

| Scenario | Use |
| --- | --- |
| **Ad-hoc file exploration** | `OPENROWSET(BULK ...)` |
| **Reusable file querying for BI or reporting** | External tables over files |
| **Cross-database querying** (SQL Server, Oracle, Teradata, MongoDB, ODBC) | PolyBase connectors with external tables |
| **Exporting query results to files** | `CREATE EXTERNAL TABLE AS SELECT` (CETAS) |
| **Bulk ingestion into tables** | `BULK INSERT` or `OPENROWSET(BULK ...)` with `INSERT ... SELECT` |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- For ad hoc file exploration, use `OPENROWSET(BULK ...)` to inspect files without creating a reusable table.
- For reusable file querying in BI or reporting scenarios, use external tables over files to persist a schema and share results across queries.
- For cross-database querying, use PolyBase connectors with external tables to access SQL Server, Oracle, Teradata, MongoDB, or ODBC sources.
- For exporting query results to files, use `CREATE EXTERNAL TABLE AS SELECT` (CETAS) to write Parquet or CSV output outside the database.
- For bulk ingestion into tables, use `BULK INSERT` or `OPENROWSET(BULK ...)` with `INSERT ... SELECT` to load file data into database tables.

## Which features are available where?

The following table shows which core PolyBase and data virtualization features are available on each SQL platform starting with SQL Server 2019. For feature availability in SQL Server 2016 and SQL Server 2017 on Windows, see [PolyBase features and limitations](polybase-versioned-feature-summary.md). Use this table to determine what you can do on your platform before you use the detailed guides.

| Feature | SQL Server 2019 | SQL Server 2022 | SQL Server 2025 | Azure SQL Database | Azure SQL Managed Instance | SQL database in Microsoft Fabric |
| --- | :---: | :---: | :---: | :---: | :---: | :---: |
| **External tables** | Yes | Yes | Yes | Yes | Yes | Yes |
| **OPENROWSET (BULK)** | Yes <sup>1</sup> | Yes | Yes | Yes | Yes | Yes |
| **CETAS** (export) | No | Yes | Yes | No | Yes | No |
| **CSV / delimited files** | Yes <sup>2</sup> | Yes | Yes | Yes | Yes | Yes |
| **Parquet files** | No | Yes | Yes | Yes | Yes | Yes |
| **Delta Lake tables** | No | Yes | Yes | No | No | No |
| **Connect to another SQL Server** | Yes | Yes | Yes | No | No | No |
| **Connect to Azure SQL Database or Azure SQL Managed Instance** | Yes <sup>3</sup> | Yes <sup>3</sup> | Yes <sup>3</sup> | No | No | No |
| **Connect to Oracle / Teradata / MongoDB** | Yes | Yes | Yes | No | No | No |
| **Connect to Azure Blob Storage** | Yes | Yes | Yes | Yes | Yes | No |
| **Connect to ADLS Gen2** | Yes <sup>5</sup> | Yes | Yes | Yes | Yes | No |
| **Connect to S3-compatible storage** | No | Yes | Yes | No | No | No |
| **Connect to OneLake (Fabric)** | No | No | No | No | No | Yes |
| **Pushdown computation** | Yes | Yes | Yes | No | No | No |
| **Managed Identity authentication** | No | No | Yes <sup>4</sup> | Yes | Yes | No |

<sup>1</sup> [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] supports `OPENROWSET(BULK...)` for local and network file paths. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, `OPENROWSET(BULK...)` also supports reading from cloud storage with `FORMAT = 'PARQUET'`, `FORMAT = DELTA`, and `FORMAT = 'CSV'`.

<sup>2</sup> CSV support in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] required Hadoop. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, CSV is natively supported without Hadoop.

<sup>3</sup> Uses the SQL Server connector (`sqlserver://`). The database-scoped credential targets the SQL endpoint. Use the same steps as for connecting to another SQL Server instance.

<sup>4</sup> Managed Identity authentication is supported for connecting to Azure Blob Storage (ABS) and ADLS Gen2. It requires Azure Arc-enabled SQL Server or SQL Server on an Azure VM for on-premises SQL Server. It's natively available on Azure SQL Database and Azure SQL Managed Instance.

<sup>5</sup> SQL Server 2019 CU11 and later versions support Azure Data Lake Storage Gen2 with the `abfs` or `abfss` prefix. In SQL Server 2022 and later versions, use the `adls` prefix.

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- External tables are supported in SQL Server 2019, SQL Server 2022, SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
- `OPENROWSET (BULK)` is supported in SQL Server 2019, SQL Server 2022, SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. SQL Server 2019 supports local and network file paths, while SQL Server 2022 and later versions also support reading cloud storage with `FORMAT = 'PARQUET'`, `FORMAT = DELTA`, and `FORMAT = 'CSV'`.
- CETAS export isn't supported in SQL Server 2019, Azure SQL Database, or SQL database in Microsoft Fabric. CETAS export is supported in SQL Server 2022, SQL Server 2025, and Azure SQL Managed Instance.
- CSV and delimited files are supported in SQL Server 2019, SQL Server 2022, SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. SQL Server 2019 requires Hadoop for CSV support, while SQL Server 2022 and later versions support CSV natively without Hadoop.
- Parquet files aren't supported in SQL Server 2019. Parquet files are supported in SQL Server 2022, SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
- Delta Lake tables aren't supported in SQL Server 2019, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric. Delta Lake tables are supported in SQL Server 2022 and SQL Server 2025.
- Connecting to another SQL Server instance is supported in SQL Server 2019, SQL Server 2022, and SQL Server 2025. Connecting to another SQL Server instance isn't supported from Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.
- Connecting to Azure SQL Database or Azure SQL Managed Instance is supported from SQL Server 2019, SQL Server 2022, and SQL Server 2025 by using the SQL Server connector. The database-scoped credential targets the Azure SQL Database or Azure SQL Managed Instance endpoint, and the configuration steps are the same as for connecting to another SQL Server instance. These connections aren't supported from Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.
- Connecting to Oracle, Teradata, or MongoDB is supported from SQL Server 2019, SQL Server 2022, and SQL Server 2025. These connections aren't supported from Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.
- Connecting to Azure Blob Storage is supported from SQL Server 2019, SQL Server 2022, SQL Server 2025, Azure SQL Database, and Azure SQL Managed Instance. Connecting to Azure Blob Storage isn't supported from SQL database in Microsoft Fabric.
- Connecting to ADLS Gen2 isn't supported in SQL Server 2019 releases before CU11 or from SQL database in Microsoft Fabric. Connecting to ADLS Gen2 is supported starting with SQL Server 2019 CU11, and in SQL Server 2022, SQL Server 2025, Azure SQL Database, and Azure SQL Managed Instance.
- Connecting to S3-compatible storage isn't supported from SQL Server 2019, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric. Connecting to S3-compatible storage is supported from SQL Server 2022 and SQL Server 2025.
- Connecting to OneLake is supported from SQL database in Microsoft Fabric. Connecting to OneLake isn't supported from SQL Server 2019, SQL Server 2022, SQL Server 2025, Azure SQL Database, or Azure SQL Managed Instance.
- Pushdown computation is supported in SQL Server 2019, SQL Server 2022, and SQL Server 2025. Pushdown computation isn't supported in Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.
- Managed Identity authentication isn't supported in SQL Server 2019 or SQL Server 2022. Managed Identity authentication is supported in SQL Server 2025 for connections to Azure Blob Storage and ADLS Gen2, and it requires Azure Arc-enabled SQL Server or SQL Server on an Azure virtual machine. Managed Identity authentication is also supported in Azure SQL Database and Azure SQL Managed Instance, but it isn't supported in SQL database in Microsoft Fabric.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], querying data files (CSV, Parquet, and Delta) on Azure Blob Storage, ADLS Gen2, or S3-compatible storage is a native engine capability and no longer requires installing or running PolyBase services. RDBMS connectors (SQL Server, Oracle, Teradata, MongoDB, ODBC) still require PolyBase services to be installed and running. [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] also adds Linux support for these connectors, which were previously available on Windows only.

## Query external data

Before you choose a specific scenario, understand the three ways to query external data:

| Approach | Syntax | Use when | Authentication | PolyBase installation required |
| --- | --- | --- | --- | :---: |
| **OLE DB ad hoc queries** | `OPENROWSET(provider, connection, query)` | You want a quick one-time query without persistent objects, or need Microsoft Entra ID authentication | SQL authentication, Windows authentication, Microsoft Entra ID (MSOLEDBSQL) | No |
| **Ad hoc queries on files** | `OPENROWSET(BULK ...)` | You want to explore file data quickly or test schemas before creating a table | SAS token, access key, Managed Identity, Microsoft Entra ID | SQL Server 2022: Yes <sup>1</sup> <br /><br />SQL Server 2025 and later versions: No<br /><br />Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric: Built in |
| **Persistent data connectors** | `CREATE EXTERNAL TABLE` with `sqlserver://`, `oracle://`, `teradata://`, etc. | You need recurring access, governance, statistics, and pushdown computation for production | SQL authentication only | Yes |

<sup>1</sup> For cloud file access in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you must install the PolyBase feature, but the Azure Blob Storage, ADLS Gen2, and S3-compatible storage connectors don't depend on PolyBase services. [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions have native support for CSV, Parquet, and Delta without installing or running PolyBase services.

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- OLE DB ad hoc queries use `OPENROWSET(provider, connection, query)` for quick one-time access to a remote data source without creating persistent objects. They can use SQL authentication, Windows authentication, or Microsoft Entra ID with MSOLEDBSQL. This scenario doesn't require PolyBase installation.
- Ad hoc queries on files use `OPENROWSET(BULK ...)` to explore file data quickly or test a schema before creating a table. They can use SAS tokens, access keys, Managed Identity, or Microsoft Entra ID. SQL Server 2022 requires the PolyBase feature installation for cloud files, but it doesn't require PolyBase services. SQL Server 2025 and later versions don't require PolyBase for cloud files. File ad hoc queries are built in to Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.
- Persistent data connectors use `CREATE EXTERNAL TABLE` with `sqlserver://`, `oracle://`, `teradata://`, and similar locations for recurring access, governance, statistics, and pushdown computation in production workloads. They require SQL authentication and PolyBase services.



### Decision guide

| Scenario | Recommendation |
| --- | --- |
| You need Microsoft Entra ID authentication for remote SQL, or want to avoid PolyBase services. | Use `OPENROWSET(MSOLEDBSQL, ...)` (ad hoc, no persistent objects). |
| You need persistent tables, statistics, or pushdown computation to remote databases. | Use `CREATE EXTERNAL TABLE` with PolyBase connectors (`sqlserver://`, `oracle://`, `teradata://`, `mongodb://`, `odbc://`). `OPENROWSET` doesn't support connectors. |
| You're exploring a new file or testing a schema. | Use `OPENROWSET(BULK ...)` (fast iteration, no persistent objects). |
| You're ingesting file data into a table with transformations. | Use `INSERT ... SELECT` from `OPENROWSET(BULK ...)`. |
| You need governance or shared access for many users or applications. | Use `CREATE EXTERNAL TABLE` so permissions and metadata are centralized. |
| You're working in SQL database in Fabric. | Use `OPENROWSET(BULK ...)` for ad hoc OneLake queries or external tables for reusable access; for external storage use OneLake shortcuts. |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- If you need Microsoft Entra ID authentication for remote SQL or want to avoid PolyBase services, use `OPENROWSET(MSOLEDBSQL, ...)` for ad hoc remote queries without persistent objects.
- If you need persistent tables, statistics, or pushdown computation to remote databases, use `CREATE EXTERNAL TABLE` with PolyBase connectors such as `sqlserver://`, `oracle://`, `teradata://`, `mongodb://`, and `odbc://`. `OPENROWSET` doesn't support these connectors.
- If you're exploring a new file or testing a schema, use `OPENROWSET(BULK ...)` for fast iteration and no persistent objects.
- If you're ingesting file data into a table with transformations, use `INSERT ... SELECT` from `OPENROWSET(BULK ...)`.
- If you need governance or shared access for many users or applications, use `CREATE EXTERNAL TABLE` so permissions and metadata are centralized.
- If you're working in SQL database in Fabric, use `OPENROWSET(BULK ...)` for ad hoc OneLake queries or external tables for reusable access, and use OneLake shortcuts for external storage.

## Choose your scenario

Now that you understand the three approaches, use one of the following guides to implement your specific use case.

### Query files (Parquet, CSV, or Delta)

If your data is in Parquet, CSV, or Delta files on Azure Blob Storage, ADLS Gen2, S3-compatible storage, or OneLake, follow one of these guides:

| Scenario | Recommended guide | Platforms |
| --- | --- | --- |
| **Quick ad hoc query** on a Parquet or CSV file | Use `OPENROWSET`. No external table needed | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric |
| **Repeated queries** on Parquet files with a persistent schema | Create an external table over Parquet | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric |
| **Query CSV files** with an external table | Create an external table with a file format for delimited text | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric |
| **Query Delta Lake tables** | Create an external table with `FILE_FORMAT = DeltaLakeFileFormat` | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions |
| **Export query results** to Parquet or CSV files (CETAS) | Use `CREATE EXTERNAL TABLE AS SELECT` | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Managed Instance |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- For a quick ad hoc query on a Parquet or CSV file, use `OPENROWSET`. This method doesn't require an external table. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric support this pattern.
- For repeated queries on Parquet files with a persistent schema, use an external table over Parquet. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric support this pattern.
- For a query on CSV files, use an external table with a file format for delimited text. [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric support this pattern.
- For a query on Delta Lake tables, use an external table with `FILE_FORMAT = DeltaLakeFileFormat`. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions support this pattern.
- For exporting query results to Parquet or CSV files, use `CREATE EXTERNAL TABLE AS SELECT`. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions and Azure SQL Managed Instance support this pattern.

You can also follow one of these step-by-step tutorials:

| Tutorial | Description |
| --- | --- |
| [Get started with PolyBase in SQL Server 2022](polybase-get-started.md) | Covers `OPENROWSET` with Parquet and CSV, external tables, and folder navigation. |
| [Virtualize parquet file in a S3-compatible object storage with PolyBase](polybase-virtualize-parquet-file.md) | Tutorial for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. |
| [Virtualize CSV file with PolyBase](virtualize-csv.md) | Tutorial for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. |
| [Virtualize delta table with PolyBase](virtualize-delta.md) | Tutorial for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. |
| [Data virtualization with Azure SQL Database (Preview)](/azure/azure-sql/database/data-virtualization-overview) | Azure SQL Database guide for Parquet and CSV. |
| [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview) | Azure SQL Managed Instance guide for Parquet, CSV, and CETAS. |
| [Data virtualization in SQL database in Fabric](/fabric/database/sql/data-virtualization) | SQL database in Fabric guide for OneLake files. |

### Connect to another SQL Server instance, Azure SQL Database, or SQL Managed Instance

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, PolyBase can query tables in another SQL Server instance, Azure SQL Database, or Azure SQL Managed Instance, without using linked servers.

> [!IMPORTANT]  
> The `sqlserver://` connector isn't supported in SQL database in Fabric. PolyBase RDBMS connectors use SQL authentication through `CREATE DATABASE SCOPED CREDENTIAL` and don't support Microsoft Entra ID, Managed Identity, or service principal authentication. Because SQL database in Fabric requires Microsoft Entra authentication, you can't connect to it using PolyBase.

| Step | What to do |
| --- | --- |
| 1. Install PolyBase | [Install PolyBase on Windows](polybase-installation.md) or [Install PolyBase on Linux](polybase-linux-setup.md) |
| 2. Create a credential | `CREATE DATABASE SCOPED CREDENTIAL` with the target login |
| 3. Create an external data source | `CREATE EXTERNAL DATA SOURCE ... WITH (LOCATION = 'sqlserver://<server>')` |
| 4. Create an external table | `CREATE EXTERNAL TABLE ... WITH (LOCATION = '<db>.<schema>.<table>')` |
| 5. Query | `SELECT * FROM <external_table>` |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- 1. Install PolyBase on Windows or Linux by using [Install PolyBase on Windows](polybase-installation.md) or [Install PolyBase on Linux](polybase-linux-setup.md) before you configure access to external SQL data.
- 2. Create a database-scoped credential with the target login by using `CREATE DATABASE SCOPED CREDENTIAL` so the engine can authenticate to the remote server.
- 3. Create an external data source for the remote SQL Server by using `CREATE EXTERNAL DATA SOURCE ... WITH (LOCATION = 'sqlserver://<server>')`.
- 4. Create an external table by using `CREATE EXTERNAL TABLE ... WITH (LOCATION = '<db>.<schema>.<table>')` to represent the remote table.
- 5. Query the external table by using `SELECT * FROM <external_table>`.

> [!TIP]  
> The SQL Server connector (`sqlserver://`) also works for Azure SQL Database and Azure SQL Managed Instance. Use the same steps, and set `LOCATION` to the Azure SQL Database or Azure SQL Managed Instance endpoint (for example, `sqlserver://myserver.database.windows.net`).

For a detailed guide, see [Configure PolyBase to access external data in SQL Server](polybase-configure-sql-server.md).

### Connect to Oracle, Teradata, or MongoDB

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions can query Oracle, Teradata, MongoDB, and Cosmos DB through PolyBase ODBC connectors.

| Data source | Guide | Requirements |
| --- | --- | --- |
| Oracle | [Configure PolyBase to access external data in Oracle](polybase-configure-oracle.md) | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, Oracle client drivers |
| Teradata | [Configure PolyBase to access external data in Teradata](polybase-configure-teradata.md) | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, Teradata ODBC driver |
| MongoDB / Cosmos DB | [Configure PolyBase to access external data in MongoDB](polybase-configure-mongodb.md) | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, MongoDB ODBC driver |
| Any ODBC source | [Configure PolyBase to access external data with ODBC generic types](polybase-configure-odbc-generic.md) | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions (Windows)<br /><br />(Linux beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]) |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- Oracle data sources use the [Configure PolyBase to access external data in Oracle](polybase-configure-oracle.md) guide and require [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions and Oracle client drivers.
- Teradata data sources use the [Configure PolyBase to access external data in Teradata](polybase-configure-teradata.md) guide and require [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions and the Teradata ODBC driver.
- MongoDB or Cosmos DB data sources use the [Configure PolyBase to access external data in MongoDB](polybase-configure-mongodb.md) guide and require [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions and the MongoDB ODBC driver.
- Any ODBC source can use the [Configure PolyBase to access external data with ODBC generic types](polybase-configure-odbc-generic.md) guide and requires [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions on Windows or [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions on Linux.

### Connect to Azure Blob Storage or ADLS Gen2

| SQL platform | Authentication options | Guide |
| --- | --- | --- |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | SAS token, access key, Managed Identity (beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]) | [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md) |
| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | Access key (via Hadoop connector) | [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md) |
| Azure SQL Database | SAS token, Managed Identity, Microsoft Entra pass-through | [Data virtualization with Azure SQL Database (Preview)](/azure/azure-sql/database/data-virtualization-overview) |
| Azure SQL Managed Instance | SAS token, Managed Identity | [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview) |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- SQL Server 2022 and later versions support Azure Blob Storage or ADLS Gen2 authentication by using SAS tokens, access keys, or Managed Identity beginning with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. For more information, see [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md).
- SQL Server 2019 supports Azure Blob Storage or ADLS Gen2 by using an access key through the Hadoop connector. For more information, see [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md).
- Azure SQL Database supports Azure Blob Storage or ADLS Gen2 by using SAS tokens, Managed Identity, or Microsoft Entra pass-through authentication. For more information, see [Data virtualization with Azure SQL Database (Preview)](/azure/azure-sql/database/data-virtualization-overview).
- Azure SQL Managed Instance supports Azure Blob Storage or ADLS Gen2 by using SAS tokens or Managed Identity. For more information, see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview).

In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the URI prefixes changed. When migrating from [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] or earlier versions:

- **Azure Blob Storage**: Change `wasb[s]://` to `abs://`
- **ADLS Gen2**: Change `abfs[s]://` to `adls://`

For more information, see [Configure PolyBase to access external data in Azure Blob Storage](polybase-configure-azure-blob-storage.md).

### Connect to S3-compatible object storage

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions support S3-compatible storage, such as Amazon S3, MinIO, and Ceph.

For more information, see [Configure PolyBase to access external data in S3-compatible object storage](polybase-configure-s3-compatible.md).

### Export data with CREATE EXTERNAL TABLE AS SELECT (CETAS)

CETAS exports query results to external files (Parquet or CSV) in Azure Blob Storage, ADLS Gen2, or S3-compatible storage.

| SQL platform | Supported | Export formats | Notes |
| --- | --- | --- | --- |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. Export to ADLS Gen2 with CETAS requires SQL Server 2022 CU5 or later version. | Yes | Parquet, CSV | Requires [Server configuration: allow polybase export](../../database-engine/configure-windows/allow-polybase-export.md). |
| Azure SQL Managed Instance | Yes | Parquet, CSV | [Disabled by default](/azure/azure-sql/managed-instance/data-virtualization-overview#cetas) |
| Azure SQL Database | No | None | Not available |
| SQL database in Fabric | No | None | Not available |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- SQL Server 2022 and later versions support CETAS and export Parquet and CSV files. The [Server configuration: allow polybase export](../../database-engine/configure-windows/allow-polybase-export.md) setting is required. CETAS export to ADLS Gen2 isn't available in SQL Server 2022 releases before CU5.
- Azure SQL Managed Instance supports CETAS and exports Parquet and CSV files. The [Disabled by default](/azure/azure-sql/managed-instance/data-virtualization-overview#cetas) guidance describes its default state.
- Azure SQL Database doesn't support CETAS.
- SQL database in Fabric doesn't support CETAS.

For the Transact-SQL reference, see [CREATE EXTERNAL TABLE AS SELECT (CETAS)](../../t-sql/statements/create-external-table-as-select-transact-sql.md).

## Quick start examples

### Example 1: Ad hoc query on a Parquet file (OPENROWSET)

No external table needed. Works on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

```sql
SELECT TOP 10 *
FROM OPENROWSET (
    BULK 'abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*.parquet',
    FORMAT = 'PARQUET'
) AS [result];
```

### Example 2: External table over CSV in Azure Blob Storage

This example works on all SQL platforms that support external tables over CSV files.

- **Step 1**: Create a database master key (DMK). This step is required because the credential stores a SAS token secret. However, you can skip this step if you use Managed Identity or Microsoft Entra authentication.

  ```sql
  CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
  ```

- **Step 2**: Create a credential with a SAS token. Omit the leading `?`.

  ```sql
  CREATE DATABASE SCOPED CREDENTIAL MyStorageCred
  WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
       SECRET = '<your_SAS_token>'; -- omit the leading '?'
  ```
  
- **Step 3**: Create an external data source.

  ```sql
  CREATE EXTERNAL DATA SOURCE MyAzureStorage
  WITH (
      LOCATION = 'abs://mycontainer@mystorageaccount.blob.core.windows.net',
      CREDENTIAL = MyStorageCred
  );
  ```
  
- **Step 4**: Create a file format for the CSV.

  ```sql
  CREATE EXTERNAL FILE FORMAT CsvFormat
  WITH (
      FORMAT_TYPE = DELIMITEDTEXT,
      FORMAT_OPTIONS (
          FIELD_TERMINATOR = ',',
          STRING_DELIMITER = '"',
          FIRST_ROW = 2
      )
  );
  ```

- **Step 5**: Create the external table.

  ```sql
  CREATE EXTERNAL TABLE dbo.SalesExternal
  (
      OrderId INT,
      OrderDate DATE,
      Amount DECIMAL (18, 2),
      Customer NVARCHAR (100)
  )
  WITH (
      DATA_SOURCE = MyAzureStorage,
      LOCATION = '/data/sales/',
      FILE_FORMAT = CsvFormat
  );
  ```

- **Step 6**: Query the external table.

  ```sql
  SELECT *
  FROM dbo.SalesExternal
  WHERE OrderDate >= '2025-01-01';
  ```

### Example 3: Query a table in another SQL Server

This example works on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

- **Step 1**: Create a database master key (required because the credential stores a password).

  ```sql
  CREATE MASTER KEY ENCRYPTION
  BY PASSWORD = '<password>';
  ```

- **Step 2**: Create a credential for the remote SQL Server instance.

  ```sql
  CREATE DATABASE SCOPED CREDENTIAL RemoteSqlCred
  WITH IDENTITY = 'remote_user',
       SECRET = '<password>';
  ```
  
- **Step 3**: Create the external data source.

  ```sql
  CREATE EXTERNAL DATA SOURCE RemoteSqlServer
  WITH (
      LOCATION = 'sqlserver://remote-server.contoso.com',
      PUSHDOWN = ON,
      CREDENTIAL = RemoteSqlCred
  );
  ```
  
- **Step 4**: Create the external table (three-part name in `LOCATION`).

  ```sql
  CREATE EXTERNAL TABLE dbo.RemoteCustomers
  (
      CustomerId INT,
      CustomerName NVARCHAR (200)
          COLLATE SQL_Latin1_General_CP1_CI_AS
  )
  WITH (
      DATA_SOURCE = RemoteSqlServer,
      LOCATION = 'SalesDB.dbo.Customers'
  );
  ```
  
- **Step 5**: Query across servers.

  ```sql
  SELECT c.CustomerName,
         s.Amount
  FROM dbo.RemoteCustomers AS c
       INNER JOIN dbo.LocalSales AS s
           ON c.CustomerId = s.CustomerId;
  ```

### Example 4: Export results to Parquet with CETAS

Works on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Managed Instance.

- **Step 1**: Enable CETAS (SQL Server only).

  ```sql
  EXECUTE sp_configure 'allow polybase export', 1;
  RECONFIGURE;
  ```

- **Step 2**: Create credential and data source (reuse from earlier examples).
  
- **Step 3**: Create a file format for Parquet export.

  ```sql
  CREATE EXTERNAL FILE FORMAT ParquetFormat
  WITH (
      FORMAT_TYPE = PARQUET
  );
  ```
  
- **Step 4**: Export query results.

  ```sql
  CREATE EXTERNAL TABLE dbo.Sales2025Export
  WITH (
      DATA_SOURCE = MyAzureStorage,
      LOCATION = '/exports/sales_2025.parquet',
      FILE_FORMAT = ParquetFormat
  ) AS
  SELECT *
  FROM Sales.Orders
  WHERE OrderDate >= '2025-01-01';
  ```

## T-SQL building blocks for PolyBase

Before implementing any scenario, understand the core T-SQL objects that PolyBase uses and how they fit together:

:::image type="complex" source="media/data-virtualization-guide/polybase-transact-sql-objects.png" alt-text="Diagram showing PolyBase Transact-SQL objects and their relationships.":::
Diagram showing PolyBase T-SQL objects and their relationships, from authentication (database master key, credentials) through data sources and file formats to query methods (External Table, OPENROWSET, BULK INSERT, CETAS).
:::image-end:::

- For external data source syntax, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).
- For external file format syntax, see [CREATE EXTERNAL FILE FORMAT](../../t-sql/statements/create-external-file-format-transact-sql.md).
- For external table syntax, see [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).
- For ad hoc data access syntax, see [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md).
- For CETAS syntax, see [CREATE EXTERNAL TABLE AS SELECT (CETAS)](../../t-sql/statements/create-external-table-as-select-transact-sql.md).

For a full Transact-SQL reference for all objects, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).

> [!IMPORTANT]  
> **Check the data type mapping for your external file format.** When you create an external file format, or query files using `OPENROWSET`, PolyBase automatically maps source data types (Parquet, CSV, Delta, Oracle, Teradata, MongoDB) to SQL Server data types. Mismatched types can cause silent truncation, precision loss, or query errors. For example, a Parquet `DECIMAL(38,18)` maps to `DECIMAL(18,0)`. Review the mapping tables before you define external table columns or a `WITH` clause. For the complete reference, see [Type mapping with PolyBase](polybase-type-mapping.md).

### When do you need CREATE MASTER KEY?

A database master key (DMK) is created using `CREATE MASTER KEY` syntax. The DMK encrypts the secrets stored inside database scoped credentials. It's required only when the credential contains a secret value, that is, when it stores a password, token, or access key.

- **DMK is required** (credential stores a secret):

  | Authentication type | `IDENTITY` value | Has secret | DMK |
  | --- | --- | :---: | :---: |
  | SAS token | `'SHARED ACCESS SIGNATURE'` | Yes | Required |
  | S3 access key | `'S3 ACCESS KEY'` | Yes | Required |
  | SQL login / basic authentication | `'<username>'` | Yes | Required |
  | Storage account access key | `'<storage_account_name>'` | Yes | Required |

  <!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

  - A SAS token credential uses `IDENTITY = 'SHARED ACCESS SIGNATURE'` and stores a secret value, so it requires a database master key.
  - An S3 access key credential uses `IDENTITY = 'S3 ACCESS KEY'` and stores a secret value, so it requires a database master key.
    - A SQL login or basic authentication credential uses `IDENTITY = '<username>'` and stores a secret value, so it requires a database master key.
  - A storage account access key credential uses `IDENTITY = '<storage_account_name>'` and stores a secret value, so it requires a database master key.

- **DMK is not required** (no secret stored):

  | Authentication type | `IDENTITY` value | Has secret | DMK |
  | --- | --- | :---: | :---: |
  | Managed Identity | `'Managed Identity'` | No | Not required |
  | Microsoft Entra ID | `'User Identity'` or `'Managed Identity'` | No | Not required |

  <!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

  - A Managed Identity credential uses `IDENTITY = 'Managed Identity'` and stores no secret, so it doesn't require a database master key.
  - A Microsoft Entra ID credential uses `IDENTITY = 'User Identity'` or `IDENTITY = 'Managed Identity'` and stores no secret, so it doesn't require a database master key.

> [!TIP]  
> If your `CREATE DATABASE SCOPED CREDENTIAL` statement doesn't include a secret, you don't need a DMK. Managed Identity and Microsoft Entra ID authentication delegate trust to the platform. The database doesn't store passwords or tokens.

**Examples**:

In this example query, the DMK is required (Credential stores a SAS token).

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';

CREATE DATABASE SCOPED CREDENTIAL SasCred
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
     SECRET = '<your_SAS_token>';
```

In this example query, the DMK isn't required (Managed Identity, no secret).

```sql
CREATE DATABASE SCOPED CREDENTIAL ManagedIdentityCred
WITH IDENTITY = 'Managed Identity';
```

In this example query, the DMK isn't required (Microsoft Entra pass-through, no secret).

```sql
CREATE DATABASE SCOPED CREDENTIAL EntraIdCred
WITH IDENTITY = 'User Identity';
```

### Remote data access with OPENROWSET and external tables

SQL Server offers three distinct approaches to query remote data. You can choose the right approach when you understand the differences in syntax, authentication, and architecture.

| Approach | Syntax | Connects to | Authentication | PolyBase services | Platforms |
| --- | --- | --- | --- | --- | --- |
| **OLE DB queries** | `OPENROWSET(provider, connection, query)` | Any OLE DB source via MSOLEDBSQL, SQLOLEDB, or other providers | SQL authentication, Windows authentication, Microsoft Entra ID (MSOLEDBSQL) | No | SQL Server (all supported versions) |
| **File queries in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]** | `OPENROWSET(BULK ...)` | Files on local disk, network, or cloud (Azure Blob, ADLS, S3, OneLake) | SAS token, access key, Managed Identity, Microsoft Entra ID | Yes for cloud <sup>1</sup>; No for local | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] |
| **File queries in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions** | `OPENROWSET(BULK ...)` | Files on local disk, network, or cloud (Azure Blob, ADLS, S3, OneLake) | SAS token, access key, Managed Identity, Microsoft Entra ID | No | [!INCLUDE [sssql25-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric |
| **PolyBase connectors** | `CREATE EXTERNAL TABLE` with `CREATE EXTERNAL DATA SOURCE` using `sqlserver://`, `oracle://`, `teradata://`, `mongodb://`, `odbc://` | Remote SQL Server, Oracle, Teradata, MongoDB, ODBC sources | SQL authentication only | Yes | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions (Windows); [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions (Linux) |

<sup>1</sup> For cloud file access in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the PolyBase feature must be installed.

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- OLE DB queries use `OPENROWSET(provider, connection, query)` to connect to any OLE DB source through MSOLEDBSQL, SQLOLEDB, or another provider on all supported versions of SQL Server. They support SQL authentication, Windows authentication, and Microsoft Entra ID with MSOLEDBSQL, and they don't require PolyBase services to be installed.
- File queries use `OPENROWSET(BULK ...)` to read files on local disk, network shares, or cloud storage such as Azure Blob Storage, ADLS, S3, or OneLake by using a SAS token, access key, Managed Identity, or Microsoft Entra ID. They're supported for local and network files in SQL Server 2005 and later versions, for cloud files in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and in Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric. File queries don't require PolyBase services for local files or for cloud files in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.
- PolyBase connectors use `CREATE EXTERNAL TABLE` with `CREATE EXTERNAL DATA SOURCE` and the `sqlserver://`, `oracle://`, `teradata://`, `mongodb://`, or `odbc://` location to connect to remote SQL Server, Oracle, Teradata, MongoDB, or ODBC sources. They require SQL authentication and PolyBase services in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions on Windows and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions on Linux. For more information, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).


#### When to use each approach

Use **OLE DB OPENROWSET** for:

- Use OLE DB `OPENROWSET` for quick, one-time ad hoc queries without creating persistent objects.
- Use OLE DB `OPENROWSET` for Microsoft Entra ID or Managed Identity authentication through MSOLEDBSQL.
- Use OLE DB `OPENROWSET` to avoid PolyBase service dependencies.
- Use OLE DB `OPENROWSET` to connect to any data source that has an OLE DB provider.

Use **File OPENROWSET(BULK)** for:

- Use file `OPENROWSET(BULK ...)` for ad hoc file exploration and schema discovery.
- Use file `OPENROWSET(BULK ...)` for quick transformations and previews before you commit to a table definition.
- Use file `OPENROWSET(BULK ...)` for flexible inline column transformations such as casting, filtering, and computed columns.
- Use file `OPENROWSET(BULK ...)` for data that doesn't change frequently and doesn't need persistent metadata.

Use **PolyBase connectors with CREATE EXTERNAL TABLE** for:

- Use PolyBase connectors with `CREATE EXTERNAL TABLE` for persistent, reusable table definitions that multiple users or applications access.
- Use PolyBase connectors with `CREATE EXTERNAL TABLE` for production workloads that require statistics and query plan optimization.
- Use PolyBase connectors with `CREATE EXTERNAL TABLE` for pushdown computation to remote sources such as Oracle and SQL Server.
- Use PolyBase connectors with `CREATE EXTERNAL TABLE` for shared governance and security; after the table is created, users need only `SELECT` permission.
- Use PolyBase connectors with `CREATE EXTERNAL TABLE` when SQL authentication is available to the remote source.

#### OPENROWSET (OLE DB) - ad hoc remote queries (no PolyBase services required)

The OLE DB form of `OPENROWSET` connects to a remote data source through an OLE DB provider, executes a pass-through query, and returns the results as a rowset. It's a one-time, ad hoc alternative to a linked server. No persistent metadata is created. This syntax doesn't require PolyBase services, and doesn't support cloud files or external data sources.

This example query connects to a remote SQL Server via OLE DB (not PolyBase).

```sql
SELECT *
FROM OPENROWSET (
    'MSOLEDBSQL',
    'Server=remote-server;Database=AdventureWorks;Trusted_Connection=yes;',
    'SELECT TOP 10 * FROM AdventureWorks.Sales.SalesOrderHeader'
);
```

#### OPENROWSET(BULK) - file-based queries (PolyBase)

The `BULK` form of `OPENROWSET` reads data directly from files. On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, it reads from local or UNC file paths and requires a format file. In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you can read from *cloud storage* using the `DATA_SOURCE` and `FORMAT` parameters. This approach is the PolyBase-integrated version used for data virtualization.

In the context of PolyBase and data virtualization, when this guide refers to `OPENROWSET` it means the `OPENROWSET(BULK ...)` syntax with a `FORMAT` clause for querying external files.

**Examples**:

This example query reads a Parquet file from Azure Blob Storage (SQL Server 2022 and later versions).

```sql
SELECT TOP 10 *
FROM OPENROWSET (
    BULK 'data/sales/*.parquet',
    DATA_SOURCE = 'MyAzureStorage',
    FORMAT = 'PARQUET'
) AS [result];
```

This example query reads a Parquet file with an inline path (Azure SQL Database, Azure SQL Managed Instance).

```sql
SELECT TOP 10 *
FROM OPENROWSET (
    BULK 'abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*.parquet',
    FORMAT = 'PARQUET'
) AS [result];
```

### When to use OPENROWSET vs. external tables

Both `OPENROWSET(BULK ...)` and external tables let you query external data with T-SQL, but they're designed for different use cases. The following table summarizes the key differences to help you decide which approach fits your scenario.

| Capability | `OPENROWSET(BULK ...)` | External table |
| --- | --- | --- |
| **Purpose** | Ad hoc exploration and one-off queries | Persistent, reusable table definition |
| **Metadata stored in database** | No. Nothing is saved after the query runs | Yes. The table definition, data source, and file format are stored as database objects |
| **Schema definition** | Inferred automatically from the file (Parquet) or specified inline with a `WITH` clause | Defined explicitly in the `CREATE EXTERNAL TABLE` statement |
| **Permissions** | Requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` | Once created, standard `SELECT` permission on the table is sufficient |
| **Computed columns** | Yes. Add expressions and computed columns in the `SELECT` list; metadata functions like `filename()` and `filepath()` are only available here. | No. Fixed column list; perform transformations in a view or in the query that reads the external table |
| **Statistics** | Azure SQL Managed Instance: manual single-column stats via [sys.sp_create_openrowset_statistics](../system-stored-procedures/sp-create-openrowset-statistics.md?view=azuresqldb-mi-current&preserve-view=true). See [OPENROWSET manual statistics](/azure/azure-sql/managed-instance/data-virtualization-overview#openrowset-manual-statistics).<br><br> [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, SQL database in Fabric: autocreate stats on predicates. Manual `OPENROWSET` statistics aren't supported on SQL Server. | Full `CREATE STATISTICS` support on all platforms, plus autocreate in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. See [Create external table manual statistics](polybase-performance.md#create-external-table-manual-statistics). |
| **Pushdown** | Limited support. The engine might push filters down to the file scan but there's no pushdown to remote RDBMS sources | Yes. Supports pushdown computation for RDBMS connectors (SQL Server, Oracle, Teradata, MongoDB) |
| **Best for** | Data exploration, schema discovery, prototyping queries, one-time data loads, flexible transformations | Production workloads, repeated queries, shared access across users, dashboards, and reporting |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- `OPENROWSET(BULK ...)` is best for ad hoc exploration and one-off queries, while an external table is better for persistent, reusable table definitions.
- `OPENROWSET(BULK ...)` does not store metadata in the database after the query runs, while an external table stores the table definition, data source, and file format as database objects.
- `OPENROWSET(BULK ...)` infers the schema automatically from a Parquet file or defines the schema inline with a `WITH` clause, while an external table defines the schema explicitly in the `CREATE EXTERNAL TABLE` statement.
- `OPENROWSET(BULK ...)` requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS`, while an external table can be queried by users who only need standard `SELECT` permission once the table exists.
- `OPENROWSET(BULK ...)` supports computed columns in the query and metadata functions like `filename()` and `filepath()`, while an external table has a fixed column list and requires transformations in a view or in the query that reads the external table.
- `OPENROWSET(BULK ...)` has limited statistics support: Azure SQL Managed Instance can use `sys.sp_create_openrowset_statistics` for single-column statistics, but [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, and SQL database in Fabric automatically create statistics on predicates. Manual `OPENROWSET` statistics aren't supported on SQL Server, Azure SQL Database, and SQL database in Fabric. An external table supports full `CREATE STATISTICS` capability on all platforms plus automatic statistics in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, and SQL database in Fabric. See [OPENROWSET manual statistics](/azure/azure-sql/managed-instance/data-virtualization-overview#openrowset-manual-statistics) and [Create external table manual statistics](polybase-performance.md#create-external-table-manual-statistics).
- `OPENROWSET(BULK ...)` has limited pushdown and no pushdown to remote RDBMS sources, while an external table supports pushdown computation for RDBMS connectors.
- `OPENROWSET(BULK ...)` is best for data exploration, schema discovery, prototyping, one-time loads, and flexible transformations, while an external table is best for production workloads, repeated queries, shared access, dashboards, and reporting.

#### Use OPENROWSET when you need flexibility

Use `OPENROWSET` to explore a file, test different schemas, or add computed columns and transformations without creating any persistent objects. For example, you can extract the file path as a column, cast data types inline, or filter on computed expressions in a single query.

This example query includes computed columns and transformations:

```sql
SELECT result.filename() AS [FileName],
       result.filepath(1) AS [Year],
       result.filepath(2) AS [Month],
       CAST (OrderDate AS DATE) AS OrderDate,
       Amount,
       OrderDate
FROM OPENROWSET (
    BULK 'abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*/*/*/*.parquet',
    FORMAT = 'PARQUET'
) AS result
WHERE result.filepath(1) = '2025';
```

> [!TIP]  
> The `filepath()` and `filename()` functions are available in Azure SQL Database, Azure SQL Managed Instance, and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. They let you filter on parts of the file path (partition elimination) and expose the source file name as a column, which isn't directly possible with external tables.

#### Use external tables when you need persistence and governance

Use external tables when multiple users or applications need to query the same external data repeatedly. You define the schema, data source, and credentials once and store them in the database. Consumers only need `SELECT` permission on the table.

External tables also support *statistics*, which the query optimizer uses to build better execution plans. You can create statistics manually or let the engine create them automatically ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions).

This example query creates statistics on an external table for better query plans.

```sql
CREATE STATISTICS Stats_OrderDate
ON dbo.SalesExternal(OrderDate)
WITH FULLSCAN;
```

For more information on statistics for both approaches, see [PolyBase performance considerations - Statistics](polybase-performance.md#statistics).

### BULK INSERT vs. OPENROWSET(BULK): Which one should I use?

Both `BULK INSERT` and `OPENROWSET(BULK ...)` import data from files into SQL Server by using the same underlying bulk-load engine. However, they differ in syntax, flexibility, and what you can do with the results. The following table summarizes the key differences:

> [!NOTE]  
> The standalone `BULK INSERT` statement isn't supported in SQL database in Fabric. To ingest data, use `INSERT ... SELECT` with `OPENROWSET(BULK ...)` against OneLake.

| Capability | `BULK INSERT` | `OPENROWSET(BULK ...)` |
| --- | --- | --- |
| **Basic purpose** | Loads data from a file directly into a *target table* | Returns a *rowset* that you use in a `SELECT` or `INSERT ... SELECT` statement |
| **Usage pattern** | Standalone statement: `BULK INSERT <table> FROM '<file>'` | Must be used inside a query: `SELECT * FROM OPENROWSET(BULK ...)` or `INSERT INTO <table> SELECT * FROM OPENROWSET(BULK ...)` |
| **Requires a target table?** | Yes. Always writes directly to a table | No. You can `SELECT` from it without inserting anywhere, or insert into any table or temp table |
| **Column transformations during load** | Limited support. The data flows from file to table as-is (mapping controlled by format file or column order) | Full support. You can add expressions, `CAST`, `WHERE` filters, `JOIN` other tables, and computed columns in the surrounding `SELECT` |
| **Table hints** | The `WITH` clause includes support for `BATCHSIZE`, `CHECK_CONSTRAINTS`, `FIRE_TRIGGERS`, `KEEPIDENTITY`, `KEEPNULLS`, `TABLOCK`, and more | Supports table hints via the `INSERT ... SELECT * FROM OPENROWSET(BULK ...) WITH (TABLOCK, IGNORE_CONSTRAINTS, ...)` syntax |
| **Large-object (LOB) single-value import** | Not supported | Yes. Supports `SINGLE_BLOB`, `SINGLE_CLOB`, `SINGLE_NCLOB` to import an entire file as one **varbinary(max)**, **varchar(max)**, or **nvarchar(max)** value |
| **Format files** | Yes. Supported via (XML and non-XML) | Yes. Supported (XML and non-XML) |
| **Cloud file access** | `DATA_SOURCE` supports Azure Blob Storage in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance. SQL Server 2019 CU11 and later updates also support ADLS Gen2. S3-compatible storage isn't supported. | `DATA_SOURCE` supports Azure Blob Storage in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, ADLS Gen2 in SQL Server 2019 CU11 and later versions, and S3-compatible storage in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. Azure SQL Database and Azure SQL Managed Instance support Azure Blob Storage and ADLS Gen2. SQL database in Fabric supports OneLake and external storage through OneLake shortcuts. |
| **Parquet or Delta files** | Not supported. Only CSV/delimited text | Yes. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Managed Instance, and Azure SQL Database support `FORMAT = 'PARQUET'` and `FORMAT = 'DELTA'`; SQL database in Fabric support `FORMAT = 'PARQUET'` but not `DELTA`. For more information, see [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md). |
| **Permission required** | `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS`, plus `INSERT` on the target table | `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` |
| **Minimal logging** | Yes. Supported under simple or bulk-logged recovery models with `TABLOCK` | Yes. Supported when used with `INSERT ... SELECT` and `TABLOCK` |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- `BULK INSERT` loads data from a file directly into a target table, while `OPENROWSET(BULK ...)` returns a rowset that you can use in a `SELECT` or `INSERT ... SELECT` statement.
- `BULK INSERT` is a standalone statement, while `OPENROWSET(BULK ...)` must be used inside a query such as `SELECT * FROM OPENROWSET(BULK ...)` or `INSERT INTO <table> SELECT * FROM OPENROWSET(BULK ...)`.
- `BULK INSERT` always writes directly to a target table, while `OPENROWSET(BULK ...)` can `SELECT` from a file without inserting anywhere or insert into any table or temp table.
- `BULK INSERT` has limited support for column transformations because data flows from the file to the table as-is, with mapping controlled by a format file or column order. `OPENROWSET(BULK ...)` supports expressions, `CAST`, `WHERE` filters, `JOIN`s, and computed columns in the surrounding `SELECT`.
- `BULK INSERT` uses a `WITH` clause for `BATCHSIZE`, `CHECK_CONSTRAINTS`, `FIRE_TRIGGERS`, `KEEPIDENTITY`, `KEEPNULLS`, `TABLOCK`, and other hints. `OPENROWSET(BULK ...)` supports table hints through `INSERT ... SELECT * FROM OPENROWSET(BULK ...) WITH (TABLOCK, IGNORE_CONSTRAINTS, ...)`.
- `BULK INSERT` doesn't support large-object single-value import. `OPENROWSET(BULK ...)` supports `SINGLE_BLOB`, `SINGLE_CLOB`, and `SINGLE_NCLOB` to import an entire file as one **varbinary(max)**, **varchar(max)**, or **nvarchar(max)** value, respectively.
- Both `BULK INSERT` and `OPENROWSET(BULK ...)` support XML and non-XML format files.
- For cloud file access with `BULK INSERT`, the `DATA_SOURCE` parameter supports Azure Blob Storage in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance. SQL Server 2019 CU11 and later updates also support ADLS Gen2, but `BULK INSERT` doesn't support S3-compatible storage. For `OPENROWSET(BULK ...)`, `DATA_SOURCE` supports Azure Blob Storage in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, ADLS Gen2 in SQL Server 2019 CU11 and later versions, and S3-compatible storage in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. Azure SQL Database and Azure SQL Managed Instance support Azure Blob Storage and ADLS Gen2. SQL database in Fabric supports OneLake and external storage through OneLake shortcuts.
- `BULK INSERT` doesn't support Parquet or Delta files and supports only CSV or delimited text. `OPENROWSET(BULK ...)` supports `FORMAT = 'PARQUET'` and `FORMAT = 'DELTA'` in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance. SQL database in Fabric supports `FORMAT = 'PARQUET'` but not `FORMAT = 'DELTA'`. For more information, see [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md).
- `BULK INSERT` requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` plus `INSERT` permission on the target table, while `OPENROWSET(BULK ...)` requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS`.
- `BULK INSERT` supports minimal logging under the simple or bulk-logged recovery model with `TABLOCK`. `OPENROWSET(BULK ...)` supports minimal logging when it's used with `INSERT ... SELECT` and `TABLOCK`.

#### When to choose BULK INSERT

Use `BULK INSERT` when you have a straightforward file-to-table load and don't need to transform, filter, or join data during the import. It uses simpler syntax for CSV or other delimited files:

This example query loads a CSV file from Azure Blob Storage directly into a table.

```sql
BULK INSERT Sales.Invoices
FROM 'invoices/inv-2025-01.csv'
WITH (
    DATA_SOURCE = 'MyAzureBlobStorage',
    FORMAT = 'CSV',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n'
);
```

This example query loads a local file with a format file for column mapping.

```sql
BULK INSERT dbo.Products
FROM 'C:\Data\products.csv'
WITH (
    FORMATFILE = 'C:\Data\products.fmt',
    FIRSTROW = 2,
    TABLOCK
);
```

#### When to choose OPENROWSET(BULK)

Use `OPENROWSET(BULK ...)` when you need one or more of the following conditions:

- Use `OPENROWSET(BULK ...)` to query or preview file data without creating a table first.
- Use `OPENROWSET(BULK ...)` to transform, filter, or join data during import.
- Use `OPENROWSET(BULK ...)` to load Parquet or Delta files because `BULK INSERT` doesn't support these formats.
- Use `OPENROWSET(BULK ...)` to import an entire file as a single LOB value with `SINGLE_BLOB`, `SINGLE_CLOB`, or `SINGLE_NCLOB`.

This example query previews a CSV file from Azure Blob Storage without inserting the data anywhere.

```sql
SELECT TOP 10 *
FROM OPENROWSET (
    BULK 'invoices/inv-2025-01.csv',
    DATA_SOURCE = 'MyAzureBlobStorage',
    FORMAT = 'CSV',
    FIRSTROW = 2,
    FIELDTERMINATOR = ','
) AS src;
```

This example query inserts data with transformation and filtering.

```sql
INSERT INTO Sales.Invoices (InvoiceDate, Amount, Customer)
SELECT CAST (InvoiceDate AS DATE),
       Amount * 1.1, -- Apply a 10% markup
       UPPER(Customer)
FROM OPENROWSET (
    BULK 'invoices/inv-2025-01.csv',
    DATA_SOURCE = 'MyAzureBlobStorage',
    FORMAT = 'CSV',
    FIRSTROW = 2
) WITH (
    InvoiceDate VARCHAR (10),
    Amount DECIMAL (18, 2),
    Customer VARCHAR (100)
) AS src
WHERE Amount IS NOT NULL;
```

This example query loads a Parquet file (not possible with `BULK INSERT`).

```sql
INSERT INTO Sales.Invoices
SELECT *
FROM OPENROWSET (
    BULK 'data/invoices/*.parquet',
    DATA_SOURCE = 'MyAzureStorage',
    FORMAT = 'PARQUET') AS src;
```

This example query imports an entire XML file as a single **varbinary(max)** value.

```sql
INSERT INTO dbo.XmlDocuments (DocContent)
SELECT BulkColumn
FROM OPENROWSET (
    BULK 'C:\Data\catalog.xml',
    SINGLE_BLOB
) AS x;
```

> [!TIP]  
> One approach is to start with `OPENROWSET(BULK ...)` in a `SELECT` to explore and validate file data, then switch to `BULK INSERT` for the final production load if you don't need transformations. If you need Parquet or Delta support or inline filtering, stay with `OPENROWSET`.

For more information, see the following related guides:

- The [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md) article provides a detailed side-by-side guide with security considerations.
- The [Bulk Import and Export of Data (SQL Server)](../import-export/bulk-import-and-export-of-data-sql-server.md) article provides an overview of all bulk data movement methods, including **bcp**, `BULK INSERT`, and `OPENROWSET`.
- The [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md) article provides the full T-SQL reference for `BULK INSERT`.
- The [OPENROWSET BULK (Transact-SQL)](../../t-sql/functions/openrowset-bulk-transact-sql.md) article provides the full T-SQL reference for `OPENROWSET(BULK ...)`.
- The [Examples of bulk access to data in Azure Blob Storage](../import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md) article provides side-by-side examples that use both methods with Azure Storage.
- The [Bulk import large-object data with OPENROWSET Bulk Rowset Provider (SQL Server)](../import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md) article provides `SINGLE_BLOB`, `SINGLE_CLOB`, and `SINGLE_NCLOB` examples.
- The [Use a format file to bulk import data (SQL Server)](../import-export/use-a-format-file-to-bulk-import-data-sql-server.md) article explains format file usage with both methods.
  - For guidance about preserving nulls or applying default values during bulk import, see [Keep nulls or default values during bulk import (SQL Server)](../import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md).
  - For guidance about preserving identity values during bulk import, see [Keep identity values when bulk importing data (SQL Server)](../import-export/keep-identity-values-when-bulk-importing-data-sql-server.md).

### Useful metadata functions

When you query external files by using `OPENROWSET` or external tables, use the built-in functions and procedures to inspect file metadata, discover schemas, and implement partition-aware queries.

#### filepath() and filename()

The `filepath()` and `filename()` functions return parts of the file path or the file name for each row in the result set. They're especially useful for:

- **Partition elimination**: Filter on folder segments (for example, year/month/day partitions) so the engine reads only the matching files instead of scanning everything.

- **Exposing source metadata**: Include the originating file name or path as a column in the query results, which is helpful for auditing or debugging.

| Function | Returns | Example |
| --- | --- | --- |
| `filename()` | The file name (including extension) of the source file for each row | `sales_2025_01.parquet` |
| `filepath(N)` | The *N*th folder segment from the wildcard (`*`) in the `BULK` path, where *N* starts at 1 | For path `sales/2025/01/*.parquet`, `filepath(1)` returns `2025`, `filepath(2)` returns `01` |

**Applies to**: Azure SQL Database, Azure SQL Managed Instance, [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, SQL database in Fabric.

This example query uses `filepath()` for partition elimination and `filename()` to identify source files. It only reads files under the `/2025/` folder, and only reads files under the `/06/` subfolder.

```sql
SELECT result.filename() AS SourceFile,
       result.filepath(1) AS [Year],
       result.filepath(2) AS [Month],
       *
FROM OPENROWSET (
    BULK 'abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*/*/*.parquet',
    FORMAT = 'PARQUET'
) AS result
WHERE result.filepath(1) = '2025' 
      AND result.filepath(2) = '06';
```

> [!TIP]  
> Place `filepath()` filters in the `WHERE` clause rather than in a subquery or CTE. When the filter is in the `WHERE` clause, the engine can perform partition elimination at the file scan level, which significantly reduces I/O.

#### sp_describe_first_result_set - discover OPENROWSET column types

When you use `OPENROWSET` with Parquet files, the engine infers column data types automatically (schema inference). The inferred types might be larger than necessary. For example, character columns are often inferred as **varchar(8000)** because Parquet metadata doesn't include a maximum length. This choice can degrade performance and consume more memory.

Use `sp_describe_first_result_set` to inspect the inferred schema *before* you finalize your query. After you see the inferred types, specify narrower types in a `WITH` clause to improve performance.

- **Step 1**: Inspect the inferred schema.

  ```sql
  EXECUTE sp_describe_first_result_set N'
  SELECT *
  FROM OPENROWSET(
      BULK ''abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*.parquet'',
      FORMAT = ''PARQUET''
  ) AS result';
  ```

  The output shows each column's name, inferred data type, max length, precision, and scale. If you see **varchar(8000)** where a **varchar(100)** would suffice, override it.

- **Step 2**: Use explicit types for better performance.

  ```sql
  SELECT TOP 100 *
  FROM OPENROWSET (
      BULK 'abs://mycontainer@mystorageaccount.blob.core.windows.net/data/sales/*.parquet',
      FORMAT = 'PARQUET'
  ) WITH (
      OrderId INT,
      OrderDate DATE,
      Amount DECIMAL (18, 2),
      Customer VARCHAR (100) -- much narrower than the inferred varchar(8000)
  ) AS result;
  ```

Schema inference only works with Parquet files. For CSV files, always specify column definitions either in a `WITH` clause (for `OPENROWSET`) or in the `CREATE EXTERNAL TABLE` statement. `sp_describe_first_result_set` is a general SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric procedure, but it's especially useful for `OPENROWSET` queries. For more information, see [sp_describe_first_result_set](../system-stored-procedures/sp-describe-first-result-set-transact-sql.md).

## Performance, troubleshooting, and best practices

After you implement data virtualization, use these guides to optimize performance, diagnose issues, and ensure production readiness:

| Area | Article | Details |
| --- | --- | --- |
| **PolyBase performance** | [Performance considerations in PolyBase for SQL Server](polybase-performance.md) | Statistics, pushdown, parallelism, and memory management |
| **Pushdown computation** | [Pushdown computations in PolyBase](polybase-pushdown-computation.md) | Specifies which operations push to the remote source |
| **How to tell if pushdown occurred** | [How to tell if external pushdown occurred](polybase-how-to-tell-pushdown-computation.md) | Query plans and DMVs |
| **Troubleshooting** | [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md) | Common errors and resolutions |
| **Kerberos connectivity** | [Troubleshoot PolyBase Kerberos connectivity](polybase-troubleshoot-connectivity.md) | |
| **FAQ** | [PolyBase frequently asked questions](polybase-faq.yml) | |
| **Errors and solutions** | [PolyBase errors and possible solutions](polybase-errors-and-possible-solutions.md) | |

## Related content

- [PolyBase overview](overview.md)
- [Get started with PolyBase in SQL Server 2022](polybase-get-started.md)
- [PolyBase Transact-SQL reference](polybase-t-sql-objects.md)
- [Type mapping with PolyBase](polybase-type-mapping.md)
- [Install PolyBase on Windows](polybase-installation.md)
- [Install PolyBase on Linux](polybase-linux-setup.md)
