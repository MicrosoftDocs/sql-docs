---
title: Reporting Services tools
description: Learn tools used to configure a report server, manage report server content & operations, and create and publish paginated & mobile Reporting Services reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/10/2024
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

#customer intent: As a SQL server user, I want to find out about various report server tools so that I can use them to create and manage reports.
---

# Reporting Services tools (SSRS)

This article discusses various tools that are available in SQL Server Reporting Services (SSRS). You can use these tools to configure a report server, manage report server content and operations, and create and publish paginated and mobile SSRS reports.

Something about brief overview of each tool and also information about how to start using it.

## Report Server Configuration Manager (native mode)

You can use Report Server Configuration Manager to complete the following tasks on a native mode report server:

- Specify the service account.
- Create or upgrade the report server database.
- Modify connection properties.
- Specify URLs.
- Manage encryption keys.
- Configure unattended report processing and email report delivery.

Report Server Configuration Manager is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SQL Server Reporting Services (2017 and later): [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SQL Server Reporting Services (2016): [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

For steps you can take to start Report Server Configuration Manager, see the [Get started](../install-windows/reporting-services-configuration-manager-native-mode.md#get-started) section of [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).

For information about configuring SSRS, see [Configure and administer a report server (SSRS native mode)](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md).

For general information about Report Server Configuration Manager capabilities, see [What is Report Server Configuration Manager (native mode)?](../install-windows/reporting-services-configuration-manager-native-mode.md).

## Web portal (native mode)

You can use the web portal of a native mode configuration of SSRS to set permissions, manage subscriptions and schedules, and work with reports. You can also use the web portal to view reports. For more information, see [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md).

The web portal is installed when you install an SSRS native mode report server. For more information, see the following resources:

- For SQL Server Reporting Services (2017 and later): [Install and configure SQL Server Reporting Services](../install-windows/install-reporting-services.md)
- For SQL Server Reporting Services (2016): [Install a Reporting Services 2016 native mode report server](../install-windows/install-reporting-services-native-mode-report-server.md)

Before you can open the web portal, you must have sufficient permissions. Initially, only members of the local administrators group have permissions that provide access to web portal features.

The pages and options that are available in web portal depend on the role assignments of the current user. Users who have no permissions get an empty page. Users with permissions to view reports get links that they can select to open the reports. To learn more about permissions, see [Roles and permissions (Reporting Services)](../../reporting-services/security/roles-and-permissions-reporting-services.md).

### Start the web portal

For steps you can take to start the web portal, see the [Get started](../web-portal-ssrs-native-mode.md#get-started) section of [What is the report server web portal (native mode)?](../web-portal-ssrs-native-mode.md).

If you run the web portal on a local report server, see [Configure a native mode report server for local administration (SSRS)](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## SQL Server Management Studio

Report server administrators can use SQL Server Management Studio (SSMS) to manage a report server alongside other [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] component servers. For more information about using SSMS to connect to a SQL Server instance and run some Transact-SQL (T-SQL) commands, see [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../../ssms/quickstarts/ssms-connect-query-sql-server.md).

For steps you can take to start using SSMS to manage a report server, see [Connect to a native mode report server](connect-to-a-report-server-in-management-studio.md#connect-to-a-native-mode-report-server).

## Report Designer in SQL Server Data Tools

You have a choice of two different tools for creating SSRS paginated reports: Report Designer and Report Builder. For information about Report Builder, see [Report Builder](#report-builder), later in this article.

Report Designer is available in [!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)] - Visual Studio. The Report Designer design surface includes tabbed windows, wizards, and menus that you use to access report authoring features. The report designer tool becomes available when you choose a Report Server Project template or a Report Server Project Wizard template in SSDT.

Download [SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md).

For information about installing SSDT, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio).

To use the Report Designer templates, you also need to install an SSRS extension for Visual Studio. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

For steps you can take to start Report Designer, see the [Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md#create-a-report-server-project) section of [Tutorial: Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md).

Solution Explorer provides categories for creating reports and data sources. You can use these categories to create new reports and data sources. Tabbed windows appear when you create a report definition. The tabbed windows are Data, Layout, and Preview.

To get started on your first report, see [Create a basic table report &#40;SSRS tutorial&#41;](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md). To learn more about query designers you can use within Report Designer, see [Query Design Tools &#40;SSRS&#41;](../../reporting-services/report-data/query-design-tools-ssrs.md).

For more information about using the Report Designer UI, see [Reporting Services in SQL Server Data Tools (SSDT)](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

## <a name="bkmk_report_builder"></a> [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]

[Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md) is a stand-alone application you can use to create paginated reports outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can customize and update all existing reports, regardless of whether they were created in Report Designer or in previous versions of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. Install it from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal or from the Microsoft Download Center.

When your paginated report is ready, publish it to a report server or [save it to the Power BI service](/power-bi/paginated-reports-save-to-power-bi-service).
[Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) from the Microsoft Download Center.

### Start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]

1. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal, on the **New** menu, select **Paginated Report**.

    :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot that shows the New menu options.":::

2. If [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] isn't installed on this computer yet, select **Get [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]**.

    Or

    [Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) from the Microsoft Download Center.

3. [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens and you can create or open a paginated report.

## <a name="bkmk_mobile_report_pub"></a> [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Use [SQL Server Mobile Report Publisher](../mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) to create mobile reports you can view in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal and in mobile devices such as iPads and iPhones.\

You can install it from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal or from the Microsoft Download Center.

[Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527) from the Microsoft Download Center.

### Start [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)]

1. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal, on the **New** menu, select **Mobile Report**.

    :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot that shows the New menu options.":::

2. If [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] isn't installed on this computer yet, select **Get [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)]**.

    Or

    [Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527) from the Microsoft Download Center.

3. [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] opens and you can create or open a mobile report.

## Related content

[Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527)  
[Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138)  
[Download SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md)  
[Install Reporting Services SharePoint mode](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  
[Reporting Services report server](../../reporting-services/report-server-sharepoint/reporting-services-report-server.md)  
[Query design tools](../../reporting-services/report-data/query-design-tools-ssrs.md)  
[Reporting Services tutorials](../../reporting-services/reporting-services-tutorials-ssrs.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
