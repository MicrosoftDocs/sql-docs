---
title: "CurrentStorageMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "CurrentStorageMode Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CurrentStorageMode"
helpviewer_keywords: 
  - "CurrentStorageMode element"
ms.assetid: 050c21e4-368b-4ff0-b0c5-349f93fe9747
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md), [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **CurrentStorageMode** element indicates the storage mode currently in use for proactive caching purposes, and applies to all attributes of the parent element.  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*MOLAP*|The parent uses multidimensional OLAP (MOLAP) storage.|  
|*ROLAP*|The parent uses relational OLAP (ROLAP) storage.|  
|*HOLAP*|The parent uses hybrid OLAP (HOLAP) storage.<br /><br /> Note: This value is only valid for [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md) parent elements.|  
  
 The enumeration corresponding to the allowed values **CurrentStorageMode** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.StorageMode>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  