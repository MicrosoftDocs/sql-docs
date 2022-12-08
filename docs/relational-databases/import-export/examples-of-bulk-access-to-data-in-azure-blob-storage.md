---
title: "Bulk access to data in Azure Blob Storage"
description: "Transact-SQL examples that use BULK INSERT and OPENROWSET to access data in an Azure Blob Storage account."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/04/2022
ms.service: sql
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "bulk importing [SQL Server], from Azure Blob Storage"
  - "Azure Blob Storage, bulk import to SQL Server"
  - "BULK INSERT, Azure Blob Storage"
  - "OPENROWSET, Azure Blob Storage"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Examples of bulk access to data in Azure Blob Storage

[!INCLUDE[sqlserver2017-asdb](../../includes/applies-to-version/sqlserver2017-asdb.md)]

The `BULK INSERT` and `OPENROWSET` statements can directly access a file in Azure Blob Storage. The following examples use data from a CSV (comma separated value) file (named `inv-2017-01-19.csv`), stored in a container (named `Week3`), stored in a storage account (named `newinvoices`).

> [!IMPORTANT]  
> All the paths to the container and to the files on Blob Storage are **case-sensitive**. If not correct, it might return an error like "Cannot bulk load. The file "file.csv" does not exist or you don't have file access rights."

## Create the credential

The external data source must be created with a database scoped credential that uses the `SHARED ACCESS SIGNATURE` identity. To create a shared access signature (SAS) for your storage account, see the **Shared access signature** property on the storage account property page in the Azure portal. For more information on shared access signatures, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview). For more information on credentials, see [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

Create a database scoped credential using `IDENTITY`, which must be `SHARED ACCESS SIGNATURE`. Use the SAS token generated for the Blob Storage account. Verify that your SAS token doesn't have a leading `?`, that you have at least read permission on the object that should be loaded, and that the expiration period is valid (all dates are in UTC time).

For example:

```sql
CREATE DATABASE SCOPED CREDENTIAL UploadInvoices
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'sv=2018-03-28&ss=b&srt=sco&sp=rwdlac&se=2019-08-31T02:25:19Z&st=2019-07-30T18:25:19Z&spr=https&sig=KS51p%2BVnfUtLjMZtUTW1siyuyd2nlx294tL0mnmFsOk%3D';
```

## Known issues

Requests from Azure SQL Database and Azure SQL Managed Instance using SAS tokens may be blocked with the following error:

```text
Msg 4861, Level 16, State 1, Line 27
Cannot bulk load because the file "FileName.extension" could not be opened. Operating system error code 5(Access is denied.).
```

Only a subset of Azure services are currently on the trusted services list. For a complete list of trusted services and updates on Azure storage firewall settings, see [Trusted access for resources registered in your subscription](/azure/storage/common/storage-network-security?tabs=azure-portal#trusted-access-for-resources-registered-in-your-subscription).

## Examples

Along with the examples in this article, you can also review the [Azure SQL Database import data samples](https://github.com/Azure-Samples/azure-sql-db-import-data) on GitHub.

### Access data in a CSV file referencing an Azure Blob Storage location

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

### Access data in a CSV file referencing a container in an Azure Blob Storage location

The following example uses an external data source pointing to a container (named `week3`) in an Azure storage account.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureInvoicesContainer
    WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://newinvoices.blob.core.windows.net/week3',
        CREDENTIAL = UploadInvoices
    );
```

Then the `OPENROWSET` statement doesn't include the container name in the file description:

```sql
SELECT * FROM OPENROWSET(
   BULK 'inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoicesContainer',
   FORMAT = 'CSV',
   FORMATFILE='invoices.fmt',
   FORMATFILE_DATA_SOURCE = 'MyAzureInvoices'
   ) AS DataFile;
```

Using `BULK INSERT`, don't use the container name in the file description:

```sql
BULK INSERT Colors2
FROM 'inv-2017-01-19.csv'
WITH (DATA_SOURCE = 'MyAzureInvoicesContainer',
      FORMAT = 'CSV');
```

## See also

- [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)
