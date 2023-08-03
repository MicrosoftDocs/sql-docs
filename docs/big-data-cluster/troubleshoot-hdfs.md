---
title: Troubleshoot HDFS
titleSuffix: SQL Server Big Data Cluster
description: Troubleshoot HDFS in SQL Server Big Data Clusters
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 12/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: troubleshooting
---

# Troubleshoot HDFS

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article contains troubleshooting scenarios for HDFS errors in SQL Server 2019 Big Data Clusters.

## Troubleshoot HDFS heap size

### Symptom

In SQL Server Big Data Clusters: `[Big Data Cluster] - nmnode pods down with Failed to start namenode.java.lang.OutOfMemoryError: Java heap space and WARN util.JvmPauseMonitor: Detected pause in JVM or host machine (eg GC)`

### Cause

HDFS heap size may not be properly configured. The proper settings of the namenode's JVM heap depends on many factors, such as the number of files and blocks, and the load on the HDFS system. For more information on calculating the heap size, see [Configuring namenode heap size](https://docs.cloudera.com/HDPDocuments/HDP2/HDP-2.6.5/bk_command-line-installation/content/configuring-namenode-heap-size.html).

### Resolution

In SQL Server Big Data Clusters, the heap size of HDFS namenode process is controlled by the big data clusters configuration `hdfs-env.HDFS_NAMENODE_OPTS`, the default value is 2 GB as specified in [HDFS configuration properties](reference-config-spark-hadoop.md). This workaround proposes increasing the heap size, which is a global configuration change for the entire big data cluster.
 
The SQL Server Big Data Clusters runtime configuration feature is enabled by default after SQL Server 2019 CU9. To proceed, upgrade your cluster to CU9+, preferably to the latest version available. For more information, see [SQL Server Big Data Clusters Release Notes](release-notes-big-data-cluster.md). 

To increase the heap size of HDFS namenode, follow the [post deployment configuration guide](configure-bdc-postdeployment.md).

The following sample uses `azdata` to increase HDFS namenode heap to 4 GB. Note this operation is only available in CU9 or later.
 
```bash
azdata bdc hdfs settings set --settings hdfs-env.HDFS_NAMENODE_OPTS="-Dhadoop.security.logger=INFO,RFAS -Xmx4g"
```

To confirm the change and monitor the update status:

```bash
# (Optional) View the pending change
azdata bdc settings show --filter-option=pending --include-details --recursive
 
# Apply the pending settings
azdata bdc settings apply
 
# Monitor the configuration update status
azdata bdc status show --all
``` 

## See also

- [azdata bdc (Azure Data CLI)](../azdata/reference/reference-azdata-bdc.md)  
- [Configure Spark HDFS](configure-spark-hdfs.md)

## Next steps

- [Monitor applications with azdata and Grafana Dashboard](app-monitor.md)   
- [Tutorial: Run a sample notebook on a SQL Server 2019 big data cluster](notebooks-tutorial-spark.md)
- [Configure HDFS Tiering](hdfs-tiering.md)
