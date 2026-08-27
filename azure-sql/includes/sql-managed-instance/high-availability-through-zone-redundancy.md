---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/16/2024
ms.service: azure-sql-managed-instance
ms.topic: include
---
By default, Azure SQL Managed Instance achieves *availability* through local redundancy, making your instance available during maintenance operations, issues with data center outages, and other problems with the SQL database engine. However, to minimize a potential outage to an entire zone affecting your data, you can achieve *high availability* by enabling [zone redundancy](../../managed-instance/high-availability-sla-local-zone-redundancy.md). Without zone redundancy, failovers happen locally within the same data center, which might result in your instance being unavailable until the outage is resolved - the only way to recover is through a disaster recovery solution, such as through a [failover group](../../managed-instance/failover-group-sql-mi.md), or a [geo-restore](../../managed-instance/recovery-using-backups.md#geo-restore) of a geo-redundant backup.