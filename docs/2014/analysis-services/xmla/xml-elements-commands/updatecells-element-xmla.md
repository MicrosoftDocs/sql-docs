---
title: "UpdateCells Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "UpdateCells Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.updatecells"
  - "urn:schemas-microsoft-com:xml-analysis#UpdateCells"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#UpdateCells"
helpviewer_keywords: 
  - "UpdateCells command"
ms.assetid: 18336a35-8a46-4532-9ee7-71828b2982af
author: minewiskan
ms.author: owend
manager: craigg
---
# UpdateCells Element (XMLA)
  Updates cells in a write-enabled cube.  
  
## Syntax  
  
```xml  
  
<Command>  
   <UpdateCells>  
      <Cell>...</Cell>  
   </UpdateCells>  
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
|Child elements|[Cell](../xml-elements-properties/cell-element-xmla.md)|  
  
## Remarks  
 The `UpdateCells` command updates cells in a cube that supports cell writeback.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](update-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
