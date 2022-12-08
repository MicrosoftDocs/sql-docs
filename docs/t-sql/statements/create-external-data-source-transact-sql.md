---
title: "CREATE EXTERNAL DATA SOURCE (Transact-SQL)"
description: CREATE EXTERNAL DATA SOURCE creates an external data source used to establish connectivity and data virtualization from SQL Server and Azure SQL platforms.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/31/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "CREATE EXTERNAL DATA SOURCE"
  - "CREATE_EXTERNAL_DATA_SOURCE"
helpviewer_keywords:
  - "External"
  - "External, data source"
  - "PolyBase, create data source"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azuresqledge-current"
---

# CREATE EXTERNAL DATA SOURCE (Transact-SQL)

Creates an external data source for querying using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)], or Azure SQL Edge.

This article provides the syntax, arguments, remarks, permissions, and examples for whichever SQL product you choose.

[!INCLUDE [select-product](../includes/select-product.md)]

<!-- In addition to moniker ranges for SQL Server, SQL DB, APS, Synapse, and SQL MI,  
     this article has version moniker ranges for SQL Server 2016, 2017 (Windows and Linux), 2019, and 2022 due to the syntax differences between each.  
     Use of the version selector above the TOC is important for this document.
     Pay attention to each ::: moniker range.-->
<!-- At this time the Azure SQL Edge moniker azuresqledge-current is not functional in sql-docs.  
     Per PMs, we have added Azure SQL Edge content to Azure SQL DB range. -->

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](create-external-data-source-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed<br />Instance](create-external-data-source-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-external-data-source-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-external-data-source-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

::: moniker-end

::: moniker range="=sql-server-2016"

## Overview: SQL Server 2016

[!INCLUDE[SQL2016+](../../includes/applies-to-version/sqlserver2016.md)]

Creates an external data source for PolyBase queries. External data sources are used to establish connectivity and support these primary use cases:

- Data virtualization and data load using [PolyBase][intro_pb]
- Bulk load operations using `BULK INSERT` or `OPENROWSET`

> [!NOTE]  
> This syntax varies in different versions of SQL Server. Use the version selector dropdown to choose the appropriate version.  
> To view the features of [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true#syntax).
> To view the features of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#syntax).

## <a id="syntax"></a> Syntax for SQL Server 2016

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] TYPE = { HADOOP } ]
    [ [ , ] RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]' )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source    | Connector location prefix | Location path                                         | Supported locations by product / service | Authentication |
| ----------------------- | --------------- | ----------------------------------------------------- | ---------------------------------------- | --:|
| Cloudera CDH or Hortonworks HDP | `hdfs`  | `<Namenode>[:port]`           | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] to [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]  | Anonymous or basic authentication |
| Azure Storage account(V2) | `wasb[s]`  | `<container>@<storage_account>.blob.core.windows.net` | Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]<br />Hierarchical Namespace **not** supported |  Azure Storage account key |

Location path:

- `<Namenode>` = the machine name, name service URI, or IP address of the `Namenode` in the Hadoop cluster. PolyBase must resolve any DNS names used by the Hadoop cluster. <!-- For highly available Hadoop configurations, provide the Nameservice ID as the `LOCATION`. -->
- `port` = The port that the external data source is listening on. In Hadoop, the port can be found using the `fs.defaultFS` configuration parameter. The default is 8020.
- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.
- `<server_name>` = the host name.
- `<instance_name>` = the name of the SQL Server named instance. Used if you have SQL Server Browser Service running on the target instance.

Additional notes and guidance when setting the location:

- The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- `wasbs` is optional but recommended in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] for accessing Azure Storage Accounts as data will be sent using a secure TLS/SSL connection.
- To ensure successful PolyBase queries during a Hadoop `Namenode` fail-over, consider using a virtual IP address for the `Namenode` of the Hadoop cluster. If you don't, execute an [ALTER EXTERNAL DATA SOURCE][alter_eds] command to point to the new location.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

`CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

#### TYPE = *[ HADOOP ]*

Specifies the type of the external data source being configured. In SQL Server 2016, this parameter is always required, and should only be specified as `HADOOP`. Supports connections to Cloudera CDH, Hortonworks HDP, or an Azure Storage account. The behavior of this parameter is different in later versions of SQL Server.

For an example of using `TYPE` = `HADOOP` to load data from an Azure Storage account, see [Create external data source to access data in Azure Storage using the wasb:// interface](#e-create-external-data-source-to-access-data-in-azure-storage-using-the-wasb-interface) <!--[Create external data source to reference Azure Storage](#e-create-external-data-source-to-reference-azure-storage).-->

#### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

Configure this optional value when connecting to Cloudera CDH, Hortonworks HDP, or an Azure Storage account only. For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which can lead to improved query performance.

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries. [Create external data source to reference Hadoop with push-down enabled](#c-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.

The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

In order for PolyBase to function correctly with a Hadoop external data source, the ports for the following Hadoop cluster components must be open:

- HDFS ports
    - Namenode
    - Datanode
- Resource Manager
    - Job submission
    - Job history

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
| 8                   | 8032                          |

The following table shows the default ports for these components. There is Hadoop version dependency as well as the possibility of custom configuration that doesn't use the default port assignment.

| **Hadoop cluster component** | **Default Port** |
| :-- | :-- |
| NameNode | 8020 |
| DataNode (Data transfer, non-privilege IPC port) | 50010 |
| DataNode (Data transfer, privilege IPC port) | 1019 |
| Resource Manager Job Submission (Hortonworks 1.3)| 50300|
| Resource Manager Job Submission (Cloudera 4.3)|8021|
| Resource Manager Job Submission (Hortonworks 2.0 on Windows, Cloudera 5.x on Linux) | 8032 |
| Resource Manager Job Submission (Hortonworks 2.x, 3.0 on Linux, Hortonworks 2.1-3 on Windows) | 8050|
| Resource Manager Job History | 10020|

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

## Examples

> [!IMPORTANT]  
> For information on how to install and enable PolyBase, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md)

### A. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks HDP or Cloudera CDH Hadoop cluster, specify the machine name, or IP address of the Hadoop `Namenode` and port. <!-- Provide the Nameservice ID as the `LOCATION` for highly available configurations. -->

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    TYPE = HADOOP
  ) ;
```

### B. Create external data source to reference Hadoop with push-down enabled

Specify the `RESOURCE_MANAGER_LOCATION` option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase makes a cost-based decision to determine whether the query computation should be pushed to Hadoop.

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8020' ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  ) ;
```

### C. Create external data source to reference Kerberos-secured Hadoop

To verify if the Hadoop cluster is Kerberos-secured, check the value of `hadoop.security.authentication` property in Hadoop core-site.xml. To reference a Kerberos-secured Hadoop cluster, you must specify a database scoped credential that contains your Kerberos username and password. The database master key is used to encrypt the database scoped credential secret.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Kerberos user name and password.
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
WITH
     IDENTITY = '<hadoop_user_name>',
     SECRET = '<hadoop_password>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    CREDENTIAL = HadoopUser1 ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  );
```

### D. Create external data source to access data in Azure Storage using the wasb:// interface

In this example, the external data source is an Azure V2 Storage account named `logs`. The storage container is called `daily`. The Azure Storage external data source is for data transfer only. It doesn't support predicate push-down. Hierarchical namespaces are not supported when accessing data via the `wasb://` interface.

This example shows how to create the database scoped credential for authentication to an Azure V2 Storage account. Specify the Azure Storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure Storage. Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], `TYPE` should be set to `HADOOP` even when accessing Azure Storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>' ,
  SECRET = '<azure_storage_account_key>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'wasbs://daily@logs.blob.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  ) ;
```

## Next steps

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)][alter_eds]
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [PolyBase Connectivity Configuration][connectivity_pb]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md

[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: ./create-external-table-transact-sql.md
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true

[alter_eds]: ./alter-external-data-source-transact-sql.md

[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]: ../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown

<!-- Azure Docs -->
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end

::: moniker range="=sql-server-2017||=sql-server-linux-2017"

## Overview: SQL Server 2017

[!INCLUDE[SQL2017 only](../../includes/applies-to-version/sqlserver2017-only.md)]

Creates an external data source for PolyBase queries. External data sources are used to establish connectivity and support these primary use cases:

- Data virtualization and data load using [PolyBase][intro_pb]
- Bulk load operations using `BULK INSERT` or `OPENROWSET`

::: moniker-end

::: moniker range="=sql-server-linux-2017"

> [!NOTE]  
> This syntax varies in different versions of SQL Server on Linux. Use the version selector dropdown to choose the appropriate version.  
> To view the features of [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-linux-ver15&preserve-view=true#syntax).
> To view the features of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-linux-ver16&preserve-view=true#syntax).

::: moniker-end

::: moniker range="=sql-server-2017"

> [!NOTE]  
> This syntax varies in different versions of SQL Server. Use the version selector dropdown to choose the appropriate version.  
> To view the features of [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true#syntax).
> To view the features of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#syntax).

::: moniker-end

::: moniker range="=sql-server-2017||=sql-server-linux-2017"

## <a id="syntax"></a> Syntax for SQL Server 2017

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] TYPE = { HADOOP | BLOB_STORAGE } ]
    [ [ , ] RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]' )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source    | Connector location prefix | Location path                                         | Supported locations by product / service | Authentication |
| ----------------------- | --------------- | ----------------------------------------------------- | ---------------------------------------- | --:|
| Cloudera CDH or Hortonworks HDP | `hdfs`  | `<Namenode>[:port]`     | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] to [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] only  | Anonymous or basic authentication|
| Azure Storage account(V2) | `wasb[s]`     | `<container>@<storage_account>.blob.core.windows.net` | Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]<br />Hierarchical Namespace **not** supported | Azure Storage account key |
| Bulk Operations         | `https`         | `<storage_account>.blob.core.windows.net/<container>` | Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]                        | Shared access signature (SAS)|

Location path:

- `<`Namenode`>` = the machine name, name service URI, or IP address of the `Namenode` in the Hadoop cluster. PolyBase must resolve any DNS names used by the Hadoop cluster. <!-- For highly available Hadoop configurations, provide the Nameservice ID as the `LOCATION`. -->
- `port` = The port that the external data source is listening on. In Hadoop, the port can be found using the `fs.defaultFS` configuration parameter. The default is 8020.
- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.
- `<server_name>` = the host name.
- `<instance_name>` = the name of the SQL Server named instance. Used if you have SQL Server Browser Service running on the target instance.

Additional notes and guidance when setting the location:

- The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- Specify the `Driver={<Name of Driver>}` when connecting via `ODBC`.
- `wasbs` is optional but recommended in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] for accessing Azure Storage Accounts as data will be sent using a secure TLS/SSL connection.
- To ensure successful PolyBase queries during a Hadoop `Namenode` fail-over, consider using a virtual IP address for the `Namenode` of the Hadoop cluster. If you don't, execute an [ALTER EXTERNAL DATA SOURCE][alter_eds] command to point to the new location.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- When the `TYPE` = `BLOB_STORAGE`, the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. 
- `TYPE` = `BLOB_STORAGE` is only permitted for bulk operations; you cannot create external tables for an external data source with `TYPE` = `BLOB_STORAGE`.
- Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).
- When `TYPE` = `HADOOP` the credential must be created using the storage account key as the `SECRET`.

There are multiple ways to create a shared access signature:
- You can create an SAS token by navigating to the **Azure portal -> <Your_Storage_Account> -> Shared access signature -> Configure permissions -> Generate SAS and connection string**. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal#generate-a-shared-access-signature).
- You can [create and configure an SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs#get-the-sas-for-a-blob-container).
- You can create an SAS programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json).
- The SAS token should be configured as follows:
  - When an SAS token is generated, it includes a question mark ('?') at the beginning of the token. Exclude the leading `?` when configured as the SECRET.
  - Use a valid expiration period (all dates are in UTC time).
  - Grant at least read permission on the file that should be loaded (for example `srt=o&sp=r`). Multiple shared access signatures can be created for different use cases. Permissions should be granted as follows:

  |    Action    |    Permission    |
  |    ------    |    ----------    |
  |    Read data from a file    |    Read    |
  |    Read data from multiple files and subfolders    |    Read and List    |
  |    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |

For an example of using a `CREDENTIAL` with `SHARED ACCESS SIGNATURE` and `TYPE` = `BLOB_STORAGE`, see [Create an external data source to execute bulk operations and retrieve data from Azure Storage into SQL Database](#c-create-an-external-data-source-for-bulk-operations-retrieving-data-from-azure-storage)

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

#### TYPE = *[ HADOOP | BLOB_STORAGE ]*

Specifies the type of the external data source being configured. This parameter isn't always required, and should only be specified when connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2.

- Use `HADOOP` when the external data source is Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2.
- Use `BLOB_STORAGE` when executing bulk operations from Azure Storage account using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. Introduced with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. Use `HADOOP` when intending to CREATE EXTERNAL TABLE against Azure Storage.

> [!NOTE]  
> `TYPE` should be set to `HADOOP` even when accessing Azure Storage.

For an example of using `TYPE` = `HADOOP` to load data from an Azure Storage account, see [Create external data source to access data in Azure Storage using the wasb:// interface](#e-create-external-data-source-to-access-data-in-azure-storage-using-the-wasb-interface) <!--[Create external data source to reference Azure Storage](#e-create-external-data-source-to-reference-azure-storage).-->

#### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

Configure this optional value when connecting to Cloudera CDH, Hortonworks HDP, or an Azure Storage account only. For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which can lead to improved query performance.

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries. [Create external data source to reference Hadoop with push-down enabled](#c-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.

The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

In order for PolyBase to function correctly with a Hadoop external data source, the ports for the following Hadoop cluster components must be open:

- HDFS ports
    - Namenode
    - Datanode
- Resource Manager
    - Job submission
    - Job history

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
| 8                   | 8032                          |

The following table shows the default ports for these components. Note that there is Hadoop version dependency as well as the possibility of custom configuration that doesn't use the default port assignment.

| **Hadoop cluster component** | **Default Port** |
| :-- | :-- |
| NameNode | 8020 |
| DataNode (Data transfer, non-privilege IPC port) | 50010 |
| DataNode (Data transfer, privilege IPC port) | 1019 |
| Resource Manager Job Submission (Hortonworks 1.3)| 50300|
| Resource Manager Job Submission (Cloudera 4.3)|8021|
| Resource Manager Job Submission (Hortonworks 2.0 on Windows, Cloudera 5.x on Linux) | 8032 |
| Resource Manager Job Submission (Hortonworks 2.x, 3.0 on Linux, Hortonworks 2.1-3 on Windows) | 8050|
| Resource Manager Job History | 10020|

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

An SAS token with type `HADOOP` is unsupported. It's only supported with type = `BLOB_STORAGE` when a storage account access key is used instead. Attempting to create an external data source with type `HADOOP` and a SAS credential fails with the following error:

`Msg 105019, Level 16, State 1 - EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect. Java exception message: Parameters provided to connect to the Azure storage account are not valid.: Error [Parameters provided to connect to the Azure storage account are not valid.] occurred while accessing external file.'`

