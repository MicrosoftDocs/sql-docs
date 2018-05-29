---
title: "Missing Values (Analysis Services - Data Mining) | Microsoft Docs"
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
# Missing Values (Analysis Services - Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Handling  *missing values* correctly is an important part of effective modeling. This section explains what missing values are, and describes the features provided in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to work with missing values when building data mining structures and mining models.  
  
## Definition of Missing Values in Data Mining  
 A missing value can signify a number of different things. Perhaps the field was not applicable, the event did not happen, or the data was not available. It could be that the person who entered the data did not know the right value, or did not care if a field was not filled in.  
  
 However, there are many data mining scenarios in which missing values provide important information. The meaning of the missing values depends largely on context. For example, a missing value for the date in a list of invoices has a meaning substantially different from the lack of a date in column that indicates an employee hire date. Generally, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] treats missing values as informative and adjusts the probabilities to incorporate the missing values into its calculations. By doing so, you can ensure that models are balanced and do not weight existing cases too heavily.  
  
 Therefore, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides two distinctly different mechanisms for managing and calculating missing values. The first method controls the handling of nulls at the level of the mining structure. The second method differs in implementation for each algorithm, but generally defines how missing values are processed and counted in models that permit null values.  
  
## Specifying Handling of Nulls  
 In your data source, missing values might be represented in many ways: as nulls, as empty cells in a spreadsheet, as the value N/A or some other code, or as an artificial value such as 9999. However, for purposes of data mining, only nulls are considered missing values. If your data contains placeholder values instead of nulls, they can affect the results of the model, so you should replace them with nulls or infer correct values if possible. There are a variety of tools that you can use to infer and fill in appropriate values, such as the Lookup transformation or the Data Profiler task in SQL Server Integration Services, or the Fill By Example tool provided in the Data Mining Add-Ins for Excel.  
  
 If the task that you are modeling specifies that a column must never have missing values, you should apply the **NOT_NULL** modeling flag to the column when you define the mining structure. This flag indicates that processing should fail if a case does not have an appropriate value. If this error occurs when processing a model, you can log the error and take steps to correct the data that is supplied to the model.  
  
## Calculation of the Missing State  
 To the data mining algorithm, missing values are informative. In case tables, **Missing** is a valid state like any other. Moreover, a data mining model can use other values to predict whether a value is missing. In other words, the fact that a value is missing is not an error.  
  
 When you create a mining model, a **Missing** state is automatically added to the model for all discrete columns. For example, if the input column [Gender] contains two possible values, Male and Female, a third value is automatically added to represent the **Missing** value, and the histogram that shows the distribution of all values for the column always includes a count of the cases with **Missing** values. If the Gender column is not missing any values, the histogram shows that the Missing state is found in 0 cases.  
  
 The rationale for including the **Missing** state by default becomes clear when you consider that your data might not have examples of all possible values, and you would not want the model to exclude the possibility just because there was no example in the data. For example, if sales data for a store showed that all customers who purchased a certain product happened to be women, you would not want to create a model that predicts that only women could purchase the product. Instead, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] adds a placeholder for the extra unknown value, called **Missing**, as a way of accommodating possible other states.  
  
 For example, the following table shows the distribution of values for the (All) node in the decision tree model created for the Bike Buyer tutorial. In the example scenario, the [Bike Buyer] column is the predictable attribute, where 1 indicates "Yes" and 0 indicates "No".  
  
|Value|Cases|  
|-----------|-----------|  
|0|9296|  
|1|9098|  
|Missing|0|  
  
 This distribution shows that about half of the customers have purchased a bike, and half have not. This particular data set is very clean; therefore, every case has a value in the [Bike Buyer] column, and the count of **Missing** values is 0. However, if any case had a null in the [Bike Buyer] field, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] would count that row as a case with a **Missing** value.  
  
 If the input is a continuous column, the model tabulates two possible states for the attribute: **Existing** and **Missing**. In other words, either the column contains a value of some numeric data type, or it contains no value. For cases that have a value, the model calculates mean, standard deviation, and other meaningful statistics. For cases that have no value, the model provides a count of the **Missing** vales and adjusts predictions accordingly. The method for adjusting the prediction differs depending on the algorithm and is described in the following section.  
  
