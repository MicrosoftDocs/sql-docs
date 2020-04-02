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

For additional information about the architecture, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]?](big-data-cluster-overview.md).

## Deployed pods

The following table lists pods deployed in a Big Data Cluster.

|Name  |Area|
|---------|---------|
|`control-<nnnn>`        |[Control](#control)|
|`controldb-<#>`         |[Control](#control)|
|`controlwd-<nnnn>`      |[Control](#control)|
|`logsdb-<#>`            |[Control](#control)|
|`logsui-<nnnn>`         |[Control](#control)|
|`metricsdb-<#>`         |[Control](#control)|
|`metricsdc-<nnnn>`      |[Control](#control)|
|`metricsui-<nnnn>`      |[Control](#control)|
|`mgmtproxy-<nnnn>`      |[Control](#control)|
|`zookeeper-<#>`         |[Control](#control)|
|`master-<#n>`           |[Master instance](#master-instance)|
|`operator-<nnnn>`       |[Master instance](#master-instance)
|`compute-<#n>-<#m>`     |[Compute pool](#compute-pool)|
|`data-<#>-<#>`          |[Data pool](#data-pool) |
|`storage-<#>-<#>`       |[Storage pool](#storage-pool)|
|`nmnode-<#>-<#>`        |[Storage pool](#storage-pool)|
|`sparkhead-<#>`         |[Storage pool](#storage-pool)|
|`appproxy-<#m>`         |[Application pool](#application-pool)|
|`gateway-<#>`           |[Gateway service](#gateway-service)|

## Control

Control pods provide the control service.

|Pod name |Deployment count| Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|-------|
|`control-#`|1| ReplicaSet |Controller service|` controller`<br><br>- `security-support`<br><br>- `fluentbit`
|`controldb`|1| StatefulSet |Configuration store|-` mssql-server`<br><br>- `fluentbit`
|`controlwd`|1|  ReplicaSet |Upgrade orchestration|- `controlwatchdog`
|`logsdb-#` |1| StatefulSet |[Elasticsearch](https://www.elastic.co/)|- `elasticsearch`
|`logsui`   |1| ReplicaSet |[Kibana](https://www.elastic.co/kibana)|- `kibana`
|`metricsdb-#`|1| StatefulSet |[InfluxDB](https://www.influxdata.com)|- `influxdb`
|`metricsdc`|1 per Kubernetes node. | DaemonSet |Node level metrics|- `telegraf` |
|`metricsui-nnnn`|1| ReplicaSet |[Grafana](https://grafana.com/)|- `grafana` |
|`mgmtproxy-nnnn`|1| ReplicaSet |Management proxy|- `service-proxy`<br><br>- `fluentbit`|
|`zookeeper`|0 or 3 for high availability. | StatefulSet |[ZooKeeper](https://kubernetes.io/docs/tutorials/stateful-application/zookeeper/) high availability|`zookeeper`<br><br>- `fluentbit`

## Master instance

`master-<#n>` is the SQL Server master instance.

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

|Pod name |Deployment count| Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|-------|
|`master-<#n>`|1 or more for high availability.| StatefulSet| SQL Server|-` mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`<br><br>- `mssql-ha-supervisor` <sup>*</sup>|
|`operator`<sup>*</sup>| 0 or 1 for high availability | ReplicaSet | SQL Server operator |`mssql-ha-operator`

<sup>*</sup> Only high availability deployments. The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster. `mssql-ha-supervisor` supports the availability group.

Each `master` pod contains one instance of SQL Server. A high-availability deployment includes 3 pods. Each pod includes a SQL Server instance with databases in a SQL Server Always On Availability Group.

Include additional pods at deployment time, depending on your workload. 

## Compute pool

Compute pool provides a SQL Server instance for computation.

|Pod name |Deployment count| Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|-------|
|`compute-<#n>-<#m>`|1 or more.| StatefulSet |SQL Server|- `mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`.

- `#n` identifies the compute pool.
- `#m` identifies the instance id within the pool.

The compute pool SQL Server instances are stateless. They only require storage for `tempdb`.

Include additional pods at deployment time, depending on your workload. 

## Data pool

The data pool provides SQL Server instances for storage and compute.

|Pod name |Deployment count| Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|-------|
|`data-<#n>-<#m>` | 0 or more | StatefulSet |SQL Server |-` mssql-server` <br><br>- `fluentbit`<br><br>- `collectd`.|

- `#n` identifies the data pool.
- `#m` identifies the instance id within the pool.

Include additional pods at deployment time, depending on workload.

## Storage pool

Storage pool provides data ingestion through Spark, storage in HDFS, data access through HDFS and SQL Server endpoints.

|Pod name |Deployment count| Kubernetes controller type |Service or application| Containers |
|--------|----|------|--------|-------|
|`storage-0-#`|1 or more. Include additional pods at deployment time, depending on workload. | StatefulSet |[HDFS DataNode](concept-storage-pool.md)|- `hadoop`<br><br>- `mssql-server`<br><br>- `fluentbit`<br><br>
|`nmnode-0-#`|1 or more for high availability| StatefulSet |[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |- `hadoop`<br><br>- `fluentbit`
|`sparkehead-#`|1 or more for high availability| StatefulSet |[Spark](configure-spark-hdfs.md)|- `hadoop-yarn-jobhistory`<br><br>- `hadoop-livy-sparkhistory`<br><br>- `hadoop-hivemetastore`<br><br>-- `fluentbit`

## Application pool

The application pool is included in some of the test configuration profiles. The application pool hosts application service proxies that you define when you deploy your applications for Big Data Clusters. 

`appproxy` is a web API that sits in front of the application pool applications. It authenticates users and then routes the requests through to the applications.

|Pod name | Kubernetes controller type |Service or application| Containers  |
|--------|----|------|--------|
|`appproxy`| ReplicaSet |User defined|- `app-service-proxy`<br><br>- `fluentbit`

For more information, see [What is Application Deployment on a Big Data Cluster?](concept-application-deployment.md).

Include additional pods at deployment time, depending on workload. 

## Gateway service

Gateway services provides the Knox gateway to Spark, HDFS, [Yarn](https://hadoop.apache.org/docs/current/hadoop-yarn/hadoop-yarn-site/YARN.html), Yarn UI, and Spark UI.

|Pod name | Kubernetes controller type |Service or application| Containers |
|--------|-----|-----|--------|
|`gateway-<#>`| StatefulSet | Knox gateway |- `knox`<br><br>- `fluentbit`

Only one gateways supported.

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile)
