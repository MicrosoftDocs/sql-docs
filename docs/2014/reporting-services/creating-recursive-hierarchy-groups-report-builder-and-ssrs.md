---
title: "Creating Recursive Hierarchy Groups (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 06eccab6-4089-46e8-a84f-5bf3bbe0c23b
caps.latest.revision: 7
author: "douglaslM"
ms.author: "douglasl"
manager: "mblythe"
---
# Creating Recursive Hierarchy Groups (Report Builder and SSRS)
  To display recursive data where the relationship between parent and child is represented by fields in the dataset, you can set the data region group expression based on the child field and set the Parent property based on the parent field.  
  
 Displaying hierarchical data is a common use for recursive hierarchy groups, for example, employees in an organizational chart. The dataset includes a list of employees and the managers, where the manager names also appear in the list of employees.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  
## Creating Recursive Hierarchies  
 To build a recursive hierarchy in a tablix data region, you must set the group expression to the field that specifies the child data and the Parent property of the group to the field that specifies the parent data. For example, for a dataset that includes fields for employee ID and manager ID where employees report to managers, set the group expression to employee ID and the Parent property to manager ID.  
  
 A group that is defined as a recursive hierarchy (that is, a group that uses the Parent property) can have only one group expression. You can use the `Level` function in text box padding to indent employee names based on their level in the hierarchy.  
  
 For more information, see [Add or Delete a Group in a Data Region &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/add-or-delete-a-group-in-a-data-region-report-builder-and-ssrs.md) and  [Create a Recursive Hierarchy Group &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/create-a-recursive-hierarchy-group-report-builder-and-ssrs.md).  
  
### Aggregate Functions that support Recursion  
 You can use Reporting Services aggregate functions that accept the parameter *Recursive* to calculate summary data for a recursive hierarchy. The following functions accept `Recursive` as a parameter: [Sum](../../2014/reporting-services/sum-function-report-builder-and-ssrs.md), [Avg](../../2014/reporting-services/avg-function-report-builder-and-ssrs.md), [Count](../../2014/reporting-services/count-function-report-builder-and-ssrs.md), [CountDistinct](../../2014/reporting-services/countdistinct-function-report-builder-and-ssrs.md), [CountRows](../../2014/reporting-services/countrows-function-report-builder-and-ssrs.md), [Max](../../2014/reporting-services/max-function-report-builder-and-ssrs.md), [Min](../../2014/reporting-services/min-function-report-builder-and-ssrs.md), [StDev](../../2014/reporting-services/stdev-function-report-builder-and-ssrs.md), [StDevP](../../2014/reporting-services/stdevp-function-report-builder-and-ssrs.md), [Sum](../../2014/reporting-services/sum-function-report-builder-and-ssrs.md), [Var](../../2014/reporting-services/var-function-report-builder-and-ssrs.md), and [VarP](../../2014/reporting-services/varp-function-report-builder-and-ssrs.md). For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/aggregate-functions-reference-report-builder-and-ssrs.md).  
  
## See Also  
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/tablix-data-region-report-builder-and-ssrs.md)   
 [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/aggregate-functions-reference-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](../../2014/reporting-services/tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/matrices-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/lists-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  