---
title: "Apache Spark Connector for SQL Server and Azure SQL"
description: "Learn how to use the Apache Spark connector for SQL Server and Azure SQL."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Apache Spark connector: SQL Server and Azure SQL

The Apache Spark connector for SQL Server and Azure SQL is a high-performance connector that you can use to include transactional data in big data analytics and persist results for ad hoc queries or reporting. By using the connector, you can use any SQL database, on-premises or in the cloud, as an input data source or output data sink for Spark jobs.

> [!NOTE]  
> This connector is no longer maintained. The upstream project was archived in February 2025, and this article is only retained for archival purposes. For new work against SQL Server or Azure SQL, use the [Apache Spark built-in JDBC data source](https://spark.apache.org/docs/latest/sql-data-sources-jdbc.html) with the [Microsoft JDBC Driver for SQL Server](../jdbc/microsoft-jdbc-driver-for-sql-server.md).

This library contains the source code for the Apache Spark Connector for SQL Server and Azure SQL platforms.

[Apache Spark](https://spark.apache.org/) is a unified analytics engine for large-scale data processing.

Import the connector using the coordinate that matches your Spark version:

| Connector | Maven Coordinate |
| --- | --- |
| Spark 2.4.x compatible connector | `com.microsoft.azure:spark-mssql-connector:1.0.2` |
| Spark 3.0.x compatible connector | `com.microsoft.azure:spark-mssql-connector_2.12:1.1.0` |
| Spark 3.1.x compatible connector | `com.microsoft.azure:spark-mssql-connector_2.12:1.2.0` |
| Spark 3.3.x compatible connector (beta) | `com.microsoft.azure:spark-mssql-connector_2.12:1.3.0-BETA` |
| Spark 3.4.x compatible connector (beta) | No Maven coordinate. Download the `spark-mssql-connector_2.12-1.4.0-BETA.jar` asset from the [GitHub releases page](https://github.com/microsoft/sql-spark-connector/releases). |

The Spark 3.3.x and Spark 3.4.x connectors are beta releases, and neither reached general availability before the project was archived. The 1.3.0-BETA release isn't compatible with the Microsoft JDBC Driver for SQL Server 7.0.1, Spark 2.4, or Spark 3.0. The upstream 1.3.0 release notes tell you to import version `1.3.0`, but that version was never published. Use `1.3.0-BETA` instead.

You can also build the connector from source or download the JAR from the Release section in GitHub. For source code and release history, see the archived [SQL Spark connector GitHub repository](https://github.com/microsoft/sql-spark-connector).

## Supported features

- Support for all Spark bindings (Scala, Python, R)
- Basic authentication and Active Directory (AD) Key Tab support
- Reordered `dataframe` write support
- Support for write to SQL Server Single instance and Data Pool in SQL Server Big Data Clusters
- Reliable connector support for Sql Server Single Instance

| Component | Versions supported |
| --- | --- |
| Apache Spark | 2.4.x, 3.0.x, 3.1.x, 3.3.x (beta), 3.4.x (beta) |
| Scala | 2.11, 2.12 |
| Microsoft JDBC Driver for SQL Server | 8.4 |
| Microsoft SQL Server | SQL Server 2008 or later |
| Azure SQL Databases | Supported |

### Supported options

The Apache Spark Connector for SQL Server and Azure SQL supports the options defined in the [SQL DataSource JDBC](https://spark.apache.org/docs/latest/sql-data-sources-jdbc.html) article.

In addition, the connector supports the following options:

| Option | Default | Description |
| --- | --- | --- |
| `reliabilityLevel` | `BEST_EFFORT` | `BEST_EFFORT` or `NO_DUPLICATES`. `NO_DUPLICATES` implements a reliable insert in executor restart scenarios |
| `dataPoolDataSource` | `none` | `none` implies the value isn't set and the connector should write to SQL Server single instance. Set this value to data source name to write a data pool table in Big Data Clusters. |
| `isolationLevel` | `READ_COMMITTED` | Specify the [isolation level](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md) |
| `tableLock` | `false` | Implements an insert with `TABLOCK` option to improve write performance |
| `schemaCheckEnabled` | `true` | Disables strict data frame and SQL table schema check when set to false |

Set other [bulk copy options](../jdbc/using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions) as options on the `dataframe`. The connector passes these options to `bulkcopy` APIs on write.

## Performance comparison

Apache Spark Connector for SQL Server and Azure SQL is up to 15x faster than generic JDBC connector for writing to SQL Server. Performance characteristics vary on type, volume of data, options used, and might show variations between each run. The following performance results are the time taken to overwrite a SQL table with 143.9M rows in a spark `dataframe`. The spark `dataframe` is constructed by reading `store_sales` HDFS table generated using [spark TPCDS Benchmark](https://github.com/databricks/spark-sql-perf). Time to read `store_sales` to `dataframe` is excluded. The results are averaged over three runs.

| Connector Type | Options | Description | Time to write |
| --- | --- | --- | --- |
| `JDBCConnector` | Default | Generic JDBC connector with default options | 1,385 seconds |
| `sql-spark-connector` | `BEST_EFFORT` | Best effort `sql-spark-connector` with default options | 580 seconds |
| `sql-spark-connector` | `NO_DUPLICATES` | Reliable `sql-spark-connector` | 709 seconds |
| `sql-spark-connector` | `BEST_EFFORT` + tabLock=true | Best effort `sql-spark-connector` with table lock enabled | 72 seconds |
| `sql-spark-connector` | `NO_DUPLICATES` + tabLock=true | Reliable `sql-spark-connector` with table lock enabled | 198 seconds |

Config

- Spark config: num_executors = 20, executor_memory = '1664 m', executor_cores = 2
- Data Gen config: scale_factor=50, partitioned_tables=true
- Data file `store_sales` with number of rows 143,997,590

Environment

- [SQL Server Big Data Cluster](../../big-data-cluster/release-notes-big-data-cluster.md) CU5
- `master` + 6 nodes
- Each node a Gen-5 server, with 512 GB RAM, 4-TB NVM per node, and 10-Gbps NIC

## Commonly faced issues

### `java.lang.NoClassDefFoundError: com/microsoft/aad/adal4j/AuthenticationException`

This error occurs when you use an older version of the `mssql` driver in your Hadoop environment. The connector now includes this driver. If you previously used the Azure SQL Connector and manually installed drivers on your cluster for Microsoft Entra authentication compatibility, remove those drivers.

To fix the error:

1. If you're using a generic Hadoop environment, check and remove the `mssql` JAR with the following command: `rm $HADOOP_HOME/share/hadoop/yarn/lib/mssql-jdbc-6.2.1.jre7.jar`.
   If you're using Databricks, add a global or cluster init script to remove old versions of the `mssql` driver from the `/databricks/jars` folder, or add this line to an existing script: `rm /databricks/jars/*mssql*`
1. Add the `adal4j` and `mssql` packages. For example, you can use Maven but any way should work.

   > [!CAUTION]  
   > Don't install the SQL spark connector this way.

1. Add the driver class to your connection configuration. For example:

   ```csharp
   connectionProperties = {
     `Driver`: `com.microsoft.sqlserver.jdbc.SQLServerDriver`
   }`
   ```

For more information, see the resolution to [https://github.com/microsoft/sql-spark-connector/issues/26](https://github.com/microsoft/sql-spark-connector/issues/26#issuecomment-672006339).

## Get started

The Apache Spark Connector for SQL Server and Azure SQL is based on the Spark DataSourceV1 API and SQL Server Bulk API. It uses the same interface as the built-in JDBC Spark-SQL connector. By using this integration, you can easily integrate the connector and migrate your existing Spark jobs by updating the format parameter with `com.microsoft.sqlserver.jdbc.spark`.

To include the connector in your projects, download this repository and build the JAR using SBT.

## Write to a new SQL table

> [!CAUTION]  
> The `overwrite` mode first drops the table if it already exists in the database. Use this option with care to avoid unexpected data loss.

If you use mode `overwrite` without the option `truncate` when recreating the table, the operation removes indexes. Also, a columnstore table changes to a heap table. To keep existing indexes, set the `truncate` option to `true`. For example, `.option("truncate","true")`.

```python
server_name = "jdbc:sqlserver://{SERVER_ADDR}"
database_name = "database_name"
url = server_name + ";" + "databaseName=" + database_name + ";"

table_name = "table_name"
username = "username"
password = "password123!#" # Please specify password here

try:
  df.write \
    .format("com.microsoft.sqlserver.jdbc.spark") \
    .mode("overwrite") \
    .option("url", url) \
    .option("dbtable", table_name) \
    .option("user", username) \
    .option("password", password) \
    .save()
except ValueError as error :
    print("Connector write failed", error)
```

### Append to SQL table

```python
try:
  df.write \
    .format("com.microsoft.sqlserver.jdbc.spark") \
    .mode("append") \
    .option("url", url) \
    .option("dbtable", table_name) \
    .option("user", username) \
    .option("password", password) \
    .save()
except ValueError as error :
    print("Connector write failed", error)
```

## Specify the isolation level

This connector uses the `READ_COMMITTED` isolation level by default when it bulk inserts data into the database. To override the isolation level, use the `mssqlIsolationLevel` option:

```python
    .option("mssqlIsolationLevel", "READ_UNCOMMITTED") \
```

## Read from SQL table

```python
jdbcDF = spark.read \
        .format("com.microsoft.sqlserver.jdbc.spark") \
        .option("url", url) \
        .option("dbtable", table_name) \
        .option("user", username) \
        .option("password", password).load()
```

<a id="azure-active-directory-authentication"></a>

## Microsoft Entra authentication

### Python example with service principal

```python
context = adal.AuthenticationContext(authority)
token = context.acquire_token_with_client_credentials(resource_app_id_url, service_principal_id, service_principal_secret)
access_token = token["accessToken"]

jdbc_db = spark.read \
        .format("com.microsoft.sqlserver.jdbc.spark") \
        .option("url", url) \
        .option("dbtable", table_name) \
        .option("accessToken", access_token) \
        .option("encrypt", "true") \
        .option("hostNameInCertificate", "*.database.windows.net") \
        .load()
```

### Python example with Active Directory password

```python
jdbc_df = spark.read \
        .format("com.microsoft.sqlserver.jdbc.spark") \
        .option("url", url) \
        .option("dbtable", table_name) \
        .option("authentication", "ActiveDirectoryPassword") \
        .option("user", user_name) \
        .option("password", password) \
        .option("encrypt", "true") \
        .option("hostNameInCertificate", "*.database.windows.net") \
        .load()
```

To authenticate by using Active Directory, install the required dependency.

When you use `ActiveDirectoryPassword`, the `user` value should be in the UPN format, such as `username@domainname.com`.

For **Scala**, install the `com.microsoft.aad.adal4j` artifact.

For **Python**, install the `adal` library. This library is available through pip.

For examples, see the [sample notebooks](https://github.com/microsoft/sql-spark-connector/tree/master/samples).

## Related content

- [SQL Spark connector GitHub repository](https://github.com/microsoft/sql-spark-connector)
