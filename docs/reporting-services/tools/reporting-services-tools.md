---
title: SQL Server Reporting Services tools
description: Find out about the tools for development, configuration, administration, and report viewing that are available in SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/13/2024
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

#customer intent: As a SQL Server user, I want to find out about tools that are available in SQL Server Reporting Services so that I can effectively use them to create and manage my reports.
---
# SQL Server Reporting Services tools

[!INCLUDE[SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE[SSRS applies to 2016 and later](../../includes/ssrs-appliesto-2016-and-later.md)]

SQL Server Reporting Services (SSRS) contains a set of graphical and scripting tools that support the development and use of rich reports in a managed environment. The tool set includes development tools, configuration and administration tools, and report-viewing tools. This article gives a brief overview of each tool in [!INCLUDE[SSRS](../../includes/ssrs.md)] and explains how you can access it.

## Tools for report authoring

The following sections discuss tools that you can use for report authoring in [!INCLUDE[SSRS](../../includes/ssrs.md)].

### [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]

You can use [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] to create mobile reports that you can view in the [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] web portal and in mobile devices.

[!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] offers a design surface with adjustable-grid rows and columns, and flexible mobile report elements. The mobile reports that you create dynamically adjust the content to fit your screen or browser window. These reports scale well to any screen size.

For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).

[!INCLUDE [SQL Server Mobile Report Publisher deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

#### [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] installation

You have the following options for installing [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]:

- You can download [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=733527) and then open the downloaded file.
- You can install [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] from the SSRS web portal by going to the **New** menu and selecting **Mobile Report**.

  :::image type="content" source="media/reporting-services-tools/web-portal-new-menu.png" alt-text="Screenshot of the web portal that shows the New menu options. The New menu and the Mobile Report option are highlighted.":::

  When prompted, you select **Get Mobile Report Publisher**, and then you download and open the [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] file.

#### [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] startup

For instructions on starting [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)], see the initial steps in [Create a Reporting Services mobile report](../mobile-reports/create-a-reporting-services-mobile-report.md).

### Report Designer

Report Designer is a tool for designing reports. It's available in [!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)] - Visual Studio.

The Report Designer design surface includes tabbed windows, wizards, and menus that you use to access report authoring capabilities. Report Designer offers the following features:

- The ability to deploy reports to a native mode or SharePoint mode report server
- A Report Data pane to organize your report data
- An interactive report design experience through tabbed views for designing and previewing reports
- Query designers that are associated with data source types in the [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md) and that help specify which data to retrieve from data sources
- An expression editor with IntelliSense to build [!INCLUDE[Visual Basic](../../includes/visual-basic-md.md)] expressions that customize report content and appearance
- Support for custom report items and custom query designers

#### Report Designer installation

For information about installing SSDT, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio).

Report Designer becomes available when you select a Report Server Project template or a Report Server Project Wizard template in SSDT. To use these templates, you also need to install an SSRS extension for Visual Studio. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

#### Report Designer startup

