---
title: "Expression Scope for Totals, Aggregates, and Built-in Collections (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a8d24287-8557-4b03-bea7-ca087f449b62
author: markingmyname
ms.author: maghan
manager: kfile
---
# Expression Scope for Totals, Aggregates, and Built-in Collections (Report Builder and SSRS)
  When you write expressions, you will find that  the term *scope* is used in multiple contexts. Scope can specify the data to use for evaluating an expression, the set of text boxes on a rendered page, the set of report items that can be shown or hidden based on a toggle. You will see the term *scope* in topics that relate to expression evaluation, aggregate function syntax, conditional visibility, and also in error messages related to these areas. Use the following descriptions to help differentiate which meaning of *scope* applies:  
  
-   **Data scope** Data scope is a hierarchy of scopes that the report processor uses as it combines report data and report layout, and builds out data regions such as tables and charts on which to display the data. Understanding data scope helps you to get the results that you want when you do the following:  
  
    -   **Write expressions that use aggregate functions** Specify which data to aggregate. The location of the expression in the report influences which data is in scope for aggregate calculations.  
  
    -   **Add sparklines to a table or matrix** Specify a minimum and maximum range for chart axes to align nested instances in a table or matrix.  
  
    -   **Add indicators to a table or matrix** Specify a minimum and maximum scale for the gauge to align nested instances in a table or matrix.  
  
    -   **Write sort expressions** Specify a containing scope that you can use to synchronize sort order among multiple related report items.  
  
-   **Cell scope** Cell scope is the set of row and column groups in a tablix data region to which a cell belongs. By default, each tablix cell contains a text box. The value of the text box is the expression. The location of the cell indirectly determines which data scopes you can specify for aggregate calculations in the expression.  
  
-   **Report item scope** Report item scope refers to the collection of items on a rendered report page. The report processor combines data and report layout elements to produce a compiled report definition. During this process, data regions such as tables and matrices expand as needed to display all of the report data. The compiled report is then processed by a report renderer. The report renderer determines which report items appear on each page. On a report server, each page is rendered as you view it. When you export a report, all pages are rendered. Understanding report item scope helps you get the results that you want when you do the following:  
  
    -   **Add toggle items** Specify a text box to add the toggle that controls the visibility of a report item. You can only add a toggle to text boxes that are in the scope of the report item that you want to toggle.  
  
    -   **Write expressions in page headers and footers** Specify values in expressions in text boxes or other report items that appear on the rendered page.  
  
 Understanding scopes helps you to successfully write expressions that give you the results that you want.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="DataScope"></a> Understanding Data Scope and Data Hierarchy  
 Data scope specifies a set of report data. Data scope has a natural hierarchy with an inherent containment relationship. Scopes higher on the hierarchy contain scopes that are lower on the hierarchy. The following list of data scopes describes the hierarchy in order from most data to least data:  
  
-   **Datasets, after dataset filters are applied** Specifies the report dataset linked to the data region or to a report item in the report body. The data used for aggregation is from the report dataset after dataset filter expressions are applied. For shared datasets, this means both the filters in the shared dataset definition and the filters in the shared dataset instance in the report.  
  
-   **Data regions** Specifies data from the data region after data region filter and sort expressions are applied. Group filters are not used when calculating aggregates for data regions.  
  
-   **Data region groups, after group filters are applied** Specifies the data after the group expressions and group filters are applied for the parent group and child groups. For a table, this is the row and column groups. For a chart, this is the series and category groups. For the purposes of identifying scope containment, every parent group contains its child groups.  
  
-   **Nested data regions** Specifies the data for the nested data region in the context of the cell to which it has been added, and after the nested data region filter and sort expressions have been applied.  
  
-   **Row and column groups for the nested data regions** Specifies the data after the nested data region group expressions and group filters have been applied.  
  
 Understanding containing and contained scopes is important when you write expressions that include aggregate functions.  
  
##  <a name="Aggregates"></a> Cell Scope and Expressions  
 When you specify a scope, you are indicating to the report processor which data to use for an aggregate calculation. Depending on the expression and the location of the expression, valid scopes might be a *containing scopes*, also known as parent scopes, or a *contained scopes*, also known as child or nested scopes. In general, you cannot specify an individual group instance in an aggregate calculation. You can specify an aggregate across all group instances.  
  
 As the report processor combines data from a report dataset with the tablix data region, it evaluates group expressions and creates the rows and columns that are needed to represent the group instances. The value of expressions in a text box in each tablix cell is evaluated in the context of the cell scope. Depending on the tablix structure, a cell can belong to multiple row groups and column groups. For aggregate functions, you can specify which scope to use by using one of the following scopes:  
  
-   **Default scope** The data that is in scope for calculations when the report processor evaluates an expression. The default scope is the innermost set of groups to which the cell or data point belongs. For a tablix data region, the set can include row and column groups. For a chart data region, the set can include category and series groups.  
  
