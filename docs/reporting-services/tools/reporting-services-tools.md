---
title: "Tools in Reporting Services"
description: Learn about the tools for development, configuration, administration, and report viewing that are contained in SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/06/2019
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "SSRS, tools"
  - "Reporting Services, tools"
  - "components [Reporting Services]"
  - "components [Reporting Services], about components"
  - "Reporting Services, components"
  - "SSRS, components"
  - "reports [Reporting Services], tools"
  - "SQL Server Reporting Services, components"
  - "SQL Server Reporting Services, tools"
  - "architecture [Reporting Services]"

#customer intent: As a SQL Server user, I want to learn about the different tools available in SQL Server Reporting Services so that I can fully use them to create my reports.
---
# Tools in Reporting Services

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] contains a set of graphical and scripting tools that support the development and use of rich reports in a managed environment. The tool set includes development tools, configuration and administration tools, and report viewing tools. This article gives a brief overview of each tool in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and how you can access it.  
  
 To find a tool right away, see [Tutorial: How to locate and start Reporting Services tools &#40;SSRS&#41;](../../reporting-services/tools/tutorial-how-to-locate-and-start-reporting-services-tools-ssrs.md).  
  
## Tools for report authoring  

 The following table lists the available tools for report authoring in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
|Tool|Description|How to access|  
|----------|-----------------|-------------------|  
|[!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]|With [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)], you can create mobile reports that dynamically adjust the content to fit your screen or browser window and scale well to any screen size.<br /><br /> Create mobile reports on a design surface with adjustable grid rows and columns, and flexible mobile report elements.<br /><br /> For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).|Download the [SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkId=733527).|  
|Report Designer|Use this tool to design reports. It includes the following features:<br /><br /> Deploy to a native mode or SharePoint mode report server.<br /><br /> Hosted in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].<br /><br /> Report Data pane to organize data used in your report.<br /><br /> Tabbed views for Design and Preview for interactive report design.<br /><br /> Query designers to help specify which data to retrieve from data sources and that are associated with data source types in the [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md).<br /><br /> Expression editor with IntelliSense to build [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] expressions that customize report content and appearance.<br /><br /> Supports custom report items and custom query designers.<br /><br /> <br /><br /> For more information, see [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).|[!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]|  
|Report Builder|Use this tool to design reports. It includes the following features:<br /><br /> Deploy to a native mode or SharePoint mode report server.<br /><br /> [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office-like authoring environment. <br /><br />[!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)].<br /><br /> Ability to save report items as report parts.<br /><br /> A wizard for creating maps.<br /><br /> Aggregates of aggregates.<br /><br /> Enhanced support for expressions.<br /><br /> Query designers to help specify which data to retrieve from a selection of built-in data sources types.<br /><br /> For more information, see [Microsoft Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md).|Download the [standalone version of Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138).<br /><br /> Or you can open it from the web portal/SharePoint.|  

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]
>
>Report parts are deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019.

## Tools for report server administration  

 A set of graphical and scripting tools are available for administering the report server in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The tools that you use depend on the deployment mode of your report server.  
  
### Native mode  

 The following table lists the available tools for administering a report server deployed in native mode.  
  
