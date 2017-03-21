---
title: "SELECT FROM &lt;model&gt;.SAMPLE_CASES (DMX) | Microsoft Docs"
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
  - "SAMPLE_CASES"
  - "SELECT"
  - "FROM"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "SELECT FROM <model>.SAMPLE_CASES statement"
  - "mining models [Analysis Services], sample cases"
  - "sample cases [DMX]"
  - "training mining models"
ms.assetid: e7a34b9b-3562-4503-bfa7-dd9b12db480a
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# SELECT FROM &lt;model&gt;.SAMPLE_CASES (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns sample cases that are representative of the cases that are used to train the data mining model.  
  
 To use this statement, you must enable drillthrough when you create the mining model. For more information about enabling drillthrough, see [CREATE MINING MODEL &#40;DMX&#41;](../dmx/create-mining-model-dmx.md), [SELECT INTO &#40;DMX&#41;](../dmx/select-into-dmx.md), and [ALTER MINING STRUCTURE &#40;DMX&#41;](../dmx/alter-mining-structure-dmx.md).  
  
## Syntax  
  
```  
  
SELECT [FLATTENED] [TOP <n>] <expression list> FROM <model>.SAMPLE_CASES  
[WHERE <condition list>] ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *expression list*  
 A comma-separated list of related column identifiers.  
  
 *model*  
 A model identifier.  
  
 *condition list*  
 Optional. Conditions to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 Sample cases may be generated and may not actually exist in the training data. The returned case is representative of the specified content node.  
  
 Although the [!INCLUDE[msCoName](../includes/msconame-md.md)] Sequence Clustering algorithm is the only [!INCLUDE[msCoName](../includes/msconame-md.md)] algorithm that supports using SELECT FROM \<model>.SAMPLE_CASES, third-party algorithms may also support it.  
  
## Examples  
 The following example returns sample cases that are used to train the Target Mail mining model. Using the [IsInNode &#40;DMX&#41;](../dmx/isinnode-dmx.md) function in the **WHERE** clause returns only cases that are associated with the '000000003' node. The node string can be found in the NODE_UNIQUE_NAME column of the schema rowset.  
  
```  
Select * from [Sequence Clustering].SAMPLE_Cases  
WHERE IsInNode('000000003')  
```  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
