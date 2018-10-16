---
title: "CustomRollupProperties Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CustomRollupProperties Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.customrollupproperties"
  - "urn:schemas-microsoft-com:xml-analysis#CustomRollupProperties"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CustomRollupProperties"
helpviewer_keywords: 
  - "CustomRollupProperties element"
ms.assetid: 4abf0129-e529-4355-b8d5-6f4e6a88e796
author: minewiskan
ms.author: owend
manager: craigg
---
# CustomRollupProperties Element (XMLA)
  Contains the custom rollup properties for an attribute member represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <CustomRollupProperties>...</CustomRollupProperties>  
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
 The `CustomRollupProperties` element contains a Multidimensional Expressions (MDX) expression that defines the custom rollup properties of the attribute member defined by the parent `Attribute` element.  
  
 For more information about MDX expressions, see [Expressions &#40;MDX&#41;](/sql/mdx/expressions-mdx).  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
