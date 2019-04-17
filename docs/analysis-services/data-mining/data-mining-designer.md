---
title: "Data Mining Designer | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Designer
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Data Mining Designer is the primary environment in which you work with mining models in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. You can access the designer either by selecting an existing mining structure, or by using the Data Mining Wizard to create a new mining structure and mining model. You can use Data Mining Designer to perform the following tasks:  
  
-   Modify the mining structure and the mining model that were initially created by the Data Mining Wizard.  
  
-   Create new models based on an existing mining structure.  
  
-   Train and browse mining models.  
  
-   Compare models by using accuracy charts.  
  
-   Create prediction queries based on mining models.  
  
## Mining Structure Tab  
 Use the **Mining Structure** tab to add columns and modify the properties of an existing mining structure. The following tasks and topics provide more information about working with mining structures:  
  
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)  
  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
## Mining Models Tab  
 Use the **Mining Models** tab to manage existing mining models and to create new models. Mining models are always based on an existing mining structure .  
  
 In the **Mining Models** tab, you can change the algorithm type, add or remove columns that are associated with the model structure, adjust algorithm-specific column properties, specify the mining model column usage, and adjust algorithm parameters that are associated with the mining model. You can also process the mining structure together with selected models or with all the associated models.  
  
 See the following topics for more information about working with mining models:  
  
 [Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-models-analysis-services-data-mining.md)  
  
 [Mining Model Tasks and How-tos](../../analysis-services/data-mining/mining-model-tasks-and-how-tos.md)  
  
## Mining Model Viewer Tab  
 Use the **Mining Model Viewer** tab to visually explore your mining models. Each mining model is associated with a custom viewer that displays content that is specific to that model. You can also view mining model content by using the content viewer.  
  
 See the following topics for more information about exploring mining models with the data mining viewers:  
  
 [Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md)  
  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)  
  
## Mining Accuracy Chart Tab  
 Use the **Mining Accuracy Chart** tab to test the predictive accuracy of a single mining model, or to compare the effectiveness of multiple mining models contained within a mining structure. The tab contains tools for filtering the data, selecting mining models, and displaying the results in a lift chart, profit chart, or classification matrix.  
  
 See the following topics for more information about testing and validating mining models:  
  
 [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)  
  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)  
  
## Mining Model Prediction Tab  
 The **Mining Model Prediction** tab includes Prediction Query Builder, which you can use to create a Data Mining Extensions (DMX) prediction query. The tab contains tools for specifying mining models and input tables, mapping the columns in the mining model to columns in the input table, adding functions to a query, and specifying criteria for each column.  
  
 After you design a query, you can use different views in the tab to display the results of the query and to modify the query manually. You can also save the results of the query to a table in a database.  
  
 See the following topics for more information about creating data mining queries:  
  
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)  
  
 [Data Mining Query Tasks and How-tos](../../analysis-services/data-mining/data-mining-query-tasks-and-how-tos.md)  
  
## See Also  
 [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md)  
  
  
