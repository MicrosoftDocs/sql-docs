---
title: "ISAPI filters detected on the report server site (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "ISAPI filters"
  - "report servers [Reporting Services], upgrade issues"
ms.assetid: dd30560d-9e16-47c7-ba68-a9743a657e4e
author: markingmyname
ms.author: maghan
manager: craigg
---
# ISAPI filters detected on the report server site (Upgrade Advisor)
  Upgrade Advisor detected one or more ISAPI filters on the Web site that hosts the report server and Report Manager virtual directories. ISAPI filters are not supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native .|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Before upgrading, verify whether the ISAPI filters on the Web site are used by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications. If you do not require the ISAPI filter, you can upgrade the report server. Setup will create the default URLs, without support for the ISAPI filter that runs in IIS. If you require the ISAPI filter, do not upgrade until you find an alternative way of hosting the ISAPI filter (for example, using ISA Server or continuing to host the ISAPI filter in IIS). The report server supports ASP.NET HTTPModules as a replacement for ISAPI filters in certain scenarios. For more information, see the ASP.NET documentation on MSDN.  
  
## Corrective Action  
 Evaluate and use a separate solution for hosting ISAPI filters required by your deployment.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
