---
title: SQL Server Big Data Clusters CU12 release notes
titleSuffix: SQL Server Big Data Clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters. 
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: melqin,dacoelho
ms.date: 08/04/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Big Data Clusters CU12 release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] cumulative update 12 (CU12).

## Tested configurations for CU12

SQL Server Big Data Clusters CU12 was tested on the following environment combinations:

| Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| ------------ | ------- | ------- | ------------ | ----------- |
| Ubuntu 20.04.2 LTS | 1.20.7 | containerd 1.4.3 | Block only | Block only |
| Ubuntu 20.04.2 LTS | 1.20.7 | docker 19.3.14; 20.10.7 | Block only | Block only |
| Ubuntu 18.04.5 LTS | 1.20.7 | containerd 1.4.4 | Block only | Block only |
| RHEL CoreOS 46.82 | 1.19.0 | CRI-O 1.19.1-11 | Block only | Block only |

Reference Architecture White Papers for SQL Server Big Data Clusters can be found on the following pages:

* https://www.microsoft.com/sql-server/sql-server-2019
* [SQL Server Big Data Clusters partners](../sql-server/partner-big-data-cluster.md)

## System environment

* __Operating System__: Ubuntu 20.04.2 LTS
* __SQL Server__: 15.0.4153.1
    * __Java__: Azul Zulu JRE 11.0.9.1
    * __Python__: 3.7.2 (miniconda 4.5.12)
    * __R__: Microsoft R 3.5.2
* __Spark__: 2.4
    * __Java__: Azul Zulu JRE 1.8.0_275
    * __Scala__: 2.11
    * __Python__: 3.7.2 (miniconda 4.5.12)
    * __R__: Microsoft R 3.5.2

## Embedded OSS component versions

| Component | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.12 |
| [InfluxDB](https://www.influxdata.com) | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.9.1 |
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.10.1.0 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.6.3 |
| [Grafana](https://grafana.com/) | 7.3.6 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) | 3.1 |
| [Hive (Metastore)](https://hive.apache.org/) | 2.3 |
| [Kibana](https://www.elastic.co/kibana) | 7.9.1 |
| [Knox](https://knox.apache.org/) | 1.4.0 |
| [Livy](https://livy.apache.org/) | 0.6 |
| [Openresty (Nginx)](https://openresty.org/) | 1.17.8-2 |
| [Spark](configure-spark-hdfs.md) | 2.4 |
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.5.8 |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
