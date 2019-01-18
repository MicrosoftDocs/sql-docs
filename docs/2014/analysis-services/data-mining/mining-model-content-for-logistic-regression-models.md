---
title: "Mining Model Content for Logistic Regression Models (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "logistic regression [Analysis Services]"
  - "mining model content, logistic regression models"
  - "regression algorithms [Analysis Services]"
ms.assetid: 69cc0b86-e8bc-4d6c-903e-85724f5c0396
author: minewiskan
ms.author: owend
manager: craigg
---
# Mining Model Content for Logistic Regression Models (Analysis Services - Data Mining)
  This topic describes mining model content that is specific to models that use the Microsoft Logistic Regression algorithm. For an explanation of how to interpret statistics and structure shared by all model types, and general definitions of terms related to mining model content, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
## Understanding the Structure of a Logistic Regression Model  
 A logistic regression model is created by using the Microsoft Neural Network algorithm with parameters that constrain the model to eliminate the hidden node. Therefore, the overall structure of a logistic regression model is almost identical to that of a neural network: each model has a single parent node that represents the model and its metadata, and a special marginal statistics node (NODE_TYPE = 24) that provides descriptive statistics about the inputs used in the model.  
  
 Additionally, the model contains a subnetwork (NODE_TYPE = 17) for each predictable attribute. Just like in a neural network model, each subnetwork always contains two branches: one for the input layer, and another branch that contains the hidden layer (NODE_TYPE = 19) and the output layer (NODE_TYPE = 20) for the network. The same subnetwork may be used for multiple attributes if they are specified as predict-only. Predictable attributes that are also inputs may not appear in the same subnetwork.  
  
 However, in a logistic regression model, the node that represents the hidden layer is empty, and has no children. Therefore the model contains nodes that represent individual outputs (NODE_TYPE = 23) and individual inputs (NODE_TYPE = 21) but no individual hidden nodes.  
  
 ![structure of content for logisitc regression model](../media/skt-modelcontentstructure-logregc.gif "structure of content for logisitc regression model")  
  
 By default, a logistic regression model is displayed in the **Microsoft Neural Network Viewer**. With this custom viewer, you can filter on input attributes and their values, and graphically see how they affect the outputs. The tooltips in the viewer show you the probability and lift associated with each pair of inputs and output values. For more information, see [Browse a Model Using the Microsoft Neural Network Viewer](browse-a-model-using-the-microsoft-neural-network-viewer.md).  
  
 To explore the structure of the inputs and subnetworks, and to see detailed statistics, you can use the Microsoft Generic Content Tree viewer. You can click on any node to expand it and see the child nodes, or view the weights and other statistics contained in the node.  
  
## Model Content for a Logistic Regression Model  
 This section provides detail and examples only for those columns in the mining model content that have particular relevance for logistic regression. The model content is almost identical to that of a neural network model, but descriptions that apply to neural network models may be repeated in this table for convenience.  
  
 For information about general-purpose columns in the schema rowset, such as MODEL_CATALOG and MODEL_NAME, that are not described here, or for explanations of mining model terminology, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 The names of the attribute that corresponds to this node.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|Input attribute name|  
