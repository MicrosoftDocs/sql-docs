---
title: "CellPermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "CellPermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CellPermission"
helpviewer_keywords: 
  - "CellPermission element"
ms.assetid: 54a6afc0-1fcb-4b24-851a-6d81e1fe0efc
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CellPermission Element (ASSL)
  Describes the permissions that members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on individual cells within a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CellPermissions>  
   <CellPermission>  
      <Access>...</Access>  
            <Description>...</Description>  
      <Expression>...</Expression>  
      <Annotations>...</Annotations>  
   </CellPermission>  
</CellPermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CellPermissions](../../../2014/analysis-services/dev-guide/cellpermissions-element-assl.md)|  
|Child elements|[Access](../../../2014/analysis-services/dev-guide/access-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Expression](../../../2014/analysis-services/dev-guide/expression-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CellPermission>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  