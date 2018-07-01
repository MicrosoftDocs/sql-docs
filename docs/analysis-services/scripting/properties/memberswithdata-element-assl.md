---
title: "MembersWithData Element (ASSL) | Microsoft Docs"
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
# MembersWithData Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Determines whether to display data members for non-leaf members in the parent attribute.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <MembersWithData>...</MembersWithData>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*NonLeafDataVisible*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **MembersWithData** element is used only by parent attributes (in other words, the value of the [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md) element of the **DimensionAttribute** parent element is set to *Parent*) to determine whether to display data members for non-leaf members in the parent attribute. For more information about data members, see [Attributes in Parent-Child Hierarchies](../../../analysis-services/multidimensional-models/parent-child-dimension-attributes.md).  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*NonLeafDataHidden*|Non-leaf data is hidden.|  
|*NonLeafDataVisible*|Non-leaf data is visible.|  
  
 The enumeration that corresponds to the allowed values for **MembersWithData** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MembersWithData>.  
  
## See Also  
 [MembersWithDataCaption Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/memberswithdatacaption-element-assl.md)   
 [DimensionAttribute Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
