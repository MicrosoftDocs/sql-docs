---
title: "CubePermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubePermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubePermission"
helpviewer_keywords: 
  - "CubePermission element"
ms.assetid: b144b623-ff20-4ead-91ad-4c718f3b140b
author: minewiskan
ms.author: owend
manager: craigg
---
# CubePermission Element (ASSL)
  Defines the permissions of the members of a particular [Role](role-element-assl.md) element in a specific [Cube](cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubePermissions>  
   <CubePermission>  
      <!-- The following elements extend Permission -->  
      <ReadSourceData>...</ReadSourceData>  
            <DimensionPermissions>...</DimensionPermissions>  
            <CellPermissions>...</CellPermissions>  
   </CubePermission>  
</CubePermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Permission](../data-type/permission-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur once or more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubePermissions](../collections/cubepermissions-element-assl.md)|  
|Child elements|[CellPermissions](../collections/cellpermissions-element-assl.md), [DimensionPermissions](../collections/dimensionpermissions-element-assl.md), [ReadSourceData](data-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubePermission>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](cube-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
