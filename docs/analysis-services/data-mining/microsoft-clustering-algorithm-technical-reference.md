---
title: "Microsoft Clustering Algorithm Technical Reference | Microsoft Docs"
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
# Microsoft Clustering Algorithm Technical Reference
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This section explains the implementation of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm, including the parameters that you can use to control the behavior of clustering models. It also provides guidance about how to improve performance when you create and process clustering models.  
  
 For additional information about how to use clustering models, see the following topics:  
  
-   [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining.md)  
  
-   [Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)  
  
## Implementation of the Microsoft Clustering Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm provides two methods for creating clusters and assigning data points to the clusters. The first, the *K-means* algorithm, is a hard clustering method. This means that a data point can belong to only one cluster, and that a single probability is calculated for the membership of each data point in that cluster. The second method, the *Expectation Maximization* (EM) method, is a *soft clustering* method. This means that a data point always belongs to multiple clusters, and that a probability is calculated for each combination of data point and cluster.  
  
 You can choose which algorithm to use by setting the *CLUSTERING_METHOD* parameter. The default method for clustering is scalable EM.  
  
### EM Clustering  
 In EM clustering, the algorithm iteratively refines an initial cluster model to fit the data and determines the probability that a data point exists in a cluster. The algorithm ends the process when the probabilistic model fits the data. The function used to determine the fit is the log-likelihood of the data given the model.  
  
 If empty clusters are generated during the process, or if the membership of one or more of the clusters falls below a given threshold, the clusters with low populations are reseeded at new points and the EM algorithm is rerun.  
  
 The results of the EM clustering method are probabilistic. This means that every data point belongs to all clusters, but each assignment of a data point to a cluster has a different probability. Because the method allows for clusters to overlap, the sum of items in all the clusters may exceed the total items in the training set. In the mining model results, scores that indicate support are adjusted to account for this.  
  
 The EM algorithm is the default algorithm used in Microsoft clustering models. This algorithm is used as the default because it offers multiple advantages in comparison to k-means clustering:  
  
-   Requires one database scan, at most.  
  
-   Will work despite limited memory (RAM).  
  
-   Has the ability to use a forward-only cursor.  
  
-   Outperforms sampling approaches.  
  
 The Microsoft implementation provides two options: scalable and non-scalable EM. By default, in scalable EM, the first 50,000 records are used to seed the initial scan. If this is successful, the model uses this data only. If the model cannot be fit using 50,000 records, an additional 50,000 records are read. In non-scalable EM, the entire dataset is read regardless of its size. This method might create more accurate clusters, but the memory requirements can be significant. Because scalable EM operates on a local buffer, iterating through the data is much faster, and the algorithm makes much better use of the CPU memory cache than non-scalable EM. Moreover, scalable EM is three times faster than non-scalable EM, even if all the data can fit in main memory. In the majority of cases, the performance improvement does not lead to lower quality of the complete model.  
  
 For a technical report that describes the implementation of EM in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm, see [Scaling EM (Expectation Maximization) Clustering to Large Databases](http://go.microsoft.com/fwlink/?LinkId=45964).  
  
### K-Means Clustering  
 K-means clustering is a well-known method of assigning cluster membership by minimizing the differences among items in a cluster while maximizing the distance between clusters. The "means" in k-means refers to the *centroid* of the cluster, which is a data point that is chosen arbitrarily and then refined iteratively until it represents the true mean of all data points in the cluster. The "k" refers to an arbitrary number of points that are used to seed the clustering process. The k-means algorithm calculates the squared Euclidean distances between data records in a cluster and the vector that represents the cluster mean, and converges on a final set of k clusters when that sum reaches its minimum value.  
  
 The k-means algorithm assigns each data point to exactly one cluster, and does not allow for uncertainty in membership. Membership in a cluster is expressed as a distance from the centroid.  
  
 Typically, the k-means algorithm is used for creating clusters of continuous attributes, where calculating distance to a mean is straightforward. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] implementation adapts the k-means method to cluster discrete attributes, by using probabilities.  For discrete attributes, the distance of a data point from a particular cluster is calculated as follows:  
  
 1 - P(data point, cluster)  
  
> [!NOTE]  
>  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm does not expose the distance function used in computing k-means, and measures of distance are not available in the completed model. However, you can use a prediction function to return a value that corresponds to distance, where distance is computed as the probability of a data point belonging to the cluster. For more information, see [ClusterProbability &#40;DMX&#41;](../../dmx/clusterprobability-dmx.md).  
  
 The k-means algorithm provides two methods of sampling the data set: non-scalable K-means, which loads the entire data set and makes one clustering pass, or scalable k-means, where the algorithm uses the first 50,000 cases and reads more cases only if it needs more data to achieve a good fit of model to data.  
  
### Updates to the Microsoft Clustering Algorithm in SQL Server 2008  
 In SQL Server 2008, the default configuration of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] clustering algorithm was changed to use the internal parameter, NORMALIZATION = 1. Normalization is performed using z-score statistics, and assumes normal distribution. The intent of this change in the default behavior is to minimize the effect of attributes that might have large magnitudes and many outliers. However, z-score normalization may alter the clustering results on distributions that are not normal (such as uniform distributions). To prevent normalization and obtain the same behavior as the K-means clustering algorithm in SQL Server 2005, you can use the **Parameter Settings** dialog box to add the custom parameter, NORMALIZATION, and set its value to 0.  
  
