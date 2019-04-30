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

Creates an external data source for PolyBase or Elastic Database queries. External data sources are used to establish connectivity and support four primary use cases:

- Data virtualization and data load using [PolyBase][intro_pb]
- Bulk load operations using SQL Server or SQL Database using `BULK INSERT` or `OPENROWSET`
- Query remote SQL Database or SQL Data Warehouse instances using SQL Database with [elastic query][remote_eq]
- Query a sharded Azure SQL Database using [elastic query][sharded_eq]

> [!NOTE]  
> PolyBase is supported on SQL Server (2016 or higher), Azure SQL Data Warehouse and Parallel Data Warehouse. [Elastic queries][intro_eq] are supported only on Azure SQL Database v12 or later.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax  

```sql
CREATE EXTERNAL DATA SOURCE <data_source_name>  
WITH
(    LOCATION                  = '<prefix>://<path>[:<port>]'
[,   CONNECTION_OPTIONS        = '<name_value_pairs>']
[,   CREDENTIAL                = <credential_name> ]
[,   PUSHDOWN                  = ON | OFF]
[,   TYPE                      = HADOOP | BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER ]
[,   RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]']
[,   DATABASE_NAME             = '<database_name>' ]
[,   SHARD_MAP_NAME            = '<shard_map_manager>' ]
)
[;]
```

## Arguments

### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in SQL Server, SQL Database (SQL DB), and SQL Data Warehouse (SQL DW). The name must be unique within the server in Parallel Data Warehouse (PDW).

### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source        | Location prefix | Location path                                         | Supported locations by product / service    |
| --------------------------- | --------------- | ----------------------------------------------------- | ------------------------------------------- |
| Cloudera or Hortonworks     | `hdfs`          | `<Namenode>[:port]`                                   | SQL Server (2016+), PDW                     |
| Azure Blob Storage          | `wasb[s]`       | `<container>@<storage_account>.blob.core.windows.net` | SQL Server (2016+), PDW, SQL DW             |
| Azure Data Lake Store Gen 1 | `adl`           | `<storage_account>.azuredatalake.net`                 | SQL DW                                      |
| Azure Data Lake Store Gen 2 | `abfs`          | `<container>@<storage_account>.dfs.core.windows.net`  | SQL DW                                      |
| SQL Server                  | `sqlserver`     | `<server_name>[\<instance_name>][:port]`              | SQL Server (2019+)                          |
| Oracle                      | `oracle`        | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| Teradata                    | `teradata`      | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| MongoDB or CosmosDB         | `mongodb`       | `<server_name>[:port]`                                | SQL Server (2019+)                          |
| ODBC                        | `odbc`          | `<server_name>{:port]`                                | SQL Server (2019+) - Windows only           |
| Bulk Operations             | `https`         | `<storage_account>.blob.core.windows.net/<container>` | SQL Server (2017+), SQL DB                  |
| Elastic Query (shard)       | Not required    | `<shard_map_server_name>.database.windows.net`        | SQL DB                                      |
| Elastic Query (remote)      | Not required    | `<remote_server_name>.database.windows.net`           | SQL DB                                      |

Location path:

- `<`Namenode`>` = the machine name, name service URI, or IP address of the `Namenode` in the Hadoop cluster. PolyBase must resolve any DNS names used by the Hadoop cluster. <!-- For highly available Hadoop configurations, provide the Nameservice ID as the `LOCATION`. -->
- `port` = The port that the external data source is listening on. In Hadoop, the port can be found using the `fs.default.name` configuration parameter. The default is 8020.
- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the azure resource.
- `<server_name>` = the host name.
- `<instance_name>` = the name of the SQL Server named instance. Used if you have SQL Server Browser Service running on the target instance.
- `<shard_map_server_name>` = The logical server name in Azure that is hosting the shard map manager. The `DATABASE_NAME` argument provides the database used to host the shard map and `SHARD_MAP_NAME` is used for the shard map itself.
- `<remote_server_name>` = The target logical server name for the elastic query. The database name is specified using the `DATABASE_NAME` argument.

Additional notes and guidance when setting the location:

