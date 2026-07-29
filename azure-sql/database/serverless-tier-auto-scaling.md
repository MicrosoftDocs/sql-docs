---
title: Serverless Compute Tier Autoscaling
description: Learn how autoscaling works for the serverless compute tier in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kendalv, moslake, mathoma, dfurman
ms.date: 07/28/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
monikerRange: "=azuresql||=azuresql-db"
---
# Serverless compute tier autoscaling for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how autoscaling works for the [serverless compute tier](serverless-tier-overview.md) in Azure SQL Database. The **minimum vCores** and **maximum vCores** parameters are important for the serverless compute tier. These parameters shape the database performance experience and compute cost.

## Scaling responsiveness

Serverless databases run on infrastructure with enough capacity to satisfy resource demand without interruption for any amount of compute requested within limits set by the maximum vCore configuration. In general, CPU scale-up to the maximum vCores configured is nearly instantaneous and occurs without any connectivity disruption. Occasionally, scale-up can take longer if machine resources need to be rebalanced, which can take up to a few minutes. The database remains online during rebalancing except at the end of the operation when connections are briefly dropped. This kind of rebalancing is rare. In all cases, CPU scale-up is independent of memory scale-up.

## Memory management

In both the General Purpose and Hyperscale service tiers, the system reclaims memory for serverless databases more frequently than for provisioned compute databases. This behavior helps control costs in serverless environments and can affect performance.

### Cache reclamation

Unlike provisioned compute databases, a serverless database reclaims memory from the SQL cache when CPU or active cache utilization is low.

- Active cache utilization is low when the total size of the most recently used cache entries stays below a threshold for a period of time.
- When cache reclamation is triggered, the system reduces the target cache size incrementally to a fraction of its previous size and continues reclaiming only if usage remains low.
- When cache reclamation occurs, the policy for selecting cache entries to evict is the same selection policy as for provisioned compute databases when memory pressure is high.
- The system never reduces the cache size below the minimum memory limit as defined by minimum vCores.

In both serverless and provisioned compute databases, the system can evict cache entries if all available memory is used.

When CPU utilization is low, active cache utilization can remain high depending on the usage pattern and prevent memory reclamation. Also, other delays can occur after user activity stops, before memory reclamation happens, due to periodic background processes responding to prior user activity. For example, delete operations and Query Store cleanup tasks generate ghost records that are marked for deletion but aren't physically deleted until the ghost cleanup process runs. Ghost cleanup might involve reading data pages into cache.

### Cache hydration

The SQL memory cache grows as the system fetches data from disk, working at the same speed as provisioned databases. When the database is busy, the cache can grow without constraint while memory is available.

<a id="disk-cache-mgmt"></a>

## Disk cache management

In the Hyperscale service tier for both serverless and provisioned compute tiers, each compute replica uses a Resilient Buffer Pool Extension (RBPEX) cache. This cache stores data pages on local SSD to improve IO performance. However, in the serverless compute tier for Hyperscale, the RBPEX cache for each compute replica automatically grows and shrinks in response to increasing and decreasing workload demand. The maximum size the RBPEX cache can grow to is three times the maximum memory configured for the database. For details on maximum memory and RBPEX autoscaling limits in serverless, see [serverless Hyperscale resource limits](resource-limits-vcore-single-databases.md#hyperscale---serverless-compute---standard-series-gen5).

## Related content

- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Auto-pause and auto-resume in serverless](serverless-tier-auto-pause-resume.md)
- [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/)
