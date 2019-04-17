---
title: "Always On availability groups: interoperability"
description: "Describes the different features that can and cannot function alongside an Always On availability group."
ms.custom: "seodec18"
ms.date: "04/20/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], about"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: daf87f90-2623-42ca-912c-b8f07d210510
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Always On availability groups: interoperability (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic documents interoperability of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].

## <a name="Interop"></a> Features that Interoperate with Always On Availability Groups

The following table lists [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features that interoperate with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. A link in the **More Information** column indicates that interoperability considerations exist for a given feature.

|Feature|More Information|
|:------|:---------------|
|Change data capture|[Replication, Change Tracking, Change Data Capture, and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replicate-track-change-data-capture-always-on-availability.md)|
|Change tracking|[Replication, Change Tracking, Change Data Capture, and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replicate-track-change-data-capture-always-on-availability.md)|
|Contained databases|[Contained Databases with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/contained-databases-with-always-on-availability-groups-sql-server.md)|
|Database encryption|[Encrypted Databases with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/encrypted-databases-with-always-on-availability-groups-sql-server.md)|
|Database snapshots|[Database Snapshots with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/database-snapshots-with-always-on-availability-groups-sql-server.md)|
|FILESTREAM and FileTable|[FILESTREAM and FileTable with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/filestream-and-filetable-with-always-on-availability-groups-sql-server.md)|
|Full-text search|Note: Full-Text indexes are synchronized with Always On secondary databases.|
|Log shipping|[Prerequisites for Migrating from Log Shipping to Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md)|
|Remote Blob Store (RBS)|[Remote Blob Store &#40;RBS&#41; and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remote-blob-store-rbs-and-always-on-availability-groups-sql-server.md)|
|Replication|[Configure Replication for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server.md)<br /><br /> [Maintaining an Always On Publication Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/maintaining-an-always-on-publication-database-sql-server.md)<br /><br /> [Replication, Change Tracking, Change Data Capture, and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replicate-track-change-data-capture-always-on-availability.md)<br /><br /> [Replication Subscribers and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replication-subscribers-and-always-on-availability-groups-sql-server.md)|
|Analysis Services|[Analysis Services with Always On Availability Groups](../../../database-engine/availability-groups/windows/analysis-services-with-always-on-availability-groups.md)|
|Reporting Services|Utilize read only secondary replicas as a reporting data source and reduce the load on your primary read-write replica.<br /><br /> [Reporting Services with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/reporting-services-with-always-on-availability-groups-sql-server.md)|
|Service Broker|[Service Broker with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/service-broker-with-always-on-availability-groups-sql-server.md)|
|SQL Server Agent|&nbsp;|

## <a name="restrictions"></a> Features that Interoperate with Always On Availability Groups with Restrictions

The following features interoperate with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with specific restrictions. See the linked topics for details.

- Cross-database transactions/distributed transactions ([!INCLUDE[ssSQL15](../../../includes/sssql15-md.md)] and Windows Server 2016). For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md)

## <a name="NoInterop"></a> Features that Do Not Interoperate with Always On Availability Groups

[!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] does not interoperate with the following features:

- Database mirroring. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).

## <a name="RelatedContent"></a> Related Content

- **Blogs:**

  [Migration Guide: Migrating to SQL Server 2012 Failover Clustering and Availability Groups from Prior Clustering and Mirroring Deployments](https://blogs.msdn.microsoft.com/sqlalwayson/2012/04/09/now-available-migration-guide-migrating-to-sql-server-2012-failover-clustering-and-availability-groups-from-prior-clustering-and-mirroring-deployments/)
  [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)
  [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)

- **Whitepapers:**

  [Migration Guide: Migrating to Always On Availability Groups from Prior Deployments Combining Database Mirroring and Log Shipping](https://msdn.microsoft.com/library/jj635217)
  [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)
  [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)
  [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)

## See Also

[Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
[Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)