-   **Named scope** The name of a dataset, a data region, or a data region group that is in scope for the expression. For aggregate calculations, you can specify a containing scope. You cannot specify a named scope for both a row group and a column group in a single expression. You cannot specify a contained scope unless the expression is for an aggregate of an aggregate.  
  
     The following expression generates the interval years between SellStartDate and LastReceiptDate. These fields are in two different datasets, DataSet1 and DataSet2. The [First Function &#40;Report Builder and SSRS&#41;](report-builder-functions-first-function.md), which is an aggregate function, returns the first value of SellStartDate in DataSet1 and the first value of LastReceiptDate in DataSet2.  
  
    ```  
    =DATEDIFF("yyyy", First(Fields!SellStartDate.Value, "DataSet1"), First(Fields!LastReceiptDate.Value, "DataSet2"))  
    ```  
  
-   **Domain scope** Also called synchronization scope. A type of data scope that applies to expression evaluation for nested data regions. Domain scope is used to specify aggregates across all instances of a group so that nested instances can be aligned and easily compared. For example, you can align the range and height for sparklines embedded in a table so that the values line up.  
  
 In some locations of a report, you must specify a scope. For example, for a text box on the design surface, you must specify the name of the dataset to use: `=Max(Fields!Sales.Value,"Dataset1")`. In other locations, there is an implicit default scope. For example, if you do not specify an aggregate for a text box in a group scope, the default aggregate First is used.  
  
 Each aggregate function topic lists the scopes that are valid for its use. For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md).  
  
##  <a name="Examples"></a> Example Aggregate Expressions for a Table Data Region  
 To write expressions that specify non-default scopes takes some practice. To help you understand different scopes, use the following figure and table. The figure labels each cell in a sales information table that displays quantity of items sold by year and quarter and also by sales territory. Note the visual cues on the row handles and column handles that display row and column group structure, indicating nested groups. The table has the following structure:  
  
-   A table header that contains the corner cell and three rows that include the column group headers.  
  
-   Two nested row groups based on category named Cat and subcategory named SubCat.  
  
-   Two nested column groups based on year named Year and quarter named Qtr.  
  
-   One static totals column labeled Totals.  
  
-   One adjacent column group based on sales territory named Territory.  
  
 The column header for the territory group has been split into two cells for display purposes. The first cell displays the territory name and totals, and the second cell has placeholder text that calculated the percentage contribution for each territory to all sales.  
  
 ![rs_BasicTableSumCellScope](../media/rs-basictablesumcellscope.gif "rs_BasicTableSumCellScope")  
  
 Assume the dataset is named DataSet1 and the table is named Tablix1. The following table lists the cell label, the default scope, and examples. The values for placeholder text are shown by in expression syntax.  
  
