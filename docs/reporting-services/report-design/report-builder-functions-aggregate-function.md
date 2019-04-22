---
title: "Aggregate Function (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/15/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 16ce643f-bbb3-40a5-ba78-7aed73156f3e
author: maggiesMSFT
ms.author: maggies
---
# Report Builder Functions - Aggregate Function
  Returns a custom aggregate of the specified expression, as defined by the data provider.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
Aggregate(expression, scope)  
```  
  
#### Parameters  
 *expression*  
 The expression on which to perform the aggregation. The expression must be a simple field reference.  
  
 *scope*  
 (**String**) The name of a dataset, group, or data region that contains the report items to which to apply the aggregate function. *Scope* must be a string constant andcannot be an expression. If *scope* is not specified, the current scope is used.  
  
## Return Type  
 Return type is determined by the data provider. Returns **Nothing** if the data provider does not support this function or data is not available.  
  
## Remarks  
 The **Aggregate** function provides a way to use aggregates that are calculated on the external data source. Support for this feature is determined by the data extension. For example, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension retrieves flattened rowsets from an MDX query. Some rows in the result set can contain aggregate values calculated on the data source server. These are known as *server aggregates*. To view server aggregates in the graphical query designer for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the **Show Aggregate** button on the toolbar. For more information, see [Analysis Services MDX Query Designer User Interface &#40;Report Builder&#41;](https://msdn.microsoft.com/library/7e288eee-2d37-485e-a6a0-dbba5e041e26).  
  
 When you display the combination of aggregate and detail dataset values on detail rows of a Tablix data region, server aggregates would not typically be included because they are not detail data. However, you may want to display all values retrieved for the dataset and customize the way aggregate data is calculated and displayed.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] detects the use of the **Aggregate** function in expressions in your report in order to determine whether to display server aggregates on detail rows. If you include **Aggregate** in an expression in a data region, server aggregates can only appear on group total or grand total rows, not on detail rows. If you want to display server aggregates on detail rows, do not use the **Aggregate** function.  
  
 You can change this default behavior by changing the value of the **Interpret subtotals as details** option on the **Dataset Properties** dialog box. When this option is set to **True**, all data, including server aggregates, appears as detail data. When set to **False**, server aggregates appear as totals. The setting for this property affects all data regions that are linked to this dataset.  
  
> [!NOTE]
>  All containing groups for the report item that references **Aggregate** must have simple field references for their group expressions, for example, `[FieldName]`. You cannot use **Aggregate** in a data region that uses complex group expressions. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension, your query must include MDX fields of type **LevelProperty** (not **MemberProperty**) to support aggregation using the **Aggregate**function.  
  
 *Expression* can contain calls to nested aggregate functions with the following exceptions and conditions:  
  
-   *Scope* for nested aggregates must be the same as, or contained by, the scope of the outer aggregate. For all distinct scopes in the expression, one scope must be in a child relationship to all other scopes.  
  
-   *Scope* for nested aggregates cannot be the name of a dataset.  
  
-   *Expression* must not contain **First**, **Last**, **Previous**, or **RunningValue** functions.  
  
-   *Expression* must not contain nested aggregates that specify *recursive*.  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
 For more information about recursive aggregates, see [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/creating-recursive-hierarchy-groups-report-builder-and-ssrs.md).  
  
## Comparing the Aggregate and Sum Functions  
 The **Aggregate** function differs from numeric aggregate functions like **Sum** in that the **Aggregate** function returns a value that is calculated by the data provider or data processing extension. Numeric aggregate functions like **Sum** return a value that is calculated by the report processor on a set of data from the dataset that is determined by the *scope* parameter. For more information, see the aggregate functions listed in [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md).  
  
## Example  
 The following code example shows an expression that retrieves a server aggregate for the field `LineTotal`. The expression is added to a cell in a row that belongs to the group `GroupbyOrder`.  
  
```  
=Aggregate(Fields!LineTotal.Value, "GroupbyOrder")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
