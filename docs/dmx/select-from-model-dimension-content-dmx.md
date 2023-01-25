---
title: "SELECT FROM &lt;model&gt;.DIMENSION_CONTENT (DMX)"
description: "SELECT FROM &lt;model&gt;.DIMENSION_CONTENT (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# SELECT FROM &lt;model&gt;.DIMENSION_CONTENT (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  A mining model can be used as a dimension in an OLAP cube, where each node in the model is represented as a member of the dimension. **The SELECT FROM \<model>.Dimension_CONTENT** statement returns the content of the model that pertains to its usage as a dimension.  
  
## Syntax  
  
```  
  
SELECT [FLATTENED] [TOP <n>] <expression list> FROM <model>.Dimension_CONTENT   
[WHERE <condition expression>]  
[ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *expression list*  
 A comma-separated list of related column identifiers derived from the content schema rowset.  
  
 *model*  
 A model identifier.  
  
 *condition expression*  
 Optional. A condition to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 Algorithm providers define which content is returned, and how to organize it. For example, the provider might limit the number of nodes that are described in the dimension content.  
  
 The following table lists the columns that can be queried for the dimension content, and the function that each column performs as a data mining dimension.  
  
|CONTENT rowset column|Function in data mining dimension|  
|---------------------------|---------------------------------------|  
|ATTRIBUTE_NAME|Member property.|  
|NODE_NAME|Member property.|  
|NODE_UNIQUE_NAME|Key attribute.|  
|NODE_TYPE|Member property.|  
|NODE_CAPTION|CaptionColumn for **Key** attribute.|  
|CHILDREN_CARDINALITY|Member property.|  
|PARENT_UNIQUE_NAME|RelatedAttribute for **Key** attribute (ParentAttribute in parent-child hierarchy).|  
|NODE_DESCRIPTION|Member property.|  
|NODE_RULE|Member property.|  
|MARGINAL_RULE|Member property.|  
|NODE_PROBABILITY|Member property.|  
|MARGINAL_PROBABILITY|Member property.|  
|NODE_SUPPORT|Member property.|  
  
## Examples  
  
### Description  
 The example selects all columns from the `[TM Decision Tree]` model content that pertain to using the model as a dimension.  
  
### Code  
  
```  
SELECT *   
FROM [TM Decision Tree].Dimension_Content  
```  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
