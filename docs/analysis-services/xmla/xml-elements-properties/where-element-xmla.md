---
title: "Where Element (XMLA) | Microsoft Docs"
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
# Where Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Defines a filter condition used by the parent [Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md) or [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md) command.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)|  
|Child elements|[Attributes](../../../analysis-services/xmla/xml-elements-properties/attributes-element-xmla.md)|  
  
## Remarks  
 For **Drop** commands, the **Where** element, combined with the [DeleteWithDescendants](../../../analysis-services/xmla/xml-elements-properties/deletewithdescendants-element-xmla.md) element, identifies the scope of attribute members to be dropped.  
  
 For **Update** commands, the **Where** element identifies the scope of attribute members to be updated. Multiple attribute members can be updated by using a combination of attributes included in the **Attributes** collection of the parent **Update** command and the **Attributes** collection of the **Where** element.  
  
 For more information about deleting and updating attribute members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
