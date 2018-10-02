---
title: "SkippedLevels Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SkippedLevels Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#SkippedLevels"
  - "microsoft.xml.analysis.skippedlevels"
  - "urn:schemas-microsoft-com:xml-analysis#SkippedLevels"
helpviewer_keywords: 
  - "SkippedLevels element"
ms.assetid: 4887b557-0ffc-4f42-b6b9-c98ad1208ca5
author: minewiskan
ms.author: owend
manager: craigg
---
# SkippedLevels Element (XMLA)
  Contains the number of levels skipped by an attribute member represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <SkippedLevels>...</SkippedLevels>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|0|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](attribute-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `SkippedLevels` element determines the number of levels skipped by an attribute member defined by the parent `Attribute` element.  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
