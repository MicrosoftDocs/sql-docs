---
title: Connect Spark to SQL Server
titleSuffix: SQL Server big data clusters
description: Learn how to use the MSSQL JDBC connector in Spark to read and write to SQL Server.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to read and write to SQL Server from Spark using the MSSQL JDBC Connector

This article provides an example of how to read and write to SQL Server from Spark by using the MSSQL JDBC Connector. In this example, data is read from HDFS in a big data cluster, processed by Spark, and then written to the SQL Server master instance in the cluster.

## MSSQL JDBC Connector

A key pattern in big data involves high volume and velocity and variety data processing in Spark. This is followed by batch/streaming writes to SQL Server to provide access to line-of-business applications. These usage patterns benefit from a connector that utilizes key SQL optimizations and provides an efficient and reliable write to SQL Server big data clusters or a stand-along SQL Server database.

The MSSQL JDBC connector, referenced by the name **com.microsoft.sqlserver.jdbc.spark**, uses [SQL Server Bulk copy APIs](../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#sqlserverbulkcopyoptions) to implement an efficient write to SQL Server. The connector is based on Spark data source APIs and provides a familiar JDBC interface for access.

## Prerequisites

- A [SQL Server big data cluster](deploy-get-started.md).

- [Azure Data Studio](../azure-data-studio/download.md).

## Create the target database

1. Open Azure Data Studio, and [connect to the SQL Server master instance of your big data cluster](connect-to-big-data-cluster.md).

1. Create a new query, and run the following command to create a sample database named **MyTestDatabase**.

   ```sql
   Create DATABASE MyTestDatabase
   GO
   ```

## Load sample data into HDFS

1. Download [AdultCensusIncome.csv](https://amldockerdatasets.azureedge.net/AdultCensusIncome.csv) to your local machine.

1. In Azure Data Studio, right-click on the HDFS folder in your big data cluster, and select **New directory**. Name the directory **spark_data**.

1. Right click on the **spark_data** directory, and select **Upload files**. Upload the **AdultCensusIncome.csv** file.

   ![AdultCensusIncome CSV file](./media/spark-mssql-connector/spark_data.png)

## Run the sample notebook

To demonstrate the use of the Microsoft JDBC connector with this data, you can download a sample notebook, open it in Azure Data Studio, and run each code block. For more information about working with notebooks, see [How to use notebooks in SQL Server 2019 preview](notebooks-guidance.md).media

1. From a PowerShell or bash command line, run the following command to download the **spark_to_sql_mssql_connector.ipynb** sample notebook:

   ```PowerShell
   curl -o spark_to_sql_mssql_connector.ipynb "https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/spark_to_sql_mssql_connector.ipynb"
   ```

1. In Azure Data Studio, open the sample notebook file. Verify that it is connected to your HDFS/Spark Gateway for your big data cluster.

1. Run each code cell in the sample to demonstrate Spark to SQL Server interactions with the MSSQL JDBC connector.

## Next steps

For more information about big data clusters, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md)