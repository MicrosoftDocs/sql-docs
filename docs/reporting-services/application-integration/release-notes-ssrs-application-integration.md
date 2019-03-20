---
title: "Release Notes for the Report Viewer controls of SSRS"
ms.date: 09/20/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: application-integration

ms.topic: reference
ms.assetid: 112e0240-351d-46a9-98c7-2be09f26ac60
ms.reviewer: maghan
author: RhysSchmidtke
ms.author: rhys
---
# Release Notes for the Report Viewer controls for WebForms and WinForms of SSRS

These are the release notes for the Report Viewer controls of WebForms and WinForms, related to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS).

For the release notes for SSRS, see [Release notes for SQL Server Reporting Services (SSRS) 2017 and later](../release-notes-reporting-services.md).

## 15.900.148

| Change description | Details |
| :----------------- | :------ |
| Fix for a bug preventing reports without parameters to be loaded through **Server.LoadReportDefinition**. | &nbsp; |
| The WebForms Report Viewer Control. | Supports embedding in RTL pages (pages that change the text flow using the *direction: rtl;* css property).<br/><br/>Supports customizing print dialog text via the *IReportViewerMessages5* localization interface.<br/><br/>Improved accessibility support.<br/><br/>&bull; &nbsp; &nbsp; [NuGet package for the Report Viewer control of WebForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Webforms/150.900.148) |
| The WinForms Report Viewer Control. | Fix for printing when an app is running in a High DPI mode.<br/><br/>&bull; &nbsp; &nbsp; [NuGet package for the Report Viewer control of WinForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/150.900.148) |
| &nbsp; | &nbsp; |

## Next steps

[Getting started](integrating-reporting-services-using-reportviewer-controls-get-started.md) with the Report Viewer controls.

More questions? [Try the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
