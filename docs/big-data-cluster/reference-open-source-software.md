---
title: Open-source software reference
titleSuffix: SQL Server Big Data Clusters
description: Identify open-source software and version used in SQL Server Big Data Clusters.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 02/11/2021
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Open-source software reference

[!INCLUDE [sqlserver2019](../includes/applies-to-version/sqlserver2019.md)]

A SQL Server Big Data Cluster includes some containers that are developed by open-source projects. This article identifies specific projects, and versions of these containers.

## Project list

The table below shows the open-source projects in use as of [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU8 and prior, and CU9 and later. 

| Project | CU8 and prior | Beginning with CU9 |
|--|--|--|
| [collectd](https://collectd.org/) | 5.8.1 | 5.12 |
| [InfluxDB](https://www.influxdata.com) | 1.7.6 | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.0.1 | 7.9.1 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.1.1 | 1.6.3 |
| [Grafana](https://grafana.com/) | 6.3.6 | 7.3.1 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |3.1.3+|3.3.0|
| [Hive (Metastore)](https://hive.apache.org/) |2.3.7|2.3.7<br/>3.0.0 (standalone)<br/>3.1.2 (hive)|
| [Kibana](https://www.elastic.co/kibana) | 7.0.1 | 7.9.1 |
| [Knox](https://knox.apache.org/) |1.2.0|1.4.0|
| [Livy](https://livy.apache.org/) |0.6.0|0.7.0|
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.0.0.1 | 1.10.1.0 |
| [Openresty (Nginx)](https://openresty.org/) | 1.15.8 | 1.17.8.2 |
| [Spark](configure-spark-hdfs.md) |2.4.6+|2.4.10|
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.10.3 | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) |3.5.8|3.6.2

## Next steps

Review [Resources deployed with Big Data Cluster](concept-architecture-pods.md).