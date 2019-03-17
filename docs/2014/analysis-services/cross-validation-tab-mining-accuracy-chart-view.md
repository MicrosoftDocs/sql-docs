---
title: "Cross-Validation Tab (Mining Accuracy Chart View) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.accuracychart.crossvalidation.f1"
ms.assetid: bd215a68-1ad7-4046-9c44-ec8e2be13a64
author: minewiskan
ms.author: owend
manager: craigg
---
# Cross-Validation Tab (Mining Accuracy Chart View)
  Cross-validation lets you partition a mining structure into cross-sections and iteratively train and test models against each cross-section. You specify a number of folds to divide the data into, and each fold is used in turn as the test data, whereas the remaining data is used to train a new model. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] then generates a set of standard accuracy metrics for each model. By comparing the metrics for the models generated for each cross-section, you can get a good idea of how reliable the mining model is for the whole data set.  
  
 For more information, see [Cross-Validation &#40;Analysis Services - Data Mining&#41;](data-mining/cross-validation-analysis-services-data-mining.md).  
  
> [!NOTE]  
>  Cross-validation cannot be used with models that were built by using the [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithm. If you run the report on a mining structure that contains these types of models, the models will not be included in the report.  
  
## Task List  
  
-   Specify the number of folds.  
  
-   Specify the maximum number of cases to use for cross-validation.  
  
-   Specify the predictable column.  
  
-   Optionally, specify a predictable state.  
  
-   Optionally, set parameters that control how the accuracy of prediction is assessed.  
  
-   Click **Get Results** to display the results of cross-validation.  
  
## UIElement List  
 **Fold Count**  
 Specify the number of folds, or partitions, to create. The minimum value is 2, meaning that half the data set is used for testing and half for training.  
  
 The maximum value is 10 for session mining structures.  
  
 The maximum value is 256 if the mining structure is stored in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  As you increase the number of folds, the time that is required to perform cross-validation similarly increases by n. You might experience performance issues if the number of cases is large and the value of **Fold Count** is also large.  
  
 **Max Cases**  
 Specify the maximum number of cases to use for cross-validation. The number of cases in any particular fold is equal to the **Max Cases** value divided by the **Fold Count** value.  
  
 If you use **0**, all cases in the source data are used for cross-validation.  
  
 There is no default value.  
  
> [!NOTE]  
>  As you increase the number of cases, processing time also increases.  
  
 **Target Attribute**  
 Select a column from the list of predictable columns found in all models. You can only select one predictable column every time that you perform cross-validation.  
  
 To test only clustering models, select **Cluster**.  
  
 **Target State**  
 Type a value, or select a target value from a drop-down list of values.  
  
 The default value is `null`, indicating that all states are to be tested.  
  
 Disabled for clustering models.  
  
 **Target**  **Threshold**  
 Specify a value between 0 and 1 that indicates the prediction probability above which a predicted state is considered to be correct. The value can be set in 0.1 increments.  
  
 The default is `null`, indicating that the most probable prediction is counted as correct.  
  
> [!NOTE]  
>  Although you can set the value to 0.0, using this value will increase processing time and not yield meaningful results.  
  
 **Get Results**  
 Click to begin cross-validation of the model using the specified parameters.  
  
 The model is partitioned into the specified number of folds and a separate model is tested for each fold. Therefore, it might take some time for cross-validation to return the results.  
  
 For more information about how to interpret the results of the cross-validation report, see [Measures in the Cross-Validation Report](data-mining/measures-in-the-cross-validation-report.md).  
  
## Setting the Accuracy Threshold  
 You can control the standard for measuring prediction accuracy by setting a value for **Target** **Threshold**. A threshold represents a kind of accuracy bar. Each prediction is assigned a probability that the predicted value is correct. Therefore, if you set the **Target** **Threshold** value closer to 1, you are requiring that the probability for any particular prediction to be fairly high to be counted as a good prediction. Conversely, if you set **Target** **Threshold** closer to 0, even predictions with lower probability values are counted as "good" predictions.  
  
 There is no recommended threshold value because the probability of any prediction depends on the amount of data and the type of prediction you are making. You should review some predictions at different probability levels to determine an appropriate accuracy bar for your data. It is important that you do this, because the value that you set for **Target** **Threshold** affects the measured accuracy of the model.  
  
 For example, suppose three predictions are made for a particular target state, and the probabilities of each prediction are 0.05, 0.15, and 0.8. If you set the threshold to 0.5, only one prediction is counted as being correct. If you set **Target** **Threshold** to 0.10, two predictions are counted as being correct.  
  
 When **Target** **Threshold** is set to `null`, which is the default value, the most probable prediction for each case is counted as correct. In the example just cited, 0.05, 0.15, and 0.8 are the probabilities for predictions in three different cases. Although the probabilities are very different, each prediction would be counted as correct, because each case generates only one prediction and these are the best predictions for these cases.  
  
## See Also  
 [Testing and Validation &#40;Data Mining&#41;](data-mining/testing-and-validation-data-mining.md)   
 [Cross-Validation &#40;Analysis Services - Data Mining&#41;](data-mining/cross-validation-analysis-services-data-mining.md)   
 [Measures in the Cross-Validation Report](data-mining/measures-in-the-cross-validation-report.md)   
 [Data Mining Stored Procedures &#40;Analysis Services - Data Mining&#41;](/sql/analysis-services/data-mining/data-mining-stored-procedures-analysis-services-data-mining)  
  
  
