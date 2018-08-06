---
title: "DefaultValue Element (ASSL) | Microsoft Docs"
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
# DefaultValue Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the read-only default value of the associated [ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ServerProperty>  
   ...  
   <DefaultValue>   </DefaultValue>  
   ...  
</ServerProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any simpleType|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This element contains the read-only installation default value of the **ServerProperty** for the current instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. The default value is supplied by the instance, and typically cannot be changed.  
  
 The element that corresponds to the parent of **DefaultValue** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [ServerProperties Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/serverproperties-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
