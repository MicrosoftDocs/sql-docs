---
title: "Cell Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Cell Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.cell"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Cell"
  - "urn:schemas-microsoft-com:xml-analysis#Cell"
helpviewer_keywords: 
  - "Cell element"
ms.assetid: 88daba54-89e9-423f-8d12-8de80cf52d6b
author: minewiskan
ms.author: owend
manager: craigg
---
# Cell Element (XMLA)
  Contains information about a cell to be updated by an [UpdateCells](../xml-elements-commands/updatecells-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<UpdateCells>  
   ...  
   <Cell CellOrdinal="Long">  
      <Value>...</Value>  
   </Cell>  
   ...  
</UpdateCells>  
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
|Parent elements|[UpdateCells](../xml-elements-commands/updatecells-element-xmla.md)|  
|Child elements|[Value](value-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|CellOrdinal|Required `Long` attribute. Contains the zero-based ordinal position of the cell to be updated.|  
  
## Remarks  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
