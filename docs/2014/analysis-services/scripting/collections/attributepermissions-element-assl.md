---
title: "AttributePermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributePermissions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributePermissions"
helpviewer_keywords: 
  - "AttributePermissions element"
ms.assetid: ac703177-5936-440e-b1a5-a254a89258bc
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributePermissions Element (ASSL)
  Contains the collection of attribute permissions for an individual [Role](../objects/role-element-assl.md) element on a specific dimension of a [Cube](../objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubeDimensionPermission> <!-- or DimensionPermission -->  
   <AttributePermissions>  
      <AttributePermission>...</AttributePermission>  
      </AttributePermissions>  
</CubeDimensionPermission>  
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
|Parent elements|[CubeDimensionPermission](../data-type/permission-data-type-assl.md), [DimensionPermission](../objects/dimensionpermission-element-assl.md)|  
|Child elements|[AttributePermission](../objects/attributepermission-element-assl.md)|  
  
## Remarks  
 For `DimensionPermission`, this collection can contain only one `AttributePermission` per attribute.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributePermissionCollection>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
