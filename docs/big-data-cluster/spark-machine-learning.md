---
title: Use Spark Machine Learning
titleSuffix: SQL Server Big Data Clusters
description: Introducing Spark Machine Learning on SQL Server Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: machine-learning-bdc
ms.topic: conceptual
---

# Introducing Spark Machine Learning on SQL Server Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article explains how to effectively use Spark for Machine Learning on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)].

## Spark Machine Learning in SQL Server Big Data Clusters

__SQL Server Big Data Clusters__ enables machine learning scenarios and solutions using different technology stacks: __SQL Server Machine Learning Services__ and __Apache Spark ML__.

To better understand when to use each technology stack, refer to [Machine Learning guide for SQL Server Big Data Clusters](machine-learning-on-bdc.md). This guide covers __Apache Spark ML__.

For big data-based machine learning scenarios, the usage of HDFS for big data hosting and Apache Spark ML capabilities is a more cost-effective, scalable, and powerful approach. Yet this is far from an exhaustive list of the possibilities of what can be achieved with Spark Machine Learning, for a complete list of features see: [Spark MLlib](https://spark.apache.org/mllib/).

The next section provides a curated list of scenarios and references for Spark in SQL Server Big Data Clusters.

## Building blocks for Spark Machine Learning on SQL Server Big Data Clusters

|Learn|Contents  |Link  |
|---------|---------|---------|
|SQL Server Big Data Clusters runtime for Apache Spark|This will show what's included with each release|[SQL Server Big Data Clusters runtime for Apache Spark Guide](runtime-for-apache-spark.md)|
|The Storage Pool|How to store and use HDFS + Spark together to unlock data for machine learning|[Introducing the storage pool in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](concept-storage-pool.md)|
|Use notebook-based experiences and your tools of choice|Connect Spark-Livy endpoint using your tools of choice|[Submit Spark jobs on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Azure Data Studio](spark-submit-job.md)<br/>[Submit Spark jobs on SQL Server big data cluster in Visual Studio Code](spark-hive-tools-vscode.md)<br/>[Use sparklyr in SQL Server big data cluster](sparklyr-from-RStudio.md)<br/>|
|How to install extra packages|In the case a package is not provided out-of-the-box, install it|[Spark library management](spark-install-packages.md)|
|How to troubleshoot|In case it breaks|[Troubleshoot a `pyspark` notebook](troubleshoot-pyspark-notebook.md)<br/>[Debug and Diagnose Spark Applications on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Spark History Server](spark-history-server.md)|
|How to submit machine learning batch jobs|Make ML training and batch scoring run using the command line|[Submit Spark jobs by using command-line tools](spark-submit-job-command-line.md)|
|How to quickly move data between SQL Server and Spark|Make SQL Server source and/or destination for your Spark ML scenarios. Usage of HDFS is not mandatory|[Use the Apache Spark Connector for SQL Server and Azure SQL](spark-mssql-connector.md)|
|Spark model operationalization|After training, operationalize using MLeap|[Create, export, and score Spark machine learning models on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](spark-create-machine-learning-model.md)|
|Data wrangling|Along with Spark's powerful data wrangling capabilities, we ship PROSE|[Data Wrangling using PROSE Code Accelerator](use-prose-for-big-data-automation.md)|


## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
