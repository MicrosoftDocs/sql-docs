---
title: Hyperscale distributed functions architecture
description: Learn how Hyperscale databases are architected to scale out storage and compute resources for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, adbadram, malstewart
ms.date: 05/14/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
ms.custom:
  - build-2023
  - build-2023-dataai
  - ignite-2023
---

# Hyperscale distributed functions architecture

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

[Azure SQL Database Hyperscale](service-tier-hyperscale.md) is a cost efficient, high performance database service tier. This article details the highly scalable architecture of Hyperscale, featuring separate storage and compute tiers, fast scaling, nearly instantaneous backups and fast restores, read scale-out, and high read and write throughput.

## Hyperscale architecture overview

Traditional database engines centralize data management functions in a single process: even so-called distributed databases in production today have multiple copies of a monolithic data engine.

Hyperscale databases follow a different approach. Hyperscale separates the query processing engine, where the semantics of various data engines diverge, from the components that provide long-term storage and durability for the data. In this way, storage capacity can be smoothly scaled out as far as needed, up to 128 TB for a single Hyperscale database. 

A Hyperscale database contains the following types of components: compute nodes, page servers, the log service, and Azure storage. All network communication among Hyperscale components uses Azure network infrastructure with built-in redundancy.

High-availability secondary replicas and named replicas are optional compute nodes that can be added on demand. Both share the same storage components, so no data copy is required to spin up a new replica. A geo-secondary replica can be added on demand in the same or a different Azure region. For data protection and redundancy, geo-secondary replicas have storage components that are separate from those used by the primary replica.

The following diagram illustrates the functional Hyperscale architecture:   

:::image type="complex" source="media/service-tier-hyperscale/hyperscale-architecture.png" alt-text="Diagram showing Hyperscale's compute tier." lightbox="media/service-tier-Hyperscale/Hyperscale-architecture.png":::
Diagram that shows that Hyperscale's compute tier consists of a primary compute note and secondary compute nodes, each with RBPEX data cache. The log service communicates both with compute notes and page servers. Page servers exist in their own tier and also have RBPEX data cache.
:::image-end:::

The diagram shows the following components:   
**A. [Compute](#compute)**: Hyperscale separates the main database engine, referred to as Compute, from the transaction logging and data storage components.   
**B. [Storage](#azure-storage)**: Components that provide long-term storage and durability for data are separated from the main database engine. Data files are stored in separate Azure storage blobs. Data in Azure storage provides redundancy to recover a page server from failure.  
**C. [Page servers](#page-server)**: Page servers retrieve data from data files in the storage layer, store it in a local SSD cache, and provide the data to the main engine. The local SSD cache is allocated based on compute size to retain the active/hot data pages. Page servers grow as the database size increases. Each page server manages up to 128 GB of data.   
**D. [Log service](#log-service)**: A Hyperscale database uses log service to coordinate transaction log propagation to replicas and page servers.  
**E. [Named replicas](service-tier-hyperscale-replicas.md#named-replica)**: Named replicas have their own compute but use the same log service. You can have up to 30 named replicas. Each named replica can have up to 4 high availability replicas.   
**F. [High availability replicas](service-tier-hyperscale-replicas.md#hyperscale-secondary-replicas)**: High-availability replicas are optional standby copies of a primary, geo, or named replica, ready for failover and available for read-only workloads. Each primary replica and named replica can have up to 4 high availability replicas.  

## Compute

The compute node is where the relational engine lives. The compute node is where language, query, and transaction processing occur. All user interactions with a Hyperscale database happen through compute nodes. Compute nodes can either be configured to use serverless or provisioned compute.

Compute nodes have local SSD-based caches called Resilient Buffer Pool Extension (RBPEX Data Cache). RBPEX Data Cache is an intelligent low-latency data cache that minimizes the need to fetch data from remote page servers.

Hyperscale databases have one primary compute node where the read-write workload and transactions are processed. Up to four high-availability secondary compute nodes can be added on demand. They act as hot standby nodes for failover purposes and can serve as read-only compute nodes to offload read workloads when desired. [Named replicas](service-tier-hyperscale-replicas.md#named-replica) are secondary compute nodes designed to enable various additional OLTP [read-scale out](read-scale-out.md) scenarios and to better support Hybrid Transactional and Analytical Processing (HTAP) workloads. A [geo secondary](active-geo-replication-overview.md) compute node can be added for disaster recovery purposes and to serve as a read-only compute node to offload read workloads in a different Azure region.

In serverless, the primary replica and any high availability replicas or named replicas each independently autoscale based on their usage. The compute autoscaling range for the primary replica and any named replicas are configured independently. The autoscaling range of any high-availability replicas is inherited from the autoscaling configuration specified by their associated primary replica or named replica.

The database engine running on Hyperscale compute nodes is the same as in other Azure SQL Database service tiers. When users interact with the database engine on Hyperscale compute nodes, the supported surface area and engine behavior are the same as in other service tiers, except for [known limitations](service-tier-hyperscale.md#known-limitations).

## Page server

Page servers are systems representing a scaled-out storage engine. Each page server is responsible for a subset of the pages in the database. Each page server also has a replica that is kept for redundancy and availability.

The job of a page server is to serve database pages out to the compute nodes on demand, and to keep the pages updated as transactions update data. Page servers are kept up to date by replaying transaction log records from the log service.

Page servers also maintain covering SSD-based caches to enhance performance. Long-term storage of data pages is kept in Azure Storage for durability.

## Log service

The log service accepts transaction log records that correspond to data changes from the primary compute replica. Page servers then receive the log records from the log service and apply the changes to their respective slices of data. Additionally, compute secondary replicas receive log records from the log service and replay only the changes to pages already in their buffer pool or local RBPEX cache. All data changes from the primary compute replica are propagated through the log service to all the secondary compute replicas and page servers.

Finally, transaction log records are pushed out to long-term storage in Azure Storage, which is a virtually infinite storage repository. This mechanism removes the need for frequent log truncation. The common reasons for log growth such as missed log backups or slow data replication to secondary replicas do not apply to Hyperscale. The log service has local memory and SSD caches to speed up access to log records.

## Azure storage

Azure Storage contains all data files in a database. Page servers keep data files in Azure Storage up to date. This storage is also used for backup purposes and can be replicated between regions based on choice of storage redundancy.

Backups are implemented using storage snapshots of data files. Restore operations using snapshots are fast regardless of data size. A database can be restored to any point in time within its backup retention period.

Hyperscale supports configurable storage redundancy. When creating a Hyperscale database, you can choose from the following types of Azure standard storage:

- Locally redundant storage (LRS)
- Zone-redundant storage (ZRS)
- Read-access geo-redundant storage (RA-GRS)
- Read-access geo-zone-redundant storage (RA-GZRS)

Zone-redundant storage options are available in Azure [regions with availability zones](/azure/reliability/availability-zones-region-support).

The selected storage redundancy option is used for the lifetime of the database, for both data storage redundancy and [backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy).

## Related content

- [Hyperscale service tier](service-tier-hyperscale.md)
- [Azure SQL Database Hyperscale FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml)
- [Quickstart: Create a Hyperscale database in Azure SQL Database](hyperscale-database-create-quickstart.md)
- [Azure SQL Database Hyperscale named replicas FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml#read-scale-out-questions)
