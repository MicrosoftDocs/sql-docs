---
title: "Enable the availability group feature for a SQL Server instance"
description: "Describes how to enable the Always On availability group feature for an instance of SQL Server."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: maghan
ms.date: 02/01/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "Availability Groups [SQL Server], server instance"
  - "Availability Groups [SQL Server], about"
---

# Enable the Always On availability group feature for a SQL Server instance

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article contains information about the requirements for configuring an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to support [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> For essential information about [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] prerequisites and restrictions for Windows Server Failover Clustering (WSFC) nodes and instances of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).

## <a id="TermsAndDefinitions"></a> Terms and definitions

[Always On Availability Groups](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
A high-availability and disaster-recovery solution that provides an enterprise-level replacement for database mirroring. An *availability group* supports a failover environment for a discrete set of user databases, known as *availability databases*, that fail over together.

**Availability replica**
An instantiation of an availability group hosted by a specific instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] and that maintains a local copy of each availability database that belongs to the availability group. Two availability replicas exist: a single *primary replica* and one to four *secondary replicas*. The server instances that host the availability replicas for a given availability group must reside on different nodes of a single Windows Server Failover Clustering (WSFC) cluster.

[database mirroring endpoint](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
An endpoint is a SQL Server object that enables SQL Server to communicate over the network. To participate in database mirroring and/or [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] a server instance requires a unique, dedicated endpoint. All mirroring and availability group connections on a server instance use the same database mirroring endpoint. This special-purpose endpoint is used exclusively to receive these connections from other server instances.

## <a id="ConfigSI"></a> To configure a server instance to support Always On availability groups

To support [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)], a server instance must reside on a node in the WSFC failover cluster that hosts the availability group, be [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] enabled and possessed a database mirroring endpoint.

1. Enable the Always On Availability Groups feature on every server instance to participate in one or more availability groups. A given server instance can host only a single availability replica for a given availability group.

1. Ensure that the server instance possesses a database mirroring endpoint.

## <a id="RelatedTasks"></a> Related tasks

**To enable Always On Availability Groups**

- [Enable and Disable Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)

**To determine whether a database mirroring endpoint exists**

- [sys.database_mirroring_endpoints (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)

**To create a database mirroring endpoint**

- [Create a Database Mirroring Endpoint for Always On Availability Groups (SQL Server PowerShell)](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)

- [Create a Database Mirroring Endpoint for Windows Authentication (Transact-SQL)](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)

- [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections (Transact-SQL)](../../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)

## Related content

- [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)
- [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)
- [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)
- [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))
- [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)
- [Overview of Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)
- [The Database Mirroring Endpoint (SQL Server)](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)
- [Always On Availability Groups: Interoperability (SQL Server)](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)
- [Failover Clustering and Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)
- [Windows Server Failover Clustering (WSFC) with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)
- [Always On Failover Cluster Instances (SQL Server)](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)
