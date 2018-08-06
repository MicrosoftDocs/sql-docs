---
title: "CellPermissions Element (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# CellPermissions Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the collection of permissions for cells in the associated [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubePermission>  
   ...  
   <CellPermissions>  
      <CellPermission>...</CellPermission>  
   </CellPermissions>  
   ...  
</CubePermission>  
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
|Parent elements|[CubePermission](../../../analysis-services/scripting/objects/cubepermission-element-assl.md)|  
|Child elements|[CellPermission](../../../analysis-services/scripting/objects/cellpermission-element-assl.md)|  
  
## Remarks  
 The collection can contain up to one **CellPermission** object for each allowed value of the [Access](../../../analysis-services/scripting/properties/access-element-assl.md) element.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CellPermissionCollection>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  
