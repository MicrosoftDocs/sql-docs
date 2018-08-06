---
title: "Member Element (ASSL) | Microsoft Docs"
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
# Member Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the name of a member of a [Group](../../../analysis-services/scripting/objects/group-element-assl.md) element or of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Members>  
   <Member>  
      <Name>...</Name>  
   </Member>  
</Members>  
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
|Parent elements|[Members](../../../analysis-services/scripting/collections/members-element-assl.md)|  
|Child elements|[Name](../../../analysis-services/scripting/properties/name-element-assl.md)|  
  
## Remarks  
 The elements that correspond to the parents of **Member** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Group> and <xref:Microsoft.AnalysisServices.Role>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  
