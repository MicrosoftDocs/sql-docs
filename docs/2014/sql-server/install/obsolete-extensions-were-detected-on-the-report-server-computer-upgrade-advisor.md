---
title: "Obsolete extensions were detected on the report server computer (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "report servers [Reporting Services], upgrade issues"
ms.assetid: 40d245a2-0631-470e-81b3-1feb47e028cb
author: markingmyname
ms.author: maghan
manager: craigg
---
# Obsolete extensions were detected on the report server computer (Upgrade Advisor)
  Upgrade Advisor detected one or more rendering extensions that are no longer available in the current release.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 The report server is configured to use one or more extensions that have been discontinued in this release. Discontinued extensions include:  
  
-   HTML OWC rendering extension  
  
-   HTML 3.2 rendering extension  
  
 Upgrade can continue, but the unsupported functionality will no longer be available on the upgraded report server.  
  
 If you require these extensions, do not upgrade until you find an alternate solution to these requirements. You might need to obtain or build a custom rendering extension that provides the same or similar functionality.  
  
## Corrective Action  
 Evaluate the current set of features that are included in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to determine whether supported functionality meets your requirements.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
