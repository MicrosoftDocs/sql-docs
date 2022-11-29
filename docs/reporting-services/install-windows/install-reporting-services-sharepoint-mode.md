---
description: "Install Reporting Services 2016 in SharePoint mode"
title: "Install Reporting Services 2016 in SharePoint mode | Microsoft Docs"
ms.date: 12/20/2017
ms.service: reporting-services
ms.topic: conceptual
helpviewer_keywords:
  - "default configuration [Reporting Services]"
  - "installing Reporting Services, SharePoint integrated mode"
  - "installation options [Reporting Services]"
ms.assetid: ac6cba68-2665-4a39-8fa3-cb7d7e6723c0
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
ms.custom:
  - intro-installation
---
# Install Reporting Services 2016 in SharePoint mode

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

SQL Server Reporting Services in SharePoint, enables report creation and viewing in document libraries, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription delivery of reports through email,  Power View, data alerting, and report management features, all in a deployment of based of [!INCLUDE[msCoName](../../includes/msconame-md.md)] SharePoint. For more information regarding features in SharePoint mode, see the section "Feature Support and Behavior Differences by Server Mode" in [Reporting Services Report Server](../../reporting-services/report-server-sharepoint/reporting-services-report-server.md).

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016. Power View support is no longer available after SQL Server 2017.

There are two core [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components to install for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode:  

|Installation|Description|  
|------------------|-----------------|  
|**Report Server:** The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server installed in SharePoint Mode|The report server handles the data and report processing and rendering as well subscription and Data Alert processing. The SharePoint mode report server is designed and installed as a SharePoint Shared Service.<br /><br /> **How:** Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media to install the report server.|  
|**Add-in:** The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products, **rssharepoint.msi**.|The add-in installs the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] user interface (UI) pages and features on a SharePoint web front-end server. The UI features include Power View, administration pages in SharePoint Central Administration, feature pages used within SharePoint document libraries, and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Data Alerting pages.<br /><br /> **How:**  The add-in can be installed from either a Web download or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. For more information,  see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).|  
  
## In this section

 [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2016&#41;](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
 [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)  
  
 [Install The First Report Server in SharePoint Mode](../../reporting-services/install-windows/install-the-first-report-server-in-sharepoint-mode.md)  
  
 [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
 [Add an Additional Report Server to a Farm &#40;SSRS Scale-out&#41;](../../reporting-services/install-windows/add-an-additional-report-server-to-a-farm-ssrs-scale-out.md)  
  
 [Add an Additional Reporting Services Web Front-end to a Farm](../../reporting-services/install-windows/add-an-additional-reporting-services-web-front-end-to-a-farm.md)  
  
 [Configure E-mail for a Reporting Services Service Application &#40;SharePoint 2013 and SharePoint 2016&#41;](./configure-e-mail-for-a-reporting-services-service-application.md)  
  
 [Provision Subscriptions and Alerts for SSRS Service Applications](../../reporting-services/install-windows/provision-subscriptions-and-alerts-for-ssrs-service-applications.md)  
  
 [Claims to Windows Token Service &#40;c2WTS&#41; and Reporting Services](../../reporting-services/install-windows/claims-to-windows-token-service-c2wts-and-reporting-services.md)  

## Next steps

 [Data Alerts Architecture and Workflow](../../reporting-services/reporting-services-data-alerts.md#AlertingWF)   
 [Data Alert Manager for Alerting Administrators](../../reporting-services/data-alert-manager-for-alerting-administrators.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