|Cell|Default scope|Placeholder labels|Text or placeholder values|  
|----------|-------------------|------------------------|--------------------------------|  
|C01|Tablix1|[Sum(Qty)]|Aggregates and Scope<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C02|Outer column group "Year"|[Year]<br /><br /> ([YearQty])|`=Fields!Year.Value`<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C03|Tablix1|[Sum(Qty)]|Totals<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C04|Peer column group "Territory"|([Total])|Territory<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C05|Inner group "Qtr"|[Qtr]<br /><br /> ([QtrQty])|Q<br /><br /> `=Fields!Qtr.Value`<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C06|Peer column group "Territory"|[Territory]<br /><br /> ([Tty])<br /><br /> [Pct]|`=Fields!Territory.Value`<br /><br /> `=Sum(Fields!Qty.Value)`<br /><br /> `=FormatPercent(Sum(Fields!Qty.Value,"Territory")/Sum(Fields!Qty.Value,"Tablix1"),0) & " of " & Sum(Fields!Qty.Value,"Tablix1")`|  
|C07|Outer row group "Cat"|[Cat]<br /><br /> [Sum(Qty)]|`=Fields!Cat.Value`<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C08|Same as C07|||  
|C09|Outer row group "Cat" and inner column group "Qtr"|[Sum(Qty)]|`=Sum(Fields!Qty.Value)`|  
|C10|Same as C07|<\<Expr>>|`=Sum(Fields!Qty.Value) & ": " & FormatPercent(Sum(Fields!Qty.Value)/Sum(Fields!Qty.Value,"Tablix1"),0) & " of " & Sum(Fields!Qty.Value,"Tablix1")`|  
|C11|Outer row group "Cat" and column group "Territory"|<\<Expr>>|`=Sum(Fields!Qty.Value) & ": " & FormatPercent(Sum(Fields!Qty.Value)/Sum(Fields!Qty.Value,"Territory"),0) & " of " & Sum(Fields!Qty.Value,"Territory")`|  
|C12|Inner row group "Subcat"|[Subcat]<br /><br /> [Sum(Qty)]|`=Fields!SubCat.Value`<br /><br /> `=Sum(Fields!Qty.Value)`|  
|C13|Inner row group "Subcat" and inner column group "Qtr"|[Sum(Qty)]|`=Sum(Fields!Qty.Value)`|  
|C14|Inner row group "Subcat"|<\<Expr>>|`=Sum(Fields!Qty.Value) & ": " & FormatPercent(Sum(Fields!Qty.Value)/Sum(Fields!Qty.Value,"Cat"),0) & " of " & Sum(Fields!Qty.Value,"Cat")`|  
|C15|Inner row group "Subcat" and column group "Territory"|<\<Expr>>|`=Sum(Fields!Qty.Value) & ": " & FormatPercent(Code.CalcPercentage(Sum(Fields!Qty.Value),Sum(Fields!Qty.Value,"Cat")),0) & " of " & Sum(Fields!Qty.Value,"Cat")`|  
  
 For more information about interpreting visual cues on tablix data regions, see [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md). For more information about the tablix data region, see [Tablix Data Region Cells, Rows, and Columns &#40;Report Builder&#41; and SSRS](tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md). For more information about expressions and aggregates, see [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md) and [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md).  
  
  
  
##  <a name="Sparklines"></a> Synchronizing Scales for Sparklines  
 To compare values across time on the horizontal axis for a sparkline chart that is nested in a table or matrix, you can synchronize the category group values. This is called aligning axes. By selecting the option to align axes, the report automatically sets minimum and maximum values for an axis, and provides placeholders for aggregate values that do not exist in every category. This causes the values in the sparkline to line up across every category and enables you to compare values for each row of aggregated data. By selecting this option, you are changing the scope of the expression evaluation to the *domain scope*. Setting the domain scope for a nested chart also indirectly controls the color assignment for each category in the legend.  
  
 For example, in a sparkline that shows weekly trends, suppose that one city had sales data for 3 months and another city had sales data for 12 months. Without synchronized scales, the sparkline for first city would only have 3 bars and they would be much wider and occupy the same space as the 12 month set of bars for the second city.  
  
 For more information, see [Align the Data in a Chart in a Table or Matrix &#40;Report Builder and SSRS&#41;](align-the-data-in-a-chart-in-a-table-or-matrix-report-builder-and-ssrs.md).  
  
  
  
##  <a name="Indicators"></a> Synchronizing Ranges for Indicators  
 To specify the data values to use for a set of indicators, you must specify a scope. Depending on the layout of the data region that contains the indicator, you specify a scope or a containing scope. For example, in a group header row associated with category sales, a set of arrows (up, down, sideways) can indicate sales values relative to a threshold. The containing scope is the name of the table or matrix that contains the indicators.  
  
 For more information, see [Set Synchronization Scope &#40;Report Builder and SSRS&#41;](set-synchronization-scope-report-builder-and-ssrs.md).  
  
  
  
##  <a name="Page"></a> Specifying Scopes from the Page Header or Page Footer  
 To display data that is different on each page of a report, you add expressions to a report item that must be on the rendered page. Because a report is split into pages while it is rendered, only during rendering can it be determined which items exist on a page. For example, a cell in a detail row has a text box that has many instances on a page.  
  
 For this purpose, there is a global collection called ReportItems. This is the set of text boxes on the current page.  
  
 For more information, see [Page Headers and Footers &#40;Report Builder and SSRS&#41;](page-headers-and-footers-report-builder-and-ssrs.md) and [ReportItems Collection References &#40;Report Builder and SSRS&#41;](built-in-collections-reportitems-collection-references-report-builder.md).  
  
  
  
##  <a name="Toggles"></a> Specifying a Toggle Item for Drilldown and Conditional Visibility  
 Toggles are plus or minus sign images that are added to a text box and that a user can click to show or hide other report items. On the **Visibility** page for most report item properties, you can specify which report item to add the toggle to. The toggle item must be in a higher containment scope than the item to show or hide.  
  
 In a tablix data region, to create a drilldown effect where you click a text box to expand the table to show more data, you must set the **Visibility** property on the group, and select as the toggle a text box in a group header that is associated with a containing group.  
  
 For more information, see [Add an Expand or Collapse Action to an Item &#40;Report Builder and SSRS&#41;](add-an-expand-or-collapse-action-to-an-item-report-builder-and-ssrs.md).  
  
  
  
##  <a name="Sort"></a> Specifying a Sort Expression to Synchronize Sort Order  
 When you add an interactive sort button to a table column, you can synchronize sorting for multiple items that have a common containing scope. For example, you can add a sort button to a column header in a matrix, and specify the containing scope as the name of the dataset that is bound to the matrix. When a user clicks the sort button, not only are the matrix rows sorted, but also the chart series groups of charts that are bound to the same datset are sorted. In this way, all data regions that depend on that dataset can be synchronized to show the same sort order.  
  
 For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](filter-group-and-sort-data-report-builder-and-ssrs.md).  
  
  
  
##  <a name="Nulls"></a> Suppressing Null or Zero Values in a Cell  
 For many reports, calculations that are scoped to groups can create many cells that have zero (0) or null values. To reduce clutter in your report, add an expression to return blanks if the aggregate value is 0. For more information, see "Examples that Suppress Null or Zero Values" in [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md).  
  
  
  
## See Also  
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Group Expression Examples &#40;Report Builder and SSRS&#41;](group-expression-examples-report-builder-and-ssrs.md)   
 [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](creating-recursive-hierarchy-groups-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Formatting Text and Placeholders &#40;Report Builder and SSRS&#41;](formatting-text-and-placeholders-report-builder-and-ssrs.md)  
  
  
