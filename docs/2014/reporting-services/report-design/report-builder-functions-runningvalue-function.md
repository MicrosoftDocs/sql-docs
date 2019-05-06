---
title: "RunningValue Function (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 6bee2f15-0e69-49c8-9689-b04544063b1d
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# RunningValue Function (Report Builder and SSRS)
  Returns a running aggregate of all non-null numeric values specified by the expression, evaluated for the given scope.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
RunningValue(expression, function, scope)  
```  
  
#### Parameters  
 *expression*  
 The expression on which to perform the aggregation, for example, `[Quantity]`.  
  
 *function*  
 (`Enum`) The name of the aggregate function to apply to the expression, for example, `Sum`. This function cannot be `RunningValue`, `RowNumber`, or `Aggregate`.  
  
 *scope*  
 (`String`) A string constant that is the name of a dataset, data region, or group, or null (`Nothing` in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]), that specifies the context in which to evaluate the aggregation. `Nothing` specifies the outermost context, usually the report dataset.  
  
## Return Type  
 Determined by the aggregate function that is specified in the *function* parameter.  
  
## Remarks  
 The value for `RunningValue` resets to 0 for each new instance of the scope. If a group is specified, the running value is reset when the group expression changes. If a data region is specified, the running value is reset for each new instance of the data region. If a dataset is specified, the running value is not reset throughout the entire dataset.  
  
 `RunningValue` cannot be used in a filter or sort expression.  
  
 The set of data for which the running value is calculated must have the same data type. To convert data that has multiple numeric data types to the same data type, use conversion functions like `CInt`, `CDbl` or `CDec`. For more information, see [Type Conversion Functions](https://go.microsoft.com/fwlink/?LinkId=96142).  
  
 *Scope* cannot be an expression.  
  
 *Expression* can contain calls to nested aggregate functions with the following exceptions and conditions:  
  
-   Scope for nested aggregates must be the same as, or contained by, the scope of the outer aggregate. For all distinct scopes in the expression, one scope must be in a child relationship to all other scopes.  
  
-   Scope for nested aggregates cannot be the name of a dataset.  
  
-   *Expression* must not contain `First`, `Last`, `Previous`, or `RunningValue` functions.  
  
-   *Expression* must not contain nested aggregates that specify *recursive*.  
  
 To calculate the running value of the number of rows, use `RowNumber`. For more information, see [RowNumber Function &#40;Report Builder and SSRS&#41;](report-builder-functions-rownumber-function.md).  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
 For more information about recursive aggregates, see [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](creating-recursive-hierarchy-groups-report-builder-and-ssrs.md).  
  
## Examples  
 The following code example provides a running sum of the field named `Cost` in the outermost scope, which is the dataset.  
  
```  
=RunningValue(Fields!Cost.Value, Sum, Nothing)  
```  
  
 The following code example provides a running sum of the field named `Score` in the dataset named `DataSet1`.  
  
```  
=RunningValue(Fields!Score.Value,sum,"DataSet1")  
```  
  
 The following code example provides a running sum of the field named `Traffic Charges` in the outermost scope.  
  
```  
=RunningValue(Fields!Traffic Charges.Value, Sum, Nothing)  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
