---
title: "Microsoft Sequence Clustering Algorithm Technical Reference | Microsoft Docs"
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
# Microsoft Sequence Clustering Algorithm Technical Reference
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The Microsoft Sequence Clustering algorithm is a hybrid algorithm that uses Markov chain analysis to identify ordered sequences, and combines the results of this analysis with clustering techniques to generate clusters based on the sequences and other attributes in the model. This topic describes the implementation of the algorithm, how to customize the algorithm, and special requirements for sequence clustering models.  
  
 For more general information about the algorithm, including how to browse and query sequence clustering models, see [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md).  
  
## Implementation of the Microsoft Sequence Clustering Algorithm  
 The Microsoft Sequence Clustering model uses Markov models to identify sequences and determine the probability of sequences. A Markov model is a directed graph that stores the transitions between different states. The Microsoft Sequence Clustering algorithm uses n-order Markov chains, not a Hidden Markov model.  
  
 The number of orders in a Markov chain tells you how many states are used to determine the probability of the current states. In a first-order Markov model, the probability of the current state depends only on the previous state. In a second-order Markov chain, the probability of a state depends on the previous two states, and so forth. For each Markov chain, a transition matrix stores the transitions for each combination of states. As the length of the Markov chain increases, the size of the matrix also increases exponentially, and the matrix becomes extremely sparse. Processing time also increases proportionally.  
  
 It might be helpful to visualize the chain by using the example of clickstream analysis, which analyzes visits to Web pages on a site. Each user creates a long sequence of clicks for each session. When you create a model to analyze user behavior on a Web site, the data set used for training is a sequence of URLs, converted to a graph that includes the count of all instances of the same click path. For example, the graph contains the probability that the user moves from page 1 to page 2 (10%), the probability that the user moves from page 1 to page 3 (20%), and so forth. When you put all the possible paths and pieces of the paths together, you obtain a graph that might be much longer and more complex than any single observed path.  
  
 By default, the Microsoft Sequence Clustering algorithm uses the Expectation Maximization (EM) method of clustering. For more information, see [Microsoft Clustering Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-clustering-algorithm-technical-reference.md).  
  
 The targets of clustering are both the sequential and nonsequential attributes. Each cluster is randomly selected using a probability distribution. Each cluster has a Markov chain that represents the complete set of paths, and a matrix that contains the sequence state transitions and probabilities. Based on the initial distribution, Bayes rule is used to calculate the probability of any attribute, including a sequence, in a specific cluster.  
  
 The Microsoft Sequence Clustering algorithm supports the additional of nonsequential attributes to the model. This means that these additional attributes are combined with the sequence attributes to create clusters of cases with similar attributes, just like in a typical clustering model.  
  
 A sequence clustering model tends to create many more clusters than a typical clustering model. Therefore, the Microsoft Sequence Clustering algorithm performs *cluster decomposition*to separate clusters based on sequences and other attributes.  
  
### Feature Selection in a Sequence Clustering Model  
 Feature selection is not invoked when building sequences; however, feature selection applies at the clustering stage.  
  
