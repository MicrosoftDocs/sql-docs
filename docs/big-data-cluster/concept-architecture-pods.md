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

A Big Data Cluster cluster deploys pods based on the deployment profile. For details see [Default configurations](deployment-guidance.md#configfile). 

This article describes the pods deployed with `aks-dev-test-ha` profile and includes a Spark pool. Query Kubernetes to see the pods deployed in your cluster. The following example returns a list of pods under a specific namespace.

```bash
kubectl get pods -n <namespace>
```

Replace `<namespace>` with the Kubernetes namespace of your big data cluster. 

For more information, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).

The following diagram displays the components deployed in a Big Data Cluster:

:::image type="content" source="media/big-data-cluster-overview/architecture-diagram-overview.png" alt-text="big-data-cluster-diagram":::

## Table summary of pods

The following table lists the pods that are typically deployed in a Big Data Cluster. 

|Name  |Count  |Description  |
|---------|---------|---------|
|[`appproxy-<#m>`](#application-pool)        |1 <sup>*</sup> |Application proxy|
|[`compute-<#n>-<#m>`](#compute-pool)|1 <sup>*</sup> |SQL Server|
|[`control-<nnnn>`](#control)        |1              |Control service|
|[`controldb-<#>`](#control)         |1              |Configuration store|
|[`controlwd-<nnnn>`](#control)      |1              |Control watchdog|
|[`data-<#>-<#>`](#data-pool)        |2 <sup>*</sup> |SQL Server|
|[`gateway-<#>`](#gateway-service)   |1              |Knox|
|[`logsdb-<#>`](#control)             |1              |Elastic search|
|[`logsui-<nnnn>`](#control)          |1              |Kibana|
|[`master-<#n>`](#master-instance)   |1-9 <sup>*</sup>|Master SQL Server instance.|
|[`metricsdb-<#>`](#control)       |1              |InfluxDB|
|[`metricsdc-<nnnn>`](#control)    |5              |Telegraf|
|[`metricsui-<nnnn>`](#control)    |1              |Grafana|
|[`mgmtproxy-<nnnn>`](#control)    |1              |Service proxy|
|[`nmnode-<#>-<#>`](#storage-pool)  |2              |HDFS node. |
|[`operator-<nnnn>`](#master-instance)      |1              |High availability|
|[`sparkhead-<#>`](#storage-pool)      |2              |HDFS spark head|
|[`storage-<#>-<#>`](#storage-pool)  |3 <sup>*</sup> |Manage storage.|
|[`zookeeper-<#>`](#control)       |3              |High availability|

<sup>*</sup> Adjust the number of instances at deployment time according to the size of the workload.

## Compute pool

Compute pool provides a SQL Server instance for computation.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|-----|-----|--------|
|`compute-<#n>-<#m>`| StatefulSet |SQL Server|- `mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`.

- `#n` identifies the compute pool.
- `#m` identifies the instance id within the pool.

The compute pool SQL Server instances are stateless. They only require storage for `tempdb`.

## Storage pool

Storage pool provides data ingestion through Spark, storage in HDFS, data access through HDFS and SQL Server endpoints.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|----------|----------|--------|
|`storage-0-#`| StatefulSet |[HDFS DataNode](concept-storage-pool.md)|- `hadoop`<br><br>- `mssql-server`<br><br>- `fluentbit`<br><br>
|`nmnode-0-#`| StatefulSet |[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |- `hadoop`<br><br>- `fluentbit`
|`sparkehead-#`| StatefulSet |[Spark](configure-spark-hdfs.md)|- `hadoop-yarn-jobhistory`<br><br>- `hadoop-livy-sparkhistory`<br><br>- `hadoop-hivemetastore`<br><br>-- `fluentbit`

## Data pool

The data pool provides SQL Server instances for storage and compute.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|
|`data-<#n>-<#m>` | StatefulSet |SQL Server |-` mssql-server` <br><br>- `fluentbit`<br><br>- `collectd`.|

- `#n` identifies the data pool.
- `#m` identifies the instance id within the pool.

## Master instance

`master-<#n>` is the SQL Server master instance.

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|----------|----------|--------|
|`master-<#n>`| StatefulSet| SQL Server|-` mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`<br><br>- `mssql-ha-supervisor` <sup>*</sup>|
|`operator`<sup>*</sup>| ReplicaSet | SQL Server operator |`mssql-ha-operator`

<sup>*</sup> Only high availability deployments. The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster. `mssql-ha-supervisor` supports availability groups.

Each `master` pod contains one instance of SQL Server. A high-availability deployment includes 3 pods. Each pod includes a SQL Server instance with databases in a SQL Server Always On Availability Group.

## Control

Control pods provide the control service.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|----------|-------|--------|
|`control-#`| ReplicaSet |Controller service|` controller`<br><br>- `security-support`<br><br>- `fluentbit`
|`controldb`| StatefulSet |Configuration store|-` mssql-server`<br><br>- `fluentbit`
|`controlwd`| ReplicaSet |Upgrade orchestration|- `controlwatchdog`
|`logsdb-#`| StatefulSet |[Elasticsearch](https://www.elastic.co/)|- `elasticsearch`
|`logsui`| ReplicaSet |[Kibana](https://www.elastic.co/kibana)|- `kibana`
|`metricsdb-#`| StatefulSet |[InfluxDB](https://www.influxdata.com)|- `influxdb`
|`metricsdc`| DaemonSet |Node level metrics|- `telegraf` |
|`metricsui-nnnn`| ReplicaSet |[Grafana](https://grafana.com/)|- `grafana` |
|`mgmtproxy-nnnn`| ReplicaSet |Management proxy|- `service-proxy`<br><br>- `fluentbit`|
|`zookeeper`| StatefulSet |[ZooKeeper](https://kubernetes.io/docs/tutorials/stateful-application/zookeeper/) high availability|- HDFS DataNode<br><br>- `zookeeper`<br><br>- `fluentbit`

## Gateway service

Gateway services provides the Knox gateway to Spark, HDFS, [Yarn](https://hadoop.apache.org/docs/current/hadoop-yarn/hadoop-yarn-site/YARN.html), Yarn UI, and Spark UI.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|-----|-----|--------|
|`gateway-<#>`| StatefulSet | Knox gateway |- `knox`<br><br>- `fluentbit`

Only one replica (one container) supported.

## Application pool

`appproxy` is a web API that sits in front of the application pool applications. It authenticates users and then routes the requests through to the applications.

|Pod name | Kubernetes controller type |Service or application| Containers  |
|--------|----|------|--------|
|`appproxy`| ReplicaSet |User defined|- `app-service-proxy`<br><br>- `fluentbit`

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile)
