---
title: "Dimension Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Dimension Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Dimension"
  - "urn:schemas-microsoft-com:xml-analysis#Dimension"
  - "microsoft.xml.analysis.dimension"
helpviewer_keywords: 
  - "Dimension element"
ms.assetid: 85093468-e971-4b8e-9ee4-7b264ad01711
author: minewiskan
ms.author: owend
manager: craigg
---
# Dimension Element (XMLA)
  Identifies the cube dimension represented by the parent [Object](object-element-dimension-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Object>  
   ...  
   <Dimension>...</Dimension>  
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
 The `Dimension` element is an object identifier that contains the name of the cube dimension represented by the `Object` element.  
  
## See Also  
 [Database Element &#40;XMLA&#41;](database-element-xmla.md)   
 [Dimension Element (XMLA)](dimension-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
