---
title: "Bulk access to data in Azure Blob storage"
description: "Transact-SQL examples that use BULK INSERT and OPENROWSET to access data in an Azure Blob storage account."
ms.date: "10/22/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk importing [SQL Server], from Azure blob storage"
  - "Azure blob storage, bulk import to SQL Server"
  - "BULK INSERT, Azure blob storage"
  - "OPENROWSET, Azure blob storage"
ms.assetid: f7d85db3-7a93-400e-87af-f56247319ecd
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Examples of bulk access to data in Azure Blob storage

[!INCLUDE[sqlserver2017-asdb](../../includes/applies-to-version/sqlserver2017-asdb.md)]

The `BULK INSERT` and `OPENROWSET` statements can directly access a file in Azure blob storage. The following examples use data from a CSV (comma separated value) file (named `inv-2017-01-19.csv`), stored in a container (named `Week3`), stored in a storage account (named `newinvoices`). The path to format file can be used, but is not included in these examples.

Bulk access to Azure blob storage from SQL Server, requires at least [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] CTP 1.1.

> [!IMPORTANT]
> All the paths to the container and to the files on blob are `CASE SENSITIVE`. If not correct, it might return error like "Cannot bulk load. The file "file.csv" does not exist or you don't have file access rights."

## Create the credential

All of the examples below require a database scoped credential referencing a shared access signature.

> [!IMPORTANT]
> The external data source must be created with a database scoped credential that uses the `SHARED ACCESS SIGNATURE` identity. To create a shared access signature for your storage account, see the **Shared access signature** property on the storage account property page, in the Azure portal. For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1). For more information on credentials, see [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

Create a database scoped credential using the `IDENTITY` which must be `SHARED ACCESS SIGNATURE`. Use the SAS token generated for the blob storage account. Verify that your SAS token does not have a leading `?`, that you have at least read permission on the object that should be loaded, and that the expiration period is valid (all dates are in UTC time).

For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL UploadInvoices
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2019-08-31T02:25:19Z&st=2019-07-30T18:25:19Z&spr=https&sig=KS51p%2BVnfUtLjMZtUTW1siyuyd2nlx294tL0mnmFsOk%3D';
```

## Accessing data in a CSV file referencing an Azure blob storage location

The following example uses an external data source pointing to an Azure storage account, named `MyAzureInvoices`.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureInvoices
    WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://newinvoices.blob.core.windows.net',
        CREDENTIAL = UploadInvoices
    );
```

Then the `OPENROWSET` statement adds the container name (`week3`) to the file description. The file is named `inv-2017-01-19.csv`.

```sql
SELECT * FROM OPENROWSET(
   BULK 'week3/inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoices',
   FORMAT = 'CSV',
   FORMATFILE='invoices.fmt',
   FORMATFILE_DATA_SOURCE = 'MyAzureInvoices'
   ) AS DataFile;   
```

Using `BULK INSERT`, use the container and file description:

```sql
BULK INSERT Colors2
FROM 'week3/inv-2017-01-19.csv'
WITH (DATA_SOURCE = 'MyAzureInvoices',
      FORMAT = 'CSV');
```

## Accessing data in a CSV file referencing a container in an Azure blob storage location

The following example uses an external data source pointing to a container (named `week3`) in an Azure storage account.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureInvoicesContainer
    WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://newinvoices.blob.core.windows.net/week3',
        CREDENTIAL = UploadInvoices
    );
```

Then the `OPENROWSET` statement does not include the container name in the file description:

```sql
SELECT * FROM OPENROWSET(
   BULK 'inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoicesContainer',
   FORMAT = 'CSV',
   FORMATFILE='invoices.fmt',
   FORMATFILE_DATA_SOURCE = 'MyAzureInvoices'
   ) AS DataFile;
```

Using `BULK INSERT`, do not use the container name in the file description:

```sql
BULK INSERT Colors2
FROM 'inv-2017-01-19.csv'
WITH (DATA_SOURCE = 'MyAzureInvoicesContainer',
      FORMAT = 'CSV');
```

## See Also

- [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)
