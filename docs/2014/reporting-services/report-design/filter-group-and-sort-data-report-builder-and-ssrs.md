---
title: "Filter, Group, and Sort Data (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.categorygroupproperties.sorting.f1"
  - "10403"
  - "sql12.rtp.rptdesigner.seriesgroupproperties.sorting.f1"
  - "10402"
  - "sql12.rtp.rptdesigner.seriesgroupproperties.general.f1"
  - "10410"
  - "sql12.rtp.rptdesigner.categorygroupproperties.general.f1"
  - "10412"
ms.assetid: 4dda2a7f-3f31-47e9-a88b-28d770ebd65e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Filter, Group, and Sort Data (Report Builder and SSRS)
  In a report, expressions are used to help control, organize, and sort report data. By default, as you create datasets and design the report layout, properties of report items are set automatically to expressions based on the dataset fields, parameters, and other items that appear in the Report Data pane. You can also add an interactive sort button to a table or matrix cell to enable a user to interactively change the row sort order for groups or rows within groups.  
  
-   **Filter expressions** A filter expression tests data for inclusion or exclusion based on a comparison that you specify. Filters are applied to data in a report after the data is retrieved from a data connection. You can add any combination of filters to the following items:  a shared dataset definition on the report server; a shared dataset instance or embedded dataset in a report; a data region such as a table or a chart; or a data region group, such as a row group in a table or a category group in a chart.  
  
-   **Group expressions** A group expression organizes data based on a dataset field or other value. Group expressions are created automatically as you build the report layout. The report processor evaluates group expressions after filters are applied to the data, and as report data and data regions are combined. You can customize a group expression after it is created.  
  
-   **Sort expressions** A sort expression controls the order in which data appears in a data region. Sort expressions are created automatically as you build the report layout. By default, a sort expression for a group is set to the same value as the group expression. You can customize a sort expression after it is created.  
  
-   **Interactive sort** To enable a user to sort or reverse the sort order of a column, you can add an interactive sort button to a column header or group header cell in a table or matrix.  
  
 To help your users customize filter, group, or sort expressions, you can change an expression to add a reference to a report parameter. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](report-parameters-report-builder-and-report-designer.md).  
  
 For more information and examples, see the following topics:  
  
-   [Group Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)  
  
-   [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md)  
  
