---
title: "Remote Blob Store (RBS) with availability groups"
description: "A description of how to use the Remote Blob Store (RBS) with databases that are part of an Always On availability group. "
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seodec18
---
# Use Remote Blob Store (RBS) with Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] can provide a high-availability and disaster recovery solution for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][Remote Blob Store (RBS)](../../../relational-databases/blob/remote-blob-store-rbs-sql-server.md) BLOB objects (blobs). [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] protects any RBS metadata and schemas stored in an availability database by replicating them to the secondary replicas. This is the SharePoint Content Database. Generally speaking, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stores this RBS metadata independently from the blob.  
  
 The protection for RBS BLOB data depends on the BLOB Store Location, as follows:  
  
|BLOB Store Location|Can Availability Groups Protect This BLOB Data?|  
|-------------------------|-----------------------------------------------------|  
|The same database that contains the RBS metadata  (stored using a RBS remote FILESTREAM provider)|Yes|  
|Another database in the same instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (stored using a RBS remote FILESTREAM provider)|Yes<br /><br /> We recommend that you put this database in the same availability group as the database that contains the RBS metadata.|  
|Another database in a different instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (stored using a RBS remote FILESTREAM provider)|Yes<br /><br /> This database must be in a separate availability group.|  
|A third-party BLOB store|No<br /><br /> To protect this BLOB data, use the high-availability mechanisms of the BLOB store provider.|  
  
##  <a name="Limitations"></a> Limitations  
  
-   RBS maintainers need to be targeted on the primary replica.  
  
##  <a name="Recommendations"></a> Recommendations  
  
-   Use an availability group listener. For more information, see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md).  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Maintaining Remote BLOB Store](https://msdn.microsoft.com/library/gg316773\(SQL.105\).aspx) (in [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] Books Online)  
  
-   [Running RBS Maintainer](/archive/blogs/sqlrbs/running-rbs-maintainer) (blog)  
  
-   [Configure Remote BLOB Storage (RBS) with the FILESTREAM provider (SharePoint 2010)](/archive/blogs/mvpawardprogram/configure-remote-blob-storage-rbs-with-the-filestream-provider-sharepoint-2010) (blog)  
  
## See Also  
 [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)   
 [Remote Blob Store &#40;RBS&#41; &#40;SQL Server&#41;](../../../relational-databases/blob/remote-blob-store-rbs-sql-server.md)  
  