|Model type|Feature Selection Method|Comments|  
|----------------|------------------------------|--------------|  
|Sequence Clustering|Not used|Feature selection is not invoked; however, you can control the behavior of the algorithm by setting the value of the parameters MINIMUM_SUPPORT and MINIMUM_PROBABILIITY.|  
|Clustering|Interestingness score|Although the clustering algorithm may use discrete or discretized algorithms, the score of each attribute is calculated as a distance and is continuous; therefore the interestingness score is used.|  
  
 For more information, see [Feature Selection](http://msdn.microsoft.com/library/73182088-153b-4634-a060-d14d1fd23b70).  
  
### Optimizing Performance  
 The Microsoft Sequence Clustering algorithm supports various ways to optimize processing:  
  
-   Controlling the number of clusters generated, by setting a value for the CLUSTER_COUNT parameter.  
  
-   Reducing the number of sequences included as attributes, by increasing the value of the MINIMUM_SUPPORT parameter. As a result, rare sequences are eliminated.  
  
-   Reducing complexity before processing the model, by grouping related attributes.  
  
 In general, you can optimize the performance of an n-order Markov chain mode in several ways:  
  
-   Controlling the length of the possible sequences.  
  
-   Programmatically reducing the value of n.  
  
-   Storing only probabilities that exceed a specified threshold.  
  
 A complete discussion of these methods is beyond the scope of this topic.  
  
## Customizing the Sequence Clustering Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm supports parameters that affect the behavior, performance, and accuracy of the resulting mining model. You can also modify the behavior of the completed model by setting modeling flags that control the way the algorithm processes training data.  
  
### Setting Algorithm Parameters  
 The following table describes the parameters that can be used with the Microsoft Sequence Clustering algorithm.  
  
 CLUSTER_COUNT  
 Specifies the approximate number of clusters to be built by the algorithm. If the approximate number of clusters cannot be built from the data, the algorithm builds as many clusters as possible. Setting the CLUSTER_COUNT parameter to 0 causes the algorithm to use heuristics to best determine the number of clusters to build.  
  
 The default is 10.  
  
> [!NOTE]  
>  Specifying a non-zero number acts as a hint to the algorithm, which proceeds with the goal of finding the specified number, but may end up finding more or less.  
  
 MINIMUM_SUPPORT  
 Specifies the minimum number of cases that is required in support of an attribute to create a cluster.  
  
 The default is 10.  
  
 MAXIMUM_SEQUENCE_STATES  
 Specifies the maximum number of states that a sequence can have.  
  
 Setting this value to a number greater than 100 may cause the algorithm to create a model that does not provide meaningful information.  
  
 The default is 64.  
  
 MAXIMUM_STATES  
 Specifies the maximum number of states for a non-sequence attribute that the algorithm supports. If the number of states for a non-sequence attribute is greater than the maximum number of states, the algorithm uses the attribute's most popular states and treats the remaining states as **Missing**.  
  
 The default is 100.  
  
### Modeling Flags  
 The following modeling flags are supported for use with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm.  
  
 NOT NULL  
 Indicates that the column cannot contain a null. An error will result if Analysis Services encounters a null during model training.  
  
 Applies to the mining structure column.  
  
 MODEL_EXISTENCE_ONLY  
 Means that the column will be treated as having two possible states: **Missing** and **Existing**. A null is treated as a **Missing** value.  
  
 Applies to the mining model column.  
  
 For more information about the use of Missing values in mining models, and how missing values affect probability scores, see [Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md).  
  
## Requirements  
 The case table must have a case ID column. Optionally the case table can contain other columns that store attributes about the case.  
  
 The Microsoft Sequence Clustering algorithm requires sequence information, stored as a nested table. The nested table must have a single Key Sequence column. A **Key Sequence** column can contain any type of data that can be sorted, including string data types, but the column must contain unique values for each case. Moreover, before you process the model, you must ensure that both the case table and the nested table are sorted in ascending order on the key that relates the tables.  
  
> [!NOTE]  
>  If you create a model that uses the Microsoft Sequence algorithm but do not use a sequence column, the resulting model will not contain any sequences, but will simply cluster cases based on other attributes that are included in the model.  
  
### Input and Predictable Columns  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm supports the specific input columns and predictable columns that are listed in the following table. For more information about what the content types mean when used in a mining model, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
|Column|Content types|  
|------------|-------------------|  
|Input attribute|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Table, and Ordered|  
|Predictable attribute|Continuous, Cyclical, Discrete, Discretized, Table, and Ordered|  
  
## Remarks  
  
-   Use the [PredictSequence &#40;DMX&#41;](../../dmx/predictsequence-dmx.md) function for Prediction of Sequences. For more information about the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that support Sequence Prediction, see [Features Supported by the Editions of SQL Server 2012](http://go.microsoft.com/fwlink/?linkid=232473) (http://go.microsoft.com/fwlink/?linkid=232473).  
  
-   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm does not support using the Predictive Model Markup Language (PMML) to create mining models.  
  
-   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Sequence Clustering algorithm supports drillthrough, the use of OLAP mining models, and the use of data mining dimensions.  
  
## See Also  
 [Microsoft Sequence Clustering Algorithm](../../analysis-services/data-mining/microsoft-sequence-clustering-algorithm.md)   
 [Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md)   
 [Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md)  
  
  
