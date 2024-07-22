---
title: "Change row height or column width in a paginated report"
description: Learn about setting a column width or a fixed row height with text box properties for rendered paginated reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Change row height or column width in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  When you set a row height, you're specifying the maximum height for the row in the rendered paginated report. However, text boxes in the row are set to grow vertically to accommodate their data at run-time by default. This setting can cause a row to expand beyond the height that you specify. To set a fixed row height, you must change the text box properties so they don't automatically expand.  
  
 When you set a column width, you're specifying the maximum width for the column in the rendered report. Columns don't automatically adjust horizontally to accommodate text.  
  
 If a cell in a row or column contains a rectangle or data region, the height and width of the contained item determines the minimum height and width of the cell. For more information, see [Renderer behaviors &#40;Report Builder&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Change row height by moving row handles  
  
1.  In Design view, choose anywhere in the tablix data region to select it. Gray row handles appear on the outside border of the tablix data region.  
  
1.  Hover over the row handle edge that you want to expand. A double-headed arrow appears.  
  
1.  Select the edge of the row and move it higher or lower to adjust the row height.  
  
### Change row height by setting cell properties  
  
1.  In Design view, choose a cell in the table row.  
  
     ![Selected Cell in a Table](../../reporting-services/report-design/media/table-selectcell.png "Selected Cell in a Table")  
  
1.  In the **Properties** pane that displays, modify the **Height** property, and then select anywhere outside the **Properties** pane.  
  
     :::image type="content" source="../../reporting-services/report-design/media/cell-propertiespane.png" alt-text="Screenshot of the Properties Pane for the selected table cell":::
 
### Prevent a row from automatically expanding vertically  
  
1.  In Design view, choose anywhere in the tablix data region to select it. Gray row handles appear on the outside border of the tablix data region.  
  
1.  Select the row handle and choose the row.  
  
1.  In the **Properties** pane, set `CanGrow` to **False**.  
  
    > [!NOTE]  
    >  If you can't see the **Properties** pane, from the **View** menu, select **Properties**.  
  
### Change column width  
  
1.  In Design view, select anywhere in the tablix data region to select it. Gray column handles appear on the outside border of the tablix data region.  
  
1.  Hover over the column handle edge that you want to expand. A double-headed arrow appears.  
  
1.  Select the edge of the column and move it left or right to adjust the column width.  
  
## Related content  
 [Tablix data region (Report Builder)](tablix-data-region-report-builder-and-ssrs.md)   
 [Tablix data region cells, rows, and columns (Report Builder)](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md)   
 [Tables (Report Builder)](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)   
 [Matrices (Report Builder)](create-a-matrix-report-builder-and-ssrs.md)   
 [Lists (Report Builder)](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, matrices, and lists (Report Builder)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
