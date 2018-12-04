---
title: "Microsoft SharePoint 2007 is Installed (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 6f1da295-d9b7-4948-99d3-ebd3587337c6
author: markingmyname
ms.author: maghan
manager: craigg
---
# Microsoft SharePoint 2007 is Installed (Upgrade Advisor)
  Upgrade Advisor detected an unsupported version of a SharePoint product or technology.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will not upgrade or install on SharePoint 2007. Upgrade is blocked.  
  
## Corrective Action  
 To continue with upgrade, you must either uninstall SharePoint 2007 or upgrade SharePoint 2007 to a SharePoint 2010 product. After you have updated your SharePoint installation, re-run Upgrade Advisor to confirm that there are no other upgrade issues.  
  
 It is not possible to directly upgrade from SharePoint 2007 to SharePoint 2013. but you can do what is referred to as a "double-hop" database attach to upgrade from Office SharePoint Server 2007 to SharePoint Server 2010, and then from SharePoint Server 2010 to SharePoint Server 2013.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
