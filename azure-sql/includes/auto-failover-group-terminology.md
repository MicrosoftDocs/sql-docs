---
title: Auto-failover group overview
description: De-duplicating content between SQL Database and SQL Managed Instance, in this case using an include for the terminology for auto-failover groups that overlap between both products.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: emlisa, mlandzic
ms.date: 02/02/2023
ms.topic: include
---

- **Failover (no data loss)**

  Planned failover performs full data synchronization between primary and secondary databases before the secondary switches to the primary role. This guarantees no data loss. Planned failover is only possible when the primary is accessible and network traffic between the primary and secondary database flows. Planned failover is used in the following scenarios:

  - Perform disaster recovery (DR) drills in production when data loss is not acceptable
  - Relocate the databases to a different region
  - Return the databases to the primary region after the outage has been mitigated (failback)
  
  > [!NOTE]
  > If a database contains in-memory OLTP objects, the primary database and the secondary geo-replica database must have matching service tiers, as in-memory OLTP objects reside in memory. A lower service tier on the geo-replica database may result in out-of-memory issues. If this occurs, the geo-replica may fail to recover the database, causing unavailability of the secondary database along with in-memory OLTP objects on the geo-secondary. This, in turn, may cause failovers to be unsuccessful as well. To avoid this, ensure that the service tier of the geo-secondary database matches that of the primary database. Service tier upgrades can be size-of-data operations and may take a while to finish.

- **Forced failover (potential data loss)**

  Forced failover immediately switches the secondary to the primary role without waiting for recent changes to propagate from the primary. This operation may result in potential data loss. Forced failover is used as a recovery method during outages when the primary is not accessible. When the outage is mitigated, the old primary will automatically reconnect and become a new secondary. A failover may be executed to fail back, returning the replicas to their original primary and secondary roles.

- **Grace period with data loss**

  Because the data is replicated to the secondary database using asynchronous replication, forced failover of failover groups with Microsoft managed failover policy may result in data loss. You can customize the failover policy to reflect your application's tolerance to data loss. By configuring `GracePeriodWithDataLossHours`, you can control how long the Azure SQL service waits before initiating a forced failover, which may result in data loss.

- **Read-only failover policy**

  By default, the failover of the read-only listener is disabled. It ensures that the performance of the primary is not impacted when the secondary is offline. However, it also means the read-only sessions will not be able to connect until the secondary is recovered. If you cannot tolerate downtime for the read-only sessions and can use the primary for both read-only and read-write traffic at the expense of the potential performance degradation of the primary, you can enable failover for the read-only listener by configuring the `AllowReadOnlyFailoverToPrimary` property. In that case, the read-only traffic will be automatically redirected to the primary if the secondary is not available.

  > [!NOTE]
  > The `AllowReadOnlyFailoverToPrimary` property only has effect if Microsoft managed failover policy is enabled and a forced failover has been triggered. In that case, if the property is set to True, the new primary will serve both read-write and read-only sessions.
