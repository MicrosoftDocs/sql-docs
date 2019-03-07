---
title: "Mining Model Content for Clustering Models (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "nearest neighbor [Data Mining]"
  - "clustering [Data Mining]"
  - "mining model content, clustering models"
  - "clustering algorithms [Analysis Services]"
ms.assetid: aed1b7d3-8f20-4eeb-b156-0229f942cefd
author: minewiskan
ms.author: owend
manager: craigg
---
# Mining Model Content for Clustering Models (Analysis Services - Data Mining)
  This topic describes mining model content that is specific to models that use the Microsoft Clustering algorithm. For a general explanation of mining model content for all model types, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
## Understanding the Structure of a Clustering Model  
 A clustering model has a simple structure. Each model has a single parent node that represents the model and its metadata, and each parent node has a flat list of clusters (NODE_TYPE = 5). This organization is shown in the following image.  
  
 ![structure of model content for clustering](../media/modelcontentstructure-clust.gif "structure of model content for clustering")  
  
 Each child node represents a single cluster and contains detailed statistics about the attributes of the cases in that cluster. This includes a count of the number of cases in the cluster, and the distribution of values that distinguish the cluster from other clusters.  
  
> [!NOTE]  
>  You do not need to iterate through the nodes to get a count or description of the clusters; the model parent node also counts and lists the clusters.  
  
 The parent node contains useful statistics that describe the actual distribution of all the training cases. These statistics are found in the nested table column, NODE_DISTRIBUTION. For example, the following table shows several rows from the NODE_DISTRIBUTION table that describe the distribution of customer demographics for the clustering model, `TM_Clustering`, that you create in the [Basic Data Mining Tutorial](../../tutorials/basic-data-mining-tutorial.md):  
  
|ATTRIBUTE_NAME|ATRIBUTE_VALUE|SUPPORT|PROBABILITY|VARIANCE|VALUE_TYPE|  
|---------------------|---------------------|-------------|-----------------|--------------|-----------------|  
|Age|Missing|0|0|0|1 (Missing)|  
|Age|44.9016152716593|12939|1|125.663453102554|3 (Continuous)|  
|Gender|Missing|0|0|0|1 (Missing)|  
|Gender|F|6350|0.490764355823479|0|4 (Discrete)|  
|Gender|M|6589|0.509235644176521|0|4 (Discrete)|  
  
 From these results, you can see that there were 12939 cases used to build the model, that the ratio of males to females was about 50-50, and that the mean age was 44. The descriptive statistics vary depending on whether the attribute being reported is a continuous numeric data type, such as age, or a discrete value type, such as gender. The statistical measures *mean* and *variance* are computed for continuous data types, whereas *probability* and *support* are computed for discrete data types.  
  
> [!NOTE]  
>  The variance represents the total variance for the cluster. When the value for variance is small, it indicates that most values in the column were fairly close to the mean. To obtain the standard deviation, calculate the square root of the variance.  
  
 Note that for each of the attributes there is a `Missing` value type that tells you how many cases had no data for that attribute. Missing data can be significant and affects calculations in different ways, depending on the data type. For more information, see [Missing Values &#40;Analysis Services - Data Mining&#41;](missing-values-analysis-services-data-mining.md).  
  
## Model Content for a Clustering Model  
 This section provides detail and examples only for those columns in the mining model content that are relevant for clustering models.  
  
 For information about the general-purpose columns in the schema rowset, such as MODEL_CATALOG and MODEL_NAME, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 Always blank in clustering models because there is no predictable attribute in the mode.  
  
 NODE_NAME  
 Always same as NODE_UNIQUE_NAME.  
  
 NODE_UNIQUE_NAME  
 A unique identifier for the node within the model. This value cannot be changed.  
  
 NODE_TYPE  
 A clustering model outputs the following node types:  
  
|Node ID and Name|Description|  
|----------------------|-----------------|  
|1 (Model)|Root node for model.|  
|5 (Cluster)|Contains a count of cases in the cluster, the characteristics of cases in the cluster, and statistics that describe the values in the cluster.|  
  
 NODE_CAPTION  
 A friendly name for display purposes. When you create a model, the value of NODE_UNIQUE_NAME is automatically used as the caption. However, you can change the value for NODE_CAPTION to update the display name for the cluster, either programmatically or by using the viewer.  
  
> [!NOTE]  
>  When you reprocess the model, all name changes will be overwritten by the new values. You cannot persist names in the model, or track changes in cluster membership between different versions of a model.  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
 **Parent node** Indicates the number of clusters in the model.  
  
 **Cluster nodes** Always 0.  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent.  
  
 **Parent node** Always NULL  
  
 **Cluster nodes** Usually 000.  
  
 NODE_DESCRIPTION  
 A description of the node.  
  
 **Parent node** Always **(All)**.  
  
 **Cluster nodes** A comma-separated list of the primary attributes that distinguish the cluster from other clusters.  
  
 NODE_RULE  
 Not used for clustering models.  
  
 MARGINAL_RULE  
 Not used for clustering models.  
  
 NODE_PROBABILITY  
 The probability associated with this node. **Parent node** Always 1.  
  
 **Cluster nodes** The probability represents the compound probability of the attributes, with some adjustments depending on the algorithm used to create the clustering model.  
  
 MARGINAL_PROBABILITY  
 The probability of reaching the node from the parent node. In a clustering model, the marginal probability is always the same as the node probability.  
  
 NODE_DISTRIBUTION  
 A table that contains the probability histogram of the node.  
  
 **Parent node** See the Introduction to this topic.  
  
 **Cluster nodes** Represents the distribution of attributes and values for cases that are included in this cluster.  
  
 NODE_SUPPORT  
 The number of cases that support this node. **Parent node** Indicates the number of training cases for the entire model.  
  
 **Cluster nodes** Indicates the size of the cluster as a number of cases.  
  
 **Note** If the model uses K-Means clustering, each case can belong to only one cluster. However, if the model uses EM clustering, each case can belong to different cluster, and the case is assigned a weighted distance for each cluster to which it belongs. Therefore, for EM models the sum of support for an individual cluster is greater than support for the overall model.  
  
 MSOLAP_MODEL_COLUMN  
 Not used for clustering models.  
  
 MSOLAP_NODE_SCORE  
 Displays a score associated with the node.  
  
 **Parent node** The Bayesian Information Criterion (BIC) score for the clustering model.  
  
 **Cluster nodes** Always 0.  
  
 MSOLAP_NODE_SHORT_CAPTION  
 A label used for display purposes. You cannot change this caption.  
  
 **Parent node** The type of model: Cluster model  
  
 **Cluster nodes** The name of the cluster. Example: Cluster 1.  
  
## Remarks  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides multiple methods for creating a clustering model. If you do not know which method was used to create the model that you are working with, you can retrieve the model metadata programmatically, by using an ADOMD client or AMO, or by querying the data mining schema rowset. For more information, see [Query the Parameters Used to Create a Mining Model](query-the-parameters-used-to-create-a-mining-model.md).  
  
> [!NOTE]  
>  The structure and content of the model stay the same, regardless of which clustering method or parameters you use.  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md)   
 [Data Mining Model Viewers](data-mining-model-viewers.md)   
 [Microsoft Clustering Algorithm](microsoft-clustering-algorithm.md)   
 [Data Mining Queries](data-mining-queries.md)  
  
  
