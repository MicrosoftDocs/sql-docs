---
title: "Display Headers and Footers with a Group (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 8eb7d648-4df2-491a-96cb-99e55629d617
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Display Headers and Footers with a Group (Report Builder and SSRS)
  You can help control whether a static row, such as a group header or footer, renders with dynamic rows that are associated with a group in a tablix data region.  
  
 To repeat all the column headings or row headings on multiple pages, you can set properties for the tablix data region. For more information, see [Display Row and Column Headers on Multiple Pages &#40;Report Builder and SSRS&#41;](display-row-and-column-headers-on-multiple-pages-report-builder-and-ssrs.md).  
  
 To control the rendering behavior for dynamic rows and columns that are associated with nested groups, or for static rows and columns that are associated with labels or subtotals, you must set properties for the tablix member. A tablix member represents a static or dynamic row or column. A static member repeats once. For example, a grand total row is a static row. A dynamic member repeats once for each group instance. For example, a row that is associated with a group that has the group expression [Territory] repeats once for each unique value for territory. For more information about tablix members, see [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md).  
  
 You can select a tablix member in the Grouping pane and set the properties **KeepWithGroup**, **KeepTogether**, and **RepeatOnNewPage** in the Properties pane. Use **KeepWithGroup** to help display group headers and footers on the same page as the group. Use **KeepTogether** to help display static members with the rows or columns of a group. Use **RepeatOnNewPage** to repeat the group header or footer on every page that displays at least one complete instance of the row group member designated by the **KeepWithGroup** value. **RepeatOnNewPage** is not supported for column group members.  
  
> [!NOTE]  
>  **KeepWithGroup**, **KeepTogether**, and **RepeatOnNewPage** are group member properties that you can set by using the **Advanced Mode** of the Grouping pane. For more information, see [Grouping Pane &#40;Report Builder&#41;](grouping-pane-report-builder.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To keep a static row with a set of dynamic rows associated with a row group  
  
1.  On the design surface, click anywhere in the tablix data region to select it. The Grouping pane displays the row and column groups for the data region.  
  
2.  On the right side of the Grouping pane, click the down arrow, and then click **Advanced Mode**. The Row Groups pane displays the hierarchical static and dynamic members for the row groups hierarchy.  
  
3.  Click the static member that corresponds to the row header or footer that you want to keep with the group rows. The Properties pane displays the **Tablix Member** properties.  
  
4.  In the Properties pane, click **KeepWithGroup**, and then choose one of the following values from the drop-down list:  
  
    -   **None** Select this option to indicate no preference for keeping this member with the members of the selected row group.  
  
    -   **Before** Select this option to keep this member with the members of the previous group. You might use this for group footer rows.  
  
    -   **After** Select this option to keep this member with the members of the following group. You might use this for group header rows.  
  
5.  (Optional) Preview the report. Where possible, the report renderer keeps this member with the specified row group members.  
  
### To keep a static column with a set of dynamic columns associated with a column group  
  
1.  On the design surface, click anywhere in the tablix data region to select it. The Grouping pane displays the row and column groups for the data region.  
  
2.  On the right side of the Grouping pane, click the down arrow, and then click **Advanced Mode**. The Column Groups pane displays the hierarchical static and dynamic members for the column group hierarchy.  
  
3.  Click the static member that corresponds to the static column that you want to keep with the group columns. The Properties pane displays the **Tablix Member** properties.  
  
4.  In the Properties pane, click **KeepWithGroup**, and then choose one of the following values from the drop-down list:  
  
    -   **None** Select this option to indicate no preference for keeping this member with the members of the selected column group.  
  
    -   **Before** Select this option to keep this member with the members of the previous group. You might use this for column labels that display before the specified column group members.  
  
    -   **After** Select this option to keep this member with the members of the following group. You might use this for column totals that display after the specified column group members.  
  
5.  (Optional) Preview the report. Where possible, the report renderer keeps this member with the specified column group members.  
  
## See Also  
 [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md)   
 [Tablix Data Region Areas &#40;Report Builder and SSRS&#41;](tablix-data-region-areas-report-builder-and-ssrs.md)   
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../tablix-data-region-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
