---
title: SQL Server Big Data Clusters Delta Lake
titleSuffix: SQL Server Big Data Clusters
description: This guide covers how to configure Delta Lake using  the Package Management feature set.
author: DaniBunny 
ms.author: dacoelho
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 06/11/2021
ms.topic: guide
ms.prod: sql
ms.technology: big-data-cluster
---

# Configuring Delta Lake on SQL Server Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

In this guide, you'll learn:

> [!div class="checklist"]
> * The requisites and capabilities of Delta Lake on SQL Server Big Data Clusters.
> * How to load Delta Lake libraries to use with Spark sessions and jobs.

## Introduction

Delta Lake is an open-source storage layer that brings ACID (atomicity, consistency, isolation, and durability) transactions to Apache Spark and big data workloads. To learn more about Delta Lake see:

* [What is Delta Lake](https://docs.microsoft.com/azure/synapse-analytics/spark/apache-spark-what-is-delta-lake)
* [Introduction to Delta Lake](https://docs.delta.io/0.6.1/delta-intro.html)

## Configure Delta Lake on SQL Server Big Data Clusters

On SQL Server Big Data Clusters 2019 it is possible to load Delta Lake libraries using the [Spark library management](spark-install-packages.md) feature.

> [!NOTE]
   > As a general rule, use the most recent compatible library. The code in this guide was tested by using Delta Lake 0.6.1 on SQL Server Big Data Clusters CU11. Delta Lake 0.6.1 is compatible with Apache Spark 2.4.x, later versions are not. The examples are provided as-is, not a supportability statement.

### Configure Delta Lake library and Spark configuration options

Set up your Delta Lake libraries with your application before you submit the jobs. The following library is required:

* [delta-core](https://mvnrepository.com/artifact/io.delta/delta-core) - This core library enables Delta Lake support.

The library must target Scala 2.11 and Spark 2.4.7. This SQL Server Big Data Cluster requirement is for Cumulative Update package 9 (CU9) or later.

Its also required to configure Spark to enable Delta Lake specific Spark SQL commands and the metastore integration. The example bellow is how a Azure Data Studio notebook would configure Delta Lake support:

```python
%%configure -f \
{
    "conf": {
        "spark.jars.packages": "io.delta:delta-core_2.11:0.6.1",
        "spark.sql.extensions":"io.delta.sql.DeltaSparkSessionExtension",
        "spark.sql.catalog.spark_catalog":"org.apache.spark.sql.delta.catalog.DeltaCatalog"
    }
}
```

#### Share library locations for jobs on HDFS

If multiple applications will use the Delta Lake library copy the appropriate library JAR files to a shared location on HDFS. Then all jobs should reference the same library files.

Copy the libraries to the common location:

```bash
azdata bdc hdfs cp --from-path delta-core_2.11-0.6.1.jar --to-path "hdfs:/apps/jars/delta-core_2.11-0.6.1.jar"
```

#### Dynamically install the libraries

You can dynamically install packages when you submit a job by using the [package management features](spark-install-packages.md) of SQL Server Big Data Clusters. There's a job startup time penalty because of the recurrent downloads of the library files on each job submission.

### Submit the Spark job by using azdata

The following example uses the shared library JAR files on HDFS:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/my-delta-lake-python-job.py \
-j '["/apps/jars/delta-core_2.11-0.6.1.jar"]' \
--config '{"spark.sql.extensions":"io.delta.sql.DeltaSparkSessionExtension","spark.sql.catalog.spark_catalog":"org.apache.spark.sql.delta.catalog.DeltaCatalog"}' \
-n MyETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

This example uses dynamic package management to install the dependencies:

```bash
azdata bdc spark batch create -f hdfs:/apps/ETL-Pipelines/my-delta-lake-python-job.py \
--config '{"spark.jars.packages":"io.delta:delta-core_2.11:0.6.1","spark.sql.extensions":"io.delta.sql.DeltaSparkSessionExtension","spark.sql.catalog.spark_catalog":"org.apache.spark.sql.delta.catalog.DeltaCatalog"' \
-n MyETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m
```

## Next steps

To learn how to effectively use Delta Lake, see the following articles.

* [Linux Foundation Delta Lake](https://docs.microsoft.com/azure/synapse-analytics/spark/apache-spark-delta-lake-overviewp)
* [Delta Lake quickstart](https://docs.delta.io/0.6.1/quick-start.html)

To submit Spark jobs to SQL Server Big Data Clusters by using `azdata` or Livy endpoints, see [Submit Spark jobs by using command-line tools](spark-submit-job-command-line.md).

For more information about SQL Server Big Data Clusters and related scenarios, see [[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
