---
title: "Mining Model Content for Sequence Clustering Models | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Mining Model Content for Sequence Clustering Models
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This topic describes mining model content that is specific to models that use the Microsoft Sequence Clustering algorithm. For an explanation of general and statistical terminology related to mining model content that applies to all model types, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
## Understanding the Structure of a Sequence Clustering Model  
 A sequence clustering model has a single parent node (NODE_TYPE = 1) that represents the model and its metadata. The parent node, which is labeled **(All)**, has a related sequence node (NODE_TYPE = 13) that lists all the transitions that were detected in the training data.  
  
 ![Structure of sequence clustering model](../../analysis-services/data-mining/media/modelcontent-seqclust.gif "Structure of sequence clustering model")  
  
 The algorithm also creates a number of clusters, based on the transitions that were found in the data and any other input attributes included when creating the model, such as customer demographics and so forth. Each cluster (NODE_TYPE = 5) contains its own sequence node (NODE_TYPE = 13) that lists only the transitions that were used in generating that specific cluster. From the sequence node, you can drill down to view the details of individual state transitions (NODE_TYPE = 14).  
  
 For an explanation of sequence and state transitions, with examples, see [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md).  
  
## Model Content for a Sequence Clustering Model  
 This section provides additional information about columns in the mining model content that have particular relevance for sequence clustering.  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 Always blank.  
  
 NODE_NAME  
 The name of the node. Currently the same value as NODE_UNIQUE_NAME.  
  
 NODE_UNIQUE_NAME  
 The unique name of the node.  
  
 NODE_TYPE  
 A sequence clustering model outputs the following node types:  
  
