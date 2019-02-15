---
title: "Microsoft Association Algorithm Technical Reference | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "MINIMUM_ITEMSET_SIZE parameter"
  - "MAXIMUM_SUPPORT parameter"
  - "association algorithms [Analysis Services]"
  - "MINIMUM_SUPPORT parameter"
  - "OPTIMIZED_PREDICTION_COUNT parameter"
  - "associations [Analysis Services]"
  - "MAXIMUM_ITEMSET_COUNT parameter"
  - "MAXIMUM_ITEMSET_SIZE parameter"
  - "MINIMUM_PROBABILITY parameter"
ms.assetid: 50a22202-e936-4995-ae1d-4ff974002e88
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Association Algorithm Technical Reference
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm is a straightforward implementation of the well-known Apriori algorithm.  
  
 Both the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees algorithm and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm can be used to analyze associations, but the rules that are found by each algorithm can differ. In a decision trees model, the splits that lead to specific rules are based on information gain, whereas in an association model, rules are based completely on confidence. Therefore, in an association model, a strong rule, or one that has high confidence, might not necessarily be interesting because it does not provide new information.  
  
## Implementation of the Microsoft Association Algorithm  
 The Apriori algorithm does not analyze patterns, but rather generates and then counts *candidate itemsets*. An item can represent an event, a product, or the value of an attribute, depending on the type of data that is being analyzed.  
  
 In the most common type of association model Boolean variables, representing a Yes/No or Missing/Existing value, are assigned to each attribute, such as a product or event name. A market basket analysis is an example of an association rules model that uses Boolean variables to represent the presence or absence of particular products in a customer's shopping basket.  
  
 For each itemset, the algorithm then creates scores that represent support and confidence. These scores can be used to rank and derive interesting rules from the itemsets.  
  
 Association models can also be created for numerical attributes. If the attributes are continuous, the numbers can be *discretized, or* grouped in buckets. The discretized values can then be handled either as Booleans or as attribute-value pairs.  
  
### Support, Probability, and Importance  
 *Support*, which issometimes referred to as *frequency*, means the number of cases that contain the targeted item or combination of items. Only items that have at least the specified amount of support can be included in the model.  
  
 A *frequent itemset* refers to a collection of items where the combination of items also has support above the threshold defined by the MINIMUM_SUPPORT parameter. For example, if the itemset is {A,B,C} and the MINIMUM_SUPPORT value is 10, each individual item A, B, and C must be found in at least 10 cases to be included in the model, and the combination of items {A,B,C} must also be found in at least 10 cases.  
  
 **Note** You can also control the number of itemsets in a mining model by specifying the maximum length of an itemset, where length means the number of items.  
  
 By default, the support for any particular item or itemset represents a count of the cases that contain that item or items. However, you can also express MINIMUM_SUPPORT as a percentage of the total cases in the data set, by typing the number as a decimal value less than 1. For example, if you specify a MINIMUM_SUPPORT value of 0.03, it means that at least 3% of the total cases in the data set must contain this item or itemset for inclusion in the model. You should experiment with your model to determine whether using a count or percentage makes more sense.  
  
 In contrast, the threshold for rules is expressed not as a count or percentage, but as a probability, sometimes referred to as *confidence*. For example, if the itemset {A,B,C} occurs in 50 cases, but the itemset {A,B,D} also occurs in 50 cases, and the itemset {A,B} in another 50 cases, it is obvious that {A,B} is not a strong predictor of {C}. Therefore, to weight a particular outcomes against all known outcomes, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] calculates the probability of the individual rule (such as If {A,B} Then {C}) by dividing the support for the itemset {A,B,C} by the support for all related itemsets.  
  
 You can restrict the number of rules that a model produces by setting a value for MINIMUM_PROBABILITY.  
  
 For each rule that is created, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] outputs a score that indicates its *importance*, which is alsoreferred to as *lift*. Lift Importance is calculated differently for itemsets and rules.  
  
 The importance of an itemset is calculated as the probability of the itemset divided by the compound probability of the individual items in the itemset. For example, if an itemset contains {A,B}, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] first counts all the cases that contain this combination A and B, and divides that by the total number of cases, and then normalizes the probability.  
  
 The importance of a rule is calculated by the log likelihood of the right-hand side of the rule, given the left-hand side of the rule. For example, in the rule `If {A} Then {B}`, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] calculates the ratio of cases with A and B over cases with B but without A, and then normalizes that ratio by using a logarithmic scale.  
  
### Feature Selection  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm does not perform any kind of automatic feature selection. Instead, the algorithm provides parameters that control the data that is used by the algorithm. This might include limits on the size of each itemset, or setting the maximum and minimum support required to add an itemset to the model.  
  
-   To filter out items and events that are too common and therefore uninteresting, decrease the value of MAXIMUM_SUPPORT to remove very frequent itemsets from the model.  
  
-   To filter out items and itemsets that are rare, increase the value of MINIMUM_SUPPORT.  
  
