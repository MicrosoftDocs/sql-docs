---
title: Apache Spark and Apache Hadoop
titleSuffix: Configure Apache Spark and Apache Hadoop in Big Data Clusters
description: SQL Server Big Data Clusters allow Spark and HDFS solutions. Learn how to configure them.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mikeray
ms.date: 08/04/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Configure Apache Spark and Apache Hadoop in Big Data Clusters

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

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


## Configurations via cluster profile

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

The following is an example of the patch file for the profile for setting HDFS block size: 

   ```json
   { 
         "op": "add", 
         "path": "spec.services.hdfs.settings", 
         "value": { 
           "hdfs-site.dfs.block.size": "268435456" 
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
          "hdfs-site.dfs.block.size": "268435456" 
        } 
      } 
    } 
  } 
} 
```
 
> [!NOTE]
> Resource level configurations override service level configurations. One resource can be assigned to multiple services.

## Enable Spark in the Storage Pool
In addition to the supported Apache configurations, we also offer the ability to configure whether or not Spark jobs can run in the Storage pool. This boolean value, `includeSpark`, is in the `bdc.json` configuration file at `spec.resources.storage-0.spec.settings.spark`.

An example storage pool definition in bdc.json may look like this:
```json
...
"storage-0": {
                "metadata": {
                    "kind": "Pool",
                    "name": "default"
                },
                "spec": {
                    "type": "Storage",
                    "replicas": 2,
                    "settings": {
                        "spark": {
                            "includeSpark": "true"
                        }
                    }
                }
            }
```


## Limitations

Configurations can only be specified at category level. To specify multiple configurations with the same sub-category, we cannot extract the common prefix in cluster profile. 

```json
{ 
      "op": "add", 
      "path": "spec.services.hdfs.settings.core-site.hadoop", 
      "value": { 
        "proxyuser.xyz.users": "*", 
        "proxyuser.abc.users": "*" 
     } 
} 
```

## Next steps

- [Apache Spark & Apache Hadoop (HDFS) configuration properties.](reference-config-spark-hadoop.md)
- [[!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] reference](../azdata/reference/reference-azdata.md)
- [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
