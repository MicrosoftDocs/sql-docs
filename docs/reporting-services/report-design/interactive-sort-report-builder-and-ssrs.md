---
title: "Interactive sort in a paginated report | Microsoft Docs"
description: Add interactive sort buttons to enable a user to toggle between ascending and descending order for rows in a table in a paginated report.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 00cafed5-1a3c-4ce0-a1fb-ff1e2613f495
author: maggiesMSFT
ms.author: maggies
---
# Interactive sort in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add interactive sort buttons in a paginated report to enable a user to toggle between ascending and descending order for rows in a table or for rows and columns in a matrix. The most common use of interactive sort is to add a sort button to every column header. The user can then choose which column to sort by.  
  
 However, you can add an interactive sort button to any text box, not just column headers. For example, for a text box in a row outside a row group, you can specify a sort for the parent group rows or columns, for child group rows or columns, or for the detail rows or columns. You can also combine fields into a single group expression, and then sort by multiple fields.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 When you add an interactive sort, you must specify the following items:  
  
-   **What to sort:** Rows or columns?  
  
-   **What to sort by:** A field that is displayed in a table column? A field that is not displayed?  
  
-   **What context to sort in:** For example, you can sort on rows associated with row groups; columns associated with column groups; detail rows; child groups within a parent group; or parent and child group together.  
  
-   **Which text box to add the sort button to:** In the column header or in the group row header?  
  
-   **Whether to synchronize the sort for multiple data regions:** You can design a report so that when the user toggles the sort order, other data regions with the same ancestor also sort.  
  
 For step-by-step instructions, see [Add Interactive Sort to a Table or Matrix &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-interactive-sort-to-a-table-or-matrix-report-builder-and-ssrs.md).  
  
 The following table summarizes the effects you can achieve by using interactive sort buttons.  
  
|Action|What to sort|Where to add the sort button|What to sort on|Sort scope|  
|------------|------------------|----------------------------------|---------------------|----------------|  
|Sort detail rows for a table with no groups|Details|Column header|Dataset field bound to this column|Data region|  
|Sort top-level group instances for a matrix|Groups|Column header|Group expression for parent group|Data region|  
|Sort detail rows for a child group in a table|Details|Child group header row|Dataset field to sort by|Child group|  
|Sort rows for multiple row groups and detail rows in a table|Groups, but you must redefine the group expression|Column header|Aggregate of dataset field to sort by|Data region|  
|Synchronize the sort order for multiple data regions|Groups|Typically, column header|Group expression|Dataset|  
  
 The report processor applies interactive sort after all data region and group sort expressions are applied. For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md).  
  
## Adding Interactive Sort for Multiple Groups  
 In a table with nested row groups each based on a single dataset field, you can add an interactive sort button that sorts parent group values, child group values, or detail rows. However, you might want to provide the user with the ability to sort the table by both the parent and child group values without having to click multiple times.  
  
 To do this, you must redesign the table to group on an expression that combines multiple fields. For example, for a dataset with inventory counts, if the original table grouped by size and then by color, you can specify a single group with a group expression that is a combination of size and color. For more information, see [Add Interactive Sort to a Table or Matrix &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-interactive-sort-to-a-table-or-matrix-report-builder-and-ssrs.md).  
  
## See Also  
 [Sort Data in a Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/sort-data-in-a-data-region-report-builder-and-ssrs.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Add Interactive Sort to a Table or Matrix &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-interactive-sort-to-a-table-or-matrix-report-builder-and-ssrs.md)  
  
  