## Examples

> [!IMPORTANT]  
> For information on how to install and enable PolyBase, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md)

### A. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks HDP or Cloudera CDH Hadoop cluster, specify the machine name, or IP address of the Hadoop `Namenode` and port. <!-- Provide the Nameservice ID as the `LOCATION` for highly available configurations. -->

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    TYPE = HADOOP
  ) ;
```

### B. Create external data source to reference Hadoop with push-down enabled

Specify the `RESOURCE_MANAGER_LOCATION` option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase makes a cost-based decision to determine whether the query computation should be pushed to Hadoop.

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8020' ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  ) ;
```

### C. Create external data source to reference Kerberos-secured Hadoop

To verify if the Hadoop cluster is Kerberos-secured, check the value of `hadoop.security.authentication` property in Hadoop core-site.xml. To reference a Kerberos-secured Hadoop cluster, you must specify a database scoped credential that contains your Kerberos username and password. The database master key is used to encrypt the database scoped credential secret.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Kerberos user name and password.
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
WITH
     IDENTITY = '<hadoop_user_name>',
     SECRET = '<hadoop_password>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    CREDENTIAL = HadoopUser1 ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  );
```

### D. Create external data source to access data in Azure Storage using the wasb:// interface

In this example, the external data source is an Azure V2 Storage account named `logs`. The storage container is called `daily`. The Azure Storage external data source is for data transfer only. It doesn't support predicate push-down. Hierarchical namespaces are not supported when accessing data via the `wasb://` interface. Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).

This example shows how to create the database scoped credential for authentication to an Azure V2 Storage account. Specify the Azure Storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure Storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>' ,
  SECRET = '<azure_storage_account_key>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'wasbs://daily@logs.blob.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  ) ;
```

## Examples: Bulk operations

> [!IMPORTANT]  
> Do not add a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

### E. Create an external data source for bulk operations retrieving data from Azure Storage

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later.

Use the following data source for bulk operations using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. The credential must set `SHARED ACCESS SIGNATURE` as the identity, mustn't have the leading `?` in the SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)][sas_token].

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE',
  -- Remove ? from the beginning of the SAS token
  SECRET = '<azure_storage_account_key>' ;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
  ( LOCATION = 'https://newinvoices.blob.core.windows.net/week3' ,
    CREDENTIAL = AccessAzureInvoices ,
    TYPE = BLOB_STORAGE
  ) ;
```

To see this example in use, see the [BULK INSERT][bulk_insert_example] example.

## Next steps

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)][alter_eds]
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]
- [PolyBase Connectivity Configuration][connectivity_pb]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md

[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: ./create-external-table-transact-sql.md
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true

[alter_eds]: ./alter-external-data-source-transact-sql.md

[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]: ../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown

<!-- Azure Docs -->
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end

::: moniker range="=sql-server-ver15 || =sql-server-linux-ver15"

## Overview: SQL Server 2019

[!INCLUDE[SQL2019](../../includes/applies-to-version/sqlserver2019.md)] and later

Creates an external data source for PolyBase queries. External data sources are used to establish connectivity and support these primary use cases:

- Data virtualization and data load using [PolyBase][intro_pb]
- Bulk load operations using `BULK INSERT` or `OPENROWSET`

::: moniker-end
::: moniker range="=sql-server-linux-ver15"

> [!NOTE]  
> This syntax varies in different versions of SQL Server. Use the version selector dropdown to choose the appropriate version.  
> To view the features of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-linux-ver16&preserve-view=true#syntax).

::: moniker-end

::: moniker range="=sql-server-ver15"

> [!NOTE]  
> This syntax varies in different versions of SQL Server. Use the version selector dropdown to choose the appropriate version.  
> To view the features of [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], visit [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md?view=sql-server-ver16&preserve-view=true#syntax).

::: moniker-end

::: moniker range="=sql-server-ver15||=sql-server-linux-ver15"

## <a id="syntax"></a> Syntax for SQL Server 2019

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CONNECTION_OPTIONS = '<key_value_pairs>'[,...]]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] PUSHDOWN = { ON | OFF } ]
    [ [ , ] TYPE = { HADOOP | BLOB_STORAGE } ]
    [ [ , ] RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]' )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source    | Connector location prefix | Location path                                         | Supported locations by product / service | Authentication  |
| ----------------------- | --------------- | ----------------------------------------------------- | ---------------------------------------- | --:|
| Cloudera CDH or Hortonworks HDP | `hdfs`  | `<Namenode>[:port]`                                   | [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] to [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] | Anonymous or basic authentication |
| Azure Storage account(V2) | `wasb[s]`       | `<container>@<storage_account>.blob.core.windows.net` | Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]<br />Hierarchical Namespace **not** supported |  Azure Storage account key |
| [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]              | `sqlserver`     | `<server_name>[\<instance_name>][:port]`              | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] | SQL authentication only |
| Oracle                  | `oracle`        | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]                       | Basic authentication only |
| Teradata                | `teradata`      | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]                       | Basic authentication only |
| MongoDB or Cosmos DB API for MongoDB     | `mongodb`       | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]      | Basic authentication only |
| Generic ODBC                    | `odbc`          | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] - Windows only        | Basic authentication only |
| Bulk Operations         | `https`         | `<storage_account>.blob.core.windows.net/<container>` | Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]                        | Shared access signature (SAS) |
| Azure Data Lake Storage Gen2 |   `abfs[s]` | `abfss://<container>@<storage _account>.dfs.core.windows.net`  |  Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU11+. | Storage Access Key |
| [!INCLUDE[ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ss-nover.md)]  data pool | `sqldatapool` | `sqldatapool://controller-svc/default` | Only supported in [!INCLUDE[ssbigdataclusters-ver15](../../includes/ssbigdataclusters-ver15.md)] | Basic authentication only |
| [!INCLUDE[ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ss-nover.md)]  storage pool | `sqlhdfs` | `sqlhdfs://controller-svc/default` | Only supported in [!INCLUDE[ssbigdataclusters-ver15](../../includes/ssbigdataclusters-ver15.md)]  |Basic authentication only |

Location path:

- `<Namenode>` = the machine name, name service URI, or IP address of the `Namenode` in the Hadoop cluster. PolyBase must resolve any DNS names used by the Hadoop cluster. <!-- For highly available Hadoop configurations, provide the Nameservice ID as the `LOCATION`. -->
- `port` = The port that the external data source is listening on. In Hadoop, the port can be found using the `fs.defaultFS` configuration parameter. The default is 8020.
- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.
- `<server_name>` = the host name.
- `<instance_name>` = the name of the SQL Server named instance. Used if you have SQL Server Browser Service running on the target instance.

Additional notes and guidance when setting the location:

- The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- You can use the `sqlserver` connector to connect [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] to another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
- Specify the `Driver={<Name of Driver>}` when connecting via `ODBC`.
- Using `wasbs` or `abfss` is optional but recommended in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] for accessing Azure Storage Accounts as data will be sent using a secure TLS/SSL connection.
- The `abfs` or `abfss` APIs are supported when accessing Azure Storage Accounts starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU11. For more information, see [the Azure Blob Filesystem driver (ABFS)](/azure/storage/blobs/data-lake-storage-abfs-driver).
- The Hierarchical Namespace option for Azure Storage Accounts(V2) using `abfs[s]` is supported via Azure Data Lake Storage Gen2 starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU11+. The Hierarchical Namespace option is otherwise not supported, and this option should remain **disabled**.
- To ensure successful PolyBase queries during a Hadoop `Namenode` fail-over, consider using a virtual IP address for the `Namenode` of the Hadoop cluster. If you don't, execute an [ALTER EXTERNAL DATA SOURCE][alter_eds] command to point to the new location.
- The `sqlhdfs` and `sqldatapool` types are supported for connecting between the master instance and storage pool of a big data cluster. For Cloudera CDH or Hortonworks HDP, use `hdfs`. For more information on using `sqlhdfs` for querying [!INCLUDE[ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ss-nover.md)] storage pools, see [Query HDFS in SQL Server 2019 Big Data Cluster](../../big-data-cluster/tutorial-query-hdfs-storage-pool.md).
- [!INCLUDE[polybase-java-connector-banner-retirement](../../includes/polybase-java-connector-banner-retirement.md)]

#### CONNECTION_OPTIONS = *key_value_pair*

<!-- See also docs\t-sql\statements\create-external-data-source-connection-options.md -->

Specified for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later. Specifies additional options when connecting over `ODBC` to an external data source. To use multiple connection options, separate them by a semi-colon.

Applies to generic `ODBC` connections, as well as built-in `ODBC` connectors for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, MongoDB, and Azure Cosmos DB API for MongoDB.

The `key_value_pair` is the keyword and the value for a specific connection option. The available keywords and values depend on the external data source type. The name of the driver is required as a minimum, but there are other options such as `APP='<your_application_name>'` or `ApplicationIntent= ReadOnly|ReadWrite` that are also useful to set and can assist with troubleshooting.

Possible key value pairs are specific to the provider for the external data source vendor. For more information for each provider, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL) CONNECTION_OPTIONS](create-external-data-source-connection-options.md).

#### Pushdown = ON | OFF

Specified for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] only. States whether computation can be pushed down to the external data source. It is **ON** by default.

`PUSHDOWN` is supported when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, MongoDB, the Azure Cosmos DB API for MongoDB, or ODBC at the external data source level.

