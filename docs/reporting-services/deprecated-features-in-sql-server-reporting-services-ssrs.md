---
title: "Deprecated features in SQL Server Reporting Services | Microsoft Docs"
description: Learn about the deprecated features in the different versions of SQL Server Reporting Services. The features are still available in the release in which they are deprecated. 
ms.date: 11/2/2022
ms.service: reporting-services
ms.subservice: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, backward compatibility"
  - "deprecated features [Reporting Services]"
  - "HTML OWC rendering extension [Reporting Services]"
  - "Report Server Web service, endpoints"
ms.assetid: 3876c01e-f81d-4cce-9104-5106a8c369e6
author: maggiesMSFT
ms.author: maggies
---

# Deprecated features in SQL Server Reporting Services

This article describes deprecated features in the different versions of SQL Server Reporting Services. The features are still available in the release in which they are deprecated; however the features are scheduled to be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Don't use deprecated features in new applications.

When we mark a feature as deprecated, it means:

- The feature is in maintenance mode only. We'll make no new changes, including changes related to interoperability with new features.
- We strive not to remove a deprecated feature from future releases, to make upgrades easier. However, in rare situations, we may choose to permanently remove the feature from Reporting Services if it limits future innovations.
- For new development work, we don't recommend using deprecated features.

::: moniker range=">=sql-server-ver16"
## Deprecated features in SQL Server 2022 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2022-and-later](../includes/ssrs-appliesto-2022-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirs](../includes/ssrs-appliesto-pbirs.md)]

SQL Server 2022 Reporting Services and Power BI Report Server September 2022 support the following features, but they are now deprecated. These features will be removed in a future release, however, the specific version of SQL Server and Power BI Report Server hasn't been determined.

| **Category** | **Deprecated feature** | **Replacement** |
| --- | --- | --- |
| Report Server | Report Part Gallery | None |

::: moniker-end

::: moniker range=">= sql-server-ver15"
## Deprecated features in SQL Server 2019 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2019-and-later](../includes/ssrs-appliesto-2019-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirs](../includes/ssrs-appliesto-pbirs.md)]

**Features deprecated in a future version of SQL Server**

SQL Server Reporting Services supports the following features in the next version of SQL Server, but will deprecate them in a later version. The specific version of SQL Server hasn't been determined.

| **Category** | **Deprecated feature** | **Replacement** |
| --- | --- | --- |
| Report Server | Report Part Gallery | None |
| Report Server | Mobile Reports and Mobile Report Publisher | Power BI reports in Power BI Report Server offer mobile capabilities. |
| Report Server | XLS and DOC render formats | XLSX and DOCX formats are available and supported. |
| Report Server | Atom Data Feed | oData feed support is available for shared datasets in SSRS and Power BI Report Server. |
| Report Server | Pin to Power BI | Paginated report support is now available directly in the Power BI service.  |

::: moniker-end

::: moniker range=">= sql-server-2017"
## Deprecated features in SQL Server 2017 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017](../includes/ssrs-appliesto-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

The following SQL Server 2017 Reporting Services features will be deprecated in the next version of SQL Server. Don't use these features in new development work, and modify applications that currently use these features as soon as possible.

> [!NOTE]
> This list is identical to the SQL Server 2016 Reporting Services (13.x) list. There are no new deprecated or discontinued features announced for SQL Server 2017 Reporting Services (14.x).

| **Category** | **Deprecated feature** | **Replacement** |
| --- | --- | --- |
| Report Server | HTML 4.0 Renderer. | HTML 5 renderer |

::: moniker-end

## Deprecated features in SQL Server 2016 Reporting Services

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../includes/ssrs-previous-versions.md)]

The following SQL Server Reporting Services features won't be supported in the next version of SQL Server. Don't use these features in new development work, and modify applications that currently use these features as soon as possible.

|Category|Deprecated feature|
|--------------|------------------------| 
|Report Server|HTML4.0 renderer. Use the HTML5 renderer.|

## Features Not Supported in Previous Versions of SQL Server Reporting Services

SQL Server 2014 Reporting Services Deprecated Features
- [SQL Server 2014 Reporting Services Deprecated Features](deprecated-features-in-sql-server-reporting-services-ssrs.md)

## Next steps

 [What's New in Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md) 
 [Backward Compatibility | Reporting Services](../reporting-services/reporting-services-backward-compatibility.md)   
 [Behavior Changes to SQL Server Reporting Services in SQL Server 2016](../reporting-services/behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)  
 [Discontinued Functionality to SQL Server Reporting Services in SQL Server 2016](../reporting-services/discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md) 

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
