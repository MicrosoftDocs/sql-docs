---
title: "MoveWithDescendants Element (XMLA) | Microsoft Docs"
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
# MoveWithDescendants Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Indicates whether the descendants of attribute members are also updated by the parent [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Update>  
   ...  
   <MoveWithDescendants>...</MoveWithDescendants>  
   ...  
</Update>  
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
|Parent elements|[Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **MoveWithDescendants** element determines whether the **Update** command should not just update the attribute members identified by the [Attributes](../../../analysis-services/xmla/xml-elements-properties/attributes-element-xmla.md) element, but also that the descendants of those attribute members are to be updated as well.  
  
> [!NOTE]  
>  This element applies only to attribute members in parent-child hierarchies.  
  
 For more information about updating members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
