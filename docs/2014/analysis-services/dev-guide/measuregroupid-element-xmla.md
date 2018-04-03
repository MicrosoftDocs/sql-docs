---
title: "MeasureGroupID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "MeasureGroupID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#MeasureGroupID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#MeasureGroupID"
  - "microsoft.xml.analysis.measuregroupid"
helpviewer_keywords: 
  - "MeasureGroupID element"
ms.assetid: ff55777e-54ea-42b9-a084-2e12e0a10988
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# MeasureGroupID Element (XMLA)
  Identifies a measure group within a parent element that contains an object reference.  
  
## Syntax  
  
```xml  
  
<Object> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <MeasureGroupID>...</MeasureGroupID>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|[Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md) = 1-1: Required element that occurs once and only once.|  
|Cardinality|All others = 0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md), [ParentObject](../../../2014/analysis-services/dev-guide/parentobject-element-xmla.md), [Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  