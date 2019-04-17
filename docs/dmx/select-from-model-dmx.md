---
title: "SELECT FROM &lt;model&gt; (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# SELECT FROM &lt;model&gt; (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs an empty prediction join, returning the most probable value or values for the specified columns. Only the content from the mining model is used to create the prediction.  
  
## Syntax  
  
```  
  
SELECT <expression list> [TOP <n>] FROM <model>   
[WHERE <condition list>]   
[ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *expression list*  
 A comma-separated list of expressions, or of predict or predict only columns.  
  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *model*  
 A model identifier.  
  
 *condition list*  
 Optional. Conditions to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 The columns in the *expression list* must be defined as predict or predict only, or related to a predictable column.  
  
## Naive Bayes Example  
 The following example performs an empty prediction join on the Bike Buyer column, returning the most likely state in the TM Naive Bayes mining model.  
  
```  
SELECT ([Bike Buyer]) FROM [TM_Naive_Bayes]  
```  
  
## Time Series Example  
 The following example performs a prediction on the Amount column in the Forecasting model, returning the next four time steps. The Model Region column combines bike models and regions into a single identifier. The query uses the [PredictTimeSeries &#40;DMX&#41;](../dmx/predicttimeseries-dmx.md) function to perform the prediction.  
  
```  
SELECT [Model Region], PredictTimeSeries(Amount, 4)   
FROM Forecasting  
```  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
