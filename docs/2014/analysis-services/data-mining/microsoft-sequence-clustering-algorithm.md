---
title: "Microsoft Sequence Clustering Algorithm | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "clusters [Analysis Services]"
  - "algorithms [data mining]"
  - "sequence clustering algorithms [Analysis Services]"
  - "sequence [Analysis Services]"
ms.assetid: ae779a1f-0adb-4857-afbd-a15543dff299
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Sequence Clustering Algorithm
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm is a sequence analysis algorithm provided by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. You can use this algorithm to explore data that contains events that can be linked by following paths, or *sequences*. The algorithm finds the most common sequences by grouping, or clustering, sequences that are identical. The following are some examples of data that contain sequences which might be used for data mining, to provide insight about common problems or business scenarios:  
  
-   Click paths that are created when users navigate or browse a Web site.  
  
-   Logs that list events preceding an incident, such as hard disk failure or server deadlocks.  
  
-   Transaction records that describe the order in which a customer adds items to a shopping cart at an online retailer.  
  
-   Records that follow customer (or patient) interactions over time, to predict service cancellations or other poor outcomes.  
  
 This algorithm is similar in many ways to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm. However, instead of finding clusters of cases that contain similar attributes, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm finds clusters of cases that contain similar paths in a sequence.  
  
## Example  
 The [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] Web site collects information about what pages site users visit, and about the order in which the pages are visited. Because the company provides online ordering, customers must log in to the site. This provides the company with click information for each customer profile. By using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm on this data, the company can find groups, or clusters, of customers who have similar patterns or sequences of clicks. The company can then use these clusters to analyze how users move through the Web site, to identify which pages are most closely related to the sale of a particular product, and to predict which pages are most likely to be visited next.  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm is a hybrid algorithm that combines clustering techniques with Markov chain analysis to identify clusters and their sequences. One of the hallmarks of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm is that it uses sequence data. This data typically represents a series of events or transitions between states in a dataset, such as a series of product purchases or Web clicks for a particular user. The algorithm examines all transition probabilities and measures the differences, or distances, between all the possible sequences in the dataset to determine which sequences are the best to use as inputs for clustering. After the algorithm has created the list of candidate sequences, it uses the sequence information as an input for the EM method of clustering.  
  
 For a detailed description of the implementation, see [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md).  
  
## Data Required for Sequence Clustering Models  
 When you prepare data for use in training a sequence clustering model, you should understand the requirements for the particular algorithm, including how much data is needed, and how the data is used.  
  
 The requirements for a sequence clustering model are as follows:  
  
-   **A single key column** A sequence clustering model requires a key that identifies records.  
  
-   **A sequence column** For sequence data, the model must have a nested table that contains a sequence ID column. The sequence ID can be any sortable data types. For example, you can use a Web page identifier, an integer, or a text string, as long as the column identifies the events in a sequence. Only one sequence identifier is allowed for each sequence, and only one type of sequence is allowed in each model.  
  
-   **Optional non sequence attributes** The algorithm supports the addition of other attributes that are not related to sequencing. These attributes can include nested columns.  
  
 For example, in the example cited earlier of the [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] Web site, a sequence clustering model might include order information as the case table, demographics about the specific customer for each order as non-sequence attributes, and a nested table containing the sequence in which the customer browsed the site or put items into a shopping cart as the sequence information.  
  
 For more detailed information about the content types and data types supported for sequence clustering models, see the Requirements section of [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md).  
  
## Viewing a Sequence Clustering Model  
 The mining model that this algorithm creates contains descriptions of the most common sequences in the data. To explore the model, you can use the **Microsoft Sequence Cluster Viewer**. When you view a sequence clustering model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] shows you clusters that contain multiple transitions. You can also view pertinent statistics. For more information, see [Browse a Model Using the Microsoft Sequence Cluster Viewer](browse-a-model-using-the-microsoft-sequence-cluster-viewer.md).  
  
 If you want to know more detail, you can browse the model in the [Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md). The content stored for the model includes the distribution for all values in each node, the probability of each cluster, and details about the transitions. For more information, see [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-sequence-clustering-models.md).  
  
## Creating Predictions  
 After the model has been trained, the results are stored as a set of patterns. You can use the descriptions of the most common sequences in the data to predict the next likely step of a new sequence. However, because the algorithm includes other columns, you can use the resulting model to identify relationships between sequenced data and inputs that are not sequential. For example, if you add demographic data to the model, you can make predictions for specific groups of customers. Prediction queries can be customized to return a variable number of predictions, or to return descriptive statistics.  
  
 For information about how to create queries against a data mining model, see [Data Mining Queries](data-mining-queries.md). For examples of how to use queries with a sequence clustering model, see [Sequence Clustering Model Query Examples](clustering-model-query-examples.md).  
  
## Remarks  
  
-   Does not support the use of Predictive Model Markup Language (PMML) to create mining models.  
  
-   Supports drillthrough.  
  
-   Supports the use of OLAP mining models and the creation of data mining dimensions.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)   
 [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md)   
 [Sequence Clustering Model Query Examples](clustering-model-query-examples.md)   
 [Browse a Model Using the Microsoft Sequence Cluster Viewer](browse-a-model-using-the-microsoft-sequence-cluster-viewer.md)  
  
  
