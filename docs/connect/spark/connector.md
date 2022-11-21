---
title: "Apache Spark connector for SQL Server"
description: "Learn how to use the Apache Spark connector for SQL Server."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 01/18/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Apache Spark connector: SQL Server & Azure SQL

The Apache Spark connector for SQL Server and Azure SQL is a high-performance connector that enables you to use transactional data in big data analytics and persist results for ad-hoc queries or reporting. The connector allows you to use any SQL database, on-premises or in the cloud, as an input data source or output data sink for Spark jobs.

This library contains the source code for the Apache Spark Connector for SQL Server and Azure SQL.

[Apache Spark](https://spark.apache.org/) is a unified analytics engine for large-scale data processing.

There are two versions of the connector available through Maven, a 2.4.x compatible version and a 3.0.x compatible version. Both versions can be found [here](https://search.maven.org/search?q=spark-mssql-connector) and can be imported using the coordinates below:

| Connector | Maven Coordinate |
| --------- | ------------------ |
| Spark 2.4.x compatible connector | `com.microsoft.azure:spark-mssql-connector:1.0.2` |
| Spark 3.0.x compatible connector | `com.microsoft.azure:spark-mssql-connector_2.12:1.1.0` |
| Spark 3.1.x compatible connector | `com.microsoft.azure:spark-mssql-connector_2.12:1.2.0` |

You can also build the connector from source or download the jar from the Release section in GitHub. For the latest information about the connector, see [SQL Spark connector GitHub repository](https://github.com/microsoft/sql-spark-connector).

## Supported Features

* Support for all Spark bindings (Scala, Python, R)
* Basic authentication and Active Directory (AD) Key Tab support
* Reordered `dataframe` write support
* Support for write to SQL Server Single instance and Data Pool in SQL Server Big Data Clusters
* Reliable connector support for Sql Server Single Instance

| Component                            | Versions Supported              |
|--------------------------------------|---------------------------------|
| Apache Spark                         | 2.4.x, 3.0.x, 3.1.x |
| Scala                                | 2.11, 2.12                            |
| Microsoft JDBC Driver for SQL Server | 8.4                             |
| Microsoft SQL Server                 | SQL Server 2008 or later        |
| Azure SQL Databases                  | Supported                       |


### Supported Options
The Apache Spark Connector for SQL Server and Azure SQL supports the options defined here: [SQL DataSource JDBC](https://spark.apache.org/docs/latest/sql-data-sources-jdbc.html)

In addition following options are supported

| Option | Default | Description |
| --------- | ------------------ | ------------------------------------------ |
| `reliabilityLevel` | `BEST_EFFORT` | `BEST_EFFORT` or `NO_DUPLICATES`. `NO_DUPLICATES` implements an reliable insert in executor restart scenarios |
| `dataPoolDataSource` | `none` | `none` implies the value is not set and the connector should write to SQL Server single instance. Set this value to data source name to write a data pool table in Big Data Clusters|
| `isolationLevel` | `READ_COMMITTED` | Specify the isolation level |
| `tableLock` | `false` | Implements an insert with `TABLOCK` option to improve write performance |
| `schemaCheckEnabled` | `true` | Disables strict data frame and sql table schema check when set to false |

Other [bulk copy options](../jdbc/using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions) can be set as options on the `dataframe` and will be passed to `bulkcopy` APIs on write

## Performance comparison

Apache Spark Connector for SQL Server and Azure SQL is up to 15x faster than generic JDBC connector for writing to SQL Server. Performance characteristics vary on type, volume of data, options used, and may show run to run variations. The following performance results are the time taken to overwrite a SQL table with 143.9M rows in a spark `dataframe`. The spark `dataframe` is constructed by reading `store_sales` HDFS table generated using [spark TPCDS Benchmark](https://github.com/databricks/spark-sql-perf). Time to read `store_sales` to `dataframe` is excluded. The results are averaged over three runs.

| Connector Type | Options | Description |  Time to write |
| --------- | ------------------ | -------------------------------------| ---------- |
| `JDBCConnector` | Default | Generic JDBC connector with default options |  1385 seconds |
| `sql-spark-connector` | `BEST_EFFORT` | Best effort `sql-spark-connector`  with default options |580 seconds |
| `sql-spark-connector` | `NO_DUPLICATES ` | Reliable `sql-spark-connector` | 709 seconds |
| `sql-spark-connector` | `BEST_EFFORT` + tabLock=true | Best effort `sql-spark-connector` with table lock enabled | 72 seconds |
| `sql-spark-connector` | `NO_DUPLICATES ` + tabLock=true| Reliable `sql-spark-connector` with table lock enabled| 198 seconds |

Config

- Spark config: num_executors = 20, executor_memory = '1664 m', executor_cores = 2
- Data Gen config: scale_factor=50, partitioned_tables=true
- Data file `store_sales` with nr of rows 143,997,590

Environment

- [SQL Server Big Data Cluster](../../big-data-cluster/release-notes-big-data-cluster.md) CU5
- `master` + 6 nodes
- Each node gen 5 server, 512 GB Ram, 4 TB NVM per node, NIC 10 GB

## Commonly Faced Issues

### `java.lang.NoClassDefFoundError: com/microsoft/aad/adal4j/AuthenticationException`

This issue arises from using an older version of the mssql driver (which is now included in this connector) in your hadoop environment. If you are coming from using the previous Azure SQL Connector and have manually installed drivers onto that cluster for Azure Active Directory compatibility, you will need to remove those drivers.

Steps to fix the issue:

1. If you are using a generic Hadoop environment, check and remove the mssql jar: `rm $HADOOP_HOME/share/hadoop/yarn/lib/mssql-jdbc-6.2.1.jre7.jar`. 
If you are using Databricks, add a global or cluster init script to remove old versions of the mssql driver from the `/databricks/jars` folder, or add this line to an existing script: `rm /databricks/jars/*mssql*`
2. Add the `adal4j` and `mssql` packages. For example, you can use Maven but any way should work. 

   > [!CAUTION]
   > Do not install the SQL spark connector this way.

1. Add the driver class to your connection configuration. For example:

   ```csharp
   connectionProperties = {
     `Driver`: `com.microsoft.sqlserver.jdbc.SQLServerDriver`
   }`
   ```

For more information and explanation, see the resolution to [https://github.com/microsoft/sql-spark-connector/issues/26](https://github.com/microsoft/sql-spark-connector/issues/26#issuecomment-672006339).

## Get Started

The Apache Spark Connector for SQL Server and Azure SQL is based on the Spark DataSourceV1 API and SQL Server Bulk API and uses the same interface as the built-in JDBC Spark-SQL connector. This integration allows you to easily integrate the connector and migrate your existing Spark jobs by simply updating the format parameter with `com.microsoft.sqlserver.jdbc.spark`.

To include the connector in your projects, download this repository and build the jar using SBT.

## Write to a new SQL Table

> [!WARNING]
> The `overwrite` mode first drops the table if it already exists in the database by default. Please use this option with due care to avoid unexpected data loss.

When using mode `overwrite` if you do not use the option `truncate` on recreation of the table, indexes will be lost. , a columnstore table would now be a heap. If you want to maintain existing indexing please also specify option `truncate` with value true. For example, `.option("truncate","true")`.

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

### Append to SQL Table

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

This connector by default uses `READ_COMMITTED` isolation level when performing the bulk insert into the database. If you wish to override the isolation level, use the `mssqlIsolationLevel` option as shown below.

```python
    .option("mssqlIsolationLevel", "READ_UNCOMMITTED") \
```

## Read from SQL Table

```python
jdbcDF = spark.read \
        .format("com.microsoft.sqlserver.jdbc.spark") \
        .option("url", url) \
        .option("dbtable", table_name) \
        .option("user", username) \
        .option("password", password).load()
```

## Azure Active Directory Authentication

### Python Example with Service Principal

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

### Python Example with Active Directory Password

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

A required dependency must be installed in order to authenticate using Active Directory. 

The format of `user` when using ActiveDirectoryPassword should be the UPN format, for example `username@domainname.com`. 

For **Scala,** the `_com.microsoft.aad.adal4j_` artifact will need to be installed.

For **Python,** the `_adal_` library will need to be installed.  This is available via pip.

Check the [sample notebooks](https://github.com/microsoft/sql-spark-connector/tree/master/samples) for examples.

## Support

The Apache Spark Connector for Azure SQL and SQL Server is an open-source project. This connector does not come with any Microsoft support. For issues with or questions about the connector, create an Issue in this project repository. The connector community is active and monitoring submissions.

## Next steps

Visit the [SQL Spark connector GitHub repository](https://github.com/microsoft/sql-spark-connector).

For information about isolation levels, see [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).
