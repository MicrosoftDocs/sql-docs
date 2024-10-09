---
title: "What is the report server web portal (Native mode)?"
description: The SQL Server Reporting Services (SSRS) web portal is a web-based experience for viewing reports and KPIs, and navigating through elements on your report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: overview
ms.custom:
  - updatefrequency5
# customer intent: As a SQL Server user, I want to understand the SSRS web portal features so that I can view and configure reports in my report server instance.
---
# What is the report server web portal (Native mode)?

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

The SQL Server Reporting Services (SSRS) report server web portal allows you to view reports, key performance indicators (KPIs), and navigate through the elements in your report server instance. You can also use the web portal to administer a single report server instance.

:::image type="content" source="../reporting-services/media/web-portal-report-server-2022.png" alt-text="Screenshot of the SQL Server Reporting Services web portal." lightbox="../reporting-services/media/web-portal-report-server-2022.png":::

## What you can do

Use the web portal for these and other tasks:

- View, search, print, and [subscribe to reports](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).
- Create, secure, and maintain the folder hierarchy to organize items on the server.
- Configure role-based security that determines access to items and operations. For more information, see [Role definitions - predefined roles](security/role-definitions-predefined-roles.md).
- Configure report execution properties, report history, and [report parameters](report-design/report-parameters-concepts-report-builder-and-ssrs.md).
- Create [shared schedules](subscriptions/schedules.md) and shared data sources to make schedules and data source connections more manageable.
- Create [data-driven subscriptions](subscriptions/create-modify-and-delete-data-driven-subscriptions.md) that role out reports to a large recipient list.
- Create [linked reports](reports/create-a-linked-report.md) to reuse and repurpose an existing report in different ways.
- Download common tools such as [Report Builder](install-windows/install-report-builder.md).
- [Create KPIs](../reporting-services/working-with-kpis-in-reporting-services.md).
- Create [branding for the web portal](../reporting-services/branding-the-web-portal.md).
- Work with [shared datasets](../reporting-services/work-with-shared-datasets-web-portal.md).
- Send feedback or make feature requests.
- Browse the report server folders or search for specific reports. 
- View a report, its general properties, and paste copies of the report that are captured in the report history.
- Subscribe to reports for delivery to an email inbox or a shared folder on the file system, depending on your access permissions.

## Feature availability

The web portal is used only for a report server that runs in Native mode. It isn't supported for a report server that you configure for SharePoint integrated mode.

Some web portal features are only available in specified editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [SQL Server Reporting Services features supported by editions](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).

On a new installation, only local administrators have sufficient permissions to work with content and settings. To grant permissions to other users, a local administrator must create role assignments that provide access to the report server. The application pages and tasks that a user can then access depend on the role assignments for that user. For more information, see [Grant user access to a report server](./security/grant-user-access-to-a-report-server.md).

> [!NOTE]
> If you're browsing the web portal on the local machine that the server is running on, you might see a message indicating that you aren't allowed to view this folder. This is due to Universal Access Control (UAC) and that you aren't running the browser as an administrator. You need to give your account content manager rights on the folder by either browsing to the server remotely, or by using Edge locally to configure the permissions. You can't run Edge as an administrator with the **Run as administrator** menu option. However, you can run Edge as the local administrator account by holding SHIFT + right-clicking on the Edge shortcut, selecting **Run as a different user**, then providing the local machine administrator account information. If you want to use the web portal remotely, give your account content manager rights on the folder.  

## Get started

To start the web portal from a browser, follow these steps:

1. Open your web browser. For a list of supported web browsers, see [Browser support for Reporting Services and Power View](../reporting-services/browser-support-for-reporting-services-and-power-view.md).

2. In the address bar of the web browser, enter the web portal URL.

    By default, the URL is ``https://[ComputerName]/reports``.

    The report server might be configured to use a specific port. For example, ``https://[ComputerName]:80/reports`` or ``https://[ComputerName]:8080/reports``.

When you start the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], the pages, links, and options that you see vary based on the permissions you have on the report server. 

To perform a task, you must be assigned to a role that includes the task. A user assigned to a role with full permissions has access to the complete set of application menus and pages available for managing a report server. A user assigned to a role with permissions to view and run reports sees only the menus and pages that support those activities. Each user can have different role assignments for different report servers, or even for the various reports and folders that are stored on a single report server.

For more information about roles, see [Grant permissions on a native mode report server](../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md).

### Group by categories

The web portal groups items into different categories. The available categories are:

- KPIs
- Paginated reports
- Power BI Desktop reports
- Excel workbooks
- Datasets
- Data sources
- Resources
::: moniker range="<=sql-server-ver15"
- Mobile reports
::: moniker-end

You can control what is displayed by selecting **View**. 

:::image type="content" source="../reporting-services/media/ssrswebportal-view.png" alt-text="Screenshot of the View list with the Show hidden items option selected.":::

If you select **Show Hidden**, those items display in a lighter color.

:::image type="content" source="../reporting-services/media/ssrswebportal-hidden.png" alt-text="Screenshot of the unavailable Paginated Reports option.":::

### Power BI Desktop reports and Excel workbooks

You can upload, organize, and manage permissions for Power BI Desktop reports and Excel workbooks. The reports are grouped together within the web portal.

:::image type="content" source="../reporting-services/media/web-portal-ssrs-native-mode/ssrs-web-portal-view-power-bi-excel.png" alt-text="Screenshot of the Power BI Desktop Reports section and the Excel Workbooks section.":::

The files are stored within Reporting Services, similar to other resource files. Selecting one of these items downloads them locally to your desktop. You can save changes you make by reuploading them to the report server.

### Search for items

Enter a search term and see everything you can access. The results are categorized into KPIs, reports, datasets, and other items. You can then interact with the results and add them to your favorites.

:::image type="content" source="../reporting-services/media/web-portal-ssrs-native-mode/ssrs-web-portal-search.png" alt-text="Screenshot of the SQL Server Reporting Services portal with the Search box highlighted." lightbox="../reporting-services/media/web-portal-ssrs-native-mode/ssrs-web-portal-search.png":::

## Related content

::: moniker range="<=sql-server-ver15"
- [Create mobile reports with SQL Server Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
::: moniker-end
- [Configure a URL (Report Server Configuration Manager)](../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)  
- [Reporting Services tools](../reporting-services/tools/reporting-services-tools.md)  
- [SQL Server Reporting Services features supported by editions](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md)  

More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
