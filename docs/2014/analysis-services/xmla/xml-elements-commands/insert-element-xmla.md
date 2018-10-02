---
title: "Insert Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Insert Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Insert"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Insert"
  - "microsoft.xml.analysis.insert"
helpviewer_keywords: 
  - "Insert command"
ms.assetid: d1137033-cc19-4bcb-b93d-8575f17bea6b
author: minewiskan
ms.author: owend
manager: craigg
---
# Insert Element (XMLA)
  Inserts attribute members into a dimension.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Insert>  
      <Object>...</Object>  
      <Attributes>...</Attributes>  
   </Insert>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Attributes](../xml-elements-properties/attributes-element-xmla.md), [Object](../xml-elements-properties/object-element-dimension-xmla.md)|  
  
## Remarks  
 The `Insert` command inserts new attribute members into a write-enabled dimension.  
  
 For more information about deleting members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](drop-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](update-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
