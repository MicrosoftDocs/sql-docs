---
title: "Always On availability groups: interoperability"
description: "Describes the different features that can and cannot function alongside an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "09/15/2021"
ms.service: sql
ms.subservice: availability-groups
ms.topic: reference
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], about"
  - "Availability Groups [SQL Server], interoperability"
---
# Always On availability groups: interoperability (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This topic documents interoperability of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features.

## <a name="Interop"></a> Features that Interoperate with Always On Availability Groups

The following table lists [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features that interoperate with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. A link in the **More Information** column indicates that interoperability considerations exist for a given feature.

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

- Cross-database transactions within the same SQL Server instance require [!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] SP2 and Windows Server 2016 or later, with some patching requirements. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md)
- Distributed transactions require [!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] SP2 and Windows Server 2012 R2 or later, with some patching requirements. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md)
- [Query statistics system data collector](../../../relational-databases/data-collection/system-data-collection-set-reports.md#Query) cannot reliably run in an environment with non-readable secondaries. To use query statistics system data collector, set all secondary availability group replicas to allow [read-access](configure-read-only-access-on-an-availability-replica-sql-server.md). 

## <a name="NoInterop"></a> Features that Do Not Interoperate with Always On Availability Groups

[!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] does not interoperate with the following features:

- Database mirroring. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).

## <a name="RelatedContent"></a> Related Content

- **Blogs:**

  [Migration Guide: Migrating to SQL Server 2012 Failover Clustering and Availability Groups from Prior Clustering and Mirroring Deployments](/archive/blogs/sqlalwayson/now-available-migration-guide-migrating-to-sql-server-2012-failover-clustering-and-availability-groups-from-prior-clustering-and-mirroring-deployments)
  [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)
  [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)

- **Whitepapers:**

  [Migration Guide: Migrating to Always On Availability Groups from Prior Deployments Combining Database Mirroring and Log Shipping](/previous-versions/sql/sql-server-2012/jj635217(v=msdn.10))
  [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))
  [Microsoft White Papers for SQL Server 2012](https://social.technet.microsoft.com/wiki/contents/articles/13146.white-paper-gallery-for-sql-server.aspx#[Category]SQLServer2012)
  [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)

## See Also

[Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
[Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)