Enabling or disabling push-down at the query level is achieved through a [hint][hint_pb].

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- When the `TYPE` = `BLOB_STORAGE`, the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. 
  - `TYPE` = `BLOB_STORAGE` is only permitted for bulk operations; you cannot create external tables for an external data source with `TYPE` = `BLOB_STORAGE`.

There are multiple ways to create a shared access signature:
- You can create an SAS token by navigating to the **Azure portal -> <Your_Storage_Account> -> Shared access signature -> Configure permissions -> Generate SAS and connection string**. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal#generate-a-shared-access-signature).
- You can [create and configure an SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs#get-the-sas-for-a-blob-container).
- You can create an SAS programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json).
- The SAS token should be configured as follows:
  - When an SAS token is generated, it includes a question mark ('?') at the beginning of the token. Exclude the leading `?` when configured as the SECRET.
  - Use a valid expiration period (all dates are in UTC time).
  - Grant at least read permission on the file that should be loaded (for example `srt=o&sp=r`). Multiple shared access signatures can be created for different use cases. Permissions should be granted as follows:

  |    Action    |    Permission    |
  |    ------    |    ----------    |
  |    Read data from a file    |    Read    |
  |    Read data from multiple files and subfolders    |    Read and List    |
  |    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |

For an example of using a `CREDENTIAL` with `SHARED ACCESS SIGNATURE` and `TYPE` = `BLOB_STORAGE`, see [Create an external data source to execute bulk operations and retrieve data from Azure Storage into SQL Database](#h-create-an-external-data-source-for-bulk-operations-retrieving-data-from-azure-storage)

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

#### TYPE = *[ HADOOP | BLOB_STORAGE ]*

Specifies the type of the external data source being configured. This parameter isn't always required, and should only be specified when connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2.

- In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], do not specify TYPE unless connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account.
- Use `HADOOP` when the external data source is Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2. 
- Use `BLOB_STORAGE` when executing bulk operations from Azure Storage account using [BULK INSERT][bulk_insert], or [OPENROWSET][openrowset] with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. Use `HADOOP` when intending to CREATE EXTERNAL TABLE against Azure Storage.
- [!INCLUDE[polybase-java-connector-banner-retirement](../../includes/polybase-java-connector-banner-retirement.md)]

For an example of using `TYPE` = `HADOOP` to load data from an Azure Storage account, see [Create external data source to access data in Azure Storage using the wasb:// interface](#e-create-external-data-source-to-access-data-in-azure-storage-using-the-wasb-interface) <!--[Create external data source to reference Azure Storage](#e-create-external-data-source-to-reference-azure-storage).-->

#### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], do not specify RESOURCE_MANAGER_LOCATION unless connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account.

Configure this optional value when connecting to Cloudera CDH, Hortonworks HDP, or an Azure Storage account only. For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which can lead to improved query performance.

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries. [Create external data source to reference Hadoop with push-down enabled](#c-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.

The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

In order for PolyBase to function correctly with a Hadoop external data source, the ports for the following Hadoop cluster components must be open:

- HDFS ports
    - Namenode
    - Datanode
- Resource Manager
    - Job submission
    - Job history

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
| 8                   | 8032                          |

The following table shows the default ports for these components. Note that there is Hadoop version dependency as well as the possibility of custom configuration that doesn't use the default port assignment.

| **Hadoop cluster component** | **Default Port** |
| :-- | :-- |
| NameNode | 8020 |
| DataNode (Data transfer, non-privilege IPC port) | 50010 |
| DataNode (Data transfer, privilege IPC port) | 1019 |
| Resource Manager Job Submission (Hortonworks 1.3)| 50300|
| Resource Manager Job Submission (Cloudera 4.3)|8021|
| Resource Manager Job Submission (Hortonworks 2.0 on Windows, Cloudera 5.x on Linux) | 8032 |
| Resource Manager Job Submission (Hortonworks 2.x, 3.0 on Linux, Hortonworks 2.1-3 on Windows) | 8050|
| Resource Manager Job History | 10020|

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

When you connect to the storage or data pool in SQL Server 2019 Big Data Cluster, the user's credentials are passed through to the back-end system. Create logins in the data pool itself to enable pass through authentication.

An SAS token with type `HADOOP` is unsupported. It's only supported with type = `BLOB_STORAGE` when a storage account access key is used instead. Attempting to create an external data source with type `HADOOP` and a SAS credential fails with the following error:

`Msg 105019, Level 16, State 1 - EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect. Java exception message: Parameters provided to connect to the Azure storage account are not valid.: Error [Parameters provided to connect to the Azure storage account are not valid.] occurred while accessing external file.'`

## Examples

> [!IMPORTANT]  
> For information on how to install and enable PolyBase, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md)

### A. Create external data source in SQL Server 2019 to reference Oracle

To create an external data source that references Oracle, ensure you have a database scoped credential. You may optionally also enable or disable push-down of computation against this data source.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL OracleProxyAccount
WITH
     IDENTITY = 'oracle_username',
     SECRET = 'oracle_password' ;

CREATE EXTERNAL DATA SOURCE MyOracleServer
WITH
  ( LOCATION = 'oracle://145.145.145.145:1521',
    CREDENTIAL = OracleProxyAccount,
    PUSHDOWN = ON
  ) ;
```

Optionally, the external data source to Oracle can use proxy authentication to provide fine grain access control. A proxy user can be configured to have limited access compared to the user being impersonated.

```sql
CREATE DATABASE SCOPED CREDENTIAL [OracleProxyCredential]
WITH IDENTITY = 'oracle_username', SECRET = 'oracle_password';

CREATE EXTERNAL DATA SOURCE [OracleSalesSrvr]
WITH (LOCATION = 'oracle://145.145.145.145:1521',
CONNECTION_OPTIONS = 'ImpersonateUser=%CURRENT_USER',
CREDENTIAL = [OracleProxyCredential]);
```

For additional examples to other data sources such as MongoDB, see [Configure PolyBase to access external data in MongoDB][mongodb_pb].

### B. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks HDP or Cloudera CDH Hadoop cluster, specify the machine name, or IP address of the Hadoop `Namenode` and port. <!-- Provide the Nameservice ID as the `LOCATION` for highly available configurations. -->

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    TYPE = HADOOP
  ) ;
```

### C. Create external data source to reference Hadoop with push-down enabled

Specify the `RESOURCE_MANAGER_LOCATION` option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase makes a cost-based decision to determine whether the query computation should be pushed to Hadoop.

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8020' ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  ) ;
```

### D. Create external data source to reference Kerberos-secured Hadoop

To verify if the Hadoop cluster is Kerberos-secured, check the value of `hadoop.security.authentication` property in Hadoop core-site.xml. To reference a Kerberos-secured Hadoop cluster, you must specify a database scoped credential that contains your Kerberos username and password. The database master key is used to encrypt the database scoped credential secret.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Kerberos user name and password.
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
WITH
     IDENTITY = '<hadoop_user_name>',
     SECRET = '<hadoop_password>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    CREDENTIAL = HadoopUser1 ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  );
```

### E. Create external data source to access data in Azure Storage using the wasb:// interface

In this example, the external data source is an Azure V2 Storage account named `logs`. The storage container is called `daily`. The Azure Storage external data source is for data transfer only. It doesn't support predicate push-down. Hierarchical namespaces are not supported when accessing data via the `wasb://` interface. Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).

This example shows how to create the database scoped credential for authentication to an Azure V2 Storage account. Specify the Azure Storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure Storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>' ,
  SECRET = '<azure_storage_account_key>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'wasbs://daily@logs.blob.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  ) ;
```

### F. Create external data source to reference a SQL Server named instance via PolyBase connectivity

**Applies to:** [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later

To create an external data source that references a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use `CONNECTION_OPTIONS` to specify the instance name.

In example below, `WINSQL2019` is the host name and `SQL2019` is the instance name. `'Server=%s\SQL2019'` is the key value pair.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019' ,
  CONNECTION_OPTIONS = 'Server=%s\SQL2019' ,
  CREDENTIAL = SQLServerCredentials
) ;
```

Alternatively, you can use a port to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default instance.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019:58137' ,
  CREDENTIAL = SQLServerCredentials
) ;
```

### G. Create external data source to reference a readable secondary replica of Always On availability group

**Applies to:** [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later

To create an external data source that references a readable secondary replica of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use `CONNECTION_OPTIONS` to specify the `ApplicationIntent=ReadOnly`.

First, create the database scoped credential, storing credentials for a SQL authenticated login. The SQL ODBC Connector for PolyBase only supports basic authentication. Before you create a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](create-master-key-transact-sql.md). The following sample creates a database scoped credential, provide your own login and password.

```sql
CREATE DATABASE SCOPED CREDENTIAL SQLServerCredentials
WITH IDENTITY = 'username', SECRET = 'password';
```

Next, create the new external data source.

The ODBC `Database` parameter is not needed, provide the database name instead via a three-part name in the CREATE EXTERNAL TABLE statement, within the LOCATION parameter. For an example, see [CREATE EXTERNAL TABLE](create-external-table-transact-sql.md?view=sql-server-ver15&preserve-view=true#g-create-an-external-table-for-sql-server).

In example below, `WINSQL2019AGL` is the availability group listener name and `dbname` is the name of the database to be the target of the CREATE EXTERNAL TABLE statement.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadOnly' ,
  CREDENTIAL = SQLServerCredentials
);
```

You can demonstrate the redirection behavior of the availability group by specifying `ApplicationIntent` and creating an external table on the system view `sys.servers`. In the following sample script, two external data sources are created, and one external table is created for each. Use the views to test which server is responding to the connection. Similar outcomes can also be achieved via the read-only routing feature. For more information, see [Configure read-only routing for an Always On availability group](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md).

```sql
CREATE EXTERNAL DATA SOURCE [DataSource_SQLInstanceListener_ReadOnlyIntent]
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadOnly' ,
  CREDENTIAL = [SQLServerCredentials]);
GO
CREATE EXTERNAL DATA SOURCE [DataSource_SQLInstanceListener_ReadWriteIntent]
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadWrite' ,
  CREDENTIAL = [SQLServerCredentials]);
GO
```

Inside the database in the availability group, create a view to return `sys.servers` and the name of the local instance, which helps you identify which replica is responding to the query. For more information, see [sys.servers](../../relational-databases/system-catalog-views/sys-servers-transact-sql.md).

```sql
CREATE VIEW vw_sys_servers AS
SELECT [name] FROM sys.servers
WHERE server_id = 0;
GO
```

Then, create an external table on the source instance:

```sql
CREATE EXTERNAL TABLE vw_sys_servers_ro
(    name sysname NOT NULL )
WITH (DATA_SOURCE = [DataSource_SQLInstanceListener_ReadOnlyIntent], LOCATION = N'dbname.dbo.vw_sys_servers');
GO
CREATE EXTERNAL TABLE vw_sys_servers_rw
(    name sysname NOT NULL)
WITH (DATA_SOURCE = [DataSource_SQLInstanceListener_ReadWriteIntent], LOCATION = N'dbname.dbo.vw_sys_servers');
GO
SELECT [name] FROM dbo.vw_sys_servers_ro; --should return secondary replica instance
SELECT [name] FROM dbo.vw_sys_servers_rw; --should return primary replica instance
GO
```

## Examples: Bulk operations

> [!IMPORTANT]  
> Do not add a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

### H. Create an external data source for bulk operations retrieving data from Azure Storage

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later.

Use the following data source for bulk operations using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. The credential must set `SHARED ACCESS SIGNATURE` as the identity, mustn't have the leading `?` in the SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)][sas_token].

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE',
  -- Remove ? from the beginning of the SAS token
  SECRET = '<azure_shared_access_signature>' ;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
  ( LOCATION = 'https://newinvoices.blob.core.windows.net/week3' ,
    CREDENTIAL = AccessAzureInvoices ,
    TYPE = BLOB_STORAGE
  ) ;
