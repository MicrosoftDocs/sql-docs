---
title: "What is SQL Server Reporting Services"
description: "Learn about tools and services for Reporting Services reports on premises."
author: maggiesMSFT
ms.author: maggies
ms.date: 06/18/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - intro-overview
  - updatefrequency5
helpviewer_keywords:
  - "reports [Reporting Services]"
  - "SSRS"
  - "reports [Reporting Services], about reports"
  - "Reporting Services"
  - "SQL Server Reporting Services"
---

# What is SQL Server Reporting Services (SSRS)?

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

SQL Server Reporting Services (SSRS) provides a set of on-premises tools and services that create, deploy, and manage paginated reports. Download [**SQL Server 2022 Reporting Services**](https://www.microsoft.com/download/details.aspx?id=104502) from the Microsoft Download Center.

:::image type="content" source="../reporting-services/media/report-server-2022-coho-winery.png" alt-text="Screenshot of a SQL Server 2022 Reporting Services report.":::

Looking for Power BI Report Server? See [What is Power BI Report Server?](/power-bi/report-server/get-started)

## Create, deploy, and manage reports

The SSRS solution flexibly delivers the right information to the right users. Users can consume the reports in a web browser on their computer or mobile device, or via email.

SQL Server Reporting Services offers an updated suite of products:

* **Paginated reports** are up to date, so you can create modern-looking reports, with updated tools and new features for creating them.
* **A modern web portal** you can view in any current browser. In the new portal, you can organize and display paginated Reporting Services reports and KPIs. You can also store Excel workbooks on the portal.
::: moniker range="<=sql-server-ver15"
* **New mobile reports** with a responsive layout that adapts to a variety of devices and the different ways you hold them.
::: moniker-end

Read on for more about each.

### What's new in Reporting Services

See [What's new in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md) to keep up to date on new features in SQL Server Reporting Services.

## Paginated reports

:::image type="content" source="../reporting-services/media/ssrs-paginated-reports.png" alt-text="Diagram of paginated reports on a desktop screen and a tablet device.":::

The Reporting Services set of tools is associated with paginated reports, ideal for fixed-layout documents optimized for printing, such as PDF and Word files.

That core BI workload still exists today, so we modernized it. Now you can create modern-looking reports with updated new features, using Report Builder, or Report Designer in [Reporting Services in SQL Server Data Tools (SSDT)](../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

* We updated all the default styles and color palettes, so by default you create reports with a new minimalist modern style.
* We updated the Parameter pane, so you can arrange parameters however you want.
* You can export to new formats such as PowerPoint. Reporting Services visualizations in PowerPoint are live and editable, not just screenshots.
* You can create a hybrid Power BI/Reporting Services experience. Rather than recreating your on-premises Reporting Services reports in Power BI, you can pin visuals from those reports to your Power BI dashboards. Then you can monitor everything in one place on your Power BI dashboard.

::: moniker range="<=sql-server-ver15"

## Mobile reports

:::image type="content" source="../reporting-services/media/ssrs-mobile-reports.png" alt-text="Diagram of mobile reports on a desktop screen and a tablet device.":::

Mobile computing shifted the devices we need to work, meaning people today have a different reporting need. The fixed-layout report experience doesn't work well when you introduce tablets and phones. Something designed for a wide PC screen isn't the optimal experience on a small phone screen that's not just smaller but a portrait or landscape orientation.

What you need with these widely different screen form factors is a responsive layout that adapts to these different screen sizes and orientations. To fulfil that need, we added a new report type: mobile reports, which is based on the Datazen technology we acquired about a year ago and integrated into the product. You can migrate your existing Datazen reports to Reporting Services with the [SQL Server Migration Assistant for Datazen](https://www.microsoft.com/download/details.aspx?id=53128).

You create these mobile reports in the new [Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) app. Then in the native [Power BI apps for mobile devices](https://powerbi.microsoft.com/documentation/powerbi-power-bi-apps-for-mobile-devices/) for Windows, iOS, Android, and HTML5, you can access the data you have in Power BI, the cloud, or SSRS.

As you create visualizations, Mobile Report Publisher automatically generates sample data. This feature allows you to see how the visualization looks with your data, and what kind of data works well in each visualization.
::: moniker-end

## Web portal

:::image type="content" source="../reporting-services/media/report-server-2022-web-portal.png" alt-text="Screenshot of the Reporting Services web portal.":::

For end users of Reporting Services, the front door is a modern web portal you can view in most browsers. You can access all your Reporting Services reports and KPIs in the new portal. KPIs can surface key business metrics at a glance in the browser, without having to open a report.

The new web portal is a complete rewrite of Report Manager. Now it's a single-page, standards-based HTML5 app, which modern browsers are optimized for: Microsoft Edge, Internet Explorer 10 and 11, Chrome, Firefox, Safari, and all the major browsers.

The content on the web portal is organized by type:

* paginated reports
* KPIs
* Excel workbooks
* shared datasets
* shared data sources
::: moniker range="<=sql-server-ver15"
* Mobile reports
::: moniker-end

You can store and manage them securely here, in the traditional folder hierarchy. Tag your favorites reports for quick access. If you have appropriate permissions, you can manage and administer SSRS content.

And you can still schedule report processing, access reports on demand, and subscribe to published reports in the new web portal.

Learn more about [The web portal of a report server (SSRS Native Mode)](../reporting-services/web-portal-ssrs-native-mode.md).

::: moniker range="=sql-server-2016"

## Reporting Services in SharePoint integrated mode

You publish reports to Reporting Services in SharePoint integrated mode. You can schedule report processing, access reports on demand, subscribe to published reports, and export reports to other applications such as Microsoft Excel. Create data alerts on reports published to a SharePoint site and receive email messages when report data changes.

Learn more about [Reporting Services report server in SharePoint integrated mode](../reporting-services/report-server-sharepoint/reporting-services-report-server-sharepoint-mode.md).

::: moniker-end

## [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] programming features

Take advantage of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] programming features so you can extend and customize your reporting functionality. Use the SSRS APIs to integrate or extend data and report processing in custom applications.

See more [Reporting Services developer documentation](../reporting-services/reporting-services-developer-documentation.md).

## Related content

* [Install Reporting Services](../reporting-services/install-windows/install-reporting-services.md)
* [Download SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)
* [Install Microsoft Report Builder](../reporting-services/install-windows/install-report-builder.md)

More questions? Try asking the [Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
