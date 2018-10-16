---
title: "ProductName Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProductName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ProductName"
helpviewer_keywords: 
  - "ProductName element"
ms.assetid: f8129bb2-55c9-44e1-8857-82dc01c04a7f
author: minewiskan
ms.author: owend
manager: craigg
---
# ProductName Element (ASSL)
  Contains the read-only product name of the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that is associated with a [Server](../objects/server-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Server>  
      ...  
      <ProductName>...</ProductName>  
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
 The `ProductName` element provides read-only access to the name of the product associated with an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 The element that corresponds to the parent of `ProductName` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Server>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
