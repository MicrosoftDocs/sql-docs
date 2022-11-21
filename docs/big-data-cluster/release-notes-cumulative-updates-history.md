---
title: SQL Server Big Data Clusters cumulative updates history
titleSuffix: SQL Server Big Data Clusters
description: This article describes the updates and known issues for SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 09/28/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server 2019 Big Data Clusters cumulative updates history

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. The article lists cumulative update information for all the releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)].

For the latest release notes, see [SQL Server 2019 Big Data Clusters platform release notes](release-notes-big-data-cluster.md).

## <a id="cu18"></a> CU18 (September 2022)

Cumulative Update 18 (CU18) release for SQL Server 2019 Big Data Clusters, [KB 5017593](https://support.microsoft.com/help/5017593).

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4261.1|[2019-CU18-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 18](release-notes-cumulative-update-18.md)   |


## <a id="cu17"></a> CU17 (August 2022)

Cumulative Update 17 (CU17) release for SQL Server 2019 Big Data Clusters, [KB 5016394](https://support.microsoft.com/help/5016394).

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4249.2|[2019-CU17-ubuntu-20.04]| [KB 5016394](https://support.microsoft.com/help/5016394) |

## <a id="cu16"></a> CU16 GDR (June 2022)

Cumulative Update 16 (CU16) GDR release for SQL Server 2019 Big Data Clusters

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4236.7|[2019-CU16-GDR3-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 17](release-notes-cumulative-update-17.md)   |


## <a id="cu16"></a> CU16 (May 2022)

Cumulative Update 16 (CU16) release for SQL Server 2019 Big Data Clusters.

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4223.1|[2019-CU16-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 16](release-notes-cumulative-update-16.md) |

## <a id="cu15"></a> CU15 (January 2022)

Cumulative Update 15 (CU15) release for SQL Server 2019 Big Data Clusters.

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4198.2|[2019-CU15-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 15](release-notes-cumulative-update-15.md) |

> [!WARNING]
   > On Cumulative Update 15, __the upgrade order is critical__. Upgrade your big data cluster to CU15 __before__ upgrading the Kubernetes cluster to version 1.21. If the Kubernetes cluster is upgraded to version 1.21 before BDC is upgraded to CU14 or CU15 then the cluster will end up in error state and the BDC upgrade will not succeed. In this case, reverting back to Kubernetes version 1.20 will fix the problem.

## <a id="cu14"></a> CU14 (November 2021)

Cumulative Update 14 (CU14) release for SQL Server Big Data Clusters.

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4188.2|[2019-CU14-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 14](release-notes-cumulative-update-14.md) |

> [!WARNING]
   > On Cumulative Update 14, __the upgrade order is critical__. Upgrade your big data cluster to CU14 __before__ upgrading Kubernetes cluster to version 1.21. If not done in this order, a CU13 cluster upgrade to CU14 on a Kubernetes version 1.21 won't finish. In this case, reverting back to Kubernetes version 1.20 will fix the problem.

## <a id="cu13"></a> CU13 (September 2021)

Cumulative Update 13 (CU13) release for SQL Server Big Data Clusters.

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4178.15|[2019-CU13-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 13](release-notes-cumulative-update-13.md) |

SQL Server Big Data Clusters CU13 includes important changes and capabilities:

* [HDFS distributed copy capabilities through azdata](distributed-data-copy-hdfs.md)
* [Apache Spark 3.1.2](spark-3-upgrade.md) and the [SQL Server Big Data Clusters runtime for Apache Spark](runtime-for-apache-spark.md)

    > [!CAUTION]
    > Before upgrading make sure to review the [Spark 3 upgrade guide](spark-3-upgrade.md).

* [Password rotation for Big Data Cluster's Auto Generated Active Directory service accounts during the BDC deployment](active-directory-password-rotation.md)
* [New Advanced Encryption Standard (AES) optional parameter `security.activeDirectory.enableAES` for the automatically generated AD accounts](active-directory-deploy.md)

## <a id="cu12"></a> CU12 (August 2021)

Cumulative Update 12 (CU12) release for SQL Server Big Data Clusters.

|Package version | Image tag | Contents |
|-----|-----| ----- |
|15.0.4153.13|[2019-CU12-ubuntu-20.04]| [SQL Server Big Data Clusters Cumulative Update 12](release-notes-cumulative-update-12.md) |

SQL Server Big Data Clusters CU12 changes the operating system default python version from 3.5 to 3.6 on all its images. This has no impact on Spark and SQL Server Machine Learning Services, as those components use dedicated Python installations and don't rely on OS python.

## <a id="cu11"></a> CU11 (June 2021)

Cumulative Update 11 (CU11) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4138.2|[2019-CU11-ubuntu-20.04]|

SQL Server Big Data Clusters CU11 includes important capabilities:

- Encryption at Rest with external key providers via BDC KMS, commonly known as bring your own key (BYOK). For more information, see [Encryption at rest concepts and configuration guide](encryption-at-rest-concepts-and-configuration.md).
- Several SQL Server PolyBase Hadoop fixes and SQL Server PolyBase support of the following data sources: Hortonworks HDP 3.1, Cloudera CDH 6.1, 6.2, 6.3, Azure Blob Storage (WASB[S]) and Azure Data Lake Storage Gen2 (ABFS[S]). For more information, see:
    - [PolyBase Connectivity Configuration (Transact-SQL)](../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md)
    - [Configure PolyBase to access external data in Hadoop](../relational-databases/polybase/polybase-configure-hadoop.md)
    - [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../t-sql/statements/create-external-data-source-transact-sql.md)

## <a id="cu10"></a> CU10 (April 2021)

Cumulative Update 10 (CU10) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4123.1|[2019-CU10-ubuntu-20.04]|

SQL Server Big Data Clusters CU10 includes important capabilities:

- Upgraded base images from Ubuntu 16.04 to Ubuntu 20.04.
   > [!CAUTION]
   > Ubuntu 20.04 has stricter security requirements and you may see issues when using BDC to connect to SQL Server instances before SQL Server 2017. For more information, see [Failed to connect to remote instance of SQL Server 2016 or older](release-notes-big-data-cluster.md#failed-to-connect-to-remote-instance-of-sql-server-2016-or-older).
- High availability support for Hadoop KMS components.
- Additional configuration settings for SQL Server networking and process affinity at the resource-scope. See [Master Pool resource-scope settings](reference-config-bdc-overview.md#master-pool-resource-scope-settings).
- Resource management for Spark-related containers through [cluster-scope settings](reference-config-bdc-overview.md#cluster-scope-settings).

## <a id="cu9"></a> CU9 (February 2021)

Cumulative Update 9 (CU9) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4102.2|[2019-CU9-ubuntu-16.04]|

SQL Server Big Data Clusters CU9 includes important capabilities:

- Support to configure BDC post deployment and provide increased visibility of system settings.

   Clusters using `mssql-conf` for SQL Server master instance configurations require additional steps after upgrading to CU9. Follow the instructions [here](bdc-upgrade-configuration.md).

- Improved [!INCLUDE[azdata](../includes/azure-data-cli-azdata.md)] experience for encryption at rest.
- Ability to dynamically [install Python Spark packages](spark-install-packages.md) using virtual environments.
- Upgraded software versions for most of our OSS components (Grafana, Kibana, FluentBit, etc.) to ensure BDC images are up to date with the latest enhancements and fixes. See [Open-source software reference](reference-open-source-software.md).
- Other miscellaneous improvements and bug fixes.

### OSS component versions

| Project | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.12 |
| [InfluxDB](https://www.influxdata.com) | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.9.1 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.6.3 |
| [Grafana](https://grafana.com/) | 7.3.1 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |3.1.4|
| [Hive (Metastore)](https://hive.apache.org/) |2.3.7<br/>3.0.0 (standalone)<br/>3.1.2 (hive)|
| [Kibana](https://www.elastic.co/kibana) | 7.9.1 |
| [Knox](https://knox.apache.org/) |1.4.0|
| [Livy](https://livy.apache.org/) |0.7.0|
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.10.1.0 |
| [Openresty (Nginx)](https://openresty.org/) | 1.17.8.2 |
| [Spark](configure-spark-hdfs.md) | 2.4.10 |
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.6.2 |

## <a id="cu8-gdr"></a> CU8-GDR(January 2021)

Cumulative Update 8 GDR (CU8-GDR) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4083.2 |[2019-CU8-GDR2-ubuntu-16.04]|

### OSS component versions

The table below shows the open-source projects in use as of [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU8 and prior.

| Project | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.8.1 |
| [InfluxDB](https://www.influxdata.com) | 1.7.6 |
| [Elasticsearch](https://www.elastic.co/) | 7.0.1 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.1.1 |
| [Grafana](https://grafana.com/) | 6.3.6 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) |3.1.3+|
| [Hive (Metastore)](https://hive.apache.org/) |2.3.7|
| [Kibana](https://www.elastic.co/kibana) | 7.0.1 |
| [Knox](https://knox.apache.org/) |1.2.0|
| [Livy](https://livy.apache.org/) |0.6.0|
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.0.0.1 |
| [Openresty (Nginx)](https://openresty.org/) | 1.15.8 |
| [Spark](configure-spark-hdfs.md) |2.4.6+|
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.10.3 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.5.8 |

## <a id="cu8"></a> CU8 (September 2020)

Cumulative Update 8 (CU8) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4073.23 |[2019-CU8-ubuntu-16.04]

This release includes several fixes and a couple of enhancements.

### Added capabilities

- [SQL Server Big Data Clusters encryption at rest](encryption-at-rest-concepts-and-configuration.md) using system-managed keys and certificates.
   > [!CAUTION]
   > This is the initial release of SQL Server BDC encryption at rest. Review the following articles: 
   > - [Security concepts for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](concept-security.md)
   > - [Encryption at rest concepts and configuration Guide](encryption-at-rest-concepts-and-configuration.md)
- [Oracle Proxy User](tutorial-query-oracle.md) support to the Data Virtualization scenario.

## <a id="cu6"></a> CU6 (July 2020)

Cumulative Update 6 (CU6) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4053.23 |[2019-CU6-ubuntu-16.04]

This release includes minor fixes and enhancements. The following articles include information related to these updates:

- [Manage big data cluster access in Active Directory mode](manage-user-access.md)
- [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](active-directory-deploy.md)
- [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on AKS in Active Directory mode](active-directory-deployment-aks.md)
- [Deploy big data clusters with Azure Kubernetes Service (AKS) Private Cluster](private-deploy.md)
- [Restrict egress traffic of big data clusters in Azure Kubernetes Service (AKS) private cluster](private-restrict-egress-traffic.md)
- [Deploy SQL Server Big Data Cluster with high availability](deployment-high-availability.md)
- [Configure a SQL Server Big Data Cluster](./configure-bdc-overview.md)
- [Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md)
- [SQL Server master instance configuration properties](reference-config-master-instance.md)
- [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md)
- [Kubernetes RBAC model & impact on users and service accounts managing BDC](kubernetes-rbac.md)

## <a id="cu5"></a> CU5 (June 2020)

Cumulative Update 5 (CU5) release for SQL Server Big Data Clusters.

|Package version | Image tag |
|-----|-----|
|15.0.4043.16 |[2019-CU5-ubuntu-16.04]

### Added capabilities

- Support for Big Data Clusters deployment on Red Hat OpenShift. Support includes OpenShift container platform deployed on premises version 4.3 and up and Azure Red Hat OpenShift. See [Deploy SQL Server Big Data Clusters on OpenShift](deploy-openshift.md)
- Updated the BDC deployment security model so privileged containers deployed as part of BDC are no longer *required*. In addition to non-privileged, containers are running as non-root user by default for all new deployments using SQL Server Big Data Clusters CU5. 
- Added support for deploying multiple big data clusters against an Active Directory domain.
- [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] has its own semantic version, independent from the server. Any dependency between the client and the server version of azdata is removed. We recommend using the latest version for both client and server to ensure you are benefiting from latest enhancements and fixes.
- Introduced two new stored procedures,  sp_data_source_objects and sp_data_source_table_columns, to support introspection of certain External Data Sources. They can be used by customers directly via T-SQL for schema discovery and to see what tables are available to be virtualized. We leverage these changes in the External Table Wizard of the [Data Virtualization Extension](../azure-data-studio/extensions/data-virtualization-extension.md) for  Azure Data Studio, which allows you to create external tables from SQL Server, Oracle, MongoDB, and Teradata.
- Added support to persist customizations performed in Grafana. Before CU5, customers would notice that any edits in Grafana configurations would be lost upon `metricsui` pod (that hosts Grafana dashboard) restart. This issue is fixed and all configurations are now persisted. 
- Fixed security issue related to the API used to collect pod and node metrics using Telegraf (hosted in the `metricsdc` pods). As a result of this change, Telegraf now requires a service account, cluster role, and cluster bindings to have the necessary permissions to collect the pod and node metrics. See [Custer role required for pods and nodes metrics collection](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) for more details.
- Added two feature switches to control the collection of pod and node metrics. In case you are using different solutions for monitoring your Kubernetes infrastructure, you can turn off the built-in metrics collection for pods and host nodes by setting *allowNodeMetricsCollection* and *allowPodMetricsCollection* to false in control.json deployment configuration file. For OpenShift environments, these settings are set to false by default in the built-in deployment profiles, since collecting pod and node metrics required privileged capabilities.

## <a id="cu4"></a> CU4 (April 2020)

Cumulative Update 4 (CU4) release for SQL Server Big Data Clusters. The SQL Server Database Engine version for this release is 15.0.4033.1.

|Package version | Image tag |
|-----|-----|
|15.0.4033.1 |[2019-CU4-ubuntu-16.04]

## <a id="cu3"></a> CU3 (March 2020)

Cumulative Update 3 (CU3) release for SQL Server Big Data Clusters. The SQL Server Database Engine version for this release is 15.0.4023.6.

|Package version | Image tag |
|-----|-----|
|15.0.4023.6 |[2019-CU3-ubuntu-16.04]

### Resolved issues

SQL Server Big Data Clusters CU3 resolves the following issues from previous releases.

- [Deployment with private repository](release-notes-big-data-cluster.md#deployment-with-private-repository)
- [Upgrade may fail due to timeout](release-notes-big-data-cluster.md#upgrade-may-fail-due-to-timeout)

## <a id="cu2"></a> CU2 (February 2020)

Cumulative Update 2 (CU2) release for SQL Server Big Data Clusters. The SQL Server Database Engine version for this release is 15.0.4013.40.

|Package version | Image tag |
|-----|-----|
|15.0.4013.40 |[2019-CU2-ubuntu-16.04]

## <a id="cu1"></a> CU1 (January 2020)

Cumulative Update 1 (CU1) release for SQL Server Big Data Clusters. The SQL Server Database Engine version for this release is 15.0.4003.23.

|Package version | Image tag |
|-----|-----|
|15.0.4003.23|[2019-CU1-ubuntu-16.04]

## <a id="rtm"></a> GDR1 (November 2019)

SQL Server Big Data Clusters General Distribution Release 1 (GDR1) - introduces general availability for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-nover.md)]. The SQL Server Database Engine version for this release is 15.0.2070.34.

|Package version | Image tag |
|-----|-----|
|15.0.2070.34|[2019-GDR1-ubuntu-16.04]

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]


## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
