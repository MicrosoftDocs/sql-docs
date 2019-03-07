---
title: "View the Formula for a Time Series Model (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], how-to topics"
  - "ARTXP"
  - "time series algorithms [Analysis Services]"
  - "ARIMA"
  - "time series [Analysis Services]"
  - "Time Series Viewer [Analysis Services]"
ms.assetid: 825ef719-2f44-4979-be01-5a81f54e1a53
author: minewiskan
ms.author: owend
manager: craigg
---
# View the Formula for a Time Series Model (Data Mining)
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series viewer inData Mining Designer provides the easiest way to view the details of the regression equation used in a time series model.  
  
 You can extract the regression formula for a time series model by querying the model content. However, to view the complete ARTXP or ARIMA formula, we recommend that you use the **Mining Legend** of the [Microsoft Time Series Viewer](browse-a-model-using-the-microsoft-time-series-viewer.md), which presents all the constants in a readable format.  
  
 If you create a mixed model, the ARIMA and ARTXP analyses are created in separate trees, joined at the root node representing the model. The structures of the ARIMA and ARTXP trees are very different. For example, the ARTXP tree is in fact a tree structure, like a decision tree, whereas the ARIMA tree represents a series of moving averages. Therefore, although the two representations are presented in one model for convenience, they should be treated as two independent models. The equations are also completely different and cannot be combined or compared.  
  
 You can also view time series models by using the [Microsoft Generic Content Tree Viewer](../microsoft-generic-content-tree-viewer-data-mining.md). For more information about the content of a time series model, see [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-time-series-models-analysis-services-data-mining.md).  
  
### To view the ARTXP regression formula for a time series model  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the time series model that you want to view, and click **Browse**.  
  
     -- or --  
  
     In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select the time series model, and then click the **Mining Model Viewer** tab.  
  
2.  Click the **Model** tab.  
  
3.  If the model contains multiple trees, select a single tree from the **Tree** drop-down list.  
  
    > [!NOTE]  
    >  A model will always have multiple trees if you have more than one data series. However, you will not see as many trees in the **Time Series viewer** as you will see in the [Microsoft Generic Content Tree Viewer](../microsoft-generic-content-tree-viewer-data-mining.md). That is because the Time Series viewer combines the ARIMA and ARTXP information for each data series into a single representation.  
  
4.  Click any leaf node in the tree.  
  
     Nodes that are labeled as **Data Series** are always leaf nodes and can contain an equation. If an **(All)** node has no child nodes, it can also contain an equation.  
  
5.  If the **Mining Legend** is not available, right-click the node, and select **Show Legend**.  
  
     The ARTXP formula is displayed in the first half of the **Mining Legend**, as the **Tree node equation**.  
  
### To view the ARIMA formula for a time series model  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the time series model that you want to view, and click **Browse**.  
  
     -- or --  
  
     In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select the time series model, and then click the **Mining Model Viewer** tab.  
  
2.  Click the **Model** tab.  
  
3.  If the model contains multiple trees, select a single tree from the **Tree** drop-down list.  
  
    > [!NOTE]  
    >  The model will always have multiple trees if you include more than one data series.  
  
4.  Click any node in the tree.  
  
     The ARIMA formula is displayed in the second half of the **Mining Legend**, as the **ARIMA equation**.  
  
5.  If the **Mining Legend** is not available, right-click the node, and select **Show Legend**.  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Browse a Model Using the Microsoft Time Series Viewer](browse-a-model-using-the-microsoft-time-series-viewer.md)   
 [Time Series Model Query Examples](time-series-model-query-examples.md)  
  
  
