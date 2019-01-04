---
title: "Deprecated Master Data Services Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: d8506bda-66dd-45a4-bfc9-3a10fa665acc
author: leolimsft
ms.author: lle
manager: craigg
---
# Deprecated Master Data Services Features in SQL Server 2014
  This topic describes the deprecated [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] features that are still available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. These features are scheduled to be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Deprecated features should not be used in new applications.  
  
## Staging Process  
 The staging process that was used in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] is no longer available in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application; however it is still available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
 Staging errors from the [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] staging process are no longer displayed in the UI. Error codes that are populated during the staging process are still available in the staging tables, and can be found here: [https://msdn.microsoft.com/library/ff487022.aspx](https://msdn.microsoft.com/library/ff487022.aspx).  
  
 The staging tables (tblStgMember, tblStgMemberAttribute, and tblStgRelationship) are still available in the database. The stored procedure used to initiate the staging process (mdm.udpStagingSweep) is still available in the database.  
  
 The web service methods that call the staging process are still available.  
  
 The staging interval set in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] applies to the staging process in both [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] and [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
 A new, higher performance staging process has been implemented in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For more information, see [Data Import &#40;Master Data Services&#41;](overview-importing-data-from-tables-master-data-services.md).  
  
## Metadata  
 Though the Metadata model is still displayed in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application, it should not be used. It will be removed in a future release. Users can also no longer view metadata in the **Explorer** functional area, and you can no longer create versions of the Metadata model.  
  
## See Also  
 [Discontinued Master Data Services Features in SQL Server 2014](discontinued-master-data-services-features.md)  
  
  
