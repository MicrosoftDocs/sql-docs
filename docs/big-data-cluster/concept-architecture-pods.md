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
|[master-\<*#*\>](sql-server-master-instance)|1-9         |         |Master SQL Server instance. 3 replicas provide HA with a contained availability group.|
|metricsdb-\<*#*\>|1         |         |         |
|metricsdc-\<*nnnn*\>|5         |Daemonset|One per node in the cluster|
|metricsui-\<*nnnn*\>|1         |         |         |
|mgmtproxy-\<*nnnn*\>|1         |         |         |
|nmnode-\<*#*\>-\<*#*\>|2         |         |HDFS node. |
|operator-\<*nnnn*\>|1         |         |         |
|sparkhead-\<*#*\>|2         |         |HDFS spark head. |
|storage-\<*#*\>-\<*#*\>|3         |         |Storage.|
|zookeeper-\<*#*\>|3         |         |HDFS|

## SQL Server master instance

|Pod name | Controller type | Containers in a pod|
|--------|----------|--------|
|`master-`| StatefulSet|- Containers in each pod:<br><br>  - SQL Server instance<br><br>  - Fluent Bit<br><br>  - Collectd<br><br>  - mssql-ha-supervisor (if Big Data Cluster is deployed for HA)|

- Controlled by: StatefulSet/`master`

- Pod: `master-#` 

Each pod contains one instance of SQL Server. If the deployment is configured for high-availability (HA), it includes 3 instances with a SQL Server Always On Availability Group containing 3 replicas.

The SQL Server master instance:

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

## Data pool

- Controlled by: StatefulSet/`data-0`

- Pod: `data-0-#`

- Containers in each pod:
  - SQL Server instance
  - Fluent Bit
  - Collectd

The data pool SQL Server instances provide storage and compute. 

## Compute pool

- Controlled by: StatefulSet/`compute-0`

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

- Controlled by: StatefulSet/`nmnode-0`

- Pod: `nmnode-0-#`

- Containers in each pod:
  - HDFS NameNode
  - Fluent Bit

HA deployment includes two NameNode pods.

A ZooKeeper pod support HA.

## Gateway service

Gateway access to HDFS and Spark

- Controlled by: StatefulSet/`gateway`

- Pod: `gateway-0`

- Containers in each pod:
  - Knox
  - Fluent Bit

Only one replica (one container) supported.

## Spark head

- Controlled by: StatefulSet/`sparkhead`

- Pod: `sparkehead-#`

- Containers in each pod:
  - YARN history server
  - Spark history server
  - Hive metastore
  - Fluent Bit

Provides YARN history server, Spark history server for Livy jobs, Hive metastore, MapReduce service (internal use only).

## Storage pool

- Controlled by: StatefulSet/`storage-0`

- Pod: `storage-0-#`

- Containers in each pod:
  - HDFS DataNode
  - SQL Server storage instance
  - Fluent Bit
  - Yarn (for on demand processes)

## ZooKeeper

- Controlled by: StatefulSet/`zookeeper`

- Pod: `zookeeper-0`

- Containers in each pod:
  - ZooKeeper
  - Fluent Bit

Manages failover for HA resources. For background, see [ZooKeeper](https://kubernetes.io/docs/tutorials/stateful-application/zookeeper/) in the Kubernetes documentation.

## Operator

- Controlled by: ReplicaSet/`operator`

- Container: `mssql-ha-operator`

The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster

## MetricsDB

- Controlled by: StatefulSet/`metricsdb`

- Pod: `metricsdb-0`

`metricsdb-0`: Runs influxdb


## Metricsdc

- Controlled by: DaemonSet/`metricsdc`

- Pod: `metricsdc-****`

## Metricsui

Dashboard for searching through cluster metrics.

- Controlled by: ReplicaSet/`metricsui`.

- Pod: `metricsui-****`

## mgmtproxy

Proxy for accessing services which monitor cluster health.

- Controlled by: ReplicaSet/`mgmtproxy`

- Pod: `mgmt--proxy-****`

## logsdb

- Controlled by: StatefulSet/logsdb

## logsui

## control

## appproxy



## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile)
