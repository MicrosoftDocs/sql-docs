---
title: "Exclude a Column from a Mining Model | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "excluding mining model columns"
  - "mining models [Analysis Services], columns"
  - "columns [data mining], excluding"
ms.assetid: 404fe5fe-3ba2-4b9b-8780-0d02343d467f
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "jhubbard"
---
# Exclude a Column from a Mining Model
  When you create a new mining model, you may not want to use all the columns that exist in the mining structure on which the model is based. For example, you might have added a customer name column for drillthrough, but don’t want to use it for modeling. Or, you might decide to create multiple copies of a column with different discretizations, and use only one of the copies in each model, and ignore the rest. You could also selectively add input columns in several different models to see how the added variable affects the output column.  
  
 You do not need to create a new mining structure for each combination of columns; instead, you can simply flag a column as not being used in a particular model.  
  
### To exclude a column from a mining model  
  
1.  In the **Mining Models** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select the cell that corresponds to the column you want to exclude, under the appropriate mining model.  
  
2.  Select **Ignore** from the drop-down list box.  
  
## See Also  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)  
  
  