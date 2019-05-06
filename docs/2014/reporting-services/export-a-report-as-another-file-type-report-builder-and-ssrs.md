---
title: "Export a Report as Another File Type (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: b577568b-ecbd-44c3-be88-31dab6fc38a2
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Export a Report as Another File Type (Report Builder and SSRS)
  You can render a report to another file format, such as CSV, Image, PDF, [!INCLUDE[ofprword](../includes/ofprword-md.md)], or [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)], while previewing your report in Report Builder or Report Designer, or you can render the report while viewing the report on the report server. Rendering the report in a specific format in is helpful when you want to immediately save the report as another file type without publishing the report to the report server or when you want to see how your report design will appear when delivered to your report readers in a specific format. Rendering the report on the report server is useful when you set up subscriptions or deliver your reports via e-mail, or if you want to save a report that is available on the report server. For more information, see [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  
### To export a report as another file type in Report Builder  
  
1.  Preview the report.  
  
2.  On the ribbon, click **Export**.  
  
3.  Select the format that you want to use.  
  
     The **Save As** dialog box opens. By default, the file name is that of the report that you exported. Optionally, you can change the file name.  
  
4.  Navigate to the location where you saved the exported report and open it.  
  
> [!NOTE]  
>  If the program cannot open the report in the format that you chose because you do not have a program associated with this file type, you will be prompted to save the exported report or to find a program online to open the report.  
  
### To export a report as another file type in Report Manager  
  
1.  From the Report Manager **Home** page, navigate to the report that you want to export.  
  
2.  Click the report.  
  
     The report is generated.  
  
3.  On the Report Viewer toolbar, click the **Select a format** drop-down arrow.  
  
4.  Select the format that you want to use.  
  
5.  Click **Export**.  
  
     A message appears asking you if you want to open or save the file.  
  
6.  To view the report in the selected export format, click **Open**.  
  
     \- or -  
  
     To immediately save the report in the selected export format, click **Save**.  
  
     Using the application that is associated with the format that you chose, the report is either displayed or saved. If you click **Save**, you will be prompted for a location where you can save your report.  
  
     **Note** If the program cannot open the report in the format that you chose because you do not have a program associated with this file type, you will be prompted to save the exported report or to find a program online to open the report.  
  
### To export a report as another file type in a SharePoint library  
  
1.  Preview the report.  
  
2.  On the toolbar, click **Actions**, point to **Export**, and then select the format that you want to use.  
  
     The **File Download** dialog box opens.  
  
3.  To view the report in the selected export format, click **Open**.  
  
     \- or -  
  
     To immediately save the report in the selected export format, click **Save**.  
  
     Using the application that is associated with the format that you chose, the report is either displayed or saved. If you click **Save**, you will be prompted for a location where you can save your report.  
  
     Optionally, change the file name of the exported report.  
  
     **Note** If the program cannot open the report in the format that you chose because you do not have a program associated with this file type, you will be prompted to save the exported report or to find a program online to open the report.  
  
## See Also  
 [Exporting Reports &#40;Report Builder and SSRS&#41;](report-builder/export-reports-report-builder-and-ssrs.md)   
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](report-builder/interactive-functionality-different-report-rendering-extensions.md)  
  
  
