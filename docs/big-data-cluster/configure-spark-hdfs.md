---
title: Apache Spark and Apache Hadoop
titleSuffix: Configure Apache Spark and Apache Hadoop in Big Data Clusters
description: SQL Server Big Data Clusters allow Spark and HDFS solutions. Learn how to configure them.
author: rajmera3
ms.author: raajmera
ms.reviewer: mikeray
ms.date: 02/21/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure Apache Spark and Apache Hadoop in Big Data Clusters

In order to configure Apache Spark and Apache Hadoop in Big Data Clusters, you need to modify the cluster profile at deployment time.

A Big Data Cluster has four configuration categories: 

- `sql` 
- `hdfs` 
- `spark` 
- `gateway` 

`sql`, `hdfs`, `spark`, `sql` are services. Each service maps to the same named configuration category. All gateway configurations go to category `gateway`. 

For example, all configurations in service `hdfs` belong to category `hdfs`. Note that all Hadoop (core-site), HDFS and Zookeeper configurations belong to category `hdfs`; all Livy, Spark, Yarn, Hive, Metastore configurations belong to category `spark`. 

[Supported configurations](reference-config-spark-hadoop.md#supported-configurations) lists properties that you can configure when you deploy a SQL Server big data cluster.

The following sections list properties that you can't modify in a cluster:

- [Unsupported `spark` configurations](reference-config-spark-hadoop.md#unsupported-spark-configurations)
- [Unsupported `hdfs` configurations](reference-config-spark-hadoop.md#unsupported-hdfs-configurations)
- [Unsupported `gateway` configurations](reference-config-spark-hadoop.md#unsupported-gateway-configurations)

You can find all possible configurations for each at the associated Apache documentation site:

- Apache Spark: https://spark.apache.org/docs/latest/configuration.html
- Apache Hadoop:
  - HDFS HDFS-Site: https://hadoop.apache.org/docs/r2.7.1/hadoop-project-dist/hadoop-hdfs/hdfs-default.xml
  - HDFS Core-Site: https://hadoop.apache.org/docs/r2.8.0/hadoop-project-dist/hadoop-common/core-default.xml  
  - Yarn: https://hadoop.apache.org/docs/r3.1.1/hadoop-yarn/hadoop-yarn-site/ResourceModel.html
- Hive: https://cwiki.apache.org/confluence/display/Hive/Configuration+Properties#ConfigurationProperties-MetaStore
- Livy: https://github.com/cloudera/livy/blob/master/conf/livy.conf.template
- Apache Knox Gateway: https://knox.apache.org/books/knox-0-14-0/user-guide.html#Gateway+Details

In addition to these configurations, we also offer the ability to configure whether or not Spark jobs can run in the Storage pool. 

This boolean value, `includeSpark`, is in the `bdc.json` configuration file at `spec.resources.storage-0.spec.settings.spark`.

## Configurations via cluster Profile

In the cluster profile there are resources and services. At deployment time, we can specify configurations in one of two ways: 

* First, at the resource level: 

   The following examples are the patch files for the profile: 

   ```json
   { 
         "op": "add", 
         "path": "spec.resources.zookeeper.spec.settings", 
         "value": { 
           "hdfs": { 
             "zoo-cfg.syncLimit": "6" 
           } 
         } 
   }
   ```

   Or: 

   ```json
   { 
         "op": "add", 
         "path": "spec.resources.gateway.spec.settings", 
         "value": { 
           "gateway": { 
             "gateway-site.gateway.httpclient.socketTimeout": "95s" 
           } 
         } 
   } 
   ```

* Second, at the service level. Assign multiple resources to a service, and specify configurations to the service.

   The following is an example of the patch file for the profile: 

   ```json
   { 
         "op": "add", 
         "path": "spec.services.hdfs.settings", 
         "value": { 
           “core-site.hadoop.proxyuser.xyz.users”: “*” 
           … 
        } 
   } 
   ```

The service `hdfs` is defined as:

```json
{ 
  "spec": { 
   "services": { 
     "hdfs": { 
        "resources": [ 
          "nmnode-0", 
          "zookeeper", 
          "storage-0", 
          "sparkhead" 
        ], 
        "settings":{ 
          "hdfs-site.dfs.replication": "3" 
        } 
      } 
    } 
  } 
} 
```
 
> [!NOTE]
> Resource level configurations override service level configurations. One resource can be assigned to multiple services.

## Limitations

Configurations can only be specified at category level. To specify multiple configurations with the same sub-category, we cannot extract the common prefix in cluster profile. 

```json
{ 
      "op": "add", 
      "path": "spec.services.hdfs.settings.core-site.hadoop", 
      "value": { 
        “proxyuser.xyz.users”: “*”, 
        “proxyuser.abc.users”: “*” 
     } 
} 
```

## Next steps

- [Apache Spark & Apache Hadoop (HDFS) configuration properties.](reference-config-spark-hadoop.md)
- [`azdata` reference](reference-azdata.md)
- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
