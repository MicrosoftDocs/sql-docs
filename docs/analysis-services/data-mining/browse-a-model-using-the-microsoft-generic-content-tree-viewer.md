---
title: "Browse a Model Using the Microsoft Generic Content Tree Viewer | Microsoft Docs"
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
# Browse a Model Using the Microsoft Generic Content Tree Viewer
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Mining Model Content Viewer provides detailed information about the patterns found by the mining algorithm, and also provides access to various statistics generated during the analysis process. The amount and type of information depends on the algorithm that was used, but can include the following categories:  
  
-   Segments of data, and their characteristics.  
  
-   Descriptive statistics about each group or about the whole set of data.  
  
-   The number of branches or child nodes in a tree.  
  
-   Calculations, such as variance and mean, for a cluster or a whole set of data.  
  
 Viewing this information can help you better understand the results of your analysis. You can also identify ways to fine-tune, and then retrain, your model. Or, you might decide to retrain by using a different algorithm.  
  
## Viewing Mining Model Content  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Viewer displays the columns, rules, properties, attributes, nodes, and other content from the *content schema rowset* of the mining model. The content schema rowset is a generic framework for presenting detailed information about the content of a data mining model.  
  
 This detailed information is contained in an HTML table that represents the patterns, clusters, or trees in the model as nodes. You can click on each node and expand it to see more detail, such as the formulas or the count of distinct values for a numeric attribute. You can also explore the child-parent relationships among the nodes.  
  
 For more information about the general meaning of the terms used in the mining model content, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md). The topic also contains links to information about mining model content for specific types of models. Each type of mining model contains information that is highly specific to the algorithm and the patterns found in the data, so we recommend that you consult the technical reference topic for each model type in order to fully understand each model type.  
  
## Querying Mining Model Content  
 The same information that is provided by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree Viewer is also available by querying the mining model. You can create queries against mining model content by using Data Mining Extensions (DMX) statements. For example, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can run a content query by executing the following DMX statement:  
  
```  
SELECT * FROM [<mining model name>].CONTENT  
```  
  
 For more information, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
## See Also  
 [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](http://msdn.microsoft.com/library/751b4393-f6fd-48c1-bcef-bdca589ce34c)   
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)  
  
  
