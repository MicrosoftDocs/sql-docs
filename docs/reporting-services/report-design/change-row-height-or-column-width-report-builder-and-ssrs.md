---
title: Change row height or column width in a Report Builder paginated report
description: Learn how to set the column width or fixed row height with text box properties for rendered paginated reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/09/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to the column width or row height so that I can adjust my reports to visually fit my data.
---
# Change row height or column width in a Report Builder paginated report

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

When you set a row height, you're specifying the maximum height for the row in the rendered paginated report. However, text boxes in the row are set to grow vertically to accommodate their data at run-time by default. This setting can cause a row to expand beyond the height that you specify. To set a fixed row height, you must change the text box properties so they don't automatically expand.  
  
When you set a column width, you specify the maximum width for the column in the rendered report. Columns don't automatically adjust horizontally to accommodate text.  
  
If a cell in a row or column contains a rectangle or data region, the height and width of the contained item determines the minimum height and width of the cell. For more information, see [Rendering behaviors in a paginated report (Report Builder)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Change row height by moving row handles
  
1. In **Design** view, choose anywhere in the tablix data region to select it. Gray row handles appear on the outside border of the tablix data region.
  
1. Hover over the row handle edge that you want to expand. A double-headed arrow appears.

    :::image type="content" source="../../reporting-services/report-design/media/row-handles.png" alt-text="Screenshot of a table highlighting the double-headed arrow at the edge of a row.":::
  
1. Select the edge of the row and move it higher or lower to adjust the row height.  
  
## Change row height by setting cell properties  
  
1. In **Design** view, choose a cell in the table row.  

    :::image type="content" source="../../reporting-services/report-design/media/table-selectcell.png" alt-text="Screenshot of a table with a cell selected.":::
  
1. In the **Properties** pane go to **Position > Size**, and then modify the **Height** property.

     :::image type="content" source="../../reporting-services/report-design/media/cell-properties-pane.png" alt-text="Screenshot of the Properties Pane for the selected table cell":::

1. Select anywhere outside the **Properties** pane to update the table height.

## Prevent a row from automatically expanding vertically  
  
1. In **Design** view, choose anywhere in the tablix data region to select it. Gray row handles appear on the outside border of the tablix data region.  
  
1. Select the row handle and choose the row.  
  
1. In the **Properties** pane, set **CanGrow** to **False**.

    :::image type="content" source="../../reporting-services/report-design/media/can-grow.png" alt-text="Screenshot of the Properties Pane for the selected table cell highlighting the CanGrow property.":::
  
    > [!NOTE]  
    > If you can't see the **Properties** pane, from the **View** menu, select **Properties**.  
  
## Change column width  
  
1. In **Design** view, select anywhere in the tablix data region to select it. Gray column handles appear on the outside border of the tablix data region.  
  
1. Hover over the column handle edge that you want to expand. A double-headed arrow appears.

    :::image type="content" source="../../reporting-services/report-design/media/row-handles.png" alt-text="Screenshot of a table highlighting the double-headed arrow at the edge of a column.":::
  
1. Select the edge of the column and move it left or right to adjust the column width.  
  
## Related content  

- [Tablix data region in a paginated report (Report Builder)](tablix-data-region-report-builder-and-ssrs.md)
- [Cells, rows, & columns in a tablix in a paginated report (Report Builder)](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md)
- [Tables in paginated reports (Report Builder)](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)
  