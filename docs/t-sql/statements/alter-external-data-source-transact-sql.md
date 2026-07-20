---
title: "ALTER EXTERNAL DATA SOURCE (Transact-SQL)"
description: ALTER EXTERNAL DATA SOURCE modifies an external data source used to create an external table.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, hudequei, wiassaf
ms.date: 11/10/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ALTER EXTERNAL DATA SOURCE"
  - "ALTER_EXTERNAL_DATA_SOURCE"
helpviewer_keywords:
  - "polybase, alter external data source statement"
  - "ALTER EXTERNAL DATA SOURCE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azuresqledge-current || =fabric || =fabric-sqldb"
---
# ALTER EXTERNAL DATA SOURCE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

 Modifies an external data source used to create an external table, used for PolyBase and data virtualization features. The external data source can be Hadoop or Azure Blob Storage (WASBS) for SQL SERVER and Azure Blob Storage (WASBS) or Azure Data Lake storage (ABFSS/ADL) for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

 Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Hadoop external data sources are no longer supported. Also, Azure Blob Storage and Azure Data Lake Gen 2 prefixes changed, refer to the following table:

| External Data Source | From | To |
|:--|:--|:--|
| `Azure Blob Storage` | `wasb[s]` | `abs` |
| `ADLS Gen2` | `abfs[s]` | `adls` |

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Modify an external data source. Syntax for SQL Server (2016, 2017 and 2019) and [!INCLUDE [sspdw-md](../../includes/sspdw-md.md)].

```syntaxsql
-- Modify an external data source
-- Applies to: SQL Server (2016, 2017 and 2019) and APS
ALTER EXTERNAL DATA SOURCE data_source_name SET
    {   
        LOCATION = '<prefix>://<path>[:<port>]' [,] |
        RESOURCE_MANAGER_LOCATION = <'IP address;Port'> [,] |
        CREDENTIAL = credential_name
    }  
    [;]  
```

Modify an external data source pointing to Azure Blob storage. Syntax for SQL Server (2017 and 2019).

```syntaxsql
-- Modify an external data source pointing to Azure Blob storage
-- Applies to: SQL Server (2017 and 2019)
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        LOCATION = 'https://storage_account_name.blob.core.windows.net'
        [, CREDENTIAL = credential_name ] 
```

Modify an external data source pointing to Azure Blob storage. Syntax for SQL Server 2022 and later versions.

```syntaxsql
-- Modify an external data source pointing to Azure Blob storage
-- Applies to: SQL Server 2022 and later versions
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        LOCATION = 'abs://storage_account_name.blob.core.windows.net'
        [, CREDENTIAL = credential_name ] 
```

Modify an external data source pointing to Azure Data Lake Storage (ADLS) Gen2. Syntax for SQL Server 2022 and later versions.

```syntaxsql
-- Modify an external data source pointing to Azure Data Lake Storage Gen2
-- Applies to: SQL Server 2022 and later versions
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        LOCATION = 'adls://storage_account_name.dfs.core.windows.net'
        [, CREDENTIAL = credential_name ] 
```

Modify an external data source pointing to Azure Blob Storage or Azure Data Lake Storage. Syntax for Azure Synapse Analytics dedicated SQL pool only.

```syntaxsql
-- Modify an external data source pointing to Azure Blob storage or Azure Data Lake storage
-- Applies to: Azure Synapse Analytics dedicated SQL pool only
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        [LOCATION = '<location prefix>://<location path>']
        [, CREDENTIAL = credential_name ] 
```

## Arguments

#### data_source_name

 Specifies the user-defined name for the data source. The name must be unique.

#### LOCATION

 Provides the connectivity protocol, path, and port to the external data source. See [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md#location--prefixpathport) for valid location options.

#### RESOURCE_MANAGER_LOCATION = '*\<IP address;Port>*'

**Doesn't apply** to [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)])

 Specifies the Hadoop Resource Manager location. When specified, the query optimizer might choose to pre-process data for a PolyBase query by using Hadoop's computation capabilities. This is a cost-based decision. Called predicate pushdown, this can significantly reduce the volume of data transferred between Hadoop and SQL, and therefore improve query performance.

#### CREDENTIAL = Credential_Name

Specifies the named credential. See  [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](create-database-scoped-credential-transact-sql.md).

#### TYPE = [ HADOOP | BLOB_STORAGE ]

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

> [!IMPORTANT]  
> For bulk operations only, `LOCATION` must be valid the URL to Azure Blob storage. Don't put a `/`, file name, or shared access signature parameters at the end of the `LOCATION` URL.

The credential you use must be created using `SHARED ACCESS SIGNATURE` as the identity. For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](/azure/storage/storage-dotnet-shared-access-signature-part-1).

## Remarks

 Only single source can be modified at a time. Concurrent requests to modify the same source cause one statement to wait. However, different sources can be modified at the same time. This statement can run concurrently with other statements.

 In Azure Synapse Analytics, connections to external data sources pointing to Azure Blob storage or Azure Data Lake storage are supported in dedicated SQL pool only.  

## Permissions

 Requires ALTER ANY EXTERNAL DATA SOURCE permission.

 > [!IMPORTANT]  
 > The ALTER ANY EXTERNAL DATA SOURCE  permission grants any principal the ability to create and modify any external data source object, and therefore, it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged, and therefore must be granted only to trusted principals in the system.

## Examples

 The following example alters the location and Resource Manager location of an existing data source.

Doesn't apply to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

```sql
ALTER EXTERNAL DATA SOURCE hadoop_eds SET
     LOCATION = 'hdfs://10.10.10.10:8020',
     RESOURCE_MANAGER_LOCATION = '10.10.10.10:8032'
    ;
```

 The following example alters the credential to connect to an existing data source.

```sql
ALTER EXTERNAL DATA SOURCE hadoop_eds SET
   CREDENTIAL = new_hadoop_user
    ;
```
 The following example alters the credential to a new LOCATION. This example is an external data source created for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]. 

```sql
ALTER EXTERNAL DATA SOURCE AzureStorage_west SET
   LOCATION = 'wasbs://loadingdemodataset@updatedproductioncontainer.blob.core.windows.net',
   CREDENTIAL = AzureStorageCredential
```

## Related content

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md)
