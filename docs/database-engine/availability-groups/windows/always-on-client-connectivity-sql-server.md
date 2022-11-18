---
title: "Driver and client connectivity support for availability groups"
description: "This topic describes considerations for client connectivity to Always On availability groups, including prerequisites, restrictions, and recommendations for client configurations and settings. "
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 07/29/2022
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], listeners"
  - "Availability Groups [SQL Server], prerequisites and restrictions"
  - "Availability Groups [SQL Server], client connectivity"
---
# Driver and client connectivity support for availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes considerations for client connectivity to Always On availability groups, including prerequisites, restrictions, and recommendations for client configurations and settings.

## Client connectivity support

The section below provides information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] support for client connectivity.

### Driver support

The following table summarizes driver support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]:

|Driver|Multi-subnet failover|Application intent|Read-only routing|Multi-subnet failover: faster single subnet endpoint failover|Multi-subnet failover: named instance resolution for SQL clustered instances|  
|------------|----------------------------|------------------------|------------------------|--------------------------------------------------------------------|-----------------------------------------------------------------------------------|  
|SQL Native Client 11.0 ODBC|Yes|Yes|Yes|Yes|Yes|  
|SQL Native Client 11.0 OLEDB|No|Yes|Yes|No|No|  
|ADO.NET with .NET Framework 4.0 with connectivity patch <sup>1</sup>|Yes|Yes|Yes|Yes|Yes|  
|ADO.NET with .NET Framework 3.5 SP1 with connectivity patch <sup>2</sup>|Yes|Yes|Yes|Yes|Yes|  
|[Microsoft ODBC Driver 13.1+ for SQL Server](../../../connect/odbc/microsoft-odbc-driver-for-sql-server.md)|Yes|Yes|Yes|Yes|Yes|
|[Microsoft JDBC Driver 4.0+ for SQL Server](../../../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md)|Yes|Yes|Yes|Yes|Yes|  
|[Microsoft OLE DB Driver for SQL Server](../../../connect/oledb/oledb-driver-for-sql-server.md) <sup>3</sup>|Yes|Yes|Yes|Yes|Yes|

<sup>1</sup> Download the connectivity patch for ADO .NET with .NET Framework 4.0: [https://support.microsoft.com/kb/2600211](https://support.microsoft.com/kb/2600211).

<sup>2</sup> Download the connectivity patch for ADO.NET with .NET Framework 3.5 SP1: [https://support.microsoft.com/kb/2654347](https://support.microsoft.com/kb/2654347).

<sup>3</sup> Download Microsoft OLE DB Driver for SQL Server: [https://aka.ms/downloadmsoledbsql](../../../connect/oledb/download-oledb-driver-for-sql-server.md).

> [!IMPORTANT]  
> To connect to an availability group listener, a client must use a TCP connection string.

## Next steps

- [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)
- [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)

## See also

- [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)
- [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)
- [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)
- [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)
- [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))
- [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)
- [A long time delay occurs when you reconnect an IPSec connection from a computer that is running Windows Server 2003, Windows Vista, Windows Server 2008, Windows 7, or Windows Server 2008 R2](https://support.microsoft.com/kb/980915)
- [The Cluster service takes about 30 seconds to fail over IPv6 IP addresses in Windows Server 2008 R2](https://support.microsoft.com/en-us/topic/the-cluster-service-takes-about-30-seconds-to-fail-over-ipv6-ip-addresses-in-windows-server-2008-09a35200-9816-b8eb-fa14-2746894ac0d1)
- [Slow failover operation if no router exists between the cluster and an application server](https://support.microsoft.com/kb/2582281)
