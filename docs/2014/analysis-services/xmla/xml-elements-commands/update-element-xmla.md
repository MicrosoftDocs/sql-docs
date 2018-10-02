---
title: "Update Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Update Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Update"
  - "microsoft.xml.analysis.update"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Update"
helpviewer_keywords: 
  - "Update command [XMLA]"
ms.assetid: 324dcc16-865d-4d0a-b393-2b06c18ac807
author: minewiskan
ms.author: owend
manager: craigg
---
# Update Element (XMLA)
  Updates attribute members in a dimension.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Update>  
      <Object>...</Object>  
      <MoveWithDescendants>...</MoveWithDescendants>  
      <Attributes>...</Attributes>  
      <Where>...</Where>  
   </Update>  
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
|Child elements|[Attributes](../xml-elements-properties/attributes-element-xmla.md), [MoveWithDescendants](../xml-elements-properties/movewithdescendants-element-xmla.md), [Object](../xml-elements-properties/object-element-dimension-xmla.md), [Where](../xml-elements-properties/where-element-xmla.md)|  
  
## Remarks  
 The `Update` command moves attribute members within a write-enabled dimension.  
  
 For more information about updating members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](insert-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
