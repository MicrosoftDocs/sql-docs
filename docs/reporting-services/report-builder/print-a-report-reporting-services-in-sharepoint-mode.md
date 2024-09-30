---
title: "Print a report (Reporting Services in SharePoint mode)"
description: If a report server runs in SharePoint mode, you can print a report from a SharePoint Web application from a SharePoint site, a browser, or a target application.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "printing reports, SharePoint Web application"
  - "printing reports"
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Print a report (Reporting Services in SharePoint mode)

  For a report server that runs in SharePoint mode, there are three ways to print a report from a SharePoint Web application:

- **From a SharePoint site**: Choose **Print** from the **Actions** menu that appears in the report toolbar when you open the report. This menu provides Reporting Services print functionality, which includes a standard **Print** dialog used to select a printer, specify pages and margins, and preview the report. This print feature is intended to be used in place of the Print command on a browser's **File** menu. When you print reports this way, the report is printed as it was designed, without the extra elements you see in a Web page printout.

- **From a browser**: The print features of a browser work best for HTML reports that fit on a single page. Typically, pages that you print from a browser include all of the visual elements that are on a web page, plus header and footer information that identifies the page or web site. When you print from a browser, only the contents of the current window are printed. If the report is long, the browser prints only a portion of the report. This portion is typically just the first page.

- **From a target application**: You can export a report to use the print features of a target application, such as Microsoft Office Excel or Adobe Acrobat Reader. Some application formats, such as TIFF or PDF, are ideally suited for printing multipage reports. When you export a report to a desktop application, you can use any specialized print features that the application provides. To export a report, choose **Export** from the **Actions** menu that appears in the report toolbar when you open the report.

> [!NOTE]  
> To print a report, you must have permission to view it.

For best results when you're printing a report from a Web page, use **Print** on the **Actions** menu. The **Print** action is tied to a client print control that is downloaded from the report server. The download occurs once, the first time you select **Print**.

Report authors can design reports specifically for print output or for a specific application format. 
Pagination is implemented differently for different application formats. So, you might not be able to achieve optimum print output results for every report in every export format. In contrast with reports that are designed for print output, on-screen report pages are designed to accommodate variable amounts of data. For example, reports that include a matrix can cause a page to grow both horizontally and vertically depending on how you expand the rows and columns. When you're printing a variable sized report, a user who doesn't expand a matrix gets different print results than a user who does. For most exported reports, report printouts include everything that is visible on the report, as viewed by the user on a computer monitor.

### How to print reports from the Actions menu

1. Open the report.

1. On the **Actions** menu, select **Print**. If you don't see the **Actions** menu, the report toolbar is hidden and you can't use the features it provides. If you have an **Actions** menu but **Print** isn't on it, the print functionality is disabled on the report server, and you can't use it.

1. In the **Print** dialog, select the printer and settings you want to use and select **OK**.

     To modify the default settings, select the **Properties** button. The default height and width of the report page size as defined in the report definition determines the page size. The extent to which you can change page dimensions depends on the capabilities of the printer you're using.

     To view the report before you print it, select the **Preview** button. This option opens the first page of the report in a separate preview window. More pages are made available as the report is rendered on the report server. A previewed report is rendered in EMF format. You can navigate to the previous or next page until the last page is reached, and the **Next** button is disabled. To modify the print margins in the preview page, select the **Margins** button. The **Margins** dialog is displayed. Configure the top, bottom, right, and left margins and select **OK**. The dialog closes and the settings are stored for rendering preview and printing.

## Related content

- [Enable and disable client-side printing for Reporting Services](../../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md)