|Hidden layer|Blank|  
|Output layer|Blank|  
|Output node|Output attribute name|  
  
 NODE_NAME  
 The name of the node. Currently, this column contains the same value as NODE_UNIQUE_NAME, though this may change in future releases.  
  
 NODE_UNIQUE_NAME  
 The unique name of the node.  
  
 For more information about how the names and IDs provide structural information about the model, see the section, [Using Node Names and IDs](#bkmk_NodeIDs).  
  
 NODE_TYPE  
 A logistic regression model outputs the following node types:  
  
|Node Type ID|Description|  
|------------------|-----------------|  
|1|Model.|  
|17|Organizer node for the subnetwork.|  
|18|Organizer node for the input layer.|  
|19|Organizer node for the hidden layer. The hidden layer is empty.|  
|20|Organizer node for the output layer.|  
|21|Input attribute node.|  
|23|Output attribute node.|  
|24|Marginal statistics node.|  
  
 NODE_CAPTION  
 A label or a caption associated with the node. In logistic regression models, always blank.  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Indicates the count of child nodes, which includes at least 1 network, 1 required marginal node, and 1 required input layer. For example, if the value is 5, there are 3 subnetworks.|  
|Marginal statistics|Always 0.|  
|Input layer|Indicates the number of input attribute-values pairs that were used by the model.|  
|Input node|Always 0.|  
|Hidden layer|In a logistic regression model, always 0.|  
|Output layer|Indicates the number of output values.|  
|Output node|Always 0.|  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent. NULL is returned for any nodes at the root level.  
  
 For more information about how the names and IDs provide structural information about the model, see the section, [Using Node Names and IDs](#bkmk_NodeIDs).  
  
 NODE_DESCRIPTION  
 A user-friendly description of the node.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|Input attribute name|  
|Hidden layer|Blank|  
|Output layer|Blank|  
|Output node|If the output attribute is continuous, contains the name of the output attribute.<br /><br /> If the output attribute is discrete or discretized, contains the name of the attribute and the value.|  
  
 NODE_RULE  
 An XML description of the rule that is embedded in the node.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|An XML fragment containing the same information as the NODE_DESCRIPTION column.|  
|Hidden layer|Blank|  
|Output layer|Blank|  
|Output node|An XML fragment containing the same information as the NODE_DESCRIPTION column.|  
  
 MARGINAL_RULE  
 For logistic regression models, always blank.  
  
 NODE_PROBABILITY  
 The probability associated with this node. For logistic regression models, always 0.  
  
 MARGINAL_PROBABILITY  
 The probability of reaching the node from the parent node. For logistic regression models, always 0.  
  
 NODE_DISTRIBUTION  
 A nested table that contains statistical information for the node. For detailed information about the contents of this table for each node type, see the section, Understanding the NODE_DISTRIBUTION Table, in [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md).  
  
 NODE_SUPPORT  
 For logistic regression models, always 0.  
  
> [!NOTE]  
>  Support probabilities are always 0 because the output of this model type is not probabilistic. The only thing that is meaningful for the algorithm is the weights; therefore, the algorithm does not compute probability, support, or variance.  
  
 To get information about the support in the training cases for specific values, see the marginal statistics node.  
  
 MSOLAP_MODEL_COLUMN  
 |Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|Input attribute name.|  
|Hidden layer|Blank|  
|Output layer|Blank|  
|Output node|Input attribute name.|  
  
 MSOLAP_NODE_SCORE  
 In logistic regression models, always 0.  
  
 MSOLAP_NODE_SHORT_CAPTION  
 In logistic regression models, always blank.  
  
##  <a name="bkmk_NodeIDs"></a> Using Node Names and IDs  
 The naming of the nodes in a logistic regression model provides additional information about the relationships between nodes in the model. The following table shows the conventions for the IDs that are assigned to nodes in each layer.  
  
|Node Type|Convention for node ID|  
|---------------|----------------------------|  
|Model root (1)|00000000000000000.|  
|Marginal statistics node (24)|10000000000000000|  
|Input layer (18)|30000000000000000|  
|Input node (21)|Starts at 60000000000000000|  
|Subnetwork (17)|20000000000000000|  
|Hidden layer (19)|40000000000000000|  
|Output layer (20)|50000000000000000|  
|Output node (23)|Starts at 80000000000000000|  
  
 You can use these IDs to determine how output attributes are related to specific input layer attributes, by viewing the NODE_DISTRIBUTION table of the output node. Each row in that table contains an ID that points back to a specific input attribute node. The NODE_DISTRIBUTION table also contains the coefficient for that input-output pair.  
  
## See Also  
 [Microsoft Logistic Regression Algorithm](microsoft-logistic-regression-algorithm.md)   
 [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md)   
 [Logistic Regression Model Query Examples](logistic-regression-model-query-examples.md)   
 [Microsoft Logistic Regression Algorithm Technical Reference](microsoft-logistic-regression-algorithm-technical-reference.md)  
  
  
