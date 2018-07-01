---
title: "Mining Model Content for Decision Tree Models  | Microsoft Docs"
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
# Mining Model Content for Decision Tree Models (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This topic describes mining model content that is specific to models that use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm. For a general explanation of mining model content for all model types, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md). It is important to remember that The Microsoft Decision Trees algorithm is a hybrid algorithm that can create models with very different functions: a decision tree can represent associations, rules, or even linear regression. The structure of the tree is essentially the same, but how you interpret the information will depend on the purpose for which you created the model.  
  
##  <a name="bkmk_Top"></a> Understanding the Structure of a Decision Trees Model  
 A decision trees model has a single parent node that represents the model and its metadata. Underneath the parent node are independent trees that represent the predictable attributes that you select. For example, if you set up your decision tree model to predict whether customers will purchase something, and provide inputs for gender and income, the model would create a single tree for the purchasing attribute, with many branches that divide on conditions related to gender and income.  
  
 However, if you then add a separate predictable attribute for participation in a customer rewards program, the algorithm will create two separate trees under the parent node. One tree contains the analysis for purchasing, and another tree contains the analysis for the customer rewards program.  If you use the Decision Trees algorithm to create an association model, the algorithm creates a separate tree for each product that is being predicted, and the tree contains all the other product combinations that contribute towards selection of the target attribute.  
  
