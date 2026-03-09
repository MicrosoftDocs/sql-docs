---
title: Troubleshoot Geo-Replication and Redo Lag
titleSuffix: Azure SQL Database
description: Learn how to understand and troubleshoot geo-replication and redo lag in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mahyon, randolphwest
ms.date: 03/02/2026
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: troubleshooting
ms.custom:
  - azure-sql-split
monikerRange: "=azuresql || =azuresql-db"
---

# Troubleshoot geo-replication and redo lag

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

In active geo-replication, the geo-secondary replica continuously receives and applies transaction log records from the primary. When the secondary replica can't apply logs as fast as the primary generates them, a backlog builds (redo queue) and the time gap increases (redo lag). This situation can affect read-only freshness on the secondary and increase failover time.

- **Redo queue**: The volume of transaction log records that geo-replication ships to the secondary but doesn't apply yet.
- **Redo lag**: The elapsed time between transaction commit on the primary and completion of replay on the secondary.

Geo-replication is asynchronous. Redo lag on the secondary replica does not cause waits on the primary, but redo lag can cause data on the secondary to be behind.

## Symptoms

- Stale data on the secondary for read-only workloads (reporting, analytics, or offloaded reads).
- Longer failover time, which increases Recovery Time Objective (RTO).
- Sustained resource pressure on the secondary, reducing its ability to catch up.
- Confirm redo lag in the DMV [sys.dm_database_replica_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-database-replica-states-azure-sql-database?view=azuresqldb-current&preserve-view=true), if `redo_queue_size > 0` and growing and `secondary_lag_seconds` is increasing.

## Why redo backlog grows

Although the secondary database is read-only, it still maintains a transaction log for internal operations, including replaying log records from the primary. When the redo queue grows, the secondary must retain more transaction log data. 

This situation can lead to:

- Transaction log growth on the secondary.
- Higher storage consumption, which can affect cost and performance.
- Potential throttling scenarios when thresholds are exceeded.

## Impact of replica size mismatch

You should configure the primary and geo-secondary replica with the same service level objective (SLO), backup storage redundancy, [compute tier](service-tiers-sql-database-vcore.md#compute) (provisioned or serverless), and compute size (DTUs or vCores). 

If you configure a secondary database with a lower compute size than the primary database, you might experience:

- Resource contention on the secondary (CPU, I/O), which slows down redo operations.
- Inability to keep up with the transaction log generation rate of the primary.
- Increased redo queue size, which worsens lag and reduces replication effectiveness.

## Recommendations

To reduce redo lag and maintain replication health and efficient log usage on the secondary:

- Align SLO and compute sizes. Ensure the secondary database has the same performance tier as the primary.
  - Configure geo-secondary: [Active geo-replication](active-geo-replication-overview.md#configure-geo-secondary)
  - Scale a single database: [Scale single database resources in Azure SQL Database](single-database-scale.md)
  - Scale an elastic pool: [Scale elastic pool resources in Azure SQL Database](elastic-pool-scale.md)
  - Cost considerations: [Plan and manage costs for Azure SQL Database](cost-management.md)

- Monitor regularly. Use dynamic management views (DMVs) such as [sys.dm_database_replica_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-database-replica-states-azure-sql-database?view=azuresqldb-current&preserve-view=true) to track redo lag and queue size. Redo lag is confirmed when `redo_queue_size > 0` and growing, and `secondary_lag_seconds` is increasing.

- Optimize workloads:

  - Reduce long-running transactions on the secondary and high log generation spikes on the primary.
    - Avoid large index rebuilds during peak times. Rebuilds can acquire schema modification (SCH-M) locks, which might block the redo thread on the secondary and contribute to redo queue build-up.

## Related content

- [Active geo-replication](active-geo-replication-overview.md)
- [Configure active geo-replication and failover](active-geo-replication-configure-portal.md)
- [Monitor geo-replication lag](active-geo-replication-overview.md#monitor-geo-replication-lag)
