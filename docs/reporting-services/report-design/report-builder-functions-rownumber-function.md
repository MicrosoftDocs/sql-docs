---
title: "RowNumber Function (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 9d718ba8-d323-49fb-aac8-e7013a117b75
author: markingmyname
ms.author: maghan
---
# Report Builder Functions - RowNumber Function
  Returns a running count of the number of rows for the specified scope.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
RowNumber(scope)  
```  
  
#### Parameters  
 *scope*  
 (**String**) The name of a dataset, data region, or group, or null (**Nothing** in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]), that specifies the context in which to evaluate the number of rows. **Nothing** specifies the outermost context, usually the report dataset.  
  
## Remarks  
 **RowNumber** returns a running value of the count of rows within the specified scope, just as [RunningValue](../../reporting-services/report-design/report-builder-functions-runningvalue-function.md) returns the running value of an aggregate function. When you specify a scope, you specify when to reset the row count to 1.  
  
 *scope* cannot be an expression. *scope* must be a containing scope. Typical scopes, from the outermost to the innermost containment, are report dataset, data region, row groups or column groups.  
  
 To increment values across columns, specify a scope that is the name of a column group. To increment numbers down rows, specify a scope that is the name of a row group.  
  
> [!NOTE]  
>  Including aggregates that specify both a row group and a column group in a single expression is not supported.  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
## Code Example  
 The following is an expression that you can use for the **BackgroundColor** property of a Tablix data region detail row to alternate the color of detail rows for each group, always beginning with White.  
  
```  
=IIF(RowNumber("GroupbyCategory") Mod 2, "White", "PaleGreen")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
