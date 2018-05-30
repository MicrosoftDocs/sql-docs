---
title: "Value Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Value Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the desired value of an [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) element to be added by an [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md) command, or a [Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md) element to be updated by an [UpdateCells](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Attribute> <!-- or Cell --!>  
   ...  
   <Value>...</Value>  
   ...  
</Attribute>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md), [Cell](../../../analysis-services/xmla/xml-elements-properties/cell-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Attribute** elements, the **Value** element contains the desired value that the member should contain after the **Insert** command is committed. For more information about inserting members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
 For **Cell** elements, the **Value** element contains the desired value that the cell should contain after the **UpdateCells** command is committed. The actual value stored in the writeback table for that cell is the difference between the original value of the cell and the desired value of the cell.  
  
 The data type used by this element should match the data type of the cell to be updated.  
  
 For more information about updating cells, see [Updating Cells &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md).  
  
## See also
 [CellOrdinal Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/cellordinal-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [UpdateCells Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
