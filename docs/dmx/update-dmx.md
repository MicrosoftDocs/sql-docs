---
title: "UPDATE (DMX)"
description: "UPDATE (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# UPDATE (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

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
  
  
