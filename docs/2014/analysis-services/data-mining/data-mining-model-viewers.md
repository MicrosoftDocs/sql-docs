---
title: "Data Mining Model Viewers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying data mining models"
  - "mining models [Analysis Services], viewing"
  - "data mining [Analysis Services], models"
  - "viewing data mining models"
  - "mining model content"
  - "support [data mining]"
  - "exploring data mining models [Analysis Services]"
ms.assetid: 14c8e656-f63c-4e8a-a3af-1d580e823d28
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Model Viewers
  After you train a data mining model in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can explore the model to look for interesting trends. Because the results of mining models are complex and can be difficult to understand in a raw format, visually investigating the data is often the easiest way to understand the rules and relationships that algorithms discover within the data.  
  
 Each algorithm that you use to build a model returns a different type of results. Therefore, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides a separate viewer for each algorithm. When you browse a mining model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer, using the appropriate viewer for the model.  
  
## How to Use the Model Viewers  
 First you select the mining model and then you select a viewer. Each model always has two viewers available: a custom viewer, which can include multiple tabs, and the generic viewer.  
  
 Depending on the type of the model that you selected, you will see very different options for exploring the model. The custom viewers associated with each model type are tailored to the algorithm that you used to create the selected data mining model. Each custom viewer has a variety of tools and dialog boxes for helping you explore the statistics and patterns in the model, view charts, or interactively work with probability thresholds or filter out items by name.  
  
 The following diagram illustrates the difference when you choose a custom viewer and the generic viewer for the same model.  
  
1.  First, you see the custom viewer that is displayed when you select a mining model based on the Microsoft Time Series algorithm.  
  
     This particular custom viewer automatically creates a graph of the time series and provides five predictions.  
  
2.  Next, you see the same model, displayed using the **Microsoft Generic Content Tree Viewer**.  
  
     On the left, the generic viewer displays a list of the nodes in the model. You can click a node to view its contents in the right-hand pane.  
  
 ![Overview of mining model designer](../media/generic-mining-model-tab1.gif "Overview of mining model designer")  
  
## More about the Microsoft Generic Content Tree Viewer  
 Each model can also be viewed using the [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md). This viewer presents the contents of the mining model according to a standard HTML table format. However, the arrangement of the nodes and the content of each node will differ greatly depending on the algorithm used to generate the results.  
  
 Whereas the custom viewers are designed for exploring and understanding the model, the generic viewer is more useful when you already understand the model and want to extract statistics or rules from a specific node. For example, you would use the generic viewer when you want to view detailed information about the patterns and statistics that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] captured during analysis, such as the probability of a node, or a regression formula.  
  
 You can also write *content queries* using DMX to get all of the information that is presented in this viewer. For more information, see [Content Queries &#40;Data Mining&#41;](content-queries-data-mining.md).  
  
## In This Section  
 The following topics describe in more detail each of the viewers, and how to interpret the information in them.  
  
 [Browse a Model Using the Microsoft Tree Viewer](browse-a-model-using-the-microsoft-tree-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tree Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm.  
  
 [Browse a Model Using the Microsoft Cluster Viewer](browse-a-model-using-the-microsoft-cluster-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm.  
  
 [Browse a Model Using the Microsoft Time Series Viewer](browse-a-model-using-the-microsoft-time-series-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm.  
  
 [Browse a Model Using the Microsoft Naive Bayes Viewer](browse-a-model-using-the-microsoft-naive-bayes-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Naive Bayes algorithm.  
  
 [Browse a Model Using the Microsoft Sequence Cluster Viewer](browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm.  
  
 [Browse a Model Using the Microsoft Association Rules Viewer](browse-a-model-using-the-microsoft-association-rules-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association algorithm.  
  
 [Browse a Model Using the Microsoft Neural Network Viewer](browse-a-model-using-the-microsoft-neural-network-viewer.md)  
 Describes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network Viewer. This viewer displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm, including models that use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Logistic Regression algorithm.  
  
 [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md)  
 Describes the detailed information that is available in the generic viewer for all data mining models and provides examples of how to interpret the information for each algorithm.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)   
 [Data Mining Designer](data-mining-designer.md)  
  
  
