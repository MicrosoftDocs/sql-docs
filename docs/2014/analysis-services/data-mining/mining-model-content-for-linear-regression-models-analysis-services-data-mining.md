---
title: "Mining Model Content for Linear Regression Models (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "linear regression algorithms [Analysis Services]"
  - "mining model content, linear regression models"
  - "regression algorithms [Analysis Services]"
ms.assetid: a6abcb75-524e-4e0a-a375-c10475ac0a9d
author: minewiskan
ms.author: owend
manager: craigg
---
# Mining Model Content for Linear Regression Models (Analysis Services - Data Mining)
  This topic describes mining model content that is specific to models that use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm. For a general explanation of mining model content for all model types, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
## Understanding the Structure of a Linear Regression Model  
 A linear regression model has an extremely simple structure. Each model has a single parent node that represents the model and its metadata, and a regression tree node (NODE_TYPE = 25) that contains the regression formula for each predictable attribute.  
  
 ![Structure of model for linear regression](../media/modelcontentstructure-linreg.gif "Structure of model for linear regression")  
  
 Linear regression models use the same algorithm as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees, but different parameters are used to constrain the tree, and only continuous attributes are accepted as inputs. However, because linear regression models are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm, linear regression models are displayed by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Tree Viewer. For information, see [Browse a Model Using the Microsoft Tree Viewer](browse-a-model-using-the-microsoft-tree-viewer.md).  
  
 The next section explains how to interpret information in the regression formula node. This information applies not only to linear regression models, but also to decision trees models that contain regressions in a portion of the tree.  
  
## Model Content for a Linear Regression Model  
 This section provides detail and examples only for those columns in the mining model content that have particular relevance for linear regression.  
  
 For information about general-purpose columns in the schema rowset, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md).  
  
 MODEL_CATALOG  
 Name of the database where the model is stored.  
  
 MODEL_NAME  
 Name of the model.  
  
 ATTRIBUTE_NAME  
 **Root node:** Blank  
  
 **Regression node:** The name of the predictable attribute.  
  
 NODE_NAME  
 Always same as NODE_UNIQUE_NAME.  
  
 NODE_UNIQUE_NAME  
 A unique identifier for the node within the model. This value cannot be changed.  
  
 NODE_TYPE  
 A linear regression model outputs the following node types:  
  
|Node Type ID|Type|Description|  
|------------------|----------|-----------------|  
|25|Regression tree root|Contains the formula that describes the relationship between the input and output variable.|  
  
 NODE_CAPTION  
 A label or a caption associated with the node. This property is primarily for display purposes.  
  
 **Root node:** Blank  
  
 **Regression node:** All.  
  
 CHILDREN_CARDINALITY  
 An estimate of the number of children that the node has.  
  
 **Root node:** Indicates the number of regression nodes. One regression node is created for each predictable attribute in the model.  
  
 **Regression node:** Always 0.  
  
 PARENT_UNIQUE_NAME  
 The unique name of the node's parent. NULL is returned for any nodes at the root level.  
  
 NODE_DESCRIPTION  
 A description of the node.  
  
 **Root node:** Blank  
  
 **Regression node:** All.  
  
 NODE_RULE  
 Not used for linear regression models.  
  
 MARGINAL_RULE  
 Not used for linear regression models.  
  
 NODE_PROBABILITY  
 The probability associated with this node.  
  
 **Root node:** 0  
  
 **Regression node:** 1  
  
 MARGINAL_PROBABILITY  
 The probability of reaching the node from the parent node.  
  
 **Root node:** 0  
  
 **Regression node:** 1  
  
 NODE_DISTRIBUTION  
 A nested table that provides statistics about the values in the node.  
  
 **Root node:** 0  
  
 **Regression node:** A table that contains the elements used to build the regression formula. A regression node contains the following value types:  
  
|VALUETYPE|  
|---------------|  
|1 (Missing)|  
|3 (Continuous)|  
|7 (Coefficient)|  
|8 (Score Gain)|  
|9 (Statistics)|  
|11 (Intercept)|  
  
 NODE_SUPPORT  
 The number of cases that support this node.  
  
 **Root node:** 0  
  
 **Regression node:** Count of training cases.  
  
 MSOLAP_MODEL_COLUMN  
 Name of predictable attribute.  
  
 MSOLAP_NODE_SCORE  
 Same as NODE_PROBABILITY  
  
 MSOLAP_NODE_SHORT_CAPTION  
 Label used for display purposes.  
  
