---
title: Support for Report Viewer control versions
description: The Microsoft Report Viewer control is compatible with SQL Server Reporting Services and Power BI Report Server that follow the modern support lifecycle policy.
author: maggiesMSFT
ms.custom: seo-lt-2019
ms.author: maggies
ms.reviewer: jonhp
ms.service: reporting-services
ms.subservice: application-integration
ms.topic: reference
ms.date: 12/01/2020
---
# Support for Report Viewer current branch versions

**_Applies to: Microsoft Report Viewer versions 150.900.148 and later_**

The **Microsoft Report Viewer control** is compatible with SQL Server Reporting Services and Power BI Report Server that follow the Microsoft modern [support lifecycle policy](https://support.microsoft.com/hub/4095338/microsoft-lifecycle-policy). This information applies to both the **ASP.NET** and **WinForms** versions distributed through [NuGet](https://www.nuget.org/). All released versions are available through [NuGet](https://www.nuget.org/). Patches, features, or other updates are rolled-forward to the latest version. Latest versions must be applied to receive changes. The Report Viewer continues to receive **Security and Critical Updates**, with at least one year advanced notice of any support policy changes.

For a version history of the Report Viewer control, see the following links:

- [Windows Forms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/)
- [ASP.NET Web Forms](https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.WebForms/)

## Application server and report server combinations

Some features of the Report Viewer control rely on the operating system's default behaviors. Thus, they may require running the same version for both the client (the application server running the Report Viewer control) and the server (running Reporting Services). The following combinations of application server and report server are supported:

| Application server | Report server |
| :----------------- | :------ |
| Windows Server 2012 | Windows Server 2012 |
| Windows Server 2012 | Windows Server 2012 R2 |
| Windows Server 2012 R2 | Windows Server 2012 R2 |
| Windows Server 2012 R2 | Windows Server 2012 |
| Windows Server 2016 and later | Windows Server 2016 and later |

## Next steps

For more information about the Report Viewer control, see [Get started integrating Reporting Services using the Report Viewer controls](integrating-reporting-services-using-reportviewer-controls-get-started.md).
