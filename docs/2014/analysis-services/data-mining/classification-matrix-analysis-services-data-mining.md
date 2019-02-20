---
title: "Classification Matrix (Analysis Services - Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models [Analysis Services], validating"
  - "validating data mining models"
  - "viewing mining accuracy"
  - "displaying mining accuracy"
  - "confusion matrix [data mining]"
  - "classification matrix [Analysis Services]"
  - "accuracy testing [data mining]"
ms.assetid: 5c12f202-2ed9-41fa-bee2-0f7ab3ff058a
author: minewiskan
ms.author: owend
manager: craigg
---
# Classification Matrix (Analysis Services - Data Mining)
  A *classification matrix* sorts all cases from the model into categories, by determining whether the predicted value matched the actual value. All the cases in each category are then counted, and the totals are displayed in the matrix. The classification matrix is a standard tool for evaluation of statistical models and is sometimes referred to as a *confusion matrix*.  
  
 The chart that is created when you choose the **Classification Matrix** option compares actual to predicted values for each predicted state that you specify. The rows in the matrix represent the predicted values for the model, whereas the columns represent the actual values. The categories used in analysis are *false positive*, *true positive*, *false negative*, and *true negative*  
  
 A classification matrix is an important tool for assessing the results of prediction because it makes it easy to understand and account for the effects of wrong predictions. By viewing the amount and percentages in each cell of this matrix, you can quickly see how often the model predicted accurately.  
  
 This section explains how to create a classification matrix and how to interpret the results.  
  
## Understanding the Classification Matrix  
 Consider the model that you created as part of the Basic Data Mining Tutorial. The [TM_DecisionTree] model is used to help create a targeted mailing campaign, and can be used to predict which customers are most likely to buy a bike. To test this expected usefulness of this model, you use a data set for which the values of the outcome attribute, [Bike Buyer], are already known. Typically, you would use the testing data set that you set aside when you created the mining structure that is used for training the model.  
  
 There are only two possible outcomes: yes (the customer is likely to buy a bike), and no (the customer will likely not purchase a bike). Therefore, the resulting classification matrix is relatively simple.  
  
## Interpreting the Results  
 The following table shows the classification matrix for the TM_DecisionTree model. Remember that for this predictable attribute, 0 means No and 1 means Yes.  
  
|Predicted|0 (Actual)|1 (Actual)|  
|---------------|------------------|------------------|  
|0|362|144|  
|1|121|373|  
  
 The first result cell, which contains the value 362, indicates the number of *true positives* for the value 0. Because 0 indicates that the customer did not purchase a bike, this statistic tells you that model predicted the correct value for non bike-buyers in 362 cases.  
  
 The cell directly underneath that one, which contains the value 121, tells you the number of *false positives*, or how many times the model predicted that someone would buy a bike when actually they did not.  
  
 The cell that contains the value 144 indicates the number of *false positives* for the value 1. Because 1 means that the customer did purchase a bike, this statistic tells you that in 144 cases, the model predicted someone would not buy a bike when in fact they did.  
  
 Finally, the cell that contains the value 373 indicates the number of true positives for the target value of 1. In other words, in 373 cases the model correctly predicted that someone would buy a bike.  
  
 By summing the values in cells that are diagonally adjacent, you can determine the overall accuracy of the model. One diagonal tells you the total number of accurate predictions, and the other diagonal tells you the total number of erroneous predictions.  
  
### Using Multiple Predictable Values  
 The [Bike Buyer] case is especially easy to interpret because there are only two possible values. When the predictable attribute has multiple possible values, the classification matrix adds a new column for each possible actual value and then counts the number of matches for each predicted value. The following table shows the results on a different model, where three values (0, 1, 2) are possible.  
  
|Predicted|0 (Actual)|1 (Actual)|2 (Actual)|  
|---------------|------------------|------------------|------------------|  
|0|111|3|5|  
|1|2|123|17|  
|2|19|0|20|  
  
 Although the addition of more columns makes the report look more complex, the additional detail can be very useful when you want to assess the cumulative cost of making the wrong prediction. To create sums on the diagonals or to compare the results for different combinations of rows, you can click the **Copy** button provided in the **Classification Matrix** tab and paste the report into Excel. Alternatively, you can use a client such as the Data Mining Client for Excel, which supports [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, to create a classification report directly in Excel that includes both counts and percentages. For more information, see [SQL Server Data Mining](https://go.microsoft.com/fwlink/?LinkID=77733).  
  
## Restrictions on the Classification Matrix  
 A classification matrix can be used only with discrete predictable attributes.  
  
 Although you can add multiple models when selecting models on the **Input Selection** tab of the **Mining Accuracy Chart** designer, the **Classification Matrix** tab will display a separate matrix for each model.  
  
## Related Content  
 The following topics contain more information about how you can build and use classification matrices and other charts.  
  
|Topics|Links|  
|------------|-----------|  
|Provides a walkthrough of how to create a lift chart for the Targeted Mailing model.|[Basic Data Mining Tutorial](../../tutorials/basic-data-mining-tutorial.md)<br /><br /> [Testing Accuracy with Lift Charts &#40;Basic Data Mining Tutorial&#41;](../../tutorials/testing-accuracy-with-lift-charts-basic-data-mining-tutorial.md)|  
|Explains related chart types.|[Lift Chart &#40;Analysis Services - Data Mining&#41;](lift-chart-analysis-services-data-mining.md)<br /><br /> [Profit Chart &#40;Analysis Services - Data Mining&#41;](profit-chart-analysis-services-data-mining.md)<br /><br /> [Scatter Plot &#40;Analysis Services - Data Mining&#41;](scatter-plot-analysis-services-data-mining.md)|  
|Describes uses of cross-validation for mining models and mining structures.|[Cross-Validation &#40;Analysis Services - Data Mining&#41;](cross-validation-analysis-services-data-mining.md)|  
|Describes steps for creating lift charts and other accuracy charts.|[Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](testing-and-validation-tasks-and-how-tos-data-mining.md)|  
  
## See Also  
 [Testing and Validation &#40;Data Mining&#41;](testing-and-validation-data-mining.md)  
  
  