For instructions on starting Report Designer, see the [Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md#create-a-report-server-project) section of [Tutorial: Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md).

When you work in Report Designer, the Visual Studio component Solution Explorer displays folders that organize all the items in your project. In a report project, these folders contain your shared data sources, shared datasets, reports, and resources.

You can create new reports, data sources, and datasets by right-clicking on a folder and then selecting an option for adding an item.

:::image type="content" source="media/reporting-services-tools/solution-explorer-reports-menu-add-new-report.png" alt-text="Screenshot of Solution Explorer that shows report item folders. The Reports folder, and in its shortcut menu, Add New Report, are highlighted.":::

When you create a new report, tabbed windows appear for Design and Preview views.

:::image type="content" source="media/reporting-services-tools/report-design-preview-tabs.png" alt-text="Screenshot of the Report Designer design surface. The Design and Preview tabs of a report definition file are highlighted.":::

For more information about Report Designer, see the following resources:

- To get started on your first report, see [Create a basic table report (SSRS tutorial)](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md).
- To find out more about query designers that you can use in Report Designer, see [Query design tools (SSRS)](../../reporting-services/report-data/query-design-tools-ssrs.md).
- To see how to use Report Designer views, menus, toolbars, and shortcuts, see [Reporting Services in SQL Server Data Tools (SSDT)](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

### [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)]

[!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] is a stand-alone application that you can use to create paginated reports outside [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)]. You can also use [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] to customize and update existing reports, besides creating reports. The existing reports can be ones that were created in your version of [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], in earlier versions, or in Report Designer.

Report Builder offers the following features:

- The ability to deploy reports to a native mode or SharePoint mode report server or to the Power BI service. For more information about saving a report to the Power BI service, see [Publish a paginated report to the Power BI service](/power-bi/paginated-reports/paginated-reports-save-to-power-bi-service).
- A [!INCLUDE[Microsoft](../../includes/msconame-md.md)] Office-like authoring environment.
- Support for [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] in SSRS 2019 and SSRS 2016.
- A wizard for creating maps.
- Aggregates of aggregates.
- Enhanced support for expressions.
- Query designers to help specify which data to retrieve from a selection of built-in data sources types.
- Support for saving report items as report parts.

[!INCLUDE [SSRS report parts deprecated](../../includes/ssrs-report-parts-deprecated.md)]

For more information about Report Builder, see [Microsoft Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md).

#### [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] installation

For instructions on installing [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], see [Install Microsoft Report Builder](../install-windows/install-report-builder.md).

#### [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] startup

For instructions on starting [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], see [Start Microsoft Report Builder](../report-builder/start-report-builder.md).

## Tools for report server administration

A set of graphical and scripting tools are available for administering the report server in [!INCLUDE[SSRS](../../includes/ssrs.md)]. The tools that you use depend on the deployment mode of your report server.

### Native mode

The following sections discuss tools for a native mode report server.

#### Report Server Configuration Manager

Report Server Configuration Manager is a tool for configuring an SSRS installation. You can use this tool to complete the following tasks on a native mode report server:

- Configuring the report server service account
- Creating and configuring web service URLs
- Configuring the web portal URL
- Creating, configuring, or upgrading the report server database
- Configuring a scale-out deployment
- Backing up, restoring, or replacing a symmetric key that's used to encrypt stored connection strings and credentials
- Configuring the unattended execution account
- Configuring subscription settings
- Configuring a Simple Mail Transfer Protocol (SMTP) server for email delivery
- Configuring the Power BI Service
- Modifying connection properties

Report Server Configuration Manager doesn't help you manage report server content, turn on other features, or grant access to the report server.

For more information, see [What is Report Server Configuration Manager (native mode)?](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).

##### Report Server Configuration Manager installation

Report Server Configuration Manager is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SSRS 2017 and later: [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SSRS 2016: [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

##### Report Server Configuration Manager startup

- For instructions on starting Report Server Configuration Manager, see the [Get started](../install-windows/reporting-services-configuration-manager-native-mode.md#get-started) section of [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).
- For information about configuring SSRS, see [Configure and administer a report server (SSRS native mode)](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md).
- For general information about Report Server Configuration Manager capabilities, see [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).

#### SQL Server Management Studio

Report server administrators can use SQL Server Management Studio (SSMS) to manage a report server alongside other [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] component servers. When you use SSMS to manage report server instances, you can complete the following tasks:

- Managing local and remote report server instances
- Setting report server properties
- Modifying role definitions
- Turning off report server features that you don't use
- Managing jobs
- Managing shared schedules

For more information about using SSMS to connect to a SQL Server instance and run some Transact-SQL (T-SQL) commands, see [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../../ssms/quickstarts/ssms-connect-query-sql-server.md).

##### SSMS access

For instructions on starting SSMS to manage a report server, see [Connect to a native mode report server](connect-to-a-report-server-in-management-studio.md#connect-to-a-native-mode-report-server).

#### Rsconfig.exe utility

You can use this tool to configure and manage a report server connection to a report server database. You can also use it to specify a user account to use for unattended report processing.

For more information, see [Report server common prompt utilities (SSRS)](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).

##### Rsconfig.exe access

The rsconfig.exe utility is installed when you install SSRS. You run this utility from the command line.

#### Rskeymgmt.exe utility

You can use this tool to:

- Extract, restore, create, and delete the symmetric key that's used to encrypt report server data.
- Join report server instances in a scale-out deployment.

For more information, see [Report server command prompt utilities (SSRS)](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).

##### Rskeymgmt.exe access

The rskeymgmt.exe utility is installed when you install SSRS. You run this utility from the command line.

#### Windows Management Instrumentation (WMI) classes

These classes can help you automate configuration tasks in Report Server Configuration Manager. They provide a way for applications to access and manipulate SSRS management information. When you use these classes, you no longer need to use the graphical user interface to perform the tasks manually.

For more information, see [Access the WMI provider programmatically](../../reporting-services/accessing-the-wmi-provider-programmatically.md).

##### WMI access

You can use a Visual Basic script to access the WMI classes.

::: moniker range="=sql-server-2016"

### SharePoint integrated mode

In SharePoint mode, SSRS is a service application in the SharePoint architecture, and you perform administrative tasks directly through SharePoint. The following sections discuss tools for a report server that's configured for SharePoint mode.

#### SharePoint Central Administration

You can use SharePoint Central Administration to create, query, and manage the shared service applications for [!INCLUDE[SSRS](../../includes/ssrs.md)].

For more information, see [Configuration and administration of a SQL Server Reporting Services (SSRS) report server](../../reporting-services/report-server-sharepoint/configuration-and-administration-of-a-report-server.md).

##### SharePoint Central Administration access

You can access this tool in a browser by going to the SharePoint Central Administration site.

#### PowerShell cmdlets

You can use PowerShell cmdlets to create, query, and manage the shared service applications for [!INCLUDE[SSRS](../../includes/ssrs.md)].

For more information, see [PowerShell cmdlets for Reporting Services SharePoint mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).

##### PowerShell cmdlet access

To use the cmdlets, you open SharePoint Management Shell and run the commands in the shell.

::: moniker-end

## Tools for report content management

Graphical and scripting tools are available for managing content in [!INCLUDE[SSRS](../../includes/ssrs.md)]. The tools that you use depend on the deployment mode of your report server.

### Report server web service URL

The report server web service acts as a communications interface between client programs and the report server. The URL of this service provides access to the full functionality of the report server. You can use this tool to browse content in the report catalog in a generic item navigation page.

For more information, see [Report Server web service](../../reporting-services/report-server-web-service/report-server-web-service.md).

#### Web service URL access

You use a browser to access the web service URL.

### Web portal

You can use the web portal to administer a single report server instance from a remote location over an HTTP connection. The web portal is only available for native mode report servers.

You can use this tool for the following tasks:

- Viewing, searching, printing, and subscribing to reports
- Organizing, creating, securing, and maintaining the folder hierarchy
- Configuring role-based security that determines access to items and operations
- Configuring report execution properties, report history, and report parameters
- Creating report models that connect to and retrieve data from a Microsoft SQL Server Analysis Services data source or from a SQL Server relational data source
- Setting model item security to allow access to specific entities in the model, or mapping entities to predefined click-through reports that you create in advance
- Creating shared schedules and shared data sources
- Making schedules and data source connections more manageable
- Creating data-driven subscriptions that roll out reports to a large recipient list
- Creating linked reports to reuse and repurpose an existing report in different ways
- Opening Report Builder to create reports that you can save and run on the report server

For more information, see [What is the report server web portal (native mode)?](../../reporting-services/web-portal-ssrs-native-mode.md).

#### Web portal installation

The web portal is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SSRS 2017 and later: [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SSRS 2016: [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

#### Web portal startup

Before you can open the web portal, you must have sufficient permissions. Initially, only members of the local administrator group have permissions that provide access to web portal features.

The pages and options that are available in the web portal depend on the role assignments of the current user. Users who have no permissions see an empty page. Users with permissions to view reports get links to those reports. To learn more about permissions, see [Roles and permissions in Reporting Services](../../reporting-services/security/roles-and-permissions-reporting-services.md).

For instructions on starting the web portal, see the [Get started](../web-portal-ssrs-native-mode.md#get-started) section of [What is the report server web portal (native mode)?](../web-portal-ssrs-native-mode.md).

If you run the web portal on a local report server, see [Configure a native mode report server for local administration (SSRS)](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

### Rs.exe utility

This tool is a script host that you use to perform scripted operations. You can use this tool to run [!INCLUDE[Visual Basic](../../includes/visual-basic-md.md)] scripts that copy data between report server databases, publish reports, and create items in a report server database.

For more information, see [Report server command prompt utilities (SSRS)](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md).

#### Rs.exe access

The rs.exe utility is installed when you install SSRS. You run this utility from the command line.

## Related content

- [Comparing native and SharePoint Reporting Services report servers](../../reporting-services/report-server-sharepoint/reporting-services-report-server.md)
- [Reporting Services concepts (SSRS)](../../reporting-services/reporting-services-concepts-ssrs.md)
- [What is SQL Server Reporting Services (SSRS)?](../../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md)
