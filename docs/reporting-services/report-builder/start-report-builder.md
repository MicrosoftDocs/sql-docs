---
title: "Start Microsoft Report Builder"
description: Report Builder is a stand-alone report authoring environment. The first time you start it, Microsoft Download Center prompts you to download it.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/15/2022
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Report Builder, launching"
  - "launching Report Builder"
  - "SharePoint integration [Reporting Services], starting Report Builder"
  - "starting Report Builder"
---
# Start Microsoft Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Microsoft [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] is a stand-alone report authoring environment. With it, you can create paginated reports and publish them to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server.

::: moniker range=">=sql-server-2016 <=sql-server-2016"
> [!NOTE]  
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
::: moniker-end

The first time you start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, you're prompted to [download Report Builder](https://www.microsoft.com/download/details.aspx?id=53613) from the Microsoft Download Center.

:::image type="content" source="media/start-report-builder/report-builder-get-report-builder.png" alt-text="Screenshot of the We're opening Report Builder message.":::

You or an administrator can also [install Report Builder on your computer from the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=219138). For more information, see the **Install Report Builder with Systems Manager Server** section in [Install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

[!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] isn't installed when you install SQL Server Reporting Services. You need to download and install it separately.

When you start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the web portal or SharePoint site, if an earlier version of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens, contact your administrator. The administrator can update the version on the web portal or SharePoint site.

## Start Report Builder from the Reporting Services web portal

1. In your web browser, type the URL for your report server in the address bar. By default, the URL is `https://<servername>/reports`.

1. In the top bar of the web portal, select **New** and choose **Paginated Report**.

     :::image type="content" source="media/start-report-builder/web-portal-new-paginated-report.png" alt-text="Screenshot of the New Paginated Report menu.":::

     The first time, you're prompted to [install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

     After that first time, [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens, and you can create a paginated report or open a report from the report server.

::: moniker range=">=sql-server-2016 <=sql-server-2016"

## Start Report Builder in SharePoint integrated mode

1. Navigate to the SharePoint site that contains the library you want.

1. Open the library.

1. Select **Documents**.

1. On the **New Document** menu, select **Report Builder Report**.

     The first time, this action launches the SQL Server [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] Wizard. For more information, see [Install Report Builder](../../reporting-services/install-windows/install-report-builder.md).

     [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens, and you can create a paginated report or open a report on the report server.

     > [!NOTE]  
     > If the **New Document** menu does not list **Report Builder Report**, **Report Builder Model**, or **Report Data Source**, their content types need to be added to the SharePoint library. For more information, see [Add Reporting Services content types to a SharePoint library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).

::: moniker-end

## Related content

- [Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md)
- [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md)

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
