---
title: "Virtual directories are unspecified (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "virtual directories [Reporting Services]"
ms.assetid: 7d32b560-49d6-4558-b5d6-9127067f82d6
author: markingmyname
ms.author: maghan
manager: craigg
---
# Virtual directories are unspecified (Upgrade Advisor)
  Upgrade Advisor did not detect virtual directory settings for the Report Server Web service or Report Manager. After upgrade completes, you must configure URL reservations for the report server by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Upgrading a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation includes reserving new URLs for the Report Server Web service and Report Manager. Upgrade Advisor did not detect virtual directories for the report server or Report Manager for the instance to be upgraded, and therefore the upgrade process has insufficient information to create URL reservations for the upgraded report server. Upgrade can continue, but the report server or Report Manager virtual directory will be undefined after the upgraded installation.  
  
## Corrective Action  
 After upgrade finishes, use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to set the URLs for report server and Report Manager. Use IIS Manager to remove any virtual directories you no longer need.  
  
 For more information, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
