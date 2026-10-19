---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/11/2025
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
ai-usage: ai-assisted
---
This article provides an overview of business continuity solutions for high availability and disaster recovery in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)], on Windows and Linux.

Everyone who deploys [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] needs to make sure that all mission critical [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] instances and the databases within them are available when the business and end users need them, whether that availability is during regular business hours or around the clock. The goal is to keep the business up and running with minimal or no interruption. This concept is also known as *business continuity*.

[!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions introduced features and enhancements for availability. The biggest addition is support for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux distributions. For a full list of the new features in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)], see the following articles:

| Version | Operating system |
| --- | --- |
| What's new in [!INCLUDE [sssql25-md](../sssql25-md.md)] | [Windows](../../sql-server/what-s-new-in-sql-server-2025.md) &#124; [Linux](../../linux/sql-server-linux-whats-new-2025.md) |
| What's new in [!INCLUDE [sssql22-md](../sssql22-md.md)] | [Windows](../../sql-server/what-s-new-in-sql-server-2022.md) &#124; [Linux](../../linux/sql-server-linux-whats-new-2022.md) |
| What's new in [!INCLUDE [sssql19-md](../sssql19-md.md)] | [Windows](../../sql-server/what-s-new-in-sql-server-2019.md) &#124; [Linux](../../linux/sql-server-linux-whats-new-2019.md) |
| What's new in [!INCLUDE [sssql17-md](../sssql17-md.md)] | [Windows](../../sql-server/what-s-new-in-sql-server-2017.md) &#124; [Linux](../../linux/sql-server-linux-whats-new-2017.md) |

This article focuses on the availability scenarios in [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions, as well as the new and enhanced availability features. The scenarios include hybrid ones that can span [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] deployments on both Windows Server and Linux, and ones that can increase the number of readable copies of a database.

While this article doesn't cover availability options external to [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] (such as virtualization), everything discussed here applies to [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] installations inside a guest virtual machine whether in the public cloud or hosted by an on-premises hypervisor server.

## SQL Server scenarios that use availability features

You can use Always On availability groups, failover cluster instances, and log shipping in different ways, and not just for availability. There are four main ways you can use the availability features:

- High availability
- Disaster recovery
- Migrations and upgrades
- Scaling out readable copies of one or more databases

The following sections describe the relevant features for each scenario. One feature not covered is [SQL Server replication](../../relational-databases/replication/sql-server-replication.md). While [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] replication isn't officially designated as an availability feature under the Always On umbrella, it's often used for making data redundant in certain scenarios. Merge replication isn't supported for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux. For more information, see [SQL Server replication on Linux](../../linux/sql-server-linux-replication.md).

> [!IMPORTANT]  
> The [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] availability features don't replace the requirement to have a robust, well tested backup and restore strategy. A backup and restore strategy is the most fundamental building block of any availability solution.

[!INCLUDE [sql-server-business-continuity-high-availability](high-availability.md)]

[!INCLUDE [sql-server-business-continuity-migrations](migrations.md)]

## Cross-platform and Linux distribution interoperability

With [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] support on both Windows Server and Linux, this section covers how they can work together for availability in addition to other purposes. It also covers the story for solutions that incorporate more than one Linux distribution.

> [!NOTE]  
> There are no scenarios where a WSFC-based failover cluster instance (FCI) or availability group (AG) works with a Linux-based FCI or AG directly. A Windows Server Failover Cluster (WSFC) can't be extended by a Pacemaker node and vice versa.

## Distributed availability groups

Distributed AGs are designed to span AG configurations, whether those two underlying clusters underneath the AGs are two different WSFCs, Linux distributions, or one on a WSFC and the other on Linux. A distributed AG is the primary method of having a cross platform solution. A distributed AG is also the primary solution for migrations such as converting from a Windows Server-based [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] infrastructure to a Linux-based one if that is what your company wants to do. As noted previously, AGs, and especially distributed AGs, would minimize the time that an application would be unavailable for use. An example of a distributed AG that spans a WSFC and Pacemaker is shown in the following diagram:

:::image type="content" source="media/business-continuity/distributed-availability-group-span.png" alt-text="Diagram showing a distributed availability group that spans a WSFC and Pacemaker.":::

If you configure an AG with a cluster type of None, it can span Windows Server and Linux, and multiple Linux distributions. Since this configuration isn't true high availability, don't use it for mission critical deployments. Instead, use it for read-scale or migration and upgrade scenarios.

## Log shipping

Log shipping is based on backup and restore, so there are no differences in the databases, file structures, and other elements for [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Windows Server versus [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] on Linux. You can configure log shipping between a Windows Server-based [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] installation and a Linux one, and between distributions of Linux. Everything else remains the same.

Just like with an AG, log shipping doesn't work when the source server is at a higher [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] major version, against a target that is at a lower major version.

## Summary

You can make instances and databases of [!INCLUDE [sssql17-md](../sssql17-md.md)] and later versions highly available by using the same features on both Windows Server and Linux. Besides standard availability scenarios of local high availability and disaster recovery, you can minimize downtime associated with upgrades and migrations by using the availability features in [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)]. AGs can also provide extra copies of a database as part of the same architecture to scale out readable copies. Whether you're deploying a new solution or considering an upgrade, [!INCLUDE [ssnoversion-md](../ssnoversion-md.md)] has the availability and reliability you require.
