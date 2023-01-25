---
title: "Release Notes for Report Viewer controls"
description: The release notes for the Report Viewer controls of WebForms and WinForms, related to Reporting Services.
ms.date: 08/30/2022
ms.service: reporting-services
ms.subservice: application-integration
ms.custom: seo-lt-2019

ms.topic: reference
ms.assetid: 112e0240-351d-46a9-98c7-2be09f26ac60
ms.reviewer: maggies
author: RhysSchmidtke
ms.author: petebro
---
# Release Notes for Report Viewer controls for WebForms and WinForms of SSRS

These are the release notes for the Report Viewer controls of WebForms and WinForms, related to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS).

For the release notes for SSRS, see [Release notes for SQL Server Reporting Services (SSRS) 2017 and later](../release-notes-reporting-services.md).

## 150.1537.0
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Fixed an issue with datepicker closing on certain size screens. |
|           | Fixed many screen reader issues. |

## 150.1484.0 
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Fixed an issue with print button not displaying correctly. |
|           | Fixed a viewer issue where only toolbar was visible in some specific scenarios. |

## 150.1427.0
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Fixed an issue with links not looking visited after being clicked. |
|           | Fixed a datetime parsing issue affecting certain locales. |

## 150.1404.0
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Fixed an issue with tab ordering of the toolbar in WebForms. |
|           | Improved HTML rendering accessibility for tables. |

## 150.1400.0
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Fixed an issue where the viewer control would not load in design mode. |

## 150.1358.0
| Change description | Details |
| :----------------- | :------ |
| Bug Fixes | Reverted a change that removed the Microsoft.ReportViewer.Design assemblies from the project references. |
|           | As part of other changes, two assemblies were changed from 15.0 version to 15.3. This has been reverted. |

## 150.1357.0
| Change description | Details |
| :----------------- | :------ |
| Bug fixes  | Proper print preview for High DPI monitor |
|            | Print dialog would show outside of visible space |
|            | Large number of parameters resulted in parameter scroll bars and drop-down lists not working correctly |
|            | Fixed issue with Null and date time parameters |
|            | Updated JQuery to version 3.3.1 |
|            | Fixed overlapping with tablix cells in HTML rendering |
|            | Removed the design time project references to eliminate erroneous VS assemblies being added to the projects |
|            | Accessibility fix for tool bar to narrate only for active items |

## 150.900.148

| Change description | Details |
| :----------------- | :------ |
| Fix for a bug preventing reports without parameters to be loaded through **Server.LoadReportDefinition**. | &nbsp; |
| The WebForms Report Viewer Control. | Supports embedding in RTL pages (pages that change the text flow using the *direction: rtl;* css property).<br/><br/>Supports customizing print dialog text via the *IReportViewerMessages5* localization interface.<br/><br/>Improved accessibility support.<br/><br/>&bull; &nbsp; &nbsp; [NuGet package for the Report Viewer control of WebForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Webforms/150.900.148) |
| The WinForms Report Viewer Control. | Fix for printing when an app is running in a High DPI mode.<br/><br/>&bull; &nbsp; &nbsp; [NuGet package for the Report Viewer control of WinForms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/150.900.148) |

## Next steps

[Getting started](integrating-reporting-services-using-reportviewer-controls-get-started.md) with the Report Viewer controls.

More questions? [Try the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
