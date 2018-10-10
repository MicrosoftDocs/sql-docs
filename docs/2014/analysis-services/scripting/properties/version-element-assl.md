---
title: "Version Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Version Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Version"
helpviewer_keywords: 
  - "Version element"
ms.assetid: fb26fe5d-de40-443b-a8bc-031c950552e6
author: minewiskan
ms.author: owend
manager: craigg
---
# Version Element (ASSL)
  Contains the read-only version number of the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] represented by the [Server](../objects/server-element-assl.md) element.  
  
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
|Parent element|[Server](../objects/server-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Version` element describes which version of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is installed.  
  
 The element that corresponds to the parent of `Version` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Server>.  
  
## See Also  
 [Edition Element &#40;ASSL&#41;](edition-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
