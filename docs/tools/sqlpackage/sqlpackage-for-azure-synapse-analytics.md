---
title: SqlPackage for Azure Synapse Analytics
description: Tips for using SqlPackage in Azure Synapse Analytics scenarios
author: dzsquared
ms.author: drskwier
ms.reviewer: "llali"
ms.date: 2/13/2023
ms.service: sql
ms.topic: conceptual
ms.custom: tools|sos
---
# SqlPackage for Azure Synapse Analytics

This article covers SqlPackage support for [Azure Synapse Analytics](/azure/synapse-analytics/sql/overview-architecture).  It includes information on the following topics:
- integration with Azure Blob Storage for accessing data in parquet files
- support for serverless SQL pools

Both dedicated and serverless SQL pools do not support the import/export actions in SqlPackage or `.bacpac` files. SqlPackage supports Azure Synapse Analytics with `.dacpac` files and can read and write data in parquet format files in Azure Blog Storage.  To import or export data from a dedicated SQL pool, you must use the publish or extract actions with data as detailed below.

## Extract (export data)
To export data from an Azure Synapse Analytics database to Azure Blob Storage, the SqlPackage [extract](sqlpackage-extract.md) action is used with following properties:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageKey

Access for the database to access the blob storage container is authorized via a storage account key. The database schema (.dacpac file) is written to the local client running SqlPackage and the data is written to Azure Blob Storage in parquet format.

An additional parameter is optional, which sets the storage root path within the container:
- /p:AzureStorageRootPath

Without this property, the path defaults to `servername/databasename/timestamp/`.  Data is stored in individual folders named with 2-part table names.

### Example

The following example extracts a database named `databasename` from a server named `yourserver.sql.azuresynapse.net` to a local file named `databaseschema.dacpac` in the current directory. The data is written to a container named `containername` in a storage account named `storageaccount` using a storage account key named `storageaccountkey`. The data is written to the default path of `servername/databasename/timestamp/` in the container.

```bash
SqlPackage /Action:Extract /SourceServerName:yourserver.sql.azuresynapse.net /SourceDatabaseName:databasename /SourceUser:sqladmin /SourcePassword:{your_password} /TargetFile:databaseschema.dacpac /p:AzureStorageBlobEndpoint=https://storageaccount.blob.core.windows.net /p:AzureStorageContainer=containername /p:AzureStorageKey=storageaccountkey
```

See [SqlPackage extract](sqlpackage-extract.md#examples) for more examples of authentication types available.

## Publish (import data)

To import data from parquet files in Azure Blob Storage to an Azure Synapse Analytics database, the SqlPackage [publish](sqlpackage-publish.md) action is used with the following properties:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageRootPath
- /p:AzureStorageKey or /p:AzureSharedAccessSignatureToken

Access for publish can be authorized via a storage account key or a shared access signature (SAS) token. The database schema (.dacpac file) is read from the local client running SqlPackage and the data is read from Azure Blob Storage in parquet format.

### Example

The following example publishes a database named `databasename` to a server named `yourserver.sql.azuresynapse.net` from a local file named `databaseschema.dacpac` in the current directory. The data is read from a container named `containername` in a storage account named `storageaccount` using a storage account key named `storageaccountkey`. The data is read from individual folders per table under the path `yourserver.sql.azuresynapse.net/databasename/6-12-2022_8-09-56_AM/` in the container.

```bash
SqlPackage /Action:Publish /SourceFile:databaseschema.dacpac /TargetServerName:yourserver.sql.azuresynapse.net /TargetDatabaseName:databasename /TargetUser:sqladmin /TargetPassword:{your_password} /p:AzureStorageBlobEndpoint=https://storageaccount.blob.core.windows.net /p:AzureStorageContainer=containername  /p:AzureStorageKey=storageaccountkey /p:AzureStorageRootPath="yourserver.sql.azuresynapse.net/databasename/6-12-2022_8-09-56_AM/"
```

See [SqlPackage publish](sqlpackage-publish.md#examples) for more examples of authentication types available.

## Support for serverless SQL pools

Synapse serverless SQL pools are supported for both the extract and publish actions.  The T-SQL surface area of serverless SQL pools is limited by [design](/azure/synapse-analytics/sql/on-demand-workspace-overview#t-sql-support) to external tables, external views, stored procedures, functions, statistics, and credentials.  The following features are included in support for serverless SQL pools:

- [Delta](/azure/synapse-analytics/sql/query-delta-lake-format) external file format
- [OPENROWSET](/azure/synapse-analytics/sql/develop-openrowset) provider CosmosDB with PROVIDER, CONNECTION, OBJECT, and CREDENTIAL or SERVER_CREDENTIAL specified
- `with ()` clause on [OPENROWSET](/azure/synapse-analytics/sql/develop-openrowset)

## Next Steps
- Learn more about [Extract](sqlpackage-extract.md)
- Learn more about [Publish](sqlpackage-publish.md)
- Learn more about [Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction)
- Learn more about [Azure Storage Account Keys](/azure/storage/common/storage-account-keys-manage)
