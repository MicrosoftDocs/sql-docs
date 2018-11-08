---
title: "Aggregate Transformation Editor (Advanced Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.aggregationtransformation.advanced.f1"
helpviewer_keywords: 
  - "Aggregate Transformation Editor"
ms.assetid: 186a9736-2554-40a0-9cb2-877a8db5fde8
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Aggregate Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Aggregate Transformation Editor** dialog box to set component properties, specify aggregations, and set properties of input and output columns.  
  
> [!NOTE]  
>  The options for key count, key scale, distinct key count, and distinct key scale apply at the component level when specified on the **Advanced** tab, at the output level when specified in the advanced display of the **Aggregations** tab, and at the column level when specified in the column list at the bottom of the **Aggregations** tab.  
>   
>  In the Aggregate transformation, **Keys** and **Keys scale** refer to the number of groups that are expected to result from a **Group by** operation. **Count distinct keys** and **Count distinct scale** refer to the number of distinct values that are expected to result from a **Distinct count** operation.  
  
 To learn more about the Aggregate transformation, see [Aggregate Transformation](data-flow/transformations/aggregate-transformation.md).  
  
## Options  
 **Keys scale**  
 Optionally specify the approximate number of keys that the aggregation expects. The transformation uses this information to optimize its initial cache size. By default, the value of this option is **Unspecified**. If both **Keys scale** and **Number of keys** are specified, **Number of keys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The **Keys scale** property is not used.|  
|Low|Aggregation can write approximately 500,000 keys.|  
|Medium|Aggregation can write approximately 5,000,000 keys.|  
|High|Aggregation can write more than 25,000,000 keys.|  
  
 **Number of keys**  
 Optionally specify the exact number of keys that the aggregation expects. The transformation uses this information to optimize its initial cache size. If both **Keys scale** and **Number of keys** are specified, **Number of keys** takes precedence.  
  
 **Count distinct scale**  
 Optionally specify the approximate number of distinct values that the aggregation can write. By default, the value of this option is **Unspecified**. If both **Count distinct scale** and **Count distinct keys** are specified, **Count distinct keys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The CountDistinctScale property is not used.|  
|Low|Aggregation can write approximately 500,000 distinct values.|  
|Medium|Aggregation can write approximately 5,000,000 distinct values.|  
|High|Aggregation can write more than 25,000,000 distinct values.|  
  
 **Count distinct keys**  
 Optionally specify the exact number of distinct values that the aggregation can write. If both **Count distinct scale** and **Count distinct keys** are specified, **Count distinct keys** takes precedence.  
  
 **Auto extend factor**  
 Use a value between 1 and 100 to specify the percentage by which memory can be extended during the aggregation. By default, the value of this option is **25%**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Aggregate Transformation Editor &#40;Aggregations Tab&#41;](../../2014/integration-services/aggregate-transformation-editor-aggregations-tab.md)   
 [Aggregate Values in a Dataset by Using the Aggregate Transformation](data-flow/transformations/aggregate-values-in-a-dataset-by-using-the-aggregate-transformation.md)  
  
  
