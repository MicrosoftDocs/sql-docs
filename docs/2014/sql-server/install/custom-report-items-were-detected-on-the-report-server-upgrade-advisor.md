---
title: "Custom report items were detected on the report server (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "custom report items, upgrading"
ms.assetid: aee32006-65b2-4dfe-9570-d85a249d17b2
author: markingmyname
ms.author: maghan
manager: craigg
---
# Custom report items were detected on the report server (Upgrade Advisor)
  Custom report items that were created for previous releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are not compatible with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Upgrade can continue, but reports that use the custom report item will not run as expected. Upgrade Advisor detected custom report items. Upgrade can continue, but you must manually move the custom report item files to the new installation folder after upgrade completes.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Upgrade Advisor detected custom [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extension settings in the configuration files, indicating that your installation includes one or more custom assemblies for reports.  
  
## Corrective Action  
 After upgrade completes, manually move the custom report item files to the new installation folder.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
