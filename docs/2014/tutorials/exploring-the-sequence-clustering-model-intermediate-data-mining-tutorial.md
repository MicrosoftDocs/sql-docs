---
title: "Exploring the Sequence Clustering Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: f8a485d5-47ed-4dd5-bb66-ef4d6d463845
author: minewiskan
ms.author: owend
manager: kfile
---
# Exploring the Sequence Clustering Model (Intermediate Data Mining Tutorial)
  Now that you have built the **Sequence Clustering with Region** model, you can explore it by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering Viewer in the **Mining Model Viewer** tab of Data Mining Designer. The [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Cluster Viewer contains five tabs: **Cluster Diagram**, **Cluster Profiles**, **Cluster Characteristics**, **ClusterDiscrimination**, and **State Transitions**. For more information about how to use this viewer, see [Browse a Model Using the Microsoft Sequence Cluster Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-sequence-cluster-viewer.md).  
  
-   [Cluster Diagram tab](#bkmk_CDiagram)  
  
-   [Cluster Profiles tab](#bkmk_CProfiles)  
  
-   [Cluster Characteristics tab](#bkmk_CChars)  
  
-   [Cluster Discrimination tab](#bkmk_CDiscrim2)  
  
-   [State Transitions tab](#bkmk_StateTran)  
  
-   [Generic Content View](#bkmk_Generic)  
  
##  <a name="bkmk_CDiagram"></a> Cluster Diagram Tab  
 The **Cluster Diagram** tab graphically displays the clusters that the algorithm discovered in the database. The layout in the diagram represents the relationships of the clusters, with similar clusters grouped close together. By default, the shade of each node represents the density of all cases in the cluster: the darker the shade of the node, the more cases it contains. You can change the meaning of the shading of the nodes so that it represents support, within each cluster, for an attribute and a state.  
  
 You can also rename the clusters, to make it easier to identify and work with target clusters. For this tutorial, you will rename the cluster that has the highest percentage of customers from the Pacific region, and the cluster that has the most cases overall.  
  
> [!NOTE]  
>  The cases that are assigned to specific clusters might change when you reprocess the model, depending on the data and the model parameters. Also, if you rename clusters, the names will be lost when you reprocess the mining model.  
  
#### To change the attribute used for highlighting clusters  
  
1.  In the **Shading Variable** list, select **Model**.  
  
2.  Select **Cycling Cap** in the **State** list.  
  
     The diagram updates to show the concentration of the selected product in each of the clusters. The cluster that has the darkest shading contains the highest density of cycling caps. You can change the shading variable to use any state of any input column.  
  
3.  In the **Shading Variable** list, select **Population**.  
  
     When you change the shading variable to population, the diagram updates to compare the clusters by size. The cluster that has the darkest shading contains more cases than the other clusters.  
  
#### To rename nodes in the model  
  
1.  Change **Shading Variable** to `Region`, and set **State** to **Pacific**.  
  
2.  Highlight the darkest node in the graph.  
  
3.  Right-click this cluster and select **Rename Cluster.**  
  
4.  Type the name**Pacific Cluster.**  
  
5.  Change the value of **Shading Variable** to **Population**.  
  
6.  In the updated graph, locate the darkest cluster, which should be the largest cluster. If you cannot tell by the shading which cluster is largest, pause the mouse over each cluster and view the ToolTip, and then choose the cluster that contains the most cases.  
  
7.  Right-click this cluster and select **Rename Cluster**. Type the new name, `Largest Cluster`.  
  
 You can drill through from the node that represents the cluster to view details of the cases that are in each cluster. This can be useful if you want to take action on the results of your analysis, such as sending e-mail to a customer. You can also browse the other attributes of the cases that you included in the structure but did not use in the model, such as Region and IncomeGroup. For more information about drilling through from mining models to the underlying cases, see [Drillthrough Queries &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
#### To drill through to details from the Cluster diagram  
  
1.  Right-click `Pacific Cluster`, select **Drill Through**, and then select **Model and Structure columns**.  
  
     The **Drill Through** dialog box opens. Columns that are not used in the model but that are available for querying are prefixed with **Structure**.  
  
     You can see that this cluster contains mostly customers from the Pacific region, with only a few customers from other regions.  
  
2.  Click the plus sign in the nested column v Assoc Seq Line Items to view the sequence of items in a particular customer order.  
  
3.  Close the **Drill Through** dialog box.  
  
    > [!NOTE]  
    >  The **Play** button enables you to requery the data; however, requerying does not change the data that is displayed, unless the model has been dynamically updated in the background by some other process.  
  
 [Back to Top](#bkmk_CDiagram)  
  
##  <a name="bkmk_CProfiles"></a> Cluster Profiles Tab  
 The **Cluster Profiles** tab displays the sequences that are in each cluster. The clusters are listed in individual columns to the right of the **States** column.  
  
 In the viewer, the **Model** row describes the overall distribution of items in a cluster, and the **Model.samples** row contains sequences of the items. Each line of the color sequences in each cell of the **Model.samples** row represents the behavior of a randomly selected user in the cluster.  
  
 Each color in an individual sequence histogram represents a product model. The Mining Legend shows you the sequences of products by using both color-coding and the product model names. If you have added other columns to the model for clustering, such as Region or Income Group, the viewer will contain an additional row for each column that shows the distribution of these values within each cluster.  
  
#### To view the sequences that are most common in a cluster  
  
1.  Right-click the **Model** row in the column for the cluster `Largest Cluster`, and select **Show Legend**.  
  
     The **Color** column contains a shaded bar that indicates the frequency of items found in sequences. Each item is represented by a different color. The **Meaning** column lists the product model names for each color. The **Distribution** column tells you the percentage of cases that contained this item in a sequence.  
  
2.  Close the **Mining Legend**.  
  
3.  Right-click the **Model.samples** row in the column with the heading, **Population,** and select **Show Legend**.  
  
4.  Scan the list of sequences in the overall model`.`  
  
     The Mining Legend lists the most common sequences first, so you can see that Mountain Tire Tube is the first item in many sequences. This means that a customer is very likely to put the Mountain Tire Tube in the shopping basket first.  
  
#### To drill through to cases from the cluster viewer  
  
1.  Scroll down in the Attribute pane until you find the row for the `Region` attribute.  
  
     The row contains a histogram for each cluster in the model, plus one additional histogram for **Population**, meaning the entire set of cases used in the model. A histogram is a bar with different colors in it, where each color represents an attribute, and the size of the colored section for that attribute represents the percentage of cases with that attribute.  
  
2.  Compare the histograms for the clusters that you renamed `Pacific Cluster` and `Largest Cluster`. Each cluster appears in a different column.  
  
     Both look like solid colors, but the colors are different.  
  
3.  In the `Region` row, pause the mouse over the colored histogram for `Largest Cluster`.  
  
     The ToolTip displays values that show the actual percentages of cases from each region.  
  
4.  Right-click the colored histogram in the `Region` row for `Pacific Cluster`, select **Drill Through**, and then select **Model Columns Only**.  
  
5.  Move the scroll bar to review all of the customers in this cluster.  
  
     Again, from drilling through to the details you can see that the cluster contains mostly orders from the Pacific region but also a few from the North America and Europe regions.  
  
6.  Close the **Drill Through** dialog box.  
  
 [Back to Top](#bkmk_CDiagram)  
  
##  <a name="bkmk_CChars"></a> Cluster Characteristics Tab  
 The **Cluster Characteristics** tab summarizes the transitions between states in a cluster by displaying bars that visually represent the importance of the attribute value for the selected cluster. The **Variables** column tells you what the model found to be important for the selected cluster or population: either a particular value or the relationship between values, known as *transition*. The **Values** column provides more detail about the value or transition, and the **Probability** column visually represents the weight of this attribute or transition.  
  
#### To view the important attributes for a cluster  
  
1.  In the **Cluster** dropdown list, select `Pacific Cluster`.  
  
     The list updates to show the characteristics of the cluster that you renamed `Pacific Cluster`. In this cluster, the most important characteristic is `Region`.  
  
2.  Pause the mouse over the shaded bar in the row for `Region`.  
  
     The probability of the value being Pacific is very high. For more information about how to interpret these values, see [Microsoft Sequence Clustering Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-sequence-clustering-algorithm-technical-reference.md).  
  
3.  Look through the list of characteristics for the cluster until you find the first transition row.  
  
4.  A transition row contains the text Transition in the **Variables** column, and some combination of sequential attribute values in the **Value** column. The sequence can also contain starting points and missing values.  
  
     For example, suppose the transition has the value, [Start] -> Road Tire Tube. This means that customers in this cluster frequently put the Road Tire Tube in their shopping basket first. This might signify that the product is a popular item that customers seek out first, or it might only indicate that the product is easy to find on the purchasing site.  
  
5.  Scroll through the list until you find the first transition that does not have **[Start]** or **missing** in it.  
  
     For example, suppose you find the transition, **Touring Tire, Touring Tire Tube**. This means that customers in this cluster frequently purchased these items together, in exactly this order.  
  
6.  Pause the mouse over the shaded bar for this transition.  
  
     The probability of this transition is displayed as a percentage.  
  
7.  In the **Cluster** dropdown list, select **Population (All)**.  
  
     The list of attributes updates to show the characteristics of all orders used to create the model. In this mining model, the most important characteristic for distinguishing between clusters is `Region`, with a value of **North America**.  
  
 After reviewing these tasks, you realize two things. The first is that you need a lot of data to obtain a meaningful number of combinations. For example, the sequences with the highest probabilities are likely to include a **[Start]** or **Missing** state.  
  
 The second is that there is a strong clustering effect on attributes for `Region`, which makes it more difficult to see the groups of sequences. Therefore, you decide to create another model that uses sequences only, and does not include the columns for region or income.  
  
 [Back to Top](#bkmk_CDiagram)  
  
##  <a name="bkmk_CDiscrim2"></a> Cluster Discrimination Tab  
 The **Cluster Discrimination** tab helps you compare two clusters, to determine which attributes distinguish a particular cluster from another cluster. The tab contains four columns: **Variables**, **Values**, **Cluster 1**, and **Cluster 2**.  You can choose any cluster to use as **Cluster 1** and **Cluster 2**.  
  
 The **Variables** column tells you the name of the attribute, which can either be a column name or combination of column name and the word **transition**. The **Values** column shows the exact value of the attribute or the transition. The shaded bars in the columns for **Cluster 1** and **Cluster 2** indicate the strength of the attribute in the clusters that you are comparing. The longer the bar, the more the cluster is likely to include cases with that attribute.  
  
#### To compare two clusters by using the Cluster Discrimination tab  
  
1.  In the **Cluster Discrimination** tab, for **Cluster 1**, select `Pacific Cluster`.  
  
     By default, the selection for **Cluster 2** changes to **Complement of Pacific Cluster**.  
  
     The top attribute that distinguishes `Pacific Cluster` from all other cases is the region. Region is such a strong attribute for clustering that it obscures other attributes. To avoid this effect, try comparing several of the smaller clusters to each other. When you do so, the list of attributes changes and might include more transitions between models.  
  
2.  Locate a transition row, and pause the mouse over the shaded bar.  
  
     The items in the **Values** column can include both states and transitions. The shading for each item indicates the discrimination score. To learn more about the meaning of different scores, see [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md).  
  
 [Back to Top](#bkmk_CDiagram)  
  
##  <a name="bkmk_StateTran"></a> State Transitions Tab  
 On the **State Transitions** tab, you can select a cluster and browse through its state transitions. If you select **Population (All)** from the cluster drop-down list, the diagram shows the distribution of states for the whole mining model.  
  
 Each node in the graph represents a state, or possible value, of the sequences that you are trying to analyze. The background color of the nodes represents the frequency of that state. Lines connect some states, indicating a transition between states. You can move the slider up or down to change the probability threshold for the transitions. Numbers are associated with some nodes, indicating the probability of that state.  
  
#### To explore the relationships in the State Transition tab  
  
1.  In the **State Transitions** tab of the Mining Model viewer, select `Pacific Cluster` from the list of clusters. Ensure that the **Show Edge Labels** option is selected.  
  
     The graph updates to show the transitions that are most common in this cluster.  
  
2.  Click any node that is connected by a line to another node.  
  
     The graph is updated and highlights the related nodes. The numeric value next to the line indicates the probability of the transition.  
  
3.  Raise the slider up to **All Links**, to increase the number of transitions included in the graph.  
  
4.  Select **Population (All)** from **Cluster**.  
  
     Note that when you load a different cluster, the graph resets to the default display settings, so the slider control is reset to the middle position.  
  
5.  Click the darkest node in the graph, which should be **Sport-100**.  
  
     Note that there are no lines connecting this product to other products.  
  
6.  Raise the slider up one step, to increase the number of transitions included in the graph. Do not go all the way to **All Links** yet.  
  
     The graph is updated by adding several more transitions to the graph, but none that include the Sport-100 model.  
  
7.  Move the slider control all the way to **All Links**. Click the Sport-100 node if it is not already selected.  
  
     The graph updates to show many transitions that include the Sport-100 product. The direction of the arrow on the connecting line tells you whether the Sport-100 item was selected as the first item or the second item in the pair.  
  
8.  Clicking the node for Touring Tire and move the slider control back down to the middle position.  
  
     At first, there are many transition lines connecting Touring Tire to other products, but when you raise the probability threshold, the less likely transitions are eliminated from the graph, leaving just the transition, Touring Tire > Touring Tire Tube. This transition means that if a customer puts a Touring Tire into the shopping basket, there is a strong probability that the customer will next put a Touring Tire Tube into the basket.  
  
 [Back to Top](#bkmk_CDiagram)  
  
##  <a name="bkmk_Generic"></a> Generic Content Tree Viewer  
 This viewer can be used for all models, regardless of the algorithm or model type. The **MicrosoftGeneric Content Tree Viewer** is available from the **Viewer** drop-down list.  
  
 A content tree is a representation of any mining model as a series of nodes, wherein each node represents learned knowledge about the training data. The node can contain a pattern, a set of rules, a cluster, or the definition of a range of dates that share some attributes. The exact content of the node differs depending on the algorithm and the predictable attribute, but the general representation of the content is the same.  
  
 You can expand each node to see increasing levels of detail, and copy the content of any node to the Clipboard. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](../../2014/analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md).  
  
#### To view details for a sequence clustering model by using the Generic Content Tree Viewer  
  
1.  In the **Mining Model Viewer** tab, click the **Viewer** list, and select **Microsoft Generic Content Tree viewer**.  
  
2.  In the **Node Caption** pane, click `Pacific Cluster (1)`.  
  
     The name for this node contains both the friendly name that you assigned to the cluster and the underlying node ID. You can use the node IDs to drill down into additional detail in the model.  
  
3.  Expand the first child node, named **Sequence level for cluster 1**.  
  
     The sequence level node for a cluster contains details about the states and transitions that are included in that cluster. You can use these details, available in the NODE_DISTRIBUTION column, to explore the sequences and the states for each cluster or for the model as a while.  
  
4.  Continue to expand nodes and view the details in the HTML viewer pane.  
  
 For more information about the mining model content, and how to use the details in the viewer, see [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md).  
  
 [Back to Top](#bkmk_CDiagram)  
  
## Next Task in Lesson  
 [Creating a Related Sequence Clustering Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/creating-a-related-sequence-clustering-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Microsoft Sequence Clustering Algorithm](../../2014/analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)   
 [Sequence Clustering Model Query Examples](../../2014/analysis-services/data-mining/sequence-clustering-model-query-examples.md)  
  
  
