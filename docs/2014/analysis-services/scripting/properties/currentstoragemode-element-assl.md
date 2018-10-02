---
title: "CurrentStorageMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CurrentStorageMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CurrentStorageMode"
helpviewer_keywords: 
  - "CurrentStorageMode element"
ms.assetid: 050c21e4-368b-4ff0-b0c5-349f93fe9747
author: minewiskan
ms.author: owend
manager: craigg
---
# CurrentStorageMode Element (ASSL)
  Determines the current storage mode for the parent element.  
  
## Syntax  
  
```xml  
  
<Dimension> <!-- or Partition -->  
   <CurrentStorageMode>...</CurrentStorageMode>  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*ROLAP*|  
|Cardinality|0-1: Optional element that can occur once or not at all.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Dimension](../objects/dimension-element-assl.md), [Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `CurrentStorageMode` element indicates the storage mode currently in use for proactive caching purposes, and applies to all attributes of the parent element.  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*MOLAP*|The parent uses multidimensional OLAP (MOLAP) storage.|  
|*ROLAP*|The parent uses relational OLAP (ROLAP) storage.|  
|*HOLAP*|The parent uses hybrid OLAP (HOLAP) storage. **Note:**  This value is only valid for [Partition](../objects/partition-element-assl.md) parent elements.|  
  
 The enumeration corresponding to the allowed values `CurrentStorageMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.StorageMode>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
