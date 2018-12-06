---
title: "Upgrade SQL Server Management Tools | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "management tools, upgrading"
ms.assetid: 1dab50b9-d16c-49a1-9ecc-af72adb6c378
author: "stevestein"
ms.author: "sstein"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Upgrade SQL Server Management Tools

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports upgrade from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later. This article documents support and behavior for upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Tools and management components such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, Database Mail, Maintenance Plans, XPStar, and XPWeb.  
  
> [!IMPORTANT]  
>  For local installations, you must run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup as an administrator. If you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup from a remote share, you must use a domain account that has read and execute permissions on the remote share.  
  
## Known Upgrade Issues  
Consider the following issues before you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]:  
  
### For all upgrade scenarios:  
  
- All TSX servers should be upgraded before the MSX server is upgraded. For more information about MSX/TSX in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md).  
  
-   All components in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be upgraded at the same time. Version numbers of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components must be the same in an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   You can add components to an existing installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the time that you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Upgrade SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Client Tools, such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor, sqlcmd, and osql are not upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Instead, Client Tools run side-by-side with tools from previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports importing settings from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Client Tools.  
  
-   Authentication from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be updated from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to Windows Authentication during upgrade. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is not supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   Data for jobs and alerts will be preserved during upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   If SQLMail is being used in the instance to be upgraded, associated XPs will be supported and enabled after the upgrade. Otherwise, they will be off.  
  
-   Database Mail, also known as SQLiMail, will be upgraded with the [!INCLUDE[ssDE](../../includes/ssde-md.md)] component of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. By default, Database Mail will be off after upgrade. Any schema updates should be reconciled with an update script after upgrade.  
  
## See Also  
 [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)   
 [Backward Compatibility_deleted](https://msdn.microsoft.com/library/15d9117e-e2fa-4985-99ea-66a117c1e9fd)   
 [Upgrade SQL Server Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
  
  