## Remarks  
 When you create a model by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm, the data mining engine creates a special instance of a decision trees model and supplies parameters that constrain the tree to contain all the training data in a single node. All continuous inputs are flagged and evaluated as potential regressors, but only those regressors that fit the data are retained as regressors in the final model. The analysis produces either a single regression formula for each regressor or no regression formula at all.  
  
 You can view the complete regression formula in the **Mining Legend**, by clicking the **(All)** node in the [Microsoft Tree Viewer](browse-a-model-using-the-microsoft-tree-viewer.md).  
  
 Also, when you create a decision trees model that includes a continuous predictable attribute, sometimes the tree has regression nodes that share the properties of regression tree nodes.  
  
##  <a name="NodeDist_Regression"></a> Node Distribution for Continuous Attributes  
 Most of the important information in a regression node is contained in the NODE_DISTRIBUTION table. The following example illustrates the layout of the NODE_DISTRIBUTION table. In this example, the Targeted Mailing mining structure has been used to create a linear regression model that predicts customer income based on age. The model is for the purpose of illustration only, because it can be built easily using the existing [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample data and mining structure.  
  
|ATTRIBUTE_NAME|ATTRIBUTE_VALUE|SUPPORT|PROBABILITY|VARIANCE|VALUETYPE|  
|---------------------|----------------------|-------------|-----------------|--------------|---------------|  
|Yearly Income|Missing|0|0.000457142857142857|0|1|  
|Yearly Income|57220.8876687257|17484|0.999542857142857|1041275619.52776|3|  
|Age|471.687717702463|0|0|126.969442359327|7|  
|Age|234.680904692439|0|0|0|8|  
|Age|45.4269617936399|0|0|126.969442359327|9|  
||35793.5477381267|0|0|1012968919.28372|11|  
  
 The NODE_DISTRIBUTION table contains multiple rows, each grouped by a variable. The first two rows are always value types 1 and 3, and describe the target attribute. The succeeding rows provide details about the formula for a particular *regressor*. A regressor is an input variable that has a linear relationship with the output variable. You can have multiple regressors, and each regressor will have a separate row for the coefficient (VALUETYPE = 7), score gain (VALUETYPE = 8), and statistics (VALUETYPE = 9). Finally, the table has a row that contains the intercept of the equation (VALUETYPE = 11).  
  
### Elements of the Regression Formula  
 The nested NODE_DISTRIBUTION table contains each element of the regression formula in a separate row. The first two rows of data in the example results contain information about the predictable attribute, **Yearly Income**, which models the dependent variable. The SUPPORT column shows the count of cases in support of the two states of this attribute: either a **Yearly Income** value was available, or the **Yearly Income** value was missing.  
  
 The VARIANCE column tells you the computed variance of the predictable attribute. *Variance* is a measure of how scattered the values are in a sample, given an expected distribution. Variance here is calculated by taking the average of the squared deviation from the mean. The square root of the variance is also known as standard deviation. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not provide the standard deviation but you can easily calculate it.  
  
 For each regressor, three rows are output. They contain the coefficient, score gain, and regressor statistics.  
  
 Finally, the table contains a row that provides the intercept for the equation.  
  
#### Coefficient  
 For each regressor, a coefficient (VALUETYPE = 7) is calculated. The coefficient itself appears in the ATTRIBUTE_VALUE column, whereas the VARIANCE column tells you the variance for the coefficient. The coefficients are calculated so as to maximize linearity.  
  
#### Score Gain  
 The score gain (VALUETYPE = 8) for each regressor represents the interestingness score of the attribute. You can use this value to estimate the usefulness of multiple regressors.  
  
#### Statistics  
 The regressor statistic (VALUETYPE = 9) is the mean for the attribute for cases that have a value. The ATTRIBUTE_VALUE column contains the mean itself, whereas the VARIANCE column contains the sum of deviations from the mean.  
  
#### Intercept  
 Normally, the *intercept* (VALUETYPE = 11) or *residual* in a regression equation tells you the value of the predictable attribute, at the point where the input attribute, is 0. In many cases, this might not happen, and could lead to counterintuitive results.  
  
 For example, in a model that predicts income based on age, it is useless to learn the income at age 0. In real-life, it is typically more useful to know about the behavior of the line with respect to average values. Therefore, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] modifies the intercept to express each regressor in a relationship with the mean.  
  
 This adjustment is difficult to see in the mining model content, but is apparent if you view the completed equation in the **Mining Legend** of the **Microsoft Tree Viewer**. The regression formula is shifted away from the 0 point to the point that represents the mean. This presents a view that is more intuitive given the current data.  
  
 Therefore, assuming that the mean age is around 45, the intercept (VALUETYPE = 11) for the regression formula tells you the mean income.  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](mining-model-content-analysis-services-data-mining.md)   
 [Microsoft Linear Regression Algorithm](microsoft-linear-regression-algorithm.md)   
 [Microsoft Linear Regression Algorithm Technical Reference](microsoft-linear-regression-algorithm-technical-reference.md)   
 [Linear Regression Model Query Examples](linear-regression-model-query-examples.md)  
  
  
