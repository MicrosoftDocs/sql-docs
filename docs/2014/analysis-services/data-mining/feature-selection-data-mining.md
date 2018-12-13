---
title: "Feature Selection (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models [Analysis Services], feature selections"
  - "attributes [data mining]"
  - "feature selection algorithms [Analysis Services]"
  - "data mining [Analysis Services], feature selections"
  - "neural network algorithms [Analysis Services]"
  - "naive bayes algorithms [Analysis Services]"
  - "decision tree algorithms [Analysis Services]"
  - "datasets [Analysis Services]"
  - "clustering algorithms [Analysis Services]"
  - "coding [Data Mining]"
ms.assetid: b044e785-4875-45ab-8ae4-cd3b4e3033bb
author: minewiskan
ms.author: owend
manager: craigg
---
# Feature Selection (Data Mining)
  *Feature selection* is a term commonly used in data mining to describe the tools and techniques available for reducing inputs to a manageable size for processing and analysis. Feature selection implies not only *cardinality reduction*, which means imposing an arbitrary or predefined cutoff on the number of attributes that can be considered when building a model, but also the choice of attributes, meaning that either the analyst or the modeling tool actively selects or discards attributes based on their usefulness for analysis.  
  
 The ability to apply feature selection is critical for effective analysis, because datasets frequently contain far more information than is needed to build the model. For example, a dataset might contain 500 columns that describe the characteristics of customers, but if the data in some of the columns is very sparse you would gain very little benefit from adding them to the model. If you keep the unneeded columns while building the model, more CPU and memory are required during the training process, and more storage space is required for the completed model.  
  
 Even if resources are not an issue, you typically want to remove unneeded columns because they might degrade the quality of discovered patterns, for the following reasons:  
  
-   Some columns are noisy or redundant. This noise makes it more difficult to discover meaningful patterns from the data;  
  
-   To discover quality patterns, most data mining algorithms require much larger training data set on high-dimensional data set. But the training data is very small in some data mining applications.  
  
 If only 50 of the 500 columns in the data source have information that is useful in building a model, you could just leave them out of the model, or you could use feature selection techniques to automatically discover the best features and to exclude values that are statistically insignificant. Feature selection helps solve the twin problems of having too much data that is of little value, or having too little data that is of high value.  
  
## Feature Selection in Analysis Services Data Mining  
 Usually, feature selection is performed automatically in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and each algorithm has a set of default techniques for intelligently applying feature reduction. Feature selection is always performed before the model is trained, to automatically choose the attributes in a dataset that are most likely to be used in the model. However, you can also manually set parameters to influence feature selection behavior.  
  
 In general, feature selection works by calculating a score for each attribute, and then selecting only the attributes that have the best scores. You can also adjust the threshold for the top scores. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides multiple methods for calculating these scores, and the exact method that is applied in any model depends on these factors:  
  
-   The algorithm used in your model  
  
-   The data type of the attribute  
  
-   Any parameters that you may have set on your model  
  
 Feature selection is applied to inputs, predictable attributes, or to states in a column. When scoring for feature selection is complete, only the attributes and states that the algorithm selects are included in the model-building process and can be used for prediction. If you choose a predictable attribute that does not meet the threshold for feature selection the attribute can still be used for prediction, but the predictions will be based solely on the global statistics that exist in the model.  
  
> [!NOTE]  
>  Feature selection affects only the columns that are used in the model, and has no effect on storage of the mining structure. The columns that you leave out of the mining model are still available in the structure, and data in the mining structure columns will be cached.  
  
### Definition of Feature Selection Methods  
 There are many ways to implement feature selection, depending on the type of data that you are working with and the algorithm that you choose for analysis. SQL Server Analysis Services provides several popular and well-established methods for scoring attributes. The method that is applied in any algorithm or data set depends on the data types, and the column usage.  
  
 The *interestingness* score is used to rank and sort attributes in columns that contain nonbinary continuous numeric data.  
  
 *Shannon's entropy* and two *Bayesian* scores are available for columns that contain discrete and discretized data. However, if the model contains any continuous columns, the interestingness score will be used to assess all input columns, to ensure consistency.  
  
 The following section describes each method of feature selection.  
  
