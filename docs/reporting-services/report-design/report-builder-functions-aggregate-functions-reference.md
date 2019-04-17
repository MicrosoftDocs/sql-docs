---
title: "Aggregate Functions Reference (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: db6542ee-02d0-4073-90e6-cba8f9510fbb
author: markingmyname
ms.author: maghan
---
# Report Builder Functions - Aggregate Functions Reference
  To include aggregated values in your report, you can use built-in aggregate functions in expressions. The default aggregate function for numeric fields is SUM. You can edit the expression and use a different built-in aggregate function or specify a different scope. Scope identifies which set of data to use for the calculation.  
  
 As the report processor combines report data and the report layout, the expressions for each report item are evaluated. As you view each page of the report, you see the results for each expression in the rendered report items.  
  
 The following table lists categories of built-in functions that you can include in an expression:  
  
-   [Built-in Aggregate Functions](#CalculatingAggregates)  
  
-   [Restrictions on Built-in Fields, Collections, and Aggregate Functions](#Restrictions)  
  
-   [Restrictions on Nested Aggregates](#NestedRestrictions)  
  
-   [Calculating Running Values](#CalculatingRunningValues)  
  
-   [Retrieving Row Counts](#RetrievingRowCounts)  
  
-   [Looking Up Values from Another Dataset](#LookupFunctions)  
  
-   [Retrieving Sort-Dependent Values](#RetrievingPostsortValues)  
  
-   [Retrieving Server Aggregates](#RetrievingServerAggregates)  
  
-   [Retrieving Recursive Level](#RetrievingRecursiveLevel)  
  
-   [Testing for Scope](#TestingforScope)  
  
 To determine the valid scopes for a function, see the individual function reference topic. For more information and for examples, see [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="CalculatingAggregates"></a> Built-in Aggregate Functions  
 The following built-in functions calculate summary values for a set of non-null numeric data in the default scope or the named scope.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[Avg](../../reporting-services/report-design/report-builder-functions-avg-function.md)|Returns the average of all non-null numeric values specified by the expression, evaluated in the given scope.|  
|[Count](../../reporting-services/report-design/report-builder-functions-count-function.md)|Returns a count of non-null values specified by the expression, evaluated in the context of the given scope.|  
|[CountDistinct](../../reporting-services/report-design/report-builder-functions-countdistinct-function.md)|Returns a count of all distinct non-null values specified by the expression, evaluated in the context of the given scope.|  
|[Max](../../reporting-services/report-design/report-builder-functions-max-function.md)|Returns the maximum value of all non-null numeric values specified by the expression, in the context of the given scope. You can use this for specifying a chart axis maximum value to control the scale.|  
|[Min](../../reporting-services/report-design/report-builder-functions-min-function.md)|Returns the minimum value of all non-null numeric values specified by the expression, in the context of the given scope. You can use this for specifying a chart axis minimum value to control the scale.|  
|[StDev](../../reporting-services/report-design/report-builder-functions-stdev-function.md)|Returns the standard deviation of all non-null numeric values specified by the expression, evaluated in the given scope.|  
|[StDevP](../../reporting-services/report-design/report-builder-functions-stdevp-function.md)|Returns the population standard deviation of all non-null numeric values specified by the expression, evaluated in the context of the given scope.|  
|[Sum](../../reporting-services/report-design/report-builder-functions-sum-function.md)|Returns the sum of all the non-null numeric values specified by the expression, evaluated in the given scope.|  
|[Union](../../reporting-services/report-design/report-builder-functions-union-function.md)|Returns the union of all the non-null spatial data values of type **SqlGeometry** or **SqlGeography** that are specified by the expression, evaluated in the given scope.|  
|[Var](../../reporting-services/report-design/report-builder-functions-var-function.md)|Returns the variance of all non-null numeric values specified by the expression, evaluated in the given scope.|  
|[VarP](../../reporting-services/report-design/report-builder-functions-varp-function.md)|Returns the population variance of all non-null numeric values specified by the expression, evaluated in the context of the given scope.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="Restrictions"></a> Restrictions on Built-in Fields, Collections, and Aggregate Functions  
 The following table summarizes restrictions in report locations on where you can add expressions that contain references to global built-in collections.  
  
|Location in Report|Fields|Parameters|ReportItems|PageNumber<br /><br /> TotalPages|DataSource<br /><br /> DataSet|Variables|RenderFormat|  
|------------------------|------------|----------------|-----------------|-------------------------------|----------------------------|---------------|------------------|  
|Page Header<br /><br /> Page Footer|Yes|Yes|At most one<br /><br /> Note 1|Yes|Yes|Yes|Yes|  
|Body|Yes<br /><br /> Note 2|Yes|Only items in the currnet scope or a containing scope<br /><br /> Note 3|No|Yes|Yes|Yes|  
|Report Parameter|No|Only parameters earlier in the list<br /><br /> Note 4|No|No|No|No|No|  
|Field|Yes|Yes|No|No|No|No|No|  
|Query Parameter|No|Yes|No|No|No|No|No|  
|Group Expression|Yes|Yes|No|No|Yes|No|No|  
|Sort Expression|Yes|Yes|No|No|Yes|Yes<br /><br /> Note 5|No|  
|Filter Expression|Yes|Yes|No|No|Yes|Yes<br /><br /> Note 6|No|  
|Code|No|Yes<br /><br /> Note 7|No|No|No|No|No|  
|Report.Language|No|Yes|No|No|No|No|No|  
|Variables|Yes|Yes|No|No|Yes|Current or containing scope|No|  
|Aggregates|Yes|Yes|Only in page header/page footer|Only in report item aggregates|Yes|No|No|  
|Lookup functions|Yes|Yes|Yes|No|Yes|No|No|  
  
-   **Note 1.** ReportItems must exist in the rendered report page, or their value is Null. If the visibility of a report item depends on an expression that evaluates to False, the report item does not exist on the page.  
  
-   **Note 2.** If a field reference is used in a group scope, and the field reference is not included in the group expression, then the value for the field is undefined, unless there is only one value in the scope. To specify a value, use First or Last and the group scope.  
  
-   **Note 3.** Expressions that include a reference to ReportItems can specify values for other ReportItems in the same group scope or in a containing group scope.  
  
-   **Note 4.** Property values for earlier parameters might be null.  
  
-   **Note 5.** In Member sorts only. Cannot use in data region sort expressions.  
  
-   **Note 6.** In Member filters only. Cannot use in data region or dataset filter expressions.  
  
-   **Note 7.** The Parameters collection is not initialized until after the Code block is processed, so methods cannot be used to control parameters on initialization.  
  
-   **Note 8.** Data type for all aggregates except Count and CountDistinct must be the same data type, or null, for all values.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="NestedRestrictions"></a> Restrictions on Nested Aggregates  
 The following table summarizes restrictions on which aggregates functions can specify other aggregate functions as nested aggregates.  
  
|Context|RunningValue|RowNumber|First<br /><br /> Last|Previous|Sum and Other Presort functions|ReportItem aggregates|Lookup functions|Aggregate Function|  
|-------------|------------------|---------------|--------------------|--------------|-------------------------------------|---------------------------|----------------------|------------------------|  
|Running Value|No|No|No|No|Yes|No|Yes|No|  
|First<br /><br /> Last|No|No|No|No|Yes|No|No|No|  
|Previous|Yes|Yes|Yes|No|Yes|No|Yes|No|  
|Sum and other Presort functions|No|No|No|No|Yes|No|Yes|No|  
|ReportItem aggregates|No|No|No|No|No|No|No|No|  
|Lookup functions|Yes|Yes<br /><br /> Note 1|Yes<br /><br /> Note 1|Yes<br /><br /> Note 1|Yes<br /><br /> Note 1|Yes<br /><br /> Note 1|No|No|  
|Aggregate Function|No|No|No|No|No|No|No|No|  
  
-   **Note 1.** Aggregate functions are only allowed inside the *Source* expression of a Lookup function if the Lookup function is not contained in an aggregate. Aggregate functions are not allowed inside the *Destination* or *Result* expressions of a Lookup function.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="CalculatingRunningValues"></a> Calculating Running Values  
 The following built-in functions calculate running values for a set of data. **RowNumber** is like **RunningValue** in that it returns the running value of a count that increments for each row within the containing scope. The scope parameter for these functions must specify a containing scope, which controls when the count restarts.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[RowNumber](../../reporting-services/report-design/report-builder-functions-rownumber-function.md)|Returns a running count of the number of rows for the specified scope. The **RowNumber** function restarts counting at 1, not 0.|  
|[RunningValue](../../reporting-services/report-design/report-builder-functions-runningvalue-function.md)|Returns a running aggregate of all non-null numeric values specified by the expression, evaluated for the given scope.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="RetrievingRowCounts"></a> Retrieving Row Counts  
 The following built-in function calculates the number of rows in the given scope. Use this function to count all rows, including rows with null values.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[CountRows](../../reporting-services/report-design/report-builder-functions-countrows-function.md)|Returns the number of rows in the specified scope, including rows with null values.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="LookupFunctions"></a> Looking Up Values from Another Dataset  
 The following lookup functions retrieve values from a specified dataset.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[Lookup Function](../../reporting-services/report-design/report-builder-functions-lookup-function.md)|Returns a value from a dataset for a specified expression.|  
|[LookupSet Function](../../reporting-services/report-design/report-builder-functions-lookupset-function.md)|Returns a set of values from a dataset for a specified expression.|  
|[Multilookup Function](../../reporting-services/report-design/report-builder-functions-multilookup-function.md)|Returns the set of first-match values for a set of names from a dataset that contains name/value pairs.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="RetrievingPostsortValues"></a> Retrieving Sort-Dependent Values  
 The following built-in functions return the first, last, or previous value within a given scope. These functions depend on the sort order of the data values. Use these functions, for example, to find the first and last values on a page to create a dictionary-style page header. Use **Previous** to compare a value in one row to the previous row's value within a specific scope, for example, to find percentage year over year values in a table.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[First](../../reporting-services/report-design/report-builder-functions-first-function.md)|Returns the first value in the given scope of the specified expression.|  
|[Last](../../reporting-services/report-design/report-builder-functions-last-function.md)|Returns the last value in the given scope of the specified expression.|  
|[Previous](../../reporting-services/report-design/report-builder-functions-previous-function.md)|Returns the value or the specified aggregate value for the previous instance of an item within the specified scope.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="RetrievingServerAggregates"></a> Retrieving Server Aggregates  
 The following built-in function retrieves custom aggregates from the data provider. For example, using an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source type, you can retrieve aggregates calculated on the data source server for use in a group header.  
  
|**Function**|**Description**|  
|------------------|---------------------|  
|[Aggregate](../../reporting-services/report-design/report-builder-functions-aggregate-function.md)|Returns a custom aggregate of the specified expression, as defined by the data provider.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="TestingforScope"></a> Testing for Scope  
 The following built-in function tests the current context of a report item to see if it is a member of a specific scope.  
  
|Function|Description|  
|--------------|-----------------|  
|[InScope](../../reporting-services/report-design/report-builder-functions-inscope-function.md)|Indicates whether the current instance of an item is within the specified scope.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
##  <a name="RetrievingRecursiveLevel"></a> Retrieving Recursive Level  
 The following built-in function retrieves the current level when a recursive hierarchy is processed. Use the result of this function with the **Padding** property in a text box to control the indent level of a visual hierarchy for a recursive group. For more information, see [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/creating-recursive-hierarchy-groups-report-builder-and-ssrs.md).  
  
|Function|Description|  
|--------------|-----------------|  
|[Level](../../reporting-services/report-design/report-builder-functions-level-function.md)|Returns the current level of depth in a recursive hierarchy.|  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link")Back to Top  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
