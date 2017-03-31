---
title: "Always On Client Connectivity (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], listeners"
  - "Availability Groups [SQL Server], prerequisites and restrictions"
  - "Availability Groups [SQL Server], client connectivity"
ms.assetid: b456448d-1757-48c8-8bbb-2d1c2d6d61e9
caps.latest.revision: 22
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Always On Client Connectivity (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  This topic describes considerations for client connectivity to Always On Availability Groups, including prerequisites, restrictions, and recommendations for client configurations and settings.  
  
 **In this Topic:**  
  
-   [Client Connectivity Support](#ClientConnSupport)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="ClientConnSupport"></a> Client Connectivity Support  
 The section below provides information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] support for client connectivity.  
  
 **Driver Support**  
  
 The following table summarizes driver support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]:  
  
|Driver|Multi-Subnet Failover|Application Intent|Read-Only Routing|Multi-Subnet Failover: Faster Single Subnet Endpoint Failover|Multi-Subnet Failover: Named Instance Resolution For SQL Clustered Instances|  
|------------|----------------------------|------------------------|------------------------|--------------------------------------------------------------------|-----------------------------------------------------------------------------------|  
|SQL Native Client 11.0 ODBC|Yes|Yes|Yes|Yes|Yes|  
|SQL Native Client 11.0 OLEDB|No|Yes|Yes|No|No|  
|ADO.NET with .NET Framework 4.0 with connectivity patch*|Yes|Yes|Yes|Yes|Yes|  
|ADO.NET with .NET Framework 3.5 SP1 with connectivity patch**|Yes|Yes|Yes|Yes|Yes|  
|Microsoft JDBC driver 4.0 for SQL Server|Yes|Yes|Yes|Yes|Yes|  
  
 *Download the connectivity patch for ADO .NET with .NET Framework 4.0: [http://support.microsoft.com/kb/2600211](http://support.microsoft.com/kb/2600211).  
  
 **Download the connectivity patch for ADO.NET with .NET Framework 3.5 SP1: [http://support.microsoft.com/kb/2654347](http://support.microsoft.com/kb/2654347).  
  
> [!IMPORTANT]  
>  To connect to an availability group listener, a client must use a TCP connection string.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)   
 [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](http://go.microsoft.com/fwlink/?LinkId=227600)   
 [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)   
 [A long time delay occurs when you reconnect an IPSec connection from a computer that is running Windows Server 2003, Windows Vista, Windows Server 2008, Windows 7, or Windows Server 2008 R2](http://support.microsoft.com/kb/980915)   
 [The Cluster service takes about 30 seconds to fail over IPv6 IP addresses in Windows Server 2008 R2](http://support.microsoft.com/kb/2578113)   
 [Slow failover operation if no router exists between the cluster and an application server](http://support.microsoft.com/kb/2582281)  
  
  
