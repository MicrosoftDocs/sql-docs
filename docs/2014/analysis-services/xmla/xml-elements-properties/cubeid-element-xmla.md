---
title: "CubeID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.cubeid"
  - "urn:schemas-microsoft-com:xml-analysis#CubeID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CubeID"
helpviewer_keywords: 
  - "CubeID element"
ms.assetid: 9dba605a-c45e-4730-827b-b7c55c8110da
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeID Element (XMLA)
  Identifies a cube within a parent element that contains an object reference.  
  
## Syntax  
  
```xml  
  
<Object> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <CubeID>...</CubeID>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
  
## Cardinality  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Source](source-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Target](../xml-elements-properties/target-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](object-element-xmla.md), [ParentObject](parentobject-element-xmla.md), [Source](source-element-xmla.md), [Target](../xml-elements-properties/target-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
