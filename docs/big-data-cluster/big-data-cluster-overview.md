---
title: Introducing Big Data Clusters
titleSuffix: SQL Server Big Data Clusters
description: Learn about SQL Server Big Data Clusters that run on Kubernetes and provide scale-out options for both relational and HDFS data.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 09/01/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: overview
ms.custom:
  - contperf-fy22q1
  - intro-overview
---

# Introducing [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

In [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)], [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] allow you to deploy scalable clusters of SQL Server, Spark, and HDFS containers running on Kubernetes. These components are running side by side to enable you to read, write, and process big data from Transact-SQL or Spark, allowing you to easily combine and analyze your high-value relational data with high-volume big data.

## Getting started

 - First, see [Get started with [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)]](deploy-get-started.md)
 - For new features for latest release, see the [release notes](release-notes-big-data-cluster.md)
 - For frequently asked questions, see [Big Data Clusters FAQ](big-data-cluster-faq.yml)

## Big data clusters architecture

The following diagram shows the components of a SQL Server big data cluster:

:::image type="content" source="media/big-data-cluster-overview/architecture-diagram-overview.png" alt-text="Big data clusters architecture overview" lightbox="media/big-data-cluster-overview/architecture-diagram-overview.png":::


### <a id="controlplane"></a> Controller

The controller provides management and security for the cluster. It contains the control service, the configuration store, and other cluster-level services such as Kibana, Grafana, and Elastic Search. 

### <a id="computeplane"></a> Compute pool

The compute pool provides computational resources to the cluster. It contains nodes running SQL Server on Linux pods. The pods in the compute pool are divided into *SQL Compute instances* for specific processing tasks. 

### <a id="dataplane"></a> Data pool

The data pool is used for data persistence. The data pool consists of one or more pods running SQL Server on Linux. It is used to ingest data from SQL queries or Spark jobs. 

### Storage pool

The storage pool consists of storage pool pods comprised of SQL Server on Linux, Spark, and HDFS. All the storage nodes in a SQL Server big data cluster are members of an HDFS cluster.

> [!TIP]
> For an in-depth look into big data cluster architecture and installation, see [Workshop: Microsoft [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/microsoft/sqlworkshops-bdc).

### App pool

Application deployment enables the deployment of applications on a SQL Server Big Data Clusters by providing interfaces to create, manage, and run applications.


## <a id="scenarios"></a> Scenarios and Features

[!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] provide flexibility in how you interact with your big data. You can query external data sources, store big data in HDFS managed by SQL Server, or query data from multiple external data sources through the cluster. You can then use the data for AI, machine learning, and other analysis tasks. 

Use [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] to:

- [Deploy scalable clusters](./deploy-get-started.md) of SQL Server, Spark, and HDFS containers running on Kubernetes. 
- Read, write, and process big data from Transact-SQL or Spark.
- Easily combine and analyze high-value relational data with high-volume big data.
- Query external data sources.
- Store big data in HDFS managed by SQL Server.
- Query data from multiple external data sources through the cluster.
- Use the data for AI, machine learning, and other analysis tasks.
- [Deploy and run applications](./concept-application-deployment.md) in [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)].
- Virtualize data with [PolyBase](../relational-databases/polybase/polybase-guide.md). Query data from external SQL Server, Oracle, Teradata, MongoDB, and generic ODBC data sources with external tables.
- Provide high availability for the SQL Server master instance and all databases by using Always On availability group technology.

The following sections provide more information about these scenarios.

## Data virtualization

By leveraging [PolyBase](../relational-databases/polybase/polybase-guide.md), [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] can query external data sources without moving or copying the data. [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] introduces new connectors to data sources, for more information see [What's new in PolyBase 2019?](../relational-databases/polybase/polybase-faq.yml).

![Data virtualization](media/big-data-cluster-overview/data-virtualization.png)

## Data lake

A SQL Server big data cluster includes a scalable HDFS *storage pool*. This can be used to store big data, potentially ingested from multiple external sources. Once the big data is stored in HDFS in the big data cluster, you can analyze and query the data and combine it with your relational data.

![Data lake](media/big-data-cluster-overview/data-lake.png)

### Integrated AI and Machine Learning

[!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] enable AI and machine learning tasks on the data stored in HDFS storage pools and the data pools. You can use Spark as well as built-in AI tools in SQL Server using R, Python, Scala, or Java.

![AI and ML](media/big-data-cluster-overview/ai-ml-spark.png)

## Management and Monitoring

Management and monitoring are provided through a combination of command-line tools, APIs, portals, and dynamic management views.

You can use [Azure Data Studio](../azure-data-studio/what-is-azure-data-studio.md) to perform a variety of tasks on the big data cluster:

- Built-in snippets for common management tasks.
- Ability to browse HDFS, upload files, preview files, and create directories.
- Ability to create, open, and run Jupyter-compatible notebooks.
- Data virtualization wizard to simplify the creation of external data sources (enabled by the **Data Virtualization Extension**).

## <a id="architecture"></a> Kubernetes concepts

A SQL Server big data cluster is a cluster of Linux containers orchestrated by [Kubernetes](https://kubernetes.io/docs/concepts/).

Kubernetes is an open source container orchestrator, which can scale container deployments according to need. The following table defines some important Kubernetes terminology:

|Term|Description|
|:--|:--|
| **Cluster** | A Kubernetes cluster is a set of machines, known as nodes. One node controls the cluster and is designated the master node; the remaining nodes are worker nodes. The Kubernetes master is responsible for distributing work between the workers, and for monitoring the health of the cluster. |
| **Node** | A node runs containerized applications. It can be either a physical machine or a virtual machine. A Kubernetes cluster can contain a mixture of physical machine and virtual machine nodes. |
| **Pod** | A pod is the atomic deployment unit of Kubernetes. A pod is a logical group of one or more containers-and associated resources-needed to run an application. Each pod runs on a node; a node can run one or more pods. The Kubernetes master automatically assigns pods to nodes in the cluster. |

In [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)], Kubernetes is responsible for the state of the cluster. Kubernetes builds and configures the cluster nodes, assigns pods to nodes, and monitors the health of the cluster.

## Next steps

* For more information about deploying [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)], see [Get started with [!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)]](deploy-get-started.md). 

* Then, get started with [loading data](data-ingestion-restore-database.md) and [running a spark job](spark-submit-job.md).

## Learn more

* [Big Data Clusters Architecture Workshop](https://github.com/microsoft/sqlworkshops-bdc)
* WATCH: [Big Data Clusters in a Nutshell](https://channel9.msdn.com/Shows/Data-Exposed/Big-Data-Clusters-in-a-Nutshell)
* WATCH: [Introduction to Big Data Cluster on SQL Server 2019 | Virtualization, Kubernetes, and Containers](https://www.youtube.com/watch?v=q7mxWcYqBMM)

Learn modules for related technologies:

* [Introduction to Kubernetes](/training/modules/intro-to-kubernetes/)
* [Distributed file systems](/training/modules/cmu-case-study-distributed-file-systems/)
* [Introduction to Python](/training/modules/intro-to-python/)
