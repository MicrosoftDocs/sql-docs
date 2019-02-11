---
title: "Lookup Function (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e60e5bab-b286-4897-9685-9ff12703517d
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Lookup Function (Report Builder and SSRS)
  Returns the first matching value for the specified name from a dataset that contains name/value pairs.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
Lookup(source_expression, destination_expression, result_expression, dataset)  
```  
  
#### Parameters  
 *source_expression*  
 (`Variant`) An expression that is evaluated in the current scope and that specifies the name or key to look up. For example, `=Fields!ProdID.Value`.  
  
 *destination_expression*  
 (`Variant`) An expression that is evaluated for each row in a dataset and that specifies the name or key to match on. For example, `=Fields!ProductID.Value`.  
  
 *result_expression*  
 (`Variant`) An expression that is evaluated for the row in the dataset where *source_expression* = *destination_expression*, and that specifies the value to retrieve. For example, `=Fields!ProductName.Value`.  
  
 *dataset*  
 A constant that specifies the name of a dataset in the report. For example, "Products".  
  
## Return  
 Returns a `Variant`, or `Nothing` if there is no match.  
  
## Remarks  
 Use `Lookup` to retrieve the value from the specified dataset for a name/value pair where there is a 1-to-1 relationship. For example, for an ID field in a table, you can use `Lookup` to retrieve the corresponding Name field from a dataset that is not bound to the data region.  
  
 `Lookup` does the following:  
  
-   Evaluates the source expression in the current scope.  
  
-   Evaluates the destination expression for each row of the specified dataset after filters have been applied, based on the collation of the specified dataset.  
  
-   On the first match of source expression and destination expression, evaluates the result expression for that row in the dataset.  
  
-   Returns the result expression value.  
  
 To retrieve multiple values for a single name or key field where there is a 1-to-many relationship, use [LookupSet Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookupset-function.md). To call `Lookup` for a set of values, use [Multilookup Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookup-function.md).  
  
 The following restrictions apply:  
  
-   `Lookup` is evaluated after all filter expressions are applied.  
  
-   Only one level of lookup is supported. A source, destination, or result expression cannot include a reference to a lookup function.  
  
-   Source and destination expressions must evaluate to the same data type. The return type is the same as the data type of the evaluated result expression.  
  
-   Source, destination, and result expressions cannot include references to report or group variables.  
  
-   `Lookup` cannot be used as an expression for the following report items:  
  
    -   Dynamic connection strings for a data source.  
  
    -   Calculated fields in a dataset.  
  
    -   Query parameters in a dataset.  
  
    -   Filters in a dataset.  
  
    -   Report parameters.  
  
    -   The Report.Language property.  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
## Example  
 In the following example, assume that a table is bound to a dataset that includes a field for the product identifier ProductID. A separate dataset called "Product" contains the corresponding product identifier ID and the product name Name.  
  
 In the following expression, `Lookup` compares the value of ProductID to ID in each row of the dataset called "Product" and, when a match is found, returns the value of the Name field for that row.  
  
```  
=Lookup(Fields!ProductID.Value, Fields!ID.Value, Fields!Name.Value, "Product")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
