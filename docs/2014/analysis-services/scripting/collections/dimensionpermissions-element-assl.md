---
title: "DimensionPermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DimensionPermissions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DimensionPermissions"
helpviewer_keywords: 
  - "DimensionPermissions element"
ms.assetid: cb9fdfbf-2118-423b-ba02-fa36813dbea0
author: minewiskan
ms.author: owend
manager: craigg
---
# DimensionPermissions Element (ASSL)
  Contains the collection of permissions applicable to a [Dimension](../objects/dimension-element-assl.md) element or a [CubePermission](../objects/cubepermission-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Dimension> <!-- or CubePermission -->  
   ...  
   <DimensionPermissions>  
            <DimensionPermission>...</DimensionPermission> <!-- parent: Dimension -->  
      <!-- or -->  
      <DimensionPermission xsi:type="CubeDimensionPermission">...</DimensionPermission> <!-- parent: CubePermission -->  
      </DimensionPermissions>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubePermission](../objects/cubepermission-element-assl.md), [Dimension](../objects/dimension-element-assl.md)|  
|Child elements|[DimensionPermission](../objects/dimensionpermission-element-assl.md)|  
  
## Remarks  
 For `CubePermission` elements, `DimensionPermission` elements in this collection override permissions specified in the `DimensionPermissions` collection of each dimension explicitly referenced. If a dimension is not referenced in this collection, then the `CubePermission` element inherits the permissions specified in the `DimensionPermissions` collection of the dimension.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionPermissionCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
