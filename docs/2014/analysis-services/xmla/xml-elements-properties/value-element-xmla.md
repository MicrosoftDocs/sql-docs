---
title: "Value Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Value Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.value"
  - "urn:schemas-microsoft-com:xml-analysis#Value"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Value"
helpviewer_keywords: 
  - "Value element"
ms.assetid: f87ca7f8-d9fe-4730-a706-5d50fcfe21df
author: minewiskan
ms.author: owend
manager: craigg
---
# Value Element (XMLA)
  Contains the desired value of an [Attribute](attribute-element-xmla.md) element to be added by an [Insert](../xml-elements-commands/insert-element-xmla.md) command, or a [Cell](cell-element-xmla.md) element to be updated by an [UpdateCells](../xml-elements-commands/updatecells-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Attribute> <!-- or Cell --!>  
   ...  
   <Value>...</Value>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](attribute-element-xmla.md), [Cell](cell-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For `Attribute` elements, the `Value` element contains the desired value that the member should contain after the `Insert` command is committed. For more information about inserting members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
 For `Cell` elements, the `Value` element contains the desired value that the cell should contain after the `UpdateCells` command is committed. The actual value stored in the writeback table for that cell is the difference between the original value of the cell and the desired value of the cell.  
  
 The data type used by this element should match the data type of the cell to be updated.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [CellOrdinal Element &#40;XMLA&#41;](cellordinal-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [UpdateCells Element &#40;XMLA&#41;](../xml-elements-commands/updatecells-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
