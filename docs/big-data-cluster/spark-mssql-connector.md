---
title: Connect Spark to SQL Server
titleSuffix: SQL Server big data clusters
description: Learn how to use the MSSQL Spark Connector in Spark to read and write to SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: shivsood
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to read and write to SQL Server from Spark using the MSSQL Spark Connector

A key big data usage pattern is high volume data processing in Spark, followed by writing the data to SQL Server for access to line-of-business applications. These usage patterns benefit from a connector that utilizes key SQL optimizations and provides an efficient write mechanism.

This article provides an example of how to use the MSSQL Spark connector to read and write to the following locations within a big data cluster:

1. The SQL Server master instance
1. The SQL Server data pool

   ![MSSQL Spark connector diagram](./media/spark-mssql-connector/mssql-spark-connector-diagram.png)

The sample performs the following tasks:

- Read a file from HDFS and do some basic processing.
- Write the dataframe to a SQL Server master instance as a SQL table and then read the table to a dataframe.
- Write the dataframe to a SQL Server data pool as a SQL external table and then read the external table to a dataframe.

## MSSQL Spark Connector Interface

SQL Server 2019 provides the **MSSQL Spark connector** for big data clusters that uses SQL Server bulk write APIs for Spark to SQL writes. MSSQL Spark Connector is based on Spark data source APIs and provides a familiar Spark JDBC connector interface. For interface parameters refer [Apache Spark documentation](http://spark.apache.org/docs/latest/sql-data-sources-jdbc.html). The MSSQL Spark connector is referenced by the name **com.microsoft.sqlserver.jdbc.spark**.

The following table describes interface parameters that have changed or are new:

| Property name | Optional | Description |
|---|---|---|
| **isolationLevel** | Yes | This describes the isolation level of the connection. The default for MSSQLSpark Connector is **READ_COMMITTED** |

The connector uses SQL Server Bulk write APIs. Any bulk write parameters can be passed as optional parameters by the user and are passed as-is by the connector to the underlying API. For more information about bulk write operations, see [SQLServerBulkCopyOptions]( ../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions).

## Prerequisites

- A [SQL Server big data cluster](deploy-get-started.md).

- [Azure Data Studio](https://aka.ms/getazuredatastudio).

## Create the target database

1. Open Azure Data Studio, and [connect to the SQL Server master instance of your big data cluster](connect-to-big-data-cluster.md).

1. Create a new query, and run the following command to create a sample database named **MyTestDatabase**.

   ```sql
   Create DATABASE MyTestDatabase
   GO
   ```

## Load sample data into HDFS

1. Download [AdultCensusIncome.csv](https://amldockerdatasets.azureedge.net/AdultCensusIncome.csv) to your local machine.

1. Launch Azure Data Studio, and [connect to your big data cluster](connect-to-big-data-cluster.md).

1. Right-click on the HDFS folder in your big data cluster, and select **New directory**. Name the directory **spark_data**.

1. Right click on the **spark_data** directory, and select **Upload files**. Upload the **AdultCensusIncome.csv** file.

   ![AdultCensusIncome CSV file](./media/spark-mssql-connector/spark_data.png)

## Run the sample notebook

To demonstrate the use of the MSSQL Spark Connector with this data, you can download a sample notebook, open it in Azure Data Studio, and run each code block. For more information about working with notebooks, see [How to use notebooks in SQL Server](notebooks-guidance.md).

1. From a PowerShell or bash command line, run the following command to download the **mssql_spark_connector.ipynb** sample notebook:

   ```PowerShell
   curl -o mssql_spark_connector.ipynb "https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/spark/data-virtualization/mssql_spark_connector.ipynb"
   ```

1. In Azure Data Studio, open the sample notebook file. Verify that it is connected to your HDFS/Spark Gateway for your big data cluster.

1. Run each code cell in the sample to see usage of MSSQL Spark connector.

## Next steps

For more information about big data clusters, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)