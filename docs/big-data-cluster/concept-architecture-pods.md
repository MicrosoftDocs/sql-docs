---
title: Deployed resources
titleSuffix: SQL Server Big Data Clusters
description: A description of the pods typically deployed in a SQL Server Big Data Cluster.
author: mihaelablendea 
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 01/05/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Resources deployed with Big Data Cluster

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the resources a SQL Server Big Data Cluster deploys.

## SQL Server master instance

- StatefulSet: `master`

- Pod: `master-#` 

- Containers in each pod:
  - SQL Server instance
  - Fluent Bit
  - Collectd
  - mssql-ha-supervisor (if Big Data Cluster is deployed for HA)

Each pod contains one instance of SQL Server. If the deployment is configured for high-availability (HA), it includes 3 instances with a SQL Server Always On Availability Group containing 3 replicas.

The SQL Server master instance:

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

## Data pool

- StatefulSet: `data-0`

- Pod: `data-0-#`

- Containers in each pod:
  - SQL Server instance
  - Fluent Bit
  - Collectd

The data pool SQL Server instances provide storage and compute. 

## Compute pool

- StatefulSet: `compute-0`

- Pod: `compute-0-#`

- Containers in each pod:
  - SQL Server instance
  - Fluent Bit
  - Collectd

The compute pool SQL Server instances are stateless. Only require storage for tempdb. 

SQL Server 2019 Big Data Cluster supports one compute pool. Compute pool can't be scaled or resized. You can not add or remove pods.

- Number of pods in compute pool.
  - Default: 1
  - Practical use limit: 8
  - Maximum support: 60

The compute pool provides compute resources for distributed queries.

## HDFS NameNode

- StatefulSet: `nmnode-0`

- Pod: `nmnode-0-#`

- Containers in each pod:
  - HDFS NameNode
  - Fluent Bit

HA deployment includes two NameNode pods.

A ZooKeeper pod support HA.

## Gateway service

- StatefulSet: `gateway`

- Pod: `gateway-0`

- Containers in each pod:
  - Knox
  - Fluent Bit

Only one replica (one container) supported.

## Spark head

- StatefulSet: `sparkhead`

- Pod: `sparkehead-#`

- Containers in each pod:
  - YARN history server
  - Spark history server
  - Hive metastore
  - Fluent Bit

Provides YARN history server, Spark history server for Livy jobs, Hive metastore, MapReduce service (internal use only).

## Storage pool

- StatefulSet: `storage-0`

- Pod: `storage-0-#`

- Containers in each pod:
  - HDFS DataNode
  - SQL Server storage instance
  - Fluent Bit
  - Yarn (for on demand processes)

## ZooKeeper

- StatefulSet: `zookeeper`

- Pod: `zookeeper-0`

- Containers in each pod:
  - ZooKeeper
  - Fluent Bit

Manages failover for HA resources. For background, see [ZooKeeper](https://kubernetes.io/docs/tutorials/stateful-application/zookeeper/) in the Kubernetes documentation.

## Operator

- ReplicaSet: `operator`

- Container: `mssql-ha-operator`

The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster

## MetricsDB

- StatefulSet: `metricsdb`

- Pod: `metricsdb-0`

- Pod: ``

`metricsdb-0`: Runs influxdb


## Metricsdc

- DaemonSet: `metricsdc`

- Pod: `metricsdc-****`

## Metricsui

- ReplicaSet: `metricsui`.

- Pod: `metricsui-****`

Runs Grafana- 

## mgmtproxy

## logsdb

## logsui

## control

## appproxy

## Table summary of pods

The following table lists the pods that are typically deployed in a Big Data Cluster. 

|Name  |Count  |Type  |Description  |
|---------|---------|---------|---------|
|appproxy-\<*nnnn*\>|1         |         |Application proxy. 1 per app.|
|compute-\<*#*\>-\<*#*\>|1         |         |Compute for SQL Server.|
|control-\<*nnnn*\>|1         |         |Kubernetes control.|
|controldb-\<*#*\>|1         |         |         |
|controlwd-\<*nnnn*\>|1         |         |         |
|data-\<*#*\>-\<*#*\>|2         |         |Used by SQL Server|
|gateway-\<*#*\>|1         |         |         |
|logsdb-\<*#*\>|1         |         |         |
|logsui-\<*nnnn*\>|1         |         |         |
|master-\<*#*\>|1-9         |         |Master SQL Server instance. 3 replicas provide HA with a contained availability group.|
|metricsdb-\<*#*\>|1         |         |         |
|metricsdc-\<*nnnn*\>|5         |Daemonset|One per node in the cluster|
|metricsui-\<*nnnn*\>|1         |         |         |
|mgmtproxy-\<*nnnn*\>|1         |         |         |
|nmnode-\<*#*\>-\<*#*\>|2         |         |HDFS node. |
|operator-\<*nnnn*\>|1         |         |         |
|sparkhead-\<*#*\>|2         |         |HDFS spark head. |
|storage-\<*#*\>-\<*#*\>|3         |         |Storage.|
|zookeeper-\<*#*\>|3         |         |HDFS|

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
