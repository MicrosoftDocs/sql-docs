---
title: Deployed resources
titleSuffix: SQL Server Big Data Clusters
description: A description of the pods typically deployed in a SQL Server Big Data Cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Resources deployed with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes the resources a SQL Server Big Data Cluster deploys.

A big data cluster deploys pods based on the deployment profile. For details see [Default configurations](deployment-guidance.md#configfile). 

This article describes the pods deployed with `aks-dev-test-ha` profile and includes a Spark pool. Query Kubernetes to see the pods deployed in your cluster. The following example returns a list of pods under a specific namespace.

```bash
kubectl get pods -n <namespace>
```

Replace `<namespace>` with the name of your big data cluster. 

For more information, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).

The following diagram displays the components deployed in a Big Data Cluster:

:::image type="content" source="media/big-data-cluster-overview/architecture-diagram-overview.png" alt-text="big-data-cluster-diagram":::

For information about the architecture, see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).

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
|`dns-<nnnn>`            |[Control](#control)|
|`master-<#n>`           |[Master instance](#master-instance)|
|`operator-<nnnn>`       |[Master instance](#master-instance)
|`compute-<#n>-<#m>`     |[Compute pool](#compute-pool)|
|`data-<#>-<#>`          |[Data pool](#data-pool) |
|`storage-<#>-<#>`       |[Storage pool](#storage-pool)|
|`nmnode-<#>-<#>`        |[Storage pool](#storage-pool)|
|`sparkhead-<#>`         |[Storage pool](#storage-pool)|
|`appproxy-<#m>`         |[Application pool](#application-pool)|
|`gateway-<#>`           |[Gateway service](#gateway-service)|

Not all pods are included in every big data cluster. Deployments with high availability, or active directory integration include specific pods. 

### High availability specific pods:

- `operator-<nnnn>`
- `zookeeper-<#>`

### Active directory specific pods:

- `dns-<nnnn>`

The following sections describe the pods and list the containers in each pod.

## Control

Control pods provide the control service.

|Pod name |Count| Kubernetes controller type | Containers |
|--------|----|------|--------|-------|
|`control-#`|1| ReplicaSet |- `controller`<br><br>- `security-support`<br><br>- `fluentbit`
|`controldb`|1| StatefulSet |- `mssql-server`<br><br>- `fluentbit`
|`controlwd`|1|  ReplicaSet |- `controlwatchdog`
|`logsdb-#` |1| StatefulSet |- `elasticsearch`
|`logsui`   |1| ReplicaSet |- `kibana`
|`metricsdb-#`|1| StatefulSet |- `influxdb`
|`metricsdc`|1 per Kubernetes node. | DaemonSet |- `telegraf` |
|`metricsui-nnnn`|1| ReplicaSet |- `grafana` |
|`mgmtproxy-nnnn`|1| ReplicaSet |- `service-proxy`<br><br>- `fluentbit`|
|`dns-nnnn`|0 or 1 for Active Directory integration| ReplicaSet |- `dns`<br><br>- `fluentbit`|

## Master instance

`master-<#n>` is the SQL Server master instance.

- Manages the data pool via DDL
- Manipulates data in the data pool via DML
- Off-loads analytic query execution to the data pool

|Pod name |Count| Kubernetes controller type | Containers |
|--------|----|------|--------|
|`master-<#n>`|1 or more for high availability.| StatefulSet|- `mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`<br><br>- `mssql-ha-supervisor` <sup>*</sup>|
|`operator`<sup>*</sup>| 0 or 1 for high availability | ReplicaSet |- `mssql-ha-operator`

<sup>*</sup> Only high availability deployments. The operator implements and registers the custom resource definition for SQL Server and the Availability Group resources. When the operator is deployed, it registers itself as a listener for notifications about SQL Server resources being deployed in the Kubernetes cluster. `mssql-ha-supervisor` supports the availability group.

Each `master` pod contains one instance of SQL Server. A high-availability deployment includes 3 pods. Each pod includes a SQL Server instance with databases in a SQL Server Always On Availability Group.

Include additional pods at deployment time, depending on your workload. 

## Compute pool

Compute pool provides a SQL Server instance for computation.

|Pod name |Count| Kubernetes controller type | Containers |
|--------|----|------|--------|
|`compute-<#n>-<#m>`|1 or more.| StatefulSet |- `mssql-server`<br><br>- `fluentbit`<br><br>- `collectd`

- `#n` identifies the compute pool.
- `#m` identifies the instance ID within the pool.

The compute pool SQL Server instances are stateless. They only require storage for `tempdb`.

Include additional pods at deployment time, depending on your workload. 

## Data pool

The data pool provides SQL Server instances for storage and compute.

|Pod name |Count| Kubernetes controller type | Containers |
|--------|----|------|--------|-------|
|`data-<#n>-<#m>` | 0 or more | StatefulSet | -` mssql-server` <br><br>- `fluentbit`<br><br>- `collectd`|

- `#n` identifies the data pool.
- `#m` identifies the instance ID within the pool.

Include additional pods at deployment time, depending on workload.

## Storage pool

Storage pool provides data ingestion through Spark, storage in HDFS, data access through HDFS and SQL Server endpoints.

|Pod name |Count| Kubernetes controller type | Containers |
|--------|----|------|--------|
|`storage-0-#`|1 or more. Include additional pods at deployment time, depending on workload. | StatefulSet |- `hadoop`<br><br>- `mssql-server`<br><br>- `fluentbit`<br><br>
|`nmnode-0-#`|1 or more for high availability| StatefulSet |- `hadoop`<br><br>- `fluentbit`
|`sparkehead-#`|1 or more for high availability| StatefulSet |- `hadoop-yarn-jobhistory`<br><br>- `hadoop-livy-sparkhistory`<br><br>- `hadoop-hivemetastore`<br><br>-- `fluentbit`
|`zookeeper`|0 or 3 for high availability. | StatefulSet |- `zookeeper`<br><br>- `fluentbit`

## Application pool

The application pool is included in some of the test configuration profiles. The application pool hosts application service proxies that you define when you deploy your applications for Big Data Clusters. 

`appproxy` is a web API that sits in front of the application pool applications. It authenticates users and then routes the requests through to the applications.

|Pod name | Kubernetes controller type | Containers  |
|--------|----|------|
|`appproxy`| ReplicaSet |- `app-service-proxy`<br><br>- `fluentbit`

For more information, see [Introducing Application Deployment on a Big Data Cluster](concept-application-deployment.md).

Include additional pods at deployment time, depending on workload. 

## Gateway service

Gateway services provides the Knox gateway to Spark, HDFS, [Yarn](https://hadoop.apache.org/docs/current/hadoop-yarn/hadoop-yarn-site/YARN.html), Yarn UI, and Spark UI.

|Pod name | Kubernetes controller type | Containers |
|--------|-----|-----|
|`gateway-<#>`| StatefulSet |- `knox`<br><br>- `fluentbit`

Only one gateway is supported.

## Open-source container references

For for specific open-source projects and versions, see [Open-source software reference](reference-open-source-software.md).

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/microsoft/sqlworkshops-bdc)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile)
