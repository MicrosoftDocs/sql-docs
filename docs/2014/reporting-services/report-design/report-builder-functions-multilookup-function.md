---
title: "Multilookup Function (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 1fec079e-33b3-4e4d-92b3-6b4d06a49a77
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Multilookup Function (Report Builder and SSRS)
  Returns the set of first-match values for the specified set of names from a dataset that contains name/value pairs.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
Multilookup(source_expression, destination_expression, result_expression, dataset)  
```  
  
#### Parameters  
 *source_expression*  
 (`VariantArray`) An expression that is evaluated in the current scope and that specifies the set of names or keys to look up. For example, for a multivalue parameter, `=Parameters!IDs.value`.  
  
 *destination_expression*  
 (`Variant`) An expression that is evaluated for each row in a dataset and that specifies the name or key to match on. For example, `=Fields!ID.Value`.  
  
 *result_expression*  
 (`Variant`) An expression that is evaluated for the row in the dataset where *source_expression* = *destination_expression*, and that specifies the value to retrieve. For example, `=Fields!Name.Value`.  
  
 *dataset*  
 A constant that specifies the name of a dataset in the report. For example, "Colors".  
  
## Return  
 Returns a `VariantArray`, or `Nothing` if there is no match.  
  
## Remarks  
 Use `Multilookup` to retrieve a set of values from a dataset for name-value pairs where each pair has a 1-to-1 relationship. `MultiLookup` is the equivalent of calling `Lookup` for a set of names or keys. For example, for a multivalue parameter that is based on primary key identifiers, you can use `Multilookup` in an expression in a text box in a table to retrieve associated values from a dataset that is not bound to the parameter or to the table.  
  
 `Multilookup` does the following:  
  
-   Evaluates the source expression in the current scope and generates an array of variant objects.  
  
-   For each object in the array, calls [Lookup Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookup-function.md) and adds the result to the return array.  
  
-   Returns the set of results.  
  
 To retrieve a single value from a dataset with name-value pairs for a specified name where there is a 1-to-1 relationship, use [Lookup Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookup-function.md). To retrieve multiple values from a dataset with name-value pairs for a name where there is a 1-to-many relationship, use [LookupSet Function &#40;Report Builder and SSRS&#41;](report-builder-functions-lookupset-function.md).  
  
 The following restrictions apply:  
  
-   `Multilookup` is evaluated after all filter expressions are applied  
  
-   Only one level of look-up is supported. A source, destination, or result expression cannot include a reference to a lookup function.  
  
-   Source and destination expressions must evaluate to the same data type.  
  
-   Source, destination, and result expressions cannot include references to report or group variables.  
  
-   `Multilookup` cannot be used as an expression for the following report items:  
  
    -   Dynamic connection strings for a data source.  
  
    -   Calculated fields in a dataset.  
  
    -   Query parameters in a dataset.  
  
    -   Filters in a dataset.  
  
    -   Report parameters.  
  
    -   The Report.Language property.  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
## Example  
 Assume a dataset called "Category" contains the field CategoryList, which is a field that contains a comma-separated list of category identifers, for example, "2, 4, 2, 1".  
  
 The dataset CategoryNames contains the category identifier and category name, as shown in the following table.  
  
|ID|Name|  
|--------|----------|  
|1|Accessories|  
|2|Bikes|  
|3|Clothing|  
|4|Components|  
  
 To look up the names that correspond to the list of  identifiers, use `Multilookup`. You must first split the list into a string array, call `Multilookup` to retrieve the category names, and concatenate the results into a string.  
  
 The following expression, when placed in a text box in a data region bound to the Category dataset, displays "Bikes, Components, Bikes, Accessories":  
  
```  
=Join(MultiLookup(Split(Fields!CategoryList.Value,","),  
   Fields!CategoryID.Value,Fields!CategoryName.Value,"Category")),  
   ", ")  
```  
  
## Example  
 Assume a dataset ProductColors contains a color identifier field ColorID and a color value field Color, as shown in the following table.  
  
|ColorID|Color|  
|-------------|-----------|  
|1|Red|  
|2|Blue|  
|3|Green|  
  
 Assume the multivalue parameter *MyColors* is not bound to a dataset for its available values. The default values for the parameter are set to 2 and 3. The following expression, when placed in a text box in a table, concatenates the multiple selected values for the parameter into a comma-separated list and displays "Blue, Green".  
  
```  
=Join(MultiLookup(Parameters!MyColors.Value,Fields!ColorID.Value,Fields!Color.Value,"ProductColors"),", ")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
