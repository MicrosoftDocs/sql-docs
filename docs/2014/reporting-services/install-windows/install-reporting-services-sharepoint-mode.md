---
title: "Reporting Services SharePoint Mode Installation (SharePoint 2010 and SharePoint 2013) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "default configuration [Reporting Services]"
  - "installing Reporting Services, SharePoint integrated mode"
  - "installation options [Reporting Services]"
ms.assetid: ac6cba68-2665-4a39-8fa3-cb7d7e6723c0
author: markingmyname
ms.author: maghan
manager: kfile
---
# Reporting Services SharePoint Mode Installation (SharePoint 2010 and SharePoint 2013)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode is a collection of server components that provide report generation and delivery, based on [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] SharePoint products.  
  
 Running [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode provides the [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] and data alerting features. For more information regarding features in SharePoint mode, see the section "Feature Support and Behavior Differences by Server Mode" in [Reporting Services Report Server](../reporting-services-report-server.md)  
  
 There are two fundamental installations needed for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode:  
  
|Installation|Description|  
|------------------|-----------------|  
|The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products.|The add-in installs the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] user interface (UI) pages and features on a SharePoint web front-end server. The UI features include [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], administration pages in SharePoint Central Administration, feature pages used within SharePoint document libraries, and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Data Alerting pages.|  
|The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server installed in SharePoint Mode|The report server handles the data and report processing and rendering as well subscription and Data Alert processing. The SharePoint mode report server is architected and installed as a SharePoint Shared Service.|  
  
 To install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation media.  
  
 For instructions on advanced deployment scenarios, see [Deployment Checklist: Reporting Services, Power View, and PowerPivot for SharePoint](../../sql-server/install/deployment-checklist-reporting-services-power-view-power-pivot-for-sharepoint.md) and [Deployment Checklist: Install Reporting Services into an Existing SharePoint Farm](../../sql-server/install/deployment-checklist-install-reporting-services-existing-sharepoint-farm.md).  
  
## In This Section  
 [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2014&#41;](supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
 [Install Reporting Services SharePoint Mode for SharePoint 2013](../../sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2013.md)  
  
 [Install Reporting Services SharePoint Mode for SharePoint 2010](../../sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)  
  
 [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
 [Where to find the Reporting Services add-in for SharePoint Products](where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)  
  
 [Add an Additional Report Server to a Farm &#40;SSRS Scale-out&#41;](add-an-additional-report-server-to-a-farm-ssrs-scale-out.md)  
  
 [Add an Additional Reporting Services Web Front-end to a Farm](add-an-additional-reporting-services-web-front-end-to-a-farm.md)  
  
 [Configure E-mail for a Reporting Services Service Application &#40;SharePoint 2010 and SharePoint 2013&#41;](configure-e-mail-for-a-reporting-services-service-application.md)  
  
 [Provision Subscriptions and Alerts for SSRS Service Applications](provision-subscriptions-and-alerts-for-ssrs-service-applications.md)  
  
 [Claims to Windows Token Service &#40;C2WTS&#41; and Reporting Services](../../sql-server/install/claims-to-windows-token-service-c2wts-and-reporting-services.md)  
  
## See Also  
 [Data Alerts Architecture and Workflow](../reporting-services-data-alerts.md#AlertingWF)   
 [Data Alert Manager for Alerting Administrators](../data-alert-manager-for-alerting-administrators.md)  
  
  