-   To filter out rules, increase the value of MINIMUM_PROBABILITY.  
  
## Customizing the Microsoft Association Rules Algorithm  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm supports several parameters that affect the behavior, performance, and accuracy of the resulting mining model.  
  
### Setting Algorithm Parameters  
 You can change the parameters for a mining model at any time by using the Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can also change parameters programmatically by using the <xref:Microsoft.AnalysisServices.MiningModel.AlgorithmParameters%2A> collection in AMO, or by using the [MiningModels Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/collections/miningmodels-element-assl) in XMLA. The following table describes each parameter.  
  
> [!NOTE]  
>  You cannot change the parameters in an existing model by using a DMX statement; you must specify the parameters in the DMX CREATE MODEL or ALTER STRUCTURE... ADD MODEL when you create the model.  
  
 *MAXIMUM_ITEMSET_COUNT*  
 Specifies the maximum number of itemsets to produce. If no number is specified, the default value is used.  
  
 The default is 200000.  
  
> [!NOTE]  
>  Itemsets are ranked by support. Among itemsets that have the same support, ordering is arbitrary.  
  
 *MAXIMUM_ITEMSET_SIZE*  
 Specifies the maximum number of items that are allowed in an itemset. Setting this value to 0 specifies that there is no limit to the size of the itemset.  
  
 The default is 3.  
  
> [!NOTE]  
>  Decreasing this value can potentially reduce the time that is required for creating the model, because processing of the model stops when the limit is reached.  
  
 *MAXIMUM_SUPPORT*  
 Specifies the maximum number of cases that an itemset has for support. This parameter can be used to eliminate items that appear frequently and therefore potentially have little meaning.  
  
 If this value is less than 1, the value represents a percentage of the total cases. Values greater than 1 represent the absolute number of cases that can contain the itemset.  
  
 The default is 1.  
  
 *MINIMUM_ITEMSET_SIZE*  
 Specifies the minimum number of items that are allowed in an itemset. If you increase this number, the model might contain fewer itemsets. This can be useful if you want to ignore single-item itemsets, for example.  
  
 The default is 1.  
  
> [!NOTE]  
>  You cannot reduce model processing time by increasing the minimum value, because [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] must calculate probabilities for single items anyway as part of processing. However, by setting this value higher you can filter out smaller itemsets.  
  
 *MINIMUM_PROBABILITY*  
 Specifies the minimum probability that a rule is true.  
  
 For example, if you set this value to 0.5, it means that no rule with less than fifty percent probability can be generated.  
  
 The default is 0.4.  
  
 *MINIMUM_SUPPORT*  
 Specifies the minimum number of cases that must contain the itemset before the algorithm generates a rule.  
  
 If you set this value to less than 1, the minimum number of cases is calculated as a percentage of the total cases.  
  
 If you set this value to a whole number greater than 1, specifies the minimum number of cases is calculated as a count of cases that must contain the itemset. The algorithm might automatically increase the value of this parameter if memory is limited.  
  
 The default is 0.03. This means that to be included in the model, an itemset must be found in at least 3% of cases.  
  
 *OPTIMIZED_PREDICTION_COUNT*  
 Defines the number of items to be cached for optimizing prediction.  
  
 The default value is 0. When the default is used, the algorithm will produce as many predictions as requested in the query.  
  
 If you specify a nonzero value for *OPTIMIZED_PREDICTION_COUNT,* prediction queries can return at most the specified number of items, even if you request additional predictions. However, setting a value can improve prediction performance.  
  
 For example, if the value is set to 3, the algorithm caches only 3 items for prediction. You cannot see additional predictions that might be equally probable to the 3 items that are returned.  
  
### Modeling Flags  
 The following modeling flags are supported for use with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm.  
  
 NOT NULL  
 Indicates that the column cannot contain a null. An error will result if [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] encounters a null during model training.  
  
 Applies to the mining structure column.  
  
 MODEL_EXISTENCE_ONLY  
 Means that the column will be treated as having two possible states: `Missing` and `Existing`. A null is a missing value.  
  
 Applies to the mining model column.  
  
## Requirements  
 An association model must contain a key column, input columns, and a single predictable column.  
  
### Input and Predictable Columns  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Association Rules algorithm supports the specific input columns and predictable columns that are listed in the following table. For more information about the meaning of content types in a mining model, see [Content Types &#40;Data Mining&#41;](content-types-data-mining.md).  
  
|Column|Content types|  
|------------|-------------------|  
|Input attribute|Cyclical, Discrete, Discretized, Key, Table, Ordered|  
|Predictable attribute|Cyclical, Discrete, Discretized, Table, Ordered|  
  
> [!NOTE]  
>  Cyclical and Ordered content types are supported, but the algorithm treats them as discrete values and does not perform special processing.  
  
## See Also  
 [Microsoft Association Algorithm](microsoft-association-algorithm.md)   
 [Association Model Query Examples](association-model-query-examples.md)   
 [Mining Model Content for Association Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-association-models-analysis-services-data-mining.md)  
  
  
