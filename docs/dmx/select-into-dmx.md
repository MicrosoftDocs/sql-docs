---
title: "SELECT INTO (DMX) | Microsoft Docs"
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
  - "SELECT_INTO"
  - "SELECT INTO"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "mining models [Analysis Services], copying"
  - "SELECT INTO statement"
  - "mining models [Analysis Services], creating"
  - "copying mining models"
ms.assetid: 31ab9b4c-e20d-41ee-886f-6665c22c6ad5
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# SELECT INTO (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
 The following example creates a new mining model based on an existing mining model, `TM_Clustering`, which you create in the [Basic Data Mining Tutorial](http://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c). In the new model, the CLUSTER_COUNT parameter is modified so that a maximum of five clusters will exist in the new model. In contrast, the existing model uses the default value, which is 10.  
  
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
  
  
