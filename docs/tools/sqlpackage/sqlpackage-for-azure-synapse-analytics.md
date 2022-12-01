---
title: SqlPackage for Azure Synapse Analytics
description: Tips for using SqlPackage in Azure Synapse Analytics scenarios
ms.custom: "tools|sos"
ms.date: 03/10/2021
ms.service: sql
ms.reviewer: "llali"
ms.topic: conceptual
author: dzsquared
ms.author: drskwier
---
# SqlPackage for Azure Synapse Analytics

This article highlights features of SqlPackage.exe that are focused on Azure Synapse Analytics databases.

## Extract
To [extract](sqlpackage-extract.md) a schema to Azure Blob Storage, the following properties are required:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageKey

Access for extract is authorized via a storage account key.  An additional parameter is optional, which sets the storage root path within the container:
- /p:AzureStorageRootPath

Without this property, the path defaults to `servername/databasename/timestamp/`.

## Publish
To [publish](sqlpackage-publish.md) a schema from a dacpac in Azure Blob Storage, the following properties are required:
- /p:AzureStorageBlobEndpoint
- /p:AzureStorageContainer
- /p:AzureStorageRootPath
- /p:AzureStorageKey or /p:AzureSharedAccessSignatureToken

Access for publish can be authorized via a storage account key or a shared access signature (SAS) token.

## Next Steps
- Learn more about [Extract](sqlpackage-extract.md)
- Learn more about [Publish](sqlpackage-publish.md)
- Learn more about [Azure Blob Storage](/azure/storage/blobs/storage-blobs-introduction)
- Learn more about [Azure Storage Account Keys](/azure/storage/common/storage-account-keys-manage)