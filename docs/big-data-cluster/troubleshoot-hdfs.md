---
title: Troubleshoot HDFS 
titleSuffix: SQL Server Big Data Cluster
description: Troubleshoot HDFS in SQL Server Big Data Clusters
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dacoelho
ms.date: 12/10/2021
ms.topic: troubleshooting
ms.prod: sql
ms.technology: big-data-cluster
---

# Troubleshoot HDFS 

## Symptom

In SQL Server Big Data Clusters CU9 or prior: `[Big Data Cluster] - nmnode pods down with Failed to start namenode.java.lang.OutOfMemoryError: Java heap space and WARN util.JvmPauseMonitor: Detected pause in JVM or host machine (eg GC)`

## Cause

HDFS heap size may not be properly configured. The proper settings of the namenode's JVM heap depends on many factors, such as the number of files and blocks, and the load on the HDFS system. For more information on calculating the heap size, see [Configuring namenode heap size](https://docs.cloudera.com/HDPDocuments/HDP2/HDP-2.6.5/bk_command-line-installation/content/configuring-namenode-heap-size.html).

## Resolution

In SQL Server Big Data Clusters, the heap size of HDFS namenode process is controlled by BDC configuration hdfs-env.HDFS_NAMENODE_OPTS, the default value is 2 GB as specified in [HDFS configuration properties](reference-config-spark-hadoop.md).
 
The SQL Server Big Data Clusters runtime configuration feature is enabled after CU9. To proceed, upgrade your cluster to CU9+ (preferably to the latest version available). To increase the heap size of HDFS namenode in CU9+, we can follow the [post deployment configuration guide](configure-bdc-postdeployment.md).
 
```bash
# Assuming we want to increase HDFS namenode heap to 4GB:
azdata bdc hdfs settings set --settings hdfs-env.HDFS_NAMENODE_OPTS="-Dhadoop.security.logger=INFO,RFAS -Xmx4g"

# (Optional) View the pending change
azdata bdc settings show --filter-option=pending --include-details --recursive
 
# Apply the pending settings
azdata bdc settings apply
 
# Monitor the configuration update status
azdata bdc status show --all
``` 


