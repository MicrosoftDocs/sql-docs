---
title: Locate and start Reporting Services tools (SSRS)
description: Find out about tools that you can use to configure or manage a report server, and create and publish paginated and mobile Reporting Services reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/11/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Reporting Services, tutorials"
  - "Reporting Services, tools"
  - "Reporting Services Configuration tool"
  - "Business Intelligence Development Studio, locating and starting tool"
  - "Model Designer [Reporting Services], locating and starting tool"
  - "Report Designer [Reporting Services], tutorials"
  - "tools [Reporting Services]"
  - "tutorials [Reporting Services]"
  - "Report Builder"

#customer intent: As a SQL server user, I want to find out about various tools so that I can use them to create reports and manage report servers.
---

# Locate and start Reporting Services tools (SSRS)

[!INCLUDE[SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE[SSRS applies to 2016 and later](../../includes/ssrs-appliesto-2016-and-later.md)]

This article discusses various tools that are available in SQL Server Reporting Services (SSRS). You can use these tools to configure a report server, manage report server content and operations, and create and publish paginated and mobile SSRS reports.

Besides a brief overview of each tool, this article also provides links to instructions for getting started with the tool.

## Report Server Configuration Manager (native mode)

You can use Report Server Configuration Manager to complete the following tasks on a native mode report server:

- Specify the service account.
- Create or upgrade the report server database.
- Modify connection properties.
- Specify URLs.
- Manage encryption keys.
- Configure unattended report processing and email report delivery.

### Install Report Server Configuration Manager

Report Server Configuration Manager is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SSRS 2017 and later: [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SSRS 2016: [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

### Start Report Server Configuration Manager

- For instructions for starting Report Server Configuration Manager, see the [Get started](../install-windows/reporting-services-configuration-manager-native-mode.md#get-started) section of [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).
- For information about configuring SSRS, see [Configure and administer a report server (SSRS native mode)](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md).
- For general information about Report Server Configuration Manager capabilities, see [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).

## Web portal (native mode)

You can use the web portal of a native mode configuration of SSRS to set permissions, manage subscriptions and schedules, and work with reports. You can also use the web portal to view reports. For more information, see [What is the report server web portal (native mode)?](../../reporting-services/web-portal-ssrs-native-mode.md).

### Install the web portal

The web portal is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SSRS 2017 and later: [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SSRS 2016: [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

### Start the web portal

Before you can open the web portal, you must have sufficient permissions. Initially, only members of the local administrator group have permissions that provide access to web portal features.

The pages and options that are available in the web portal depend on the role assignments of the current user. Users who have no permissions see an empty page. Users with permissions to view reports get links to those reports. To learn more about permissions, see [Roles and permissions in Reporting Services](../../reporting-services/security/roles-and-permissions-reporting-services.md).

For instructions for starting the web portal, see the [Get started](../web-portal-ssrs-native-mode.md#get-started) section of [What is the report server web portal (native mode)?](../web-portal-ssrs-native-mode.md).

If you run the web portal on a local report server, see [Configure a native mode report server for local administration (SSRS)](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## SQL Server Management Studio

Report server administrators can use SQL Server Management Studio (SSMS) to manage a report server alongside other [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] component servers. For more information about using SSMS to connect to a SQL Server instance and run some Transact-SQL (T-SQL) commands, see [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../../ssms/quickstarts/ssms-connect-query-sql-server.md).

### Start SSMS to manage a report server

For instructions for starting SSMS to manage a report server, see [Connect to a native mode report server](connect-to-a-report-server-in-management-studio.md#connect-to-a-native-mode-report-server).

## Report Designer in SQL Server Data Tools

You have a choice of two different tools for creating SSRS paginated reports: Report Designer and Report Builder. For information about Report Builder, see [Report Builder](#report-builder), later in this article.

Report Designer is available in [!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)] - Visual Studio. The Report Designer design surface includes tabbed windows, wizards, and menus that you use to access report authoring features. Report Designer becomes available when you select a Report Server Project template or a Report Server Project Wizard template in SSDT.

### Install Report Designer

For information about installing SSDT, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio).

To use the Report Designer templates, you also need to install an SSRS extension for Visual Studio. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

### Start Report Designer

For instructions for starting Report Designer, see the [Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md#create-a-report-server-project) section of [Tutorial: Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md).

When you work in Report Designer, the Visual Studio component Solution Explorer displays folders that display all the items in your project. In a report project, these folders organize your shared data sources, shared datasets, reports, and resources.

You can create new reports, data sources, and datasets by right-clicking on a folder and then selecting an option for adding an item.

:::image type="content" source="media/locate-start-reporting-services-tools-ssrs/solution-explorer-reports-menu-add-new-report.png" alt-text="Screenshot of Solution Explorer that shows report item folders. The Reports folder, and in its shortcut menu, Add New Report, are highlighted.":::

When you create a new report, tabbed windows appear for Design and Preview views.

:::image type="content" source="media/locate-start-reporting-services-tools-ssrs/report-design-preview-tabs.png" alt-text="Screenshot of the Report Designer design surface. The Design and Preview tabs of a report definition file are highlighted.":::

For more information about Report Designer, see the following resources:

- To get started on your first report, see [Create a basic table report (SSRS tutorial)](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md).
- To find out more about query designers that you can use in Report Designer, see [Query design tools (SSRS)](../../reporting-services/report-data/query-design-tools-ssrs.md).
- To see how to use Report Designer views, menus, toolbars, and shortcuts, see [Reporting Services in SQL Server Data Tools (SSDT)](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

## Report Builder

[!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] is a stand-alone application that you can use to create paginated reports outside [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)]. You can also use [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)] to customize and update existing reports, besides creating reports. The existing reports can be ones that were created in your version of [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], in earlier versions, or in Report Designer.

When a paginated report is ready, you can publish it to a report server or to the Power BI service. For more information about saving a report to the Power BI service, see [Publish a paginated report to the Power BI service](/power-bi/paginated-reports/paginated-reports-save-to-power-bi-service).

For more general information about [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], see [Microsoft Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md).

### Install [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)]

For instructions for installing [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], see [Install Microsoft Report Builder](../install-windows/install-report-builder.md).

### Start [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)]

For instructions for starting [!INCLUDE[Report Builder](../../includes/ssrbnoversion.md)], see [Start Microsoft Report Builder](../report-builder/start-report-builder.md).

## [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]

[!INCLUDE [SQL Server Mobile Report Publisher deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

You can use [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] to create mobile reports that you can view in the [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] web portal and in mobile devices. For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).

### Install [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]

You have the following options for installing [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]:

- You can download [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=733527) and then open the downloaded file.
- You can install [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] from the SSRS web portal by going to the **New** menu and selecting **Mobile Report**.

  :::image type="content" source="media/locate-start-reporting-services-tools-ssrs/web-portal-new-menu.png" alt-text="Screenshot of the web portal that shows the New menu options. The New menu and the Mobile Report option are highlighted.":::

  When prompted, you select **Get Mobile Report Publisher**, and then you download and open the [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)] file.

### Start [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)]

For instructions for starting [!INCLUDE[SQL Server Mobile Report Publisher](../../includes/ss-mobilereptpub-long.md)], see the initial steps in [Create a Reporting Services mobile report](../mobile-reports/create-a-reporting-services-mobile-report.md).

## Related content

- [Reporting Services tutorials (SSRS)](../reporting-services-tutorials-ssrs.md)
- [Report Builder tutorials](../report-builder-tutorials.md)
