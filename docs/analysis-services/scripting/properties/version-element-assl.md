---
title: "Version Element (ASSL) | Microsoft Docs"
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
# Version Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the read-only version number of the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] represented by the [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Server>  
      ...  
      <Version>...</Version>  
   ...  
</Server>  
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
|Parent element|[Server](../../../analysis-services/scripting/objects/server-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **Version** element describes which version of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is installed.  
  
 The element that corresponds to the parent of **Version** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Server>.  
  
## See Also  
 [Edition Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/edition-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
