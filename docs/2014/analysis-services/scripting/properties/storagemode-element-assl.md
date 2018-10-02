---
title: "StorageMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "StorageMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "StorageMode"
helpviewer_keywords: 
  - "StorageMode element"
ms.assetid: 197e8153-1ab6-43ba-a7e9-ae9be19ac511
author: minewiskan
ms.author: owend
manager: craigg
---
# StorageMode Element (ASSL)
  Determines the storage mode for the parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Dimension, MeasureGroup, Partition -->  
   ...  
   <StorageMode>...</StorageMode>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*MOLAP*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md), [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md), [MeasureGroup Element &#40;ASSL&#41;](../objects/group-element-assl.md), [Partition Element &#40;ASSL&#41;](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*MOLAP*|The parent uses multidimensional OLAP (MOLAP) storage.|  
|*ROLAP*|The parent uses relational OLAP (ROLAP) storage.|  
|*HOLAP*|The parent uses hybrid OLAP (HOLAP) storage. **Note:**  This value is not valid for [Dimension](../objects/dimension-element-assl.md) parent elements.|  
|*InMemory*|The parent uses IMBI storage.|  
  
 The enumeration that corresponds to the allowed values for `StorageMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.StorageMode>.  
  
 The elements that correspond to the parents of `StorageMode` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
