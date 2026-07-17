---
title: "Start Microsoft Report Builder"
description: Learn how to start Microsoft Report Builder from the SQL Server Reporting Services (SSRS) web portal and create paginated reports.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Report Builder, launching"
  - "launching Report Builder"
  - "SharePoint integration [Reporting Services], starting Report Builder"
  - "starting Report Builder"
# customer intent: As a SQL Server report author, I want to start Microsoft Report Builder from the Reporting Services web portal so that I can create and manage reports.
---
# Start Microsoft Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Learn how to start Microsoft Report Builder from the Reporting Services web portal. Microsoft [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] is a stand-alone report authoring environment. With it, you can create paginated reports and publish them to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server.

The first time you start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, you can [download Report Builder](https://www.microsoft.com/download/details.aspx?id=53613) from the Microsoft Download Center by selecting **Get Report Builder**.

:::image type="content" source="media/start-report-builder/report-builder-get-report-builder.png" alt-text="Screenshot of the We're opening Report Builder message.":::

You or an administrator can install Report Builder on your computer from the Microsoft Download Center. For more information, see [Install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

When you start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the web portal or SharePoint site, if an earlier version of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens, contact your administrator. The administrator can update the version on the web portal or SharePoint site.

## Prerequisites

- SQL Server 2016 (13.x) or later.
- Connection to a report server database.
- Access to the Reporting Services web portal. 

## Start Report Builder from the Reporting Services web portal

1. In your web browser, go to the URL for your report server. By default, the URL is `https://<servername>/reports`.

1. In the web portal's menu bar, select **New** and choose **Paginated Report**.

     :::image type="content" source="media/start-report-builder/web-portal-new-paginated-report.png" alt-text="Screenshot of the New Paginated Report menu.":::

     The first time you start Report Builder with the **Paginated Report** option, you receive the prompt to [install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

     After the first time, [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens, and you can create a paginated report or open a report from the report server.

## Related content

- [Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server.md)
- [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
