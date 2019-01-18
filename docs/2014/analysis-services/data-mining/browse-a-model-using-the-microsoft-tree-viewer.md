---
title: "Browse a Model Using the Microsoft Tree Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Tree Viewer [Analysis Services]"
  - "predictions [Analysis Services], discrete attributes"
  - "mining model content, viewing"
  - "predictions [Analysis Services], continuous attributes"
  - "mining legend [Analysis Services]"
  - "discrete attributes [Analysis Services]"
  - "Microsoft Decision Trees algorithm [Analysis Services]"
  - "decision tree algorithms [Analysis Services]"
  - "Microsoft Tree Viewer"
  - "decision trees [Analysis Services]"
  - "dependencies [Analysis Services]"
  - "continuous attributes"
ms.assetid: 0c96d518-ed20-40b7-8d62-b26ad6244287
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Tree Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tree Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays decision trees that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm is a hybrid decision tree algorithm that supports both classification and regression. Therefore, you can also use this viewer to view models based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm is used for predictive modeling of both discrete and continuous attributes. For more information about this algorithm, see [Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md).  
  
> [!NOTE]  
>  To view detailed information about the equations used in the model and the patterns that were discovered, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
##  <a name="BKMK_TabsPanes"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tree Viewer includes the following tabs and panes:  
  
-   [Decision Tree](#BKMK_DecisionTree)  
  
-   [Dependency Network](#BKMK_DependencyNetwork)  
  
-   [Mining Legend](#BKMK_MiningLegend)  
  
###  <a name="BKMK_DecisionTree"></a> Decision Tree  
 When you build a decision tree model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] builds a separate tree for each predictable attribute. You can view an individual tree by selecting it from the **Tree** list on the **Decision Tree** tab of the viewer.  
  
 A decision tree is composed of a series of splits, with the most important split, as determined by the algorithm, at the left of the viewer in the **All** node. Additional splits occur to the right. The split in the **All** node is most important because it contains the strongest split-causing conditional in the dataset, and therefore it caused the first split.  
  
 You can expand or collapse individual nodes in the tree to show or hide the splits that occur after each node. You can also use the options on the **Decision Tree** tab to affect how the tree is displayed. Use the **Show Level** slider to adjust the number of levels that are shown in the tree. Use **Default Expansion** to set the default number of levels that are displayed for all trees in the model.  
  
#### Predicting Discrete Attributes  
 When a tree is built with a discrete predictable attribute, the viewer displays the following on each node in the tree:  
  
-   The condition that caused the split.  
  
-   A histogram that represents the distribution of the states of the predictable attribute, ordered by popularity.  
  
 You can use the **Histogram** option to change the number of states that appear in the histograms in the tree. This is useful if the predictable attribute has many states. The states appear in a histogram in order of popularity from left to right; if the number of states that you choose to display is fewer than the total number of states in the attribute, the least popular states are displayed collectively in gray. To see the exact count for each state for a node, pause the pointer over the node to view an InfoTip, or select the node to view its details in the **Mining Legend**.  
  
 The background color of each node represents the concentration of cases of the particular attribute state that you select by using the **Background** option. You can use this option to highlight nodes that contain a particular target in which you are interested.  
  
#### Predicting Continuous Attributes  
 When a tree is built with a continuous predictable attribute, the viewer displays a diamond chart, instead of a histogram, for each node in the tree. The diamond chart has a line that represents the range of the attribute. The diamond is located at the mean for the node, and the width of the diamond represents the variance of the attribute at that node. A thinner diamond indicates that the node can create a more accurate prediction. The viewer also displays the regression equation, which is used to determine the split in the node.  
  
#### Additional Decision Tree Display Options  
 When drill through is enabled for a decision tree model, you can access the training cases that support a node by right-clicking the node in the tree and selecting **Drill Through**. You can enable drill through within the Data Mining Wizard, or by adjusting the drill through property on the mining model in the **Mining Models** tab.  
  
 You can use the zoom options on the **Decision Tree** tab to zoom in or out of a tree, or use **Size to Fit** to fit the whole model in the viewer screen. If a tree is too large to be sized to fit the screen, you can use the **Navigation**option to navigate through the tree. Clicking **Navigation** opens a separate navigation window that you can use to select sections of the model to display.  
  
 You can also copy the tree view image to the Clipboard, so that you can paste it into documents or into image manipulation software. Use **Copy Graph View** to copy only the section of the tree that is visible in the viewer, or use **Copy Entire Graph** to copy all the expanded nodes in the tree.  
  
 [Back to Top](#BKMK_TabsPanes)  
  
###  <a name="BKMK_DependencyNetwork"></a> Dependency Network  
 The **Dependency Network** displays the dependencies between the input attributes and the predictable attributes in the model. The slider at the left of the viewer acts as a filter that is tied to the strengths of the dependencies. If you lower the slider, only the strongest links are shown in the viewer.  
  
 When you select a node, the viewer highlights the dependencies that are specific to the node. For example, if you choose a predictable node, the viewer also highlights each node that helps predict the predictable node.  
  
 If the viewer contains numerous nodes, you can search for specific nodes by using the **Find Node** button. Clicking **Find Node** opens the **Find Node** dialog box, in which you can use a filter to search for and select specific nodes.  
  
 The legend at the bottom of the viewer links color codes to the type of dependency in the graph. For example, when you select a predictable node, the predictable node is shaded turquoise, and the nodes that predict the selected node are shaded orange.  
  
 [Back to Top](#BKMK_TabsPanes)  
  
###  <a name="BKMK_MiningLegend"></a> Mining Legend  
 The **Mining Legend** displays the following information when you select a node in the decision tree model:  
  
-   The number of cases in the node, broken down by the states of the predictable attribute.  
  
-   The probability of each case of the predictable attribute for the node.  
  
-   A histogram that includes a count for each state of the predictable attribute.  
  
-   The conditions that are required to reach a specific node, also known as the *node path*.  
  
-   For linear regression models, the regression formula.  
  
 You can dock and work with the **Mining Legend** in a similar manner as with Solution Explorer.  
  
 [Back to Top](#BKMK_TabsPanes)  
  
## See Also  
 [Microsoft Decision Trees Algorithm](microsoft-decision-trees-algorithm.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](../mining-model-viewers-data-mining-model-designer.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
