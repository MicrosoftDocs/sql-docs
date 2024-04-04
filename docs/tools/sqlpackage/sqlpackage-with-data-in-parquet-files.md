---
title: SqlPackage with data in Parquet files (preview)
description: Tips for using SqlPackage with data stored in Azure Blob Storage
author: dzsquared
ms.author: drskwier
ms.reviewer: "llali"
ms.date: 10/19/2023
ms.service: sql
ms.topic: conceptual
ms.custom: tools|sos
---

# SqlPackage with data in Parquet files (preview)

This article covers SqlPackage support for interacting with data stored in Azure Blob Storage that is in Parquet format. For SQL Server 2022 and Azure SQL Managed Instance, preview support for [extract](#extract-export-data) and [publish](#publish-import-data) with data in Parquet files in Azure Blob Storage is available in SqlPackage 162.1.176 and higher. Azure SQL Database and SQL Server 2019 and earlier aren't supported. The [import](sqlpackage-import.md) and [export](sqlpackage-export.md) actions continue to be available for SQL Server, Azure SQL Managed Instance, and Azure SQL Database. Support for Parquet files in Azure Blob Storage continues to be generally available for [Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md).

With [extract](#extract-export-data), the database schema (`.dacpac` file) is written to the local client running SqlPackage and the data is written to Azure Blob Storage in Parquet format. The data is stored in individual folders named with two-part table names. [CETAS](../../t-sql/statements/create-external-table-as-select-transact-sql.md) is used to write the files in Azure Blob Storage.

With [publish](#publish-import-data), the database schema (`.dacpac` file) is read from the local client running SqlPackage and the data is read from or written to Azure Blob Storage in Parquet format.

In SQL databases hosted in Azure, the extract/publish operations with Parquet files offer improved performance over import/export operations with `.bacpac` files in many scenarios.


## Extract (export data)
To export data from a database to Azure Blob Storage, the SqlPackage [extract](sqlpackage-extract.md) action is used with following properties:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageKey or /p:AzureSharedAccessSignatureToken

Access for the database to access the blob storage container is authorized via a storage account key. The database schema (.dacpac file) is written to the local client running SqlPackage and the data is written to Azure Blob Storage in Parquet format.

The parameter `/p:AzureStorageRootPath` is optional, which sets the storage root path within the container. Without this property, the path defaults to `servername/databasename/timestamp/`. Data is stored in individual folders named with two-part table names. The number of files created per table depends upon the MAXDOP and available SQL cores at the time of the export.

Finally, the property `/p:TableData` specifies which tables have their data exported. Specify the table name with or without the brackets surrounding the name parts in the format schema_name.table_identifier. This property may be specified multiple times to indicate multiple tables.

### Example

The following example extracts a database named `databasename` from a server named `yourserver` to a local file named `databaseschema.dacpac` in the current directory. The data is written to a container named `containername` in a storage account named `storageaccount` using a storage account key named `storageaccountkey`. The data is written to the default path of `servername/databasename/timestamp/` in the container.

```bash
SqlPackage /Action:Extract /SourceServerName:yourserver /SourceDatabaseName:databasename /TargetFile:databaseschema.dacpac /p:AzureStorageBlobEndpoint=https://storageaccount.blob.core.windows.net /p:AzureStorageContainer=containername /p:AzureStorageKey=storageaccountkey
```

See [SqlPackage extract](sqlpackage-extract.md#examples) for more examples of authentication types available.

## Publish (import data)

To import data from Parquet files in Azure Blob Storage to a database, the SqlPackage [publish](sqlpackage-publish.md) action is used with the following properties:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageRootPath
- /p:AzureStorageKey or /p:AzureSharedAccessSignatureToken

Access for publish can be authorized via a storage account key or a shared access signature (SAS) token. The database schema (.dacpac file) is read from the local client running SqlPackage and the data is read from Azure Blob Storage in Parquet format.

### Example

The following example publishes a database named `databasename` to a server named `yourserver` from a local file named `databaseschema.dacpac` in the current directory. The data is read from a container named `containername` in a storage account named `storageaccount` using a storage account key named `storageaccountkey`. The data is read from individual folders per table under the path `yourserver/databasename/10-19-2023_11-09-56/` in the container.

```bash
SqlPackage /Action:Publish /SourceFile:databaseschema.dacpac /TargetServerName:yourserver /TargetDatabaseName:databasename /p:AzureStorageBlobEndpoint=https://storageaccount.blob.core.windows.net /p:AzureStorageContainer=containername  /p:AzureStorageKey=storageaccountkey /p:AzureStorageRootPath="yourserver/databasename/10-19-2023_11-09-56/"
```

See [SqlPackage publish](sqlpackage-publish.md#examples) for more examples of authentication types available.


## Limitations

### Polybase

[Polybase](../../relational-databases/polybase/polybase-guide.md) is required for SqlPackage operations with Parquet files. The following query can be used to check if Polybase is enabled:

```sql
// configuration_id = 16397 is 'allow polybase export'
// configuration_id = 16399 is 'polybase enabled'
SELECT configuration_id, value_in_use FROM sys.configurations
WHERE configuration_id IN (16397, 16399)
```

You may need to enable [Polybase](../../relational-databases/polybase/polybase-installation.md) or [Polybase export](../../database-engine/configure-windows/allow-polybase-export.md). Enabling Polybase on Azure SQL Managed Instance requires [PowerShell or Azure CLI](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true#methods-to-enable-cetas). It's recommended that you evaluate whether enabling Polybase is right for your environment before making configuration changes.

### Table and data types

Data types supported by [CETAS](../../t-sql/statements/create-external-table-as-select-transact-sql.md#supported-data-types) are supported for extract and publish operations with Parquet files.

Ledger tables are enabled for extract and publish operations with Parquet files.

Data stored with Always Encrypted isn't supported for extract and publish operations with Parquet files.


## Next Steps
- Learn more about [Extract](sqlpackage-extract.md)
- Learn more about [Publish](sqlpackage-publish.md)
- Learn more about [Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction)
- Learn more about [Azure Storage shared access signature (SAS)](/azure/storage/common/storage-sas-overview)
- Learn more about [Azure Storage Account Keys](/azure/storage/common/storage-account-keys-manage)