---
title: SQL Server Big Data Clusters Configuration Pre CU9
titleSuffix: SQL Server Big Data Clusters
description: Big Data Clusters Configuration Pre CU9
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rahul.ajmera
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Configure a SQL Server Big Data Cluster - Pre CU9 release

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

In SQL Server 2019 Big Data Clusters releases CU8 and earlier, you can configure big data cluster settings at deployment time through the deployment `bdc.json` file. The SQL Server master instance can be configured post-deployment only using mssql-conf.

> [!NOTE]
> Prior to the CU9 release and support for configuration-enabled clusters, Big Data Clusters could be configured at deployment time only, with exception to the SQL Server master instance - which could be configured post-deployment only using mssql-conf. For instructions to configure a CU9 and later release, see [Configure a SQL Server Big Data Cluster](configure-bdc-overview.md).

## Configuration Scopes

Big Data Clusters configuration pre-CU9 has two scoping levels: `service`, and `resource`. The hierarchy of the settings follows in this order as well, from highest to lowest. BDC components will take the value of the setting defined at the lowest scope. If the setting is not defined at a given scope, it will inherit the value from its higher parent scope.

For example, you may want to define the default number of cores the Spark driver will use in the storage pool and `Sparkhead` resources. You can do this in two ways:

* Specify a default cores value at the `Spark` service scope 
* Specify a default cores value at the `storage-0` and `sparkhead` resource scope

In the first scenario, all lower-scoped resources of the Spark service (storage pool and `Sparkhead`) will *inherit* the default number of cores from the Spark service default value.

In the second scenario, each resource will use the value defined at its respective scope.

If the default number of cores is configured at both service and resource scope, then the resource-scoped value will override the service-scoped value since this is the lowest **user configured** scope for the given setting.

For specific information about configuration, see the appropriate articles:

## Configure the SQL Server master instance
Configure master instance of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)].

Server configuration settings cannot be configured for SQL Server master instance at deployment time. This article describes a temporary workaround on how to configure settings like SQL Server edition, enable or disable SQL Server Agent, enable specific trace flags or enable/disable customer feedback.

To change any of these settings, follow these steps:

1. Create a custom `mssql-custom.conf` file that includes targeted settings. The following example enables SQL Agent, telemetry, sets a PID for Enterprise Edition, and enables trace flag 1204.:

   ```
   [sqlagent]
   enabled=true
   
   [telemetry]
   customerfeedback=true
   userRequestedLocalAuditDirectory = /tmp/audit

   [DEFAULT]
   pid = Enterprise

   [traceflag]
   traceflag0 = 1204
   ```

1. Copy `mssql-custom.conf` file to `/var/opt/mssql` in the `mssql-server` container in the `master-0` pod. Replace `<namespaceName>` with the big data cluster name.

   ```bash
   kubectl cp mssql-custom.conf master-0:/var/opt/mssql/mssql-custom.conf -c mssql-server -n <namespaceName>
   ```

1. Restart SQL Server instance.  Replace `<namespaceName>` with the big data cluster name.

   ```bash
   kubectl exec -it master-0  -c mssql-server -n <namespaceName> -- /bin/bash
   supervisorctl restart mssql-server
   exit
   ```

> [!IMPORTANT]
> If SQL Server master instance is in an availability groups configuration, copy the `mssql-custom.conf` file in all the `master` pods. Note that each restart will cause a failover, so you must make sure you are timing this activity during downtime periods.

### Known limitations

- The steps above require Kubernetes cluster admin permissions
- You cannot change the server collation for SQL Server master instance of the big data cluster post deployment.

## Configure Apache Spark and Apache Hadoop
In order to configure Apache Spark and Apache Hadoop in Big Data Clusters, you need to modify the cluster profile at deployment time.

A Big Data Cluster has four configuration categories: 

- `sql` 
- `hdfs` 
- `spark` 
- `gateway` 

`sql`, `hdfs`, `spark`, `sql` are services. Each service maps to the same named configuration category. All gateway configurations go to category `gateway`. 

For example, all configurations in service `hdfs` belong to category `hdfs`. Note that all Hadoop (core-site), HDFS and Zookeeper configurations belong to category `hdfs`; all Livy, Spark, Yarn, Hive, Metastore configurations belong to category `spark`. 

[Supported configurations](reference-config-spark-hadoop.md) lists Apache Spark & Hadoop properties that you can configure when you deploy a SQL Server Big Data Cluster.

The following sections list properties that you **can't** modify in a cluster:

- [Unsupported `spark` configurations](reference-config-spark-hadoop.md#unsupported-spark-configurations)
- [Unsupported `hdfs` configurations](reference-config-spark-hadoop.md#unsupported-hdfs-configurations)
- [Unsupported `gateway` configurations](reference-config-spark-hadoop.md#unsupported-gateway-configurations)

## Next steps

[Configure a SQL Server Big Data Cluster](configure-bdc-overview.md)