```

To see this example in use, see the [BULK INSERT][bulk_insert_example] example.

### I. Create external data source to access data in Azure Storage using the abfs:// interface

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU11 and later

In this example, the external data source is an Azure Data Lake Storage Gen2 account `logs`, using [the Azure Blob Filesystem driver (ABFS)](/azure/storage/blobs/data-lake-storage-abfs-driver). The storage container is called `daily`. The Azure Data Lake Storage Gen2 external data source is for data transfer only, as predicate push-down is not supported.

This example shows how to create the database scoped credential for authentication to an Azure Data Lake Storage Gen2 account. Specify the Azure Storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure Storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;
-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>' ,
  SECRET = '<azure_storage_account_key>' ;
-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'abfss://daily@logs.dfs.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  ) ;
```

### J. Create external data source using generic ODBC to PostgreSQL

As in previous examples, first create a database master key and database scoped credential. The database scoped credential will be used for the external data source. This example also assumes that a generic ODBC data provider for PostgreSQL is installed on the server.

In this example, the generic ODBC data provider is used to connect to a PostgreSQL database server in the same network, where the fully qualified domain name of the PostgreSQL server is `POSTGRES1`, using the default port of TCP 5432.

```sql
CREATE EXTERNAL DATA SOURCE POSTGRES1
WITH
(
 LOCATION = 'odbc://POSTGRES1.domain:5432'
,CONNECTION_OPTIONS = 'Driver={PostgreSQL Unicode(x64)};'
,CREDENTIAL = postgres_credential
)
```

## Next steps

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)][alter_eds]
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]
- [PolyBase Connectivity Configuration][connectivity_pb]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md

[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: ./create-external-table-transact-sql.md
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true

[alter_eds]: ./alter-external-data-source-transact-sql.md

[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]: ../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown

<!-- Azure Docs -->
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end

::: moniker range=">=sql-server-ver16"


## Overview: SQL Server 2022

[!INCLUDE[SQL2022](../../includes/applies-to-version/sqlserver2022.md)] and later

Creates an external data source for PolyBase queries. External data sources are used to establish connectivity and support these primary use cases:

- Data virtualization and data load using [PolyBase][intro_pb]
- Bulk load operations using `BULK INSERT` or `OPENROWSET`

> [!NOTE]
> This syntax varies in different versions of SQL Server. Use the version selector dropdown to choose the appropriate version. This content applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.

## <a id="syntax"></a> Syntax for SQL Server 2022 and later

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CONNECTION_OPTIONS = '<key_value_pairs>'[,...]]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] PUSHDOWN = { ON | OFF } ]
  )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source    | Connector location prefix | Location path                                         | Supported locations by product / service | Authentication |
| ----------------------- | --------------- | ----------------------------------------------------- | ---------------------------------------- | --: |
| Azure Storage Account(V2) | `abs` | `abs://<storage_account_name>.blob.core.windows.net/<container_name>` | Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]<br />Hierarchical Namespace is supported. | Shared access signature (SAS) |
| Azure Data Lake Storage Gen2 | `adls`   | `adls://<storage_account_name>.dfs.core.windows.net/<container_name>` | Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] | Shared access signature (SAS) |
| [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] | `sqlserver`|`<server_name>[\<instance_name>][:port]`| Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]   | SQL authentication only |
| Oracle                  | `oracle`        | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]                       | Basic authentication only |
| Teradata                | `teradata`      | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]                       | Basic authentication only |
| MongoDB or Cosmos DB API for MongoDB     | `mongodb`       | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]  | Basic authentication only |
| Generic ODBC                    | `odbc`          | `<server_name>[:port]`                                | Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] - Windows only  | Basic authentication only |
| Bulk Operations         | `https`         | `<storage_account>.blob.core.windows.net/<container>` | Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]                        | Shared access signature (SAS) |
| S3-compatible object storage | `s3` | `s3://<server_name>:<port>/` | Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] | \* |

\* Must be a [database scoped credential](create-database-scoped-credential-transact-sql.md), where the IDENTITY is hard-coded to `IDENTITY = 'S3 Access Key'` and the SECRET argument is in the format `= '<AccessKeyID>:<SecretKeyID>'` or use STS authorization. For more information, see [Configure PolyBase to access external data in S3-compatible object storage](../../relational-databases/polybase/polybase-configure-s3-compatible.md).

Location path:

- `port` = The port that the external data source is listening on.
- `<container_name>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.
- `<server_name>` = the host name.
- `<instance_name>` = the name of the SQL Server named instance. Used if you have SQL Server Browser Service running on the target instance.
- `<ip_address>:<port>` = For S3-compatible object storage only (starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]), the endpoint and port used to connect to the S3-compatible storage.

Additional notes and guidance when setting the location:

- The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- You can use the `sqlserver` connector to connect [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] to another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
- Specify the `Driver={<Name of Driver>}` when connecting via `ODBC`.
- The Hierarchical Namespace option for Azure Storage Accounts(V2) using the prefix `adls` is supported via Azure Data Lake Storage Gen2 in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].
<!--- - The `sqlhdfs` and `sqldatapool` types are supported for connecting between the master instance and storage pool of SQL Server 2019 Big Data Cluster. For Cloudera CDH or Hortonworks HDP, use `hdfs`. For more information on using `sqlhdfs` for querying [!INCLUDE[ssbigdataclusters-ss-nover](../../includes/ssbigdataclusters-ss-nover.md)] storage pools, see [Query HDFS in SQL Server 2019 Big Data Cluster](../../big-data-cluster/tutorial-query-hdfs-storage-pool.md).
- [!INCLUDE[polybase-java-connector-banner-retirement](../../includes/polybase-java-connector-banner-retirement.md)] -->
- SQL Server support for HDFS Cloudera (CDP) and Hortonworks (HDP) external data sources are retired and not included in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. There is no need to use the TYPE argument in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].
- For more information on S3-compatible object storage and PolyBase starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], see [Configure PolyBase to access external data in S3-compatible object storage](../../relational-databases/polybase/polybase-configure-s3-compatible.md). For an example of querying a parquet file within S3-compatible object storage, see [Virtualize parquet file in a S3-compatible object storage with PolyBase](../../relational-databases/polybase/polybase-virtualize-parquet-file.md).
- Differing from previous versions, in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the prefix used for Azure Storage Account (v2) changed from `wasb[s]` to `abs`.
- Differing from previous versions, in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the prefix used for Azure Data Lake Storage Gen2 changed from `abfs[s]` to `adls`.
- For an example using PolyBase to virtualize a CSV file in Azure Storage, see [Virtualize CSV file with PolyBase](../../relational-databases/polybase/virtualize-csv.md).
- For an example using PolyBase to virtualize a delta table in ADLS Gen2, see [Virtualize delta table with PolyBase](../../relational-databases/polybase/virtualize-delta.md).

#### CONNECTION_OPTIONS = *key_value_pair*

<!-- See also docs\t-sql\statements\create-external-data-source-connection-options.md -->

Specified for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later. Specifies additional options when connecting over `ODBC` to an external data source. To use multiple connection options, separate them by a semi-colon.

Applies to generic `ODBC` connections, as well as built-in `ODBC` connectors for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, MongoDB, and Azure Cosmos DB API for MongoDB.

The `key_value_pair` is the keyword and the value for a specific connection option. The available keywords and values depend on the external data source type. The name of the driver is required as a minimum, but there are other options such as `APP='<your_application_name>'` or `ApplicationIntent= ReadOnly|ReadWrite` that are also useful to set and can assist with troubleshooting.

Possible key value pairs are specific to the driver. For more information for each provider, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL) CONNECTION_OPTIONS](create-external-data-source-connection-options.md).

#### PUSHDOWN = ON | OFF

**Applies to: [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later.** States whether computation can be pushed down to the external data source. It is on by default.

`PUSHDOWN` is supported when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, MongoDB, the Azure Cosmos DB API for MongoDB, or ODBC at the external data source level.

Enabling or disabling push-down at the query level is achieved through a [hint][hint_pb].

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- When accessing Azure Storage Account (V2) or Azure Data Lake Storage Gen2, the `IDENTITY` must be `SHARED ACCESS SIGNATURE`.
  - For an example, see [Create an external data source to execute bulk operations and retrieve data from Azure Storage into SQL Database](#h-create-an-external-data-source-for-bulk-operations-retrieving-data-from-azure-storage-1).

There are multiple ways to create a shared access signature:
  - You can create an SAS token by navigating to the **Azure portal -> <Your_Storage_Account> -> Shared access signature -> Configure permissions -> Generate SAS and connection string**. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal#generate-a-shared-access-signature).
  - You can [create and configure an SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs#get-the-sas-for-a-blob-container).
  - You can create an SAS programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json).
  - The SAS token should be configured as follows:
    - When an SAS token is generated, it includes a question mark ('?') at the beginning of the token. Exclude the leading `?` when configured as the SECRET.
    - Use a valid expiration period (all dates are in UTC time).
    - Grant at least read permission on the file that should be loaded (for example `srt=o&sp=r`). Multiple shared access signatures can be created for different use cases. Permissions should be granted as follows:

    |    Action    |    Permission    |
    |    ------    |    ----------    |
    |    Read data from a file    |    Read    |
    |    Read data from multiple files and subfolders    |    Read and List    |
    |    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |


- When the `TYPE` = `BLOB_STORAGE`, the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. Furthermore, the SAS token should be configured as follows:
  - Exclude the leading `?` when configured as the secret.
  - Have at least read permission on the file that should be loaded (for example `srt=o&sp=r`).
  - Use a valid expiration period (all dates are in UTC time).
  - `TYPE` = `BLOB_STORAGE` is only permitted for bulk operations; you cannot create external tables for an external data source with `TYPE` = `BLOB_STORAGE`.

For an example of using a `CREDENTIAL` with S3-compatible object storage and PolyBase, see [Configure PolyBase to access external data in S3-compatible object storage](../../relational-databases/polybase/polybase-configure-s3-compatible.md).

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

<!---
#### TYPE = *[ HADOOP | BLOB_STORAGE ]*

Specifies the type of the external data source being configured. This parameter isn't always required, and should only be specified when connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2.

- In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], do not specify TYPE unless connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account.
- Use `HADOOP` when the external data source is Cloudera CDH, Hortonworks HDP, an Azure Storage account, or an Azure Data Lake Storage Gen2. 
- Use `BLOB_STORAGE` when executing bulk operations from Azure Storage account using [BULK INSERT][bulk_insert], or [OPENROWSET][openrowset] with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. Use `HADOOP` when intending to CREATE EXTERNAL TABLE against Azure Storage.
- [!INCLUDE[polybase-java-connector-banner-retirement](../../includes/polybase-java-connector-banner-retirement.md)]
- Do not specify a `TYPE` for S3-compatible object storage.

#### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

Configure this optional value when connecting to Cloudera CDH, Hortonworks HDP, or an Azure Storage account only. For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which can lead to improved query performance.

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries. [Create external data source to reference Hadoop with push-down enabled](#c-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.

The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

In order for PolyBase to function correctly with a Hadoop external data source, the ports for the following Hadoop cluster components must be open:

- HDFS ports
    - Namenode
    - Datanode
- Resource manager
    - Job submission
    - Job history

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
| 8                   | 8032                          |

The following table shows the default ports for these components. Note that there is Hadoop version dependency as well as the possibility of custom configuration that doesn't use the default port assignment.

| **Hadoop cluster component** | **Default Port** |
| :-- | :-- |
| NameNode | 8020 |
| DataNode (Data transfer, non-privilege IPC port) | 50010 |
| DataNode (Data transfer, privilege IPC port) | 1019 |
| Resource Manager Job Submission (Hortonworks 1.3)| 50300|
| Resource Manager Job Submission (Cloudera 4.3)|8021|
| Resource Manager Job Submission (Hortonworks 2.0 on Windows, Cloudera 5.x on Linux) | 8032 |
| Resource Manager Job Submission (Hortonworks 2.x, 3.0 on Linux, Hortonworks 2.1-3 on Windows) | 8050|
| Resource Manager Job History | 10020|

> [!IMPORTANT]  
> The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

[Create external data source to reference Hadoop with push-down enabled](#c-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.
-->
## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

## Upgrade to SQL Server 2022

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Hadoop external data sources are no longer supported. It is required to manually recreate external data sources previously created with `TYPE = HADOOP`, and any external table that uses this external data source.

Users will also need to configure their external data sources to use new connectors when connecting to Azure Storage.

| External Data Source | From | To |
|:--|:--|:--|
| Azure Blob Storage | wasb[s] | abs |
| ADLS Gen2 | abfs[s] | adls |

## Examples

> [!IMPORTANT]  
> For information on how to install and enable PolyBase, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md)

### A. Create external data source in SQL Server to reference Oracle

To create an external data source that references Oracle, ensure you have a database scoped credential. You may optionally also enable or disable push-down of computation against this data source.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL OracleProxyAccount
WITH
     IDENTITY = 'oracle_username',
     SECRET = 'oracle_password' ;

CREATE EXTERNAL DATA SOURCE MyOracleServer
WITH
  ( LOCATION = 'oracle://145.145.145.145:1521',
    CREDENTIAL = OracleProxyAccount,
    PUSHDOWN = ON
  ) ;
```

