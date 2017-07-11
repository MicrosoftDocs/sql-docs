---
title: "DimensionPermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DimensionPermissions Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DimensionPermissions"
helpviewer_keywords: 
  - "DimensionPermissions element"
ms.assetid: cb9fdfbf-2118-423b-ba02-fa36813dbea0
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DimensionPermissions Element (ASSL)
  Contains the collection of permissions applicable to a [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) element or a [CubePermission](../../../analysis-services/scripting/objects/cubepermission-element-assl.md) element.  
  
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
|Parent elements|[CubePermission](../../../analysis-services/scripting/objects/cubepermission-element-assl.md), [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md)|  
|Child elements|[DimensionPermission](../../../analysis-services/scripting/objects/dimensionpermission-element-assl.md)|  
  
## Remarks  
 For **CubePermission** elements, **DimensionPermission** elements in this collection override permissions specified in the **DimensionPermissions** collection of each dimension explicitly referenced. If a dimension is not referenced in this collection, then the **CubePermission** element inherits the permissions specified in the **DimensionPermissions** collection of the dimension.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionPermissionCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  