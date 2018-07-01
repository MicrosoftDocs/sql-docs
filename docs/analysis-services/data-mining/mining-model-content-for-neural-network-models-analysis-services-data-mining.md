---
title: "Mining Model Content for Neural Network Models | Microsoft Docs"
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
# Mining Model Content for Neural Network Models (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This topic describes mining model content that is specific to models that use the Microsoft Neural Network algorithm. For an explanation of how to interpret statistics and structure shared by all model types, and general definitions of terms related to mining model content, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
## Understanding the Structure of a Neural Network Model  
 Each neural network model has a single parent node that represents the model and its metadata, and a marginal statistics node (NODE_TYPE = 24) that provides descriptive statistics about the input attributes. The marginal statistics node is useful because it summarizes information about inputs, so that you do not need to query data from the individual nodes.  
  
 Underneath these two nodes, there are at least two more nodes, and might be many more, depending on how many predictable attributes the model has.  
  
-   The first node (NODE_TYPE = 18) always represents the top node of the input layer. Beneath this top node, you can find input nodes (NODE_TYPE = 21) that contain the actual input attributes and their values.  
  
-   Successive nodes each contain a different *subnetwork* (NODE_TYPE = 17). Each subnetwork always contains a hidden layer (NODE_TYPE = 19), and an output layer (NODE_TYPE = 20) for that subnetwork.  
  
 ![structure of model content for neural networks](../../analysis-services/data-mining/media/modelcontentstructure-nn.gif "structure of model content for neural networks")  
  
 The information in the input layer is straightforward: the top node for each input layer (NODE_TYPE = 18) serves as an organizer for a collection of input nodes (NODE_TYPE = 21). The content of the input nodes is described in the following table.  
  
 Each subnetwork (NODE_TYPE = 17) represents the analysis of the influence of the input layer on a particular predictable attribute. If there are multiple predictable outputs, there are multiple subnetworks. The hidden layer for each subnetwork contains multiple hidden nodes (NODE_TYPE = 22) that contain details about the weights for each transition that ends in that particular hidden node.  
  
 The output layer (NODE_TYPE = 20) contains output nodes (NODE_TYPE = 23) that each contain distinct values of the predictable attribute. If the predictable attribute is a continuous numeric data type, there is only one output node for the attribute.  
  
> [!NOTE]  
>  The logistic regression algorithm uses a special case of a neural network that has only one predictable outcome and potentially many inputs. Logistic regression does not use a hidden layer.  
  
 The easiest way to explore the structure of the inputs and subnetworks is to use the **Microsoft Generic Content Tree viewer**. You can click any node to expand it and see the child nodes, or view the weights and other statistics that is contained in the node.  
  
 To work with the data and see how the model correlates inputs with outputs, use the **Microsoft Neural Network Viewer**. By using this custom viewer, you can filter on input attributes and their values, and graphically see how they affect the outputs. The tooltips in the viewer show you the probability and lift associated with each pair of inputs and output values. For more information, see [Browse a Model Using the Microsoft Neural Network Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-neural-network-viewer.md).  
  
## Model Content for a Neural Network Model  
 This section provides detail and examples only for those columns in the mining model content that have particular relevance for neural networks. For information about general-purpose columns in the schema rowset, such as MODEL_CATALOG and MODEL_NAME, that are not described here, or for explanations of mining model terminology, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 The names of the attributes that correspond to this node.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|Input attribute name|  
