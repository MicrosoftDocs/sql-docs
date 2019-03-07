---
title: "Virtual directory has unsupported authentication method (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "virtual directories [Reporting Services]"
ms.assetid: 216eca6f-9a66-42e1-aa54-dcf99cec9f7d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Virtual directory has unsupported authentication method (Upgrade Advisor)
  Upgrade Advisor detected an unsupported authentication method on the Report Manager or report server virtual directory. Authentication methods that are not supported by upgrade include Anonymous, Digest, and .NET Passport.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Setup cannot upgrade a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation that uses one of the following authentication methods  
  
-   Anonymous  
  
-   Digest  
  
-   .NET Passport  
  
 To continue, you can either modify the Authentication method that is specified for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] virtual directories in Internet Information Services (IIS), or you can migrate your installation. For more information about migrating your installation, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Corrective Action  
 To continue with upgrade, modify the Authentication method in IIS for the ReportServer and Reports virtual directories. For more information about modifying Authentication methods in IIS, see the IIS documentation. After you modify the Authentication method for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] virtual directories, re-run Upgrade Advisor to confirm that there are no other upgrade issues.  
  
## See Also  
 [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
  