Optionally, the external data source to Oracle can use proxy authentication to provide fine grain access control. A proxy user can be configured to have limited access compared to the user being impersonated.

```sql
CREATE DATABASE SCOPED CREDENTIAL [OracleProxyCredential]
WITH IDENTITY = 'oracle_username', SECRET = 'oracle_password';

CREATE EXTERNAL DATA SOURCE [OracleSalesSrvr]
WITH (LOCATION = 'oracle://145.145.145.145:1521',
CONNECTION_OPTIONS = 'ImpersonateUser=%CURRENT_USER',
CREDENTIAL = [OracleProxyCredential]);
```

### B. Create external data source to reference a SQL Server named instance via PolyBase connectivity

**Applies to:** [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later

To create an external data source that references a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use `CONNECTION_OPTIONS` to specify the instance name.

First, create the database scoped credential, storing credentials for a SQL authenticated login. The SQL ODBC Connector for PolyBase only supports basic authentication. Before you create a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](create-master-key-transact-sql.md). The following sample creates a database scoped credential, provide your own login and password.

```sql
CREATE DATABASE SCOPED CREDENTIAL SQLServerCredentials
WITH IDENTITY = 'username', SECRET = 'password';
```

In example below, `WINSQL2019` is the host name and `SQL2019` is the instance name. `'Server=%s\SQL2019'` is the key value pair.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019' ,
  CONNECTION_OPTIONS = 'Server=%s\SQL2019' ,
  CREDENTIAL = SQLServerCredentials
) ;
```

Alternatively, you can use a port to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default instance.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019:58137',
  CREDENTIAL = SQLServerCredentials
) ;
```

### C. Create external data source to reference a readable secondary replica of Always On availability group

**Applies to:** [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later

To create an external data source that references a readable secondary replica of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use `CONNECTION_OPTIONS` to specify the `ApplicationIntent=ReadOnly`.

First, create the database scoped credential, storing credentials for a SQL authenticated login. The SQL ODBC Connector for PolyBase only supports basic authentication. Before you create a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](create-master-key-transact-sql.md). The following sample creates a database scoped credential, provide your own login and password.

```sql
CREATE DATABASE SCOPED CREDENTIAL SQLServerCredentials
WITH IDENTITY = 'username', SECRET = 'password';
```

Next, create the new external data source.

The ODBC `Database` parameter is not needed, provide the database name instead via a three-part name in the CREATE EXTERNAL TABLE statement, within the LOCATION parameter. For an example, see [CREATE EXTERNAL TABLE](create-external-table-transact-sql.md?view=sql-server-ver15&preserve-view=true#g-create-an-external-table-for-sql-server).

In example below, `WINSQL2019AGL` is the availability group listener name and `dbname` is the name of the database to be the target of the CREATE EXTERNAL TABLE statement.

```sql
CREATE EXTERNAL DATA SOURCE SQLServerInstance2
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadOnly' ,
  CREDENTIAL = SQLServerCredentials
);
```

You can demonstrate the redirection behavior of the availability group by specifying `ApplicationIntent` and creating an external table on the system view `sys.servers`. In the following sample script, two external data sources are created, and one external table is created for each. Use the views to test which server is responding to the connection. Similar outcomes can also be achieved via the read-only routing feature. For more information, see [Configure read-only routing for an Always On availability group](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md).

```sql
CREATE EXTERNAL DATA SOURCE [DataSource_SQLInstanceListener_ReadOnlyIntent]
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadOnly' ,
  CREDENTIAL = [SQLServerCredentials]);
GO
CREATE EXTERNAL DATA SOURCE [DataSource_SQLInstanceListener_ReadWriteIntent]
WITH (
  LOCATION = 'sqlserver://WINSQL2019AGL' ,
  CONNECTION_OPTIONS = 'ApplicationIntent=ReadWrite' ,
  CREDENTIAL = [SQLServerCredentials]);
GO
```

Inside the database in the availability group, create a view to return `sys.servers` and the name of the local instance, which helps you identify which replica is responding to the query. For more information, see [sys.servers](../../relational-databases/system-catalog-views/sys-servers-transact-sql.md).

```sql
CREATE VIEW vw_sys_servers AS
SELECT [name] FROM sys.servers
WHERE server_id = 0;
GO
```

Then, create an external table on the source instance:

```sql
CREATE EXTERNAL TABLE vw_sys_servers_ro
(    name sysname NOT NULL )
WITH (DATA_SOURCE = [DataSource_SQLInstanceListener_ReadOnlyIntent], LOCATION = N'dbname.dbo.vw_sys_servers');
GO
CREATE EXTERNAL TABLE vw_sys_servers_rw
(    name sysname NOT NULL)
WITH (DATA_SOURCE = [DataSource_SQLInstanceListener_ReadWriteIntent], LOCATION = N'dbname.dbo.vw_sys_servers');
GO
SELECT [name] FROM dbo.vw_sys_servers_ro; --should return secondary replica instance
SELECT [name] FROM dbo.vw_sys_servers_rw; --should return primary replica instance
GO
```

### D. Create external data source to query a parquet file in S3-compatible object storage via PolyBase

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

The following sample script creates an external data source `s3_ds` in the source user database in SQL Server. The external data source references the `s3_dc` database scoped credential.

```sql
CREATE DATABASE SCOPED CREDENTIAL s3_dc
WITH
    IDENTITY = 'S3 Access Key', -- for S3-compatible object storage the identity must always be S3 Access Key
    SECRET = <access_key_id>:<secret_key_id> -- provided by the S3-compatible object storage
GO

CREATE EXTERNAL DATA SOURCE s3_ds
WITH
(   LOCATION = 's3://<ip_address>:<port>/'
,   CREDENTIAL = s3_dc
);
GO
```

Verify the new external data source with [sys.external_data_sources](../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md).

```sql
SELECT * FROM sys.external_data_sources;
```

Then, the following example demonstrates using T-SQL to query a parquet file stored in S3-compatible object storage via OPENROWSET query. For more information, see [Virtualize parquet file in a S3-compatible object storage with PolyBase](../../relational-databases/polybase/polybase-virtualize-parquet-file.md).

```sql
SELECT  *
FROM    OPENROWSET
        (   BULK '/<bucket>/<parquet_folder>'
        ,   FORMAT       = 'PARQUET'
        ,   DATA_SOURCE  = 's3_ds'
        ) AS [cc];
```

### E. Create external data source using generic ODBC to PostgreSQL

As in previous examples, first create a database master key and database scoped credential. The database scoped credential will be used for the external data source. This example also assumes that a generic ODBC data provider for PostgreSQL is installed on the server.

In this example, the generic ODBC data provider is used to connect to a PostgreSQL database server in the same network, where the fully qualified domain name of the PostgreSQL server is `POSTGRES1`, using the default port of TCP 5432.

```sql
CREATE EXTERNAL DATA SOURCE POSTGRES1
WITH
(
 LOCATION = 'odbc://POSTGRES1.domain:5432'
,CONNECTION_OPTIONS = 'Driver={PostgreSQL Unicode(x64)};'
,CREDENTIAL = postgres_credential
)
```

### Azure Storage

#### Create a shared access signature

For both Azure Blob Storage and Azure Data Lake Gen2 the supported authentication method is shared access signature (SAS). One simple way to generate a shared access signature token follow the steps below. For more information, see [CREDENTIAL](#credential--credential_name-3).

1. Navigate to the Azure portal, and the desired Storage Account.
2. Navigate to your desired Container under **Data Storage** menu.
3. Select **Shared access tokens**.
4. Choose the appropriate permission based on the desired action, for reference use the table bellow:

|    Action    |    Permission    |
|    ------    |    ----------    |
|    Read data from a file    |    Read    |
|    Read data from multiple files and subfolders    |    Read and List    |
|    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |

5. Choose the token expiration date.
6. Generate SAS token and URL.
7. Copy the SAS token.


### F. Create external data source to access data in Azure Blob Storage using the abs:// interface

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use a new prefix `abs` for Azure Storage Account v2. The `abs` prefix supports authentication using `SHARED ACCESS SIGNATURE`. The `abs` prefix replaces `wasb`, used in previous versions. HADOOP is not longer supported, there is no more need to use `TYPE = BLOB_STORAGE`.

The Azure storage account key is no longer needed, instead using SAS Token as we can see in the following example:

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;
GO
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredentialv2
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE', -- to use SAS the identity must be fixed as-is
  SECRET = '<Blob_SAS_Token>' ;
GO
-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'abs://<storage_account_name>.blob.core.windows.net/<container>' ,
    CREDENTIAL = AzureStorageCredentialv2,
  ) ;
```

For a more detailed example on how to access CSV files stored in Azure Blob Storage, see [Virtualize CSV file with PolyBase](../../relational-databases/polybase/virtualize-csv.md).

### G. Create external data source to access data in Azure Data Lake Gen2

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], use a new prefix `adls` for Azure Data Lake Gen2, replacing `abfs` used in previous versions. The `adls` prefix also supports SAS token as authentication method as shown in the example below:

```sql
--Create a database scoped credential using SAS Token
CREATE DATABASE SCOPED CREDENTIAL datalakegen2
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<DataLakeGen2_SAS_Token>';
GO
CREATE EXTERNAL DATA SOURCE data_lake_gen2_dfs
WITH
(
LOCATION = 'adls://<storage_account>.dfs.core.windows.net'
,CREDENTIAL = datalakegen2
)
```

For a more detailed example on  how to access delta files stored on Azure Data Lake Gen2, see [Virtualize delta table with PolyBase](../../relational-databases/polybase/virtualize-delta.md).

## Examples: Bulk Operations

> [!IMPORTANT]  
> Do not add a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

### H. Create an external data source for bulk operations retrieving data from Azure Storage

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later.

Use the following data source for bulk operations using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. The credential must set `SHARED ACCESS SIGNATURE` as the identity, mustn't have the leading `?` in the SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)][sas_token].

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE',
  -- Remove ? from the beginning of the SAS token
  SECRET = '<azure_shared_access_signature>' ;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
  ( LOCATION = 'https://newinvoices.blob.core.windows.net/week3' ,
    CREDENTIAL = AccessAzureInvoices ,
    TYPE = BLOB_STORAGE
  ) ;
```

## Next steps

- [ALTER EXTERNAL DATA SOURCE (Transact-SQL)][alter_eds]
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]
- [PolyBase Connectivity Configuration][connectivity_pb]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-importing-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md

