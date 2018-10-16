---
title: "Where Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Where Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Where"
  - "microsoft.xml.analysis.where"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Where"
helpviewer_keywords: 
  - "Where element"
ms.assetid: 81fb4190-3379-4ddf-8795-a0772f3b92bb
author: minewiskan
ms.author: owend
manager: craigg
---
# Where Element (XMLA)
  Defines a filter condition used by the parent [Drop](../xml-elements-commands/drop-element-xmla.md) or [Update](../xml-elements-commands/update-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Drop> <!-- or Update -->  
   ...  
   <Where>  
      <Attributes>...</Attributes>  
   </Where>  
   ...  
</Insert>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Drop](../xml-elements-commands/drop-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md)|  
|Child elements|[Attributes](attributes-element-xmla.md)|  
  
## Remarks  
 For `Drop` commands, the `Where` element, combined with the [DeleteWithDescendants](deletewithdescendants-element-xmla.md) element, identifies the scope of attribute members to be dropped.  
  
 For `Update` commands, the `Where` element identifies the scope of attribute members to be updated. Multiple attribute members can be updated by using a combination of attributes included in the `Attributes` collection of the parent `Update` command and the `Attributes` collection of the `Where` element.  
  
 For more information about deleting and updating attribute members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
