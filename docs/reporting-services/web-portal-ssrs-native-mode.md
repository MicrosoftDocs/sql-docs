---
title: "The web portal of a report server (Native Mode) | Microsoft Docs"
ms.date: 08/16/2022
ms.service: reporting-services
ms.subservice: reporting-services
description: The web portal of a Reporting Services report server is a web-based experience for viewing reports, KPIs, and navigating through the elements in your report server instance.
ms.topic: conceptual
ms.assetid: 7349e626-6ed5-4d21-b05f-cf042ad9ad70
author: maggiesMSFT
ms.author: maggies
---
# The web portal of a report server (SSRS Native Mode)

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

The web portal of a Reporting Services report server is a web-based experience. In the portal, you can view reports, KPIs, and navigate through the elements in your report server instance. You can also use the web portal to administer a single report server instance.

![Screenshot showing SQL Server Reporting Services web portal.](../reporting-services/media/web-portal-report-server-2022.png)

## Tasks in the web portal

Use the web portal for these and other tasks:

- View, search, print, and [subscribe to reports](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).
- Create, secure, and maintain the folder hierarchy to organize items on the server.
- Configure role-based security that determines access to items and operations. See [Role definitions - predefined roles](security/role-definitions-predefined-roles.md) for details.
- Configure report execution properties, report history, and [report parameters](report-design/report-parameters-concepts-report-builder-and-ssrs.md).
- Create [shared schedules](subscriptions/schedules.md) and shared data sources to make schedules and data source connections more manageable.
- Create [data-driven subscriptions](subscriptions/create-modify-and-delete-data-driven-subscriptions.md) that role out reports to a large recipient list.
- Create [linked reports](reports/create-a-linked-report.md) to reuse and repurpose an existing report in different ways.
- Download common tools such as [Report Builder](install-windows/install-report-builder.md).
- [Create KPIs](../reporting-services/working-with-kpis-in-reporting-services.md).
- Create [branding for the web portal](../reporting-services/branding-the-web-portal.md).
- Work with [shared datasets](../reporting-services/work-with-shared-datasets-web-portal.md).
- Send feedback or make feature requests.

You can use the web portal to browse the report server folders or search for specific reports. You can view a report, its general properties and past copies of the report that are captured in report history. Depending on your permissions, you might also be able to subscribe to reports for delivery to an e-mail inbox or a shared folder on the file system.

> [!NOTE]
> For information on supported browsers and versions, see [Planning for Reporting Services Browser Support](../reporting-services/browser-support-for-reporting-services-and-power-view.md).

The web portal is used only for a report server that runs in native mode. It is not supported for a report server that you configure for SharePoint integrated mode.

Some web portal features are only available in specified editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Reporting Services Features supported by the Editions of SQL Server](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).

On a new installation, only local administrators have sufficient permissions to work with content and settings. To grant permissions to other users, a local administrator must create role assignments that provide access to the report server. The application pages and tasks that a user can subsequently access will depend on the role assignments for that user. For more information, see [Grant User Access to a Report Server](./security/grant-user-access-to-a-report-server.md)

> [!NOTE]
> If you are browsing to the web portal on the local machine that the server is running on, you may see a message indicating that you are not allowed to view this folder. This is due to Universal Access Control (UAC) and that you are not running the browser as an admin. You will need to give your account content manager rights on the folder by either browsing to the server remotely, or use Edge locally to configure the permissions. You are not able to run Microsoft Edge as an admin via the "Run as administrator" context menu. However, you can run Edge as the local administrator account by holding SHIFT + right-clicking on the edge shortcut and selecting "Run as a different user" then providing the local machine administrator account information in the login dialog. If you want to use the web portal remotely, you will need to give your account content manager rights on the folder.  

## Start and use the web portal

The web portal is a web application that you open by typing the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] URL in the address bar of the browser window. When you start the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], the pages, links, and options that you see will vary based on the permissions you have on the report server. To perform a task, you must be assigned to a role that includes the task.  A user who is assigned to a role that has full permissions has access to the complete set of application menus and pages available for managing a report server. A user assigned to a role that has permissions to view and run reports sees only the menus and pages that support those activities. Each user can have different role assignments for different report servers, or even for the various reports and folders that are stored on a single report server.

For more information about roles, see [Granting Permissions on a Native Mode Report Server](../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md).

### Start the web portal

To start the web portal from a browser, follow these steps:

1. Open your web browser. For a list of supported web browsers, see [Planning for Reporting Services Browser Support](../reporting-services/browser-support-for-reporting-services-and-power-view.md).

2. In the address bar of the web browser, type the web portal URL.

    By default, the URL is *https://[ComputerName]/reports*.

    The report server might be configured to use a specific port. For example, *https://[ComputerName]:80/reports* or *https://[ComputerName]:8080/reports*.

## Grouping by categories

The web portal will group items into different categories. The available categories are the following.

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

You can control what is displayed by selecting **View** in the upper right. If you select Show Hidden, those items will be displayed in a lighter color.

![Screenshot of the View dropdown with the Show hidden items option selected.](../reporting-services/media/ssrswebportal-view.png)

![Screenshot showing the unavailable Paginated Reports option.](../reporting-services/media/ssrswebportal-hidden.png)

### Power BI Desktop reports and Excel workbooks

You can upload, organize, and manage permissions for Power BI Desktop reports and Excel workbooks. They're grouped together within the web portal.

![Screenshot showing the Power BI Desktop Reports section and the Excel Workbooks section.](../reporting-services/media/web-portal-ssrs-native-mode/ssrs-web-portal-view-power-bi-excel.png)

The files are stored within Reporting Services, similar to other resource files. Selecting one of these items downloads them locally to your desktop. You can save changes you've made by reuploading them to the report server.

## Search for items

Enter a search term, and see everything you can access. The results are categorized into KPIs, reports, datasets, and other items. You can then interact with the results and add them to your favorites.

![Screenshot showing  the SQL Server Reporting Servers portal with the Search text box called out.](../reporting-services/media/web-portal-ssrs-native-mode/ssrs-web-portal-search.png)

## See also

::: moniker range="<=sql-server-ver15"
[Create mobile reports with SQL Server Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
::: moniker-end
[Configure a URL (Report Server Configuration Manager)](../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)  
[Reporting Services Tools](../reporting-services/tools/reporting-services-tools.md)  
[Planning for Reporting Services Browser Support](../reporting-services/browser-support-for-reporting-services-and-power-view.md)  
[Reporting Services Features supported by the Editions of SQL Server](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server-2016.md)  

More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
