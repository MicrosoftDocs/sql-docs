---
title: Discontinued Functionality
author: markingmyname
ms.author: maghan
manager: kfile
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.prod: reporting-services-2014, sql-server-2014
ms.prod_service: reporting-services-native, reporting-services-sharepoint 
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/14/2018
---

# Discontinued Functionality in SQL Server Reporting Services (SSRS)

  This topic describes [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features that are no longer available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. It does not include announcements about discontinued support for specific versions of the operating system or [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Information Services (IIS). For more information about system prerequisites, see [Hardware and Software Requirements for Installing SQL Server 2014](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
 In this topic:  
  
- [SQL Server 2014 Reporting Services Discontinued Functionality](#bkmk_sql14)  
  
- [SQL Server 2012 Reporting Services Discontinued Functionality](#bkmk_rc0)  
  
- [SQL Server 2008 R2 Reporting Services Discontinued Functionality](#bkmk_kj)  
  
##  <a name="bkmk_sql14"></a> [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Reporting Services Discontinued Functionality

 No [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features were discontinued in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
##  <a name="bkmk_rc0"></a> [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Reporting Services Discontinued Functionality

 This section describes discontinued [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] functionality in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
 No [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features were discontinued in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
##  <a name="bkmk_kj"></a> SQL Server 2008 R2 Reporting Services Discontinued Functionality

 This section describes discontinued in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
> [!NOTE]  
> Because SQL Server 2008 R2 is a minor version upgrade of SQL Server 2008, we recommend that you also review the content in the SQL Server 2008 section.
  
### 64-bit Platform Support

 Starting in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)], the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] component no longer supports Itanium-based servers running Windows Server 2003 or Windows Server 2003 R2. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] continues to support other 64-bit operating systems, including Windows Server 2008 for Itanium-Based Systems and Windows Server 2008 R2 for Itanium-Based Systems. To upgrade to [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] from a [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] installation with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] on an Itanium-based system edition of Windows Server 2003 or Windows Server 2003 R2, you must first upgrade the operating system.  
  
### Data Source Credentials in URL Access

 The URL access parameter strings *dsu:datasourcename=value* and *dsp:datasourcename=value* are now discontinued. In prior versions, these parameter strings are stored in plain text in the browser cache, which is not secure.  
  
## Next steps

 - [What's New &#40;Reporting Services&#41;](what-s-new-reporting-services.md)
 - [Behavior Changes to SQL Server Reporting Services  in SQL Server 2014](behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)
 - [Deprecated Features in SQL Server Reporting Services in SQL Server 2014](deprecated-features-in-sql-server-reporting-services-ssrs.md)