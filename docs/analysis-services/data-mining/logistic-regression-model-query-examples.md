---
title: "Logistic Regression Model Query Examples | Microsoft Docs"
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
# Logistic Regression Model Query Examples
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you create a query against a data mining model, you can create a content query, which provides details about the patterns discovered in analysis, or you can create a prediction query, which uses the patterns in the model to make predictions using new data.  
  
 This section explains how to create queries for models that are based on the Microsoft Logistic Regression algorithm.  
  
 **Content Queries**  
  
 [Retrieving Model Parameters by Using the Data Mining Schema Rowset](#bkmk_Query1)  
  
 [Finding Additional Detail about the Model by Using DMX](#bkmk_Query2)  
  
 **Prediction Queries**  
  
 [Making Predictions for a Continuous Value](#bkmk_Query3)  
  
 [Making Predictions for a Discrete Value](#bkmk_Query4)  
  
##  <a name="bkmk_top"></a> Getting Information about the Logistic Regression Model  
 Logistic regression models are created by using the Microsoft Neural Network algorithm with a special set of parameters; therefore, a logistic regression model has some of the same information as a neural networks model, but is less complex. To understand the structure of the model content, and which node types store what kind of information, see [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md).  
  
 To follow along in the query scenarios, you can create a logistic regression model as described in the following section of the Intermediate Data Mining Tutorial: [Lesson 5: Building Neural Network and Logistic Regression Models &#40;Intermediate Data Mining Tutorial&#41;](http://msdn.microsoft.com/library/42c3701a-1fd2-44ff-b7de-377345bbbd6b).  
  
 You can also use the mining structure, Targeted Mailing, from the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c).  
  
```  
ALTER MINING STRUCTURE [Targeted Mailing]  
ADD MINING MODEL [TM_Logistic Regression]  
([Customer Key],  
[Age],  
[Bike Buyer] PREDICT,  
[Yearly Income] PREDICT,  
[Commute Distance],  
[English Education],  
Gender,  
[House Owner Flag],  
[Marital Status],  
[Number Cars Owned],  
[Number Children At Home],  
[Region],  
[Total Children]  
)  
USING Microsoft_Logistic_Regression  
```  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Retrieving Model Parameters by Using the Data Mining Schema Rowset  
 By querying the data mining schema rowset, you can find metadata about the model, such as when it was created, when the model was last processed, the name of the mining structure that the model is based on, and the name of the column used as the predictable attribute. The following example returns the parameters that were used when the model was first created, together with the name and type of the model, and the date that it was created.  
  
```  
SELECT MODEL_NAME, SERVICE_NAME, DATE_CREATED, MINING_PARAMETERS   
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'Call Center_LR'  
```  
  
 Sample results:  
  
|MODEL_NAME|SERVICE_NAME|DATE_CREATED|MINING_PARAMETERS|  
|-----------------|-------------------|-------------------|------------------------|  
|Call Center_LR|Microsoft_Logistic_Regression|04/07/2009 20:38:33|HOLDOUT_PERCENTAGE=30, HOLDOUT_SEED=1, MAXIMUM_INPUT_ATTRIBUTES=255, MAXIMUM_OUTPUT_ATTRIBUTES=255, MAXIMUM_STATES=100, SAMPLE_SIZE=10000|  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Finding Additional Detail about the Model by Using DMX  
 The following query returns some basic information about the logistic regression model. A logistic regression model is similar to a neural network model in many ways, including the presence of a marginal statistic node (NODE_TYPE = 24) that describes the values used as inputs. This example query uses the Targeted Mailing model, and gets the values of all the inputs by retrieving them from the nested table, NODE_DISTRIBUTION.  
  
```  
SELECT FLATTENED NODE_DISTRIBUTION AS t  
FROM [TM_Logistic Regression].CONTENT   
```  
  
 Partial results:  
  
|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|t.SUPPORT|t.PROBABILITY|t.VARIANCE|t.VALUETYPE|  
|-----------------------|------------------------|---------------|-------------------|----------------|-----------------|  
|Age|Missing|0|0|0|1|  
|Age|45.43491192|17484|1|126.9544114|3|  
|Bike Buyer|Missing|0|0|0|1|  
|Bike Buyer|0|8869|0.507263784|0|4|  
|Bike Buyer|1|8615|0.492736216|0|4|  
|Commute Distance|Missing|0|0|0|1|  
|Commute Distance|5-10 Miles|3033|0.173472889|0|4|  
  
 The actual query returns many more rows; however, this sample illustrates the type of information that is provided about the inputs. For discrete inputs, each possible value is listed in the table. For continuous-value inputs such as Age, a complete listing is impossible, so the input is discretized as a mean. For more information about how to use the information in the marginal statistics node, see [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md).  
  
> [!NOTE]  
>  The results have been flattened for easier viewing, but you can return the nested table in a single column if your provider supports hierarchical rowsets.  
  
## Prediction Queries on a Logistic Regression Model  
 You can use the [Predict &#40;DMX&#41;](../../dmx/predict-dmx.md) function with every kind of mining model to provide new data to the model and make predictions based on the new values. You can also use functions to return additional information about the prediction, such as the probability that a prediction is correct. This section provides some examples of prediction queries on a logistic regression model.  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Making Predictions for a Continuous Value  
 Because logistic regression supports the use of continuous attributes for both input and prediction, it is easy to create models that correlate various factors in your data. You can use prediction queries to explore the relationship among these factors.  
  
 The following query sample is based on the Call Center model, from the Intermediate Tutorial, and creates a singleton query that predicts service grade for the Friday AM shift. The [PredictHistogram (DMX)](../../dmx/predicthistogram-dmx.md) function returns a nested table that provides statistics relevant to understanding the validity of the predicted value.  
  
```  
SELECT  
  Predict([Call Center_LR].[Service Grade]) as Predicted ServiceGrade,  
  PredictHistogram([Call Center_LR].[Service Grade]) as [Results],  
FROM  
  [Call Center_LR]  
NATURAL PREDICTION JOIN  
(SELECT 'Friday' AS [Day Of Week],  
  'AM' AS [Shift]) AS t  
```  
  
 Sample results:  
  
|Predicted Service Grade|Service Grade|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|$VARIANCE|$STDEV|  
|-----------------------------|-------------------|--------------|------------------|--------------------------|---------------|------------|  
|0.102601830123659|0.102601830123659|83.0232558139535|0.988372093023256|0|0.00120552660600087|0.034720694203902|  
|||0.976744186046512|0.0116279069767442|0.0116279069767442|0|0|  
  
 For more information about the probability, support, and standard deviation values in the nested NODE_DISTRIBUTION table, see [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md).  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Making Predictions for a Discrete Value  
 Logistic regression is typically used in scenarios where you want to analyze the factors that contribute to a binary outcome. Although the model used in the tutorial predicts a continuous value, **ServiceGrade**, in a real-life scenario you might want to set up the model to predict whether service grade met some discretized target value. Alternatively, you could output the predictions using a continuous value but later group the predicted outcomes into **Good**, **Fair**, or **Poor**.  
  
 The following sample illustrates how to change the way that the predictable attribute is grouped. To do this, you create a copy of the mining structure and then change the discretization method of the target column so that the values are grouped rather than continuous.  
  
 The following procedure describes how to change the grouping of Service Grade values in the Call Center data.  
  
##### To create a discretized version of the Call Center mining structure and models  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in Solution Explorer, expand **Mining Structures**.  
  
2.  Right-click Call Center.dmm and select **Copy**.  
  
3.  Right click **Mining Structures** and select **Paste**. A new mining structure iss added, named Call Center 1.  
  
4.  Right-click the new mining structure and select **Rename**. Type the new name, **Call Center Discretized**.  
  
5.  Double-click the new mining structure to open it in the designer. Notice that the mining models have all been copied as well, and all have the extension 1. Leave the names as is for now.  
  
6.  In the **Mining Structure** tab, right-click the column for Service Grade, and select **Properties**.  
  
7.  Change the **Content** property from **Continuous** to **Discretized**. Change the **DiscretizationMethod** property to **Clusters**. For Discretization BucketCount, type **3**.  
  
    > [!NOTE]  
    >  These parameters are just used for illustrating the process, and do not necessarily produce a valid model,  
  
8.  From the **Mining Model** menu, select **Process structure and all models**.  
  
 The following sample query is based on this discretized model, and predicts the service grade for the specified day of the week, together with the probabilities for each predicted outcome.  
  
```  
SELECT  
  (PredictHistogram([Call Center_LR 1].[Service Grade])) as [Predictions]  
FROM  
  [Call Center_LR 1]  
NATURAL PREDICTION JOIN  
(SELECT 'Saturday' AS [Day Of Week]) AS t    
```  
  
 Expected results:  
  
 **Predictions:**  
  
|Service Grade|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|$VARIANCE|$STDEV|  
|-------------------|--------------|------------------|--------------------------|---------------|------------|  
|0.10872718383125|35.7246504770641|0.425293458060287|0.0170168360030293|0|0|  
|0.05855769230625|31.7098880800703|0.377498667619885|0.020882020060454|0|0|  
|0.170169491525|15.6109159883202|0.185844237956192|0.0661386571386049|0|0|  
||0.954545454545455|0.0113636363636364|0.0113636363636364|0|0|  
  
 Note that the predicted outcomes have been grouped into three categories as specified; however, these groupings are based on the clustering of actual values in the data, not arbitrary values that you might set as business goals.  
  
## List of Prediction Functions  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Logistic Regression algorithm supports the additional functions listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[IsDescendant &#40;DMX&#41;](../../dmx/isdescendant-dmx.md)|Determines whether one node is a child of another node in the model.|  
|[PredictAdjustedProbability &#40;DMX&#41;](../../dmx/predictadjustedprobability-dmx.md)|Returns the adjusted probability of a specified state.|  
|[PredictHistogram &#40;DMX&#41;](../../dmx/predicthistogram-dmx.md)|Returns a predicted value, or set of values, for a specified column.|  
|[PredictProbability &#40;DMX&#41;](../../dmx/predictprobability-dmx.md)|Returns the probability for a specified state.|  
|[PredictStdev &#40;DMX&#41;](../../dmx/predictstdev-dmx.md)|Returns standard deviation for the predicted value.|  
|[PredictSupport &#40;DMX&#41;](../../dmx/predictsupport-dmx.md)|Returns the support value for a specified state.|  
|[PredictVariance &#40;DMX&#41;](../../dmx/predictvariance-dmx.md)|Returns the variance of a specified column.|  
  
 For a list of the functions that are common to all [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, see [General Prediction Functions &#40;DMX&#41;](../../dmx/general-prediction-functions-dmx.md). For the syntax of specific functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](../../dmx/data-mining-extensions-dmx-function-reference.md).  
  
> [!NOTE]  
>  For neural network and logistic regression models, the [PredictSupport &#40;DMX&#41;](../../dmx/predictsupport-dmx.md) function returns a single value that represents the size of the training set for the entire model.  
  
## See Also  
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)   
 [Microsoft Logistic Regression Algorithm](../../analysis-services/data-mining/microsoft-logistic-regression-algorithm.md)   
 [Microsoft Logistic Regression Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-logistic-regression-algorithm-technical-reference.md)   
 [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md)   
 [Lesson 5: Building Neural Network and Logistic Regression Models &#40;Intermediate Data Mining Tutorial&#41;](http://msdn.microsoft.com/library/42c3701a-1fd2-44ff-b7de-377345bbbd6b)  
  
  
