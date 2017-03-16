---
title: "CubePermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "CubePermissions Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CubePermissions"
helpviewer_keywords: 
  - "CubePermissions element"
ms.assetid: 75a3a0c2-e1d4-4896-b0f5-2ea9c769b927
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CubePermissions Element (ASSL)
  Contains the collection of permissions applicable to a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Cube>  
   ...  
   <CubePermissions>  
      <CubePermission>...</CubePermission>  
   </CubePermissions>  
   ...  
</Cube>  
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
|Parent elements|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md)|  
|Child elements|[CubePermission](../../../analysis-services/scripting/objects/cubepermission-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubePermissionCollection>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  