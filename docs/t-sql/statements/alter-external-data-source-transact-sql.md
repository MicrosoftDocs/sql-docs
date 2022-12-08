---
title: "ALTER EXTERNAL DATA SOURCE (Transact-SQL)"
description: ALTER EXTERNAL DATA SOURCE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "07/26/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER EXTERNAL DATA SOURCE"
  - "ALTER_EXTERNAL_DATA_SOURCE"
helpviewer_keywords:
  - "polybase, alter external data source statement"
  - "ALTER EXTERNAL DATA SOURCE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER EXTERNAL DATA SOURCE (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

  Modifies an external data source used to create an external table. The external data source can be Hadoop or Azure Blob Storage (WASBS) for SQL SERVER and Azure Blob Storage (WASBS) or Azure Data Lake storage (ABFSS/ADL) for [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]. 

## Syntax  

```syntaxsql
-- Modify an external data source
-- Applies to: SQL Server (2016 or later) and APS
ALTER EXTERNAL DATA SOURCE data_source_name SET
    {   
        LOCATION = '<prefix>://<path>[:<port>]' [,] |
        RESOURCE_MANAGER_LOCATION = <'IP address;Port'> [,] |
        CREDENTIAL = credential_name
    }  
    [;]  

-- Modify an external data source pointing to Azure Blob storage
-- Applies to: SQL Server (starting with 2017)
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        LOCATION = 'https://storage_account_name.blob.core.windows.net'
        [, CREDENTIAL = credential_name ] 

-- Modify an external data source pointing to Azure Blob storage or Azure Data Lake storage
-- Applies to: Azure Synapse Analytics dedicated SQL pool only
ALTER EXTERNAL DATA SOURCE data_source_name
    SET
        [LOCATION = '<location prefix>://<location path>']
        [, CREDENTIAL = credential_name ] 
```

## Arguments  
 data_source_name
 Specifies the user-defined name for the data source. The name must be unique.

 LOCATION = '\<prefix\>://\<path\>[:\<port\>]'
 Provides the connectivity protocol, path, and port to the external data source. See [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](create-external-data-source-transact-sql.md#location--prefixpathport) for valid location options.

 RESOURCE_MANAGER_LOCATION = '\<IP address;Port>' (Does not apply to [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)])
 Specifies the Hadoop Resource Manager location. When specified, the query optimizer might choose to pre-process data for a PolyBase query by using Hadoop's computation capabilities. This is a cost-based decision. Called predicate pushdown, this can significantly reduce the volume of data transferred between Hadoop and SQL, and therefore improve query performance.

 CREDENTIAL = Credential_Name
 Specifies the named credential. See  [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

TYPE = [HADOOP | BLOB_STORAGE]   
**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].
For bulk operations only, `LOCATION` must be valid the URL to Azure Blob storage. Do not put **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL.
The credential used, must be created using `SHARED ACCESS SIGNATURE` as the identity. For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](/azure/storage/storage-dotnet-shared-access-signature-part-1).

  

## Remarks
 Only single source can be modified at a time. Concurrent requests to modify the same source cause one statement to wait. However, different sources can be modified at the same time. This statement can run concurrently with other statements.
 
 In Azure Synapse Analytics, connections to external data sources pointing to Azure Blob storage or Azure Data Lake storage are supported in dedicated SQL pool only.

## Permissions  
 Requires ALTER ANY EXTERNAL DATA SOURCE permission.
 > [!IMPORTANT]  
 > The ALTER ANY EXTERNAL DATA SOURCE  permission grants any principal the ability to create and modify any external data source object, and therefore, it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged, and therefore must be granted only to trusted principals in the system.


## Examples  
 The following example alters the location and resource manager location of an existing data source.

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
 The following example alters the credential to a new LOCATION. This example is an external data source created for [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]. 

```sql  
ALTER EXTERNAL DATA SOURCE AzureStorage_west SET
   LOCATION = 'wasbs://loadingdemodataset@updatedproductioncontainer.blob.core.windows.net',
   CREDENTIAL = AzureStorageCredential
```
