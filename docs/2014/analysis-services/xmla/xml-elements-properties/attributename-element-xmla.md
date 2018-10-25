---
title: "AttributeName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#AttributeName"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#AttributeName"
  - "microsoft.xml.analysis.attributename"
helpviewer_keywords: 
  - "AttributeName element"
ms.assetid: 4dc8260b-522e-46d9-9bd8-22a5a0068982
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeName Element (XMLA)
  Contains the name of an attribute represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <AttributeName>...</AttributeName>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](attribute-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](../xml-elements-commands/drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Where Element &#40;XMLA&#41;](where-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
