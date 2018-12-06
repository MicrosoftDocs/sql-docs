---
title: "Exploring the Clustering Model (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ce8aa034-161b-473f-baec-9c29e0a8e5f5
author: minewiskan
ms.author: owend
manager: craigg
---
# Exploring the Clustering Model (Basic Data Mining Tutorial)
  The [!INCLUDE[msCoName](../includes/msconame-md.md)] Clustering algorithm groups cases into clusters that contain similar characteristics. These groupings are useful for exploring data, identifying anomalies in the data, and creating predictions.  
  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Cluster Viewer provides the following tabs for use in exploring clustering mining models:  
  

  
##  <a name="ClusterDiagramTab"></a> Cluster Diagram Tab  
 The Cluster Diagram tab displays all the clusters that are in a mining model. The lines between the clusters represent "closeness" and are shaded based on how similar the clusters are. The actual color of each cluster represents the frequency of the variable and the state in the cluster.  
  
#### To explore the model in the Cluster Diagram tab  
  
1.  Use the **Mining Model** list at the top of the **Mining Model Viewer** tab to switch to the `TM_Clustering` model.  
  
2.  In the **Viewer** list, select **Microsoft Cluster Viewer**.  
  
3.  In the **Shading Variable** box, select **Bike Buyer**.  
  
     The default variable is **Population**, but you can change this to any attribute in the model, to discover which clusters contain members that have the attributes you want.  
  
4.  Select **1** in the **State** box to explore those cases where a bike was purchased.  
  
     The **Density** legend describes the density of the attribute state pair selected in the Shading Variable and the State. In this example it tells us that the clusterwith the darkest shading has the highest percentage of bike buyers.  
  
5.  Pause your mouse over the cluster with the darkest shading.  
  
     A tooltip displays the percentage of cases that have the attribute, `Bike Buyer = 1`.  
  
6.  Select the cluster that has the highest density, right-click the cluster, select **Rename Cluster** and type **Bike Buyers High** for later identification. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
7.  Find the cluster that has the lightest shading (and the lowest density). Right-click the cluster, select **Rename Cluster** and type **Bike Buyers Low**. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
8.  Click the **Bike Buyers High** cluster and drag it to an area of the pane that will give you a clear view of its connections to the other clusters.  
  
     When you select a cluster, the lines that connect this cluster to other clusters are highlighted, so that you can easily see all the relationships for this cluster. When the cluster is not selected, you can tell by the darkness of the lines how strong the relationships are amongst all the clusters in the diagram. If the shading is light or nonexistent, the clusters are not very similar.  
  
9. Use the slider to the left of the network, to filter out the weaker links and find the clusters with the closest relationships. The [!INCLUDE[ssSampleDBCoFull](../includes/sssampledbcofull-md.md)] marketing department might want to combine similar clusters together when determining the best method for delivering the targeted mailing.  
  

  
##  <a name="ClusterProfilesTab"></a> Cluster Profiles Tab  
 The **Cluster Profiles** tab provides an overall view of the `TM_Clustering` model. The **Cluster Profiles** tab contains a column for each cluster in the model. The first column lists the attributes that are associated with at least one cluster. The rest of the viewer contains the distribution of the states of an attribute for each cluster. The distribution of a discrete variable is shown as a colored bar with the maximum number of bars displayed in the **Histogram bars** list. Continuous attributes are displayed with a diamond chart, which represents the mean and standard deviation in each cluster.  
  
#### To explore the model in the Cluster Profiles tab  
  
1.  Set **Histogram** bars to **5**.  
  
     In our model, 5 is the maximum number of states for any one variable.  
  
2.  If the **Mining Legend** blocks the display of the **Attribute profiles**, move it out of the way.  
  
3.  Select the **Bike Buyers High** column and drag it to the right of the **Population** column.  
  
4.  Select the **Bike Buyers Low** column and drag it to the right of the **Bike Buyers High** column.  
  
5.  Click the **Bike Buyers High** column.  
  
     The **Variables** column is sorted in order of importance for that cluster. Scroll through the column and review characteristics of the Bike Buyer High cluster. For example, they are more likely to have a short commute.  
  
6.  Double-click the **Age** cell in the **Bike Buyers High** column.  
  
     The **Mining Legend** displays a more detailed view and you can see the age range of these customers as well as the mean age.  
  
7.  Right-click the **Bike Buyers Low** column and select **Hide Column**.  
  

  
##  <a name="ClusterCharacteristicsTab"></a> Cluster Characteristics Tab  
 With the **Cluster Characteristics** tab, you can examine in more detail the characteristics that make up a cluster. Instead of comparing the characteristics of all of the clusters (as in the Cluster Profiles tab), you can explore one cluster at a time. For example, if you select **Bike Buyers High** from the **Cluster** list, you can see the characteristics of the customers in this cluster. Though the display is different from the Cluster Profiles viewer, the findings are the same.  
  
> [!NOTE]  
>  Unless you set an initial value for **holdoutseed**, results will vary each time you process the model. For more information, see [HoldoutSeed Element](https://docs.microsoft.com/bi-reference/assl/properties/holdoutseed-element)  
  

  
##  <a name="ClusterDiscriminationTab"></a> Cluster Discrimination Tab  
 With the **Cluster Discrimination** tab, you can explore the characteristics that distinguish one cluster from another. After you select two clusters, one from the **Cluster 1** list, and one from the **Cluster 2** list, the viewer calculates the differences between the clusters and displays a list of the attributes that distinguish the clusters most.  
  
#### To explore the model in the Cluster Discrimination tab  
  
1.  In the **Cluster 1** box, select **Bike Buyers High**.  
  
2.  In the **Cluster 2** box, select **Bike Buyers Low**.  
  
3.  Click **Variables** to sort alphabetically.  
  
     Some of the more substantial differences among the customers in the **Bike Buyers Low** and **Bike Buyers High** clusters include age, car ownership, number of children, and region.  
  
## Related Tasks  
 See the following topics to explore the other mining models.  
  
-   [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
-   [Exploring the Naive Bayes Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-naive-bayes-model-basic-data-mining-tutorial.md)  
  
## Next Task in Lesson  
 [Exploring the Naive Bayes Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-naive-bayes-model-basic-data-mining-tutorial.md)  
  
## Previous Task in Lesson  
 [Exploring the Decision Tree Model &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-decision-tree-model-basic-data-mining-tutorial.md)  
  
## See Also  
 [Browse a Model Using the Microsoft Cluster Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md)   
 [Cluster Discrimination Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/cluster-discrimination-tab-mining-model-viewer.md)   
 [Cluster Profiles Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/cluster-profiles-tab-mining-model-viewer.md)   
 [Cluster Characteristics Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/cluster-characteristics-tab-mining-model-viewer.md)   
 [Cluster Diagram Tab &#40;Mining Model Viewer&#41;](../../2014/analysis-services/cluster-diagram-tab-mining-model-viewer.md)  
  
  
