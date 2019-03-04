---
title: "Display Row and Column Headers on Multiple Pages (Report Builder and SSRS) | Microsoft Docs"
author: markingmyname
ms.author: maghan
manager: kfile
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design
ms.topic: conceptual
ms.date: 03/01/2017
---

# Display Row and Column Headers on Multiple Pages (Report Builder and SSRS)

  You can control whether to repeat row and column headers on every page of a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report for a tablix data region (a table, matrix, or list) that spans multiple pages.
  
 How you control the rows and columns depends on whether the tablix data region has group headers. When you click in a tablix data region that has group headers, a dotted line shows the tablix areas, as shown in the following figure:  
  
 ![Tablix data region areas](../../reporting-services/report-design/media/rs-tablixareas.gif "Tablix data region areas")  
  
 Row and column group headers are created automatically when you add groups by using the New Table or Matrix wizard or the New Chart wizard, by adding fields to the Grouping pane, or by using context menus. If the tablix data region has only a tablix body area and no group headers, the rows and columns are tablix members.  
  
 For static members, you can display the top adjacent rows or the side adjacent columns on multiple pages.  
  
## To display row headers on multiple pages  
  
1. Right-click the row, column, or corner handle of a tablix data region, and then click **Tablix Properties**.  
  
2. In **Row Headers**, select **Repeat header rows on each page**.  
  
3. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## To display column headers on multiple pages  
  
1. Right-click the row, column, or corner handle of a tablix data region, and then click **Tablix Properties**.  
  
2. In **Column Headers**, select **Repeat header columns on each page**.  
  
3. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## To display a static row or column on multiple pages  
  
1. On the design surface, click the row or column handle of the tablix data region to select it. The Grouping pane displays the row and column groups.  
  
2. On the right side of the Grouping pane, click the down arrow, and then click **Advanced Mode**. The Row Groups pane displays the hierarchical static and dynamic members for the row groups hierarchy and the Column groups pane shows a similar display for the column groups hierarchy.  
  
3. Click the static member that corresponds to the static member (row or column) that you want to remain visible while scrolling. The Properties pane displays the **Tablix Member** properties.  
  
     If you don't see the Properties pane, click the **View** tab at the top of the Report Builder window and then click **Properties**.  
  
4. In the Properties pane, set **RepeatOnNewPage** to True.  
  
5. Set **KeepWithGroup** to After.  
  
6. Repeat this for as many adjacent members as you want to repeat.  
  
7. Preview the report.  
  
 As you view each page of the report that the tablix data region spans, the static tablix members repeat on each page.  
  
## See Also  
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [Export Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)   
 [Controlling Page Breaks, Headings, Columns, and Rows &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/controlling-page-breaks-headings-columns-and-rows-report-builder-and-ssrs.md)   
 [Display Headers and Footers with a Group &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/display-headers-and-footers-with-a-group-report-builder-and-ssrs.md)   
 [Keep Headers Visible When Scrolling Through a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/keep-headers-visible-when-scrolling-through-a-report-report-builder-and-ssrs.md)  
  
  