> [!NOTE]  
>  The NORMALIZATION parameter is an internal property of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm and is not supported. In general, the use of normalization is recommended in clustering models to improve model results.  
  
## Customizing the Microsoft Clustering Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm supports several parameters that affect the behavior, performance, and accuracy of the resulting mining model.  
  
### Setting Algorithm Parameters  
 The following table describes the parameters that can be used with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm. These parameters affect both the performance and accuracy of the resulting mining model.  
  
 CLUSTERING_METHOD  
 Specifies the clustering method for the algorithm to use. The following clustering methods are available:  
  
|ID|Method|  
|--------|------------|  
|1|Scalable EM|  
|2|Non-scalable EM|  
|3|Scalable K-Means|  
|4|Non-scalable K-Means.|  
  
 The default is 1 (scalable EM).  
  
 CLUSTER_COUNT  
 Specifies the approximate number of clusters to be built by the algorithm. If the approximate number of clusters cannot be built from the data, the algorithm builds as many clusters as possible. Setting the CLUSTER_COUNT to 0 causes the algorithm to use heuristics to best determine the number of clusters to build.  
  
 The default is 10.  
  
 CLUSTER_SEED  
 Specifies the seed number that is used to randomly generate clusters for the initial stage of model building.  
  
 By changing this number, you can change the way that the initial clusters are built, and then compare models that have been built using different seeds. If the seed is changed but the clusters that are found do not change greatly , the model can be considered relatively stable.  
  
 The default is 0.  
  
 MINIMUM_SUPPORT  
 Specifies the minimum number of cases that are required to build a cluster. If the number of cases in the cluster is lower than this number, the cluster is treated as empty and discarded.  
  
 If you set this number too high, you may miss valid clusters.  
  
> [!NOTE]  
>  If you use EM, which is the default clustering method, some clusters may have a support value that is lower than the specified value. This is because each case is evaluated for its membership in all possible clusters, and for some clusters there may be only minimal support.  
  
 The default is 1.  
  
 MODELLING_CARDINALITY  
 Specifies the number of sample models that are constructed during the clustering process.  
  
 Reducing the number of candidate models can improve performance at the risk of missing some good candidate models.  
  
 The default is 10.  
  
 STOPPING_TOLERANCE  
 Specifies the value that is used to determine when convergence is reached and the algorithm is finished building the model. Convergence is reached when the overall change in cluster probabilities is less than the ratio of the STOPPING_TOLERANCE parameter divided by the size of the model.  
  
 The default is 10.  
  
 SAMPLE_SIZE  
 Specifies the number of cases that the algorithm uses on each pass if the CLUSTERING_METHOD parameter is set to one of the scalable clustering methods. Setting the SAMPLE_SIZE parameter to 0 will cause the whole dataset to be clustered in a single pass. Loading the entire dataset in a single pass can cause memory and performance issues.  
  
 The default is 50000.  
  
 MAXIMUM_INPUT_ATTRIBUTES  
 Specifies the maximum number of input attributes that the algorithm can handle before it invokes feature selection. Setting this value to 0 specifies that there is no maximum number of attributes.  
  
 Increasing the number of attributes can significantly degrade performance.  
  
 The default is 255.  
  
 MAXIMUM_STATES  
 Specifies the maximum number of attribute states that the algorithm supports. If an attribute has more states than the maximum, the algorithm uses the most popular states and ignores the remaining states.  
  
 Increasing the number of states can significantly degrade performance.  
  
 The default is 100.  
  
### Modeling Flags  
 The algorithm supports the following modeling flags. You define modeling flags when you create the mining structure or mining model. The modeling flags specify how values in each column are handled during analysis.  
  
|Modeling Flag|Description|  
|-------------------|-----------------|  
|MODEL_EXISTENCE_ONLY|The column will be treated as having two possible states: Missing and Existing. A null is a missing value.<br /><br /> Applies to mining model column.|  
|NOT NULL|The column cannot contain a null. An error will result if Analysis Services encounters a null during model training.<br /><br /> Applies to mining structure column.|  
  
## Requirements  
 A clustering model must contain a key column and input columns. You can also define input columns as being predictable. Columns set to **Predict Only** are not used to build clusters. The distribution of these values in the clusters are calculated after the clusters are built.  
  
### Input and Predictable Columns  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering algorithm supports the specific input columns and predictable columns that are listed in the following table. For more information about what the content types mean when used in a mining model, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
|Column|Content types|  
|------------|-------------------|  
|Input attribute|Continuous, Cyclical, Discrete, Discretized, Key, Table, Ordered|  
|Predictable attribute|Continuous, Cyclical, Discrete, Discretized, Table, Ordered|  
  
> [!NOTE]  
>  Cyclical and Ordered content types are supported, but the algorithm treats them as discrete values and does not perform special processing.  
  
## See Also  
 [Microsoft Clustering Algorithm](../../analysis-services/data-mining/microsoft-clustering-algorithm.md)   
 [Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)   
 [Mining Model Content for Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-clustering-models-analysis-services-data-mining.md)  
  
  
