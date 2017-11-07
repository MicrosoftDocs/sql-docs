---
title: "Value Element (XMLA) | Microsoft Docs"
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
  - "Value Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.value"
  - "urn:schemas-microsoft-com:xml-analysis#Value"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Value"
helpviewer_keywords: 
  - "Value element"
ms.assetid: f87ca7f8-d9fe-4730-a706-5d50fcfe21df
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Value Element (XMLA)
  Contains the desired value of an [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) element to be added by an [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md) command, or a [Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md) element to be updated by an [UpdateCells](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md) command.  
  
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
|Parent elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md), [Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Attribute** elements, the **Value** element contains the desired value that the member should contain after the **Insert** command is committed. For more information about inserting members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
 For **Cell** elements, the **Value** element contains the desired value that the cell should contain after the **UpdateCells** command is committed. The actual value stored in the writeback table for that cell is the difference between the original value of the cell and the desired value of the cell.  
  
 The data type used by this element should match the data type of the cell to be updated.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See Also  
 [CellOrdinal Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/cellordinal-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [UpdateCells Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  