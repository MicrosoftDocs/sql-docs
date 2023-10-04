---
title: Open-source software reference
titleSuffix: SQL Server Big Data Clusters
description: Identify open-source software and version used in SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Open-source software reference

[!INCLUDE [sqlserver2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

A SQL Server Big Data Cluster includes some containers that are developed by open-source projects. This article identifies specific projects, and versions of these containers.

## Project list

The table below shows the open-source projects in use on [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]. For the exact version used on each cumulative update, see [SQL Server Big Data Clusters platform release notes](release-notes-big-data-cluster.md).

| Project |
|--|
| [collectd](https://collectd.org/) |
| [InfluxDB](https://www.influxdata.com) |
| [Elasticsearch](https://www.elastic.co/) |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) |
| [Grafana](https://grafana.com/) |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |
| [Hive (Metastore)](https://hive.apache.org/) |
| [Kibana](https://www.elastic.co/kibana) |
| [Knox](https://knox.apache.org/) |
| [Livy](https://livy.apache.org/) |
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) |
| [Openresty (Nginx)](https://openresty.org/) |
| [Spark](configure-spark-hdfs.md) |
| [Telegraf](https://docs.influxdata.com/telegraf/) |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) |

## Next steps

Review [Resources deployed with Big Data Cluster](concept-architecture-pods.md).