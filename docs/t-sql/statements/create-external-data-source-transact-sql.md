---
title: "CREATE EXTERNAL DATA SOURCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/01/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE EXTERNAL DATA SOURCE"
  - "CREATE_EXTERNAL_DATA_SOURCE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "External"
  - "External, data source"
  - "PolyBase, create data source"
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# CREATE EXTERNAL DATA SOURCE (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

External data sources are used to establish connectivity and support four primary use cases:

1. Data virtualization and data load using [PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)
1. Bulk load operations using SQL Server or Azure SQL Database using `BULK INSERT` or `OPENROWSET`
1. Query remote Azure SQL Database and Azure SQL Data Warehouse instances using Azure SQL Database with [elastic query](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started-vertical/)
1. Query a sharded Azure SQL Database using [elastic query](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started/)

> [!NOTE]  
> PolyBase is supported on SQL Server (2016 or higher), Azure SQL Data Warehouse and Parallel Data Warehouse. Elastic Database queries are supported only on Azure SQL Database v12 or later.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax  
  
```sql
CREATE EXTERNAL DATA SOURCE <data_source_name>  
WITH
(    LOCATION   = '<prefix>://<path>[:<port>]'
[,   CREDENTIAL = <credential_name> ]
[,   TYPE       = HADOOP | BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER ]
[,   RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]']
[,   DATABASE_NAME  = '<database_name>' ]
[,   SHARD_MAP_NAME = '<shard_map_manager>' ]
)
[;]
```

## Arguments

### Data_Source_Name

Specifies the user-defined name for the data source. The name must be unique within the database in SQL Server, Azure SQL Database, and Azure SQL Data Warehouse. The name must be unique within the server in Parallel Data Warehouse.

### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source      | Location prefix | Location path                                         | Supported locations by product / service    |
| ------------------------- | --------------- | ----------------------------------------------------- | ------------------------------------------- |
| Cloudera or Hortonworks   | hdfs            | `<Namenode>[:port]`                                   | SQL Server (2016+), Parallel Data Warehouse |
| Azure Blob Storage        | wasb[s]         | `<container>@<storage_account>.blob.core.windows.net` | SQL Server (2016+), Parallel Data Warehouse, Azure SQL Data Warehouse |
| ADLS Gen 1                | adl             | `<storage_account>.azuredatalake.net`                 | Azure SQL Data Warehouse                    |
| ADLS Gen 2                | abfs            | `<container>@<storage_account>.dfs.core.windows.net`  | Azure SQL Data Warehouse                    |
| SQL Server                | sqlserver       | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| Oracle                    | oracle          | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| Teradata                  | teradata        | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| MongoDB or Azure CosmosDB | mongodb         | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| ODBC                      | odbc            |                                                       | SQL Server (2019+)                          |
| Bulk Operations           | https           | `<storage_account>.blob.core.windows.net/<container>` | SQL Server (2017+), Azure SQL Database      |
| Elastic Query (shard)     | Not required    | `<shard_map_server_name>.database.windows.net`        | Azure SQL Database                          |
| Elastic Query (remote)    | Not required    | `<remote_server_name>.database.windows.net`           | Azure SQL Database                          |

Location path parameters:

- `Namenode` = the machine name or IP address of the Namenode in the Hadoop cluster.
- `port` = The port that the external data source is listening on. For Hadoop this can be found using the `fs.default.name` configuration parameter in Hadoop. If the value is not specified, 8020 will be used by default.
- `<storage_account>` = the storage account name of the azure resource
- `<container>` = the container of the storage account holding the data. Root containers are read-only, so data cannot be written back to the container.
- `<shard_map_server_name>` = The logical server name in Azure that is hosting the shard map manager. The `DATABASE_NAME` argument provides the database used to host the shard map and `SHARD_MAP_NAME` is used for the shard map itself.
- `<remote_server_name>` = The target logical server name for the elastic query. The database name is specified using the `DATABASE_NAME` argument.

Additional notes and guidance when setting the location:

- The `LOCATION` path must be valid as it is validated at creation time.
- Do not put a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL.
- You can use the `sqlserver` location prefix when connecting from SQL Server 2019 to also connect to Azure SQL Database or Azure SQL Data Warehouse.
- `wasb` is the default protocol for Azure blob storage. `wasbs` is optional but strongly recommended as data will be sent using a secure SSL connection.
- To ensure successful PolyBase queries in the event of Hadoop Namenode fail-over, consider using a virtual IP address for the Namenode of the Hadoop cluster. If you do not use a virtual IP address for the Hadoop Namenode, in the event of a Hadoop Namenode fail-over you will have to ALTER EXTERNAL DATA SOURCE object to point to the new location.

### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load from Azure Blob storage into SQL DW or Parallel Data Warehouse, the secret must be the Azure Storage Key.
- `CREDENTIAL` is only required if the blob has been secured. `CREDENTIAL` is not required for public data sets that allow anonymous access.
- When the `TYPE` = `BLOB_STORAGE` the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. Furthermore, the SAS token should be configured as follows:
  - Exclude the leading `?` when configured as the secret
  - Have at least read permission on the file that should be loaded (for example `srt=o&sp=r`)
  - the expiration period should be valid (all dates are in UTC time).

For an example of using a `CREDENTIAL` with `SHARED ACCESS SIGNATURE` and `TYPE` = `BLOB_STORAGE` refer to [I. Create an external data source to perform bulk operations and retrieve data from Azure Blob Storage into Azure SQL Database](#j.-create-an-external-data-source-for-bulk-operations-retrieving-data-from-Azure-Blob-storage)

To create a credential, see [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md).

### TYPE = *[ HADOOP | BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER]*

Specifies the type of the external data source being configured. This parameter is not always required.

- Use HADOOP when the external data source is Cloudera, Hortonworks, Azure Blob Storage, Azure Data Lake Store Gen 1 or Azure Data Lake Store Gen 2.
- Use RDBMS for cross-database queries using elastic query from Azure SQL Database.  
- Use SHARD_MAP_MANAGER when creating an external data source when connecting to a sharded Azure SQL Database.
- Use BLOB_STORAGE when performing bulk operations using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) with [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)].

> [!IMPORTANT]
> Do not set `TYPE` if using any other external data source.

