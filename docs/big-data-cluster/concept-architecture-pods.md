---
title: Deployed resources
titleSuffix: SQL Server Big Data Clusters
description: A description of the pods typically deployed in a SQL Server Big Data Cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 03/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Resources deployed with Big Data Cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the resources a SQL Server Big Data Cluster deploys.

A Big Data Cluster cluster deploys pods based on the deployment configuration. This article describes the pods deployed with `aks-dev-test-ha` profile and includes a Spark pool.

For more information, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).

The following diagram displays the components deployed in a Big Data Cluster:

:::image type="content" source="media/big-data-cluster-overview/architecture-diagram-overview.png" alt-text="big-data-cluster-diagram":::

## Table summary of pods

The following table lists the pods that are typically deployed in a Big Data Cluster. 

|Name  |Count  |Description  |
|---------|---------|---------|
|[appproxy-\<*#m*\>](#appproxy)|1         |Application proxy|
|[compute-\<*#*\>-\<*#*\>](#compute-pool)|1         |SQL Server|
|[control-\<*nnnn*\>](#control)|1         |Control service|
|[controldb-\<*#*\>](#control)|1         |Configuration store|
|[controlwd-\<*nnnn*\>](#control)|1         |Control watchdog|
|[data-\<*#*\>-\<*#*\>](#data-pool)|2         |SQL Server|
|[gateway-\<*#*\>](#gateway-service)|1         |Knox|
|[logsdb-\<*#*\>](#logsdb)|1         |Elastic search|
|[logsui-\<*nnnn*\>](#logsui)|1         |Kibana|
|[master-\<*#*\>](#master-instance)|1-9         |Master SQL Server instance.|
|[metricsdb-\<*#*\>](#metricsdb)|1         |InfluxDB|
|[metricsdc-\<*nnnn*\>](#metricsdc)|5         |Telegraf|
|[metricsui-\<*nnnn*\>](#metricsui)|1         |Grafana|
|[mgmtproxy-\<*nnnn*\>](#mgmtproxy)|1         |Service proxy|
|[nmnode-\<*#*\>-\<*#*\>](#hdfs-namenode)|2            |HDFS node. |
|[operator-\<*nnnn*\>](#operator)|1         |High availability|
|[sparkhead-\<*#*\>](#spark-head)|2        |HDFS spark head|
|[storage-\<*#*\>-\<*#*\>](#storage-pool)|3         |Manage storage.|
|[zookeeper-\<*#*\>](#zookeeper)|3          |High availability|

## appproxy

`appproxy` is a web API that sits in front of the application pool applications. It authenticates users and then routes the requests through to the applications.

|Pod name | Controller type |  Containers in a pod  |
|--------|----------|--------|
|`appproxy`| ReplicaSet |- `app-service-proxy`<br><br>- `fluentbit`

## Compute pool

Compute pool provides a SQL Server instance for computation.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`compute-\<*#n*\>-\<*#m*\>`| StatefulSet |- SQL Server instance<br><br>- `fluentbit`<br><br>- CollectD.

- `#n` identifies the compute pool.
- `#m` identifies the instance id within the pool.

The compute pool SQL Server instances are stateless. They only require storage for `tempdb`.

SQL Server 2019 Big Data Cluster supports one compute pool. Compute pool can't be scaled or resized. You can not add or remove pods. SQL Server 2019 provides one SQL Server instance for the compute pool.

## Control

Control pods provide the control service.

|Pod name | Controller type | Containers in a pod |
|--------|----------|--------|
|`control-#`| ReplicaSet |- controller<br><br>- security-support<br><br>- `fluentbit`
|`controldb`| StatefulSet |- mssql-server<br><br>- `fluentbit`
|`controlwd`| ReplicaSet |ControlWatchDog

- `#` identifies the control pod. 

`control-#` is the controller service.

`controldb` is the configuration store. 

`controlwd` orchestrates upgrades. 

## Data pool

The data pool provides SQL Server instances for storage and compute.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`data-\<*#n*\>-\<*#m*\>` | StatefulSet |SQL Server instance<br><br>`fluentbit`<br><br>CollectD.|

- `#n` identifies the data pool.
- `#m` identifies the instance id within the pool.

## Gateway service

Gateway services provides the Knox gateway to Spark, HDFS, [Yarn](https://hadoop.apache.org/docs/current/hadoop-yarn/hadoop-yarn-site/YARN.html), Yarn UI, and Spark UI.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`gateway-\<*#*\>`| StatefulSet |- Knox<br><br>- `fluentbit`

- `#` identifies the gateway.

Only one replica (one container) supported.

## HDFS NameNode

[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) keeps the directory tree of all files.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`nmnode-0-#`| StatefulSet |- HDFS NameNode<br><br>- `fluentbit`

HA deployment includes two NameNode pods.

## logsdb

`locgsdb` provides [Elasticsearch](https://www.elastic.co/).

|Pod name | Controller type | Containers in a pod |
|--------|----------|--------|
|`logsdb-0`| StatefulSet |- Elasticsearch<br><br>- logsdb

## logsui

`logsui` provides [Kibana](https://www.elastic.co/kibana)

|Pod name | Controller type | Containers in a pod |
|--------|----------|--------|
|`logsui`| ReplicaSet |- Elasticsearch<br><br>- Kibana

## Master instance

`master-\<*#n*\>` is the SQL Server master instance.

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`master-\<*#n*\>`| StatefulSet|- SQL Server instance<br><br>- `fluentbit`<br><br>- Collectd<br><br>- mssql-ha-supervisor (if Big Data Cluster is deployed for HA)|

- `#n` identifies the instance number.

Each pod contains one instance of SQL Server. If the deployment is configured for high-availability (HA), it includes 3 pods. Each pod includes a SQL Server instance with databases in a SQL Server Always On Availability Group.

## MetricsDB

`metricsb` runs [InfluxDB](https://www.influxdata.com).

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`metricsdb-0`| StatefulSet | InfluxDB

## Metricsdc

`metricsdc` collects node level metrics.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`metricsdc`| DaemonSet | Telegraf |

## Metricsui

`metrics` ui provides a dashboard for searching through cluster metrics.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`metricsui-****`| ReplicaSet |- Grafana |

## mgmtproxy

Mgmtproxy is a proxy for accessing services which monitor cluster health. Enables access to Kibana and Grafana.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`mgmtproxy-****`| ReplicaSet |- Service proxy<br><br>- `fluentbit`|

## Operator

The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`operator`| ReplicaSet |`mssql-ha-operator`

## Spark head

Spark head provides YARN history server, Spark history server for Livy jobs, Hive metastore, MapReduce service (internal use only).

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`sparkehead-#`| StatefulSet |- YARN history server<br><br>- Spark history server<br><br>- Hive metastore<br><br>- `fluentbit`

[Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md)

## Storage pool

Storage pool provides data ingestion through Spark, storage in HDFS, data access through HDFS and SQL Server endpoints.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`storage-0-#`| StatefulSet |- HDFS DataNode<br><br>- SQL Server storage instance<br><br>- `fluentbit`<br><br>- Yarn (for on demand processes)

[What is the storage pool ([!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)])?](concept-storage-pool.md)

## ZooKeeper

Zookeeper manages failover for HA resources. For background, see [ZooKeeper](https://kubernetes.io/docs/tutorials/stateful-application/zookeeper/) in the Kubernetes documentation.

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`zookeeper`| StatefulSet |- HDFS DataNode<br><br>- ZooKeeper<br><br>- `fluentbit`

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile)
