---
title: Using the Apache Spark Connector for SQL Server and Azure SQL
titleSuffix: SQL Server Big Data Clusters
description: Learn how to use the Apache Spark Connector for SQL Server and Azure SQL to read and write to SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 11/04/2019
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Use the Apache Spark Connector for SQL Server and Azure SQL

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

The [Apache Spark Connector for SQL Server and Azure SQL](https://github.com/microsoft/sql-spark-connector) is a high-performance connector that enables you to use transactional data in big data analytics and persists results for ad-hoc queries or reporting. The connector allows you to use any SQL database, on-premises or in the cloud, as an input data source or output data sink for Spark jobs. The connector uses SQL Server bulk write APIs. Any bulk write parameters can be passed as optional parameters by the user and are passed as-is by the connector to the underlying API. For more information about bulk write operations, see [Using bulk copy with the JDBC driver]( ../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

The connector is included by default in SQL Server Big Data Clusters.

Learn more about the connector at the [open source repository](https://github.com/microsoft/sql-spark-connector). For examples, see [samples](https://github.com/microsoft/sql-spark-connector/tree/master/samples).

## Write to a new SQL Table

>[!CAUTION]
> In `overwrite` mode, the connector first drops the table if it already exists in the database by default. Use this option with due care to avoid unexpected data loss.
> 
> When using mode `overwrite` if you do not use the option `truncate`, on re-creation of the table, indexes will be lost. For example, a columnstore table becomes a heap. If you want to maintain existing indexing please also specify option `truncate` with value `true`. For example `.option("truncate",true)`

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

## Append to SQL Table
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

This connector by default uses READ_COMMITTED isolation level when performing the bulk insert into the database. If you wish to override this to another isolation level, please use the `mssqlIsolationLevel` option as shown below.
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

## Non-Active Directory mode

In non-Active Directory mode security, each user has a username and password which need to be provided as parameters during the connector instantiation to perform read and/or writes.

An example connector instantiation for non-Active Directory mode is below. Before you run the script, replace the `?` with the value for your account.

```python
# Note: '?' is a placeholder for a necessary user-specified value
connector_type = "com.microsoft.sqlserver.jdbc.spark" 

url = "jdbc:sqlserver://master-p-svc;databaseName=?;"
writer = df.write \ 
   .format(connector_type)\ 
   .mode("overwrite") 
   .option("url", url) \ 
   .option("user", ?) \ 
   .option("password",?) 
writer.save() 
```

## Active Directory mode

In Active Directory mode security, after a user has generated a key tab file, the user needs to provide the `principal` and `keytab` as parameters during the connector instantiation.

In this mode, the driver loads the keytab file to the respective executor containers. Then, the executors use the principal name and keytab to generate a token that is used to create a JDBC connector for read/write.

An example connector instantiation for Active Directory mode is below. Before you run the script, replace the `?` with the value for your account.

```python
# Note: '?' is a placeholder for a necessary user-specified value
connector_type = "com.microsoft.sqlserver.jdbc.spark"

url = "jdbc:sqlserver://master-p-svc;databaseName=?;integratedSecurity=true;authenticationScheme=JavaKerberos;" 
writer = df.write \ 
   .format(connector_type)\ 
   .mode("overwrite") 
   .option("url", url) \ 
   .option("principal", ?) \ 
   .option("keytab", ?)   

writer.save() 
```

## Next steps

For more information about big data clusters, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)

Have feedback or feature recommendations for SQL Server Big Data Clusters? [Leave us a note at SQL Server Big Data Clusters Feedback](https://aka.ms/sql-server-bdc-feedback).
