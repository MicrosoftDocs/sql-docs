---
title: "Microsoft SQL Server Reporting Services SharePoint Shared Service is Installed Side by Side (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 6ae1017e-129b-4702-9ea7-00ac9b024062
author: markingmyname
ms.author: maghan
manager: craigg
---
# Microsoft SQL Server Reporting Services SharePoint Shared Service is Installed Side by Side (Upgrade Advisor)
  Upgrade Advisor detected [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared service is installed side by side with a previous version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Upgrade Advisor detected [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Shared service is installed side by side with a previous version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that is not based on the SharePoint shared service architecture. Because the computer has both older and newer [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint related technologies installed side by side, upgrade is blocked.  
  
## Corrective Action  
 To continue with upgrade, you must uninstall one of the existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installations. After removing one of the installations of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], re-run Upgrade Advisor to confirm that there are no other upgrade issues.  
  
  
