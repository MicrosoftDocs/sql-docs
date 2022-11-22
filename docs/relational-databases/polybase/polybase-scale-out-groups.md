---
title: "PolyBase scale-out groups | Microsoft Docs"
description: Use the PolyBase Group feature to create a cluster of SQL Server instances. This improves query performance for large data sets from external sources.
ms.date: 02/22/2022
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
f1_keywords:
  - sql13.swb.polybasescaleoutcluster.page.f1
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, scale-out groups"
  - "scale-out PolyBase"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
monikerRange: ">= sql-server-2016"
---
# PolyBase scale-out groups

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

A standalone SQL Server instance with PolyBase can become a performance bottleneck when dealing with massive data sets in Hadoop or Azure Blob Storage. The PolyBase Group feature allows you to create a cluster of SQL Server instances to process large data sets from external data sources, such as Hadoop or Azure Blob Storage, in a scale-out fashion for better query performance. You can now scale your SQL Server compute to meet the performance demands of your workload. PolyBase Scale-out Groups, a group of SQL Server instances, enable you to process large external data sets in a parallel processing architecture. Data loading and query performance can increase linearly as you add more SQL Server instances to the group. 
  
[!INCLUDE[polybase-scaleout-banner-retirement](../../includes/polybase-scaleout-banner-retirement.md)]

See [Get started with PolyBase](./polybase-guide.md) and [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md).
  
![Diagram showing PolyBase scale-out groups.](../../relational-databases/polybase/media/polybase-scale-out-groups.png "PolyBase scale-out groups")  
  
## Head node  

The head node contains the SQL Server instance to which PolyBase queries are submitted. Each PolyBase group can have only one head node. A head node is a logical group of SQL Server Database Engine, PolyBase Engine, and PolyBase Data Movement Service on the SQL Server instance. With SQL Server 2017 and SQL Server 2016, the head node must be an Enterprise Edition. Beginning with SQL Server 2019 the PolyBase head node can be either an Enterprise or Standard edition.
  
## Compute node

A compute node contains the SQL Server instance that assists with scale-out query processing on external data. A compute node is a logical group of SQL Server and the PolyBase data movement service on the SQL Server instance. A PolyBase group can have multiple compute nodes. The head node and the compute nodes must all run the same version of SQL Server. The initial release of SQL Server 2016 allowed the compute nodes to be either an Enterprise or Standard edition. Beginning with SQL Server 2016 SP1, all editions of SQL Server can be a compute node.

## Scale-out Reads

When querying external SQL Server, Oracle or Teradata instances, partitioned tables will benefit from scale-out reads. Each node in a PolyBase scale-out group can spin up to 8 readers to read external data. And each reader is assigned one partition to read in the external table. 

For example, say you have an external SQL Server table with 12 monthly partitions and a 3-node PolyBase scale-out group, each node will use 4 PolyBase readers to process each of the 12 partitions. This is illustrated in the following image. 

> [!NOTE]
> This is different from scale-out reads over Hadoop. 

![PolyBase scale-out reads](../../relational-databases/polybase/media/polybase-scale-out-groups2.png "PolyBase scale-out groups")
  
## Distributed query processing  

PolyBase queries are submitted to the SQL Server on the head node. The part of the query that refers to external tables is handed-off to the PolyBase engine.
  
The PolyBase engine is the key component  behind PolyBase queries. It parses the query on external data, generates the query plan and distributes the work to the data movement service on the compute nodes for execution. After completion of the work, it receives the results from the compute nodes and submits them to SQL Server for processing and returning to the client.
  
The PolyBase data movement service receives instructions from the PolyBase engine and transfers data between HDFS and SQL Server, and between SQL Server instances on the head and compute nodes.
  
## Next steps

To configure a PolyBase scale-out group, see the following guide:

[Improve PolyBase scale-out groups on Windows](configure-scale-out-groups-windows.md)

## See Also

 [sys-dm-exec-compute-nodes](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md)   
 [sys-dm-exec-compute-node-status](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-node-status-transact-sql.md)   
 [sys.dm_exec_compute_node_errors](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-node-errors-transact-sql.md)
