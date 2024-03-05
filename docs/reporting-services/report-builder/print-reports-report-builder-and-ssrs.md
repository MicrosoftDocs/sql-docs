---
title: "Print reports"
description: You can view and print a report from the web portal or an application that you use to view it. The client computer performs print processing on demand.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/24/2018
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Print reports

  After you save a report to a report server, you can view and print the report from the web portal or any application that you use to view an exported report. Before saving a report, you can print it when you preview it.

All print processing is performed on demand and on the client computer. There's no server-side print functionality that enables you to route a print job directly from a report server to a printer that is attached to the Web server. Printers and print options are selected by individual report users by using a standard **Print** dialog.

Report authors who design reports specifically for print output can use page breaks, page headers and footers, expressions, and background images to create a print-based design. Examples of report design elements intended for print output might include terms and conditions that you print on the back of every report. Or, other examples include graphic and text elements that mimic letterhead.

Pagination can be implemented for different rendering formats. As such, you might not be able to achieve optimum print output results for every report in every rendering format. The following list provides examples:

- Report pages are designed to accommodate variable amounts of data. Reports that include a matrix, for example, can cause a page to grow both horizontally and vertically depending on whether a user interactively toggles rows and columns. A user who doesn't expand a matrix gets different print results than a user who does.

-  You can't combine landscape and portrait mode pages in the same report. Also, there's not a way to create a print-based layout that replaces or exists alongside the layout of a report as rendered in a browser or other application.

- Report printouts include everything that is visible on the report for most exported reports, as viewed by the user on a computer monitor. White space from the report design surface is preserved. To add or remove extra blank pages horizontally, change the report page width.

> [!NOTE]  
> HTML report printouts might contain only the content on the first page if you are using the browser's Print command. You can achieve better results if you print HTML reports using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] client printing functionality. For more information, see [Print reports from a browser with the print control (Report Builder)](../../reporting-services/report-builder/print-reports-from-a-browser-with-the-print-control-report-builder-and-ssrs.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## In this section

[Print reports from a browser with the print control (Report Builder)](../../reporting-services/report-builder/print-reports-from-a-browser-with-the-print-control-report-builder-and-ssrs.md)  
Describes how to use client-side printing to print reports from the web portal.

[Print reports from other applications (Report Builder)](../../reporting-services/report-builder/print-reports-from-other-applications-report-builder-and-ssrs.md)  
Describes how to print reports exported to another application.

[Print a report (Report Builder)](../../reporting-services/report-builder/print-a-report-report-builder-and-ssrs.md)  
Provides step-by-step instructions on how to print a report. Also provides instructions on how to control the margins on a page, and on how to specify the paper size for reports that PDF, Image, and Print hard-page break renderers render.

## Related content

- [Export reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Page headers and footers (Report Builder)](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)
- [Images (Report Builder)](../../reporting-services/report-design/images-report-builder-and-ssrs.md)
- [Pagination in Reporting Services (Report Builder)](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)
