---
title: "Install SQL Server 2014 BI Features"
ms.custom: ""
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.topic: conceptual
author: markingmyname
ms.author: maghan
manager: kfile
ms.date: "10/24/2018"
ms.technology: install
---

# Install SQL Server 2014 BI Features

  SQL Server features that are part of the Microsoft Business Intelligence platform include [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and several client applications used for creating or working with analytical data. This section of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup documentation explains how to install these features.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can be installed as standalone servers, in scale-out configurations, or as shared service applications in a SharePoint farm. Installing the services in a farm enables BI features that are only available in SharePoint, including [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint and [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] ad hoc interactive report designer that runs on [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] or [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular model databases.  
  
 If you are already familiar with the installation steps for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], or PowerPivot for SharePoint, you can skip ahead to the checklists for guidance on how to enable specific scenarios. For more information, see [Checklists for Installing BI Features with SharePoint](checklists-for-installing-bi-features-with-sharepoint.md).  
  
## Contents

In this section:
  
|Link|Task|  
|----------|----------|  
|[Checklists for Installing BI Features with SharePoint](checklists-for-installing-bi-features-with-sharepoint.md)|If you already know what you want to install and are familiar with the installation steps for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], or [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, you can use the checklists in this section for guidance on installation order, account and permission requirements, and steps deploying advanced topologies, including multi-server and multi-feature deployments.|  
|[Install SQL Server BI Features with SharePoint &#40;PowerPivot and Reporting Services&#41;](install-sql-server-bi-features-sharepoint-powerpivot-reporting-services.md)|This section explains how to install SQL Server features in a SharePoint environment. It identifies which SQL Server features are available given a specific version and edition of SharePoint. It also includes installation procedures for PowerPivot for SharePoint and Reporting Services in SharePoint mode.|  
|[Install Analysis Services in Multidimensional and Data Mining Mode](install-analysis-services-in-multidimensional-and-data-mining-mode.md)<br /><br /> [Install Analysis Services in Tabular Mode](../../analysis-services/instances/install-windows/install-analysis-services.md)<br /><br /> [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)<br /><br /> [Install Integration Services](../../integration-services/install-windows/install-integration-services.md)<br /><br /> [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)<br /><br /> [Install Reporting Services Native Mode Report Server](../../reporting-services/install-windows/install-reporting-services-native-mode-report-server.md)|This section provides instructions for installing Analysis Services, Integration Services, Master Data Services, and Reporting Services, where Analysis Services and Reporting Services are installed without SharePoint. This is sometimes referred to as *native mode*, and it is the most common installation scenario for both Reporting Services and Analysis Services. In this section, you will learn about installation options that directly determine the operational context of your server. For Reporting Services, this might be a server that is pre-configured or one that requires multiple configuration steps before you can use it. For Analysis Services, the installation options you select will determine the type of project you can deploy to the server.|  
|[Verify or Troubleshoot SQL Server BI Feature Installation Problems](../../../2014/sql-server/install/verify-or-troubleshoot-sql-server-bi-feature-installation-problems.md)|This section includes steps for verifying an installation. It also provides links to problem resolution information on the web.|  
  
## Related content  
  
|Link|Task|  
|----------|----------|  
|[Upgrade to SQL Server 2014](../../database-engine/install-windows/upgrade-sql-server.md)<br /><br /> [Upgrade Analysis Services](../../database-engine/install-windows/upgrade-analysis-services.md)<br /><br /> [Upgrade PowerPivot for SharePoint](../../database-engine/install-windows/upgrade-power-pivot-for-sharepoint.md)<br /><br /> [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)|Use the instructions in this section to upgrade servers and content from a previous release to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
|[Uninstall SQL Server 2014](uninstall-sql-server.md)<br /><br /> [Uninstall PowerPivot for SharePoint](../../../2014/sql-server/install/uninstall-power-pivot-for-sharepoint.md)<br /><br /> [Uninstall Reporting Services](../../../2014/sql-server/install/uninstall-reporting-services.md)|Use the instructions in this section to uninstall BI features.|  
  
## See Also

* [What's New &#40;Reporting Services&#41;](../../../2014/reporting-services/what-s-new-reporting-services.md)

* [What's New in Analysis Services and Business Intelligence](../../analysis-services/what-s-new-in-analysis-services.md)

* [Install SQL Server 2014](../../database-engine/install-windows/install-sql-server.md)

* [Upgrade to SQL Server 2014](../../database-engine/install-windows/upgrade-sql-server.md)
