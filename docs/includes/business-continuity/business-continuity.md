---
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/23/2022
ms.service: sql
ms.topic: include
---
This article provides an overview of business continuity solutions for high availability and disaster recovery in [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)], on Windows and Linux.

One common task everyone deploying [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] has to account for is making sure that all mission critical [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] instances and the databases within them are available when the business and end users need them, whether that is 9 to 5 or around the clock. The goal is to keep the business up and running with minimal or no interruption. This concept is also known as *business continuity*.

[!INCLUDE[sssql17-md](../sssql17-md.md)] introduced many new features or enhancements to existing ones, some of which are for availability. The biggest addition to [!INCLUDE[sssql17-md](../sssql17-md.md)] was support for [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux distributions. For a full list of the new features in [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)], see the following articles:

- [What's new in SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md)
- [What's new for SQL Server 2017 on Linux](../../linux/sql-server-linux-whats-new.md)
- [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new for SQL Server 2019 on Linux](../../linux/sql-server-linux-whats-new-2019.md)
- [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md)

This article is focused on covering the availability scenarios in [!INCLUDE[sssql17-md](../sssql17-md.md)] and later versions, as well as the new and enhanced availability features. The scenarios include hybrid ones that will be able to span [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] deployments on both Windows Server and Linux, and ones that can increase the number of readable copies of a database.

While this article doesn't cover availability options external to [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)], such as those provided by virtualization, everything discussed here applies to [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] installations inside a guest virtual machine whether in the public cloud or hosted by an on premises hypervisor server.

## SQL Server scenarios using the availability features

Always On availability groups, Always On failover cluster instances, and log shipping, can be used in various ways, and not necessarily just for availability purposes. There are four main ways the availability features can be used:

- High availability
- Disaster recovery
- Migrations and upgrades
- Scaling out readable copies of one or more databases

The following sections discuss the relevant features that can be used for that particular scenario. The one feature not covered is [SQL Server replication](../../relational-databases/replication/sql-server-replication.md). While not officially designated as an availability feature under the Always On umbrella, [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] replication is often used for making data redundant in certain scenarios. Merge replication isn't supported for [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux. For more information, see [SQL Server Replication on Linux](../../linux/sql-server-linux-replication.md).

> [!IMPORTANT]  
> The [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] availability features do not replace the requirement to have a robust, well tested backup and restore strategy, the most fundamental building block of any availability solution.

[!INCLUDE[sql-server-business-continuity-high-availability](high-availability.md)]

[!INCLUDE[sql-server-business-continuity-migrations](migrations.md)]

## Cross-platform and Linux distribution interoperability

With [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] now supported on both Windows Server and Linux, this section covers the scenarios of how they can work together for availability in addition to other purposes, and the story for solutions that will incorporate more than one Linux distribution.

> [!NOTE]  
> There are no scenarios where a WSFC-based FCI or AG will work with a Linux-based FCI or AG directly. A WSFC can't be extended by a Pacemaker node and vice versa.

## Distributed availability groups

Distributed AGs are designed to span AG configurations, whether those two underlying clusters underneath the AGs are two different WSFCs, Linux distributions, or one on a WSFC and the other on Linux. A distributed AG will be the primary method of having a cross platform solution. A distributed AG is also the primary solution for migrations such as converting from a Windows Server-based [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] infrastructure to a Linux-based one if that is what your company wants to do. As noted above, AGs, and especially distributed AGs, would minimize the time that an application would be unavailable for use. An example of a distributed AG that spans a WSFC and Pacemaker is shown below.

:::image type="content" source="media/business-continuity/distributed-availability-group-span.png" alt-text="Diagram showing a distributed availability group that spans a WSFC and Pacemaker.":::

If an AG is configured with a cluster type of None, it can span Windows Server and Linux, as well as multiple Linux distributions. Since this isn't a true high availability configuration, it shouldn't be used for mission critical deployments, but for read-scale or migration/upgrade scenarios.

## Log shipping

Since log shipping is based on backup and restore, and there are no differences in the databases, file structures, etc., for [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Windows Server versus [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] on Linux. This means that log shipping can be configured between a Windows Server-based [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] installation and a Linux one, as well as between distributions of Linux. Everything else remains the same. The only caveat is that log shipping, just like an AG, can't work when the source is at a higher [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] major version against a target that is at a lower version of [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)].

## Summary

Instances and databases of [!INCLUDE[sssql17-md](../sssql17-md.md)] and later versions can be made highly available using the same features on both Windows Server and Linux. Besides standard availability scenarios of local high availability and disaster recovery, downtime associated with upgrades and migrations can be minimized with the availability features in [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)]. AGs can also provide additional copies of a database as part of the same architecture to scale out readable copies. Whether you're deploying a new solution or considering an upgrade, [!INCLUDE[ssnoversion-md](../ssnoversion-md.md)] has the availability and reliability you require.