[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: ./create-external-table-transact-sql.md
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true

[alter_eds]: ./alter-external-data-source-transact-sql.md

[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]: ../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown

<!-- Azure Docs -->
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end

::: moniker range="=azuresqldb-current||=azuresqledge-current"

:::row:::
    :::column:::
        [SQL Server](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Database \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Managed<br />Instance](create-external-data-source-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-external-data-source-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-external-data-source-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: Azure SQL Database

[!INCLUDE [Applies to](../../includes/applies-md.md)] [!INCLUDE[asdb](../../includes/applies-to-version/_asdb.md)]

Creates an external data source for elastic queries. External data sources are used to establish connectivity and support these primary use cases:

- Bulk load operations using `BULK INSERT` or `OPENROWSET`
- Query remote SQL Database or Azure Synapse instances using SQL Database with [elastic query][remote_eq]
- Query a sharded SQL Database using [elastic query][sharded_eq]

## <a id="syntax"></a> Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] TYPE = { BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER } ]
    [ [ , ] DATABASE_NAME = '<database_name>' ]
    [ [ , ] SHARD_MAP_NAME = '<shard_map_manager>' ] )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database in SQL Database.

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source   | Connector location prefix | Location path                                         | Availability |
| ---------------------- | --------------- | ----------------------------------------------------- | ------------ |
| Bulk Operations        | `https`         | `<storage_account>.blob.core.windows.net/<container>` | |
| Elastic Query (shard)  | Not required    | `<shard_map_server_name>.database.windows.net`        | |
| Elastic Query (remote) | Not required    | `<remote_server_name>.database.windows.net`           | |
| EdgeHub         | `edgehub`         | `edgehub://` | Available in [Azure SQL Edge](/azure/azure-sql-edge/overview) *only*. EdgeHub is always local to the instance of [Azure SQL Edge](/azure/azure-sql-edge/overview). As such there is no need to specify a path or port value. |
| Kafka        | `kafka`         | `kafka://<kafka_bootstrap_server_name_ip>:<port_number>` | Available in [Azure SQL Edge](/azure/azure-sql-edge/overview) *only*.  |

Location path:

- `<shard_map_server_name>` = The logical server name in Azure that is hosting the shard map manager. The `DATABASE_NAME` argument provides the database used to host the shard map and `SHARD_MAP_NAME` is used for the shard map itself.
- `<remote_server_name>` = The target logical server name for the elastic query. The database name is specified using the `DATABASE_NAME` argument.

Additional notes and guidance when setting the location:

- The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load data from Azure Storage into [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], use a Shared Access Signature (SAS token).
- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- When the `TYPE` = `BLOB_STORAGE`, the credential must be created using `SHARED ACCESS SIGNATURE` as the identity. 
- When connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).
- When `TYPE` = `HADOOP` the credential must be created using the storage account key as the `SECRET`.
- `TYPE` = `BLOB_STORAGE` is only permitted for bulk operations; you cannot create external tables for an external data source with `TYPE` = `BLOB_STORAGE`.

There are multiple ways to create a shared access signature:
  - You can create an SAS token by navigating to the **Azure portal -> <Your_Storage_Account> -> Shared access signature -> Configure permissions -> Generate SAS and connection string**. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal#generate-a-shared-access-signature).
  - You can [create and configure an SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs#get-the-sas-for-a-blob-container).
  - You can create an SAS programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json).
  - The SAS token should be configured as follows:
    - When an SAS token is generated, it includes a question mark ('?') at the beginning of the token. Exclude the leading `?` when configured as the SECRET.
    - Use a valid expiration period (all dates are in UTC time).
    - Grant at least read permission on the file that should be loaded (for example `srt=o&sp=r`). Multiple shared access signatures can be created for different use cases. Permissions should be granted as follows:

    |    Action    |    Permission    |
    |    ------    |    ----------    |
    |    Read data from a file    |    Read    |
    |    Read data from multiple files and subfolders    |    Read and List    |
    |    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |


For an example of using a `CREDENTIAL` with `SHARED ACCESS SIGNATURE` and `TYPE` = `BLOB_STORAGE`, see [Create an external data source to execute bulk operations and retrieve data from Azure Storage into SQL Database](#c-create-an-external-data-source-for-bulk-operations-retrieving-data-from-azure-storage)

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

#### TYPE = *[ BLOB_STORAGE | RDBMS | SHARD_MAP_MANAGER]*

Specifies the type of the external data source being configured. This parameter isn't always required.

- Use `RDBMS` for cross-database queries using elastic query from SQL Database.
- Use `SHARD_MAP_MANAGER` when creating an external data source when connecting to a sharded SQL Database.
- Use `BLOB_STORAGE` when executing bulk operations with [BULK INSERT][bulk_insert], or [OPENROWSET][openrowset].

> [!IMPORTANT]  
> Do not set `TYPE` if using any other external data source.

#### DATABASE_NAME = *database_name*

Configure this argument when the `TYPE` is set to `RDBMS` or `SHARD_MAP_MANAGER`.

| TYPE              | Value of DATABASE_NAME                                       |
| ----------------- | ------------------------------------------------------------ |
| RDBMS             | The name of the remote database on the server provided using `LOCATION` |
| SHARD_MAP_MANAGER | Name of the database operating as the shard map manager      |

For an example showing how to create an external data source where `TYPE` = `RDBMS` refer to [Create an RDBMS external data source](#b-create-an-rdbms-external-data-source)

#### SHARD_MAP_NAME = *shard_map_name*

Used when the `TYPE` argument is set to `SHARD_MAP_MANAGER` only to set the name of the shard map.

For an example showing how to create an external data source where `TYPE` = `SHARD_MAP_MANAGER` refer to [Create a shard map manager external data source](#a-create-a-shard-map-manager-external-data-source)

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Examples

### A. Create a shard map manager external data source

To create an external data source to reference a `SHARD_MAP_MANAGER`, specify the SQL Database server name that hosts the shard map manager in SQL Database or a SQL Server database on a virtual machine.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

CREATE DATABASE SCOPED CREDENTIAL ElasticDBQueryCred
WITH
  IDENTITY = '<username>',
  SECRET = '<password>' ;

CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc
WITH
  ( TYPE = SHARD_MAP_MANAGER ,
    LOCATION = '<server_name>.database.windows.net' ,
    DATABASE_NAME = 'ElasticScaleStarterKit_ShardMapManagerDb' ,
    CREDENTIAL = ElasticDBQueryCred ,
    SHARD_MAP_NAME = 'CustomerIDShardMap'
  ) ;
```

For a step-by-step tutorial, see [Getting started with elastic queries for sharding (horizontal partitioning)][sharded_eq_tutorial].

### B. Create an RDBMS external data source

To create an external data source to reference an RDBMS, specifies the SQL Database server name of the remote database in SQL Database.

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

CREATE DATABASE SCOPED CREDENTIAL SQL_Credential
WITH
  IDENTITY = '<username>' ,
  SECRET = '<password>' ;

CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc
WITH
  ( TYPE = RDBMS ,
    LOCATION = '<server_name>.database.windows.net' ,
    DATABASE_NAME = 'Customers' ,
    CREDENTIAL = SQL_Credential
  ) ;
```

For a step-by-step tutorial on RDBMS, see [Getting started with cross-database queries (vertical partitioning)][remote_eq_tutorial].

## Examples: Bulk operations

> [!IMPORTANT]  
> Do not add a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

### C. Create an external data source for bulk operations retrieving data from Azure Storage

Use the following data source for bulk operations using [BULK INSERT][bulk_insert] or [OPENROWSET][openrowset]. The credential must set `SHARED ACCESS SIGNATURE` as the identity, mustn't have the leading `?` in the SAS token, must have at least read permission on the file that should be loaded (for example `srt=o&sp=r`), and the expiration period should be valid (all dates are in UTC time). For more information on shared access signatures, see [Using Shared Access Signatures (SAS)][sas_token].

```sql
CREATE DATABASE SCOPED CREDENTIAL AccessAzureInvoices
WITH
  IDENTITY = 'SHARED ACCESS SIGNATURE',
  -- Remove ? from the beginning of the SAS token
  SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************' ;

CREATE EXTERNAL DATA SOURCE MyAzureInvoices
WITH
  ( LOCATION = 'https://newinvoices.blob.core.windows.net/week3' ,
    CREDENTIAL = AccessAzureInvoices ,
    TYPE = BLOB_STORAGE
  ) ;
```

To see this example in use, see [BULK INSERT][bulk_insert_example].

## Examples: Azure SQL Edge

> [!IMPORTANT]  
> For information on configuring external data for Azure SQL Edge, see [Data streaming in Azure SQL Edge](/azure/azure-sql-edge/stream-data).

### A. Create external data source to reference Kafka

**Applies to:** [Azure SQL Edge](/azure/azure-sql-edge/overview) *only*

In this example, the external data source is a Kafka server with IP address xxx.xxx.xxx.xxx and listening on port 1900. The Kafka external data source is only for data streaming and does not support predicate push down.

```sql
-- Create an External Data Source for Kafka
CREATE EXTERNAL DATA SOURCE MyKafkaServer WITH (
    LOCATION = 'kafka://xxx.xxx.xxx.xxx:1900'
)
GO
```

### B. Create external data source to reference EdgeHub

**Applies to:** [Azure SQL Edge](/azure/azure-sql-edge/overview) *only*

In this example, the external data source is a EdgeHub running on the same edge device as Azure SQL Edge. The edgeHub external data source is only for data streaming and does not support predicate push down.

```sql
-- Create an External Data Source for Kafka
CREATE EXTERNAL DATA SOURCE MyEdgeHub WITH (
    LOCATION = 'edgehub://'
)
go
```
## Next steps

- [What is Azure SQL Edge?](/azure/azure-sql-edge/overview)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]
- [Introduction to elastic query][intro_eq]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md
[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_etb]: /sql/t-sql/statements/create-external-data-source
[alter_eds]: ./alter-external-data-source-transact-sql.md
[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]:../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown
<!-- Elastic Query Docs -->
[intro_eq]: /azure/azure-sql/database/elastic-query-overview
[remote_eq]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[remote_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[sharded_eq]: /azure/azure-sql/database/elastic-query-getting-started
[sharded_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started

<!-- Azure Docs -->
[azure_ad]: /azure/data-lake-store/data-lake-store-authenticate-using-active-directory
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-external-data-source-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed<br />Instance](create-external-data-source-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-external-data-source-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: Azure Synapse Analytics

[!INCLUDE [Applies to](../../includes/applies-md.md)] [!INCLUDE[asa](../../includes/applies-to-version/_asa.md)]

Creates an external data source for PolyBase. External data sources are used to establish connectivity and support the primary use case of data virtualization and data load using [PolyBase][intro_pb].

> [!IMPORTANT]  
> To create an external data source to query a [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)] resource using Azure SQL Database with [elastic query][remote_eq], see [SQL Database](create-external-data-source-transact-sql.md?view=azuresqldb-current&preserve-view=true).

## <a id="syntax"></a> Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

### [[!INCLUDE[sss-dedicated-pool-md.md](../../includes/sss-dedicated-pool-md.md)]](#tab/dedicated)

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] TYPE = HADOOP ]
[ ; ]
```
### [[!INCLUDE[sssod-md.md](../../includes/sssod-md.md)]](#tab/serverless)

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
(    LOCATION = '<prefix>://<path>[:<port>]'
)
[;]
```
---

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)] in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source        | Connector location prefix | Location path                                         |
| --------------------------- | --------------- | ----------------------------------------------------- |
| Azure Data Lake Store Gen 1 | `adl`           | `<storage_account>.azuredatalake.net`                 |
| Azure Data Lake Store Gen2 | `abfs[s]`       | `<container>@<storage_account>.dfs.core.windows.net`  |
| Azure V2 Storage account    | `wasb[s]`       | `<container>@<storage_account>.blob.core.windows.net` |

Location path:

- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.

Additional notes and guidance when setting the location:

- The default option is to use `enable secure SSL connections` when provisioning Azure Data Lake Storage Gen2. When this is enabled, you must use `abfss` when a secure TLS/SSL connection is selected. Note `abfss`works for unsecure TLS connections as well. For more information, see [the Azure Blob Filesystem driver (ABFS)](/azure/storage/blobs/data-lake-storage-abfs-driver).
- Azure Synapse doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- `wasbs` is recommended as data will be sent using a secure TLS connection.
- Hierarchical Namespaces aren't supported with Azure V2 Storage Accounts when accessing data via PolyBase using the wasb:// interface.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load data from Azure Storage or Azure Data Lake Store (ADLS) Gen2 into [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], use an Azure Storage Key.
- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.

To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].

#### TYPE = *HADOOP*

Specifies the type of the external data source being configured. This parameter isn't always required.

Use HADOOP when the external data source is Azure Storage, ADLS Gen 1, or ADLS Gen2.

