---
title: "Tablix Data Region (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 99f83b32-4b86-4d40-973c-9a328d23ac8b
author: markingmyname
ms.author: maghan
---
# Tablix Data Region (Report Builder and SSRS)
  In [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)], the tablix data region is a generalized layout report item that displays paginated report data in cells that are organized into rows and columns. Report data can be detail data as it is retrieved from the data source, or aggregated detail data organized into groups that you specify. Each tablix cell can contain any report item, such as a text box or an image, or another data region, such as a tablix region, chart, or gauge. To add multiple report items to a cell, first add a rectangle to act as a container. Then, add the report items to the rectangle.  
  
 The table, matrix, and list data regions are represented on the ribbon by templates for the underlying tablix data region. When you add one of these templates to a report, you are actually adding a tablix data region that is optimized for a specific data layout. By default, a table template displays detail data in a grid layout, a matrix displays group data in a grid layout, and a list displays detail data in a free-form layout.  
  
 By default, each tablix cell in a table or matrix contains a text box. The cell in a list contains a rectangle. You can replace a default report item with a different report item, such as an image.  
  
 When you define groups for a table, matrix, or list, Report Builder and Report Designer add rows and columns to the tablix data region on which to display grouped data.  
  
 To understand the tablix data region, it helps to understand the following:  
  
*   The difference between detail data and grouped data.  
  
*   Groups, which are organized as members of group hierarchies on the horizontal axis as row groups and on the vertical axis as column groups.  
  
*  The purpose of tablix cells in the four areas of a tablix data region: the body, the row group headers, the column group headers, and the corner.  
  
*  Static and dynamic rows and columns, and how they relate to groups.  
  
 This article spells out these concepts to explain the structure that Report Builder and Report Designer add for you when you add templates and create groups, so you can modify the structure to suit your own needs. Report Builder and Report Designer provide multiple visual indicators to help you recognize tablix data region structure. For more information, see [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](../../reporting-services/report-design/tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Understanding Detail and Grouped Data  
 Detail data is all the data from a report dataset as it comes back from the data source. Detail data is essentially what you see in the query designer results pane when you run a dataset query. The actual detail data includes calculated fields that you create, and is restricted by filters set on the dataset, data region, and details group. You display detail data on a detail row by using a simple expression such as [Quantity]. When the report runs, the detail row repeats once for each row in the query results at run time.  
  
 Grouped data is detail data that is organized by a value that you specify in the group definition, such as [SalesOrder]. You display grouped data on group rows and columns by using simple expressions that aggregate the grouped data, such as [Sum(Quantity)]. For more information, see [Understanding Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
## Understanding Group Hierarchies  
 Groups are organized as members of group hierarchies. Row group and column group hierarchies are identical structures on different axes. Think of row groups as expanding down the page and column groups as expanding across the page.  
  
 A tree structure represents nested row and column groups that have a parent/child relationship, such as a category with subcategories. The parent group is the root of the tree and child groups are its branches. Groups can also have an independent, adjacent relationship, such as sales by territory and sales by year. Multiple unrelated tree hierarchies are called a forest. In a tablix data region, row groups and columns groups are each represented as an independent forest. For more information, see [Understanding Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
## Understanding Tablix Data Region Areas  
 A tablix data region has four possible areas for cells: the tablix corner, the tablix row group hierarchy, the tablix column group hierarchy, or the tablix body. The tablix body always exists. The other areas are optional.  
  
 Cells in the tablix body area display detail and group data.  
  
 Cells in the Row Groups area are created automatically when you create a row group. These are row group header cells and display row group instance values by default. For example, when you group by [SalesOrder], group instance values are the individual sales orders that you are grouping by.  
  
 Cells in the Column Groups area are created automatically when you create a column group. These are column group header cells, and they display column group instance values by default. For example, when you group by [Year], group instance values are the individual years that you are grouping by.  
  
 Cells in the tablix corner area are created automatically when you have both row groups and column groups defined. Cells in this area can display labels, or you can merge the cells and create a title.  
  
 For more information, see [Tablix Data Region Areas &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-areas-report-builder-and-ssrs.md).  
  
## Understanding Static and Dynamic Rows and Columns  
 A tablix data region organizes cells in rows and columns that are associated with groups. Group structures for row groups and columns are identical. This example uses row groups, but you can apply the same concepts to column groups.  
  
 A row is either a static or dynamic. A static row is not associated with a group. When the report runs, a static row renders once. Table headers and footers are static rows. Static rows display labels and totals. Cells in a static row are scoped to the data region.  
  
 A dynamic row is associated with one or more groups. A dynamic row renders once for every unique group value for the innermost group. Cells in a dynamic row are scoped to the innermost row group and column group to which the cell belongs.  
  
 Dynamic detail rows are associated with the Details group that is automatically created when you add a table or list to the design surface. By definition, the Details group is the innermost group for a tablix data region. Cells in detail rows display detail data.  
  
 Dynamic group rows are created when you add a row group or column group to an existing tablix data region. Cells in dynamic group rows display aggregated values for the default scope.  
  
 The Add Total feature automatically creates a row outside the current group on which to display values that are scoped to the group. You can also add static and dynamic rows manually. Visual indicators help you understand which rows are static and which rows are dynamic. For more information, see [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](../../reporting-services/report-design/tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md).  
  
## See Also  
 [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md)   
 [Controlling the Tablix Data Region Display on a Report Page &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/controlling-the-tablix-data-region-display-on-a-report-page.md)   
 [Exploring the Flexibility of a Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/exploring-the-flexibility-of-a-tablix-data-region-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
