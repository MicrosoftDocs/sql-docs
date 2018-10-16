---
title: "Cube Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Cube Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.cube"
  - "urn:schemas-microsoft-com:xml-analysis#Cube"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Cube"
helpviewer_keywords: 
  - "Cube element"
ms.assetid: 2e8662f4-fb2e-43af-b70a-9e0b5872c9b9
author: minewiskan
ms.author: owend
manager: craigg
---
# Cube Element (XMLA)
  Identifies the cube that contains the dimension represented by the parent [Object](object-element-dimension-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Object>  
   ...  
   <Cube>...</Cube>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](object-element-dimension-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Cube` element is an object identifier that contains the name of the cube that contains the dimension represented by the `Object` element.  
  
## See Also  
 [Database Element &#40;XMLA&#41;](database-element-xmla.md)   
 [Dimension Element &#40;XMLA&#41;](dimension-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