For an example of using `TYPE` = `HADOOP` to load data from Azure Storage, see [Create external data source to reference Azure Data Lake Store Gen 1 or 2 using a service principal](#b-create-external-data-source-to-reference-azure-data-lake-store-gen-1-or-2-using-a-service-principal).

## Permissions

Requires `CONTROL` permission on the database.

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

Shared Access Signature (SAS) keys are supported for authenticating to Azure Data Lake Store Gen 2 Storage Accounts. Customers who want to authenticate by using a Shared Access Signature must create a database scoped credential where `IDENTITY = "Shared Access Signature"` and enter a SAS token as the secret. 

If you create a database scoped credential where `IDENTITY = "Shared Access Signature"` and use a storage key value as the secret, you'll get the following error message: 

`'HdfsBridge::isDirExist - Unexpected error encountered checking whether directory exists or not: AbfsRestOperationException: Operation failed: "Server failed to authenticate the request. Please refer to the information in the www-authenticate header.", 401, HEAD, [Storage path URL]'`

## Examples

### A. Create external data source to access data in Azure Storage using the wasb:// interface

In this example, the external data source is an Azure V2 Storage account named `logs`. The storage container is called `daily`. The Azure Storage external data source is for data transfer only. It doesn't support predicate push-down. Hierarchical namespaces are not supported when accessing data via the `wasb://` interface. Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).

This example shows how to create the database scoped credential for authentication to Azure Storage. Specify the Azure Storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>',
  SECRET = '<azure_storage_account_key>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'wasbs://daily@logs.blob.core.windows.net/' ,
    CREDENTIAL = AzureStorageCredential ,
    TYPE = HADOOP
  ) ;
```

### B. Create external data source to reference Azure Data Lake Store Gen 1 or 2 using a service principal

Azure Data Lake Store connectivity can be based on your ADLS URI and your Azure Active directory Application's service principal. Documentation for creating this application can be found at [Data lake store authentication using Active Directory][azure_ad].

```sql
-- If you do not have a Master Key on your DW you will need to create one.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- These values come from your Azure Active Directory Application used to authenticate to ADLS
CREATE DATABASE SCOPED CREDENTIAL ADLS_credential
WITH
  -- IDENTITY = '<clientID>@<OAuth2.0TokenEndPoint>' ,
  IDENTITY = '536540b4-4239-45fe-b9a3-629f97591c0c@https://login.microsoftonline.com/42f988bf-85f1-41af-91ab-2d2cd011da47/oauth2/token' ,
  -- SECRET = '<KEY>'
  SECRET = 'BjdIlmtKp4Fpyh9hIvr8HJlUida/seM5kQ3EpLAmeDI='
;

-- For Gen 1 - Create an external data source
-- TYPE: HADOOP - PolyBase uses Hadoop APIs to access data in Azure Data Lake Storage.
-- LOCATION: Provide Data Lake Storage Gen 1 account name and URI
-- CREDENTIAL: Provide the credential created in the previous step
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH
  ( LOCATION = 'adl://newyorktaxidataset.azuredatalakestore.net' ,
    CREDENTIAL = ADLS_credential ,
    TYPE = HADOOP
  ) ;

-- For Gen2 - Create an external data source
-- TYPE: HADOOP - PolyBase uses Hadoop APIs to access data in Azure Data Lake Storage.
-- LOCATION: Provide Data Lake Storage Gen2 account name and URI
-- CREDENTIAL: Provide the credential created in the previous step
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH
  -- Please note the abfss endpoint when your account has secure transfer enabled
  ( LOCATION = 'abfss://data@newyorktaxidataset.dfs.core.windows.net' ,
    CREDENTIAL = ADLS_credential ,
    TYPE = HADOOP
  ) ;
```

### C. Create external data source to reference Azure Data Lake Store Gen2 using the storage account key

```sql
-- If you do not have a Master Key on your DW you will need to create one.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

CREATE DATABASE SCOPED CREDENTIAL ADLS_credential
WITH
-- IDENTITY = '<storage_account_name>' ,
  IDENTITY = 'newyorktaxidata' ,
-- SECRET = '<storage_account_key>'
  SECRET = 'yz5N4+bxSb89McdiysJAzo+9hgEHcJRJuXbF/uC3mhbezES/oe00vXnZEl14U0lN3vxrFKsphKov16C0w6aiTQ=='
;

-- Note this example uses a Gen2 secured endpoint (abfss)
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( LOCATION = 'abfss://2013@newyorktaxidataset.dfs.core.windows.net' ,
    CREDENTIAL = ADLS_credential ,
    TYPE = HADOOP
  ) ;
```

### D. Create external data source to reference PolyBase connectivity to Azure Data Lake Store Gen2 using abfs://

There is no need to specify SECRET when connecting to Azure Data Lake Store Gen2 account with [Managed Identity](/azure/active-directory/managed-identities-azure-resources/overview) mechanism.

```sql
-- If you do not have a Master Key on your DW you will need to create one
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

--Create database scoped credential with **IDENTITY = 'Managed Service Identity'**

CREATE DATABASE SCOPED CREDENTIAL msi_cred
WITH IDENTITY = 'Managed Service Identity' ;

--Create external data source with abfss:// scheme for connecting to your Azure Data Lake Store Gen2 account

CREATE EXTERNAL DATA SOURCE ext_datasource_with_abfss
WITH
  ( TYPE = HADOOP ,
    LOCATION = 'abfss://myfile@mystorageaccount.dfs.core.windows.net' ,
    CREDENTIAL = msi_cred
  ) ;
```

## Next steps

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [CREATE EXTERNAL TABLE AS SELECT (Azure Synapse Analytics)][create_etb_as_sel]
- [CREATE TABLE AS SELECT (Azure Synapse Analytics)][create_tbl_as_sel]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md

[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: /sql/t-sql/statements/create-external-data-source
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true

[alter_eds]: ./alter-external-data-source-transact-sql.md

[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]:../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown
<!-- Elastic Query Docs -->
[intro_eq]: /azure/azure-sql/database/elastic-query-overview
[remote_eq]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[remote_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[sharded_eq]: /azure/azure-sql/database/elastic-query-getting-started
[sharded_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started

<!-- Azure Docs -->
[azure_ad]: /azure/data-lake-store/data-lake-store-authenticate-using-active-directory
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end
::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-external-data-source-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed<br />Instance](create-external-data-source-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-external-data-source-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Analytics<br />Platform System (PDW) \*_** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: Analytics Platform System

[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

Creates an external data source for PolyBase queries. External data sources are used to establish connectivity and support the following use case: Data virtualization and data load using [PolyBase][intro_pb].

## <a id="syntax"></a> Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] TYPE = HADOOP ]
    [ [ , ] RESOURCE_MANAGER_LOCATION = '<resource_manager>[:<port>]' )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the server in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source    | Connector location prefix | Location path                                         |
| ----------------------- | --------------- | ----------------------------------------------------- |
| Cloudera CDH or Hortonworks HDP | `hdfs`          | `<Namenode>[:port]`                                   |
| Azure Storage Account   | `wasb[s]`       | `<container>@<storage_account>.blob.core.windows.net` |

Location path:

- `<Namenode>` = the machine name, name service URI, or IP address of the `Namenode` in the Hadoop cluster. PolyBase must resolve any DNS names used by the Hadoop cluster. <!-- For highly available Hadoop configurations, provide the Nameservice ID as the `LOCATION`. -->
- `port` = The port that the external data source is listening on. In Hadoop, the port can be found using the `fs.defaultFS` configuration parameter. The default is 8020.
- `<container>` = the container of the storage account holding the data. Root containers are read-only, data can't be written back to the container.
- `<storage_account>` = the storage account name of the Azure resource.

Additional notes and guidance when setting the location:

- The PDW engine doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.
- Use the same external data source for all tables when querying Hadoop to ensure consistent querying semantics.
- `wasbs` is recommended as data will be sent using a secure TLS connection.
- Hierarchical Namespaces are not supported when used with Azure Storage accounts over wasb://.
- To ensure successful PolyBase queries during a Hadoop `Namenode` fail-over, consider using a virtual IP address for the `Namenode` of the Hadoop cluster. If you don't, execute an [ALTER EXTERNAL DATA SOURCE][alter_eds] command to point to the new location.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load data from Azure Storage into Azure Synapse or PDW, use an Azure Storage Key.
- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.

#### TYPE = *[ HADOOP ]*

Specifies the type of the external data source being configured. This parameter isn't always required.

- Use HADOOP when the external data source is Cloudera CDH, Hortonworks HDP, or Azure Storage.

For an example of using `TYPE` = `HADOOP` to load data from Azure Storage, see [Create external data source to reference Hadoop](#a-create-external-data-source-to-reference-hadoop).

#### RESOURCE_MANAGER_LOCATION = *'ResourceManager_URI[:port]'*

In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], do not specify RESOURCE_MANAGER_LOCATION unless connecting to Cloudera CDH, Hortonworks HDP, an Azure Storage account.

Configure this optional value when connecting to Cloudera CDH, Hortonworks HDP, or an Azure Storage account only. For a complete list of supported Hadoop versions, see [PolyBase Connectivity Configuration (Transact-SQL)][connectivity_pb].

When the `RESOURCE_MANAGER_LOCATION` is defined, the query optimizer will make a cost-based decision to improve performance. A MapReduce job can be used to push down the computation to Hadoop. Specifying the `RESOURCE_MANAGER_LOCATION` can significantly reduce the volume of data transferred between Hadoop and SQL, which can lead to improved query performance.

If the Resource Manager isn't specified, pushing compute to Hadoop is disabled for PolyBase queries. [Create external data source to reference Hadoop with push-down enabled](#b-create-external-data-source-to-reference-hadoop-with-push-down-enabled) provides a concrete example and further guidance.

The RESOURCE_MANAGER_LOCATION value is not validated when you create the external data source. Entering an incorrect value may cause query failure at execution time whenever push-down is attempted as the provided value would not be able to resolve.

In order for PolyBase to function correctly with a Hadoop external data source, the ports for the following Hadoop cluster components must be open:

- HDFS ports
    - Namenode
    - Datanode
- Resource Manager
    - Job submission
    - Job history

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

The following table shows the default ports for these components. Note that there is Hadoop version dependency as well as the possibility of custom configuration that doesn't use the default port assignment.

| **Hadoop cluster component** | **Default Port** |
| :-- | :-- |
| NameNode | 8020 |
| DataNode (Data transfer, non-privilege IPC port) | 50010 |
| DataNode (Data transfer, privilege IPC port) | 1019 |
| Resource Manager Job Submission (Hortonworks 1.3)| 50300|
| Resource Manager Job Submission (Cloudera 4.3)|8021|
| Resource Manager Job Submission (Hortonworks 2.0 on Windows, Cloudera 5.x on Linux) | 8032 |
| Resource Manager Job Submission (Hortonworks 2.x, 3.0 on Linux, Hortonworks 2.1-3 on Windows) | 8050|
| Resource Manager Job History | 10020|

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

> [!NOTE]  
> In previous releases of PDW, create external data source required `ALTER ANY EXTERNAL DATA SOURCE` permissions.

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Security

PolyBase supports proxy based authentication for most external data sources. Create a database scoped credential to create the proxy account.

An SAS token with type `HADOOP` is unsupported. It's only supported with type = `BLOB_STORAGE` when a storage account access key is used instead. Attempting to create an external data source with type `HADOOP` and a SAS credential fails with the following error:

`Msg 105019, Level 16, State 1 - EXTERNAL TABLE access failed due to internal error: 'Java exception raised on call to HdfsBridge_Connect. Java exception message: Parameters provided to connect to the Azure storage account are not valid.: Error [Parameters provided to connect to the Azure storage account are not valid.] occurred while accessing external file.'`

## Examples

### A. Create external data source to reference Hadoop

To create an external data source to reference your Hortonworks HDP or Cloudera CDH, specify the machine name, or IP address of the Hadoop `Namenode` and port. <!-- Provide the Nameservice ID as the `LOCATION` for highly available configurations. -->

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    TYPE = HADOOP
  ) ;
```

### B. Create external data source to reference Hadoop with push-down enabled

Specify the `RESOURCE_MANAGER_LOCATION` option to enable push-down computation to Hadoop for PolyBase queries. Once enabled, PolyBase makes a cost-based decision to determine whether the query computation should be pushed to Hadoop.

```sql
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8020'
    TYPE = HADOOP
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
) ;
```

### C. Create external data source to reference Kerberos-secured Hadoop

