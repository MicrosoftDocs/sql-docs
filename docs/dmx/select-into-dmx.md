---
title: "SELECT INTO (DMX) | Microsoft Docs"
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
# SELECT INTO (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Creates a new mining model that is built on the mining structure of an existing mining model. The **SELECT INTO** statement creates the new mining model by copying schema and other information that is not specific to the actual algorithm.  
  
## Syntax  
  
```  
  
SELECT INTO <new model>   
USING <algorithm> [(<parameter list>)] [WITH DRILLTHROUGH[,] [FILTER(<expression>)]]  
FROM <existing model>  
```  
  
## Arguments  
 *new model*  
 A unique name for the new model that is being created.  
  
 *algorithm*  
 The provider-defined name of a data mining algorithm.  
  
 *parameter list*  
 Optional. A comma-separated list of provider-defined parameters for the algorithm.  
  
 *expression*  
 An expression that evaluates to a valid filter condition on the training data. For more information about expressions that can be used as filters, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
 *existing model*  
 The name of the existing model to be copied.  
  
## Remarks  
 If the existing model is trained, the new model is automatically processed when this statement executes. Otherwise, the new model remains unprocessed.  
  
 The **SELECT INTO** statement works only if the structure of the existing model is compatible with the algorithm of the new model. Therefore, this statement is most useful for rapidly creating and testing models that are based on the same algorithm. If you change the algorithm type, the new algorithm must support the data type of each column that is in the existing model, or an error might occur when the model is processed,  
  
 The **WITH DRILLTHROUGH** clause enables drillthrough on the new mining model. Drillthrough can only be enabled when you create the model.  
  
## Example 1: Altering the Parameters of the Model  
 The following example creates a new mining model based on an existing mining model, `TM_Clustering`, which you create in the [Basic Data Mining Tutorial](https://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). In the new model, the CLUSTER_COUNT parameter is modified so that a maximum of five clusters will exist in the new model. In contrast, the existing model uses the default value, which is 10.  
  
```  
SELECT * INTO [New_Clustering]  
USING [Microsoft_Clustering] (CLUSTER_COUNT = 5)   
FROM [TM Clustering]  
```  
  
## Example 2: Adding a Filter to the Model  
 The following example creates a new mining model based on an existing mining model, and adds a filter on the model. The filter restricts the training data to only those customers who live in a particular region.  
  
```  
SELECT * INTO [Clustering Europe Region]  
USING [Microsoft_Clustering] WITH FILTER(Region='Europe')  
FROM [TM Clustering]  
```  
  
> [!NOTE]  
>  Filters that are applied to the case table can be altered by using the SELECT INTO statement as shown in this example; however, if the original model contains a filter on a nested table, the nested table filter cannot be altered or removed by using this syntax, but is copied unchanged from the original model. To create a model with a different filter on a nested table, use the ALTER STRTUCTURE...ADD MODEL syntax.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
