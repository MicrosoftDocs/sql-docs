---
title: "Add, Move, or Delete a Table, Matrix, or List (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 4b97c470-cde0-4bb1-a46e-5f5f5553feaa
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add, Move, or Delete a Table, Matrix, or List (Report Builder and SSRS)
  A data region displays data from a report dataset. Data regions include table, matrix, list, chart, and gauge. To nest one data region inside another, add each data region separately, and then drag the child data region onto the parent data region.  
  
 The simplest way to add a table or matrix data region to your report is to run the New Table or New Matrix Wizard. These wizards will guide you through the process of choosing a connection to a data source, arranging fields, and choosing the layout and style.  
  
> [!NOTE]  
>  The wizard is available only in Report Builder.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a table or matrix to a report by using the New Table or New Matrix Wizard  
  
1.  On the **Insert** tab, click **Table** or **Matrix**, and then click **Table Wizard** or **Matrix Wizard**.  
  
2.  Follow the steps in the **NewTable** or **New Matrix** wizard.  
  
3.  On the **Home** tab, click **Run** to see the rendered report.  
  
4.  On the **Run** tab, click **Design** to continue working on the report.  
  
### To add a data region  
  
1.  On the **Ribbon**, in the **Data Regions** group, click the data region to add.  
  
2.  Click the design surface, and then drag to create a box that is the desired size of the data region.  
  
3.  Drag a report dataset field from the Report Data pane onto a data region cell. The data region is now bound to data from the report dataset.  
  
### To select a data region  
  
-   For a tablix data region, right-click the corner handle. For a chart or gauge data region, click in the data region.  
  
     A selection handle and eight resizing handles appear.  
  
     For nested data regions, right-click in the nested data region, click **Select**, and then select the report item you want. To verify which report item is selected, use the Properties pane. The name of the selected item on the design surface appears in the toolbar of the Properties pane.  
  
### To move a data region  
  
-   To move a data region, click the selection handle of the data region and drag it. Use snaplines to align it to existing report items.  
  
     If the ruler is not visible, click the View tab and select the **Ruler** option.  
  
     Alternatively, use the arrow keys to move the selected data region on the design surface.  
  
### To delete a data region  
  
-   Select the data region, right-click in the data region, and then click **Delete**.  
  
## See Also  
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../tablix-data-region-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