To verify if the Hadoop cluster is Kerberos-secured, check the value of `hadoop.security.authentication` property in Hadoop core-site.xml. To reference a Kerberos-secured Hadoop cluster, you must specify a database scoped credential that contains your Kerberos username and password. The database master key is used to encrypt the database scoped credential secret.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Kerberos user name and password.
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
WITH
  IDENTITY = '<hadoop_user_name>' ,
  SECRET = '<hadoop_password>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyHadoopCluster
WITH
  ( LOCATION = 'hdfs://10.10.10.10:8050' ,
    CREDENTIAL = HadoopUser1 ,
    TYPE = HADOOP ,
    RESOURCE_MANAGER_LOCATION = '10.10.10.10:8050'
  ) ;
```

### D. Create external data source to access data in Azure Storage using the wasb:// interface

In this example, the external data source is an Azure V2 Storage account named `logs`. The storage container is called `daily`. The Azure Storage external data source is for data transfer only. It doesn't support predicate push-down. Hierarchical namespaces are not supported when accessing data via the `wasb://` interface. Note that when connecting to the Azure Storage via the WASB[s] connector, authentication must be done with a storage account key, not with a shared access signature (SAS).

This example shows how to create the database scoped credential for authentication to Azure storage. Specify the Azure storage account key in the database credential secret. You can specify any string in database scoped credential identity as it isn't used during authentication to Azure storage.

```sql
-- Create a database master key if one does not already exist, using your own password. This key is used to encrypt the credential secret in next step.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>' ;

-- Create a database scoped credential with Azure storage account key as the secret.
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential
WITH
  IDENTITY = '<my_account>' ,
  SECRET = '<azure_storage_account_key>' ;

-- Create an external data source with CREDENTIAL option.
CREATE EXTERNAL DATA SOURCE MyAzureStorage
WITH
  ( LOCATION = 'wasbs://daily@logs.blob.core.windows.net/'
    CREDENTIAL = AzureStorageCredential
    TYPE = HADOOP
  ) ;
```

## Next steps

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL FILE FORMAT (Transact-SQL)][create_eff]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]

<!-- links to external pages -->
<!-- SQL Docs -->
[bulk_insert]: ./bulk-insert-transact-sql.md
[bulk_insert_example]: ./bulk-insert-transact-sql.md#f-import-data-from-a-file-in-azure-blob-storage
[openrowset]: ../functions/openrowset-transact-sql.md
[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_eff]: ./create-external-file-format-transact-sql.md
[create_etb]: /sql/t-sql/statements/create-external-data-source
[create_etb_as_sel]: ./create-external-table-as-select-transact-sql.md?view=azure-sqldw-latest&preserve-view=true
[create_tbl_as_sel]: ./create-table-as-select-azure-sql-data-warehouse.md?view=azure-sqldw-latest&preserve-view=true
[alter_eds]: ./alter-external-data-source-transact-sql.md
[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- PolyBase docs -->
[intro_pb]: ../../relational-databases/polybase/polybase-guide.md
[mongodb_pb]: ../../relational-databases/polybase/polybase-configure-mongodb.md
[connectivity_pb]:../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md
[hint_pb]: ../../relational-databases/polybase/polybase-pushdown-computation.md#force-pushdown
<!-- Elastic Query Docs -->
[intro_eq]: /azure/azure-sql/database/elastic-query-overview
[remote_eq]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[remote_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started-vertical
[sharded_eq]: /azure/azure-sql/database/elastic-query-getting-started
[sharded_eq_tutorial]: /azure/azure-sql/database/elastic-query-getting-started
<!-- Azure Docs -->
[azure_ad]: /azure/data-lake-store/data-lake-store-authenticate-using-active-directory
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1
::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](create-external-data-source-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](create-external-data-source-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-external-data-source-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-external-data-source-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

## Overview: Azure SQL Managed Instance

[!INCLUDE [Applies to](../../includes/applies-md.md)] [!INCLUDE[asdbmi](../../includes/applies-to-version/_asmi.md)]

Creates an external data source in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)]. For complete information, see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview).

Data virtualization in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] provides access to external data in a variety of file formats via the [OPENROWSET T-SQL syntax](/azure/azure-sql/managed-instance/data-virtualization-overview#query-data-sources-using-openrowset) or the [CREATE EXTERNAL TABLE](create-external-table-transact-sql.md) T-SQL syntax.

## <a id="syntax"></a> Syntax

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE EXTERNAL DATA SOURCE <data_source_name>
WITH
  ( [ LOCATION = '<prefix>://<path>[:<port>]' ]
    [ [ , ] CREDENTIAL = <credential_name> ]
  )
[ ; ]
```

## Arguments

#### data_source_name

Specifies the user-defined name for the data source. The name must be unique within the database.

#### LOCATION = *`'<prefix>://<path[:port]>'`*

Provides the connectivity protocol and path to the external data source.

| External Data Source   | Location prefix | Location path                                         |
| ---------------------- | --------------- | ----------------------------------------------------- |
| Azure Blob Storage     | `abs`         | `abs://<container>@<storage_account>.blob.core.windows.net/<path>/<file_name>` |
| Azure Data Lake Service Gen2 | `adls`  | `adls://<container>@<storage_account>.dfs.core.windows.net/<path>/<file_name>` |

The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] doesn't verify the existence of the external data source when the object is created. To validate, create an external table using the external data source.

Do not add a trailing **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL when configuring an external data source for bulk operations.

#### CREDENTIAL = *credential_name*

Specifies a database-scoped credential for authenticating to the external data source.

Additional notes and guidance when creating a credential:

- To load data from Azure Storage into [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], use a Shared Access Signature (SAS token).
- `CREDENTIAL` is only required if the data has been secured. `CREDENTIAL` isn't required for data sets that allow anonymous access.
- If a credential is required, the credential must be created using `Managed Identity` or `SHARED ACCESS SIGNATURE` as the IDENTITY. To create a database scoped credential, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc].
- `TYPE` = `BLOB_STORAGE` is only permitted for bulk operations; you cannot create external tables for an external data source with `TYPE` = `BLOB_STORAGE`.

To use the managed service identity for the database scoped credential:
  - Specify `WITH IDENTITY = 'Managed Identity'`
    - Use the system-assigned managed service identity of the Azure SQL Managed Instance, which must be enabled if it is to be used for this purpose.

  - Grant the **Reader** Azure RBAC role to the system assigned managed service identity of the Azure SQL Managed Instance to the necessary Azure Blob Storage containers. For example, via the Azure portal, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

To create a shared access signature (SAS) for the database scoped credential:
  - Specify `WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = ...`
  - There are multiple ways to create a shared access signature:
    - You can get an SAS token by navigating to the **Azure portal -> <Your_Storage_Account> -> Shared access signature -> Configure permissions -> Generate SAS and connection string**. For more information, see [Generate a shared access signature](/azure/storage/blobs/blob-containers-portal#generate-a-shared-access-signature).
    - You can [create and configure an SAS with Azure Storage Explorer](/azure/vs-azure-tools-storage-explorer-blobs#get-the-sas-for-a-blob-container).
    - You can create an SAS programmatically via PowerShell, Azure CLI, .NET, and REST API. For more information, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json).
  - The SAS token should be configured as follows:
    - When an SAS token is generated, it includes a question mark ('?') at the beginning of the token. Exclude the leading `?` when configured as the SECRET.
    - Use a valid expiration period (all dates are in UTC time).
    - Grant at least read permission on the file that should be loaded (for example `srt=o&sp=r`). Multiple shared access signatures can be created for different use cases. Permissions should be granted as follows:

    |    Action    |    Permission    |
    |    ------    |    ----------    |
    |    Read data from a file    |    Read    |
    |    Read data from multiple files and subfolders    |    Read and List    |
    |    Use Create External Table as Select (CETAS)    |    Read, Create and Write    |

## Permissions

Requires `CONTROL` permission on database in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)].

## Locking

Takes a shared lock on the `EXTERNAL DATA SOURCE` object.

## Examples

For more examples, see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview).

### A. Query external data from Azure SQL Managed Instance with OPENROWSET or an external table

For more examples, see [Create external data source](create-external-data-source-transact-sql.md) or see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview).

1. Create the database master key, if it doesn't exist.

    ```sql
    -- Optional: Create MASTER KEY if it doesn't exist in the database:
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Strong Password>'
    GO
    ```
 
1. Create the database scoped credential using a SAS token. You can also use a managed identity.

    ```sql    
    CREATE DATABASE SCOPED CREDENTIAL MyCredential
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = '<KEY>' ; --Removing leading '?'
    GO
    ```

1. Create the external data source using the credential.

    ```sql
    --Create external data source pointing to the file path, and referencing database-scoped credential:
    CREATE EXTERNAL DATA SOURCE MyPrivateExternalDataSource
    WITH (
        LOCATION = 'abs://public@pandemicdatalake.blob.core.windows.net/curated/covid-19/bing_covid-19_data/latest'
            CREDENTIAL = [MyCredential]
    )
    GO
    ```

1. Query parquet data file in the external data source using the OPENROWSET T-SQL syntax, relying on schema inference to quickly explore data without knowing the schema.

    ```sql
    --Query data with OPENROWSET, relying on schema inference.
    SELECT TOP 10 *
    FROM OPENROWSET(
     BULK 'bing_covid-19_data.parquet',
     DATA_SOURCE = 'MyExternalDataSource',
     FORMAT = 'parquet'
    ) AS filerows
    ```

1. Or, query data using OPENROWSET the WITH clause, instead of relying on schema inference, which may query execution cost. On a CSV, schema inference is not supported.

    ```sql
    --Or, query data using the WITH clause on a CSV, where schema inference is not supported
    SELECT TOP 10 id, updated, confirmed, confirmed_change
    FROM OPENROWSET(
     BULK 'bing_covid-19_data.csv',
     DATA_SOURCE = 'MyExternalDataSource',
     FORMAT = 'CSV',
     FIRSTROW = 2
    )
    WITH (
     id int,
     updated date,
     confirmed int,
     confirmed_change int
    ) AS filerows
    ```

1. Or, create an EXTERNAL FILE FORMAT and an EXTERNAL TABLE, to query the data as if it were a local table.

    ```sql
    -- Or, create an EXTERNAL FILE FORMAT and an EXTERNAL TABLE
    
    --Create external file format
    CREATE EXTERNAL FILE FORMAT DemoFileFormat
    WITH (
     FORMAT_TYPE=PARQUET
    )
    GO
    
    --Create external table:
    CREATE EXTERNAL TABLE tbl_TaxiRides(
     vendorID VARCHAR(100) COLLATE Latin1_General_BIN2,
     tpepPickupDateTime DATETIME2,
     tpepDropoffDateTime DATETIME2,
     passengerCount INT,
     tripDistance FLOAT,
     puLocationId VARCHAR(8000),
     doLocationId VARCHAR(8000),
     startLon FLOAT,
     startLat FLOAT,
     endLon FLOAT,
     endLat FLOAT,
     rateCodeId SMALLINT,
     storeAndFwdFlag VARCHAR(8000),
     paymentType VARCHAR(8000),
     fareAmount FLOAT,
     extra FLOAT,
     mtaTax FLOAT,
     improvementSurcharge VARCHAR(8000),
     tipAmount FLOAT,
     tollsAmount FLOAT,
     totalAmount FLOAT
    )
    WITH (
     LOCATION = 'yellow/puYear=*/puMonth=*/*.parquet',
     DATA_SOURCE = NYCTaxiExternalDataSource,
     FILE_FORMAT = MyFileFormat\.\./\.\./\.\./azure-sql/
    );
    GO
    
    --Then, query the data via an external table with T-SQL:
    SELECT TOP 10 *
    FROM tbl_TaxiRides;
    GO
    ```

## Next steps

- [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)][create_dsc]
- [CREATE EXTERNAL TABLE (Transact-SQL)][create_etb]
- [sys.external_data_sources (Transact-SQL)][cat_eds]
- [Using Shared Access Signatures (SAS)][sas_token]

<!-- links to external pages -->
<!-- SQL Docs -->
[create_dsc]: ./create-database-scoped-credential-transact-sql.md
[create_etb]: /sql/t-sql/statements/create-external-data-source
[alter_eds]: ./alter-external-data-source-transact-sql.md
[cat_eds]: ../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md
<!-- Azure Docs -->
[sas_token]: /azure/storage/storage-dotnet-shared-access-signature-part-1

::: moniker-end
