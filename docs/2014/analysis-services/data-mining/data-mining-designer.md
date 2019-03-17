---
title: "Data Mining Designer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], structure"
  - "mining structures [Analysis Services], modifying"
  - "data mining editor [Analysis Services]"
  - "Data Mining Designer"
  - "data mining [Analysis Services], modifying"
ms.assetid: 2540db5b-2bf3-4b6c-87c8-79c48d71acce
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Designer
  Data Mining Designer is the primary environment in which you work with mining models in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. You can access the designer either by selecting an existing mining structure, or by using the Data Mining Wizard to create a new mining structure and mining model. You can use Data Mining Designer to perform the following tasks:  
  
-   Modify the mining structure and the mining model that were initially created by the Data Mining Wizard.  
  
-   Create new models based on an existing mining structure.  
  
-   Train and browse mining models.  
  
-   Compare models by using accuracy charts.  
  
-   Create prediction queries based on mining models.  
  
## Mining Structure Tab  
 Use the **Mining Structure** tab to add columns and modify the properties of an existing mining structure. The following tasks and topics provide more information about working with mining structures:  
  
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)  
  
 [Mining Structure Tasks and How-tos](mining-structure-tasks-and-how-tos.md)  
  
## Mining Models Tab  
 Use the **Mining Models** tab to manage existing mining models and to create new models. Mining models are always based on an existing mining structure .  
  
 In the **Mining Models** tab, you can change the algorithm type, add or remove columns that are associated with the model structure, adjust algorithm-specific column properties, specify the mining model column usage, and adjust algorithm parameters that are associated with the mining model. You can also process the mining structure together with selected models or with all the associated models.  
  
 See the following topics for more information about working with mining models:  
  
 [Mining Models &#40;Analysis Services - Data Mining&#41;](mining-models-analysis-services-data-mining.md)  
  
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)  
  
## Mining Model Viewer Tab  
 Use the **Mining Model Viewer** tab to visually explore your mining models. Each mining model is associated with a custom viewer that displays content that is specific to that model. You can also view mining model content by using the content viewer.  
  
 See the following topics for more information about exploring mining models with the data mining viewers:  
  
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)  
  
## Mining Accuracy Chart Tab  
 Use the **Mining Accuracy Chart** tab to test the predictive accuracy of a single mining model, or to compare the effectiveness of multiple mining models contained within a mining structure. The tab contains tools for filtering the data, selecting mining models, and displaying the results in a lift chart, profit chart, or classification matrix.  
  
 See the following topics for more information about testing and validating mining models:  
  
 [Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)  
  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](testing-and-validation-tasks-and-how-tos-data-mining.md)  
  
## Mining Model Prediction Tab  
 The **Mining Model Prediction** tab includes Prediction Query Builder, which you can use to create a Data Mining Extensions (DMX) prediction query. The tab contains tools for specifying mining models and input tables, mapping the columns in the mining model to columns in the input table, adding functions to a query, and specifying criteria for each column.  
  
 After you design a query, you can use different views in the tab to display the results of the query and to modify the query manually. You can also save the results of the query to a table in a database.  
  
 See the following topics for more information about creating data mining queries:  
  
 [Data Mining Queries](data-mining-queries.md)  
  
 [Data Mining Query Tasks and How-tos](data-mining-query-tasks-and-how-tos.md)  
  
## See Also  
 [Data Mining Solutions](data-mining-solutions.md)  
  
  
