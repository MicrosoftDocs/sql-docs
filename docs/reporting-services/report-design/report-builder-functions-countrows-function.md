---
title: "CountRows function in a paginated report | Microsoft Docs"
description: Learn about the CountRows function, which returns the number of rows in a specified scope, including rows with null values in a paginated report. 
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 5b1c403d-6afd-44c8-b5f6-5ecff2a29a45
author: maggiesMSFT
ms.author: maggies
---
# Report Builder functions - CountRows function in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Returns the number of rows in the specified scope in a paginated report, including rows with null values.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
CountRows(scope, recursive)  
```  
  
#### Parameters  
 *scope*  
 (**String**) The name of a dataset, data region, or group that contains the report items to count.  
  
 *recursive*  
 (**Enumerated Type**) Optional. **Simple** (default) or **RdlRecursive**. Specifies whether to perform the aggregation recursively.  
  
## Return Type  
 Returns an **Integer**.  
  
## Remarks  
 **CountRows** counts all rows in the specified scope, including rows that have null values.  
  
 The value of *scope* cannot be an expression and must refer to the current scope or a containing scope.  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
 For more information about recursive aggregates, see [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/creating-recursive-hierarchy-groups-report-builder-and-ssrs.md).  
  
## Example  
 The following code example shows an expression that calculates the number of rows in a row group named `GroupbyCategory` (based on the expression `[Category]`).  
  
```  
="Number of rows: " & CountRows("GroupbyCategory")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
