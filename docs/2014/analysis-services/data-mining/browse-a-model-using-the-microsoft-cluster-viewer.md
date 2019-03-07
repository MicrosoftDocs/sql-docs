---
title: "Browse a Model Using the Microsoft Cluster Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "clusters [Analysis Services]"
  - "discrimination [Analysis Services]"
  - "names [Analysis Services], clusters"
  - "Microsoft Cluster Viewer"
  - "mining model content, viewing"
  - "comparing clusters"
  - "viewing clusters"
  - "displaying clusters"
  - "data mining [Analysis Services], clusters"
  - "Cluster Viewer [Analysis Services]"
  - "mining models [Analysis Services], clusters"
ms.assetid: 591fe30b-d88f-4a71-94d4-4a3907fc275d
author: minewiskan
ms.author: owend
manager: craigg
---
# Browse a Model Using the Microsoft Cluster Viewer
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] displays mining models that are built with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm is a segmentation algorithm for use in exploring data to identify anomalies in the data and to create predictions. For more information about this algorithm, see [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md).  
  
> [!NOTE]  
>  To view detailed information about the equations used in the model and the patterns that were discovered, use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Tree viewer. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md) or [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](../microsoft-generic-content-tree-viewer-data-mining.md).  
  
##  <a name="BKMK_ViewerTabs"></a> Viewer Tabs  
 When you browse a mining model in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the model is displayed on the **Mining Model Viewer** tab of Data Mining Designer in the appropriate viewer for the model. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer provides the following tabs for use in exploring clustering mining models:  
  
-   [Cluster Diagram](#BKMK_Diagram)  
  
-   [Cluster Profiles](#BKMK_Profile)  
  
-   [Cluster Characteristics](#BKMK_Characteristics)  
  
-   [Cluster Discrimination](#BKMK_Discrimination)  
  
###  <a name="BKMK_Diagram"></a> Cluster Diagram  
 The **Cluster Diagram** tab of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Cluster Viewer displays all the clusters that are in a mining model. The shading of the line that connects one cluster to another represents the strength of the similarity of the clusters. If the shading is light or nonexistent, the clusters are not very similar. As the line becomes darker, the similarity of the links becomes stronger. You can adjust how many lines the viewer shows by adjusting the slider to the right of the clusters. Lowering the slider shows only the strongest links.  
  
 By default, the shade represents the population of the cluster. By using the **ShadingVariable** and **State** options, you can select which attribute and state pair the shading represents. The darker the shading, the greater the attribute distribution is for a specific state. The distribution decreases as the shading gets lighter.  
  
 To rename a cluster, right-click its node and select **Rename Cluster**. The new name is persisted to the server.  
  
 To copy the visible section of the diagram to the Clipboard, click **Copy Graph View**. To copy the complete diagram, click **Copy Entire Graph**. You can also zoom in and out by using **Zoom In** and **Zoom Out**, or you can fit the diagram to the screen by using **Scale Diagram to Fit in Window**.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Profile"></a> Cluster Profiles  
 The **Cluster Profiles** tab provides an overall view of the clusters that the algorithm in your model creates. This view displays each attribute, together with the distribution of the attribute in each cluster. An InfoTip for each cell displays the distribution statistics, and an InfoTip for each column heading displays the cluster population. Discrete attributes are shown as colored bars, and continuous attributes are shown as a diamond chart that represents the mean and standard deviation in each cluster. The **Histogram bars** option controls the number of bars that are visible in the histogram. If more bars exist than you choose to display, the bars of highest importance are retained, and the remaining bars are grouped together into a gray bucket.  
  
 You can change the default names of the clusters, to make the names more descriptive. Rename a cluster by right-clicking its column heading and selecting **Rename cluster**. You can also hide clusters by selecting **Hide column**.  
  
 To open a window that provides a larger, more detailed view of the clusters, double-click either a cell in the **States** column or a histogram in the viewer.  
  
 Click a column heading to sort the attributes in order of importance for that cluster. You can also drag columns to reorder them in the viewer.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Characteristics"></a> Cluster Characteristics  
 To use the **Cluster Characteristics** tab, select a cluster from the **Cluster** list. After you select a cluster, you can examine the characteristics that make up that specific cluster. The attributes that the cluster contains are listed in the **Variables** columns, and the state of the listed attribute is listed in the **Values** column. Attribute states are listed in order of importance, described by the probability that they will appear in the cluster. The probability is shown in the **Probability** column.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
###  <a name="BKMK_Discrimination"></a> Cluster Discrimination  
 You can use the **Cluster Discrimination** tab to compare attributes between two clusters. Use the **Cluster 1** and **Cluster 2** lists to select the clusters to compare. The viewer determines the most important differences between the clusters, and displays the attribute states that are associated with the differences, in order of importance. A bar to the right of the attribute shows which cluster the state favors, and the size of the bar shows how strongly the state favors the cluster.  
  
 [Back to Top](#BKMK_ViewerTabs)  
  
## See Also  
 [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Tools](data-mining-tools.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)  
  
  
