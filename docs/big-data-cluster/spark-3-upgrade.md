---
title: Spark 3 upgrade guide
titleSuffix: SQL Server Big Data Clusters
description: Spark 3 upgrade guide
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server Big Data Clusters Spark 3 upgrade guide

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article contains important information and guidance for migrating Apache Spark 2.4 workloads to Spark version 3.1. This is required in order to upgrade from [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU12 to CU13, and above.

## Introduction of Apache Spark 3 on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]

Up to cumulative update 12 (CU12), big data clusters relied on the Apache Spark 2.4 line, which reached its [end of life in May 2021](https://spark.apache.org/versioning-policy.html). Consistent with our commitment to continuous improvements of the Big Data and Machine Learning capabilities brought by the Apache Spark engine, CU13 brings in the current release of Apache Spark, version 3.1.2.

### A new performance baseline

This new version of Apache Spark brings performance benefits over big data processing workloads. Using the reference __TCP-DS 10TB workload__ in our tests we were able to reduce runtime from 4.19 hours to 2.96 hours, a __29.36% improvement__ achieved just by switching engines using the same hardware and configuration profile on [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)], no additional application optimizations. The improvement mean of individual query runtime is 36%.

:::image type="content" source="./media/spark-3-upgrade/spark-2-vs-3-tcpds-10tb.png" alt-text="Submit menu by clicking dashboard" lightbox="./media/spark-3-upgrade/spark-2-vs-3-tcpds-10tb.png":::

### Upgrade guidance

Spark 3 is a major release and __contains breaking changes__. Following the same established best practice in the SQL Server universe, it's recommended:

1. Review this article entirely.
1. Review the official [Apache Spark 3 Migration Guide](https://spark.apache.org/docs/3.1.2/core-migration-guide.html).
1. Perform a side-by-side deployment of a new big data cluster version CU13 with your current environment.
1. (Optional) Leverage the new [azdata HDFS distributed copy](distributed-data-copy-hdfs.md) capability to have a subset of your data needed for validation.
1. Validate your current workload with Spark 3 before upgrading.
1. Reassess enforced Spark optimizations in your code and table definition strategies. Spark 3 brings new shuffling, partitioning, and Adaptive Query Execution enhancements. This is a great opportunity to reevaluate previous decisions and try leverage the newer engine out-of-the-box features.

## What happens during cluster upgrade?

The cluster upgrade process will deploy Spark pods with the new version and the refreshed [runtime for Apache Spark](runtime-for-apache-spark.md). After the upgrade, there will be no Spark 2.4 components any longer.

Persistent configuration changes made through the configuration framework will be preserved.

User libraries and artifacts loaded directly into HDFS will be preserved. Yet, make sure that those libraries and artifacts are compatible with Spark 3.

> [!WARNING]
> Customizations made directly to the pods will be lost, make sure you validate and re-apply those if still applicable to Spark 3.

## Breaking changes

Spark 3 is not fully backward compatible with 2.4, the breaking changes are mainly caused by three parts:

* Scala 2.12 used by Spark 3 is incompatible with Scala 2.11 used by Spark 2.4
* Spark 3 API changes and deprecations
* SQL Server Big Data Clusters runtime for Apache Spark library updates

## Scala 2.12 used by Spark 3 is incompatible with Scala 2.11

If running Spark jobs based on Scala 2.11 jars, it is required to rebuild it using Scala 2.12. Scala 2.11 and 2.12 are mostly source compatible, but not binary compatible. For more information, see [Scala 2.12.0](https://www.scala-lang.org/news/2.12.0/).

Below changes are needed:

1. Change Scala version for all Scala dependencies.
1. Change Spark version for all Spark dependencies.
1. Change all Spark dependencies have provided scope except external dependencies such as ```spark-sql-kafka-0-10```.
 
Here is a sample pom.xml as below:

```xml
  <properties>
    <spark.version>3.1.2</spark.version>
    <scala.version.major>2.12</scala.version.major>
    <scala.version.minor>10</scala.version.minor>
    <scala.version>${scala.version.major}.${scala.version.minor}</scala.version>
  </properties>
 
  <dependencies>
 
    <dependency>
      <groupId>org.scala-lang</groupId>
      <artifactId>scala-library</artifactId>
      <version>${scala.version}</version>
      <scope>provided</scope>
    </dependency>
 
    <dependency>
      <groupId>org.apache.spark</groupId>
      <artifactId>spark-core_${scala.version.major}</artifactId>
      <version>${spark.version}</version>
     <scope>provided</scope>
    </dependency>
    <dependency>
      <groupId>org.apache.spark</groupId>
      <artifactId>spark-sql_${scala.version.major}</artifactId>
      <version>${spark.version}</version>
      <scope>provided</scope>
    </dependency>
 
    <dependency>
      <groupId>org.apache.spark</groupId>
      <artifactId>spark-sql-kafka-0-10_${scala.version.major}</artifactId>
      <version>${spark.version}</version>
    </dependency>
    
  </dependencies>
```

## Spark 3 API changes and deprecations

Review the official [Apache Spark 3 Migration Guide](https://spark.apache.org/docs/3.1.2/core-migration-guide.html), which covers all API changes in detail.

Some captured highlights are:

|Breaking change  |Action  |
|---------|---------|
|```spark-submit``` parameters ```yarn-client``` and ```yarn-clustermodes``` are removed in Spark 3|Use ```spark-submit --master yarn --deploy-mode client``` or ```--deploy-mode cluster``` instead.<br/>Details refer https://spark.apache.org/docs/latest/running-on-yarn.html |
|```HiveContext``` class is removed|Use ```SparkSession.builder.enableHiveSupport()``` instead|
|The order of argument is reversed in the TRIM method|Use ```TRIM(str, trimStr)``` instead of ```TRIM(trimStr, str)```|
|Due to the upgrade to Scala 2.12, ```DataStreamWriter.foreachBatch``` is not a compatible source with Scala program|Update your Scala source code to distinguish between Scala function and Java lambda.|

## SQL Server Big Data Clusters runtime for Apache Spark library updates

As covered by the [SQL Server Big Data Clusters runtime for Apache Spark](runtime-for-apache-spark.md) specification, all default Python, R, and Scala libraries were updated on the CU13 release. Also, many libraries were added to provide a better out-of-the-box experience.

1. Make sure that your workload works with the newer library set. 
1. Review if a custom loaded library is now part of the default package baseline, and adjust your jobs specifications to remove the custom library to allow the job to use the shipped library.

## Frequently Asked Questions

### How to solve strange java.lang.NoSuchMethodError or java.lang.ClassNotFoundException

This error is most likely caused by Spark or Scala version conflict. Double-check the below and rebuild your project.

1. Make sure all Scala versions are updated.
1. Make sure all Spark dependencies are updated with correct scala version and spark version.
1. Make sure all Spark dependencies have provided scope except spark-sql-kafka-0-10.

### SparkUpgradeException due to calendar mode change

There are changes in Spark 3.0 calendar model. You may see exception like this when writing calendar column in Spark SQL:

```txt
Caused by: org.apache.spark.SparkUpgradeException: 
You may get a different result due to the upgrading of Spark 3.0:
writing dates before 1582-10-15 or timestamps before 1900-01-01T00:00:00Z into Parquet INT96 files can be dangerous,
as the files may be read by Spark 2.x or legacy versions of Hive later, 
which uses a legacy hybrid calendar that is different from Spark 3.0+'s Proleptic Gregorian calendar. 
See more details in SPARK-31404.
You can set spark.sql.legacy.parquet.int96RebaseModeInWrite to 'LEGACY' to 
rebase the datetime values w.r.t. the calendar difference during writing, to get maximum interoperability. 
Or set spark.sql.legacy.parquet.int96RebaseModeInWrite to 'CORRECTED' to 
write the datetime values as it is, if you are 100% sure that the written files 
will only be read by Spark 3.0+ or other systems that use Proleptic Gregorian calendar.
```

__Solution:__ Set configuration spark.sql.legacy.parquet.int96RebaseModeInWrite to LEGACY or CORRECTED, as explained above. Below is a possible solution in PySpark code:

```python
spark.conf.set("spark.sql.legacy.parquet.int96RebaseModeInWrite","CORRECTED")
```

## Next steps

For more information, see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
