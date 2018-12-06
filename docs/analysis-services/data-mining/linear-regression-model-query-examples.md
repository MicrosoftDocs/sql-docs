---
title: "Linear Regression Model Query Examples | Microsoft Docs"
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
# Linear Regression Model Query Examples
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you create a query against a data mining model, you can create a content query, which provides details about the patterns discovered in analysis, or you can create a prediction query, which uses the patterns in the model to make predictions for new data. For example, a content query might provide additional details about the regression formula, while a prediction query might tell you if a new data point fits the model. You can also retrieve metadata about the model by using a query.  
  
 This section explains how to create queries for models that are based on the Microsoft Linear Regression algorithm.  
  
> [!NOTE]  
>  Because linear regression is based on a special case of the Microsoft Decision Trees algorithm, there are many similarities, and some decision tree models that use continuous predictable attributes can contain regression formulas. For more information, see [Microsoft Decision Trees Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-decision-trees-algorithm-technical-reference.md).  
  
 **Content queries**  
  
 [Using the Data Mining Schema Rowset to determine parameters used for a model](#bkmk_Query1)  
  
 [Using DMX to return the regression formula for the model](#bkmk_Query2)  
  
 [Returning only the coefficient for the model](#bkmk_Query3)  
  
 **Prediction queries**  
  
 [Predicting income using a singleton query](#bkmk_Query4)  
  
 [Using prediction functions with a regression model](#bkmk_Query5)  
  
##  <a name="bkmk_top"></a> Finding Information about the Linear Regression Model  
 The structure of a linear regression model is extremely simple: the mining model represents the data as a single node, which defines the regression formula. For more information, see [Mining Model Content for Logistic Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-logistic-regression-models.md).  
  
 [Return to Top](#bkmk_top)  
  
###  <a name="bkmk_Query1"></a> Sample Query 1: Using the Data Mining Schema Rowset to Determine Parameters Used for a Model  
 By querying the data mining schema rowset, you can find metadata about the model. This might include when the model was created, when the model was last processed, the name of the mining structure that the model is based on, and the name of the column designated as the predictable attribute. You can also return the parameters that were used when the model was first created.  
  
```  
SELECT MINING_PARAMETERS   
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'TM_PredictIncome'  
```  
  
 Sample results:  
  
|MINING_PARAMETERS|  
|------------------------|  
|COMPLEXITY_PENALTY=0.9,<br /><br /> MAXIMUM_INPUT_ATTRIBUTES=255,<br /><br /> MAXIMUM_OUTPUT_ATTRIBUTES=255,<br /><br /> MINIMUM_SUPPORT=10,<br /><br /> SCORE_METHOD=4,<br /><br /> SPLIT_METHOD=3,<br /><br /> FORCE_REGRESSOR=|  
  
> [!NOTE]  
>  The parameter setting, "`FORCE_REGRESSOR =` ", indicates that the current value for the FORCE_REGRESSOR parameter is null.  
  
 [Return to Top](#bkmk_top)  
  
###  <a name="bkmk_Query2"></a> Sample Query 2: Retrieving the Regression Formula for the Model  
 The following query returns the mining model content for a linear regression model that was built by using the same Targeted Mailing data source that was used in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). This model predicts customer income based on age.  
  
 The query returns the contents of the node that contains the regression formula. Each variable and coefficient is stored in a separate row of the nested NODE_DISTRIBUTION table. If you want to view the complete regression formula, use the [Microsoft Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-tree-viewer.md), click the **(All)** node, and open the **Mining Legend**.  
  
```  
SELECT FLATTENED NODE_DISTRIBUTION as t  
FROM LR_PredictIncome.CONTENT  
```  
  
> [!NOTE]  
>  If you reference individual columns of the nested table by using a query such as `SELECT <column name> from NODE_DISTRIBUTION`, some columns, such as **SUPPORT** or **PROBABILITY**, must be enclosed in brackets to distinguish them from reserved keywords of the same name.  
  
 Expected results:  
  
|t.ATTRIBUTE_NAME|t.ATTRIBUTE_VALUE|t.SUPPORT|t.PROBABILITY|t.VARIANCE|t.VALUETYPE|  
|-----------------------|------------------------|---------------|-------------------|----------------|-----------------|  
|Yearly Income|Missing|0|0.000457142857142857|0|1|  
|Yearly Income|57220.8876687257|17484|0.999542857142857|1041275619.52776|3|  
|Age|471.687717702463|0|0|126.969442359327|7|  
|Age|234.680904692439|0|0|0|8|  
|Age|45.4269617936399|0|0|126.969442359327|9|  
||35793.5477381267|0|0|1012968919.28372|11|  
  
 In comparison, in the **Mining Legend**, the regression formula appears as follows:  
  
 `Yearly Income = 57,220.919 + 471.688 * (Age - 45.427)`  
  
 You can see that in the **Mining Legend**, some numbers are rounded; however, the NODE_DISTRIBUTION table and the **Mining Legend** essentially contain the same values.  
  
 The values in the VALUETYPE column tell you what kind of information is contained in each row, which is useful if you are processing the results programmatically. The following table shows the value types that are output for a linear regression formula.  
  
|VALUETYPE|  
|---------------|  
|1 (Missing)|  
|3 (Continuous)|  
|7 (Coefficient)|  
|8 (Score Gain)|  
|9 (Statistics)|  
|7 (Coefficient)|  
|8 (Score Gain)|  
|9 (Statistics)|  
|11 (Intercept)|  
  
 For more information about the meaning of each value type for regression models, see [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md).  
  
 [Return to Top](#bkmk_top)  
  
###  <a name="bkmk_Query3"></a> Sample Query 3: Returning Only the Coefficient for the Model  
 By using the VALUETYPE enumeration, you can return only the coefficient for the regression equation, as shown in the following query:  
  
```  
SELECT FLATTENED MODEL_NAME,  
    (SELECT ATTRIBUTE_VALUE, VALUETYPE  
     FROM NODE_DISTRIBUTION  
     WHERE VALUETYPE = 11)   
AS t  
FROM LR_PredictIncome.CONTENT  
```  
  
 This query returns two rows, one from the mining model content, and the row from the nested table that contains the coefficient. The ATTRIBUTE_NAME column is not included here because it is always blank for the coefficient.  
  
|MODEL_NAME|t.ATTRIBUTE_VALUE|t.VALUETYPE|  
|-----------------|------------------------|-----------------|  
|LR_PredictIncome|||  
|LR_PredictIncome|35793.5477381267|11|  
  
 [Return to Top](#bkmk_top)  
  
## Making Predictions from a Linear Regression Model  
 You can build prediction queries on linear regression models by using the Mining Model Prediction tab in Data Mining Designer. The prediction query builder is available in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
> [!NOTE]  
>  You can also create queries on regression models by using the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Data Mining Add-ins for Excel or the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Data Mining Add-ins for Excel. Even though the Data Mining Add-ins for Excel do not create regression models, you can browse and query any mining model that is stored on an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 [Return to Top](#bkmk_top)  
  
###  <a name="bkmk_Query4"></a> Sample Query 4: Predicting Income using a Singleton Query  
 The easiest way to create a single query on a regression model is by using the **Singleton Query Input** dialog box. For example, you can build the following DMX query by selecting the appropriate regression model, choosing **Singleton Query**, and then typing **20** as the value for **Age**.  
  
```  
SELECT [LR_PredictIncome].[Yearly Income]  
From   [LR_PredictIncome]  
NATURAL PREDICTION JOIN  
(SELECT 20 AS [Age]) AS t  
```  
  
 Sample results:  
  
|Yearly Income|  
|-------------------|  
|45227.302092176|  
  
 [Return to Top](#bkmk_top)  
  
###  <a name="bkmk_Query5"></a> Sample Query 5: Using Prediction Functions with a Regression Model  
 You can use many of the standard prediction functions with linear regression models. The following example illustrates how to add some descriptive statistics to the prediction query results. From these results, you can see that there is considerable deviation from the mean for this model.  
  
```  
SELECT  
  ([LR_PredictIncome].[Yearly Income]) as [PredIncome],  
  (PredictStdev([LR_PredictIncome].[Yearly Income])) as [StDev1]  
From  
  [LR_PredictIncome]  
NATURAL PREDICTION JOIN  
(SELECT 20 AS [Age]) AS t  
```  
  
 Sample results:  
  
|Yearly Income|StDev1|  
|-------------------|------------|  
|45227.302092176|31827.1726561396|  
  
 [Return to Top](#bkmk_top)  
  
## List of Prediction Functions  
 All [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms support a common set of functions. However, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Linear Regression algorithm supports the additional functions listed in the following table.  
  
|||  
|-|-|  
|Prediction Function|Usage|  
|[IsDescendant &#40;DMX&#41;](../../dmx/isdescendant-dmx.md)|Determines whether one node is a child of another node in the model.|  
|[IsInNode &#40;DMX&#41;](../../dmx/isinnode-dmx.md)|Indicates whether the specified node contains the current case.|  
|[PredictHistogram &#40;DMX&#41;](../../dmx/predicthistogram-dmx.md)|Returns a predicted value, or set of values, for a specified column.|  
|[PredictNodeId &#40;DMX&#41;](../../dmx/predictnodeid-dmx.md)|Returns the Node_ID for each case.|  
|[PredictStdev &#40;DMX&#41;](../../dmx/predictstdev-dmx.md)|Returns standard deviation for the predicted value.|  
|[PredictSupport &#40;DMX&#41;](../../dmx/predictsupport-dmx.md)|Returns the support value for a specified state.|  
|[PredictVariance &#40;DMX&#41;](../../dmx/predictvariance-dmx.md)|Returns the variance of a specified column.|  
  
 For a list of the functions that are common to all [!INCLUDE[msCoName](../../includes/msconame-md.md)] algorithms, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md). For more information about how to use these functions, see [Data Mining Extensions &#40;DMX&#41; Function Reference](../../dmx/data-mining-extensions-dmx-function-reference.md).  
  
## See Also  
 [Microsoft Linear Regression Algorithm](../../analysis-services/data-mining/microsoft-linear-regression-algorithm.md)   
 [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md)   
 [Microsoft Linear Regression Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-linear-regression-algorithm-technical-reference.md)   
 [Mining Model Content for Linear Regression Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-linear-regression-models-analysis-services-data-mining.md)  
  
  
