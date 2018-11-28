---
title: "Mining Model Content (Analysis Services - Data Mining) | Microsoft Docs"
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
# Mining Model Content (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  After you have designed and processed a mining model using data from the underlying mining structure, the mining model is complete and contains *mining model content*. You can use this content to make predictions or analyze your data.  
  
 Mining model content includes metadata about the model, statistics about the data, and patterns discovered by the mining algorithm. Depending on the algorithm that was used, the model content may include regression formulas, the definitions of rules and itemsets, or weights and other statistics.  
  
 Regardless of the algorithm that was used, mining model content is presented in a standard structure. You can browse the structure in the Microsoft Generic Content Tree Viewer, provided in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and then switch to one of the custom viewers to see how the information is interpreted and displayed graphically for each model type. You can also create queries against the mining model content by using any client that supports the MINING_MODEL_CONTENT schema rowset. For more information, see [Data Mining Query Tasks and How-tos](../../analysis-services/data-mining/data-mining-query-tasks-and-how-tos.md).  
  
 This section describes the basic structure of the content provided for all kinds of mining models. It describes the node types that are common to all mining model content, and provides guidance on how to interpret the information.  
  
 [Structure of Mining Model Content](#bkmk_Structure)  
  
 [Nodes in the Model Content](#bkmk_Nodes)  
  
 [Mining Model Content by Algorithm Type](#bkmk_AlgoType)  
  
 [Tools for Viewing Mining Model Content](#bkmk_Viewing)  
  
 [Tools for Querying Mining Model Content](#bkmk_Querying)  
  
##  <a name="bkmk_Structure"></a> Structure of Mining Model Content  
 The content of each model is presented as a series of *nodes*. A node is an object within a mining model that contains metadata and information about a portion of the model. Nodes are arranged in a hierarchy. The exact arrangement of nodes in the hierarchy, and the meaning of the hierarchy, depends on the algorithm that you used. For example, if you create a decision trees model, the model can contain multiple trees, all connected to the model root; if you create a neural network model, the model may contain one or more networks, plus a statistics node.  
  
 The first node in each model is called the *root node*, or the *model parent* node. Every model has a root node (NODE_TYPE = 1). The root node typically contains some metadata about the model, and the number of child nodes, but little additional information about the patterns discovered by the model.  
  
 Depending on which algorithm you used to create the model, the root node has a varying number of child nodes. Child nodes have different meanings and contain different content, depending on the algorithm and the depth and complexity of the data.  
  
##  <a name="bkmk_Nodes"></a> Nodes in Mining Model Content  
 In a mining model, a node is a general-purpose container that stores a piece of information about all or part of the model. The structure of each node is always the same, and contains the columns defined by the data mining schema rowset. For more information, see [DMSCHEMA_MINING_MODEL_CONTENT Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/data-mining/dmschema-mining-model-content-rowset).  
  
 Each node includes metadata about the node, including an identifier that is unique within each model, the ID of the parent node, and the number of child nodes that the node has. The metadata identifies the model to which the node belongs, and the database catalog where that particular model is stored. Additional content provided in the node differs depending on the type of algorithm you used to create the model, and might include the following:  
  
-   Count of cases in the training data that supports a particular predicted value.  
  
-   Statistics, such as mean, standard deviation, or variance.  
  
-   Coefficients and formulas.  
  
-   Definition of rules and lateral pointers.  
  
-   XML fragments that describe a portion of the model.  
  
### List of Mining Content Node Types  
 The following table lists the different types of nodes that are output in data mining models. Because each algorithm processes information differently, each model generates only a few specific kinds of nodes. If you change the algorithm, the type of nodes may change. Also, if you reprocess the model, the content of each node may change.  
  
> [!NOTE]  
>  If you use a different data mining service, or if you create your own plug-in algorithms, additional custom node types may be available.  
  
|NODE_TYPE ID|Node Label|Node Contents|  
|-------------------|----------------|-------------------|  
|1|Model|Metadata and root content node. Applies to all model types.|  
|2|Tree|Root node of a classification tree. Applies to decision tree models.|  
|3|Interior|Interior split node in a tree. Applies to decision tree models.|  
|4|Distribution|Terminal node of a tree. Applies to decision tree models.|  
|5|Cluster|Cluster detected by the algorithm. Applies to clustering models and sequence clustering models.|  
|6|Unknown|Unknown node type.|  
|7|ItemSet|Itemset detected by the algorithm. Applies to association models or sequence clustering models.|  
|8|AssociationRule|Association rule detected by the algorithm. Applies to association models or sequence clustering models.|  
|9|PredictableAttribute|Predictable attribute. Applies to all model types.|  
|10|InputAttribute|Input attribute. Applies to decision trees and Naïve Bayes models.|  
|11|InputAttributeState|Statistics about the states of an input attribute. Applies to decision trees and Naïve Bayes models.|  
|13|Sequence|Top node for a Markov model component of a sequence cluster. Applies to sequence clustering models.|  
|14|Transition|Markov transition matrix. Applies to sequence clustering models.|  
|15|TimeSeries|Non-root node of a time series tree. Applies to time series models only.|  
|16|TsTree|Root node of a time series tree that corresponds to a predictable time series. Applies to time series models, and only if the model was created using the MIXED parameter.|  
|17|NNetSubnetwork|One sub-network. Applies to neural network models.|  
|18|NNetInputLayer|Group that contains the nodes of the input layer. Applies to neural network models.|  
|19|NNetHiddenLayer|Groups that contains the nodes that describe the hidden layer. Applies to neural network models.|  
|21|NNetOutputLayer|Groups that contains the nodes of the output layer. Applies to neural network models.|  
|21|NNetInputNode|Node in the input layer that matches an input attribute with the corresponding states. Applies to neural network models.|  
|22|NNetHiddenNode|Node in the hidden layer. Applies to neural network models.|  
|23|NNetOutputNode|Node in the output layer. This node will usually match an output attribute and the corresponding states. Applies to neural network models.|  
|24|NNetMarginalNode|Marginal statistics about the training set. Applies to neural network models.|  
|25|RegressionTreeRoot|Root of a regression tree. Applies to linear regression models and to decision trees models that contains continuous input attributes.|  
|26|NaiveBayesMarginalStatNode|Marginal statistics about the training set. Applies to Naïve Bayes models.|  
|27|ArimaRoot|Root node of an ARIMA model. Applies to only those time series models that use the ARIMA algorithm.|  
|28|ArimaPeriodicStructure|A periodic structure in an ARIMA model. Applies to only those time series models that use the ARIMA algorithm.|  
|29|ArimaAutoRegressive|Autoregressive coefficient for a single term in an ARIMA model.<br /><br /> Applies to only those time series models that use the ARIMA algorithm.|  
|30|ArimaMovingAverage|Moving average coefficient for a single term in an ARIMA model. Applies to only those time series models that use the ARIMA algorithm.|  
|1000|CustomBase|Starting point for custom node types. Custom node types must be integers greater in value than this constant. Applies to models created by using custom plug-in algorithms.|  
  
### Node ID, Name, Caption and Description  
 The root node of any model always has the unique ID (**NODE_UNIQUE_NAME**) of 0. All node IDs are assigned automatically by Analysis Services and cannot be modified.  
  
 The root node for each model also contains some basic metadata about the model. This metadata includes the Analysis Services database where the model is stored (**MODEL_CATALOG**), the schema (**MODEL_SCHEMA)**, and the name of the model (**MODEL_NAME)**. However, this information is repeated in all the nodes of the model, so you do not need to query the root node to get this metadata.  
  
 In addition to a name used as the unique identifier, each node has a *name* (**NODE_NAME**). This name is automatically created by the algorithm for display purposes and cannot be edited.  
  
> [!NOTE]  
>  The Microsoft Clustering algorithm allows users to assign friendly names to each cluster. However, these friendly names are not persisted on the server, and if you reprocess the model, the algorithm will generate new cluster names.  
  
 The *caption* and *description* for each node are automatically generated by the algorithm, and serve as labels to help you understand the content of the node. The text generated for each field depends on the model type. In some cases, the name, caption, and description may contain exactly the same string, but in some models, the description may contain additional information. See the topic about the individual model type for details of the implementation.  
  
> [!NOTE]  
>  Analysis Services server supports the renaming of nodes only if you build models by using a custom plug-in algorithm that implements renaming,. To enable renaming, you must override the methods when you create the plug-in algorithm.  
  
### Node Parents, Node Children, and Node Cardinality  
 The relationship between parent and child nodes in a tree structure is determined by the value of the PARENT_UNIQUE_NAME column. This value is stored in the child node and tells you the ID of the parent node. Some examples follow of how this information might be used:  
  
-   A PARENT_UNIQUE_NAME that is NULL means that the node is the top node of the model.  
  
-   If the value of PARENT_UNIQUE_NAME is 0, the node must be a direct descendant of the top node in the model. This is because the ID of the root node is always 0.  
  
-   You can use functions within a Data Mining Extensions (DMX) query to find descendants or parents of a particular node. For more information about using functions in queries, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
 *Cardinality* refers to the number of items in a set. In the context of a processed mining model, cardinality tells you the number of children in a particular node. For example, if a decision tree model has a node for [Yearly Income], and that node has two child nodes, one for the condition [Yearly Income] = High and one for the condition, [Yearly Income] = Low, the value of CHILDREN_CARDINALITY for the [Yearly Income] node would be 2.  
  
> [!NOTE]  
>  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], only the immediate child nodes are counted when calculating the cardinality of a node. However, if you create a custom plug-in algorithm, you can overload CHILDREN_CARDINALITY to count cardinality differently. This may be useful, for example, if you wanted to count the total number of descendants, not just the immediate children.  
  
 Although cardinality is counted in the same way for all models, how you interpret or use the cardinality value differs depending on the model type. For example, in a clustering model, the cardinality of the top node tells you the total number of clusters that were found. In other types of models, cardinality may always have a set value depending on the node type. For more information about how to interpret cardinality, see the topic about the individual model type.  
  
> [!NOTE]  
>  Some models, such as those created by the Microsoft Neural Network algorithm, additionally contain a special node type that provides descriptive statistics about the training data for the entire model. By definition, these nodes never have child nodes.  
  
### Node Distribution  
 The NODE_DISTRIBUTION column contains a nested table that in many nodes provides important and detailed information about the patterns discovered by the algorithm. The exact statistics provided in this table change depending on the model type, the position of the node in the tree, and whether the predictable attribute is a continuous numeric value or a discrete value; however, they can include the minimum and maximum values of an attribute, weights assigned to values, the number of cases in a node, coefficients used in a regression formula, and statistical measures such as standard deviation and variance. For more information about how to interpret node distribution, see the topic for the specific type of model type that you are working with.  
  
> [!NOTE]  
>  The NODE_DISTRIBUTION table may be empty, depending on the node type. For example, some nodes serve only to organize a collection of child nodes, and it is the child nodes that contain the detailed statistics.  
  
 The nested table, NODE_DISTRIBUTION, always contains the following columns. The content of each column varies depending on the model type. For more information about specific model types, see [Mining Model Content by Algorithm Type](#bkmk_AlgoType).  
  
 ATTRIBUTE_NAME  
 Content varies by algorithm. Can be the name of a column, such as a predictable attribute, a rule, an itemset, or a piece of information internal to the algorithm, such as part of a formula.  
  
 This column can also contain an attribute-value pair.  
  
 ATTRIBUTE_VALUE  
 Value of the attribute named in ATTRIBUTE_NAME.  
  
 If the attribute name is a column, then in the most straightforward case, the ATTRIBUTE_VALUE contains one of the discrete values for that column.  
  
 Depending on how the algorithm processes values, the ATTRIBUTE_VALUE can also contain a flag that tells you whether a value exists for the attribute (**Existing**), or whether the value is null (**Missing**).  
  
 For example, if your model is set up to find customers who have purchased a particular item at least once, the ATTRIBUTE_NAME column might contain the attribute-value pair that defines the item of interest, such as `Model = 'Water bottle'`, and the ATTRIBUTE_VALUE column would contain only the keyword **Existing** or **Missing**.  
  
 SUPPORT  
 Count of the cases that have this attribute-value pair, or that contain this itemset or rule.  
  
 In general, for each node, the support value tells you how many cases in the training set are included in the current node. In most model types, support represents an exact count of cases. Support values are useful because you can view the distribution of data within your training cases without having to query the training data. The Analysis Services server also uses these stored values to calculate stored probability versus prior probability, to determine whether inference is strong or weak.  
  
 For example, in a classification tree, the support value indicates the number of cases that have the described combination of attributes.  
  
 In a decision tree, the sum of support at each level of a tree sums to the support of its parent node. For example, if a model containing 1200 cases is divided equally by gender, and then subdivided equally by three values for Income-Low, Medium, and High-the child nodes of node (2), which are nodes (4), (5) and (6), always sum to the same number of cases as node (2).  
  
|Node ID and node attributes|Support count|  
|---------------------------------|-------------------|  
|(1) Model root|1200|  
|(2) Gender = Male<br /><br /> (3) Gender = Female|600<br /><br /> 600|  
|(4) Gender = Male and Income = High<br /><br /> (5) Gender = Male and Income = Medium<br /><br /> (6) Gender = Male and Income = Low|200<br /><br /> 200<br /><br /> 200|  
|(7) Gender = Female and Income = High<br /><br /> (8) Gender = Female and Income = Medium<br /><br /> (9) Gender = Female and Income = Low|200<br /><br /> 200<br /><br /> 200|  
  
 For a clustering model, the number for support can be weighted to include the probabilities of belonging to multiple clusters. Multiple cluster membership is the default clustering method. In this scenario, because each case does not necessarily belong to one and only one cluster, support in these models might not add up to 100 percent across all clusters.  
  
 PROBABILITY  
 Indicates the probability for this specific node within the entire model.  
  
 Generally, probability represents support for this particular value, divided by the total count of cases within the node (NODE_SUPPORT).  
  
 However, probability is adjusted slightly to eliminate bias caused by missing values in the data.  
  
 For example, if the current values for [Total Children] are 'One' and 'Two', you want to avoid creating a model that predicts that it is impossible to have no children, or to have three children. To ensure that missing values are improbable, but not impossible, the algorithm always adds 1 to the count of actual values for any attribute.  
  
 Example:  
  
 Probability of [Total Children = One] = [Count of cases where Total Children = One] + 1/[Count of all cases] + 3  
  
 Probability of [Total Children = Two]= [Count of cases where Total Children = Two] +1/[Count of all cases] +3  
  
> [!NOTE]  
>  The adjustment of 3 is calculated by adding 1 to the total number of existing values, n.  
  
 After adjustment, the probabilities for all values still add up to 1. The probability for the value with no data (in this example, [Total Children = 'Zero', 'Three', or some other value]), starts at a very low non-zero level, and rises slowly as more cases are added.  
  
 VARIANCE  
 Indicates the variance of the values within the node. By definition, variance is always 0 for discrete values. If the model supports continuous values, variance is computed as σ (sigma), using the denominator n, or the number of cases in the node.  
  
 There are two definitions in general use to represent standard deviation (**StDev**). One method for calculating standard deviation takes into account bias, and another method computes standard deviation without using bias. In general, Microsoft data mining algorithms do not use bias when computing standard deviation.  
  
 The value that appears in the NODE_DISTRIBUTION table is the actual value for all discrete and discretized attributes, and the mean for continuous values.  
  
 VALUE_TYPE  
 Indicates the data type of the value or an attribute, and the usage of the value. Certain value types apply only to certain model types:  
  
|VALUE_TYPE ID|Value Label|Value Type Name|  
|--------------------|-----------------|---------------------|  
|1|Missing|Indicates that the case data did not contain a value for this attribute. The **Missing** state is calculated separately from attributes that have values.|  
|2|Existing|Indicates that the case data contains a value for this attribute.|  
|3|Continuous|Indicates that the value of the attribute is a continuous numeric value and therefore can be represented by a mean, together with variance and standard deviation.|  
|4|Discrete|Indicates a value, either numeric or text, that is treated as discrete.<br /><br /> **Note** Discrete values can also be missing; however, they are handled differently when making calculations. For information, see [Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md).|  
|5|Discretized|Indicates that the attribute contains numeric values that have been discretized. The value will be a formatted string that describes the discretization buckets.|  
|6|Existing|Indicates that the attribute has continuous numeric values and that values have been supplied in the data, vs. values that are missing or inferred.|  
|7|Coefficient|Indicates a numeric value that represents a coefficient.<br /><br /> A coefficient is a value that is applied when calculating the value of the dependent variable. For example, if your model creates a regression formula that predicts income based on age, the coefficient is used in the formula that relates age to income.|  
|8|Score gain|Indicates a numeric value that represents score gain for an attribute.|  
|9|Statistics|Indicates a numeric value that represents a statistic for a regressor.|  
|10|Node unique name|Indicates that the value should not be handled not as numeric or string, but as the unique identifier of another content node in a model.<br /><br /> For example, in a neural network model, the IDs provide pointers from nodes in the output layer to nodes in the hidden layer, and from nodes in the hidden layer to nodes in the input layer.|  
|11|Intercept|Indicates a numeric value that represents the intercept in a regression formula.|  
|12|Periodicity|Indicates that the value denotes a periodic structure in a model.<br /><br /> Applies only to time series models that contain an ARIMA model.<br /><br /> Note: The Microsoft Time Series algorithm automatically detects periodic structures based on the training data. As a result, the periodicities in the final model may include periodicity values that you did not provide as a parameter when creating the model.|  
|13|Autoregressive order|Indicates that the value represents the number of autoregressive series.<br /><br /> Applies to time series models that use the ARIMA algorithm.|  
|14|Moving average order|Represents a value that represents the number of moving averages in a series.<br /><br /> Applies to time series models that use the ARIMA algorithm.|  
|15|Difference order|Indicates that the value represents a value that indicates how many times the series is differentiated.<br /><br /> Applies to time series models that use the ARIMA algorithm.|  
|16|Boolean|Represents a Boolean type.|  
|17|Other|Represents a custom value defined by the algorithm.|  
|18|Prerendered string|Represents a custom value that the algorithm renders as a string. No formatting was applied by the object model.|  
  
 The value types are derived from the ADMOMD.NET enumeration. For more information, see <xref:Microsoft.AnalysisServices.AdomdServer.MiningValueType>.  
  
### Node Score  
 The meaning of the node score differs depending on the model type, and can be specific to the node type as well. For information about how NODE_SCORE is calculated for each model and node type, see [Mining Model Content by Algorithm Type](#bkmk_AlgoType).  
  
### Node Probability and Marginal Probability  
 The mining model schema rowset includes the columns NODE_PROBABILITY and MARGINAL_PROBABILITY for all model types. These columns contain values only in nodes where a probability value is meaningful. For example, the root node of a model never contains a probability score.  
  
 In those nodes that do provide probability scores, the node probability and marginal probabilities represent different calculations.  
  
-   **Marginal probability** is the probability of reaching the node from its parent.  
  
-   **Node probability** is the probability of reaching the node from the root.  
  
-   **Node probability** is always less than or equal to **marginal probability**.  
  
 For example, if the population of all customers in a decision tree is split equally by gender (and no values are missing), the probability of the child nodes should be .5. However, suppose that each of the nodes for gender is divided equally by income levels-High, Medium, and Low. In this case the MARGINAL_PROBABILITY score for each child node should always be .33 but the NODE_PROBABILTY value will be the product of all probabilities leading to that node and thus always less than the MARGINAL_PROBABILITY value.  
  
|Level of node/attribute and value|Marginal probability|Node probability|  
|----------------------------------------|--------------------------|----------------------|  
|Model root<br /><br /> All target customers|1|1|  
|Target customers split by gender|.5|.5|  
|Target customers split by gender, and split again three ways by income|.33|.5 * .33 = .165|  
  
### Node Rule and Marginal Rule  
 The mining model schema rowset also includes the columns NODE_RULE and MARGINAL_RULE for all model types. These columns contain XML fragments that can be used to serialize a model, or to represent some part of the model structure. These columns may be blank for some nodes, if a value would be meaningless.  
  
 Two kinds of XML rules are provided, similar to the two kinds of probability values. The XML fragment in MARGINAL_RULE defines the attribute and value for the current node, whereas the XML fragment in NODE_RULE describes the path to the current node from the model root.  
  
##  <a name="bkmk_AlgoType"></a> Mining Model Content by Algorithm Type  
 Each algorithm stores different types of information as part of its content schema. For example, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Clustering Algorithm generates many child nodes, each of which represents a possible cluster. Each cluster node contains rules that describe characteristics shared by items in the cluster. In contrast, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm does not contain any child nodes; instead, the parent node for the model contains the equation that describes the linear relationship discovered by analysis.  
  
 The following table provides links to topics for each type of algorithm.  
  
-   **Model content topics:** Explain the meaning of each node type for each algorithm type, and provide guidance about which nodes are of most interest in a particular model type.  
  
-   **Querying topics:** Provide examples of queries against a particular model type and guidance on how to interpret the results.  
  
|Algorithm or Model Type|Model Content|Querying Mining Models|  
|-----------------------------|-------------------|----------------------------|  
|Association rules models|[Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-association-models-analysis-services-data-mining.md)|[Association Model Query Examples](../../analysis-services/data-mining/association-model-query-examples.md)|  
|Clustering models|[Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-decision-tree-models-analysis-services-data-mining.md)|[Clustering Model Query Examples](../../analysis-services/data-mining/clustering-model-query-examples.md)|  
|Decision trees model|[Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-decision-tree-models-analysis-services-data-mining.md)|[Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md)|  
|Linear regression models|[Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)|[Linear Regression Model Query Examples](../../analysis-services/data-mining/linear-regression-model-query-examples.md)|  
|Logistic regression models|[Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md)|[Linear Regression Model Query Examples](../../analysis-services/data-mining/linear-regression-model-query-examples.md)|  
|Naïve Bayes models|[Mining Model Content for Naive Bayes Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-naive-bayes-models-analysis-services-data-mining.md)|[Naive Bayes Model Query Examples](../../analysis-services/data-mining/naive-bayes-model-query-examples.md)|  
|Neural network models|[Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-neural-network-models-analysis-services-data-mining.md)|[Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md)|  
|Sequence clustering|[Mining Model Content for Sequence Clustering Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-sequence-clustering-models.md)|[Sequence Clustering Model Query Examples](../../analysis-services/data-mining/sequence-clustering-model-query-examples.md)|  
|Time series models|[Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-time-series-models-analysis-services-data-mining.md)|[Time Series Model Query Examples](../../analysis-services/data-mining/time-series-model-query-examples.md)|  
  
##  <a name="bkmk_Viewing"></a> Tools for Viewing Mining Model Content  
 When you browse or explore a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you can view the information in the **Microsoft Generic Content Tree Viewer**, which is available in both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Generic Content Viewer displays the columns, rules, properties, attributes, nodes, and other content from the model by using the same information that is available in the content schema rowset of the mining model. The content schema rowset is a generic framework for presenting detailed information about the content of a data mining model. You can view model content in any client that supports hierarchical rowsets. The viewer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] presents this information in an HTML table viewer that represents all models in a consistent format, making it easier to understand the structure of the models that you create. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md).  
  
##  <a name="bkmk_Querying"></a> Tools for Querying Mining Model Content  
 To retrieve mining model content, you must create a query against the data mining model.  
  
 The easiest way to create a content query is to execute the following DMX statement in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
```  
SELECT * FROM [<mining model name>].CONTENT  
```  
  
 For more information, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
 You can also query the mining model content by using the data mining schema rowsets. A schema rowset is a standard structure that clients use to discover, browse, and query information about mining structures and models. You can query the schema rowsets by using XMLA, Transact-SQL, or DMX statements.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can also access the information in the data mining schema rowsets by opening a connection to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and querying the system tables. For more information, see [Data Mining Schema Rowsets &#40;SSAs&#41;](../../analysis-services/data-mining/data-mining-schema-rowsets-ssas.md).  
  
## See Also  
 [Microsoft Generic Content Tree Viewer &#40;Data Mining&#41;](http://msdn.microsoft.com/library/751b4393-f6fd-48c1-bcef-bdca589ce34c)   
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)  
  
  
