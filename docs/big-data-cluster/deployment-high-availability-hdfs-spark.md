---
title: Deploy HDFS or Spark with high availability
titleSuffix: Deploy HDFS or Spark with high availability 
description: Learn how to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview) to with high availability.
author: mihaelablendea
ms.author: mihaelab 
ms.reviewer: mikeray
ms.date: 08/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy HDFS name node and shared Spark services in a highly available configuration

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

In addition to deploying SQL Server master instance in a highly available configuration using availability groups, you can deploy other mission critical services in the big data cluster to ensure an increased level of reliability. You can configure **HDFS name node** and the shared Spark services grouped under **SparkHead** with an additional replica. In this case, **Zookeeper** is also deployed in the big data cluster to server as cluster coordinator and metadata store for following services: HDFS name node, Livy and Yarn Resource Manager. Spark History, Job History and Hive metadata service are stateless services, Zookeeper is not involved in ensuring the service health for these components. Deploying multiple replicas for these services results in enhanced scalability, reliability and load balancing of the workloads between the available replicas.

> [!NOTE]
> Following services are deployed as containers in the `sparkhead` pod: Livy, Yarn Resource Manager, Spark History, Job History and Hive metadata service.  
>

# Deploy

If either name node or spark head is configured to be deployed with 2 replicas, then you must also configure the Zookeeper resource with 3 replicas. In a highly available configuration for HDFS name node, the 2 replicas will be hosted by 2 pods: `nmnode-0` and `nmnode-1`. This is an active-passive configuration - only one of the name nodes are active at a time, the other is in stand-by, and it will be active as a result of a failover event. 

You can use either the `aks-dev-test-ha` or the `kubeadm-prod` built-in configuration profiles to start customizing your big data cluster deployment. These profiles include the settings required for resources you can configure additional high availability. For example, below is a section in the `bdc.json` configuration file that is relevant for  deploying HDFS name node, Zookeeper and shared Spark resources (`sparkhead`) with high availability.  

```json
{
  ...
    "nmnode-0": {
        "spec": {
            "replicas": 2
        }
    },
    "sparkhead": {
        "spec": {
            "replicas": 2
        }
    },
    "zookeeper": {
        "spec": {
            "replicas": 3
        }
    },
  ...
}
```

> [!IMPORTANT]
> As a best practice, in a production deployment, you must also configure HDFS block replication to 3. This setting is already specified in the `aks-dev-test-ha` and `kubeadm-prod` profiles. See below section from `bdc.json` configuration file:

```json
{
  ...
  "hdfs": {
      "resources": [
          "nmnode-0",
          "zookeeper",
          "storage-0",
          "sparkhead"
      ],
      "settings": {
          "hdfs-site.dfs.replication": "3"
      }
  },
  ...
}
```

## Known limitations

These are the known issues and limitations with configuring high availability for the Hadoop services in the SQL Server big data clusters:

- All configurations must be specified at the time of the big data cluster deployment. At the time of the SQL Server 2019 CU1 release, you cannot enable the high availability configuration post deployment.

## Next steps

- For more information about using configuration files in big data cluster deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).
- For more information about SQL Server master high availability options in big data clusters, see [Deploy SQL Server master instance with high availability](deployment-high-availability.md) topic.