|Node Type ID|Description|  
|------------------|-----------------|  
|1 (Model)|Root node for model|  
|5 (Cluster)|Contains a count of transitions in the cluster, a list of the attributes, and statistics that describe the values in the cluster.|  
|13 (Sequence)|Contains a list of transitions included in the cluster.|  
|14 (Transition)|Describes a sequence of events as a table in which the first row contains the starting state, and all other rows contain successive states, together with support and probability statistics.|  
  
 NODE_GUID  
 Blank.  
  
 NODE_CAPTION  
 A label or a caption associated with the node for display purposes.  
  
 You can rename the cluster captions while you are using the model; however, the new name is not persisted if you close the model.  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
 **Model root** Cardinality value equals the number of clusters plus one. For more information, see [Cardinality](#bkmk_cardinality).  
  
 **Cluster nodes** Cardinality is always 1, because each cluster has a single child node, which contains the list of sequences in the cluster.  
  
 **Sequence nodes** Cardinality indicates the number of transitions that are included in that cluster. For example, the cardinality of the sequence node for the model root tells you how many transitions were found in the entire model.  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent.  
  
 NULL is returned for any nodes at the root level.  
  
 NODE_DESCRIPTION  
 Same as node caption.  
  
 NODE_RULE  
 Always blank.  
  
 MARGINAL_RULE  
 Always blank.  
  
 NODE_PROBABILITY  
 **Model root** Always 0.  
  
 **Cluster nodes** The adjusted probability of the cluster in the model. The adjusted probabilities do not sum to 1, because the clustering method used in sequence clustering permits partial membership in multiple clusters.  
  
 **Sequence nodes** Always 0.  
  
 **Transition nodes** Always 0.  
  
 MARGINAL_PROBABILITY  
 **Model root** Always 0.  
  
 **Cluster nodes** The same value as NODE_PROBABILITY.  
  
 **Sequence nodes** Always 0.  
  
 **Transition nodes** Always 0.  
  
 NODE_DISTRIBUTION  
 A table that contains probabilities and other information. For more information, see [NODE_DISTRIBUTION Table](#bkmk_NODEDIST).  
  
 NODE_SUPPORT  
 The number of transitions that support this node. Therefore, if there are 30 examples of sequence "Product A followed by Product B" in the training data, the total support is 30.  
  
 **Model root** Total number of transitions in the model.  
  
 **Cluster nodes** Raw support for the cluster, meaning the number of training cases that contribute cases to this cluster.  
  
 **Sequence nodes** Always 0.  
  
 **Transition nodes** Percentage of cases in the cluster that represent a specific transition. Can be 0, or can have a positive value. Calculated by taking the raw support for the cluster node, and multiplying by the probability of the cluster.  
  
 From this value, you can tell how many training cases contributed to the transition.  
  
 MSOLAP_MODEL_COLUMN  
 Not applicable.  
  
 MSOLAP_NODE_SCORE  
 Not applicable.  
  
 MSOLAP_NODE_SHORT_CAPTION  
 Same as NODE_DESCRIPTION.  
  
## Understanding Sequences, States and Transitions  
 A sequence clustering model has a unique structure that combines two kinds of objects with very different types of information: the first are clusters, and the second are state transitions.  
  
 The clusters created by sequence clustering are like the clusters created by the Microsoft Clustering algorithm. Each cluster has a profile and characteristics. However, in sequence clustering, each cluster additionally contains a single child node that lists the sequences in that cluster. Each sequence node contains multiple child nodes that describe the state transitions in detail, with probabilities.  
  
 There are almost always more sequences in the model than you can find in any single case, because the sequences can be chained together. Microsoft Analysis Services stores pointers from one state to the other so that you can count the number of times each transition happens. You can also find information about how many times the sequence occurred, and measure its probability of occurring as compared to the entire set of observed states.  
  
 The following table summarizes how information is stored in the model, and how the nodes are related.  
  
|Node|Has child node|NODE_DISTRIBUTION table|  
|----------|--------------------|------------------------------|  
|Model root|Multiple cluster nodes<br /><br /> Node with sequences for entire model|Lists all products in the model, with support and probability.<br /><br /> Because the clustering method permits partial membership in multiple clusters, support and probability can have fractional values. That is, instead of counting a single case once, each case can potentially belong to multiple clusters. Therefore, when the final cluster membership is determined, the value is adjusted by the probability of that cluster.|  
|Sequence node for model|Multiple transition nodes|Lists all products in the model, with support and probability.<br /><br /> Because the number of sequences is known for the model, at this level, calculations for support and probability are straightforward:<br /><br /> <br /><br /> Support = count of cases<br /><br /> Probability = raw probability of each sequence in model. All probabilities should sum to 1.|  
|Individual cluster nodes|Node with sequences for that cluster only|Lists all products in a cluster, but provides support and probability values only for products that are characteristic of the cluster.<br /><br /> Support represents the adjusted support value for each case in this cluster. Probability values are adjusted probability.|  
|Sequence nodes for individual clusters|Multiple nodes with transitions for sequences in that cluster only|Exactly the same information as in individual cluster nodes.|  
|Transitions|No children|Lists transitions for the related first state.<br /><br /> Support is an adjusted support value, indicating the cases that take part in each transition. Probability is the adjusted probability, represented as a percentage.|  
  
###  <a name="bkmk_NODEDIST"></a> NODE_DISTRIBUTION Table  
 The NODE_DISTRIBUTION table provides detailed probability and support information for the transitions and sequences for a specific cluster.  
  
 A row is always added to the transition table to represent possible **Missing** values. For information about what the **Missing** value means, and how it affects calculations, see [Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md).  
  
 The calculations for support and probability differ depending on whether the calculation applies to the training cases or to the finished model. This is because the default clustering method, Expectation Maximization (EM), assumes that any case can belong to more than one cluster. When calculating support for the cases in the model, it is possible to use raw counts and raw probabilities. However, the probabilities for any particular sequence in a cluster must be weighted by the sum of all possible sequence and cluster combinations.  
  
###  <a name="bkmk_cardinality"></a> Cardinality  
 In a clustering model, the cardinality of the parent node generally tells you how many clusters are in the model. However, a sequence clustering model has two kinds of nodes at the cluster level: one kind of node contains clusters, and the other kind of node contains a list of sequences for the model as a whole.  
  
 Therefore, to learn the number of clusters in the model, you can take the value of NODE_CARDINALITY for the (All) node and subtract one. For example, if the model created 9 clusters, the cardinality of the model root is 10. This is because the model contains 9 cluster nodes, each with its own sequence node, plus one additional sequence node labeled cluster 10, which represents the sequences for the model.  
  
## Walkthrough of Structure  
 An example might help clarify how the information is stored, and how you can interpret it. For example, you can find the largest order, meaning the longest observed chain in the underlying [!INCLUDE[ssSampleDBDWobject](../../includes/sssampledbdwobject-md.md)] data, by using the following query:  
  
```  
USE AdventureWorksDW2012  
SELECT DISTINCT OrderNumber, Count(*)  
FROM vAssocSeqLineItems  
GROUP BY OrderNumber  
ORDER BY Count(*) DESC  
```  
  
 From these results, you find that the order numbers 'SO72656', 'SO58845', and 'SO70714' contain the largest sequences, with eight items each. By using the order IDs, you can view the details of a particular order to see which items were purchased, and in what order.  
  
|OrderNumber|LineNumber|Model|  
|-----------------|----------------|-----------|  
|SO58845|1|Mountain-500|  
|SO58845|2|LL Mountain Tire|  
|SO58845|3|Mountain Tire Tube|  
|SO58845|4|Fender Set - Mountain|  
|SO58845|5|Mountain Bottle Cage|  
|SO58845|6|Water Bottle|  
|SO58845|7|Sport-100|  
|SO58845|8|Long-Sleeve Logo Jersey|  
  
 However, some customers who purchase the Mountain-500 might purchase different products. You can view all the products that follow the Mountain-500 by viewing the list of sequences in the model. The following procedures walk you through viewing these sequences by using the two viewers provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
#### To view related sequences by using the Sequence Clustering viewer  
  
1.  In Object Explorer, right-click the [Sequence Clustering] model, and select Browse.  
  
2.  In the Sequence Clustering viewer, click the **State Transitions** tab.  
  
3.  In the **Cluster** dropdown list, ensure that **Population (All)** is selected.  
  
4.  Move the slider bar at the left of the pane all the way to the top, to show all links.  
  
5.  In the diagram, locate **Mountain-500**, and click the node in the diagram.  
  
6.  The highlighted lines point to the next states (the products that were purchased after the Mountain-500) and the numbers indicate the probability. Compare these to the results in the generic model content viewer.  
  
#### To view related sequences by using the generic model content viewer  
  
1.  In Object Explorer, right-click the [Sequence Clustering] model, and select Browse.  
  
2.  In the viewer dropdown list, select the **Microsoft Generic Content Tree Viewer**.  
  
3.  In the **Node caption** pane, click the node named **Sequence level for cluster 16**.  
  
4.  In the Node details pane, find the NODE_DISTRIBUTION row, and click anywhere in the nested table.  
  
     The top row is always for the Missing value. This row is sequence state 0.  
  
5.  Press the down arrow key, or use the scroll bars, to move down through the nested table until you see the row, Mountain-500.  
  
     This row is sequence state 20.  
  
    > [!NOTE]  
    >  You can obtain the row number for a particular sequence state programmatically, but if you are just browsing, it might be easier to simply copy the nested table into an Excel workbook.  
  
6.  Return to the Node caption pane, and expand the node, **Sequence level for cluster 16**, if it is not already expanded.  
  
7.  Look among its child nodes for **Transition row for sequence state 20**. Click the transition node.  
  
8.  The nested NODE_DISTRIBUTION table contains the following products and probabilities. Compare these to the results in the **State Transition** tab of the Sequence Clustering viewer.  
  
 The following table shows the results from the NODE_DISTRIBUTION table, together with the rounded probability values that are displayed in the graphical viewer.  
  
|Product|Support (NODE_DISTRIBUTION table)|Probability (NODE_DISTRIBUTION) table)|Probability (from graph)|  
|-------------|------------------------------------------|------------------------------------------------|--------------------------------|  
|Missing|48.447887|0.138028169|(not shown)|  
|Cycling Cap|10.876056|0.030985915|0.03|  
|Fender Set - Mountain|80.087324|0.228169014|0.23|  
|Half-Finger Gloves|0.9887324|0.002816901|0.00|  
|Hydration Pack|0.9887324|0.002816901|0.00|  
|LL Mountain Tire|51.414085|0.146478873|0.15|  
|Long-Sleeve Logo Jersey|2.9661972|0.008450704|0.01|  
|Mountain Bottle Cage|87.997183|0.250704225|0.25|  
|Mountain Tire Tube|16.808451|0.047887324|0.05|  
|Short-Sleeve Classic Jersey|10.876056|0.030985915|0.03|  
|Sport-100|20.76338|0.05915493|0.06|  
|Water Bottle|18.785915|0.053521127|0.25|  
  
 Although the case that we initially selected from the training data contained the product 'Mountain-500' followed by 'LL Mountain Tire', you can see that there are many other possible sequences. To find detailed information for any particular cluster, you must repeat the process of drilling down from the list of sequences in the cluster to the actual transitions for each state, or product.  
  
 You can jump from the sequence listed in one particular cluster, to the transition row. From that transition row, you can determine which product is next, and jump back to that product in the list of sequences. By repeating this process for each first and second state you can work through long chains of states.  
  
## Using Sequence Information  
 A common scenario for sequence clustering is to track user clicks on a Web site. For example, if the data were from records of customer purchases on the Adventure Works e-commerce Web site, the resulting sequence clustering model could be used to infer user behavior, to redesign the e-commerce site to solve navigation problems, or to promote sales.  
  
 For example, analysis might show that users always follow a particular chain of products, regardless of demographics. Also, you might find that users frequently exit the site after clicking on a particular product. Given that finding, you might ask what additional paths you could provide to users that would induce users to stay on the Web site.  
  
 If you do not have additional information to use in classifying your users, then you can simply use the sequence information to collect data about navigation to better understand overall behavior. However, if you can collect information about customers and match that information with your customer database, you can combine the power of clustering with prediction on sequences to provide recommendations that are tailored to the user, or perhaps based on the path of navigation to the current page.  
  
 Another use of the extensive state and transition information compiled by a sequence clustering model is to determine which possible paths are never used. For example, if you have many visitors going to pages 1-4, but visitors never continue on to page 5, you might investigate whether there are problems that prevent navigation to page 5. You can do this by querying the model content, and comparing it against a list of possible paths.  Graphs that tell you all the navigation paths in a Web site can be created programmatically, or by using a variety of site analysis tools.  
  
 To find out how to obtain the list of observed paths by querying the model content, and to see other examples of queries on a sequence clustering model, see [Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md).  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)   
 [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)   
 [Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md)  
  
  
