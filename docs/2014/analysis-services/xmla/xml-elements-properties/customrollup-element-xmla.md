---
title: "CustomRollup Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CustomRollup Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#CustomRollup"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CustomRollup"
  - "microsoft.xml.analysis.customrollup"
helpviewer_keywords: 
  - "CustomRollup element"
ms.assetid: b266ac8b-1ae7-461c-9127-3c5642f829f5
author: minewiskan
ms.author: owend
manager: craigg
---
# CustomRollup Element (XMLA)
  Contains the custom rollup formula for an attribute member represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <CustomRollup>...</CustomRollup>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](attribute-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `CustomRollup` element contains a Multidimensional Expressions (MDX) expression that defines the rollup behavior of the attribute member defined by the parent `Attribute` element.  
  
 For more information about MDX expressions, see [Expressions &#40;MDX&#41;](/sql/mdx/expressions-mdx).  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
