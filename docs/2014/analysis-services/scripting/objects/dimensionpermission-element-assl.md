---
title: "DimensionPermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DimensionPermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DimensionPermission"
helpviewer_keywords: 
  - "DimensionPermission element"
ms.assetid: e06efbda-64fd-4dca-a2b5-c8ffbf21512c
author: minewiskan
ms.author: owend
manager: craigg
---
# DimensionPermission Element (ASSL)
  Defines the permissions that belong to a particular [Role](role-element-assl.md) element for a specific database dimension or cube dimension.  
  
## Syntax  
  
```xml  
  
<DimensionPermissions>  
   <DimensionPermission xsi:type="DimensionPermission">...</DimensionPermission> <!-- ancestor: Dimension -->  
   <!-- or -->  
   <DimensionPermission xsi:type="CubeDimensionPermission">...</DimensionPermission> <!-- ancestor: CubePermission -->  
</DimensionPermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length||  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur once or more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Dimension](../data-type/permission-data-type-assl.md)|  
|[CubePermission](../data-type/cubedimensionpermission-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionPermissions](../collections/dimensionpermissions-element-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionPermission> and <xref:Microsoft.AnalysisServices.CubeDimensionPermission>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
