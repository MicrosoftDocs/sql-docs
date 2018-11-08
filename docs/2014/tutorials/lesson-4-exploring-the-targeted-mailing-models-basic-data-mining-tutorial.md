---
title: "Lesson 4: Exploring the Targeted Mailing Models (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 1e00c5b9-a9f8-4503-99ee-377c9cc02d7f
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 4: Exploring the Targeted Mailing Models (Basic Data Mining Tutorial)
  After the models in your project have been processed, you can explore them to look for interesting trends. Because patterns can be complex and difficult simply by looking at numbers, SQL Server Data Mining provides some visual tools that help you investigate the data and understand the rules and relationships that the algorithms have discovered within the data. You can also use a variety of accuracy tests to validate your dataset or discover which model performs best before you deploy it.  
  
 When you use [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to explore your models, each model you created is listed in the **Mining Model Viewer** tab in Data Mining Designer. You can use the viewers to explore the models. These viewers are also available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
 Each algorithm that you used to build a model in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] returns a different type of result. Therefore, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides custom viewers for each type of machine learning model.  
  
 If you want to get into details, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] also provides an HTML viewer, called the **Generic Content Tree Viewer**, that displays detailed information about the model data and any patterns that were found, in a semi-tabular format. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md).  
  
 In this lesson you will look at the results from your three models. Each model type is based on a different algorithm and provides different insights into the data.  
  
-   The Decision Tree model tells you about factors that influence bike buying.  
  
-   The Clustering model groups your customers by attributes that include their bike buying behavior and other selected attributes.  
  
-   The Naive Bayes model enables you to explore the relationship between different attributes.  
  
 See the following topics to learn more about each of the mining model viewers.  
  
-   [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
-   [Exploring the Clustering Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-clustering-model-basic-data-mining-tutorial.md)  
  
-   [Exploring the Naive Bayes Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-naive-bayes-model-basic-data-mining-tutorial.md)  
  
 All three models can be viewed using the **Generic Content Tree Viewer**, to extract formulas, data values, and so forth.  
  
## First Task in Lesson  
 [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
## Previous Lesson  
 [Lesson 3: Adding and Processing Models](../../2014/tutorials/lesson-3-adding-and-processing-models.md)  
  
## Next Lesson  
 [Lesson 5: Testing Models &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/lesson-5-testing-models-basic-data-mining-tutorial.md)  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../2014/analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Model Viewers](../../2014/analysis-services/data-mining/data-mining-model-viewers.md)  
  
  
