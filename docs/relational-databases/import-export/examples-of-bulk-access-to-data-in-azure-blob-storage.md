---
title: "Examples of Bulk Access to Data in Azure Blob Storage | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2017"
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
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Examples of Bulk Access to Data in Azure Blob Storage
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

The `BULK INSERT` and `OPENROWSET` statements can directly access a file in Azure blob storage. The following examples use data from a CSV (comma separated value) file (named `inv-2017-01-19.csv`), stored in a container (named `Week3`), stored in a storage account (named `newinvoices`). The path to format file can be used, but is not included in these examples. 

Bulk access to Azure blob storage from SQL Server, requires at least [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] CTP 1.1.

> [!IMPORTANT]
>  All the paths to the container and to the files on blob are `CASE SENSITIVE`. If not correct, it might return error like "Cannot bulk load. The file "file.csv" does not exist or you don't have file access rights."
> "


## Create the credential   
   
All of the examples below require a database scoped credential referencing a shared access signature.   

> [!IMPORTANT]
>  The external data source must be created with a database scoped credential that uses the `SHARED ACCESS SIGNATURE` identity. To create a shared access signature for your storage account, see the **Shared access signature** property on the storage account property page, in the Azure portal. For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1). For more information on credentials, see [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).  
 
Create a database scoped credential using the `IDENTITY` which must be `SHARED ACCESS SIGNATURE`. Use the secret from your Azure portal. For example:  

```sql
CREATE DATABASE SCOPED CREDENTIAL UploadInvoices  
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'QLYMgmSXMklt%2FI1U6DcVrQixnlU5Sgbtk1qDRakUBGs%3D';
```


## Accessing data in a CSV file referencing an Azure blob storage location   
The following example uses an external data source pointing to an Azure storage account, named `newinvoices`.   
```sql
CREATE EXTERNAL DATA SOURCE MyAzureInvoices
    WITH  (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://newinvoices.blob.core.windows.net', 
        CREDENTIAL = UploadInvoices  
    );
```   

Then the `OPENROWSET` statement adds the container name (`week3`) to the file description. The file is named `inv-2017-01-19.csv`.
```sql     
SELECT * FROM OPENROWSET(
   BULK  'week3/inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoices',
   SINGLE_CLOB) AS DataFile;
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
    WITH  (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://newinvoices.blob.core.windows.net/week3', 
        CREDENTIAL = UploadInvoices  
    );
```  
  
Then the `OPENROWSET` statement does not include the container name in the file description:
```sql
SELECT * FROM OPENROWSET(
   BULK  'inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoicesContainer',
   SINGLE_CLOB) AS DataFile;
```   

Using `BULK INSERT`, do not use the container name in the file description: 

```sql
BULK INSERT Colors2
FROM 'inv-2017-01-19.csv'
WITH (DATA_SOURCE = 'MyAzureInvoicesContainer',
      FORMAT = 'CSV'); 
```

## See Also   

[CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)   
[CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)   
[BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md)   
[OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)   

