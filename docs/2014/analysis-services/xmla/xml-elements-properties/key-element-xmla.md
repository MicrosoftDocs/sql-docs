---
title: "Key Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Key Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Key"
  - "urn:schemas-microsoft-com:xml-analysis#Key"
  - "microsoft.xml.analysis.key"
helpviewer_keywords: 
  - "Key element"
ms.assetid: 09d3cd48-49f7-4b58-b8bb-ca75b81bb02f
author: minewiskan
ms.author: owend
manager: craigg
---
# Key Element (XMLA)
  Contains a member key value for an attribute member.  
  
## Syntax  
  
```xml  
  
<Keys>  
   ...  
   <Key>...</Key>  
   ...  
</Keys>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Keys](keys-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The data type used by this element should match the data type of the appropriate key column of the specified attribute. If `Key` elements are not specified for a parent `Attribute` element, the `AttributeName` and `Name` elements specified in the parent `Attribute` element are used to identify the attribute member to be modified.  
  
## See Also  
 [Attribute Element &#40;XMLA&#41;](attribute-element-xmla.md)   
 [AttributeName Element &#40;XMLA&#41;](name-element-xmla.md)   
 [Drop Element &#40;XMLA&#41;](../xml-elements-commands/drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [KeyColumn Element &#40;ASSL&#41;](../../scripting/objects/column-element-assl.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Where Element &#40;XMLA&#41;](where-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
