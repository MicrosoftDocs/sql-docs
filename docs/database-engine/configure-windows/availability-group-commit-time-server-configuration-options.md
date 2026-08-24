---
title: "Server Configuration: availability group commit time (ms)"
description: "Learn to modify the commit time for an availability group replica and the options that control the cache's behavior. See when to change this option in SQL Server."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
ms.custom:
  - build-2025
---
# Server configuration: availability group commit time (ms)

[!INCLUDE [SQL Server](../../includes/applies-to-version/_ss2025.md)]

Use the `availability group commit time` server configuration option to specify the group commit time, in milliseconds, for an [Always On availability group](../availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).

> [!NOTE]  
> The `availability group commit time` server configuration option is available starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

## Overview

Changes made inside a transaction aren't visible outside of the transaction until the transaction is committed. The definition of a committed transaction for an Always On availability group involves all synchronous secondary replicas in the availability group acknowledging the hardened commit. After a commit is issued on the primary replica, that fact needs to be propagated quickly across the network to all secondary replicas.

Since SQL Server relies on [write-ahead transaction logging](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#WAL) to maintain ACID properties of a transaction, changes are first recorded to the transaction log in the form of [log blocks](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#log-blocks). These log blocks are sent, and then applied, to the transaction log of all the secondary replicas.

To improve performance and reduce latency in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, SQL Server uses a 10 millisecond delay in an attempt to fill Always On availability group log blocks with multiple commits before sending them to secondary replicas.

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces the `availability group commit time` server configuration option to specify the group commit time, in milliseconds, for an availability group replica. For business scenarios where the default value of 10 milliseconds is too long, this option gives SQL Server the ability to group multiple commits into batches within fewer log blocks.

Grouping commits introduces a *tradeoff* between the efficiency of data replication, and the time it takes to report a successful commit to the issuer:

- On busy systems, grouping commits results in log blocks that are filled with more transactions, which helps avoid network saturation, and the overhead associated with applying a large number of small log blocks on a secondary replica.
- However, there's a 10 millisecond delay before the transaction is applied to the secondary replica, which can be problematic for some business scenarios.

For business scenarios where the default value of 10 milliseconds is too long, you can modify the `availability group commit time` server configuration option to a lower value, so that transactions are sent to the secondary replica faster.

## Remarks

- The default value of `0` indicates that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the default value of 10 ms for the availability group commit time.

- The `availability group commit time` server configuration option is available when [show advanced options](show-advanced-options-server-configuration-option.md) is set to `1`.

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