-   [Tutorials &#40;Report Builder&#41;](../report-builder-tutorials.md)  
  
-   [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)  
  
-   [Report Samples (Report Builder and SSRS)](https://go.microsoft.com/fwlink/?LinkId=198283)  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Filtering"></a> Filtering Data in the Report  
 Filters are parts of a report that help control report data after it is retrieved from the data connection. Use filters when you cannot change a dataset query to filter data before it is retrieved from an external data source.  
  
 When it is possible, build dataset queries that return only the data that you need to display in the report. When you reduce the amount of the data that must be retrieved and processed, you are helping to improve report performance. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
 After the data is retrieved from the external data source, you can add filters to datasets, data regions, and data region groups, including detail groups. Filters are applied at run time first on the dataset, and then on the data region, and then on the group, in top-down order for group hierarchies. In a table, matrix, or list, filters for row groups, column groups, and adjacent groups are applied independently. In a chart, filters for category groups and series groups are applied independently. For more information, see [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](add-dataset-filters-data-region-filters-and-group-filters.md).  
  
 For each filter, you specify a *filter equation*. A filter equation includes a dataset field or expression that specifies the data to filter, an operator, and a value to compare. Only those data values that match the filter condition are included when the item is processed.  
  
 To enable your users to help control the data in a report, you can include parameters in filter expressions. For more information, see [Parameters Collection References &#40;Report Builder and SSRS&#41;](built-in-collections-parameters-collection-references-report-builder.md).  
  
 To customize a view for each user, you can include a reference to the built-in field UserID in a filter. For more information, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](built-in-collections-built-in-globals-and-users-references-report-builder.md).

##  <a name="Grouping"></a> Grouping Data in the Report  
 Groups organize data in a report for display or for calculating aggregate values. Understanding how to define groups and use group features helps you to design reports that are more concise.  
  
 Group expressions are created automatically when you do the following:  
  
-   Arrange dataset fields in a Table, Matrix, Chart wizard or match fields in the Map wizard.  
  
-   In a table, matrix, or list, add a field to the Row Groups or Column Groups area in the Grouping pane.  
  
-   In a chart, add a field to the Category Groups or Series Groups area in the Chart data pane.  
  
-   In a map, specify a field to match map elements with analytical data in the Layer Data context menu item.  
  
 A group is a part of the report definition. Each group has a name. By default, the group name is the dataset field that it is based on.  
  
 In a table or matrix data region, you can create multiple row groups and column groups. You can display your data in a visual hierarchy by organizing nested groups, adjacent groups, and recursive hierarchy groups (such as an organizational chart).  
  
 The group name identifies an expression scope. You can specify the name of a group as a scope in which to calculate aggregates, to organize data hierarchically and toggle the display of child nodes from parent nodes in a drilldown report, to display different views of the same data on multiple data regions, and to visualize summary data in a table, matrix, chart, gauge, or map. For more information, see [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
 To group on several dataset fields, add each field to the set of group expressions. You can also write your own group expressions in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]. For example, you can group by a range of values, or by using a report parameter to enable your user to select how to group data in a data region. For more information, see [Group Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md).  
  
 For report presentation, you can add page breaks before and after each group, or each instance of a group, to reduce the amount of data on each page and help you manage report rendering performance. For more information, see [Add a Page Break &#40;Report Builder and SSRS&#41;](add-a-page-break-report-builder-and-ssrs.md).  
  
 Creating data region groups is one way to organize data in a report. There are several other ways to organize data, each with its own advantages. For more information, see [Drillthrough, Drilldown, Subreports, and Nested Data Regions &#40;Report Builder and SSRS&#41;](drillthrough-drilldown-subreports-and-nested-data-regions.md).  
  
### Defining Group Variables  
 When you define a group, you can create a group variable to use in expressions that are scoped to the group and accessed from nested groups. A group variable is calculated once per group instance and can be accessed from expressions in child groups. For example, for data that is grouped by region and subregion, you can calculate a tax for each region and use that tax in calculations from the subregion group.  
  
 For more information, see [Report and Group Variables Collections References &#40;Report Builder and SSRS&#41;](built-in-collections-report-and-group-variables-references-report-builder.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
### Groups and Scope in Data Regions  
 To provide multiple views of data from the same dataset, you can specify the same group expressions for eac data region. For example, you can display categorized data in a table to show all detail data and in a pie chart to show aggregates and to help visualize each category in relation to the entire dataset. For more information, see [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md).  
  
 When you nest a data region in a cell in a table, matrix, or list, you are automatically scoping the data to the innermost group memberships of the cell. For example, assume that you add a chart to a cell that is in both a row group and a column group. The data available to that chart is scoped to the innermost row group instance and innermost column group instance at run time. For more information, see [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  

##  <a name="Sorting"></a> Sorting Data in the Report  
 To control the sort order of data in your report, you can sort data in a dataset query, or define a sort expression for a data region or group. You can also add interactive sort buttons to tables and matrices to enable a user to change the sort order for rows.  
  
 All three types of sorts can be combined in the same report. By default, sort order is determined by the order in which data is returned by the dataset query. Sort expressions are applied in the data region and data region group. Interactive sorts are applied after sort expressions.  
  
 For expressions that contain aggregate functions, most results are not affected by sort order. Return values for the following aggregate functions are affected by sort order:: First, Last, and Previous. For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md).  
  
### Sorting Data in a Dataset Query  
 Include sort order in the dataset query to pre-sort data before it is retrieved for a report. By sorting data in the query, the sorting work is done by the data source instead of by the report processor.  
  
 For a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source type, you can add an ORDER BY clause to the dataset query. For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] query sorts the columns Sales and Region by Sales in descending order from the table SalesOrders: `SELECT Sales, Region FROM SalesOrders ORDER BY Sales DESC`. For more information, see "Sorting Rows with ORDER BY" in [SQL Server Books Online](https://go.microsoft.com/fwlink/?linkid=98335).  
  
> [!NOTE]  
>  Not all data sources support the ability to specify sort order in the query.  
  
### Sorting Data with Sort Expressions  
 To sort data in the report after it is retrieved from the data source, you can set sort expressions on a Tablix data region or a group, including the details group. The following list describes the effect of setting sort expressions on different items:  
  
-   **Tablix data region.** Set sort expressions on a table, matrix, or list data region to control the sort order of data in the data region, after dataset filters and data region filters are applied at run time.  
  
-   **Tablix data region group.** Set sort expressions for each group, including the details group, to control the sort order of group instances. For example, for the details group, you control the order of the detail rows. For a child group, you control the order of group instances for the child group within the parent group. By default, when you create a group, the sort expression is set to the group expression and to ascending order.  
  
     If you have only one details group, you can define a sort expression in the query, on the data region, or on the details group to the same effect.  
  
-   **Chart data region.** Set a sort expression for the category and series groups to control the sort order for data points. By default, the order of data points is also the order of the colors in the chart legend. For more information, see [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](formatting-series-colors-on-a-chart-report-builder-and-ssrs.md).  
  
-   **Map report item.** You do not typically need to sort data for a map data region because the map groups data to display on map elements.  
  
-   **Gauge data region.** You do not typically need to sort data for a gauge data region because the gauge displays a single value relative to a range. If you do need sort data in a gauge, you must first define a group, and then set a sort expression for the group.  
  
#### Sorting by a Different Value  
 You might want to sort the rows in a data region by a value other than the field value. For example, suppose that the field Size contains text values that correspond to small, medium, large, and extra large. By default, the sort expression for a row group based on Size is also [Size]. To have more control over the way that data is sorted, you can add a field to the dataset query that defines the sort order that you want.  
  
 Alternatively, you can define a dataset that includes only the sizes and a value that specifies the order that you want. You can change the sort expression to use the Lookup function for the sort order value.  
  
 For example, assume that the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] query defines a dataset named Sizes. The query uses a CASE statement to define a sort order value SizeSortOrder for each value of Size:  
  
```  
SELECT Size,   
  CASE Size  
        WHEN 'S' THEN 1  
        WHEN 'M' THEN 2    
        WHEN 'L' THEN 3  
        WHEN 'XL' THEN 4  
        ELSE 0  
  END as SizeSortOrder  
FROM Production.Product  
```  
  
 In a table that has a row group based on `[Size]`, you can change the group sort expression to use a Lookup function to find the numeric field that corresponds to the size value. The expression would be similar to this:  
  
```  
=Lookup(Fields!Size.Value, Fields!Size.Value, Fields!SizeSortOrder.Value, "Sizes")  
```  
  
 For more information, see [Sort Data in a Data Region &#40;Report Builder and SSRS&#41;](sort-data-in-a-data-region-report-builder-and-ssrs.md) and [Lookup Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookup-function.md).  
  
###  <a name="Interactive"></a> Adding Interactive Sorting for the User  
 To enable a user to change the sort order of report data in a table or matrix, you can add interactive sort buttons to column headers or group headers. Users can click the button to toggle the sort order. Interactive sort is supported in rendering formats that allow user interaction, such as HTML.  
  
 You add interactive sort buttons to a text box in a tablix data region cell. By default, every cell contains a text box. In the text box properties, you specify which part of a table or matrix data region to sort (the parent group values, the child group values, or the detail rows), what to sort by, and whether to apply the sort expression to other report items that have a peer relationship. For example, if a table and a chart that provide views on the same dataset are contained in a rectangle, they are peer data regions. When a user toggles the sort order in the table, the sort order for the chart also toggles. For more information, see [Interactive Sort &#40;Report Builder and SSRS&#41;](interactive-sort-report-builder-and-ssrs.md).  

##  <a name="HowTo"></a> How-To Topics  
 [Keep Headers Visible When Scrolling Through a Report &#40;Report Builder and SSRS&#41;](keep-headers-visible-when-scrolling-through-a-report-report-builder-and-ssrs.md)  
  
 [Display Headers and Footers with a Group &#40;Report Builder and SSRS&#41;](display-headers-and-footers-with-a-group-report-builder-and-ssrs.md)  
  
 [Add Interactive Sort to a Table or Matrix &#40;Report Builder and SSRS&#41;](add-interactive-sort-to-a-table-or-matrix-report-builder-and-ssrs.md)  
  
 [Set a No Data Message for a Data Region &#40;Report Builder and SSRS&#41;](../report-data/set-a-no-data-message-for-a-data-region-report-builder-and-ssrs.md)  
  
 [Create a Recursive Hierarchy Group &#40;Report Builder and SSRS&#41;](create-a-recursive-hierarchy-group-report-builder-and-ssrs.md)  
  
 [Add or Delete a Group in a Data Region &#40;Report Builder and SSRS&#41;](add-or-delete-a-group-in-a-data-region-report-builder-and-ssrs.md)  
  
 [Display Headers and Footers with a Group &#40;Report Builder and SSRS&#41;](display-headers-and-footers-with-a-group-report-builder-and-ssrs.md)  
  
 [Add or Delete a Group in a Chart &#40;Report Builder and SSRS&#41;](add-or-delete-a-group-in-a-chart-report-builder-and-ssrs.md)  
  
 [Add a Total to a Group or Tablix Data Region &#40;Report Builder and SSRS&#41;](add-a-total-to-a-group-or-tablix-data-region-report-builder-and-ssrs.md)  
  
##  <a name="Section"></a> In This Section  
 [Group Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)  
  
 [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md)  
  
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](add-dataset-filters-data-region-filters-and-group-filters.md)  
  
##  <a name="Related"></a> Related Sections  
 [Understanding Groups &#40;Report Builder and SSRS&#41;](understanding-groups-report-builder-and-ssrs.md)  
  
 [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](creating-recursive-hierarchy-groups-report-builder-and-ssrs.md)  
  
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
 [Report and Group Variables Collections References &#40;Report Builder and SSRS&#41;](built-in-collections-report-and-group-variables-references-report-builder.md)  
  
 [Displaying a Series with Multiple Data Ranges on a Chart &#40;Report Builder and SSRS&#41;](displaying-a-series-with-multiple-data-ranges-on-a-chart.md)  
  
 [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md)  
  
## See Also  
 [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md)   
 [Sparklines and Data Bars &#40;Report Builder and SSRS&#41;](sparklines-and-data-bars-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](gauges-report-builder-and-ssrs.md)   
 [Indicators &#40;Report Builder and SSRS&#41;](indicators-report-builder-and-ssrs.md)  
