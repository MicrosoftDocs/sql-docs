---
title: "Creating Recursive Hierarchy Groups (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 06eccab6-4089-46e8-a84f-5bf3bbe0c23b
author: markingmyname
ms.author: maghan
---
# Creating Recursive Hierarchy Groups (Report Builder and SSRS)
To display recursive data in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated reports (where the relationship between parent and child is represented by fields in the dataset), set the data region group expression based on the child field and set the Parent property based on the parent field.  
  
 Displaying hierarchical data is a common use for recursive hierarchy groups, for example, employees in an organizational chart. The dataset includes a list of employees and the managers, where the manager names also appear in the list of employees.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Creating Recursive Hierarchies  
 To build a recursive hierarchy in a tablix data region, you must set the group expression to the field that specifies the child data and the Parent property of the group to the field that specifies the parent data. For example, for a dataset that includes fields for employee ID and manager ID where employees report to managers, set the group expression to employee ID and the Parent property to manager ID.  
  
 A group that is defined as a recursive hierarchy (that is, a group that uses the Parent property) can have only one group expression. You can use the **Level** function in text box padding to indent employee names based on their level in the hierarchy.  
  
 For more information, see [Add or Delete a Group in a Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-or-delete-a-group-in-a-data-region-report-builder-and-ssrs.md) and  [Create a Recursive Hierarchy Group &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-a-recursive-hierarchy-group-report-builder-and-ssrs.md).  
  
### Aggregate Functions that support Recursion  
 You can use Reporting Services aggregate functions that accept the parameter *Recursive* to calculate summary data for a recursive hierarchy. The following functions accept **Recursive** as a parameter: [Sum](../../reporting-services/report-design/report-builder-functions-sum-function.md), [Avg](../../reporting-services/report-design/report-builder-functions-avg-function.md), [Count](../../reporting-services/report-design/report-builder-functions-count-function.md), [CountDistinct](../../reporting-services/report-design/report-builder-functions-countdistinct-function.md), [CountRows](../../reporting-services/report-design/report-builder-functions-countrows-function.md), [Max](../../reporting-services/report-design/report-builder-functions-max-function.md), [Min](../../reporting-services/report-design/report-builder-functions-min-function.md), [StDev](../../reporting-services/report-design/report-builder-functions-stdev-function.md), [StDevP](../../reporting-services/report-design/report-builder-functions-stdevp-function.md), [Sum](../../reporting-services/report-design/report-builder-functions-sum-function.md), [Var](../../reporting-services/report-design/report-builder-functions-var-function.md), and [VarP](../../reporting-services/report-design/report-builder-functions-varp-function.md). For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md).  
  
## See Also  
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)    
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
