---
title: "Cross-Validation Formulas | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: fd1ea582-29a1-4154-8de2-47bab3539b4d
author: minewiskan
ms.author: owend
manager: craigg
---
# Cross-Validation Formulas
  When you generate a cross-validation report, it contains accuracy measures for each model, depending on the type of mining model (that is, the algorithm that was used to create the model), the data type of the predictable attribute, and the predictable attribute value, if any.  
  
 This section lists the measures used in the cross-validation report and describes the method of calculation.  
  
 For a breakdown of the accuracy measures by model type, see [Measures in the Cross-Validation Report](measures-in-the-cross-validation-report.md).  
  
## Formulas used for Cross-Validation Measures  
  
> [!NOTE]  
>  **Important:** These measures of accuracy are computed for each target attribute. For each attribute you can specify or omit a target value. If a case in the data set does not have any value for the target attribute, the case is treated as having a special value called the *missing value*. Rows that have missing values are not counted when computing the accuracy measure for a particular target attribute. Note that because the scores are computed for each attribute individually, if values are present for the target attribute but missing for other attributes, it does not affect the score for the target attribute.  
  
|Measure|Applies To|Implementation|  
|-------------|----------------|--------------------|  
|**True positive**|Discrete attribute, value is specified|Count of cases that meet these conditions:<br /><br /> Case contains the target value.<br /><br /> Model predicted that case contains the target value.|  
|**True Negative**|Discrete attribute, value is specified|Count of cases that meet these conditions:<br /><br /> Case does not contain the target value.<br /><br /> Model predicted that case does not contain the target value.|  
|**False positive**|Discrete attribute, value is specified|Count of cases that meet these conditions:<br /><br /> Actual value is equal to target value.<br /><br /> Model predicted that case contains the target value.|  
|**False Negative**|Discrete attribute, value is specified|Count of cases that meet these conditions:<br /><br /> Actual value not equal to target value.<br /><br /> Model predicted that case does not contain the target value.|  
|**Pass/fail**|Discrete attribute, no specified target|Count of cases that meet these conditions:<br /><br /> Pass if the predicted state with the highest probability is the same as the input state and probability is greater than the value of **State Threshold**.<br /><br /> Otherwise, fail.|  
|**Lift**|Discrete attribute. Target value can be specified but is not required.|The mean log likelihood for all rows with values for the target attribute, where log likelihood for each case is calculated as Log(ActualProbability/MarginalProbability). To compute the mean, the sum of the log likelihood values is divided by the number of rows in the input dataset, excluding rows with missing values for the target attribute.<br /><br /> Lift can be either a negative or positive value. A positive value means an effective model that outperforms the random guess.|  
|**Log score**|Discrete attribute. Target value can be specified but is not required.|Log of the actual probability for each case, summed, and then divided by the number of rows in the input dataset, excluding rows with missing values for the target attribute.<br /><br /> Because probability is represented as a decimal fraction, log scores are always negative numbers. A score that is closer to 0 is a better score.|  
|**Case likelihood**|Cluster|Sum of the cluster likelihood scores for all cases, divided by the number of cases in the partition, excluding rows with missing values for the target attribute.|  
|**Mean absolute error**|Continuous attribute|Sum of the absolute error for all cases in the partition, divided by the number of cases in the partition.|  
|**Root mean square error**|Continuous attribute|Square root of the mean squared error for the partition.|  
|**Root mean squared error**|Discrete attribute. Target value can be specified but is not required.|Square root of the mean of the squares of complement of the probability score, divided by the number of cases in the partition, excluding rows with missing values for the target attribute.|  
|**Root mean squared error**|Discrete attribute, no specified target.|Square root of the mean of the squares of complement of the probability score, divided by the number of cases in the partition, excluding cases with missing values for the target attribute.|  
  
## See Also  
 [Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)   
 [Cross-Validation &#40;Analysis Services - Data Mining&#41;](cross-validation-analysis-services-data-mining.md)  
  
  
