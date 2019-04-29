---
title: "Print a Report (Reporting Services in SharePoint Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "printing reports, SharePoint Web application"
  - "printing reports"
ms.assetid: 026784f7-8cb4-4351-93ee-230b2ab0f8f5
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Print a Report (Reporting Services in SharePoint Mode)
  For a report server that runs in SharePoint mode, there are three ways to print a report from a SharePoint Web application:  
  
-   **From a SharePoint site** Choose **Print** from the **Actions** menu that appears in the report toolbar when you open the report. This provides Reporting Services print functionality, which includes a standard **Print** dialog box used to select a printer, specify pages and margins, and preview the report. This print feature is intended to be used in place of the Print command on a browser's File menu. When you print reports this way, the report is printed as it was designed, without the extra elements you see in a Web page print out.  
  
-   **From a browser** The print features of a browser work best for HTML reports that fit on a single page. Typically, pages that you print from a browser include all of the visual elements that are on a Web page, plus header and footer information that identifies the page or Web site. When you print from a browser, only the contents of the current window are printed. If the report is long, the browser prints only a portion of the report (typically just the first page).  
  
-   **From a target application** You can export a report to use the print features of a target application, such as Microsoft Office Excel or Adobe Acrobat Reader. Some application formats, such as TIFF or PDF, are ideally suited for printing multipage reports. When you export a report to a desktop application, you can use any specialized print features that the application provides. To export a report, choose **Export** from the **Actions** menu that appears in the report toolbar when you open the report.  
  
> [!NOTE]  
>  To print a report, you must have permission to view it.  
  
 For best results when printing a report from a Web page, use **Print** on the **Actions** menu. The **Print** action is tied to a client print control that is downloaded from the report server. The download occurs once, the first time you select **Print**.  
  
 Report authors can design reports specifically for print output or for a specific application format. Recognize that due to the way pagination is implemented for different application formats, you may not be able to achieve optimum print output results for every report in every export format. In contrast with reports that are designed for print output, on-screen report pages are designed to accommodate variable amounts of data. For example, reports that include a matrix can cause a page to grow both horizontally and vertically depending on how you expand the rows and columns. When printing a variable sized report, a user who does not expand a matrix will get different print results than a user who does. For most exported reports, report printouts include everything that is visible on the report, as viewed by the user on a computer monitor.  
  
### How to print reports from the Actions menu  
  
1.  Open the report.  
  
2.  On the **Actions** menu, click **Print**. If you do not see the **Actions** menu, the report toolbar has been hidden and you cannot use the features it provides. If you have an **Actions** menu but **Print** is not on it, the print functionality has been disabled on the report server and you cannot use it.  
  
3.  In the **Print** dialog box, select the printer and settings you want to use and click **OK**.  
  
     To modify the default settings, click the **Properties** button. Page size is determined by the default height and width of the report page size as defined in the report definition. The extent to which you can change page dimensions depends on the capabilities of the printer you are using.  
  
     To view the report before you print it, click the **Preview** button. This opens the first page of the report in a separate preview window. Additional pages are made available as the report is rendered on the report server. A previewed report is rendered in EMF format. You can navigate to the previous or next page until the last page is reached, and the **Next** button is disabled. To modify the print margins in the preview page, click the **Margins** button. The **Margins** dialog box is displayed. Configure the top, bottom, right, and left margins and click **OK**. The dialog box closes and the settings are stored for rendering preview and printing.  
  
## See Also  
 [Enable and Disable Client-Side Printing for Reporting Services](../report-server/enable-and-disable-client-side-printing-for-reporting-services.md)  
  
  
