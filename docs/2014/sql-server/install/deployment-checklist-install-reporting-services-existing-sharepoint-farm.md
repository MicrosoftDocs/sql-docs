---
title: "Deployment Checklist: Install Reporting Services into an Existing SharePoint Farm | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 436b4c3d-3f2f-464a-be7e-5c051d9ffb8f
author: markingmyname
ms.author: maghan
manager: craigg
---
# Deployment Checklist: Install Reporting Services into an Existing SharePoint Farm
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint report servers can be installed into a new SharePoint Farm or into an existing SharePoint farm. This topic describes the possible scenarios and best practices for installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into and existing SharePoint farm.  
  
## Prerequisites  
 Before you run Setup, review the following information:  
  
|Step|Link|  
|----------|----------|  
|Create or identify the accounts used in a report server deployment. You must have a service account for the Report Server service, and credentials for connecting to the report server database||  
|Decide on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to host the report server database. You can use a local or remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You should choose an instance that is on a computer that has the storage capacity to accommodate your reports.||  
|(Optional) Find the name of the SMTP server or gateway that provides e-mail service to your organization if you want to use report server e-mail in subscriptions|[Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)|  
|Note: If you are upgrading a computer from a previous CTP release [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and you had made custom changes to the configuration files, you will need to make the same changes to the configuration files, following the upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The affected files are **web.config** and **client.config**.||  
  
## Installation Scenarios  
 The following table describes the possible scenarios when you are installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into and existing SharePoint Farm. Local mode allows reports to be rendered locally from the SharePoint document library, without integration with a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products is required but a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server is not. For more information on local mode, see [Local Mode vs. Connected Mode Reports in the Report Viewer &#40;Reporting Services in SharePoint Mode&#41;](../../../2014/reporting-services/local-vs-connected-mode-report-viewer-reporting-services-sharepoint-mode.md) and [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
|Starting Configuration|Workflow|Ending Configuration|Comments|  
|----------------------------|--------------|--------------------------|--------------|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] in Local mode|Installation|Connected mode [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].||  
|Connected Mode [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|In place upgrade|Connected mode [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].||  
|Connected Mode [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|Migration|Connected mode [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].||  
  
## Installation and in place Upgrade Checklist  
 The following table summarizes the steps, tools, and information you should review and use for the installation:  
  
|Step|Link|  
|----------|----------|  
|**Installation and initial configuration**||  
|Install the SharePoint add-in on all Web front-end (WFE) computers.|[Add an Additional Reporting Services Web Front-end to a Farm](../../reporting-services/install-windows/add-an-additional-reporting-services-web-front-end-to-a-farm.md)|  
|Install SQL Server [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Reporting Services and the Database engine.|[Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)|  
|Create at least one SSRS service application and configure service app association.|See the 'Service Application' section in [Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)|  
|**Additional Configuration**||  
|Add SSRS content types to your document library.|[Add Report Server Content Types to a Library &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../../2014/reporting-services/add-reporting-services-content-types-to-a-sharepoint-library.md)|  
|Provision SQL Server Agent|[Provision Subscriptions and Alerts for SSRS Service Applications](../../reporting-services/install-windows/provision-subscriptions-and-alerts-for-ssrs-service-applications.md)|  
|Configure e-mail settings for your Service application|[Configure E-mail for a Reporting Services Service Application &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/configure-e-mail-for-a-reporting-services-service-application.md)|  
|Configure Claims to Windows Token Service (c2WTS)|[Claims to Windows Token Service &#40;C2WTS&#41; and Reporting Services](../../../2014/sql-server/install/claims-to-windows-token-service-c2wts-and-reporting-services.md)|  
  
## Migration Checklist  
 The following is a list of the basic steps for a migration from an existing installation to a new installation.  
  
|Step|Link|  
|----------|----------|  
|Install and configure your new server. This includes the following:<br /><br /> SharePoint Products Preparation Tool<br /><br /> SharePoint 2010 Product<br /><br /> SharePoint 2010 SP1<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint Mode<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint 2010 products|[Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)|  
|Create at least one [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application||  
|Backup [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Databases||  
|Backup [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Encryption keys||  
|Restore [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] database and Encryption keys||  
|Map all web applications to new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]service application(s)|The new installation **is now functional**|  
|Remove the Integration URL on the old server.|From SharePoint Central Administration, on the **General Application Settings** Page, click **Reporting Services Integration**.|  
|Uninstall [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] from the old installation, if desired.||  
  
## Next Steps  
  
## See Also  
 [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)   
 [Guidance for Using SQL Server BI Features in a SharePoint 2010 Farm](../../../2014/sql-server/install/guidance-for-using-sql-server-bi-features-in-a-sharepoint-2010-farm.md)   
 [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2014&#41;](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
  