For an example of using `TYPE` = `HADOOP` to load data from Azure Blob Storage refer to ["Create external data source to reference Azure blob storage"](#d.-create-external-data-source-to-reference-azure-blob-storage).

### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

Configure this optional value when connecting to Hortonworks or Cloudera.

When the RESOURCE_MANAGER_LOCATION is defined, the query optimizer will consider optimizing each query by initiating a map reduce job on the external Hadoop source and pushing down computation. This is entirely a cost-based decision and can significantly reduce the volume of data transferred between Hadoop and SQL, and therefore improve query performance.  

If the resource manager is not specified, pushing compute to Hadoop is disabled for PolyBase queries.

If the port is not specified, the default value is determined using the current setting for 'hadoop connectivity' configuration.

| Hadoop Connectivity | Default Resource Manager Port |
| ------------------- | ----------------------------- |
| 1                   | 50300                         |
| 2                   | 50300                         |
| 3                   | 8021                          |
| 4                   | 8032                          |
| 5                   | 8050                          |
| 6                   | 8032                          |
| 7                   | 8050                          |

For a complete list of Hadoop distributions and versions supported by each connectivity value, see [PolyBase Connectivity Configuration (Transact-SQL)](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md).
  
> [!IMPORTANT]  
> The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

For an example showing the `RESOURCE_MANAGER_LOCATION` enabled refer to ["Create external data source to reference Hadoop with push-down enabled"](B.-create-external-data-source-to-reference-Hadoop-with-push-down-enabled)

### DATABASE_NAME = *database_name*

Configure this argument when the `TYPE` is set to `RDBMS` or `SHARD_MAP_MANAGER`.

| TYPE              | Value of DATABASE_NAME                                                  |
| ----------------- | ----------------------------------------------------------------------- |
| RDBMS             | The name of the remote database on the server provided using `LOCATION` |
| SHARD_MAP_MANAGER | Name of the database operating as the shard map manager               |

For an example showing how to create an external data source where `TYPE` = `RDBMS` refer to ["Create an RDBMS external data source"](F.-create-an-rdbms-external-data-source)

### SHARD_MAP_NAME = *shard_map_name*

Used when the `TYPE` argument is set to `SHARD_MAP_MANAGER` only to set the name of the shard map.

For an example showing how to create an external data source where `TYPE` = `SHARD_MAP_MANAGER` refer to ["Create a shard map manager external data source"](E.-create-a-shard-map-manager-external-data-source)

## Permissions

Requires CONTROL permission on database in SQL Server, Parallel Data Warehouse, Azure SQL Database and Azure SQL Data Warehouse.

> [!IMPORTANT]
> In previous releases of PDW, create external data source required ALTER ANY EXTERNAL DATA SOURCE permissions.

## Error Handling

A runtime error will occur if the external Hadoop data sources are inconsistent about having RESOURCE_MANAGER_LOCATION defined. That is, you cannot specify two external data sources that reference the same Hadoop cluster and then providing resource manager location for one and not for the other.  
  
The SQL engine does not verify the existence of the external data source when it creates the external data source object. If the data source does not exist during query execution, an error will occur.

## Limitations and Restrictions

All data sources defined on the same Hadoop cluster location must use the same setting for RESOURCE_MANAGER_LOCATION. If there is inconsistency, a runtime error will occur.

If the Hadoop cluster is set up with a name and the external data source uses the IP address for the cluster location, PolyBase must still be able to resolve the cluster name when the data source is used. To resolve the name, you must enable a DNS forwarder.

For PolyBase, the external data source is database-scoped in SQL Server and SQL Data Warehouse. It is server-scoped in Parallel Data Warehouse.
  
## Locking

Takes a shared lock on the EXTERNAL DATA SOURCE object.  
  
## Examples: SQL Server (2016+) and Parallel Data Warehouse

### A. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks or Cloudera Hadoop cluster, specify the machine name or IP address of the Hadoop Namenode and port.  
  
```sql  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
(    LOCATION = 'hdfs://10.10.10.10:8050'
,    TYPE     = HADOOP
)
;
```

### B. Create external data source to reference Hadoop with push-down enabled

Specify the RESOURCE_MANAGER_LOCATION option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase uses a cost-based decision to determine whether the query computation should be pushed to Hadoop or all the data should be moved to process the query in SQL Server.
  
```sql  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
(    LOCATION                  = 'hdfs://10.10.10.10:8020'
,    TYPE                      = HADOOP
,    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
)
;
```

### C. Create external data source to reference Kerberos-secured Hadoop

To verify if the Hadoop cluster is Kerberos-secured, check the value of hadoop.security.authentication property in Hadoop core-site.xml. To reference a Kerberos-secured Hadoop cluster, you must specify a database scoped credential that contains your Kerberos username and password. The database master key is used to encrypt the database scoped credential secret.
  
```sql  
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo'
;

-- Create a database scoped credential with Kerberos user name and password.
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
WITH
     IDENTITY   = '<hadoop_user_name>'
,    SECRET     = '<hadoop_password>'
;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
(    LOCATION                  = 'hdfs://10.10.10.10:8050'
,    CREDENTIAL                = HadoopUser1
,    TYPE                      = HADOOP
,    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
)
;
```

## Examples: SQL Server (2016+), Azure SQL Data Warehouse and Parallel Data Warehouse

### D. Create external data source to reference Azure blob storage

In this example, the external data source is an Azure blob storage container called `daily` under Azure storage account named `logs`. The Azure storage external data source is for data transfer only; and it does not support predicate push-down.

This example shows how to create the database scoped credential for authentication to Azure storage. Specify the Azure storage account key in the database credential secret. Specify any string in database scoped credential identity, it is not used for authentication to Azure storage. Then, the credential is used in the statement that creates an external data source.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo'
;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
     IDENTITY   = '<my_account>'
,    SECRET     = '<azure_storage_account_key>'
;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
(    LOCATION   = 'wasbs://daily@logs.blob.core.windows.net/'
,    CREDENTIAL = AzureStorageCredential
,    TYPE       = HADOOP
)
;
```

## Examples: Azure SQL Database

### E. Create a Shard map manager external data source

To create an external data source to reference a SHARD_MAP_MANAGER, specify the SQL Database server name that hosts the shard map manager in Azure SQL Database or a SQL Server database on an Azure virtual machine.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
;

CREATE DATABASE SCOPED CREDENTIAL ElasticDBQueryCred
WITH
     IDENTITY   = '<username>'
,    SECRET     = '<password>'
;

CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc
WITH
(    TYPE             = SHARD_MAP_MANAGER
,    LOCATION         = '<server_name>.database.windows.net'
,    DATABASE_NAME    = 'ElasticScaleStarterKit_ShardMapManagerDb'
,    CREDENTIAL       = ElasticDBQueryCred
,    SHARD_MAP_NAME   = 'CustomerIDShardMap'
)
;
```

For a step-by-step tutorial, see [Getting started with elastic queries for sharding (horizontal partitioning)](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started/).

### F. Create an RDBMS external data source

To create an external data source to reference a RDBMS, specifies the SQL Database server name of the remote database in Azure SQL Database.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
;

CREATE DATABASE SCOPED CREDENTIAL SQL_Credential  
WITH
     IDENTITY  = '<username>'
,    SECRET    = '<password>'
;

CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc
WITH
(    TYPE          = RDBMS
,    LOCATION      = '<server_name>.database.windows.net'
,    DATABASE_NAME = 'Customers'
,    CREDENTIAL    = SQL_Credential
)
;
```

For a step-by-step tutorial on RDBMS, see [Getting started with cross-database queries (vertical partitioning)](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started-vertical/).

## Examples: Azure SQL Data Warehouse

### G. Create external data source to reference Azure Data Lake Store Gen 1

Azure Data lake Store connectivity is based on your ADLS URI and your Azure Active directory Application's service principle. Documentation for creating this application can be found at[Data lake store authentication using Active Directory](https://docs.microsoft.com/azure/data-lake-store/data-lake-store-authenticate-using-active-directory).

```sql
-- If you do not have a Master Key on your DW you will need to create one.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
;

-- These values come from your Azure Active Directory Application used to authenticate to ADLS
CREATE DATABASE SCOPED CREDENTIAL ADLS_credential
WITH
--   IDENTITY   = '<clientID>@<OAuth2.0TokenEndPoint>'
     IDENTITY   = '536540b4-4239-45fe-b9a3-629f97591c0c@https://login.microsoftonline.com/42f988bf-85f1-41af-91ab-2d2cd011da47/oauth2/token'
--,  SECRET     = '<KEY>'
,    SECRET     = 'BjdIlmtKp4Fpyh9hIvr8HJlUida/seM5kQ3EpLAmeDI='
;
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH
(    LOCATION       = 'adl://newyorktaxidataset.azuredatalakestore.net'
,    CREDENTIAL     = ADLS_credential
,    TYPE           = HADOOP
)
;
```

### H. Create external data source to reference Azure Data Lake Store (ADLS) Gen 2

```sql
-- If you do not have a Master Key on your DW you will need to create one.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
;

-- These values come from your Azure Active Directory Application used to authenticate to ADLS
CREATE DATABASE SCOPED CREDENTIAL ADLS_credential
WITH
--   IDENTITY   = '<clientID>@<OAuth2.0TokenEndPoint>'
     IDENTITY   = '536540b4-4239-45fe-b9a3-629f97591c0c@https://login.microsoftonline.com/42f988bf-85f1-41af-91ab-2d2cd011da47/oauth2/token'
--,  SECRET     = '<KEY>'
,    SECRET     = 'BjdIlmtKp4Fpyh9hIvr8HJlUida/seM5kQ3EpLAmeDI='
;

CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
(    LOCATION   = 'abfs://2013@newyorktaxidataset.dfs.core.windows.net'
,    CREDENTIAL = ADLS_credential
,    TYPE       = HADOOP
)
[;]
```

## Examples: Bulk Operations

### I. Create an external data source for bulk operations retrieving data from Azure Blob storage

**Applies to:** [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)].
Use the following data source for bulk operations using [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) or [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md). The credential used, must be created using `SHARED ACCESS SIGNATURE` as the identity, should not have the leading `?` in SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1).

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
     IDENTITY = 'SHARED ACCESS SIGNATURE'
,    SECRET = '(REMOVE ? FROM THE BEGINNING)******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************'
;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
(    LOCATION   = 'https://newinvoices.blob.core.windows.net/week3'
,    CREDENTIAL = AccessAzureInvoices
,    TYPE       = BLOB_STORAGE
)
;
```

To see this example in use, see [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md#f-importing-data-from-a-file-in-azure-blob-storage).

## See Also

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/alter-external-data-source-transact-sql.md)
- [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)
- [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE EXTERNAL TABLE AS SELECT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-as-select-transact-sql.md)
- [CREATE TABLE AS SELECT &#40;Azure SQL Data Warehouse&#41;](../../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md)
- [sys.external_data_sources (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md)
- [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1).
- [Introduction to elastic query](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-overview/)