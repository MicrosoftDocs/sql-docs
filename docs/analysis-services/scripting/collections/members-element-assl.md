---
title: "Members Element (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Members Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the collection of [Member](../../../analysis-services/scripting/objects/member-element-assl.md) elements of the parent element.  
  
## Syntax  
  
```xml  
  
<Group> <!-- or Role --<  
   ...  
   <Members>  
      <Member>...</Member>  
   </Members>  
   ...  
</Group>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Group](../../../analysis-services/scripting/objects/group-element-assl.md), [Role](../../../analysis-services/scripting/objects/role-element-assl.md)|  
|Child elements|[Member](../../../analysis-services/scripting/objects/member-element-assl.md)|  
  
## Remarks  
 The elements that correspond to the parents of **Members** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Group> and <xref:Microsoft.AnalysisServices.Role>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  