|Tool|Description|How to access|  
|----------|-----------------|-------------------|  
|Report Server Configuration Manager|Use this tool to configure a Reporting Services installation. Available tasks include:<br /><br />  Configuring the Report Server service account.<br /><br /> Creating and configuring one or more Web service URLs.<br /><br /> Configuring the web portal URL.<br /><br /> Creating and configuring the report server database.<br /><br /> Configuring a scale-out deployment.<br /><br /> Backup, restoring, or replacing a symmetric key used to encrypt stored connection strings and credentials.<br /><br /> Configuring the unattended execution account.<br /><br /> Configuring Subscription settings.<br /><br /> Configuring an SMTP (Simple Mail Transfer Protocol) server for e-mail delivery.<br /><br /> Configuring the Power BI Service (cloud).<br /><br /> Note: The Report Server Configuration Manager doesn't help you manage report server content, enable other features, or grant access to the server.<br /><br /> For more information, see [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).|Start menu|  
|SQL Server Management Studio|Use this tool to manage one or more report server instances in a single environment, including:<br /><br /> Managing both local and remote report server instances.<br /><br /> Setting report server properties.<br /><br /> Modifying role definitions.<br /><br /> Turning off report server features you aren't using.<br /><br /> Managing jobs.<br /><br /> Managing shared schedules.|Start menu|
|Rsconfig Utility|Use this tool to configure and manage a report server connection to the report server database. You can also use it to specify a user account to use for unattended report processing.<br /><br /> For more information, see [Report server common prompt utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).|Command prompt|  
|Rskeymgmt Utility|Use this tool to:<br /><br /> Extract, restore, create, and delete the symmetric key used to encrypt report server data.<br /><br /> Join report server instances in a scale-out deployment.<br /><br /> <br /><br /> For more information, see [Report server command prompt utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).|Command prompt|  
|Windows Management Instrumentation (WMI) Classes|Use these classes to automate the configuration tasks in Report Server Configuration Manager without needing to use the graphical user interface.<br /><br /> For more information, see [Access the WMI Provider programmatically](../../reporting-services/accessing-the-wmi-provider-programmatically.md).|Visual Basic script|  

::: moniker range="=sql-server-2016"

### SharePoint integrated mode  

 In SharePoint mode, the Reporting Services is a service application in the SharePoint architecture, and you administer directly through SharePoint.  
  
|Tool|Description|How to access|  
|----------|-----------------|-------------------|  
|SharePoint Central Administration|Use SharePoint Central Administration to create, query, and manage the shared service applications for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].<br /><br /> For more information, see [Configuration and administration of a SQL Server Reporting Services (SSRS) report server](../../reporting-services/report-server-sharepoint/configuration-and-administration-of-a-report-server.md).|Browser to the SharePoint site URL for Central Administration|  
|PowerShell Cmdlets|Use PowerShell cmdlets to create, query, and manage the shared service applications for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].<br /><br /> For more information, see [PowerShell cmdlets for Reporting Services SharePoint mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).|SharePoint 2010 Management Shell|  

::: moniker-end

## Tools for report content management  

 A set of graphical and scripting tools are available for managing content in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The tools that you use depend on the deployment mode of your report server.  
  
|Tool|Description|How to access|  
|----------|-----------------|-------------------|  
|Report Server Web service URL|Use this tool to browse content in the report catalog in a generic item navigation page.<br /><br /> For more information, see [Report Server web service](../../reporting-services/report-server-web-service/report-server-web-service.md).|Browser|  
|Web Portal|**(Native mode only)** Use this tool to administer a single report server instance from a remote location over an HTTP connection. You can do the following actions:<br /><br /> View, search, print, and subscribe to reports.<br /><br /> To organize items on the server, you can create, secure, and maintain the folder hierarchy.<br /><br /> Configure role-based security that determines access to items and operations.<br /><br /> Configure report execution properties, report history, and report parameters.<br /><br /> Create report models that connect to and retrieve data from a Microsoft SQL Server Analysis Services data source or from a SQL Server relational data source.<br /><br /> Set model item security to allow access to specific entities in the model, or map entities to predefined clickthrough reports that you create in advance.<br /><br /> To make schedules and data source connections more manageable, you can create shared schedules and shared data sources.<br /><br /> Create data-driven subscriptions that roll out reports to a large recipient list.<br /><br /> Create linked reports to reuse and repurpose an existing report in different ways.<br /><br /> Launch Report Builder to create reports that you can save and run on the report server. For more information, see [The web portal of a report server (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md).| Browser  
|RS Utility|This tool is a script host that you can use to perform scripted operations. Use this tool to run [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] scripts that copy data between report server databases, publish reports, create items in a report server database, and more. For more information, see [Report server command prompt utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).|Command prompt|  
  
## Related content  

 [Comparing native and SharePoint Reporting Services report servers](../../reporting-services/report-server-sharepoint/reporting-services-report-server.md)
 [Reporting Services concepts &#40;SSRS&#41;](../../reporting-services/reporting-services-concepts-ssrs.md)
 [What is SQL Server Reporting Services (SSRS)](../../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md)  
  