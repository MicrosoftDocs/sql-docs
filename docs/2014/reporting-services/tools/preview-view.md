---
title: "Preview View | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.previewview.f1"
helpviewer_keywords: 
  - "Preview view [Reporting Services]"
ms.assetid: 108255d1-5be8-47c1-80f3-1f2a055e4d02
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Preview View
  Use **Preview** view to display the rendered report. When a report is previewed, Report Designer runs the report locally and displays it in the Preview view. In preview mode, the report is processed in full. If the report has a complex query or has a large amount of data, preview might take several minutes to complete the first time you view it. For subsequent changes that affect only the format of the report, preview uses cached data.  
  
> [!IMPORTANT]  
>  When [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] is run as a RemoteApp, reports cannot be displayed in **Preview** view in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. RemoteApp programs are programs that are accessed remotely through Remote Desktop Services. For more information, see [TS RemoteApp Step-by-Step Guide](https://technet.microsoft.com/library/cc730673\(WS.10\).aspx).  
  
## Options  
 Use the toolbar to manage preview functions.  
  
 **Show or Hide Document Map**  
 Choose this option to show or hide the document map if one exists.  
  
 **Show or Hide Parameter Area**  
 Choose this option to show or hide the parameters boxes for reports with parameters.  
  
 **First Page**  
 Choose this option to go to the first page of the report.  
  
 **Previous Page**  
 Choose this option to go to the previous page of the report.  
  
 **Current Page**  
 Displays the current page of the report.  
  
 **Total Pages**  
 Displays the total number of pages in the report.  
  
 **Next Page**  
 Choose this option to go to the next page of the report.  
  
 **Last Page**  
 Choose this option to go to the last page of the report.  
  
 **Back to Parent Report**  
 Choose this option to go to the parent report. This option is used to navigate drillthrough reports.  
  
 **Stop Rendering**  
 Choose this option to stop the rendering process.  
  
 **Refresh**  
 Choose this option to refresh the data cache and run the report again.  
  
 **Print**  
 Choose this option to print the report.  
  
 **Print Layout**  
 Choose this option to toggle between the preview report and the view of the report as it will appear on the printed page.  
  
 **Page Setup**  
 Choose this option to conveniently change page and print properties. Use Print Layout to view changes before printing.  
  
 **Export**  
 Choose this option to export the rendered report in a specific format.  
  
 **Zoom**  
 Select a zoom factor to zoom in or out on the report.  
  
 **Search Text**  
 Type text to search for a match within the report. You cannot use search operators. Click **Find** to search for the first instance.  
  
 **Find**  
 Choose this option to begin searching the report for the search text.  
  
 **Find Next**  
 Choose this option to search for the next instance of the search text.  
  
## See Also  
 [Previewing Reports](../reports/previewing-reports.md)   
 [Report Designer F1 Help](report-designer-f1-help.md)  
  
  
