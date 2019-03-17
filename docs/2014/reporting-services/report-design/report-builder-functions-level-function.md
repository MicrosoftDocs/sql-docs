---
title: "Level Function (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 41235402-bb9e-4cb7-b91e-431e77db19cf
author: markingmyname
ms.author: maghan
manager: kfile
---
# Level Function (Report Builder and SSRS)
  Returns the current level of depth in a recursive hierarchy.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Syntax  
  
```  
  
Level(scope)  
```  
  
#### Parameters  
 *scope*  
 (`String`) (Optional). The name of a dataset, group, or data region that contains the report items to which to apply the aggregate function. If *scope* is not specified, the current scope is used.  
  
## Return Type  
 Returns an `Integer`. If *scope* specifies a dataset or data region, or specifies a nonrecursive grouping (that is, a grouping with no `Parent` element), `Level` returns 0. If *scope* is omitted, it returns the level of the current scope.  
  
## Remarks  
 The value returned by the `Level` function is zero based; that is, the first level in a hierarchy is 0.  
  
 The `Level` function can be used to provide indentation in a recursive hierarchy, such as an employee list.  
  
 For more information about recursive hierarchies, see [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](creating-recursive-hierarchy-groups-report-builder-and-ssrs.md).  
  
## Example  
 The following code example provides the level of row in the Employees group:  
  
```  
=Level("Employees")  
```  
  
## See Also  
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Data Types in Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md)   
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](expression-scope-for-totals-aggregates-and-built-in-collections.md)  
  
  
