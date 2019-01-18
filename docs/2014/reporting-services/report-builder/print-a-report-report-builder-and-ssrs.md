---
title: "Print a Report (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: b96936c9-c387-41a9-8c19-6eb325769ffd
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Print a Report (Report Builder and SSRS)
  After you save a report to a report server, you can view and print the report from a browser, Report Manager, or any application that you use to view an exported report. Before saving a report, you can print it when you preview it.  
  
 When you print a report, you can specify the size of the paper to use. The size of the paper determines the number of pages in a report and which report data fits on each page. Paper size affects only reports that are rendered with hard page-break renders: PDF, Image, and Print. Setting the paper size has no effect on other renderers. For more information, see [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
 From the report viewer toolbar in Report Manager or in preview in Report Builder, you can export a report to a hard page-break renderer or click the Print button to print a copy of the report. You might need to set the paper size or other page setup properties. Use the **Report Properties** dialog box to change page setup properties, including paper size.  
  
 You can specify print page margins in two different locations: in design mode and in run mode.  
  
-   **Design mode.** When you set page margins in design mode, these settings are saved in the report definition when you save the report.  
  
-   **Run mode.** When you set page margins in run mode, this information is not saved in the report definition. The next time you print the report, you will get the settings from the report definition, unless you indicate your print margins again.  
  
    > [!NOTE]  
    >  Print margins are not displayed in design or run modes. There is no relationship between the design surface area and the print area of your report. To see print margins, in run mode, click Print Layout on the **Run** tab on the Ribbon.  
  
 For more information about report paging, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../report-design/pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To print a report in Report Builder  
  
1.  Open a report.  
  
2.  On the Home tab, click **Run**.  
  
3.  (optional) Click **Print Layout** to see how the report will look when it is printed.  
  
4.  (optional) Click **Page Setup** to set paper, orientation, and margins.  
  
    > [!NOTE]  
    >  The default values for these come from the report properties, which are set in Design view. The values you set here in the **Page Setup** dialog box are for this session only. When you close this report and reopen it, it will have the default values again.  
  
5.  Click **Print**.  
  
6.  In the **Print** dialog box, select a printer and specify other printing options.  
  
### To print a report from a Web browser application  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the report that you want to print. Open the report.  
  
3.  On the toolbar at the top of the report, click **Print**.  
  
    > [!NOTE]  
    >  The first time you print an HTML report, the report server prompts you to install an ActiveX control used for printing. You must install and configure the control to print.  
  
4.  In the **Print** dialog box, select a printer, and then click **Prin**t.  
  
### To print a report from other applications  
  
1.  In Report Manager, navigate to the report that you want to print. Open the report.  
  
2.  On the toolbar at the top of the report, select a rendering format, and then click **Export**. The report opens in a viewer application that corresponds to the rendering format.  
  
     For example, if you select PDF, the report opens in Adobe Acrobat Reader.  
  
3.  On the **File** menu in that program, click **Print**.  
  
### To change paper size  
  
1.  Right-click outside of the report body and click **Report Properties**.  
  
2.  In **Page Setup**, select a value from the **Paper Size** list. Each option populates the **Width** and **Height** properties. You can also specify a custom size by typing numeric values in the **Width** and **Height** boxes. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  Size values have a default unit based on the user's locale settings. To designate a different unit, type a physical unit designator such as cm, mm, pt, or pc after the numeric value.  
  
### To set page margins in design mode  
  
-   Right-click the blue area around the design surface, click **Report Properties**, and then click the **Page Setup** page.  
  
### To set page margins in run mode  
  
-   Click **Page Setup** on the **Run** tab.  
  
## See Also  
 [Print Reports &#40;Report Builder and SSRS&#41;](print-reports-report-builder-and-ssrs.md)   
 [Exporting Reports &#40;Report Builder and SSRS&#41;](export-reports-report-builder-and-ssrs.md)   
 [Report Properties Dialog Box, Page Setup &#40;Report Builder&#41;](../report-properties-dialog-box-page-setup-report-builder.md)   
 [Report Design View &#40;Report Builder&#41;](report-design-view-report-builder.md)  
  
  
