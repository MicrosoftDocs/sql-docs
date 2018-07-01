---
title: "DeleteWithDescendants Element (XMLA) | Microsoft Docs"
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
# DeleteWithDescendants Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Indicates whether the descendants of attribute members are also deleted by the parent [Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Drop>  
   ...  
   <DeleteWithDescendants>...</DeleteWithDescendants>  
   ...  
</Drop>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DeleteWithDescendants** element determines whether the **Drop** command should delete the attribute members identified by the [Where](../../../analysis-services/xmla/xml-elements-properties/where-element-xmla.md) element, but also that the descendants of those attribute members are to be dropped as well.  
  
> [!NOTE]  
>  This element applies only to attribute members in parent-child hierarchies.  
  
 For more information about deleting and updating attribute members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
