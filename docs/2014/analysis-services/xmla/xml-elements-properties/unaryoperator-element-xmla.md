---
title: "UnaryOperator Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "UnaryOperator Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#UnaryOperator"
  - "urn:schemas-microsoft-com:xml-analysis#UnaryOperator"
  - "microsoft.xml.analysis.unaryoperator"
helpviewer_keywords: 
  - "UnaryOperator element"
ms.assetid: 4dc9cfbe-6f8b-42bc-8d3a-42f48ca5d299
author: minewiskan
ms.author: owend
manager: craigg
---
# UnaryOperator Element (XMLA)
  Contains the unary operator for an attribute member represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <UnaryOperator>...</UnaryOperator>  
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
 The `UnaryOperator` element contains a Multidimensional Expressions (MDX) expression that defines the unary operator for the attribute member defined by the parent `Attribute` element.  
  
 For more information about MDX expressions, see [Expressions &#40;MDX&#41;](/sql/mdx/expressions-mdx).  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
