---
title: "Microsoft Decision Trees Algorithm Technical Reference | Microsoft Docs"
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
# Microsoft Decision Trees Algorithm Technical Reference
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm is a hybrid algorithm that incorporates different methods for creating a tree, and supports multiple analytic tasks, including regression, classification, and association. The Microsoft Decision Trees algorithm supports modeling of both discrete and continuous attributes.  
  
 This topic explains the implementation of the algorithm, describes how to customize the behavior of the algorithm for different tasks, and provides links to additional information about querying decision tree models.  
  
## Implementation of the Decision Trees Algorithm  
 The Microsoft Decision Trees algorithm applies the Bayesian approach to learning causal interaction models by obtaining approximate posterior distributions for the models. For a detailed explanation of this approach, see the paper on the Microsoft Research site, by [Structure and Parameter Learning](http://go.microsoft.com/fwlink/?LinkId=237640&clcid=0x409).  
  
 The methodology for assessing the information value of the *priors* needed for learning is based on the assumption of *likelihood equivalence*. This assumption says that data should not help to discriminate network structures that otherwise represent the same assertions of conditional independence. Each case is assumed to have a single Bayesian prior network and a single measure of confidence for that network.  
  
 Using these prior networks, the algorithm then computes the relative *posterior probabilities* of network structures given the current training data, and identifies the network structures that have the highest posterior probabilities.  
  
 The Microsoft Decision Trees algorithm uses different methods to compute the best tree. The method used depends on the task, which can be linear regression, classification, or association analysis. A single model can contain multiple trees for different predictable attributes. Moreover, each tree can contain multiple branches, depending on how many attributes and values there are in the data. The shape and depth of the tree built in a particular model depends on the scoring method and other parameters that were used. Changes in the parameters can also affect where the nodes split.  
  
### Building the Tree  
 When the Microsoft Decision Trees algorithm creates the set of possible input values, it performs *feature selection* to identify the attributes and values that provide the most information, and removes from consideration the values that are very rare. The algorithm also groups values into *bins*, to create groupings of values that can be processed as a unit to optimize performance.  
  
 A tree is built by determining the correlations between an input and the targeted outcome. After all the attributes have been correlated, the algorithm identifies the single attribute that most cleanly separates the outcomes. This point of the best separation is measured by using an equation that calculates information gain. The attribute that has the best score for information gain is used to divide the cases into subsets, which are then recursively analyzed by the same process, until the tree cannot be split any more.  
  
 The exact equation used to evaluate information gain depends on the parameters set when you created the algorithm, the data type of the predictable column, and the data type of the input.  
  
### Discrete and Continuous Inputs  
 When the predictable attribute is discrete and the inputs are discrete, counting the outcomes per input is a matter of creating a matrix and generating scores for each cell in the matrix.  
  
 However, when the predictable attribute is discrete and the inputs are continuous, the input of the continuous columns are automatically discretized. You can accept the default and have [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] find the optimum number of bins, or you can control the manner in which continuous inputs are discretized by setting the <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationMethod%2A> and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn.DiscretizationBucketCount%2A> properties. For more information, see [Change the Discretization of a Column in a Mining Model](../../analysis-services/data-mining/change-the-discretization-of-a-column-in-a-mining-model.md).  
  
 For continuous attributes, the algorithm uses linear regression to determine where a decision tree splits.  
  
 When the predictable attribute is a continuous numeric data type, feature selection is applied to the outputs as well, to reduce the possible number of outcomes and build the model faster. You can change the threshold for feature selection and thereby increase or decrease the number of possible values by setting the MAXIMUM_OUTPUT_ATTRIBUTES parameter.  
  
 For a more detained explanation about how the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm works with discrete predictable columns, see [Learning Bayesian Networks: The Combination of Knowledge and Statistical Data](http://research.microsoft.com/en-us/um/people/heckerman/hgc94uai.pdf). For more information about how the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm works with a continuous predictable column, see the appendix of [Autoregressive Tree Models for Time-Series Analysis](http://go.microsoft.com/fwlink/?LinkId=45966).  
  
### Scoring Methods and Feature Selection  
 The Microsoft Decision Trees algorithm offers three formulas for scoring information gain: Shannon's entropy, Bayesian network with K2 prior, and Bayesian network with a uniform Dirichlet distribution of priors. All three methods are well established in the data mining field. We recommend that you experiment with different parameters and scoring methods to determine which provides the best results. For more information about these scoring methods, see [Feature Selection](http://msdn.microsoft.com/library/73182088-153b-4634-a060-d14d1fd23b70).  
  
 All [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data mining algorithms automatically use feature selection to improve analysis and reduce processing load. The method used for feature selection depends on the algorithm that is used to build the model. The algorithm parameters that control feature selection for a decision trees model are MAXIMUM_INPUT_ATTRIBUTES and MAXIMUM_OUTPUT.  
  
|Algorithm|Method of analysis|Comments|  
|---------------|------------------------|--------------|  
|Decision Trees|Interestingness score<br /><br /> Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|If any columns contain non-binary continuous values, the interestingness score is used for all columns, to ensure consistency. Otherwise, the default or specified method is used.|  
|Linear Regression|Interestingness score|Linear Regression only uses interestingness, because it only supports continuous columns.|  
  
### Scalability and Performance  
 Classification is an important data mining strategy. Generally, the amount of information that is needed to classify the cases grows in direct proportion to the number of input records. This limits the size of the data that can be classified. The Microsoft Decision Trees algorithm using uses the following methods to resolve these problems, improve performance, and eliminate memory restrictions:  
  
-   Feature selection to optimize the selection of attributes.  
  
-   Bayesian scoring to control tree growth.  
  
-   Optimization of binning for continuous attributes.  
  
-   Dynamic grouping of input values to determine the most important values.  
  
 The Microsoft Decision Trees algorithm is fast and scalable, and has been designed to be easily parallelized, meaning that all processors work together to build a single, consistent model. The combination of these characteristics makes the decision-tree classifier an ideal tool for data mining.  
  
 If performance constraints are severe, you might be able to improve processing time during the training of a decision tree model by using the following methods. However, if you do so, be aware that eliminating attributes to improve processing performance will change the results of the model, and possibly make it less representative of the total population.  
  
-   Increase the value of the COMPLEXITY_PENALTY parameter to limit tree growth.  
  
-   Limit the number of items in association models to limit the number of trees that are built.  
  
-   Increase the value of the MINIMUM_SUPPORT parameter to avoid overfitting.  
  
-   Restrict the number of discrete values for any attribute to 10 or less. You might try grouping values in different ways in different models.  
  
    > [!NOTE]  
    >  You can use the data exploration tools available in  [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] to visualize the distribution of values in your data and group your values appropriately before beginning data mining. For more information, see [Data Profiling Task and Viewer](../../integration-services/control-flow/data-profiling-task-and-viewer.md). You can also use the [Data Mining Add-ins for Excel 2007](http://www.microsoft.com/downloads/details.aspx?FamilyID=7C76E8DF-8674-4C3B-A99B-55B17F3C4C51), to explore, group and relabel data in Microsoft Excel.  
  
## Customizing the Decision Trees Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm supports parameters that affect the performance and accuracy of the resulting mining model. You can also set modeling flags on the mining model columns or mining structure columns to control the way that data is processed.  
  
> [!NOTE]  
>  The Microsoft Decision Trees algorithm is available in all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; however, some advanced parameters for customizing the behavior of the Microsoft Decision Trees algorithm are available for use only in specific editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2012](http://go.microsoft.com/fwlink/?linkid=232473) (http://go.microsoft.com/fwlink/?linkid=232473).  
  
### Setting Algorithm Parameters  
 The following table describes the parameters that you can use with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm.  
  
 *COMPLEXITY_PENALTY*  
 Controls the growth of the decision tree. A low value increases the number of splits, and a high value decreases the number of splits. The default value is based on the number of attributes for a particular model, as described in the following list:  
  
-   For 1 through 9 attributes, the default is 0.5.  
  
-   For 10 through 99 attributes, the default is 0.9.  
  
-   For 100 or more attributes, the default is 0.99.  
  
 *FORCE_REGRESSOR*  
 Forces the algorithm to use the specified columns as regressors, regardless of the importance of the columns as calculated by the algorithm. This parameter is only used for decision trees that are predicting a continuous attribute.  
  
> [!NOTE]  
>  By setting this parameter, you force the algorithm to try to use the attribute as a regressor. However, whether the attribute is actually used as a regressor in the final model depends on the results of analysis. You can find out which columns were used as regressors by querying the model content.  
  
 [Available only in some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ]  
  
 *MAXIMUM_INPUT_ATTRIBUTES*  
 Defines the number of input attributes that the algorithm can handle before it invokes feature selection.  
  
 The default is 255.  
  
 Set this value to 0 to turn off feature selection.  
  
 [Available only in some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]]  
  
 *MAXIMUM_OUTPUT_ATTRIBUTES*  
 Defines the number of output attributes that the algorithm can handle before it invokes feature selection.  
  
 The default is 255.  
  
 Set this value to 0 to turn off feature selection.  
  
 [Available only in some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]]  
  
 *MINIMUM_SUPPORT*  
 Determines the minimum number of leaf cases that is required to generate a split in the decision tree.  
  
 The default is 10.  
  
 You may need to increase this value if the dataset is very large, to avoid overtraining.  
  
 *SCORE_METHOD*  
 Determines the method that is used to calculate the split score. The following options are available:  
  
|ID|Name|  
|--------|----------|  
|1|Entropy|  
|3|Bayesian with K2 Prior|  
|4|Bayesian Dirichlet Equivalent (BDE) with uniform prior<br /><br /> (default)|  
  
 The default is 4, or BDE.  
  
 For an explanation of these scoring methods, see [Feature Selection](http://msdn.microsoft.com/library/73182088-153b-4634-a060-d14d1fd23b70).  
  
 *SPLIT_METHOD*  
 Determines the method that is used to split the node. The following options are available:  
  
|ID|Name|  
|--------|----------|  
|1|**Binary:** Indicates that regardless of the actual number of values for the attribute, the tree should be split into two branches.|  
|2|**Complete:** Indicates that the tree can create as many splits as there are attribute values.|  
|3|**Both:** Specifies that Analysis Services can determine whether a binary or complete split should be used to produce the best results.|  
  
 The default is 3.  
  
### Modeling Flags  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm supports the following modeling flags. When you create the mining structure or mining model, you define modeling flags to specify how values in each column are handled during analysis. For more information, see [Modeling Flags &#40;Data Mining&#41;](../../analysis-services/data-mining/modeling-flags-data-mining.md).  
  
|Modeling Flag|Description|  
|-------------------|-----------------|  
|MODEL_EXISTENCE_ONLY|Means that the column will be treated as having two possible states: **Missing** and **Existing**. A null is a missing value.<br /><br /> Applies to mining model columns.|  
|NOT NULL|Indicates that the column cannot contain a null. An error will result if Analysis Services encounters a null during model training.<br /><br /> Applies to mining structure columns.|  
  
### Regressors in Decision Tree Models  
 Even if you do not use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm, any decision tree model that has continuous numeric inputs and outputs can potentially include nodes that represent a regression on a continuous attribute.  
  
 You do not need to specify that a column of continuous numeric data represents a regressor. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm will automatically use the column as a potential regressor and partition the dataset into regions with meaningful patterns even if you do not set the REGRESSOR flag on the column.  
  
 However, you can use the FORCE_REGRESSOR parameter to guarantee that the algorithm will use a particular regressor. This parameter can be used only with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithms. When you set the modeling flag, the algorithm will try to find regression equations of the form `a*C1 + b*C2 + ...` to fit the patterns in the nodes of the tree. The sum of the residuals is calculated, and if the deviation is too great, a split is forced in the tree.  
  
 For example, if you are predicting customer purchasing behavior using **Income** as an attribute, and set the REGRESSOR modeling flag on the column, the algorithm will first try to fit the **Income** values by using a standard regression formula. If the deviation is too great, the regression formula is abandoned and the tree will be split on another attribute. The decision tree algorithm will then try to fit a regressor for income in each of the branches after the split.  
  
## Requirements  
 A decision tree model must contain a key column, input columns, and at least one predictable column.  
  
### Input and Predictable Columns  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm supports the specific input columns and predictable columns that are listed in the following table. For more information about what the content types mean when used in a mining model, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
|Column|Content types|  
|------------|-------------------|  
|Input attribute|Continuous, Cyclical, Discrete, Discretized, Key, Ordered, Table|  
|Predictable attribute|Continuous, Cyclical, Discrete, Discretized, Ordered, Table|  
  
> [!NOTE]  
>  Cyclical and Ordered content types are supported, but the algorithm treats them as discrete values and does not perform special processing.  
  
## See Also  
 [Microsoft Decision Trees Algorithm](../../analysis-services/data-mining/microsoft-decision-trees-algorithm.md)   
 [Decision Trees Model Query Examples](../../analysis-services/data-mining/decision-trees-model-query-examples.md)   
 [Mining Model Content for Decision Tree Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-decision-tree-models-analysis-services-data-mining.md)  
  
  
