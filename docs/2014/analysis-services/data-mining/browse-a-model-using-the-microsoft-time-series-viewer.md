---
title: "Browse a Model Using the Microsoft Time Series Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], continuous columns"
  - "mining model content, viewing"
  - "Microsoft Time Series Viewer"
  - "charts [Analysis Services]"
  - "Time Series Viewer [Analysis Services]"
  - "continuous columns"
  - "regression algorithms [Analysis Services]"
ms.assetid: a77c16cd-1cd0-4fc5-afeb-d1dab30d1e25
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Time Series Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithm is a regression algorithm that creates data mining models for prediction of continuous columns, such as product sales, in a forecasting scenario. These time series models can include information based on different algorithms:  
  
-   The ARTxp algorithm, which is optimized for short-term prediction.  
  
-   The ARIMA algorithm, which is optimized for long-term prediction.  
  
-   A blend of the ARTxp and ARIMA algorithms.  
  
 For more information about these algorithms, see [Microsoft Time Series Algorithm](microsoft-time-series-algorithm.md) and [Microsoft Time Series Algorithm Technical Reference](microsoft-time-series-algorithm-technical-reference.md).  
  
> [!NOTE]  
>  To view detailed information about the equations used in the model and the patterns that were discovered, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series Viewer provides the following tabs:  
  
-   [Model](#BKMK_Tree)  
  
-   [Charts](#BKMK_Charts)  
  
 **Note** The information shown for the model content and in the Mining Legend depends on the algorithm that the model uses. However, the **Model** and **Charts** tabs are the same regardless of the algorithm mix.  
  
###  <a name="BKMK_Tree"></a> Model  
 When you build a time series model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] presents the completed model as a tree. If your data contains multiple case series, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] builds a separate tree for each series. For example, you are predicting sales for the Pacific, North America, and Europe regions. The predictions for each of these regions are case series. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] builds a separate tree for each of these series. To view a particular series, select the series from the **Tree** list.  
  
 For each tree, the time series model contains an **All** node, and then splits into a series of nodes that represent periodic structures discovered by the algorithm. You can click each node to display statistics such as the number of cases and the equation.  
  
 If you created the model using ARTxp only, the **Mining Legend** for the root node contains only the total number of cases. For each non-root node, the **Mining Legend** contains more detailed information about the tree split: for example, it might show the equation for the node and the number of cases. The *rule* in the legend contains information that identifies the series, and the time slice to which the rule applies. For example, the legend text `M200 Europe Amount -2` indicates that the node represents the model for the M200 Europe series, at a period two time slices ago.  
  
 If you created the model using ARIMA only, the **Model** tab contains a single node with the caption, **All**. The **Mining Legend** for the root node contains the ARIMA equation.  
  
 If you created a mixed model, the root node contains the number of cases and the ARIMA equation only. After the root node, the tree splits into separate nodes for each periodic structure. For each non-root node, the Mining Legend contains both the ARTxp and ARIMA algorithms, the equation for the node, and the number of cases in the node. The ARTxp equation is listed first, and is labeled as the tree node equation. This is followed by the ARIMA equation. For more information about how to interpret this information, see [Microsoft Time Series Algorithm Technical Reference](microsoft-time-series-algorithm-technical-reference.md).  
  
 In general, the decision tree graph shows the most important split, the **All** node, at the left of the viewer. In decision trees, the split after the **All** node is the most important because it contains the condition that most strongly separates the cases in the training data. In a time series model, the main branching indicates the most likely seasonal cycle. Splits after the **All** node appear to the right of the branch.  
  
 You can expand or collapse individual nodes in the tree to show or hide the splits that occur after each node. You can also use the options on the **Decision Tree** tab to affect how the tree is displayed. Use the **Show Level** slider to adjust the number of levels that are shown in the tree. Use **Default Expansion** to set the default number of levels that are displayed for all trees in the model.  
  
 The shading of the background color for each node signifies the number of cases that are in the node. To find the exact number of cases in a node, pause the pointer over the node to view an InfoTip for the node.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Charts"></a> Charts  
 The **Charts** tab displays a graph that shows the behavior of the predicted attribute over time, together with five predicted future values. The vertical axis of the chart represents the value of the series, and the horizontal axis represents time.  
  
> [!NOTE]  
>  The time slices used on the time axis depend on the units used in your data: they could represent days, months, or even seconds.  
  
 Use the **Abs** button to toggle between absolute and relative curves. If your chart contains multiple models, the scale of the data for each model might be very different. If you use an absolute curve, one model might appear as a flat line, whereas another model shows significant changes. This occurs because the scale of one model is greater than the scale of the other model. By switching to a relative curve, you change the scale to show the percentage of change instead of absolute values. This makes it easier to compare models that are based on different scales.  
  
 If the mining model contains multiple time series, you can select one or multiple series to display in the chart. Just click the list at the right of the viewer and select the series you want from the list. If the chart becomes too complex, you can filter the series that are displayed by selecting or clearing the series check boxes in the legend.  
  
 The chart displays both historical and future data. Future data appears shaded, to differentiate it from historical data. The data values appear as solid lines for historical data and as dotted lines for predictions. You can change the color of the lines that are used for each series by setting properties in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Change the Colors Used in the Data Mining Viewer](change-the-colors-used-in-the-data-mining-viewer.md).  
  
 You can adjust the range of time that is displayed by using the zoom options. You can also view a specific time range by clicking the chart, dragging a time selection across the chart, and then clicking again to zoom in on the selected range.  
  
 You can select how many future time **steps** that you want to see in the model by using **Prediction Steps**. If you select the **Show Deviations** check box, the viewer provides error bars so that you can see how accurate the predicted value is.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Microsoft Time Series Algorithm](microsoft-time-series-algorithm.md)   
 [Time Series Model Query Examples](time-series-model-query-examples.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
