---
title: "SourceAttributeID Element (ASSL) | Microsoft Docs"
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
# SourceAttributeID Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the identifier (ID) of the source attribute on which the [Level](../../../analysis-services/scripting/objects/level-element-assl.md) element is based.  
  
## Syntax  
  
```xml  
  
<Level>  
   ...  
   <SourceAttributeID>...</SourceAttributeID>  
   ...  
</Level>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Level](../../../analysis-services/scripting/objects/level-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of **SourceAttributeID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Level>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
