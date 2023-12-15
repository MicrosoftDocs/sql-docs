---
title: Failover group overview
description: Deduplicating content between SQL Database and SQL Managed Instance, in this case using an include for the terminology for failover groups that overlap between both products.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: rsetlem, mlandzic, strrodic
ms.date: 12/15/2023
ms.topic: include
---
- **Failover (no data loss)**

  Failover performs full data synchronization between primary and secondary databases before the secondary switches to the primary role. This guarantees no data loss. Failover is only possible when the primary is accessible. Failover is used in the following scenarios:

  - Perform disaster recovery (DR) drills in production when data loss isn't acceptable
  - Relocate the workload to a different region
  - Return the workload to the primary region after the outage has been mitigated (failback)
  
- **Forced failover (potential data loss)**

  Forced failover immediately switches the secondary to the primary role without waiting for recent changes to propagate from the primary. This operation can result in potential data loss. Forced failover is used as a recovery method during outages when the primary isn't accessible. When the outage is mitigated, the old primary will automatically reconnect and become a new secondary. A failover can be executed to fail back, returning the replicas to their original primary and secondary roles.

- **Grace period with data loss**

  Because data is replicated to the secondary using asynchronous replication, forced failover of groups with Microsoft managed failover policies can result in data loss. You can customize the failover policy to reflect your application's tolerance to data loss. By configuring `GracePeriodWithDataLossHours`, you can control how long the Azure SQL service waits before initiating a forced failover, which can result in data loss.
