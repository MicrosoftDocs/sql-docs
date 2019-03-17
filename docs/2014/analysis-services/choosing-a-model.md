---
title: "Choosing a Model | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining algorithms"
  - "mining models"
  - "mining structures"
  - "data mining models"
  - "data mining structures"
ms.assetid: 444bbf9c-cec8-460e-881d-38784fb146fa
author: minewiskan
ms.author: owend
manager: craigg
---
# Choosing a Model
  **Mining algorithm:** The data mining *algorithm* is the mechanism that creates patterns from data. The algorithm defines how data is counted, how relationships are derived, and how patterns are stored. The selection of an algorithm is partly dependent on the type of data you want to analyze. For example, some algorithms can only work with continuous numbers, whereas others can work best with a limited number of distinct values.  
  
 **Mining model:** The result of data analysis by an algorithm is saved in a *mining model*. A mining model is a collection of rules, statistics, and patterns. The *content* of the mining model depends on the algorithm that you used to process the data, but can include the following:  
  
-   If-then rules that describe how products are grouped together in a transaction.  
  
-   A decision tree that traces the paths leading to an outcome, with probabilities for the occurrence of each path.  
  
-   A mathematical model with equations either for the model as a whole, or for segments of the model.  
  
-   Collections of similar items (called *clusters* or *segments*) that are defined by the characteristics they share, and by a similarity score.  
  
-   *Nodes* in a network, connected by *edges*. The nodes represent items or groups of items. The edges are scored according to the strength of the relationships among nodes.  
  
 **Using the model:** After you have created a model, you can use the provided viewers to explore it, or you can create a query against the model. Queries can be used to:  
  
-   Predict future values.  
  
-   Generate a set of related products or recommended products.  
  
-   Return rules, patterns, or formulas in the model.  
  
-   Get metadata from the model.  
  
-   Provide probability and support scores for all or some predictions.  
  
## Types of Machine Learning Algorithms  
 Because different types of algorithms use the data differently, you must select the algorithm that is appropriate for your goals and for the data that you want to analyze when you create a model.  
  
 The Data Mining Add-ins for Excel include the following broad types of algorithms:  
  
-   *Classification algorithms*.  
  
     Predict one or more discrete variables, based on the other attributes in the dataset.  
  
-   *Regression algorithms*  
  
     Predict one or more continuous variables, such as profit or loss, based on other attributes in the dataset.  
  
-   *Segmentation algorithms*  
  
     Divide data into groups, or clusters, of items that have similar properties.  
  
-   *Association algorithms*  
  
     Find correlations between different attributes in a dataset. This kind of algorithm is most commonly used to create association rules. Association rules can be used in a market basket analysis.  
  
-   *Sequence analysis algorithms*  
  
     Summarize frequent sequences or episodes in data, such as the paths users follow while navigating a Web site.  
  
 The algorithms used by the SQL Server Data Mining Add-ins for Office are based on the algorithms provided by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. You can also use third-party algorithms that comply with the OLE DB for Data Mining specification, if the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to which you are connected has been configured to allow third-party algorithms.  
  
## Requirements  
 Each algorithm differs in what kind of data it can work with.  
  
-   A linear regression model can only model numeric values. Both your input variables and target outcomes must be continuous number types. Use a decision tree or estimation model if you must mix discrete and continuous variables.  
  
-   A Naïve Bayes model requires that all numbers be binned. If you use one of the wizards based on this algorithm, binning is automatically performed for you.  
  
-   A decision trees model can contain both discrete and continuous variables. However, numbers will be automatically binned as needed for splits in the tree.  
  
-   Neural networks and logistic regression models automatically bin numbers used either as outcomes or as input variables. If you want to group your numbers according to some other criteria, you should use the Relabel tool to create the groupings before modeling. For example, you might want to group values in an **Age** column by deciles (10-20, 21-30 and so on), rather than the statistically significant groupings found by the model (an example might be 35.6-41.8 years).  
  
