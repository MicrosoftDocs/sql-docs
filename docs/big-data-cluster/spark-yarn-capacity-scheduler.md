---
title: Spark workload management using the YARN capacity scheduler
description: This article provides guidelines for Spark workload management using YARN on SQL Server Big Data Clusters
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 11/02/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Spark workload management using the YARN capacity scheduler

[!INCLUDE [sqlserver2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Introduction

This article describes how to segment your Spark workload using YARN's Capacity Scheduler of SQL Server Big Data Clusters. The capacity scheduler allows the configuration of multiple queues with different resource usage profiles to best fit your workload requirements. It provides a hierarchy queue design, capacity can be assigned as min and max percentages of the parent in the hierarchy. 

The YARN Capacity Scheduler can manage resource management scenarios of any complexity. For more information, see [Apache Hadoop 3.1.4 – Hadoop: Capacity Scheduler](https://hadoop.apache.org/docs/r3.1.4/hadoop-yarn/hadoop-yarn-site/CapacityScheduler.html) and [YARN - The Capacity Scheduler](https://blog.cloudera.com/yarn-capacity-scheduler/).

SQL Server Big Data Clusters contains a single pre-configured YARN queue for all job types. The `default` queue is configured to consume all cluster resources (CPU and memory) for all jobs submitted in a first come first serve method, including:

- Spark Jobs and interactive sessions (such as Notebooks)
- HDFS copy and distributed copy commands
- SQL Server Compute Pool access to the Storage Pool distributed file system (HDFS)

Depending on Spark job parameters specified (such as executors, cores, and memory), a big data cluster may be able to serve one to multiple concurrent Spark jobs out of the box.

To achieve more granular resource management on SQL Server Big Data Clusters, use the YARN Capacity Scheduler. YARN's Capacity Scheduler is a highly configurable feature, allowing queues, sub-queues, preemption, priorities, etc. This samples in this article will show how to implement a common Spark resource management scenario using YARN capacity scheduler and the [configuration framework](configure-bdc-postdeployment.md). More advanced scenarios may well leverage the building blocks from this article.

## Sample Spark resource management scenario

In this end-to-end resource map example, the following configuration is applied:

1. Creates a queue called `largebatch` for long running ETL and data transformation. Configured with 70% capacity and 90% max capacity. Jobs submitted to this queue won't be preempted. Note that it is still possible to achieve concurrency in this queue if the jobs are configured not to take all queue capacity. Accessible only by the admin user.
2. Creates a queue called `smallbatch` for select data transformation jobs. Configured with 25% capacity and 50% max capacity. Accessible by the admin user and `dataengineers` group.
3. Creates a queue called `powerusers` for notebook-based experimentation for data engineers and data scientists. Configured from 5% capacity to 50% max capacity. Accessible by the admin user, and the `dataengineers` and `datascientists` groups.-
4. Setup access control lists (ACLs) for these three queues so that only authorized users and groups can access a given queue, and setup automatic user/group queue mapping. 

> [!NOTE]
> On SQL Server Big Data Clusters, Access Control Lists (ACLs) isolation is only possible on Active Directory enabled clusters. On clusters with basic authentication, all applications run using a single user context. Yet it is still possible to apply the techniques in this article to achieve resource segmentation.

The following samples will apply settings on an existing big data cluster using the configuration framework. The configuration may also be performed at deployment time with a custom configuration profile.

## Configure YARN queues

Use ```azdata``` to login to the cluster with administrative privileges.

The following commands apply the queue and user mapping configuration described in the last section:

```bash
azdata bdc spark settings set --settings 
"capacity-scheduler.yarn.scheduler.capacity.root.queues"="largebatch,smallbatch,powerusers",
"capacity-scheduler.yarn.scheduler.capacity.root.largebatch.capacity"="70",
"capacity-scheduler.yarn.scheduler.capacity.root.largebatch.maximum-capacity"="90",
"capacity-scheduler.yarn.scheduler.capacity.root.largebatch.disable_preemption"="true",
"capacity-scheduler.yarn.scheduler.capacity.root.smallbatch.capacity"="25",
"capacity-scheduler.yarn.scheduler.capacity.root.smallbatch.maximum-capacity"="50",
"capacity-scheduler.yarn.scheduler.capacity.root.powerusers.capacity"="5",
"capacity-scheduler.yarn.scheduler.capacity.root.powerusers.maximum-capacity"="50",
"capacity-scheduler.yarn.scheduler.capacity.root.largebatch.acl_submmit_applications"="admin",
"capacity-scheduler.yarn.scheduler.capacity.root.smallbatch.acl_submmit_applications"="admin dataengineers",
"capacity-scheduler.yarn.scheduler.capacity.root.powerusers.acl_submmit_applications"="admin dataengineers,datascientists",
"capacity-scheduler.yarn.scheduler.capacity.queue-mappings"="u:admin:largebatch,u:admin:smallbatch,u:admin:powerusers,g:dataengineers:smallbatch,g:dataengineers:powerusers,g:datascientists:powerusers"
```

> [!NOTE]
> An ACL is of the form "user1,user2 space group1,group2". The special value of * implies anyone.

Apply the new configuration using the following commands. Pods will restart.

```bash
azdata bdc settings show --filter-option=pending --include-details --recursive
azdata bdc settings apply 
```

Use the YARN UI page to validate and monitor queue usage.

It's also possible to monitor YARN queue placement for jobs and sessions using the monitoring patterns described in [Submit Spark jobs by using command-line tools](spark-submit-job-command-line.md).

## Submit Spark jobs on YARN queues

Use the ```-–queue-name``` or ```-q``` options on ```azdata``` to assign the jobs to a specific queue. If queue is not specified and ```capacity-scheduler.yarn.scheduler.capacity.queue-mappings``` is not configured, the ```spark.yarn.queue``` parameter in ```spark-defaults.conf``` will be applied. It is possible to change the default queue for all sessions on [spark-defaults.conf using the configuration framework](configure-bdc-postdeployment.md).

The following example runs a PySpark python file on the `smallbatch` queue:

```bash
azdata bdc spark batch create -q smallbatch \
-f hdfs:/apps/ETL-Pipelines/parquet_etl_sample.py \
-n MyETLPipelinePySpark --executor-count 2 --executor-cores 2 --executor-memory 1664m 
```

## Notebooks using YARN queues

If ACLs and user mappings were configured, the Notebook session will automatically be assigned to the correct queue. The example below is how an Azure Data Studio notebook would explicitly configure the `smallbatch` queue in the Livy session:

```python
%%configure -f \
{
    "conf": {
        "spark.yarn.queue": "smallbatch",
        … other settings …
    }
}
```

## Advanced resource management using YARN

In SQL Server Big Data Clusters, a YARN node manager process runs on each storage/spark pool Kubernetes pod. Different Spark driver applications and the distributed Spark executors run as YARN container processes inside those pods. Different drivers and executor processes can potentially run in the same pod. This means that user applications with different resource and security requirements will share resources if using the same YARN queue; in a SQL Server big data cluster this is the default behavior.

A common pattern to address this scenario is to partition cluster resources using YARN queues and node labels as described below. This way it is possible to assign users and groups to queues that relate to distinct workers to achieve proper resource and security segmentation that may be required by the organization.

In order to implement complete worker segmentation, make use of [YARN node labels](https://hadoop.apache.org/docs/r3.1.4/hadoop-yarn/hadoop-yarn-site/NodeLabel.html) to isolate YARN queues to different Kubernetes nodes. Configure the YARN resource manager in [centralized mode](https://hadoop.apache.org/docs/r3.1.4/hadoop-yarn/hadoop-yarn-site/NodeLabel.html#Features) to manage Kubernetes node labels. Then, create YARN queues that are associated through node labeling.

## Next steps

For more resources on Spark performance and configuration for SQL Server Big Data Clusters, see:

- [How to configure big data clusters settings post deployment](configure-bdc-postdeployment.md)

- [Performance best practices and configuration guidelines for SQL Server Big Data Clusters](performance-guidelines-tuned.md)

- [Case Study: SQL Workloads running on Apache Spark in MS SQL Server 2019 Big Data Cluster](https://aka.ms/sql-bdc-spark-perf/)