- The SQL engine doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- You can use the `sqlserver` location prefix to connect SQL Server 2019 to SQL Server, SQL Database, or SQL Data Warehouse.
- Specify the `Driver={<Name of Driver>}` when connecting via `ODBC`
- `wasb` is the default protocol for Azure blob storage. `wasbs` is optional but recommended as data will be sent using a secure SSL connection.
- To ensure successful PolyBase queries during a Hadoop `Namenode` fail-over, consider using a virtual IP address for the `Namenode` of the Hadoop cluster. If you don't, execute an [ALTER EXTERNAL DATA SOURCE][alter_eds] command to point to the new location.

### CONNECTION_OPTIONS = *key_value_pair*

Specifies additional options when connecting over `ODBC` to an external data source.

The name of the driver is required as a minimum but there are other options such as `APP='<your_application_name>'` or `ApplicationIntent= ReadOnly|ReadWrite` which are also useful to set and can assist with troubleshooting.

Refer to the `ODBC` product documentation for a list of permitted [CONNECTION_OPTIONS][]

### PUSHDOWN = *ON | OFF*

States whether computation can be pushed down to the external data source. It is on by default.

`PUSHDOWN` is supported when connecting to SQL Server, Oracle, Teradata, MongoDB, or ODBC at the external data source level.

Enabling or disabling push-down at the query level is achieved through a [hint][hint_pb].

### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load data from either Azure Blob storage or Azure Data Lake Store (ADLS) Gen 2 into SQL DW or PDW, use an Azure Storage Key.
- `CREDENTIAL` is only required if the blob has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- When the `TYPE` = `BLOB_STORAGE` the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. Furthermore, the SAS token should be configured as follows:
  - Exclude the leading `?` when configured as the secret
  - Have at least read permission on the file that should be loaded (for example `srt=o&sp=r`)
  - the expiration period should be valid (all dates are in UTC time).

For an example of using a `CREDENTIAL` with `SHARED ACCESS SIGNATURE` and `TYPE` = `BLOB_STORAGE` refer to [Create an external data source to execute bulk operations and retrieve data from Azure Blob Storage into SQL Database](#J.-Create-an-external-data-source-for-bulk-operations-retrieving-data-from-Azure-Blob-storage)

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][CREATE_DATABASE_SCOPED_CREDENTIAL].

### TYPE = *[ HADOOP | BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER]*

Specifies the type of the external data source being configured. This parameter isn't always required.

- Use HADOOP when the external data source is Cloudera, Hortonworks, Azure Blob Storage, ADLS Gen 1, or ADLS Gen 2.
- Use RDBMS for cross-database queries using elastic query from SQL Database.  
- Use SHARD_MAP_MANAGER when creating an external data source when connecting to a sharded SQL Database.
- Use BLOB_STORAGE when executing bulk operations with [BULK INSERT][bulk_insert], or [OPENROWSET][openrowset] with [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)].

> [!IMPORTANT]
> Do not set `TYPE` if using any other external data source.

For an example of using `TYPE` = `HADOOP` to load data from Azure Blob Storage, see [Create external data source to reference Azure blob storage](#E.-create-external-data-source-to-reference-azure-blob-storage).

### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

Configure this optional value when connecting to Hortonworks or Cloudera.

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and SQL, which can lead to improved query performance.  

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries.

If the port isn't specified, the default value is chosen using the current setting for 'hadoop connectivity' configuration.

| Hadoop Connectivity | Default Resource Manager Port |
| ------------------- | ----------------------------- |
| 1                   | 50300                         |
| 2                   | 50300                         |
| 3                   | 8021                          |
| 4                   | 8032                          |
| 5                   | 8050                          |
| 6                   | 8032                          |
| 7                   | 8050                          |

For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].
  
> [!IMPORTANT]  
> The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

