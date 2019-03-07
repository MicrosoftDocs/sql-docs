---
title: "Measures in the Cross-Validation Report | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "root mean square error [data mining]"
  - "cross-validation [data mining]"
  - "mean absolute error [data mining]"
  - "log score [data mining]"
  - "likelihood [data mining]"
ms.assetid: a07b1665-7f72-4266-82a4-43a91ae2571d
author: minewiskan
ms.author: owend
manager: craigg
---
# Measures in the Cross-Validation Report
  During cross-validation, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] divides the data in a mining structure into multiple cross-sections and then iteratively tests the structure and any associated mining models. Based on this analysis, it outputs a set of standard accuracy measures for the structure and each model.  
  
 The report contains some basic information about the number of folds in the data and the amount of data in each fold, and a set of general metrics that describe data distribution. By comparing the general metrics for each cross-section, you can assess the reliability of the structure or model.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also displays a set of detailed measures for mining models. These measures depend on the model type and on the type of attribute that is being analyzed: for example, whether it is discrete or continuous.  
  
 This section provides a list of the measures that are contained in the **Cross-Validation** report, and what they mean. For details on how each measure is calculated, see [Cross-Validation Formulas](cross-validation-formulas.md).  
  
## List of Measures in the Cross-Validation Report  
 The following table lists the measures that appear in the cross-validation report. The measures are grouped by *test type*, which is provided in the left-hand column of the following table. The right-hand column lists the name of the measure as it appears in the report, and provides a brief explanation of what it means.  
  
|Test Type|Measures and Descriptions|  
|---------------|-------------------------------|  
|Clustering|Measures that apply to clustering models:<br /><br /> **Case likelihood**: This measure usually indicates how likely it is that a case belongs to a particular cluster. <br />                      For cross-validation, the scores are summed and then divided by the number of cases, so here the score is an average case likelihood.|  
|Classification|Measures that apply to classification models:<br /><br /> **True Positive**/<br />                      **True Negative**/ **False Positive**/ **False Positive**: Count of rows or values in the partition where the predicted state matches the target state, and the predict probability is greater than the specified threshold. Cases that have missing values for the target attribute are excluded, meaning the counts of all values might not add up|  
||**Pass/Fail**: Count of rows or values in the partition where the predicted state matches the target state, and where the predict probability value is greater than 0.|  
|Likelihood|Likelihood measures apply to multiple model types:<br /><br /> **Lift**: The ratio of the actual prediction probability to the marginal probability in the test cases. Rows that have missing values for the target attribute are excluded. This measure generally shows how much the probability of the target outcome improves when the model is used.<br /><br /> **Root Mean Square Error**: Square root of the mean error for all partition cases, divided by the number of cases in the partition, excluding rows that have missing values for the target attribute. RMSE is a popular estimator for predictive models. The score averages the residuals for each case to yield a single indicator of model error.<br /><br /> **Log score**: The logarithm of the actual probability for each case, summed, and then divided by the number of rows in the input dataset, excluding rows that have missing values for the target attribute. Because probability is represented as a decimal fraction, log scores are always negative numbers. A number closer to 0 is a better score. Whereas raw scores can have very irregular or skewed distributions, a log score is similar to a percentage.|  
|Estimation|Measures that apply only to estimation models, which predict a continuous numeric attribute:<br /><br /> **Root Mean Square Error**: Average error when the predicted value is compared to the actual value. RMSE is a popular estimator for predictive models. The score averages the residuals for each case to yield a single indicator of model error.<br /><br /> **Mean Absolute Error**: Average error when predicted values are compared to actual values, calculated as the mean of the absolute sum of errors. Mean absolute error is useful for understanding how close overall the predictions were to actual values. A smaller score means predictions were more accurate.<br /><br /> **Log Score**: The logarithm of the actual probability for each case, summed, and then divided by the number of rows in the input dataset, excluding rows that have missing values for the target attribute. Because probability is represented as a decimal fraction, log scores are always negative numbers. A number closer to 0 is a better score. Whereas raw scores can have very irregular or skewed distributions, a log score is similar to a percentage.|  
|Aggregates|Aggregate measures provide an indication of the variance in the results for each partition:<br /><br /> **Mean**: Average of the partition values for a particular measure.<br /><br /> **Standard Deviation**: Average of the deviation from the mean for a specific measure, across all the partitions in a model. For cross-validation, a higher value for this score implies substantial variation between the folds.|  
  
## See Also  
 [Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)  
  
  
