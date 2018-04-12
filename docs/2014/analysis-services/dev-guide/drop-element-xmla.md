---
title: "Drop Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Drop Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Drop"
  - "microsoft.xml.analysis.drop"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Drop"
helpviewer_keywords: 
  - "Drop element"
ms.assetid: a5d21db3-743a-4958-b16d-b6816a5ee787
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Drop Element (XMLA)
  Deletes attribute members from a dimension.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Drop>  
      <Object>...</Object>  
      <DeleteWithDescendants>...</DeleteWithDescendants>  
      <Where>...</Where>  
   </Drop>  
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
|Parent elements|[Command](../../../2014/analysis-services/dev-guide/command-element-xmla.md)|  
|Child elements|[DeleteWithDescendants](../../../2014/analysis-services/dev-guide/deletewithdescendants-element-xmla.md), [Object](../../../2014/analysis-services/dev-guide/object-element-dimension-xmla.md), [Where](../../../2014/analysis-services/dev-guide/where-element-xmla.md)|  
  
## Remarks  
 The `Drop` command deletes attribute members from a write-enabled dimension.  
  
 For more information about deleting members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/update-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/commands-xmla.md)  
  
  