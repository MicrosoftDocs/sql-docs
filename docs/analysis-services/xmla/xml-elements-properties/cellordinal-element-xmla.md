---
title: "CellOrdinal Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "CellOrdinal Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.cellordinal"
  - "urn:schemas-microsoft-com:xml-analysis#CellOrdinal"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CellOrdinal"
helpviewer_keywords: 
  - "CellOrdinal element"
ms.assetid: 1808c498-e3b4-4e5c-9e22-7f8662d32874
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# CellOrdinal Element (XMLA)
  Contains the ordinal position within a cube of a cell to be updated by an [UpdateCells](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Cell>  
   ...  
   <CellOrdinal>...</CellOrdinal>  
   ...  
</Cell>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **CellOrdinal** element identifies the cell to be updated by the **UpdateCells** command.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [Value Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/value-element-xmla.md)   
 [UpdateCells Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  