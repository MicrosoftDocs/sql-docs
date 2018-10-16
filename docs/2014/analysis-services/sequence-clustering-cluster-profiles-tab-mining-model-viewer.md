---
title: "Sequence Clustering Cluster Profiles Tab (Mining Model Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.sequenceclustering.profiles.f1"
ms.assetid: 44230895-0a42-4032-8d6c-0cdb8a2dbb8c
author: minewiskan
ms.author: owend
manager: craigg
---
# Sequence Clustering Cluster Profiles Tab (Mining Model Viewer
  The **Cluster Profiles** tab in the **Microsoft Sequence Clustering Viewer** provides a color-coded view of sequences that are included in each cluster.  
  
 Use this view of a sequence clustering model to get a quick view of how the sequences found by the model are grouped. You can see at a glance how many sequences are long and how many are short. You can also click a cluster and display the **Mining Legend** to see exactly which states the colors in each sequence represent.  
  
 **For More Information:**  [Microsoft Sequence Clustering Algorithm](data-mining/microsoft-sequence-clustering-algorithm.md), [Browse a Model Using the Microsoft Sequence Cluster Viewer](data-mining/browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
  
## Options  
 **Refresh viewer content**  
 Reload the mining model in the viewer.  
  
 **Mining Model**  
 Choose a mining model to view that is contained in the current mining structure. The mining model will open in its associated viewer.  
  
 **Viewer**  
 Choose a viewer to use in exploring the selected mining model. You can use the custom viewer, or the **Microsoft Generic Content Tree Viewer**. You can also use plug-in viewers if available.  
  
 **Show Legend**  
 Select this option to display a legend that shows the correlation between the colors displayed in the cluster profiles and the text values of the states.  
  
 **Histogram Bars**  
 Use this option to change the number of colored bars that are included in the histogram. If more bars exist than you choose to display, the bars of highest importance are retained, and the remaining bars are grouped together into the **Other**.  
  
 **Attributes** and **Cluster Profiles**  
 This section of the graph chart lists the clusters of sequences that were found in the model.  
  
 Each sequence cluster is displayed using the number of states you selected in the option, **Histogram bars**.  
  
 Two sets of histograms are displayed for each cluster in the model, each on a different row in the graph:  
  
-   **\<attribute name>.samples**: The histograms in this row show the sequences of items that are representative of each cluster. In DMX terms, these are the sample cases for each cluster.  
  
-   **\<attribute name>**: The histograms in this row describe all the items that the cluster contains and their overall distribution. Click a histogram when the **Mining Legend** is visible and you can see the numeric values for each  
  
 **States**  
 This column in the chart is optional, and can be displayed or removed by selecting the **Show Legend** option. The **States** column provides a guide as to which state is represented by which color in the corresponding clusters histogram.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Microsoft Sequence Clustering Algorithm](data-mining/microsoft-sequence-clustering-algorithm.md)   
 [Mining Model Viewers &#40;Data Mining Model Designer&#41;](mining-model-viewers-data-mining-model-designer.md)   
 [Data Mining Model Viewers](data-mining/data-mining-model-viewers.md)   
 [Browse a Model Using the Microsoft Sequence Cluster Viewer](data-mining/browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
  
  
