---
title: "UpdateCells Element (XMLA) | Microsoft Docs"
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
  - "UpdateCells Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.updatecells"
  - "urn:schemas-microsoft-com:xml-analysis#UpdateCells"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#UpdateCells"
helpviewer_keywords: 
  - "UpdateCells command"
ms.assetid: 18336a35-8a46-4532-9ee7-71828b2982af
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
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
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md)|  
  
## Remarks  
 The **UpdateCells** command updates cells in a cube that supports cell writeback.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  