[Create external data source to reference Hadoop with push-down enabled](#C.-Create-external-data-source-to-reference-Hadoop-with-push-down-enabled) provides a concrete example and further guidance.

### DATABASE_NAME = *database_name*

Configure this argument when the `TYPE` is set to `RDBMS` or `SHARD_MAP_MANAGER`.

| TYPE              | Value of DATABASE_NAME                                                  |
| ----------------- | ----------------------------------------------------------------------- |
| RDBMS             | The name of the remote database on the server provided using `LOCATION` |
| SHARD_MAP_MANAGER | Name of the database operating as the shard map manager                 |

For an example showing how to create an external data source where `TYPE` = `RDBMS` refer to [Create an RDBMS external data source](#G.-create-an-rdbms-external-data-source)

### SHARD_MAP_NAME = *shard_map_name*

Used when the `TYPE` argument is set to `SHARD_MAP_MANAGER` only to set the name of the shard map.

For an example showing how to create an external data source where `TYPE` = `SHARD_MAP_MANAGER` refer to [Create a shard map manager external data source](#F.-create-a-shard-map-manager-external-data-source)

## Permissions

Requires CONTROL permission on database in SQL Server, Parallel Data Warehouse, SQL Database, and SQL Data Warehouse.

> [!NOTE]
> In previous releases of PDW, create external data source required ALTER ANY EXTERNAL DATA SOURCE permissions.

## Locking

Takes a shared lock on the EXTERNAL DATA SOURCE object.  

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

When you connect to the storage or data pool in a Big Data Cluster, the user's credentials are passed through to the back-end system. Create logins in the data pool itself to enable pass through authentication.

Currently a SAS token with type `HADOOP` is unsupported. It's only supported with a storage account access key. Attempting to create an external data source with type `HADOOP` and a SAS credential fails with the following error:

`Msg 105019, Level 16, State 1 - EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect. Java exception message: Parameters provided to connect to the Azure storage account are not valid.: Error [Parameters provided to connect to the Azure storage account are not valid.] occurred while accessing external file.'`

## Examples: SQL Server (2016+) and Parallel Data Warehouse

### A. Create external data source in SQL 2019 to reference Oracle

To create an external data source that references Oracle, ensure you have a database scoped credential. You may optionally also enable or disable push-down of computation against this data source.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '!MyC0mpl3xP@ssw0rd!
;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL OracleProxyAccount
WITH
     IDENTITY   = 'oracle_username'
,    SECRET     = 'oracle_password'
;

CREATE EXTERNAL DATA SOURCE MyOracleServer
WITH
(    LOCATION   = 'oracle://145.145.145.145:1521'
,    CREDENTIAL = OracleProxyAccount
,    PUSHDOWN   = ON
;
```

### B. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks or Cloudera Hadoop cluster, specify the machine name, or IP address of the Hadoop `Namenode` and port. <!-- Provide the Nameservice ID as the `LOCATION` for highly available configurations. -->
  
```sql  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
(    LOCATION = 'hdfs://10.10.10.10:8050'
,    TYPE     = HADOOP
)
;
```

### C. Create external data source to reference Hadoop with push-down enabled

Specify the `RESOURCE_MANAGER_LOCATION` option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase makes a cost-based decision to determine whether the query computation should be pushed to Hadoop.
  
```sql  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
(    LOCATION                  = 'hdfs://10.10.10.10:8020'
,    TYPE                      = HADOOP
,    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
)
;
```

### D. Create external data source to reference Kerberos-secured Hadoop

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

## Examples: SQL Server (2016+), SQL Data Warehouse, and Parallel Data Warehouse

### E. Create external data source to reference Azure blob storage

In this example, the external data source is an Azure blob storage container called `daily` under Azure storage account named `logs`. The Azure storage external data source is for data transfer only. It doesn't support predicate push-down.

This example shows how to create the database scoped credential for authentication to Azure storage. Specify the Azure storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure storage.

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

## Examples: SQL Database

### F. Create a Shard map manager external data source

To create an external data source to reference a SHARD_MAP_MANAGER, specify the SQL Database server name that hosts the shard map manager in SQL Database or a SQL Server database on a virtual machine.

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

For a step-by-step tutorial, see [Getting started with elastic queries for sharding (horizontal partitioning)][sharded_eq_tutorial].

### G. Create an RDBMS external data source

To create an external data source to reference an RDBMS, specifies the SQL Database server name of the remote database in SQL Database.

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

For a step-by-step tutorial on RDBMS, see [Getting started with cross-database queries (vertical partitioning)][remote_eq_tutorial].

## Examples: SQL Data Warehouse

### H. Create external data source to reference Azure Data Lake Store Gen 1

Azure Data lake Store connectivity is based on your ADLS URI and your Azure Active directory Application's service principle. Documentation for creating this application can be found at[Data lake store authentication using Active Directory][azure_ad[].

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

### I. Create external data source to reference Azure Data Lake Store (ADLS) Gen 2

Connecting to ADLS Gen 2 requires the storage account key as the secret for the database scoped credential. Oauth2.0 support isn't available at this time.

```sql
-- If you do not have a Master Key on your DW you will need to create one.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
;

-- These values come from your Azure Active Directory Application used to authenticate to ADLS
CREATE DATABASE SCOPED CREDENTIAL ADLS_credential
WITH
--   IDENTITY   = '<storage_account_name>'
     IDENTITY   = 'newyorktaxidata'
--,  SECRET     = '<storage_account_key>'
,    SECRET     = 'yz5N4+bxSb89McdiysJAzo+9hgEHcJRJuXbF/uC3mhbezES/oe00vXnZEl14U0lN3vxrFKsphKov16C0w6aiTQ=='
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

> [!NOTE]
> Do not put a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

### J. Create an external data source for bulk operations retrieving data from Azure Blob storage

**Applies to:** [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)].
Use the following data source for bulk operations using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. The credential must set `SHARED ACCESS SIGNATURE` as the identity, mustn't have the leading `?` in the SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)][sas_token].

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
     IDENTITY = 'SHARED ACCESS SIGNATURE'
--   REMOVE ? FROM THE BEGINNING OF THE SAS TOKEN
,    SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************'
;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
(    LOCATION   = 'https://newinvoices.blob.core.windows.net/week3'
,    CREDENTIAL = AccessAzureInvoices
,    TYPE       = BLOB_STORAGE
)
;
```

To see this example in use, see [BULK INSERT][bulk_insert_example].

## See Also

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)][alter_eds]
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [CREATE EXTERNAL TABLE AS SELECT (Azure SQL Data Warehouse)][create_etb_as_sel]
- [CREATE TABLE AS SELECT (Azure SQL Data Warehouse)][create_tbl_as_sel]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]
- [Introduction to elastic query][intro_eq]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: https://docs.microsoft.com/sql/t-sql/statements/bulk-insert-transact-sql
[bulk_insert_example]: https://docs.microsoft.com/sql/t-sql/statements/bulk-insert-transact-sql#f-importing-data-from-a-file-in-azure-blob-storage
[openrowset]: https://docs.microsoft.com/sql/t-sql/functions/openrowset-transact-sql

[create_dsc]: https://docs.microsoft.com/sql/t-sql/statements/create-database-scoped-credential-transact-sql
[create_eff]: https://docs.microsoft.com/sql/t-sql/statements/create-external-file-format-transact-sql
[create_etb]: https://docs.microsoft.com/sql/t-sql/statements/create-external-table-transact-sql
[create_etb_as_sel]: https://docs.microsoft.com/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azure-sqldw-latest
[create_tbl_as_sel]: https://docs.microsoft.com/sql/t-sql/statements/create-table-as-select-azure-sql-data-warehouse?view=azure-sqldw-latest

[alter_eds]: https://docs.microsoft.com/sql/t-sql/statements/alter-external-data-source-transact-sql

[cat_eds]: https://docs.microsoft.com/sql/relational-databases/system-catalog-views/sys-external-data-sources-transact-sql
<!-- PolyBase docs -->
[intro_pb]: https://docs.microsoft.com/sql/relational-databases/polybase/polybase-guide
[connectivity_pb]:https://docs.microsoft.com/sql/database-engine/configure-windows/polybase-connectivity-configuration-transact-sql
[CONNECTION_OPTIONS]: https://docs.microsoft.com/sql/relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client
[hint_pb]: https://docs.microsoft.com/sql/relational-databases/polybase/polybase-pushdown-computation#force-pushdown
<!-- Elastic Query Docs -->
[intro_eq]: https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-overview/
[remote_eq]: https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started-vertical/
[remote_eq_tutorial]: https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started-vertical/
[sharded_eq]: https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started/
[sharded_eq_tutorial]: https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-getting-started/

<!-- Azure Docs -->
[azure_ad]: https://docs.microsoft.com/azure/data-lake-store/data-lake-store-authenticate-using-active-directory
[sas_token]: https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1