---
title: "Optionality Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Optionality Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Indicates the optionality of the members for an [AttributeRelationship](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AttributeRelationship>  
   ...  
   <Optionality>...</OPtionality>  
   ...  
</AttributeRelationship>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Mandatory*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributeRelationship](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Mandatory*|Each member in the related attribute has to be associated with at least one member in the attribute that owns the **AttributeRelationship** element.|  
|*Optional*|Each member in the related attribute does not have to be associated with at least one member in the attribute that owns the **AttributeRelationship** element.|  
  
 The enumeration that corresponds to the allowed values for **Cardinality** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Optionality>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