-   An association model requires that data be grouped in transactions, each referring to several items or rows. If you are using the [Associate Wizard &#40;Data Mining Client for Excel&#41;](associate-wizard-data-mining-client-for-excel.md) wizard or the [Shopping Basket Analysis &#40;Table AnalysisTools for Excel&#41;](shopping-basket-analysis-table-analysistools-for-excel.md) tool, the data should be laid out as shown in the **Associate** tab of the sample workbook.  
  
     If you want to use nested tables in an external data source, you must use the [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md) modeling options to create a mining structure and mining model that is saved on the server. Excel does not support nested tables.  
  
## Feature Selection  
 Depending on the data set, the algorithm might apply *feature selection*, to weed out columns that aren't useful, and to determine which columns of data are statistically significant in relation to the outcome.  
  
 Each algorithm uses slightly different methods of feature selection (such as entropy, or various information scores) to determine which trends are important and which differences can be discarded.  
  
 In the Data Mining Add-ins for Excel, feature selection is automatically applied, using the scoring method appropriate for each algorithm. If you want to affect the results of feature selection, use the wizards in the **Data Mining** ribbon, and click **Advanced** to set parameters using the **Algorithm Parameters** dialog box.  
  
 For a list of the feature selection methods used by each algorithm see the topic on [Feature Selection &#40;Data Mining&#41;](data-mining/feature-selection-data-mining.md) in SQL Server Books Online.  
  
## List of Supported Algorithms  
 The following algorithms are provided by default.  
  
|Algorithm Name|Description|Used in|  
|--------------------|-----------------|-------------|  
|Microsoft Association Rules|Builds rules that describe which items are likely to appear together in a transaction.|[Associate Wizard &#40;Data Mining Client for Excel&#41;](associate-wizard-data-mining-client-for-excel.md)<br /><br /> [Shopping Basket Analysis &#40;Table AnalysisTools for Excel&#41;](shopping-basket-analysis-table-analysistools-for-excel.md)|  
|Microsoft Clustering|Identifies relationships in a dataset that you might not logically derive through casual observation. Uses iterative techniques to group records into clusters that contain similar characteristics.|[Detect Categories &#40;Table Analysis Tools for Excel&#41;](detect-categories-table-analysis-tools-for-excel.md)<br /><br /> [Cluster Wizard &#40;Data Mining Add-ins for Excel&#41;](cluster-wizard-data-mining-add-ins-for-excel.md)|  
|Microsoft Decision Trees|Makes predictions based on the relationships between columns in the dataset, and models the relationships as a tree-like series of splits on specific values.<br /><br /> Supports the prediction of both discrete and continuous attributes.|[Classify Wizard &#40;Data Mining Add-ins for Excel&#41;](classify-wizard-data-mining-add-ins-for-excel.md)<br /><br /> [Estimate Wizard &#40;Data Mining Add-ins for Excel&#41;](estimate-wizard-data-mining-add-ins-for-excel.md)|  
|Microsoft Linear Regression|If there is a linear dependency between the target variable and the variables being examined, finds the most efficient relationship between the target and its inputs.<br /><br /> Supports prediction of continuous attributes.|This algorithm is available in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. In the Data Mining Add-ins for Office, you can create a model that uses this algorithm by creating a structure and then manually adding a model.<br /><br /> For more information, see [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md).|  
|Microsoft Logistic Regression|Analyzes the factors that contribute to an outcome, where the outcome is restricted to two values, usually the occurrence or non-occurrence of an event.<br /><br /> Supports the prediction of both discrete and continuous attributes.|[Fill From Example &#40;Table Analysis Tools for Excel&#41;](fill-from-example-table-analysis-tools-for-excel.md)<br /><br /> [Goal Seek Scenario &#40;Table Analysis Tools for Excel&#41;](goal-seek-scenario-table-analysis-tools-for-excel.md)<br /><br /> [What-If Scenario &#40;Table Analysis Tools for Excel&#41;](what-if-scenario-table-analysis-tools-for-excel.md)<br /><br /> [Prediction Calculator &#40;Table Analysis Tools for Excel&#41;](prediction-calculator-table-analysis-tools-for-excel.md)|  
|Microsoft Naïve Bayes|Finds the probability of the relationship between all input and predictable columns. This algorithm is useful for quickly generating mining models to discover relationships.<br /><br /> Supports only discrete or discretized attributes.<br /><br /> Treats all input attributes as independent.|[Analyze Key Influencers &#40;Table Analysis Tools for Excel&#41;](analyze-key-influencers-table-analysis-tools-for-excel.md)|  
|Microsoft Neural Network|Analyzes complex input data or business problems for which a significant quantity of training data is available but for which rules cannot be easily derived by using other algorithms.<br /><br /> Can predict multiple attributes.<br /><br /> Can be used to classify discrete attributes and regression of continuous attributes.|This algorithm is available in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. In the Data Mining Add-ins for Office, you can create a model that uses this algorithm by creating a structure and then manually adding a model.<br /><br /> For more information, see [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md).|  
|Microsoft Sequence Clustering|Identifies clusters of similarly ordered events in a sequence.<br /><br /> Provides a combination of sequence analysis and clustering.|This algorithm is available only in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. However, in the Data Mining Add-ins for Office, you can create a model that uses this algorithm by creating a structure and then manually adding a model.<br /><br /> For more information, see [Advanced Modeling &#40;Data Mining Add-ins for Excel&#41;](advanced-modeling-data-mining-add-ins-for-excel.md).|  
|Microsoft Time Series|Analyzes time-related data by using a linear decision tree.<br /><br /> Patterns can be used to predict future values in the time series.|[Forecast &#40;Table Analysis Tools for Excel&#41;](forecast-table-analysis-tools-for-excel.md)<br /><br /> [Forecast Wizard &#40;Data Mining Add-ins for Excel&#41;](forecast-wizard-data-mining-add-ins-for-excel.md)|  
  
## See Also  
 [What's Included in the Data Mining Add-Ins for Office](what-s-included-in-the-data-mining-add-ins-for-office.md)  
  
  
