---
title: "Upgrade Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading databases"
  - "databases [Analysis Services], upgrading"
  - "installing Analysis Services, side-by-side installations"
  - "Analysis Services upgrades"
  - "Analysis Services upgrades, about upgrading Analysis Services"
  - "SSAS, database migration"
  - "upgrading Analysis Services"
  - "installing Analysis Services, upgrading"
  - "SSAS, upgrading"
ms.assetid: a131d329-386e-4470-aaa9-ffcde4e5ec0c
author: Minewiskan
ms.author: owend
manager: craigg
---
# Upgrade Analysis Services
  Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup to upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For detailed information on upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in SharePoint mode, see [Upgrade PowerPivot for SharePoint](upgrade-power-pivot-for-sharepoint.md). For more information about upgrading an existing SQL Server instance, see [Upgrade to SQL Server 2014 Using the Installation Wizard &#40;Setup&#41;](upgrade-sql-server-using-the-installation-wizard-setup.md).  
  
## Known Upgrade Issues  
 Before upgrading to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], review the following:  
  
-   [SQL Server 2014 Release Notes](https://go.microsoft.com/fwlink/?LinkID=296445).  
  
-   To learn which [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] features and functionality have been discontinued, deprecated, or changed see [Analysis Services Backward Compatibility](../../analysis-services/analysis-services-backward-compatibility.md).  
  
## Pre-Upgrade Checklist  
 Before upgrading, review the following information:  
  
-   [Supported Version and Edition Upgrades](supported-version-and-edition-upgrades.md)  
  
-   [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
-   [Check Parameters for the System Configuration Checker](check-parameters-for-the-system-configuration-checker.md)  
  
-   [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
-   [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
-   [Use Upgrade Advisor to Prepare for Upgrades](../../sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md)  
  
## Upgrading Analysis Services  
 You can choose from several approaches to upgrade server and data:  
  
-   An **in-place upgrade** replaces the existing program files with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] program files. Databases remain in the same location. Program folders are updated to reflect the new name.  
  
-   A **side-by-side upgrade** is a new installation of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on the same computer that has an existing Analysis Services instance. You can move databases over to the new instance on the same computer, and then uninstall the old version if you no longer use it.  
  
-   You can also install Analysis Services on new hardware and then migrate existing databases to that server.  
  
## In-place Upgrade  
 You can upgrade an existing instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and, as part of the upgrade process, automatically migrate existing databases from the old instance to the new instance. Because the metadata and binary data is compatible between the two versions, you will retain the data after you upgrade and you do not have to manually migrate the data.  
  
 To upgrade an existing instance, run Setup and specify the name of the existing instance as the name of the new instance.  
  
## Upgrading Databases  
 Databases that were created in previous versions of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] run on the upgraded server under an older database compatibility level setting. Databases created in the following versions, have a database compatibility level of 105. You can change the compatibility level if you want to use features that require a newer database compatibility level. Otherwise, you can run the databases on the upgraded server using the original settings. For more information, see [Set the Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services.md).  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md)   
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Understanding Microsoft OLAP Architecture](../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-architecture.md)   
 [Upgrade PowerPivot for SharePoint](upgrade-power-pivot-for-sharepoint.md)   
 [Install Analysis Services in Multidimensional and Data Mining Mode](../../sql-server/install/install-analysis-services-in-multidimensional-and-data-mining-mode.md)   
 [PowerPivot for SharePoint 2010 Installation](../../sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
  
