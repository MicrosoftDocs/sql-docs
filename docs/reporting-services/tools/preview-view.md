---
title: "Preview view"
description: Learn about the Preview view of the Reporting Services Report Designer where you view a display of your rendered report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.previewview.f1"
helpviewer_keywords:
  - "Preview view [Reporting Services]"
---
# Preview view
In [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Report Designer, use **Preview** view to display the rendered report. When a report is previewed, Report Designer runs the report locally and displays it in the Preview view. In preview mode, the report is processed in full. A report might have a complex query or a large amount of data. If that happens, preview might take several minutes to complete the first time you view it. For subsequent changes that affect only the format of the report, preview uses cached data.

:::image type="content" source="../../reporting-services/media/ssrs-ssdt-preview.png" alt-text="Screenshot that shows the Preview view.":::
  
## Options  
 Use the toolbar to manage preview functions.  

:::image type="content" source="../../reporting-services/tools/media/ssrs-ssdt-viewer-toolbar.png" alt-text="Screenshot that shows the Preview viewer toolbar.":::


 **(1) First Page**  
 Choose this option to go to the first page of the report.  
  
 **(2) Previous Page**  
 Choose this option to go to the previous page of the report.  
  
 **(3) Current Page**  
 Displays the current page of the report.  
  
 **(4) Total Pages**  
 Displays the total number of pages in the report.  
  
 **(5) Next Page**  
 Choose this option to go to the next page of the report.  
  
 **(6) Last Page**  
 Choose this option to go to the last page of the report.  
  
 **(7) Back to Parent Report**  
 Choose this option to go to the parent report. This option is used to navigate drillthrough reports.  
  
 **(8) Stop Rendering**  
 Choose this option to stop the rendering process.  
  
 **(9) Refresh**  
 Choose this option to refresh the data cache and run the report again.  
  
 **(10) Print**  
 Choose this option to print the report.  
  
 **(11) Print Layout**  
 Choose this option to toggle between the preview report and the view of the report as it appears on the printed page.  
  
 **(12) Page Setup**  
 Choose this option to conveniently change page and print properties. Use Print Layout to view changes before printing.  
  
 **(13) Export**  
 Choose this option to export the rendered report in a specific format.  
  
 **(14) Zoom**  
 Select a zoom factor to zoom in or out on the report.  
  
 **(15) Find Text in report**  
 Enter text to search for a match within the report. You can't use search operators. Select **Find** to search for the first instance.  

 **(16) Show or Hide Parameter Area**  
 Choose this option to show or hide the parameters boxes for reports with parameters.
 
 **(17) Find**  
 Choose this option to begin searching the report for the search text.  
  
 **(18) Find Next**  
 Choose this option to search for the next instance of the search text.  
  
## Related content

- [Preview reports](../../reporting-services/reports/previewing-reports.md)
- [Report designer F1 help](../../reporting-services/tools/report-designer-f1-help.md)