|Hidden layer|Blank|  
|Hidden node|Blank|  
|Output layer|Blank|  
|Output node|Output attribute name|  
  
 NODE_NAME  
 The name of the node. This column contains the same value as NODE_UNIQUE_NAME.  
  
 NODE_UNIQUE_NAME  
 The unique name of the node.  
  
 For more information about how the names and IDs provide structural information about the model, see the section, [Using Node Names and IDs](#bkmk_NodeIDs).  
  
 NODE_TYPE  
 A neural network model outputs the following node types:  
  
|Node Type ID|Description|  
|------------------|-----------------|  
|1|Model.|  
|17|Organizer node for the subnetwork.|  
|18|Organizer node for the input layer.|  
|19|Organizer node for the hidden layer.|  
|20|Organizer node for the output layer.|  
|21|Input attribute node.|  
|22|Hidden layer node|  
|23|Output attribute node.|  
|24|Marginal statistics node.|  
  
 NODE_CAPTION  
 A label or a caption associated with the node. In neural network models, always blank.  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Indicates the count of child nodes, which includes at least 1 network, 1 required marginal node, and 1 required input layer. For example, if the value is 5, there are 3 subnetworks.|  
|Marginal statistics|Always 0.|  
|Input layer|Indicates the number of input attribute-values pairs that were used by the model.|  
|Input node|Always 0.|  
|Hidden layer|Indicates the number of hidden nodes that were created by the model.|  
|Hidden node|Always 0.|  
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
|Hidden node|Integer that indicates the sequence of the hidden node in the list of hidden nodes.|  
|Output layer|Blank|  
|Output node|If the output attribute is continuous, contains the name of the output attribute.<br /><br /> If the output attribute is discrete or discretized, contains the name of the attribute and the value.|  
  
 NODE_RULE  
 An XML description of the rule that is embedded in the node.  
  
|Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|An XML fragment that contains the same information as the NODE_DESCRIPTION column.|  
|Hidden layer|Blank|  
|Hidden node|Integer that indicates the sequence of the hidden node in the list of hidden nodes.|  
|Output layer|Blank|  
|Output node|An XML fragment that contains the same information as the NODE_DESCRIPTION column.|  
  
 MARGINAL_RULE  
 For neural network models, always blank.  
  
 NODE_PROBABILITY  
 The probability associated with this node. For neural network models, always 0.  
  
 MARGINAL_PROBABILITY  
 The probability of reaching the node from the parent node. For neural network models, always 0.  
  
 NODE_DISTRIBUTION  
 A nested table that contains statistical information for the node. For detailed information about the contents of this table for each node type, see the section, [Understanding the NODE_DISTRIBUTION Table](#bkmk_NodeDistTable).  
  
 NODE_SUPPORT  
 For neural network models, always 0.  
  
> [!NOTE]  
>  Support probabilities are always 0 because the output of this model type is not probabilistic. Only the weights are meaningful for the algorithm; therefore, the algorithm does not compute probability, support, or variance.  
  
 To get information about the support in the training cases for specific values, see the marginal statistics node.  
  
 MSOLAP_MODEL_COLUMN  
 |Node|Content|  
|----------|-------------|  
|Model root|Blank|  
|Marginal statistics|Blank|  
|Input layer|Blank|  
|Input node|Input attribute name.|  
|Hidden layer|Blank|  
|Hidden node|Blank|  
|Output layer|Blank|  
|Output node|Input attribute name.|  
  
 MSOLAP_NODE_SCORE  
 For a neural network model, always 0.  
  
 MSOLAP_NODE_SHORT_CAPTION  
 For neural network models, always blank.  
  
## Remarks  
 The purpose of training a neural network model is to determine the weights that are associated with each transition from an input to a midpoint, and from a midpoint to an endpoint. Therefore, the input layer of the model principally exists to store the actual values that were used to build the model. The hidden layer stores the weights that were computed, and provides pointers back to the input attributes. The output layer stores the predictable values, and also provides pointers back to the midpoints in the hidden layer.  
  
##  <a name="bkmk_NodeIDs"></a> Using Node Names and IDs  
 The naming of the nodes in a neural network model provides additional information about the type of node, to make it easier to relate the hidden layer to the input layer, and the output layer to the hidden layer. The following table shows the convention for the IDs that are assigned to nodes in each layer.  
  
|Node Type|Convention for node ID|  
|---------------|----------------------------|  
|Model root (1)|00000000000000000.|  
|Marginal statistics node (24)|10000000000000000|  
|Input layer (18)|30000000000000000|  
|Input node (21)|Starts at 60000000000000000|  
|Subnetwork (17)|20000000000000000|  
|Hidden layer (19)|40000000000000000|  
|Hidden node (22)|Starts at 70000000000000000|  
|Output layer (20)|50000000000000000|  
|Output node (23)|Starts at 80000000000000000|  
  
 You can determine which input attributes are related to a specific hidden layer node by viewing the NODE_DISTRIBUTION table in the hidden node (NODE_TYPE = 22). Each row of the NODE_DISTRIBUTION table contains the ID of an input attribute node.  
  
 Similarly, you can determine which hidden layers are related to an output attribute by viewing the NODE_DISTRIBUTION table in the output node (NODE_TYPE = 23). Each row of the NODE_DISTRIBUTION table contains the ID of a hidden layer node, together with the related coefficient.  
  
##  <a name="bkmk_NodeDistTable"></a> Interpreting the Information in the NODE_DISTRIBUTION Table  
 The NODE_DISTRIBUTION table can be empty in some nodes. However, for input nodes, hidden layer nodes, and output nodes, the NODE_DISTRIBUTION table stores important and interesting information about the model. To help you interpret this information, the NODE_DISTRIBUTION table contains a VALUETYPE column for each row that tells you whether the value in the ATTRIBUTE_VALUE column is Discrete (4), Discretized (5), or Continuous (3).  
  
### Input Nodes  
 The input layer contains a node for each value of the attribute that was used in the model.  
  
 **Discrete attribute:** The input node stores only the name of the attribute and its value in the ATTRIBUTE_NAME and ATTRIBUTE_VALUE columns. For example, if [Work Shift] is the column, a separate node is created for each value of that column that was used in the model, such as AM and PM. The NODE_DISTRIBUTION table for each node lists only the current value of the attribute.  
  
 **Discretized numeric attribute:** The input node stores the name of the attribute, and the value, which can be a range or a specific value. All values are represented by expressions, such as '77.4 - 87.4' or ' < 64.0' for the value of the [Time Per Issue]. The NODE_DISTRIBUTION table for each node lists only the current value of the attribute.  
  
 **Continuous attribute:** The input node stores the mean value of the attribute. The NODE_DISTRIBUTION table for each node lists only the current value of the attribute.  
  
### Hidden Layer Nodes  
 The hidden layer contains a variable number of nodes. In each node, the NODE_DISTRIBUTION table contains mappings from the hidden layer to the nodes in the input layer. The ATTRIBUTE_NAME column contains a node ID that corresponds to a node in the input layer. The ATTRIBUTE_VALUE column contains the weight associated with that combination of input node and hidden layer node. The last row in the table contains a coefficient that represents the weight of that hidden node in the hidden layer.  
  
### Output Nodes  
 The output layer contains one output node for each output value that was used in the model. In each node, the NODE_DISTRIBUTION table contains mappings from the output layer to the nodes in the hidden layer. The ATTRIBUTE_NAME column contains a node ID that corresponds to a node in the hidden layer. The ATTRIBUTE_VALUE column contains the weight associated with that combination of output node and hidden layer node.  
  
 The NODE_DISTRIBUTION table has the following additional information, depending on whether the type of the attribute:  
  
 **Discrete attribute:** The final two rows of the NODE_DISTRIBUTION table contain a coefficient for the node as a whole, and the current value of the attribute.  
  
 **Discretized numeric attribute:** Identical to discrete attributes, except that the value of the attribute is a range of values.  
  
 **Continuous attribute:** The final two rows of the NODE_DISTRIBUTION table contain the mean of the attribute, the coefficient for the node as a whole, and the variance of the coefficient.  
  
## See Also  
 [Microsoft Neural Network Algorithm](../../analysis-services/data-mining/microsoft-neural-network-algorithm.md)   
 [Microsoft Neural Network Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md)   
 [Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md)  
  
  
