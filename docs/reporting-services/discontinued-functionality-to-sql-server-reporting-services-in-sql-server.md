---
title: Discontinued functionality in SQL Server Reporting Services (SSRS)
description: Learn details about SQL Server Reporting Services features that are no longer available in different versions of Reporting Services.
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
author: maggiesMSFT 
ms.author: maggies
ms.reviewer: ""
ms.custom: seodec18
ms.date: 11/2/2022
---

# Discontinued functionality in SQL Server Reporting Services (SSRS)

This article describes SQL Server Reporting Services features that are no longer available in different versions of SQL Server. It doesn't include announcements about discontinued support for specific versions of the operating system or Microsoft Internet Information Services (IIS). For more information about system prerequisites, see [Planning a SQL Server Installation](../sql-server/install/planning-a-sql-server-installation.md).

A *discontinued feature* is one that is no longer supported. It might also be physically removed from the product. The following features are discontinued.

::: moniker range=">=sql-server-ver16"

## Discontinued functionality in SQL Server 2022 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2022-and-later](../includes/ssrs-appliesto-2022-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirs](../includes/ssrs-appliesto-pbirs.md)]

These features are discontinued and no longer available in SQL Server 2022 Reporting Services and Power BI Report Server September 2022.

| **Category** | **Discontinued feature** | **Replacement** |
| --- | --- | --- |
| Report Server | Mobile Reports and Mobile Report Publisher | Power BI reports in Power BI Report Server offer mobile capabilities. |
| Report Server | Pin to Power BI | Paginated report support is now available directly in the Power BI service.  |

These features are discontinued and no longer available in SQL Server 2022 Reporting Services.

| **Category** | **Discontinued feature** | **Replacement** |
| --- | --- | --- |
| Report Server | XLS and DOC render formats | XLSX and DOCX formats are available and supported. |
| Report Server | Atom Data Feed | oData feed support is available for shared datasets in SSRS and Power BI Report Server. |

::: moniker-end

::: moniker range=">=sql-server-ver15"

## Discontinued functionality in SQL Server 2019 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2019-and-later](../includes/ssrs-appliesto-2019-and-later.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

These features that are no longer available in SQL Server 2019 Reporting Services. 

| Category | Discontinued feature | Replacement |
| --- | --- | --- |
| Report Server | HTML 4.0 Renderer | HTML 5 renderer |
| Report Server | Customized style sheets for HTML Viewer and  Report Manager | You can still [brand the web portal](branding-the-web-portal.md). |

::: moniker-end

::: moniker range=">=sql-server-2016"

## Discontinued functionality in SQL Server 2016 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

These features that are no longer available in SQL Server 2016 Reporting Services. 

|Feature|Replacement or workaround|
|-|-|
|Upload report models through the web portal|
|Manage report models through the web portal|
|Customized style sheets for HTML Viewer and Report Manager|You can still [brand the web portal](branding-the-web-portal.md).|
|Install multiple instances of Reporting Services on a single server|

::: moniker-end

[!INCLUDE [ssrs-previous-versions](../includes/ssrs-previous-versions.md)]

## Next steps

* [What's New in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md)  
* [Behavior Changes to SQL Server Reporting Services in SQL Server 2016](../reporting-services/behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)  
* [Deprecated features in SQL Server Reporting Services in SQL Server 2016](../reporting-services/deprecated-features-in-sql-server-reporting-services-ssrs.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
