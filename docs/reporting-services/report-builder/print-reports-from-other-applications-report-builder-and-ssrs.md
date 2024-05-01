---
title: "Print reports from other applications (Report Builder)"
description: Report Builder lets you export to view a report in other applications. For printing, export a report if the application has print features that you want to use.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Print reports from other applications (Report Builder)

  Report Builder provides an export option that allows you to easily view a report in other applications. The **Export** command is available on the report toolbar that appears at the top of a report when you open it in a browser or web-based application. Exporting a report displays it in a different application (for example, exporting a report to Excel opens the report in [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)]). For printing purposes, exporting a report is recommended only if the application has specific printing features that you want to use.

To export a report to another application, you must have that application installed. For example, you must have Adobe Acrobat Reader installed on your computer before you can export to the Acrobat (PDF) format. If you choose to export a report to TIFF format, the report server places the report in a viewing application that is associated with the TIFF file type. Although the application used depends on which version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows you have, typically this tool is Windows Picture and Fax Viewer. The default resolution corresponds to a screen resolution of 96 dots per inch (DPI). You can increase the resolution in Windows Picture and Fax Viewer to 300 DPI or 600 DPI to match the capabilities of your printer. For more information about adjusting the resolution, see the Windows product documentation.

If you choose web archive, also known as MHTML, the report is exported to your default browser. Printing from the browser might result in report path information being added at the bottom of every page. In most cases, you can set browser options to omit path information on a printed page. For more information, see the product documentation for the browser you're using.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## Related content

- [Print a report (Report Builder)](../../reporting-services/report-builder/print-a-report-report-builder-and-ssrs.md)
- [Print reports from a browser with the print control (Report Builder)](../../reporting-services/report-builder/print-reports-from-a-browser-with-the-print-control-report-builder-and-ssrs.md)
- [Export reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Export a report as another file type (Report Builder)](/previous-versions/sql/)
- [Find, view, and manage reports (Report Builder)](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
