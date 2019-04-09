---
title: "InScope Function (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/08/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: a8cd209a-e5d3-4dce-ab2d-f271f6c54955
author: markingmyname
ms.author: maghan
---
# Report Builder Functions - InScope Function
  Indicates whether the current instance of an item is in the specified scope.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
InScope(scope)  
```  
  
#### Parameters  
 *scope*  
 (**String**) The name of a dataset, data region, or group that specifies a scope.  
  
## Return Type  
 Returns a **Boolean**.  
  
## Remarks  
 The **InScope** function tests the scope of the current instance of a report item for membership in the scope specified by the *scope*parameter.  
  
 *Scope* cannot be an expression.  
  
 A typical use for the **InScope** function is in data regions that have dynamic scoping. For example, **InScope** can be used in a drillthrough link in a data region cells to provide a different report name and different sets of parameters depending on which cell is clicked. An example of this is as follows:  
  
-   The following expression, used as the report name in a drillthrough link, opens the `ProductDetail` report if the clicked cell is in the `Month` group, and the `ProductSummary` report if it is not.  
  
    ```  
    =Iif(InScope("Month"), "ProductDetail", "ProductSummary")  
    ```  
  
-   The following expression, used in the **Omit** property of a drillthrough report parameter, will pass the parameter to the target report only if the clicked cell is in the `Product` group.  
  
    ```  
    =Not(InScope("Product"))  
    ```  
  
 For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md) and [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
## Example  
 The following code example indicates whether the current instance of the item is in the `Product` dataset, data region, or group scope.  
  
```  
=InScope("Product")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/data-types-in-expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