#### Interestingness score  
 A feature is interesting if it tells you some useful piece of information. Because the definition of what is useful varies depending on the scenario, the data mining industry has developed various ways to measure *interestingness*. For example, *novelty* might be interesting in outlier detection, but the ability to discriminate between closely related items, or *discriminating weight*, might be more interesting for classification.  
  
 The measure of interestingness that is used in SQL Server Analysis Services is *entropy-based*, meaning that attributes with random distributions have higher entropy and lower information gain; therefore, such attributes are less interesting. The entropy for any particular attribute is compared to the entropy of all other attributes, as follows:  
  
 Interestingness(Attribute) = - (m - Entropy(Attribute)) * (m - Entropy(Attribute))  
  
 Central entropy, or m, means the entropy of the entire feature set. By subtracting the entropy of the target attribute from the central entropy, you can assess how much information the attribute provides.  
  
 This score is used by default whenever the column contains nonbinary continuous numeric data.  
  
#### Shannon's Entropy  
 Shannon's entropy measures the uncertainty of a random variable for a particular outcome. For example, the entropy of a coin toss can be represented as a function of the probability of it coming up heads.  
  
 Analysis Services uses the following formula to calculate Shannon's entropy:  
  
 H(X) = -∑ P(xi) log(P(xi))  
  
 This scoring method is available for discrete and discretized attributes.  
  
