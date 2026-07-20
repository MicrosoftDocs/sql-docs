---
title: "PolyBase Scale-Out Groups"
description: Use the PolyBase Group feature to create a cluster of SQL Server instances. This improves query performance for large data sets from external sources.
author: markingmyname
ms.author: maghan
ms.reviewer: hudequei, randolphwest
ms.date: 12/03/2025
ms.service: sql
ms.subservice: polybase
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.polybasescaleoutcluster.page.f1"
helpviewer_keywords:
  - "PolyBase"
  - "PolyBase, scale-out groups"
  - "scale-out PolyBase"
monikerRange: ">=sql-server-2017"
---
# PolyBase scale-out groups

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

A standalone SQL Server instance with PolyBase can become a performance bottleneck when dealing with massive data sets in Hadoop or Azure Blob Storage. The PolyBase Group feature allows you to create a cluster of SQL Server instances to process large data sets from external data sources, such as Hadoop or Azure Blob Storage, in a scale-out fashion for better query performance. You can now scale your SQL Server compute to meet the performance demands of your workload. PolyBase Scale-out Groups, a group of SQL Server instances, enable you to process large external data sets in a parallel processing architecture. Data loading and query performance can increase linearly as you add more SQL Server instances to the group.

[!INCLUDE [polybase-scaleout-banner-retirement](../../includes/polybase-scaleout-banner-retirement.md)]

See [Data virtualization with PolyBase in SQL Server](overview.md) and [Data virtualization with PolyBase in SQL Server](overview.md).

:::image type="content" source="media/polybase-scale-out-groups.png" alt-text="Diagram showing PolyBase scale-out groups." lightbox="media/polybase-scale-out-groups.png":::

## Head node

The head node contains the SQL Server instance to which PolyBase queries are submitted. Each PolyBase group can have only one head node. A head node is a logical group of SQL Server Database Engine, PolyBase Engine, and PolyBase Data Movement Service on the SQL Server instance. With SQL Server 2017 and SQL Server 2016, the head node must be an Enterprise Edition. Beginning with SQL Server 2019 the PolyBase head node can be either an Enterprise or Standard edition.

## Compute node

A compute node contains the SQL Server instance that assists with scale-out query processing on external data. A compute node is a logical group of SQL Server and the PolyBase data movement service on the SQL Server instance. A PolyBase group can have multiple compute nodes. The head node and the compute nodes must all run the same version of SQL Server. The initial release of SQL Server 2016 allowed the compute nodes to be either an Enterprise or Standard edition. Beginning with SQL Server 2016 SP1, all editions of SQL Server can be a compute node.

## Scale-out reads

When you query external SQL Server, Oracle, or Teradata instances, partitioned tables benefit from scale-out reads. Each node in a PolyBase scale-out group can spin up to eight readers to read external data. And each reader is assigned one partition to read in the external table.

For example, say you have an external SQL Server table with 12 monthly partitions and a three-node PolyBase scale-out group, each node uses four PolyBase readers to process each of the 12 partitions. This scenario is illustrated in the following image.

> [!NOTE]  
> This functionality is different to scale-out reads over Hadoop.

:::image type="content" source="media/polybase-scale-out-groups2.png" alt-text="Diagram of PolyBase scale-out reads." lightbox="media/polybase-scale-out-groups2.png":::

## Distributed query processing

PolyBase queries are submitted to the SQL Server on the head node. The part of the query that refers to external tables is handed-off to the PolyBase engine.

The PolyBase engine is the key component behind PolyBase queries. It parses the query on external data, generates the query plan, and distributes the work to the data movement service on the compute nodes for execution. After completion of the work, it receives the results from the compute nodes and submits them to SQL Server for processing and returning to the client.

The PolyBase data movement service receives instructions from the PolyBase engine and transfers data between HDFS and SQL Server, and between SQL Server instances on the head and compute nodes.

## Related content

- [sys-dm-exec-compute-nodes](../system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)
- [sys-dm-exec-compute-node-status](../system-dynamic-management-views/sys-dm-exec-compute-node-status-transact-sql.md)
- [sys.dm_exec_compute_node_errors](../system-dynamic-management-views/sys-dm-exec-compute-node-errors-transact-sql.md)
- [Configure PolyBase scale-out groups on Windows](configure-scale-out-groups-windows.md)
