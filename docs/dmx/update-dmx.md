---
title: "UPDATE (DMX) | Microsoft Docs"
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
  - "UPDATE"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "NODE_CAPTION column"
  - "mining models [Analysis Services], content changes"
  - "modifying mining model content"
  - "UPDATE statement [SQL Server], DMX"
ms.assetid: 8a2b0942-c490-410c-b1cf-ff2e0fd8e24b
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# UPDATE (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the **NODE_CAPTION** column in the data mining model.  
  
## Syntax  
  
```  
  
UPDATE <model>.CONTENT  
SET NODE_CAPTION='new caption'  
[WHERE <condition expression>]  
```  
  
## Arguments  
 *model*  
 A model identifier.  
  
 *new caption*  
 A string that contains the new name for the **NODE_CAPTION** column.  
  
 *condition expression*  
 Optional. A condition to restrict the values that are returned from the column list.  
  
## Examples  
 In the following example, the **UPDATE** statement changes the default name, `Cluster 1`, for cluster `001` to the more descriptive name, `Likely Customers`.  
  
```  
UPDATE [TM Clustering].CONTENT  
SET NODE_CAPTION= 'Likely Customers'  
WHERE NODE_UNIQUE_NAME = '001'  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
