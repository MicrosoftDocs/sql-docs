---
title: SQL Server Big Data Clusters CU12 release notes
titleSuffix: SQL Server Big Data Clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters. 
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: melqin,dacoelho
ms.date: 08/02/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Big Data Clusters CU12 release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] cumulative update 12 (CU12).

## Tested configurations

SQL Server Big Data Clusters CU12 was tested on the following environment combinations:

| Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| ------------ | ------- | ------- | ------------ | ----------- |
| Ubuntu 20.04.2 | 1.20.7 | containerd 1.4.3 | Block only | Block only |
| Ubuntu 20.04.2 | 1.20.7 | docker 19.3.14; 20.10.7 | Block only | Block only |
| Ubuntu 18.04.5 | 1.20.7 | containerd 1.4.4 | Block only | Block only |
| RHEL CoreOS 46.82 | 1.19.0 | CRI-O 1.19.1-11 | Block only | Block only |

Reference Architecture White Papers for SQL Server Big Data Clusters can be found on the following pages:

* https://www.microsoft.com/sql-server/sql-server-2019
* [SQL Server Big Data Clusters partners](../sql-server/partner-big-data-cluster.md)

## System environment

* __Operating System__: Ubuntu 20.04.2
* __SQL Server__: 15.0.4153.13
    * __Java__: Azul
    * __Python__: 3.x
    * __R__: 3.x
* __Spark__: 2.4.7
    * __Java__: Azul
    * __Scala__: Microsoft R 3.4.2
    * __Python__: 3.8
    * __R__: Microsoft R 3.4.2
* __App Pool__
    * __Java__: Azul
    * __Python__: 3.8
    * __R__: Microsoft R 3.4.2
    * __MLeap__: Microsoft R 3.4.2

## Embedded OSS component versions

| Component | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.12 |
| [InfluxDB](https://www.influxdata.com) | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.9.1 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.6.3 |
| [Grafana](https://grafana.com/) | 7.3.1 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) | 3.1.4 |
| [Hive (Metastore)](https://hive.apache.org/) | 2.3.7<br/>3.0.0 (standalone)<br/>3.1.2 (hive) |
| [Kibana](https://www.elastic.co/kibana) | 7.9.1 |
| [Knox](https://knox.apache.org/) | 1.4.0 |
| [Livy](https://livy.apache.org/) | 0.7.0 |
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.10.1.0 |
| [Openresty (Nginx)](https://openresty.org/) | 1.17.8.2 |
| [Spark](configure-spark-hdfs.md) | 2.4.10 |
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.6.2 |

## Installed Java and Scala libraries

### SQL Server Machine Learning Services

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

### Spark

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

## Installed Python Libraries

### SQL Server Machine Learning Services

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

### Spark

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

## Installed Python libraries

### SQL Server Machine Learning Services

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

### Spark

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|--|--|--|--|--|--|

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
