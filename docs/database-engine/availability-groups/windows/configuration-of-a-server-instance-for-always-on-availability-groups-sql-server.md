---
title: "Enable the availability group feature for a SQL Server instance"
description: "Describes how to enable the Always On availability group feature for an instance of SQL Server."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], server instance"
  - "Availability Groups [SQL Server], about"
---
# Enable the Always On availability group feature for a SQL Server instance
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic contains information about the requirements for configuring an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  For essential information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites and restrictions for Windows Server Failover Clustering (WSFC) nodes and for instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
   
##  <a name="TermsAndDefinitions"></a> Terms and Definitions  
 [Always On Availability Groups](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
 A high-availability and disaster-recovery solution that provides an enterprise-level replacement for database mirroring. An *availability group* supports a failover environment for a discrete set of user databases, known as *availability databases*, that fail over together.  
  
 availability replica  
 An instantiation of an availability group that is hosted by a specific instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and that maintains a local copy of each availability database that belongs to the availability group. Two types of availability replicas exist: a single *primary replica* and one to four *secondary replicas*. The server instances that host the availability replicas for a given availability group must reside on different nodes of a single Windows Server Failover Clustering (WSFC) cluster.  
  
 [database mirroring endpoint](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
 An endpoint is a SQL Server object that enables SQL Server to communicate over the network. To participate in database mirroring and/or [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] a server instance requires a special, dedicated endpoint. All mirroring and availability group connections on a server instance use the same database mirroring endpoint. This endpoint is a special-purpose endpoint used exclusively to receive these connections from other server instances.  
  
##  <a name="ConfigSI"></a> To Configure a Server Instance to Support Always On Availability Groups  
 To support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], a server instance must reside on a node in the WSFC failover cluster that hosts the availability group, be [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] enabled, and possess a database mirroring endpoint.  
  
1.  Enable the Always On Availability Groups feature on every server instance that is to participate in one or more availability groups. A given server instance can host only a single availability replica for a given availability group.  
  
2.  Ensure that the server instance possesses a database mirroring endpoint.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To enable Always On Availability Groups**  
  
-   [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
  
 **To determine whether a database mirroring endpoint exists**  
  
-   [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)  
  
 **To create a database mirroring endpoint**  
  
-   [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))  
  
     [Microsoft White Papers for SQL Server 2012](https://social.technet.microsoft.com/wiki/contents/articles/13146.white-paper-gallery-for-sql-server.aspx#[Category]SQLServer2012)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Always On Availability Groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)   
 [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)  
  
