---
title: "Always On Availability Groups: Interoperability (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
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
# Always On Availability Groups: Interoperability (SQL Server)
  This topic documents interoperability of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] with other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  

  
##  <a name="Interop"></a> Features That Interoperate with AlwaysOn Availability Groups  
 The following table lists [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features that interoperate with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. A link in the **More Information** column indicates that interoperability considerations exist for a given feature.  
  
|Feature|More Information|  
|-------------|----------------------|  
|Change data capture|[Replication, Change Tracking, Change Data Capture, and AlwaysOn Availability Groups &#40;SQL Server&#41;](replicate-track-change-data-capture-always-on-availability.md)|  
|Change tracking|[Replication, Change Tracking, Change Data Capture, and AlwaysOn Availability Groups &#40;SQL Server&#41;](replicate-track-change-data-capture-always-on-availability.md)|  
|Contained databases|[Contained Databases with AlwaysOn Availability Groups (SQL Server)](always-on-availability-groups-sql-server.md)|  
|Database encryption|[Encrypted Databases with AlwaysOn Availability Groups &#40;SQL Server&#41;](encrypted-databases-with-always-on-availability-groups-sql-server.md)|  
|Database snapshots|[Database Snapshots with AlwaysOn Availability Groups &#40;SQL Server&#41;](database-snapshots-with-always-on-availability-groups-sql-server.md)|  
|FILESTREAM and FileTable|[FILESTREAM and FileTable with AlwaysOn Availability Groups &#40;SQL Server&#41;](filestream-and-filetable-with-always-on-availability-groups-sql-server.md)|  
|Full-text search|Note: Full-Text indexes are synchronized with AlwaysOn secondary databases.|  
|Log shipping|[Prerequisites for Migrating from Log Shipping to AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-migrating-log-shipping-to-always-on-availability-groups.md)|  
|Remote Blob Store (RBS)|[Remote Blob Store &#40;RBS&#41; and AlwaysOn Availability Groups &#40;SQL Server&#41;](remote-blob-store-rbs-and-always-on-availability-groups-sql-server.md)|  
|Replication[Configure Replication for AlwaysOn Availability Groups (SQL Server)](configure-replication-for-always-on-availability-groups-sql-server.md)<br /><br /> [Maintaining an AlwaysOn Publication Database &#40;SQL Server&#41;](maintaining-an-always-on-publication-database-sql-server.md)<br /><br /> [Replication, Change Tracking, Change Data Capture, and AlwaysOn Availability Groups &#40;SQL Server&#41;](replicate-track-change-data-capture-always-on-availability.md)<br /><br /> [Replication Subscribers and AlwaysOn Availability Groups &#40;SQL Server&#41;](replication-subscribers-and-always-on-availability-groups-sql-server.md)|  
|Analysis Services|[Analysis Services with AlwaysOn Availability Groups](analysis-services-with-always-on-availability-groups.md)|  
|Reporting Services|Utilize read only secondary replicas as a reporting data source and reduce the load on your primary read-write replica.<br /><br /> [Reporting Services with AlwaysOn Availability Groups &#40;SQL Server&#41;](reporting-services-with-always-on-availability-groups-sql-server.md)|  
|Service Broker|[Service Broker with AlwaysOn Availability Groups &#40;SQL Server&#41;](service-broker-with-always-on-availability-groups-sql-server.md)|  
|SQL Server Agent||  
  
##  <a name="NoInterop"></a> Features that Do Not Interoperate with AlwaysOn Availability Groups  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] does not interoperate with the following features:  
  
-   Cross-database transactions/distributed transactions  
  
     For information about why such transactions are not supported, see [Cross-Database Transactions Not Supported For Database Mirroring or AlwaysOn Availability Groups &#40;SQL Server&#41;](transactions-always-on-availability-and-database-mirroring.md).  
  
-   Database mirroring  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Migration Guide: Migrating to SQL Server 2012 Failover Clustering and Availability Groups from Prior Clustering and Mirroring Deployments](https://blogs.msdn.com/b/sqlalwayson/archive/2012/04/09/now-available-migration-guide-migrating-to-sql-server-2012-failover-clustering-and-availability-groups-from-prior-clustering-and-mirroring-deployments.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Whitepapers:**  
  
     [Migration Guide: Migrating to AlwaysOn Availability Groups from Prior Deployments Combining Database Mirroring and Log Shipping](https://msdn.microsoft.com/library/jj635217)  
  
     [Microsoft SQL Server AlwaysOn Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
