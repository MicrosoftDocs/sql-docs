---
title: "Supported combinations of SharePoint & Reporting Services server | Microsoft Docs"
description: "A SQL Server Reporting Services report server installed in SharePoint mode requires a version of SharePoint and the SQL Server Reporting Services add-in (rsSharePoint.msi) for SharePoint products, which you install on the SharePoint servers."
ms.date: 08/15/2021
ms.service: reporting-services
ms.custom: seo-lt-2019​, seo-mmd-2019

ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint mode"
  - "add-in for sharepoint"
  - "rsSharePoint"
ms.assetid: dc6a3372-db26-43f0-b7aa-f725acc635c2
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---

# Supported combinations of SharePoint and Reporting Services server

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

A SQL Server Reporting Services report server installed in SharePoint mode requires a version of SharePoint and the SQL Server Reporting Services add-in (rsSharePoint.msi) for SharePoint products, which you install on the SharePoint servers. This topic summarizes the supported combinations.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016. To add a report within a SharePoint site using SQL Server Reporting Services 2017 and later, use the [Report Viewer web part](../../reporting-services/report-server-sharepoint/report-viewer-web-part-sharepoint-site.md). Power View support is no longer available after SQL Server 2017.

## Supported combinations of SharePoint and Reporting Services components

 The following table summarizes the supported combinations of report server, the Reporting Services add-in for SharePoint products, and SharePoint products. Combinations that aren't list in the following table aren't supported

### Supported combinations

|Number|Report server|Add-in|SharePoint version|
|-|-------------------|-------------|------------------------|
|1|SQL Server 2016|SQL Server 2016|SharePoint 2016|
|2|SQL Server 2016|SQL Server 2016|SharePoint 2013|
|3|SQL Server 2014|SQL Server 2014|SharePoint 2013|
|4|SQL Server 2014|SQL Server 2014|SharePoint 2010|
|5|SQL Server 2012 SP4|SQL Server 2014 and SQL Server 2012 SP4|SharePoint 2013|
|6|SQL Server 2012 SP3|SQL Server 2014 and SQL Server 2012 SP3|SharePoint 2013|
|7|SQL Server 2012 SP2|SQL Server 2014 and SQL Server 2012 SP2|SharePoint 2013|
|8|SQL Server 2012 SP1|SQL Server 2014 and SQL Server 2012 SP1|SharePoint 2013|
|9|SQL Server 2012 and SQL Server 2012 SP1*|SQL Server 2014|SharePoint 2010|
|10|SQL Server 2012|SQL Server 2012|SharePoint 2010|
|11|SQL Server 2008 R2|SQL Server 2014|SharePoint 2010|
|12|SQL Server 2008 R2|SQL Server 2012 and SQL Server 2012 SP1 or later|SharePoint 2010|
|13|SQL Server 2008 R2|SQL Server 2008 R2|SharePoint 2010|
|14|SQL Server 2008 R2|SQL Server 2008 SP2|SharePoint 2007|
|15|SQL Server 2008 SP2|SQL Server 2008 R2|SharePoint 2010|
|16|SQL Server 2008 SP2|SQL Server 2008 and SQL Server 2008 SP2|SharePoint 2007|

 *Exception: Power view integration isn't supported.

 For links to the add-in download pages, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  

 **Additional considerations:**

- Be sure to upgrade all of the SharePoint servers within the farm. This includes the App and Web Front End servers.

- SharePoint 2016 support, including Power view integration, requires the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in version of SQL Server 2016 or later.

- SharePoint 2013 support, including Power view integration, requires the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in version of SQL Server 2012 SP1 or later.

- Power View was introduced in SQL Server 2012. Therefore, Power View integration with SharePoint 2010 requires the SQL Server 2012 or later of the add-in.

- The SQL Server 2008 R2 Add-In isn't supported by SQL Server 2012 (or later) report servers. The SharePoint 2010 prerequisite installer automatically installs the SQL Server 2008 R2 Add-In. It must be uninstalled before installing newer versions of the add-in. In place upgrade of the add-in isn't supported.

- **Upgrade:** SharePoint 2010 with the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-In installed, cannot be upgraded in-place to SharePoint 2013. SharePoint 2013 requires SQL Server 2012 SP1 or later of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in and report server. For more information on upgrade, see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).

## Next steps

 [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)   
 [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-components-of-sql-server-2017.md)   
 [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)