> [!NOTE]  
>  For attributes in a nested table, missing values are not informative. For example, if a customer has not purchased a product, the nested **Products** table would not have a row corresponding to that product, and the mining model would not create an attribute for the missing product. However, if you are interested in customers who have not purchased certain products, you can create a model that is filtered on the non-existence of the products in the nested table, by using a NOT EXISTS statement in the model filter. For more information, see [Apply a Filter to a Mining Model](../../analysis-services/data-mining/apply-a-filter-to-a-mining-model.md).  
  
## Adjusting Probability for Missing States  
 In addition to counting values, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] calculates the probability of any value across the data set. The same is true for the **Missing** value. For example, the following table shows the probabilities for the cases in the previous example:  
  
|Value|Cases|Probability|  
|-----------|-----------|-----------------|  
|0|9296|50.55%|  
|1|9098|49.42%|  
|Missing|0|0.03%|  
  
 It may seem odd that the probability of the **Missing** value is calculated as 0.03%, when the number of cases is 0. In fact, this behavior is by design, and represents an adjustment that lets the model handle unknown values gracefully.  
  
 In general, probability is calculated as the favorable cases divided by all possible cases. In this example, the algorithm computes the sum of the cases that meet a particular condition ([Bike Buyer] = 1, or [Bike Buyer] = 0), and divides that number by the total count of rows. However, to account for the **Missing** cases, 1 is added to the number of all possible cases. As a result, the probability for the unknown case is no longer zero, but a very small number, indicating that the state is merely improbable, not impossible.  
  
 The addition of the small **Missing** value does not change the outcome of the predictor; however, it enables better modeling in scenarios where the historical data does not include all possible outcomes.  
  
> [!NOTE]  
>  Data mining providers differ in the way they handle missing values. For example, some providers assume that missing data in a nested column is sparse representation, but that missing data in a non-nested column is missing at random.  
  
 If you are certain that all outcomes are specified in your data and want to prevent probabilities from being adjusted, you should set the NOT_NULL modeling flag on the column in the mining structure.  
  
> [!NOTE]  
>  Each algorithm, including custom algorithms that you may have obtained from a third-party plug-in, can handle missing values differently.  
  
### Special Handling of Missing Values in Decision Tree Models  
 The Microsoft Decision Trees algorithm calculates probabilities for missing values differently than in other algorithms. Instead of just adding 1 to the total number of cases, the decision trees algorithm adjusts for the **Missing** state by using a slightly different formula.  
  
 In a decision tree model, the probability of the **Missing** state is calculated as follows:  
  
 StateProbability = (NodePriorProbability)* (StateSupport + 1) / (NodeSupport + TotalStates)  
  
The Decision Trees algorithm provides an additional adjustment that helps the algorithm compensate for the presence of filters on the model, which may result in many states to be excluded during training.  
  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], if a state is present during training but just happens to have zero support in a certain node, the standard adjustment is made. However, if a state is never encountered during training, the algorithm sets the probability to exactly zero. This adjustment applies not only to the **Missing** state, but also to other states that exist in the training data but have zero support as result of model filtering.  
  
 This additional adjustment results in the following formula:  
  
 StateProbability = 0.0 if that state has 0 support in the training set  
  
 ELSE StateProbability = (NodePriorProbability)* (StateSupport + 1) / (NodeSupport + TotalStatesWithNonZeroSupport)  
  
 The net effect of this adjustment is to maintain the stability of the tree.  
  
## Related Tasks  
 The following topics provide more information about how to handle missing values.  
  
|Tasks|Links|  
|-----------|-----------|  
|Add flags to individual model columns to control handling of missing values|[View or Change Modeling Flags &#40;Data Mining&#41;](../../analysis-services/data-mining/view-or-change-modeling-flags-data-mining.md)|  
|Set properties on a mining model to control handling of missing values|[Change the Properties of a Mining Model](../../analysis-services/data-mining/change-the-properties-of-a-mining-model.md)|  
|Learn how to specify modeling flags in DMX|[Modeling Flags &#40;DMX&#41;](../../dmx/modeling-flags-dmx.md)|  
|Alter the way that the mining structure handles missing values|[Change the Properties of a Mining Structure](../../analysis-services/data-mining/change-the-properties-of-a-mining-structure.md)|  
  
## See Also  
 [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md)   
 [Modeling Flags &#40;Data Mining&#41;](../../analysis-services/data-mining/modeling-flags-data-mining.md)  
  
  
