---
title: "Testing and Validation (Data Mining) | Microsoft Docs"
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
# Testing and Validation (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Validation is the process of assessing how well your mining models perform against real data. It is important that you validate your mining models by understanding their quality and characteristics before you deploy them into a production environment.  
  
 This section introduces some basic concepts related to model quality, and describes the strategies for model validation that are provided in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For an overview of how model validation fits into the larger data mining process, see [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md).  
  
## Methods for Testing and Validation of Data Mining Models  
 There are many approaches for assessing the quality and characteristics of a data mining model.  
  
-   Use various measures of statistical validity to determine whether there are problems in the data or in the model.  
  
-   Separate the data into training and testing sets to test the accuracy of predictions.  
  
-   Ask business experts to review the results of the data mining model to determine whether the discovered patterns have meaning in the targeted business scenario  
  
 All of these methods are useful in data mining methodology and are used iteratively as you create, test, and refine models to answer a specific problem. No single comprehensive rule can tell you when a model is good enough, or when you have enough data.  
  
## Definition of Criteria for Validating Data Mining Models  
 Measures of data mining generally fall into the categories of accuracy, reliability, and usefulness.  
  
 *Accuracy* is a measure of how well the model correlates an outcome with the attributes in the data that has been provided. There are various measures of accuracy, but all measures of accuracy are dependent on the data that is used. In reality, values might be missing or approximate, or the data might have been changed by multiple processes. Particularly in the phase of exploration and development, you might decide to accept a certain amount of error in the data, especially if the data is fairly uniform in its characteristics. For example, a model that predicts sales for a particular store based on past sales can be strongly correlated and very accurate, even if that store consistently used the wrong accounting method. Therefore, measurements of accuracy must be balanced by assessments of reliability.  
  
 *Reliability* assesses the way that a data mining model performs on different data sets. A data mining model is reliable if it generates the same type of predictions or finds the same general kinds of patterns regardless of the test data that is supplied. For example, the model that you generate for the store that used the wrong accounting method would not generalize well to other stores, and therefore would not be reliable.  
  
 *Usefulness* includes various metrics that tell you whether the model provides useful information. For example, a data mining model that correlates store location with sales might be both accurate and reliable, but might not be useful, because you cannot generalize that result by adding more stores at the same location. Moreover, it does not answer the fundamental business question of why certain locations have more sales. You might also find that a model that appears successful in fact is meaningless, because it is based on cross-correlations in the data.  
  
## Tools for Testing and Validation of Mining Models  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports multiple approaches to validation of data mining solutions, supporting all phases of the data mining test methodology.  
  
-   Partitioning data into testing and training sets.  
  
-   Filtering models to train and test different combinations of the same source data.  
  
-   Measuring *lift* and *gain*. A *lift chart* is a method of visualizing the improvement that you get from using a data mining model, when you compare it to random guessing.  
  
-   Performing *cross-validation* of data sets  
  
-   Generating *classification matrices*. These charts sort good and bad guesses into a table so that you can quickly and easily gauge how accurately the model predicts the target value.  
  
-   Creating *scatter plots* to assess the fit of a regression formula.  
  
-   Creating *profit charts* that associate financial gain or costs with the use of a mining model, so that you can assess the value of the recommendations.  
  
 These metrics do not aim to answer the question of whether the data mining model answers your business question; rather, these metrics provide objective measurements that you can use to assess the reliability of your data for predictive analytics, and to guide your decision of whether to use a particular iterate on the development process.  
  
 The topics in this section provide an overview of each method and walk you through the process of measuring the accuracy of models that you build using SQL Server Data Mining.  
  
### Related Topics  
  
|Topics|Links|  
|------------|-----------|  
|Learn how to set up a testing data set using a wizard or DMX commands|[Training and Testing Data Sets](../../analysis-services/data-mining/training-and-testing-data-sets.md)|  
|Learn how to test the distribution and representativeness of the data in a mining structure|[Cross-Validation &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/cross-validation-analysis-services-data-mining.md)|  
|Learn about the accuracy chart types provided.|[Lift Chart &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/lift-chart-analysis-services-data-mining.md)<br /><br /> [Profit Chart &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/profit-chart-analysis-services-data-mining.md)<br /><br /> [Scatter Plot &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/scatter-plot-analysis-services-data-mining.md)|  
|Learn how to create a classification matrix, sometimes called a confusion matrix, for assessing the number of true and false positives and negatives.|[Classification Matrix &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/classification-matrix-analysis-services-data-mining.md)|  
  
## See Also  
 [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md)   
 [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md)   
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)  
  
  
