---
title: "Browse a Model Using the Microsoft Sequence Cluster Viewer | Microsoft Docs"
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
# Browse a Model Using the Microsoft Sequence Cluster Viewer
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm is a sequence analysis algorithm for use in exploring data that contains events that can be linked by following paths, or *sequences*. For more information about this algorithm, see [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md).  
  
> [!NOTE]  
>  To view detailed information about the equations used in the model and the patterns that were discovered, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](http://msdn.microsoft.com/library/751b4393-f6fd-48c1-bcef-bdca589ce34c).  
  
> [!NOTE]  
>  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer provides functionality and options that are similar to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer. For more information, see [Browse a Model Using the Microsoft Cluster Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer provides the following tabs for use in exploring sequence clustering mining models:  
  
-   [Cluster Diagram](#BKMK_Diagram)  
  
-   [Cluster Profiles](#BKMK_Profile)  
  
-   [Cluster Characteristics](#BKMK_Characteristics)  
  
-   [Cluster Discrimination](#BKMK_Discrimination)  
  
-   [Cluster Transitions](#BKMK_Transitions)  
  
###  <a name="BKMK_Diagram"></a> Cluster Diagram  
 The **Cluster Diagram** tab of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Cluster Viewer displays all the clusters that are in a mining model. The shading of the line that connects one cluster to another represents the strength of the similarity of the clusters. If the shading is light or nonexistent, the clusters are not very similar. As the line becomes darker, the similarity of the links becomes stronger. You can adjust how many lines the viewer shows by adjusting the slider to the right of the clusters. Lowering the slider shows only the strongest links.  
  
 By default, the shade represents the population of the cluster. By using the **Shading****Variable** and **State** options, you can select which attribute and state pair the shading represents. The darker the shading, the greater the attribute distribution is for a specific state. The distribution decreases as the shading gets lighter.  
  
 To rename a cluster, right-click its node and select **Rename Cluster**. The new name is persisted to the server.  
  
 To copy the visible section of the diagram to the Clipboard, click **Copy Graph View**. To copy the complete diagram, click **Copy Entire Graph**. You can also zoom in and out by using **Zoom In** and **Zoom Out**, or you can fit the diagram to the screen by using **Scale Diagram to Fit in Window**.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Profile"></a> Cluster Profiles  
 The **Cluster Profile** tab provides an overall view of the clusters that the algorithm in your model creates. Each column that follows the **Population** column in the grid represents a cluster that the model discovered. The \<attribute>.samples row represents different sequences of data that exist in the cluster, and the \<attribute> row describes all the items that the cluster contains and their overall distribution.  
  
 The **Histogram bars** option controls the number of bars that are visible in the histogram. If more bars exist than you choose to display, the bars of highest importance are retained, and the remaining bars are grouped together into a gray bucket.  
  
 You can change the default names of the clusters, to make the names more descriptive. Rename a cluster by right-clicking its column heading and selecting **Rename cluster**. You can hide clusters by selecting **Hide column**, and you can also drag columns to reorder them in the viewer.  
  
 To open a window that provides a larger, more detailed view of the clusters, double-click either a cell in the **States** column or a histogram in the viewer.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Characteristics"></a> Cluster Characteristics  
 To use the **Cluster Characteristics** tab, select a cluster from the **Cluster** list. After you select a cluster, you can examine the characteristics that make up that specific cluster. The attributes that the cluster contains are listed in the **Variables** columns, and the state of the listed attribute is listed in the **Values** column. Attribute states are listed in order of importance, described by the probability that they will appear in the cluster. The probability is shown in the **Probability** column.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Discrimination"></a> Cluster Discrimination  
 You can use the **Cluster Discrimination** tab to compare attributes between two clusters, to determine how items in a sequence favor one cluster over another. Use the **Cluster 1** and **Cluster 2** lists to select the clusters to compare. The viewer determines the most important differences between the clusters, and displays the attribute states that are associated with the differences, in order of importance. A bar to the right of the attribute shows which cluster the state favors, and the size of the bar shows how strongly the state favors the cluster.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Transitions"></a> Cluster Transitions  
 By selecting a cluster on the **Cluster Transitions** tab, you can browse the transitions between sequence states in the selected cluster. Each node in the viewer represents a state of the sequence column. An arrow represents a transition between two states and the probability that is associated with the transition. If a transition returns back to the originating node, an arrow can point back to the originating node.  
  
 An arrow that originates from a dot represents the probability that the node is the start of a sequence. An ending edge that leads to a null represents the probability that the node is the end of the sequence.  
  
 You can filter the edge of the nodes by using the slider at the left of the tab.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)   
 [Mining Model Viewer Tasks and How-tos](../../analysis-services/data-mining/mining-model-viewer-tasks-and-how-tos.md)   
 [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)   
 [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md)   
 [Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md)   
 [Browse a Model Using the Microsoft Cluster Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md)  
  
  
