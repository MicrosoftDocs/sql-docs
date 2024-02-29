---
title: "Add, move, or delete a table, matrix, or list in a paginated report"
description: Arrange your table or matrix data regions in a paginated report by using the New Table Wizard or New Matrix Wizard in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add, move, or delete a table, matrix, or list in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, a data region displays data from a report dataset. Data regions include table, matrix, list, chart, and gauge. To nest one data region inside another, add each data region separately, and then drag the child data region onto the parent data region.  
  
 The simplest way to add a table or matrix data region to your report is to run the New Table or New Matrix Wizard. These wizards guide you through the process of choosing a connection to a data source, arranging fields, and choosing the layout and style. The wizards are only available in Report Builder.  
  
## Add a table or matrix to a report by using the New Table or New Matrix Wizard  
  
1.  On the **Insert** tab, select **Table** or **Matrix**, and then choose **Table Wizard** or **Matrix Wizard**.  
  
1.  Follow the steps in the **New Table** or **New Matrix** wizard.  
  
1.  On the **Home** tab, select **Run** to see the rendered report.  
  
1.  On the **Run** tab, select **Design** to continue working on the report.  
  
## Add a data region  
  
1.  On the **Ribbon**, in the **Data Regions** group, select the data region to add.  
  
1.  Select the design surface, and then drag to create a box that is the desired size of the data region.  
  
1.  Drag a report dataset field from the **Report Data** pane onto a data region cell. The data region is now bound to data from the report dataset.  
  
## Select a data region  
  
-   For a tablix data region, right-click the corner handle. For a chart or gauge data region, select the data region.  
  
     A selection handle and eight resizing handles appear.  
  
     For nested data regions, right-click in the nested data region, select **Select**, and then choose the report item you want. To verify which report item is selected, use the Properties pane. The name of the selected item on the design surface appears in the toolbar of the **Properties** pane.  
  
## Move a data region  
  
-   To move a data region, select the selection handle of the data region and drag it. Use snaplines to align it to existing report items.  
  
     If the ruler isn't visible, choose the **View** tab and select the **Ruler** option.  
  
     Alternatively, use the arrow keys to move the selected data region on the design surface.  
  
## Delete a data region  
  
-   Select the data region, right-click in the data region, and then select **Delete**.  
  
## Related content 
 [Tablix data region &#40;Report Builder&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Tables, matrices, and lists &#40;Report Builder&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
