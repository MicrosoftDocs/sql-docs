---
title: "DefaultValue Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DefaultValue Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DefaultValue"
helpviewer_keywords: 
  - "DefaultValue element"
ms.assetid: 87e964a3-f317-46c3-98c7-b3621765c77b
author: minewiskan
ms.author: owend
manager: craigg
---
# DefaultValue Element (ASSL)
  Contains the read-only default value of the associated [ServerProperty](../objects/serverproperty-element-assl.md) element.  
  
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
|Parent elements|[ServerProperty](../objects/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This element contains the read-only installation default value of the `ServerProperty` for the current instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. The default value is supplied by the instance, and typically cannot be changed.  
  
 The element that corresponds to the parent of `DefaultValue` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [ServerProperties Element &#40;ASSL&#41;](../collections/serverproperties-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../objects/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