#### Bayesian with K2 Prior  
 Analysis Services provides two feature selection scores that are based on Bayesian networks. A Bayesian network is a *directed* or *acyclic* graph of states and transitions between states, meaning that some states are always prior to the current state, some states are posterior, and the graph does not repeat or loop. By definition, Bayesian networks allow the use of prior knowledge. However, the question of which prior states to use in calculating probabilities of later states is important for algorithm design, performance, and accuracy.  
  
 The K2 algorithm for learning from a Bayesian network was developed by Cooper and Herskovits and is often used in data mining. It is scalable and can analyze multiple variables, but requires ordering on variables used as input. For more information, see [Learning Bayesian Networks](https://go.microsoft.com/fwlink/?LinkId=105885) by Chickering, Geiger, and Heckerman.  
  
 This scoring method is available for discrete and discretized attributes.  
  
#### Bayesian Dirichlet Equivalent with Uniform Prior  
 The Bayesian Dirichlet Equivalent (BDE) score also uses Bayesian analysis to evaluate a network given a dataset. The BDE scoring method was developed by Heckerman and is based on the BD metric developed by Cooper and Herskovits. The Dirichlet distribution is a multinomial distribution that describes the conditional probability of each variable in the network, and has many properties that are useful for learning.  
  
 The Bayesian Dirichlet Equivalent with Uniform Prior (BDEU) method assumes a special case of the Dirichlet distribution, in which a mathematical constant is used to create a fixed or uniform distribution of prior states. The BDE score also assumes likelihood equivalence, which means that the data cannot be expected to discriminate equivalent structures. In other words, if the score for If A Then B is the same as the score for If B Then A, the structures cannot be distinguished based on the data, and causation cannot be inferred.  
  
 For more information about Bayesian networks and the implementation of these scoring methods, see [Learning Bayesian Networks](https://go.microsoft.com/fwlink/?LinkId=105885).  
  
### Feature Selection Methods used by Analysis Services Algorithms  
 The following table lists the algorithms that support feature selection, the feature selection methods used by the algorithm, and the parameters that you set to control feature selection behavior:  
  
|Algorithm|Method of analysis|Comments|  
|---------------|------------------------|--------------|  
|Naive Bayes|Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|The Microsoft Naïve Bayes algorithm accepts only discrete or discretized attributes; therefore, it cannot use the interestingness score.<br /><br /> For more information about this algorithm, see [Microsoft Naive Bayes Algorithm Technical Reference](microsoft-naive-bayes-algorithm-technical-reference.md).|  
|Decision trees|Interestingness score<br /><br /> Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|If any columns contain non-binary continuous values, the interestingness score is used for all columns, to ensure consistency. Otherwise, the default feature selection method is used, or the method that you specified when you created the model.<br /><br /> For more information about this algorithm, see [Microsoft Decision Trees Algorithm Technical Reference](microsoft-decision-trees-algorithm-technical-reference.md).|  
|Neural network|Interestingness score<br /><br /> Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|The Microsoft Neural Networks algorithm can use both Bayesian and entropy-based methods, as long as the data contains continuous columns.<br /><br /> For more information about this algorithm, see [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md).|  
|Logistic regression|Interestingness score<br /><br /> Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|Although the Microsoft Logistic Regression algorithm is based on the Microsoft Neural Network algorithm, you cannot customize logistic regression models to control feature selection behavior; therefore, feature selection always default to the method that is most appropriate for the attribute.<br /><br /> If all attributes are discrete or discretized, the default is BDEU.<br /><br /> For more information about this algorithm, see [Microsoft Logistic Regression Algorithm Technical Reference](microsoft-logistic-regression-algorithm-technical-reference.md).|  
|Clustering|Interestingness score|The Microsoft Clustering algorithm can use discrete or discretized data. However, because the score of each attribute is calculated as a distance and is represented as a continuous number, the interestingness score must be used.<br /><br /> For more information about this algorithm, see [Microsoft Clustering Algorithm Technical Reference](microsoft-clustering-algorithm-technical-reference.md).|  
|Linear regression|Interestingness score|The Microsoft Linear Regression algorithm can only use the interestingness score, because it only supports continuous columns.<br /><br /> For more information about this algorithm, see [Microsoft Linear Regression Algorithm Technical Reference](microsoft-linear-regression-algorithm-technical-reference.md).|  
|Association rules<br /><br /> Sequence clustering|Not used|Feature selection is not invoked with these algorithms.<br /><br /> However, you can control the behavior of the algorithm and reduce the size of input data if necessary by setting the value of the parameters MINIMUM_SUPPORT and MINIMUM_PROBABILIITY.<br /><br /> For more information, see [Microsoft Association Algorithm Technical Reference](microsoft-association-algorithm-technical-reference.md) and [Microsoft Sequence Clustering Algorithm Technical Reference](microsoft-sequence-clustering-algorithm-technical-reference.md).|  
|Time series|Not used|Feature selection does not apply to time series models.<br /><br /> For more information about this algorithm, see [Microsoft Time Series Algorithm Technical Reference](microsoft-time-series-algorithm-technical-reference.md).|  
  
## Feature Selection Parameters  
 In algorithms that support feature selection, you can control when feature selection is turned on by using the following parameters. Each algorithm has a default value for the number of inputs that are allowed, but you can override this default and specify the number of attributes. This section lists the parameters that are provided for managing feature selection.  
  
#### MAXIMUM_INPUT_ATTRIBUTES  
 If a model contains more columns than the number that is specified in the *MAXIMUM_INPUT_ATTRIBUTES* parameter, the algorithm ignores any columns that it calculates to be uninteresting.  
  
#### MAXIMUM_OUTPUT_ATTRIBUTES  
 Similarly, if a model contains more predictable columns than the number that is specified in the *MAXIMUM_OUTPUT_ATTRIBUTES* parameter, the algorithm ignores any columns that it calculates to be uninteresting.  
  
#### MAXIMUM_STATES  
 If a model contains more cases than are specified in the *MAXIMUM_STATES* parameter, the least popular states are grouped together and treated as missing. If any one of these parameters is set to 0, feature selection is turned off, affecting processing time and performance.  
  
 In addition to these methods for feature selection, you can improve the ability of the algorithm to identify or promote meaningful attributes by setting *modeling flags* on the model or by setting *distribution flags* on the structure. For more information about these concepts, see [Modeling Flags &#40;Data Mining&#41;](modeling-flags-data-mining.md) and [Column Distributions &#40;Data Mining&#41;](column-distributions-data-mining.md).  
  
## See Also  
 [Customize Mining Models and Structure](customize-mining-models-and-structure.md)  
  
  
