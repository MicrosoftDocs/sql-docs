---
title: "Microsoft Linear Regression Algorithm Technical Reference | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "AUTO_DETECT_PERIODICITY parameter"
  - "linear regression algorithms [Analysis Services]"
  - "regression algorithms [Analysis Services]"
ms.assetid: 7807b5ff-8e0d-418d-a05b-b1a9644536d2
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Linear Regression Algorithm Technical Reference
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm is a special version of the Microsoft Decision Trees algorithm that is optimized for modeling pairs of continuous attributes. This topic explains the implementation of the algorithm, describes how to customize the behavior of the algorithm, and provides links to additional information about querying models.  
  
## Implementation of the Linear Regression Algorithm  
 The Microsoft Decision Trees algorithm can be used for many tasks: linear regression, classification, or association analysis. To implement this algorithm for the purpose of linear regression, the parameters of the algorithm are controlled to restrict the growth of the tree and keep all data in the model in a single node. In other words, although linear regression is based on a decision tree, the tree contains only a single root and no branches: all data resides in the root node.  
  
 To accomplish this, the algorithm's *MINIMUM_LEAF_CASES* parameter is set to be greater than or equal to the total number of cases that the algorithm uses to train the mining model. With the parameter set in this way, the algorithm will never create a split, and therefore performs a linear regression.  
  
 The equation that represents the regression line takes the general form of y = ax + b, and is known as the regression equation. The variable Y represents the output variable, X represents the input variable, and a and b are adjustable coefficients. You can retrieve the coefficients, intercepts, and other information about the regression formula by querying the completed mining model. For more information, see [Linear Regression Model Query Examples](linear-regression-model-query-examples.md).  
  
### Scoring Methods and Feature Selection  
 All [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data mining algorithms automatically use feature selection to improve analysis and reduce processing load. The method used for feature selection in linear regression is the interestingness score, because the model supports only supports continuous columns. For reference, the following table shows the difference in feature selection for the Linear Regression algorithm and the Decision Trees algorithm.  
  
|Algorithm|Method of analysis|Comments|  
|---------------|------------------------|--------------|  
|Linear Regression|Interestingness score|Default.<br /><br /> Other feature selection methods that are available with the Decision Trees algorithm apply to discrete variables only and therefore are not applicable to linear regression models.|  
|Decision Trees|Interestingness score<br /><br /> Shannon's Entropy<br /><br /> Bayesian with K2 Prior<br /><br /> Bayesian Dirichlet with uniform prior (default)|If any columns contain non-binary continuous values, the interestingness score is used for all columns, to ensure consistency. Otherwise, the default or specified method is used.|  
  
 The algorithm parameters that control feature selection for a decision trees model are MAXIMUM_INPUT_ATTRIBUTES and MAXIMUM_OUTPUT.  
  
## Customizing the Linear Regression Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm supports parameters that affect the behavior, performance, and accuracy of the resulting mining model. You can also set modeling flags on the mining model columns or mining structure columns to control the way that data is processed.  
  
### Setting Algorithm Parameters  
 The following table lists the parameters that are provided for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm.  
  
|Parameter|Description|  
|---------------|-----------------|  
|*MAXIMUM_INPUT_ATTRIBUTES*|Defines the number of input attributes that the algorithm can handle before it invokes feature selection. Set this value to 0 to turn off feature selection.<br /><br /> The default is 255.|  
|*MAXIMUM_OUTPUT_ATTRIBUTES*|Defines the number of output attributes that the algorithm can handle before it invokes feature selection. Set this value to 0 to turn off feature selection.<br /><br /> The default is 255.|  
|*FORCE_REGRESSOR*|Forces the algorithm to use the indicated columns as regressors, regardless of the importance of the columns as calculated by the algorithm.|  
  
### Modeling Flags  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm supports the following modeling flags. When you create the mining structure or mining model, you define modeling flags to specify how values in each column are handled during analysis. For more information, see [Modeling Flags &#40;Data Mining&#41;](modeling-flags-data-mining.md).  
  
|Modeling Flag|Description|  
|-------------------|-----------------|  
|NOT NULL|Indicates that the column cannot contain a null. An error will result if Analysis Services encounters a null during model training.<br /><br /> Applies to mining structure columns.|  
|REGRESSOR|Indicates that the column contains continuous numeric values that should be treated as potential independent variables during analysis.<br /><br /> Note: Flagging a column as a regressor does not ensure that the column will be used as a regressor in the final model.<br /><br /> Applies to mining model columns.|  
  
### Regressors in Linear Regression Models  
 Linear regression models are based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm. However, even if you do not use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm, any decision tree model can contain a tree or nodes that represent a regression on a continuous attribute.  
  
 You do not need to specify that a continuous column represents a regressor. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm will partition the dataset into regions with meaningful patterns even if you do not set the REGRESSOR flag on the column. The difference is that when you set the modeling flag, the algorithm will try to find regression equations of the form a*C1 + b\*C2 + ... to fit the patterns in the nodes of the tree. The sum of the residuals is calculated, and if the deviation is too great, a split is forced in the tree.  
  
 For example, if you are predicting customer purchasing behavior using **Income** as an attribute, and set the REGRESSOR modeling flag on the column, the algorithm would first try to fit the **Income** values by using a standard regression formula. If the deviation is too great, the regression formula is abandoned and the tree would be split on some other attribute. The decision tree algorithm would then try to fit a regressor for income in each of the branches after the split.  
  
 You can use the FORCED_REGRESSOR parameter to guarantee that the algorithm will use a particular regressor. This parameter can be used with the Microsoft Decision Trees and Microsoft Linear Regression algorithms.  
  
## Requirements  
 A linear regression model must contain a key column, input columns, and at least one predictable column.  
  
### Input and Predictable Columns  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm supports the specific input columns and predictable columns that are listed in the following table. For more information about what the content types mean when used in a mining model, see [Content Types &#40;Data Mining&#41;](content-types-data-mining.md).  
  
|Column|Content types|  
|------------|-------------------|  
|Input attribute|Continuous, Cyclical, Key, Table, and Ordered|  
|Predictable attribute|Continuous, Cyclical, and Ordered|  
  
> [!NOTE]  
>  `Cyclical` and `Ordered` content types are supported, but the algorithm treats them as discrete values and does not perform special processing.  
  
## See Also  
 [Microsoft Linear Regression Algorithm](microsoft-linear-regression-algorithm.md)   
 [Linear Regression Model Query Examples](linear-regression-model-query-examples.md)   
 [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)  
  
  
