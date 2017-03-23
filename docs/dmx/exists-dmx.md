---
title: "Exists (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "Exists"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "Exists function"
ms.assetid: 3b54dd93-f0a8-4f9a-96ae-a38bf977dda1
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Exists (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns **true** if the specified sub-query returns at least one row.  
  
## Syntax  
  
```  
  
EXISTS(<subquery>)  
```  
  
## Arguments  
 *subquery*  
 A SELECT statement of the form SELECT * FROM \<column name> [WHERE \<predicate list>].  
  
## Result Type  
 Returns **true** if the result set returned by the subquery contains at least one row; otherwise, returns **false**.  
  
## Remarks  
 You can use the NOT keyword before EXISTS: for example, `WHERE NOT EXISTS (<subquery>)`.  
  
 The list of columns that you add to the sub-query argument of EXISTS is irrelevant; the function only checks for the existence of a row that meets the condition.  
  
## Examples  
 You can use EXISTS and NOT EXISTS to check for conditions in a nested table. This is useful when creating a filter that controls the data used to train or test a data mining model. For more information, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
 The following example is based on the `[Association]` mining structure and mining model that you created in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). The query returns only those cases where the customer purchased at least one patch kit.  
  
```  
SELECT * FROM [Association].CASES  
WHERE EXISTS  
(  
SELECT * FROM [v Assoc Seq Line Numbers]  
WHERE [[Model] = 'Patch kit'  
)  
```  
  
 Another way to view the same data that is returned by this query is to open the model in the Association viewer, right-click the itemset **Patch kit = Existing**, select the **Drill Through** option, and then select **Model Cases Only**.  
  
## See Also  
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [Model Filter Syntax and Examples &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/model-filter-syntax-and-examples-analysis-services-data-mining.md)  
  
  
