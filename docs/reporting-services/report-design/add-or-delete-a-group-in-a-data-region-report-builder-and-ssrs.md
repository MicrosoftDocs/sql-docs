---
title: "Add or delete a group in a data region in a paginated report"
description: Consider adding a group to a data region in a paginated report. This addition helps organize data by a specific value or set of expressions for display and calculations in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add or delete a group in a data region in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In paginated reports, add a group to a data region when you want to organize data by a specific value or set of expressions, for display and calculations. A group has a name and an expression that identifies which data from a dataset belongs to the group. For more information about groups, see [Understand groups &#40;Report Builder&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
 In a tablix data region, select the table, matrix, or list to display the **Grouping** pane. Drag dataset fields to the **Row Group** and **Column Group** pane to create parent or child groups. Right-click an existing group to add an adjacent group. By definition, the details group is the innermost group and can only be added as a child group. Right-click an existing group and delete it. Rows and columns on which to display group values are automatically added for you. For more information, see [Tables, matrices, and lists &#40;Report Builder&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).  
  
 In a Chart data region, select the chart to display the drop-zones. Create groups by dragging dataset fields to the category and series drop zones. To add nested groups, add multiple fields to the drop-zone.  
  
 Groups aren't defined in a gauge by default. The default behavior for the gauge is to aggregate all values in the specified field into one value that is displayed on the gauge. For more information, see [Gauges &#40;Report Builder&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Add a parent or child row or column group to a tablix data region  
  
1.  Drag a field from the **Report Data** pane to the **Row Groups** pane or the **Column Groups** pane.  
  
    > [!NOTE]  
    >  If you don't see the **Grouping** pane, on the **View** tab, select **Grouping**.  
  
1.  Drop the field before or after the group hierarchy using the guide bar to place the group as a parent group or a child group to an existing group.  
  
     The group is added with a default name, group expression, and sort expression that is based on the field name.  
  
## Add an adjacent row or column group to a tablix data region  
  
1.  In the **Grouping** pane, right-click a group that is a peer to the group that you want to add. Select **Add Group**, and then choose **Adjacent Before** or **Adjacent After** to specify where to add the group. The **Tablix Group** dialog opens.  
  
1.  In **Name**, enter a name for the group.  
  
1.  In **Group expression**, enter an expression or select the expression button (**fx**) to create an expression.  
  
1.  Select **OK**.
  
     A new group is added to the **Grouping** pane and a row or column on which to display group values is added to the tablix data region on the design surface.  
  
## Add a details group to a tablix data region  
  
1.  In the **Grouping** pane, right-click a group that is the innermost child group, but not the **Details** group. Select **Add Group**, and then choose **Child Group**. The **Tablix Group** dialog opens.  
  
1.  In **Group expression**, leave the expression blank. A details group has no expression.  
  
1.  Select **Show detail data**.  
  
1.  Select **OK**.
  
     A new details group is added as a child group in the Grouping pane, and the row handle for the group you selected in step 1 displays the details group icon. For more information about handles, see [Tablix data region cells, rows, and columns &#40;Report Builder&#41;](../../reporting-services/report-design/tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md).  
  
## Edit a row or column group in a tablix data region  
  
1.  On the report design surface, choose the tablix data region to select it. The **Grouping** pane displays the row and column groups.  
  
1.  Right-click the group, and then select **Group Properties**.  
  
1.  In **Name**, enter the name of the group.  
  
1.  In **Group expressions**, enter or select an expression, or choose the Expression (**fx**) button to create a group expression.  
  
1.  Select **Add** to create more expressions. All expressions you specify are combined using a logical AND to specify data for this group.  
  
1.  (Optional) Select **Page Breaks** to set page break options.  
  
1.  (Optional) Select **Sorting** to select or type expressions that specify the sort order for values in the group.  
  
1.  (Optional) Select **Visibility** to select the visibility options for the item.  
  
1. (Optional) Select **Filters** to set filters for this group.  
  
1. (Optional) Select **Variables** to define variables scoped to this group and accessible from any child groups.  
  
1. Select **OK**.
  
## Delete a group from a tablix data region  
  
1.  In the **Grouping** pane, right-click the group, and then select **Delete Group**.  
  
1.  In the **Delete Group** dialog, select one of the following options:  
  
    -   **Delete group and related rows and columns**: Choose this option to delete the group definition and all related rows that display group data. For the details group, if the same row belongs to both detail and group data, only the detail data rows are deleted.  
  
    -   **Delete group only**: Choose this option to keep the structure of the tablix data region the same and delete only the group definition.  
  
1.  Select **OK**.
  
## Delete a details group from a tablix data region  
  
1.  In the **Grouping** pane, right-click the details group, and then select **Delete Group**.  
  
1.  In the **Delete Group** dialog, select one of the following options:  
  
    -   **Delete group and related rows and columns**: Choose this option to delete the group definition and all related rows that display group data. For the details group, if the same row belongs to both detail and group data, only the detail data rows are deleted.  
  
    -   **Delete group only**: Choose this option to keep the structure of the tablix data region the same and delete only the group definition.  
  
1.  Select **OK**.
  
     The details group is deleted.  
  
    > [!NOTE]  
    >  Verify that after you remove a details row, the expression in each cell specifies an aggregate expression where appropriate. If necessary, edit the expression to specify aggregate functions as needed.  
  
## Related content 
 [Report and group variables collections references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-report-and-group-variables-references-report-builder.md)   
 [Group expression examples &#40;Report Builder&#41;](../../reporting-services/report-design/group-expression-examples-report-builder-and-ssrs.md)   
 [Filter, group, and sort data &#40;Report Builder&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Tablix data region &#40;Report Builder&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder&#41;](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder&#41;](../../reporting-services/report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, matrices, and lists &#40;Report Builder&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
