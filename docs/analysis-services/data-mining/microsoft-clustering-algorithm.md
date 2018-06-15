---
title: "Microsoft Clustering Algorithm | Microsoft Docs"
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
# Microsoft Clustering Algorithm
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm is a *segmentation* or *clustering* algorithm that iterates over cases in a dataset to group them into clusters that contain similar characteristics. These groupings are useful for exploring data, identifying anomalies in the data, and creating predictions.  
  
 Clustering models identify relationships in a dataset that you might not logically derive through casual observation. For example, you might easily guess that people who commute to their jobs by bicycle do not typically live a long distance from where they work. The algorithm, however, can find other characteristics about bicycle commuters that are not as obvious. In the following diagram, cluster A represents data about people who tend to drive to work, while cluster B represents data about people who tend to ride bicycles to work.  
  
 ![Cluster pattern of commuter tendencies](../../analysis-services/data-mining/media/clustering-example.gif "Cluster pattern of commuter tendencies")  
  
 The clustering algorithm differs from other data mining algorithms, such as the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm, in that you do not have to designate a predictable column to be able to build a clustering model. The clustering algorithm trains the model strictly from the relationships that exist in the data and from the clusters that the algorithm identifies.  
  
## Example  
 Consider a group of people who share similar demographic information and who buy similar products from the [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] company. This group of people represents a cluster of data. Several such clusters may exist in a database. By observing the columns that make up a cluster, you can more clearly see how records in a dataset are related to one another.  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm first identifies relationships in a dataset and generates a series of clusters based on those relationships. A scatter plot is a useful way to visually represent how the algorithm groups data, as shown in the following diagram. The scatter plot represents all the cases in the dataset, and each case is a point on the graph. The clusters group points on the graph and illustrate the relationships that the algorithm identifies.  
  
 ![Scatter plot of cases in a dataset](../../analysis-services/data-mining/media/clustering-plot.gif "Scatter plot of cases in a dataset")  
  
 After first defining the clusters, the algorithm calculates how well the clusters represent groupings of the points, and then tries to redefine the groupings to create clusters that better represent the data. The algorithm iterates through this process until it cannot improve the results more by redefining the clusters.  
  
 You can customize the way the algorithm works by selecting a specifying a clustering technique, limiting the maximum number of clusters, or changing the amount of support required to create a cluster. For more information, see [Microsoft Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-clustering-algorithm-technical-reference.md). this algorithm includes two popular clustering methods: K-means clustering and the Expectation Maximization method.  
  
## Data Required for Clustering Models  
 When you prepare data for use in training a clustering model, you should understand the requirements for the particular algorithm, including how much data is needed, and how the data is used.  
  
 The requirements for a clustering model are as follows:  
  
-   **A single key column** Each model must contain one numeric or text column that uniquely identifies each record. Compound keys are not allowed.  
  
-   **Input columns** Each model must contain at least one input column that contains the values that are used to build the clusters. You can have as many input columns as you want, but depending on the number of values in each column, the addition of extra columns can increase the time it takes to train the model.  
  
-   **Optional predictable column** The algorithm does not need a predictable column to build the model, but you can add a predictable column of almost any data type. The values of the predictable column can be treated as input to the clustering model, or you can specify that it be used for prediction only. For example, if you want to predict customer income by clustering on demographics such as region or age, you would specify income as **PredictOnly** and add all the other columns, such as region or age, as inputs.  
  
 For more detailed information about the content types and data types supported for clustering models, see the Requirements section of [Microsoft Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-clustering-algorithm-technical-reference.md).  
  
## Viewing a Clustering Model  
 To explore the model, you can use the **Microsoft Cluster Viewer**. When you view a clustering model, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] shows you the clusters in a diagram that depicts the relationships among clusters, and also provides a detailed profile of each cluster, a list of the attributes that distinguish each cluster from the others, and the characteristics of the entire training data set. For more information, see [Browse a Model Using the Microsoft Cluster Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-cluster-viewer.md).  
  
 If you want to know more detail, you can browse the model in the [Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md). The content stored for the model includes the distribution for all values in each node, the probability of each cluster, and other information. For more information, see [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining.md).  
  
## Creating Predictions  
 After the model has been trained, the results are stored as a set of patterns, which you can explore or use to make predictions.  
  
 You can create queries to return predictions about whether new data fits into the clusters that were discovered, or to obtain descriptive statistics about the clusters.  
  
 For information about how to create queries against a data mining model, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md). For examples of how to use queries with a clustering model, see [Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md).  
  
## Remarks  
  
-   Supports the use of Predictive Model Markup Language (PMML) to create mining models.  
  
-   Supports drillthrough.  
  
-   Supports the use of OLAP mining models and the creation of data mining dimensions.  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Microsoft Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-clustering-algorithm-technical-reference.md)   
 [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining.md)   
 [Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)  
  
  
