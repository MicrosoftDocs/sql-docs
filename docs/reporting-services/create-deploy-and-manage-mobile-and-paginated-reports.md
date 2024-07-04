---
title: "What is SQL Server Reporting Services?"
description: "Learn how to create, deploy, and manage reports by using SQL Server Reporting Services (SSRS) to deliver data insights through paginated reports to web portals."
author: maggiesMSFT
ms.author: maggies
ms.date: 07/03/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: overview
ms.custom:
  - intro-overview
  - updatefrequency5
helpviewer_keywords:
  - "reports [Reporting Services]"
  - "SSRS"
  - "reports [Reporting Services], about reports"
  - "Reporting Services"
  - "SQL Server Reporting Services"
#customer intent: As a SQL Server user, I want to learn how I can manage reports by using SQL Server Reporting Services (SSRS) paginated reports so that I can deliver paginated reports.
---

# What is SQL Server Reporting Services (SSRS)?

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

SQL Server Reporting Services (SSRS) provides a set of on-premises tools and services to create, deploy, and manage paginated reports. Download [**SQL Server 2022 Reporting Services**](https://www.microsoft.com/download/details.aspx?id=104502) from the Microsoft Download Center, and then [install Reporting Services](../reporting-services/install-windows/install-reporting-services.md).

:::image type="content" source="../reporting-services/media/report-server-2022-coho-winery.png" alt-text="Screenshot of a SQL Server 2022 Reporting Services report.":::

Looking for Power BI Report Server? See [What is Power BI Report Server?](/power-bi/report-server/get-started)

## Create, deploy, and manage reports

SSRS makes it easy to deliver the right information to the right users. You can view reports in a web browser on your computer, mobile device, or receive them via email.

SSRS offers an updated suite of products:

* **Paginated reports**: Create modern-looking reports with updated tools and new features.
* **A modern web portal**: Organize and display paginated Reporting Services reports and KPIs in any modern browser. Store Excel workbooks on the portal as well.
::: moniker range="<=sql-server-ver15"
* **Mobile reports**: Create reports with responsive layouts that adapt to different devices and orientations.
::: moniker-end

## Paginated reports

:::image type="content" source="../reporting-services/media/ssrs-paginated-reports.png" alt-text="Diagram of paginated reports on a desktop screen and a tablet device.":::

Paginated reports are perfect for fixed-layout documents optimized for printing, such as PDFs and Word files. You can create innovative reports with newly updated features, by using [Report Builder](../reporting-services/install-windows/install-report-builder.md), or [Report Designer](../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md) in [SQL Server Data Tools (SSDT)](../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md). We updated the core BI workload with:

* **New styles and color palettes**: Create reports with a fresh, minimalist style by default.
* **Updated Parameter pane**: Arrange parameters exactly the way you want them.
* **Export to new formats**: Export reports to PowerPoint with live, editable visualizations.
* **Hybrid Power BI/SSRS experience**: Pin visuals from SSRS reports to your Power BI dashboards and monitor everything in one place.

::: moniker range="<=sql-server-ver15"

## Mobile reports

:::image type="content" source="../reporting-services/media/ssrs-mobile-reports.png" alt-text="Diagram of mobile reports on a desktop screen and a tablet device.":::

Mobile computing transforms how we work, creating new reporting needs. Fixed-layout reports don't work well on tablets and phones. They're designed for wide PC screens, not small, portrait, or landscape screens.

To meet these demands, mobile reports have responsive layouts that adapt to different devices and orientations. Based on Datazen technology, these reports ensure your data looks great no matter where you view it. You can even migrate existing Datazen reports to Reporting Services with the [SQL Server Migration Assistant for Datazen](https://www.microsoft.com/download/details.aspx?id=53128).

Create these mobile reports with the [Mobile Report Publisher](../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) app. Whether you're on Windows, iOS, Android, or HTML5, you can access your data in Power BI, the cloud, or SSRS through the [Power BI apps for mobile devices](https://powerbi.microsoft.com/documentation/powerbi-power-bi-apps-for-mobile-devices/).

As you build your visualizations, Mobile Report Publisher automatically generates sample data, so you can see exactly how your data looks and which types of data work best in each visualization.

::: moniker-end

## Web portal

:::image type="content" source="../reporting-services/media/report-server-2022-web-portal.png" alt-text="Screenshot of the Reporting Services web portal.":::

For Reporting Services, the front door is a modern web portal. The web portal is a complete redesign of Report Manager. Now, you can access all your Reporting Services reports and KPIs in the new portal. KPIs can surface key business metrics immediately in the browser without opening a report. 

The web portal is a sleek, single-page, standards-based HTML5 app that works with all major browsers, including Microsoft Edge, Internet Explorer 10 and 11, Chrome, Firefox, and Safari. Access your SSRS reports and KPIs in one place. The content on the web portal is organized by type:

* Paginated reports
* KPIs
* Excel workbooks
* Shared datasets
* Shared data sources
::: moniker range="<=sql-server-ver15"
* Mobile reports
::: moniker-end

Store and manage your content securely the traditional folder hierarchy. Tag your favorite reports for quick access. Those users with appropriate permissions can manage and administer SSRS content. Schedule report processing, access reports on demand, and subscribe to published reports

To learn more, see [The web portal of a report server - SSRS Native mode](../reporting-services/web-portal-ssrs-native-mode.md).

::: moniker range="=sql-server-2016"

## Reporting Services in SharePoint integrated mode

You can publish your reports with Reporting Services in SharePoint integrated mode. Schedule report processing, access reports on demand, subscribe to published reports, and export them to applications like Microsoft Excel. You can also create data alerts on reports published to a SharePoint site and receive email notifications when report data changes.

To learn more, see [Reporting Services report server in SharePoint integrated mode](../reporting-services/report-server-sharepoint/reporting-services-report-server-sharepoint-mode.md).

::: moniker-end

## [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] programming features

Extend and customize your reporting functionality with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] programming features. Use the SSRS APIs to integrate or extend data and report processing in your custom applications.

To learn more, see [Reporting Services developer documentation](../reporting-services/reporting-services-developer-documentation.md).

## Related content

- Keep up with new SSRS features: [What's new in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md).

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
