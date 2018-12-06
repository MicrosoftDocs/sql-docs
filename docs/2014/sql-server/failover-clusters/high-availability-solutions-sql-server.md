---
title: "High Availability Solutions (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "high availability [SQL Server], solutions"
  - "Database Engine [SQL Server], availability"
  - "database availability [SQL Server]"
  - "availability [SQL Server]"
  - "server availability [SQL Server]"
ms.assetid: b2eda634-0f8e-4703-801b-7ba895544ff5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# High Availability Solutions (SQL Server)
  This topic introduces several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] high-availability solutions that improve the availability of servers or databases. A high-availability solution masks the effects of a hardware or software failure and maintains the availability of applications so that the perceived downtime for users is minimized.  
  
> [!NOTE]  
>  For information about which editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support a given high availability solution, see the "High Availability (AlwaysOn)" section of [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
(#RecommendedSolutions)  
  
##  <a name="TermsAndDefinitions"></a> Overview of SQL Server High-Availability Solutions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides several options for creating high availability for a server or database. The high-availability options include the following:  
  
 AlwaysOn Failover Cluster Instances  
 As part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] AlwaysOn offering, AlwaysOn Failover Cluster Instances leverages Windows Server Failover Clustering (WSFC) functionality to provide local high availability through redundancy at the server-instance level-a *failover cluster instance* (FCI). An FCI is a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is installed across Windows Server Failover Clustering (WSFC) nodes and, possibly, across multiple subnets. On the network, an FCI appears to be an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on a single computer, but the FCI provides failover from one WSFC node to another if the current node becomes unavailable.  
  
 For more information, see [ AlwaysOn Failover Cluster Instances (SQL Server)](windows/always-on-failover-cluster-instances-sql-server.md).  
  
 [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]  
 [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] is an enterprise-level high-availability and disaster recovery solution introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to enable you to maximize availability for one or more user databases. [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] requires that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances reside on Windows Server Failover Clustering (WSFC) nodes. For more information, see [ AlwaysOn Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md).  
  
> [!NOTE]  
>  An FCI can leverage [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] to provide remote disaster recovery at the database level. For more information, see [Failover Clustering and AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md).  
  
 Database mirroring  
 > [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 Database mirroring is a solution to increase database availability by supporting almost instantaneous failover. Database mirroring can be used to maintain a single standby database, or *mirror database*, for a corresponding production database that is referred to as the *principal database*. For more information, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
 Log shipping  
 Like [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] and database mirroring, log shipping operates at the database level. You can use log shipping to maintain one or more warm standby databases (referred to as *secondary databases*) for a single production database that is referred to as the *primary database*. For more information about log shipping, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
##  <a name="RecommendedSolutions"></a> Recommended Solutions for Using SQL Server to Protect Data  
 Our recommendation for providing data protection for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment are as follows:  
  
-   For data protection through a third-party shared disk solution (a SAN), we recommend that you use AlwaysOn Failover Cluster Instances.  
  
-   For data protection through [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], we recommend that you use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].  
  
    > [!NOTE]  
    >  If you are running an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that does not support [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], we recommend log shipping. For information about which editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], see the "High Availability (AlwaysOn)" section of [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## See Also  
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [Database Mirroring: Interoperability and Coexistence &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-interoperability-and-coexistence-sql-server.md)   
 [Deprecated Database Engine Features in SQL Server 2014](../../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)  
  
  
