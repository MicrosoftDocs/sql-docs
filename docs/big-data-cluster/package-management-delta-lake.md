---
title: SQL Server Big Data Clusters Delta Lake
titleSuffix: SQL Server Big Data Clusters
description: This guide covers how to use Delta Lake on SQL Server Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/06/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: guide
ms.metadata: seo-lt-2019
---

# Delta Lake on SQL Server Big Data Clusters

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

In this guide, you'll learn:

> [!div class="checklist"]
> * The requisites and capabilities of Delta Lake on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)].
> * How to load Delta Lake libraries on CU12 clusters to use with Spark 2.4 sessions and jobs.

## Introduction

Linux Foundation Delta Lake is an open-source storage layer that brings ACID (atomicity, consistency, isolation, and durability) transactions to Apache Spark and big data workloads. To learn more about Delta Lake, see:

* [What is Delta Lake?](/azure/synapse-analytics/spark/apache-spark-what-is-delta-lake)
* [Introduction to Delta Lake](https://docs.delta.io/1.0.0/delta-intro.html)

## Delta Lake on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU13 and above (Spark 3)

Delta Lake is installed and configured by default on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU13 and above. No further action is required.

This article covers configuration of Delta Lake on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU12 and below.

## Configure Delta Lake on SQL Server Big Data Clusters CU12 and below (Spark 2.4)

On [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU12 or below, it is possible to load Delta Lake libraries using the [Spark library management](spark-install-packages.md) feature.

> [!NOTE]
> As a general rule, use the most recent compatible library. The code in this guide was tested by using Delta Lake 0.6.1 on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU12. Delta Lake 0.6.1 is compatible with Apache Spark 2.4.x, later versions are not. The examples are provided as-is, not a supportability statement.

### Configure Delta Lake library and Spark configuration options

Set up your Delta Lake libraries with your application before you submit the jobs. The following library is required:

* [delta-core](https://mvnrepository.com/artifact/io.delta/delta-core) - This core library enables Delta Lake support.

The library must target Scala 2.11 and Spark 2.4.7. This [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] requirement is for SQL Server 2019 Cumulative Update 9 (CU9) or later.

It's also required to configure Spark to enable Delta Lake-specific Spark SQL commands and the metastore integration. The example below is how an Azure Data Studio notebook would configure Delta Lake support:

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

If multiple applications will use the Delta Lake library, copy the appropriate library JAR files to a shared location on HDFS. Then all jobs should reference the same library files.

Copy the libraries to the common location:

```bash
azdata bdc hdfs cp --from-path delta-core_2.11-0.6.1.jar --to-path "hdfs:/apps/jars/delta-core_2.11-0.6.1.jar"
```

#### Dynamically install the libraries

You can dynamically install packages when you submit a job by using the [package management features](spark-install-packages.md) of Big Data Clusters. There's a job startup time penalty because of the recurrent downloads of the library files on each job submission.

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

* [Linux Foundation Delta Lake](/azure/synapse-analytics/spark/apache-spark-delta-lake-overview)
* [Delta Lake quickstart](https://docs.delta.io/0.6.1/quick-start.html)

To submit Spark jobs to [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] by using `azdata` or Livy endpoints, see [Submit Spark jobs by using command-line tools](spark-submit-job-command-line.md).

For more information about [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] and related scenarios, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
