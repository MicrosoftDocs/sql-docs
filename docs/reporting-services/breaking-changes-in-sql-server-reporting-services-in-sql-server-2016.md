---
title: "Breaking changes in SQL Server Reporting Services in SQL Server 2016 | Microsoft Docs"
description: Learn about changes in Reporting Services that might break applications, scripts, or functionalities based on earlier versions of SQL Server.
ms.date: 07/02/2017
ms.service: reporting-services
ms.subservice: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "Me.Value references"
  - "Reporting Services, backward compatibility"
  - "breaking changes [Reporting Services]"
ms.assetid: 39c7aafd-dcb9-4317-b8f7-d15828eb4f9a
author: maggiesMSFT
ms.author: maggies
---
# Breaking changes in SQL Server Reporting Services in SQL Server 2016

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE [ssrs-appliesto-not-2017](../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../includes/ssrs-previous-versions.md)]

This topic describes breaking changes in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade, or in custom scripts or reports.

## Security Extensions

Custom security extensions need some modification to work with the new [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)]. Security extensions need to use the IAuthenticationExtension2 interface.

## WMI Provider

The [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] application name changes from "ReportManager" to "ReportServerWebApp".

## Next steps

[Behavior changes to SQL Server Reporting Services in SQL Server 2016](../reporting-services/behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)  
[What's New in Reporting Services (SSRS)](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md)   
[Deprecated features in SQL Server Reporting Services in SQL Server 2016](../reporting-services/deprecated-features-in-sql-server-reporting-services-ssrs.md)    
[Discontinued functionality to SQL Server Reporting Services in SQL Server 2016](../reporting-services/discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)