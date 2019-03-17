---
title: "Changelog for the Reporting Services Report Viewer controls"
ms.date: 09/19/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: application-integration


ms.topic: reference
ms.assetid: 112e0240-351d-46a9-98c7-2be09f26ac60
author: RhysSchmidtke
ms.author: rhys
---
# Changelog for the Report Viewer controls

This changelog tracks release of the WinForms and WebForms Report Viewer controls.

## 15.900.148
With this update
 - Fix for a bug preventing reports without parameters to be loaded through Server.LoadReportDefinition

The [WebForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Webforms/150.900.148) Report Viewer Control
 - Supports embedding in RTL pages (pages that change the text flow using the *direction: rtl;* css property).
 - Supports customizing print dialog text via the *IReportViewerMessages5* localization interface.
 - Improved accessibility support.

The [WinForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/150.900.148) Report Viewer Control
 - Fix for printing when an app is running in a High DPI mode.

## Next steps

[Getting started](integrating-reporting-services-using-reportviewer-controls-get-started.md) with the Report Viewer controls  
More questions? [Try the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
