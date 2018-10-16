---
title: "ReadSourceData Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ReadSourceData Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ReadSourceData element"
ms.assetid: 7da4665a-fba3-4aae-8dee-678dc14d3b05
author: minewiskan
ms.author: owend
manager: craigg
---
# ReadSourceData Element (ASSL)
  Determines how unique names are generated for hierarchies that are contained within the [CubePermission](../objects/cubepermission-element-assl.md).  
  
## Syntax  
  
```xml  
  
<CubePermission>  
   ...  
   <ReadSourceData>...</ReadSourceData>  
   ...  
</CubePermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubePermission](../objects/cubepermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|No access is permitted to data available on calculation pass 0.|  
|*Allowed*|Access is permitted to data available on calculation pass 0.|  
  
## Remarks  
 The element that corresponds to the parent of `ReadSourceData` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubePermission>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
