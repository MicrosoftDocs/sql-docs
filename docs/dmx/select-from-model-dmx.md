---
title: "SELECT FROM &lt;model&gt; (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "SELECT"
  - "FROM"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "empty prediction joins [DMX]"
  - "SELECT FROM <model> statement"
ms.assetid: dc5b9a01-e308-4ee8-84fc-ba4b991c60aa
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# SELECT FROM &lt;model&gt; (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