> [!NOTE]  
>  If your model includes multiple trees, you can view only one tree at a time in the **Microsoft Tree Viewer**. However, in the **Generic Content Tree Viewer** , all trees in the same model are displayed at the same time.  
  
 ![structure of model content for decision tree](../../analysis-services/data-mining/media/modelcontentstructure-dt.gif "structure of model content for decision tree")  
  
 The tree for each predictable attribute contains information that describes how the input columns that you choose affect the outcome of that particular predictable attribute. Each tree is headed by a node (NODE_TYPE = 9) that contains the predictable attribute, followed by a series of nodes (NODE_TYPE = 10) that represent the input attributes. An attribute corresponds to either a case-level column or values of nested table columns, which are generally the values in the **Key** column of the nested table.  
  
 Interior and leaf nodes represent split conditions. A tree can split on the same attribute multiple times. For example, the **TM_DecisionTree** model might split on [Yearly Income] and [Number of Children], and then split again on [Yearly Income] further down the tree.  
  
 The Microsoft Decision Trees algorithm can also contain linear regressions in all or part of the tree. If the attribute that you are modeling is a continuous numeric data type, the model can create a regression tree node (NODE_TYPE = 25) wherever the relationship between the attributes can be modeled linearly. In this case, the node contains a regression formula.  
  
 However, if the predictable attribute has discrete values, or if numeric values have been bucketed or discretized, the model always creates a classification tree (NODE_TYPE =2). A classification tree can have multiple branches or interior tree nodes (NODE_TYPE =3) for each value of the attribute. However, the split is not necessarily on each value of the attribute.  
  
 The Microsoft Decision Trees algorithm does not allow continuous data types as inputs; therefore, if any columns have a continuous numeric data type, the values are discretized. The algorithm performs its own discretization at the point of a split for all continuous attributes.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] automatically chooses a method for bucketing continuous attributes; however, you can control how continuous values in the inputs are discretized by setting the content type of the mining structure column to **Discretized** and then setting the <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationBucketCount%2A> or <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationMethod%2A> property.  
  
 [Top](#bkmk_Top)  
  
##  <a name="bkmk_ModelContent"></a> Model Content for a Decision Trees Model  
 This section provides details and examples only for those columns in the mining model content that have particular relevance for decision trees models. For information about general-purpose columns in the schema rowset, and explanations of mining model terminology, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 Name of the attribute that corresponds to this node.  
  
 NODE_NAME  
 Always same as NODE_UNIQUE_NAME.  
  
 NODE_UNIQUE_NAME  
 A unique identifier for the node within the model. This value cannot be changed.  
  
 For decision tree models, the unique names follow the following convention, which does not apply to all algorithms:  
  
 The child nodes of any particular node will all have the same hexadecimal prefix, followed by another hexadecimal number that represents the sequence of the child node within the parent. You can use the prefixes to infer a path.  
  
 NODE_TYPE  
 In decision tree models, the following types of nodes are created:  
  
|Node Type|Description|  
|---------------|-----------------|  
|1 (Model)|Root node for model.|  
|2 (Tree)|Parent node for classification trees in the model. Labeled **"All"**.|  
|3 (Interior)|Head of interior branch, found within in a classification tree or regression tree.|  
|4 (Distribution)|Leaf node, found within a classification tree or regression tree.|  
|25 (Regression tree)|Parent node for regression tree within the model. Labeled as **"All"**.|  
  
 NODE_CAPTION  
 A friendly name for display purposes.  
  
 When you create a model, the value of NODE_UNIQUE_NAME is automatically used as the caption. However, you can change the value for NODE_CAPTION to update the display name for the cluster, either programmatically or by using the viewer. The caption is automatically generated by the model. The content of the caption depends on the type of model, and the node type.  
  
 In a decision trees model, the NODE_CAPTION and the NODE_DESCRIPTION have different information, depending on the level in the tree. For more information and examples, see [Node Caption and Node Description](#NodeCaption).  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
 **Parent node** Indicates the number of predictable attributes that were modeled. A tree is created for each predictable attribute.  
  
 **Tree node** The **All** node for each tree tells you how many values were used for the target attribute.  
  
-   If the target attribute is discrete, the value equals the number of distinct values plus 1 for the **Missing** state.  
  
-   If the predictable attribute is continuous, the value tells you how many buckets were used to model the continuous attribute.  
  
 **Leaf nodes** Always 0.  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent. NULL is returned for any nodes at the root level.  
  
 NODE_DESCRIPTION  
 A description of the node.  
  
 In a decision trees model, the NODE_CAPTION and the NODE_DESCRIPTION have different information, depending on the level in the tree.  
  
 For more information and examples, see [Node Caption and Node Description](#NodeCaption).  
  
 NODE_RULE  
 An XML description of the rule that describes the path to the current node from its immediate parent node.  
  
 For more information and examples, see [Node Rule and Marginal Rule](#NodeRule).  
  
 MARGINAL_RULE  
 An XML description of the rule that describes the path from the model parent node to the current node.  
  
 For more information, see [Node Rule and Marginal Rule](#NodeRule).  
  
 NODE_PROBABILITY  
 The probability associated with this node.  
  
 For more information, see [Probability](#bkmk_NodeDist_Discrete).  
  
 MARGINAL_PROBABILITY  
 The probability of reaching the node from the parent node.  
  
 For more information, see [Probability](#bkmk_NodeDist_Discrete).  
  
 NODE_DISTRIBUTION  
 A table that contains the probability histogram of the node. The information in this table differs depending on whether the predictable attribute is a continuous or discrete variable.  
  
 **Model root node** This table is empty.  
  
 **(All) node** Contains a summary for the model as a whole.  
  
 **Interior node** Contains aggregated statistics for its leaf nodes.  
  
 **Leaf node** Contains support and probability for the predicted outcomes given all the conditions in the path leading to the current leaf node.  
  
 **Regression node** Contains regression formula that represents the relationship between the inputs and the predictable attribute.  
  
 For more information, see [Node Distribution for Discrete Attributes](#bkmk_NodeDist_Discrete) and [Node Distribution for Continuous Attributes](#bkmk_RegressionNodes).  
  
 NODE_SUPPORT  
 The number of cases that support this node.  
  
 MSOLAP_MODEL_COLUMN  
 Indicates the column that contains the predictable attribute.  
  
 MSOLAP_NODE_SCORE  
 Displays a score associated with the node. For more information, see [Node Score](#NodeScore).  
  
 MSOLAP_NODE_SHORT_CAPTION  
 A label used for display purposes.  
  
## Remarks  
 A decision trees model does not have a separate node that stores statistics for the entire model, unlike the marginal statistics node found in a Naive Bayes or neural network model. Instead, the model creates a separate tree for each predictable attribute, with an (All) node at the top of the tree. Each tree is independent of the others. If your model contains only one predictable attribute, there is only one tree, and therefore only one (All) node.  
  
 Each tree that represents an output attribute is additionally subdivided into interior branches (NODE_TYPE = 3) that represent splits. Each of these trees contains statistics about the distribution of the target attribute. In addition, each leaf node (NODE_TYPE = 4) contains statistics that describe input attributes and their values, together with the number of cases in support of each attribute-value pair. Therefore, in any branch of a decision tree, you can view the probabilities or the distribution of data easily without having to query the source data. Each level of the tree necessarily represents the sum of its immediate child nodes.  
  
 For examples of how to retrieve these statistics, see [Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md).  
  
 [Top](#bkmk_Top)  
  
## Example of Decision Tree Structure  
 To understand how a decision tree works, consider an example, such as the AdventureWorks bike buyer scenario. Assuming that the predictable attribute is customer purchases, the decision trees algorithm tries to find one column of data, among all the inputs that you provided, that most effectively detects the customers that are likely to purchase a bike and those who are unlikely to buy a bike. For example, the model might find that Age is the best indicator of purchasing behavior. Specifically, that the customers over the age of 30 are very likely to purchase a bike, and all other customers are unlikely to make a purchase. In this scenario, the model creates a *split* on the Age attribute. That means that the tree divides into two branches, one containing customers over the age of 30, and the other containing customers under 30. The new branches are represented in the model structure as two new interior trees (NODE_TYPE = 3).  
  
 For each branch, the model continues to look for additional attributes to use in differentiating customers. If there is insufficient evidence in the data to continue creating subgroups of customers, the model stops building the tree. The model will also stop building the tree whenever the number of cases in the node is too small to continue, regardless of how good the split is, or if the value is null or missing. By stopping the growth of the tree early, you prevent the model from training too closely to one particular set of data.  
  
 Each interior tree node contains leaf nodes that provide a breakdown of the outcomes given the current classification results. For example, you might have an interior node that represents Age >= 30 and Gender = Male. The node for this group shows you how many customers in this category purchased or did not purchase something. For example, the classification might contain the following tree splits:  
  
|Interior tree|Split|  
|-------------------|-----------|  
|Age >= 30|Age >= 30 and Gender = Male|  
||Age >= 30 and Gender = Female|  
|Age < 30|Age < 30 and Gender = Male|  
||Age < 30 and Gender = Female|  
  
 When you use a decision tree model for prediction, the model takes the attributes that you provide to it as arguments and follows the path of the attributes down through the tree. In general, all predictions go to a leaf, and the interior nodes are used only for classification.  
  
 A leaf node always has a NODE_TYPE of 4 (Distribution) and contains a histogram that tells the probability of each outcome (purchase or not purchase) given the attributes you provide. For example, if you ask for a prediction for a new customer who is a male over 60, the model will look up the corresponding node (Age > 30 and Gender = Male) and then return the probability for the outcome that you specify. These probabilities are stored in the [NODE_DISTRIBUTION](#bkmk_NodeDist_Discrete) table for the node.  
  
 If the predictable attribute is a continuous number, the algorithm tries to create a regression formula that models the relationship between the predictable attribute and the inputs.  
  
 [Top](#bkmk_Top)  
  
###  <a name="NodeCaption"></a> Node Caption and Node Description  
 In a decision tree model, the node caption and node description contain similar information. However, the node description is more complete and contains more information as you move closer to the leaf nodes. Both the node caption and node description are localized strings.  
  
|||  
|-|-|  
|**NODE_CAPTION**|Displays the attribute that distinguishes that particular node relative to the parent node. The node caption defines a sub-segment of the population based the split condition. For example, if the split was on [Age] and it was a three-way split, the node captions for the three child nodes might be "[Age] < 40", "40 <= [Age] < 50", "[Age] >= 50".|  
|**NODE_DESCRIPTION**|Contains a full list of the attributes that distinguish that node from other nodes, starting from the model parent node. For example, Product name = Apple and Color = Red.|  
  
 [Top](#bkmk_Top)  
  
###  <a name="NodeRule"></a> Node Rule and Marginal Rule  
 The NODE_RULE and MARGINAL_RULE columns contain the same information as the NODE_CAPTION and NODE_DESCRIPTION columns, but represent the information as XML fragments. The node rule is an XML version of the full path, whereas the marginal rule indicates the most recent split.  
  
 The attribute represented by the XML fragment can be either simple or complex. A simple attribute contains the name of the model column, and the value of the attribute. If the model column contains a nested table, the nested table attribute is represented as a concatenation of the table name, the key value, and the attribute.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports version 2.0 of the PMML standard, with extensions to support the use of nested table. If your data contains nested tables and you generate a PMML version of the model, all elements in the model that include the predicates are marked as an extension.  
  
 [Top](#bkmk_Top)  
  
###  <a name="bkmk_NodeDist_Discrete"></a> Node Distribution for Discrete Attributes  
 In a decision trees model, the NODE_DISTRIBUTION table contains useful statistics. However, the type of statistics depends on whether the tree predicts a discrete or continuous attribute. This section describes the meaning of the node distribution statistics for discrete attributes.  
  
#### Attribute Name and Attribute Value  
 In a classification tree, the attribute name always contains the name of the predictable column. This value tells you what the tree predicts. Because a single tree always represents a single predictable attribute, this value is repeated throughout the tree.  
  
 For a discrete data type, the attribute value field lists the possible values of the predictable column, plus the **Missing** value.  
  
#### Support  
 The support value for each node tells you how many cases are included in this node. At the (All) level, you should see the complete count of cases that were used to train the model. For each split in the tree, the support value is the count of cases that were grouped into that node of the tree. The sum of cases in the leaf nodes necessarily equals the count of cases in the parent node of the tree.  
  
 For nodes that represent continuous attributes, the presence of nulls in the data might lead to some counterintuitive results. For example, if there are m cases, a mean value would be calculated as sum(all cases)/n, where n is a number less than m, and m-n indicates the count of cases with missing values. Support is also represented as n.  
  
#### Probability  
 The probability associated with each node tells you the probability that any case in the whole data set would end up in this particular node. Probability scores are computed both for the tree as a whole, and for the immediate split.  
  
 For example, the following table shows a very simple model, with 100 cases.  
  
|Interior tree|Cases|Leaf node|Cases|Probability relative to parent node|Probability relative to top node|  
|-------------------|-----------|---------------|-----------|-----------------------------------------|--------------------------------------|  
|Age >= 30|60|Age >= 30 and Gender = Male|50|50/60 = .83|50/100 = .5|  
|||Age >= 30 and Gender = Female|10|10/60 = .16|10/100 = .10|  
|Age < 30|40|Age < 30 and Gender = Male|30|30/40 = .75|30/100 = .30|  
|||Age < 30 and Gender = Female|10|10/40 = .25|10/100 = .10|  
  
 A small adjustment is made in all models to account for possible missing values. For continuous attributes, each value or range of values is represented as a state (for example, Age \<30, Age = 30, and Age >30) and the probabilities are calculated as follows: state exists (value = 1), some other state exists (value = 0), state is **Missing**. For more information about how probabilities are adjusted to represent missing values, see [Missing Values &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/missing-values-analysis-services-data-mining.md).  
  
 The probabilities for each node are calculated almost directly from the distribution, as follows:  
  
 Probability = (support for state + support for prior state) / (node support plus the prior node support)  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses probabilities for each node to compare the stored probability with the prior probability to determine whether the path from the parent to the child node indicates a strong inference.  
  
 When making predictions, the probability of the distribution must be balanced with the probability of the node, to smoothen the probabilities. For example, if a split in the tree separates cases by a ratio of 9000/1000, the tree is very unbalanced. As a result, a prediction coming from the small branch should not carry the same weight as a prediction coming from a branch with many cases.  
  
#### Variance  
 Variance is a measure of how scattered values in a sample are, given an expected distribution. For discrete values, the variance is 0 by definition.  
  
 For information about how variance is calculated for continuous values, see [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md).  
  
#### Value Type  
 The value type column provides information about the meaning of the numeric value provided in the other columns in the NODE_DISTRIBUTION table. You can use the value type in queries to retrieve specific rows from the nested tables. For examples, see [Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md).  
  
 Of the types in the <xref:Microsoft.AnalysisServices.AdomdClient.MiningValueType> enumeration, the following are used in classification trees.  
  
|Value type|Description|  
|----------------|-----------------|  
|1 (Missing)|Indicates a count, probability, or other statistic related to missing values.|  
|4 (Discrete)|Indicates a count, probability, or other statistic related to a discrete or discretized value.|  
  
 If the model includes a continuous predictable attribute, the tree might also contain value types that are unique to regression formulas. For a list of the value types that are used in regression trees, see [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md).  
  
###  <a name="NodeScore"></a> Node Score  
 The node score represents slightly different information at each level of the tree. In general, the score is a numeric value that tells you how good a split was achieved by splitting on the condition. The value is represented as a double, where a higher value is better.  
  
 By definition, the model node and all leaf nodes have a node score of 0.  
  
 For the (All) node that represents the top of each tree, the MSOLAP_NODE_SCORE column contains the best split score in the whole tree.  
  
 For all other nodes in the tree (except leaf nodes), the score for each node represents the best split score for the current node, minus the split score for the parent node. Typically, the split score for a parent node should always be better than the split score on any one of its child nodes. That is because a decision trees model ideally splits on the most important attributes first.  
  
 There are many ways of calculating a score for a split, depending on the algorithm parameter you choose. A discussion of how the scores are calculated for each of the scoring methods is beyond the scope of this topic. For more information, see "[Learning Bayesian Networks: The Combination of Knowledge and Statistical Data](http://research.microsoft.com/en-us/um/people/heckerman/hgc94uai.pdf)", on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Research Web site.  
  
> [!NOTE]  
>  If you create a decision trees model that has both continuous and discrete predictable attributes, you will see completely different scores in the (All) nodes that represent each tree type. Each model should be considered independently, and the methods used for scoring regression are completely different from those used for scoring classification. The node score values cannot be compared.  
  
 [Top](#bkmk_Top)  
  
##  <a name="bkmk_RegressionNodes"></a> Regression Nodes within a Decision Tree Model  
 If a decision trees model contains a predictable attribute with continuous numeric data, the Microsoft Decision Trees algorithm seeks to find areas in the data where the relationship between the predicted state and the input variables is linear. If the algorithm is successful in finding a linear relationship, it creates a special tree (NODE_TYPE = 25) that represents a linear regression. These regression tree nodes are more complex than nodes that represent discrete values.  
  
 In general, a regression maps the changes in the continuous dependent (predictable variable) as a function of changes in the inputs. If the dependent variable has any continuous inputs, and the relationship between the input and predicted value is stable enough to be computed as a line graph, the node for the regression contains a formula.  
  
 However, if the relationship between the input and predicted value is *nonlinear*, a split is created instead, just like a standard decision tree. For example, assume that A is the predictable attribute, and B and C are the inputs, where C is a continuous value type. If the relationship between A and C is fairly stable in parts of the data, but unstable in others, the algorithm will create splits to represent the different areas of the data.  
  
|Split condition|Result in node|  
|---------------------|--------------------|  
|if n < 5|Relationship can be expressed as equation 1|  
|if n between 5 and 10|No equation|  
|if n > 10|Relationship can be expressed as equation 2|  
  
 For more information about regression nodes, see [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md).  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)   
 [Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)   
 [Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)  
  
  
