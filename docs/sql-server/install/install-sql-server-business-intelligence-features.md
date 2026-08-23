---
title: Install Business Intelligence Features
description: This article provides links to information to install SQL Server features that are part of the Microsoft Business Intelligence platform.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 12/13/2019
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
---

# Install SQL Server Business Intelligence Features

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

  SQL Server features that are part of the Microsoft Business Intelligence platform include [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and several client applications used for creating or working with analytical data. This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install these features.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can be installed as standalone servers, in scale-out configurations, or as shared service applications in a SharePoint farm. Installing the services in a farm enables BI features that are only available in SharePoint, including [!INCLUDE[power-pivot-md](../../includes/power-pivot-md.md)] for SharePoint and Power View, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] ad hoc interactive report designer that runs on [!INCLUDE[power-pivot-md](../../includes/power-pivot-md.md)] or [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] tabular model databases. 

 > [!NOTE]
 > Reporting Services integration with SharePoint is no longer available after SQL Server 2016. Power View support is no longer available after SQL Server 2017.
  
## SQL Server BI Features  
 All SQL Server features, including the BI components, are installed through SQL Server Setup. The following links provide supplemental information specific to each BI feature.  
  
-   [Install Analysis Services](/analysis-services/instances/install-windows/install-analysis-services)  
  
-   [Install Analysis Services in Power Pivot Mode](/analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode)  
  
-   [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)  
  
-   [Install Integration Services](../../integration-services/install-windows/install-integration-services.md)  
  
-   [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)  
  
-   [Install Reporting Services](../../reporting-services/install-windows/install-reporting-services.md)  
  
-   [Install Reporting Services SharePoint Mode](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  

> [!NOTE]
> SQL Server Data Tools (SSDT) is not included with SQL Server 2016. [Download SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md).
  
## Related content

- [What's new in SQL Server Reporting Services (SSRS)](../../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md)
- [What's New in Analysis Services](/analysis-services/what-s-new-in-analysis-services)
- [What's New in Integration Services](../../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md)
- [What's New in Master Data Services (MDS)](../../master-data-services/what-s-new-in-master-data-services-mds.md)
- [SQL Server installation guide](../../database-engine/install-windows/install-sql-server.md)
- [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
